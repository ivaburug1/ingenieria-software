using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_BLL;
using _686DP_BE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using _686DP_SERVICIOS.Observer;
using System.Net;

namespace AseguraYa
{
    public partial class _686DP_frmCrearProducto : Form
    {
        string idi = "";

        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        int seleccion = 0;
        _686DP_ExpresionesRegulares _686DP_ExpresionesRegulares = new _686DP_ExpresionesRegulares();
        private int codigoPlanSeleccionado = -1;

        public _686DP_frmCrearProducto(string idiomaLocal)
        {
            InitializeComponent();
            DGPlan.CellClick += DGPlan_CellClick;
            idi = idiomaLocal;
            cambiarIdioma();
        }
        _686DPBLLSeguro bll = new _686DPBLLSeguro();
        _686DP_BLLPlan bllp = new _686DP_BLLPlan();
        _686DP_BLLCobertura bllc = new _686DP_BLLCobertura();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();

        private void _686DP_frmCrearProducto_Load(object sender, EventArgs e)
        {

            LMG.CargarMensajesGlobales(idi);
            CargarCombo();

            TXTDescripcionCobertura.Enabled = false;
            TXTFranquicia.Enabled = false;
            TXTProductos.Enabled = false;
            TXTSumaAsegurada.Enabled = false;

            BTNCrearProducto.Enabled = false;

            BTNCrearPlan.Enabled = false;
            BTNModificarPlan.Enabled= false;

            BTNCrearCobertura.Enabled= false;
            BTNAsociarPlan.Enabled= false;

            cmbProductos.Enabled = false;

            DGCobertura.ReadOnly = true;
            DGPlan.ReadOnly = true;

            cambiarIdioma();
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }

        private void CargarCombo()
        {
            cmbProductos.Items.Clear();
            List<string> productos = bll.TraerProductos();
            foreach (string producto in productos)
            {
                string traducido = LMG.Traducir(producto);
                string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                if (limpio == producto)
                {
                    cmbProductos.Items.Add(producto);
                }
                else
                {
                    cmbProductos.Items.Add(traducido);
                }
            }

            DGCobertura.DataSource = null;
            List<_686DP_Cobertura> Cobeturas = bllc.traerCoberturas();
            DGCobertura.DataSource = Cobeturas;
            DGCobertura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DGPlan.DataSource = null;
            List<_686DP_Plan> Planes = bllp.TraerPlanes();
            DGPlan.DataSource = Planes;
            DGPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in DGCobertura.Columns)
            {
                string traducido = LMG.Traducir(col.HeaderText);
                if (!string.IsNullOrEmpty(traducido))
                    col.HeaderText = traducido;
            }

            foreach (DataGridViewColumn col in DGPlan.Columns)
            {
                string traducido = LMG.Traducir(col.HeaderText);
                if (!string.IsNullOrEmpty(traducido))
                    col.HeaderText = traducido;
            }
        }

        private void RBCrearProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (RBCrearProducto.Checked)
            {
                TXTDescripcionCobertura.Enabled = false;
                TXTFranquicia.Enabled = false;
                TXTProductos.Enabled = true;
                TXTSumaAsegurada.Enabled = false;

                BTNCrearProducto.Enabled = true;

                BTNCrearPlan.Enabled = false;
                BTNModificarPlan.Enabled = false;

                BTNCrearCobertura.Enabled = false;
                BTNAsociarPlan.Enabled = false;

                cmbProductos.Enabled = false;

            }
            else
            {
                
                TXTDescripcionCobertura.Enabled = false;
                TXTFranquicia.Enabled = false;
                TXTProductos.Enabled = false;
                TXTSumaAsegurada.Enabled = false;

                BTNCrearProducto.Enabled = false;

                BTNCrearPlan.Enabled = false;
                BTNModificarPlan.Enabled = false;

                BTNCrearCobertura.Enabled = false;
                BTNAsociarPlan.Enabled = false;
            }
        }

        private void RBAgruparSeguro_CheckedChanged(object sender, EventArgs e)
        {
            if(RBAgruparSeguro.Checked)
            {
                
                TXTDescripcionCobertura.Enabled = true;
                TXTFranquicia.Enabled = true;
                TXTProductos.Enabled = false;
                TXTSumaAsegurada.Enabled = true;

                BTNCrearProducto.Enabled = false;

                BTNCrearPlan.Enabled = true;
                BTNModificarPlan.Enabled = true;

                BTNCrearCobertura.Enabled = true;
                BTNAsociarPlan.Enabled = true;

                cmbProductos.Enabled = true;
            }
        }

        private void BTNCrearProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (TXTProductos.Text != "")
                {
                    string nProducto = TXTProductos.Text;

                    bool existe = bll.VaidarProducto(nProducto);
                    if (!existe)
                    {
                        bll.CrearProucto(nProducto);
                        MessageBox.Show(LMG.Traducir("CreacionOk"));
                        cmbProductos.Items.Add(nProducto);
                        BLLDV.CalcularDigitoVerificador("Seguro");
                        int dniActual = _686DP_SERVICIOS.Singleton._686DP_Singleton.Instancia.Usuario._686DPDNI;
                        blle.RegistrarEvento(dniActual, this.Name, "Producto creado con exito", 3);
                    }
                }
                TXTProductos.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("Erroralcrearproducto") + ex.Message  );
                int dniActual = _686DP_SERVICIOS.Singleton._686DP_Singleton.Instancia.Usuario._686DPDNI;
                blle.RegistrarEvento(dniActual, this.Name, "Error al crear el producto", 3);
            }
        }

        private void BTNCrearPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TXTFranquicia.Text))
                {
                    MessageBox.Show(LMG.Traducir("Ingresarfranquicia"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbProductos.SelectedItem == null)
                {
                    MessageBox.Show(LMG.Traducir("Seleccionaproducto"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string traducido = cmbProductos.SelectedItem.ToString();
                string Producto = LMG.ObtenerClaveDesdeValor(traducido) ?? traducido;
                decimal franquicia = Convert.ToDecimal(TXTFranquicia.Text);
                decimal prima = Convert.ToDecimal(TXTPrima.Text);
                if(Producto==null)
                {
                    bllp.CrearPlan(traducido, franquicia, prima);
                }
                else
                {
                    bllp.CrearPlan(Producto, franquicia, prima);
                }
                BLLDV.CalcularDigitoVerificador("Plan");

                MessageBox.Show(LMG.Traducir("PlanOK"));
                int dniActual = _686DP_SERVICIOS.Singleton._686DP_Singleton.Instancia.Usuario._686DPDNI;
                blle.RegistrarEvento(dniActual, this.Name, "Plan creado con exito", 3);
                CargarCombo();
                DGPlan.DataSource = null;
                DGPlan.DataSource = bllp.TraerPlanesFiltrado(Producto);
                TXTPrima.Text = "";
                TXTFranquicia.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("PlanNOOK") + ex.Message);
                int dniActual = _686DP_SERVICIOS.Singleton._686DP_Singleton.Instancia.Usuario._686DPDNI;
                blle.RegistrarEvento(dniActual, this.Name, "Error al crear el plan", 3);
            }
            
        }


        private void DGPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = DGPlan.Rows[e.RowIndex];
                codigoPlanSeleccionado = Convert.ToInt32(fila.Cells["DP686_CodigoPlan"].Value);
                List <_686DP_Cobertura> coberturasXPLAN = bllc.TraerCoberturasFiltrado(codigoPlanSeleccionado);
                DGCobertura.DataSource = null;
                DGCobertura.DataSource = coberturasXPLAN;


                foreach (DataGridViewColumn col in DGPlan.Columns)
                {

                    string traducido = LMG.Traducir(col.HeaderText);
                    string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                    col.HeaderText = limpio;
                }

                foreach (DataGridViewColumn col in DGCobertura.Columns)
                {
                    string traducido = LMG.Traducir(col.HeaderText);
                    string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                    col.HeaderText = limpio;
                }
            }
        }

        private void DGPlan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGPlan.SelectedRows.Count > 0)
                {
                    DataGridViewRow fila = DGPlan.SelectedRows[0];

                    DGCobertura.DataSource = null;
                    List<_686DP_Cobertura> Cobeturas = bllc.traerCoberturas();
                    DGCobertura.DataSource = Cobeturas;
                    DGCobertura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("SeleleccionPlan"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorInesperado") + ex.Message, LMG.Traducir("TituloAviso") );
            }
        }

        private void BTNCrearCobertura_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGPlan.SelectedRows.Count > 0)
                {
                    int codigoPlan = Convert.ToInt32(DGPlan.SelectedRows[0].Cells["DP686_CodigoPlan"].Value);

                    string descripcion = TXTDescripcionCobertura.Text;
                    decimal suma = Convert.ToDecimal(TXTSumaAsegurada.Text);

                    bool yaVisible = DGCobertura.Rows
                        .Cast<DataGridViewRow>()
                        .Any(r =>
                            r.Cells["DP686_Descripcion"].Value.ToString() == descripcion &&
                            Convert.ToDecimal(r.Cells["DP686_SumaAsegurada"].Value) == suma);

                    if (yaVisible)
                    {
                        MessageBox.Show(LMG.Traducir("CoberturaExisente"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int codigoCobertura = bllc.CrearCobertura(descripcion, suma);
                    bllp.AsociarCoberturaAPlan(codigoPlan, codigoCobertura);
                    int dniActual = _686DP_SERVICIOS.Singleton._686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dniActual, this.Name, "Cobertura creada con exito", 3);

                    List<_686DP_Cobertura> coberturas = bllc.TraerCoberturasFiltrado(codigoPlan);
                    DGCobertura.DataSource = null;
                    DGCobertura.DataSource = coberturas;
                    BLLDV.CalcularDigitoVerificador("Cobertura");
                    limpiar();
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("SelPlan"), LMG.Traducir("TituloAviso"));
                }
                TXTDescripcionCobertura.Text = "";
                TXTSumaAsegurada.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorInesperado") + ex.Message, LMG.Traducir("TituloAviso") );
            }
        }

        private void limpiar()
        {
            TXTDescripcionCobertura.Text = "";
            TXTFranquicia.Text = "";
            TXTPrima.Text = "";
            TXTSumaAsegurada.Text = "";
            TXTProductos.Text = "";
            cmbProductos.SelectedIndex = -1;
        }

        private void BTNModificarPlan_Click(object sender, EventArgs e)
        {
            try
            {
                seleccion++;

                if (seleccion == 1)
                {
                    DGCobertura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DGCobertura.DataSource = null;
                    List<_686DP_Cobertura> Cobeturas = bllc.traerCoberturas();
                    DGCobertura.DataSource = Cobeturas;

                    if (DGCobertura.SelectedRows.Count == 0)
                    {
                        MessageBox.Show(LMG.Traducir("SelCob"));
                        return;
                    }

                    int codigoCobertura = Convert.ToInt32(DGCobertura.SelectedRows[0].Cells["CodigoCobertura"].Value);

                    
                    if (bllp.YaExisteRelacionCoberturaPlan(codigoPlanSeleccionado, codigoCobertura))
                    {
                        MessageBox.Show(LMG.Traducir("CoberturaExistente"));
                        return;
                    }

                    bllp.AsociarCoberturaAPlan(codigoPlanSeleccionado, codigoCobertura);
                    MessageBox.Show(LMG.Traducir("CobOK"));
                }
                else if (seleccion == 2)
                {
                    if (codigoPlanSeleccionado == -1)
                    {
                        MessageBox.Show(LMG.Traducir("SelPlan"));
                        return;
                    }

                    if (DGCobertura.SelectedRows.Count == 0)
                    {
                        MessageBox.Show(LMG.Traducir("SelCob"));
                        return;
                    }

                    foreach (DataGridViewRow fila in DGCobertura.SelectedRows)
                    {
                        int codigoCobertura = Convert.ToInt32(fila.Cells["CondigoCobertura"].Value); 

                        if (!bllp.YaExisteRelacionCoberturaPlan(codigoPlanSeleccionado, codigoCobertura))
                        {
                            bllp.AsociarCoberturaAPlan(codigoPlanSeleccionado, codigoCobertura);
                        }
                    }

                    MessageBox.Show(LMG.Traducir("COBOK"));
                    seleccion = 0;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorFueraDeRango") + "\n" + ex.Message, LMG.Traducir("TituloError") );

                seleccion = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorInesperado") + "\n" + ex.Message, LMG.Traducir("TituloError") );

                seleccion = 0;
            }
        }

        private void BTNAsociarPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGPlan.SelectedRows.Count == 0)
                {
                    MessageBox.Show(LMG.Traducir("SeleccionarPlan"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbProductos.SelectedItem == null)
                {
                    MessageBox.Show(LMG.Traducir("SeleccionarProducto"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                int codigoPlan = Convert.ToInt32(DGPlan.SelectedRows[0].Cells["DP686_CodigoPlan"].Value);
                string producto = cmbProductos.SelectedItem.ToString();
                int codSeguro = bll.ObtenerCodSeguroPorProducto(producto);

                if (bll.YaExisteRelacionSeguroPlan(codSeguro, codigoPlan))
                {
                    MessageBox.Show(LMG.Traducir("SeguroYaAsociado"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bllp.AsociarPlanASeguro(codigoPlan, codSeguro);
                MessageBox.Show(LMG.Traducir("AsociacionExitosa"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorAsociarPlan") + ex.Message, LMG.Traducir("TituloError") );
            }
        }


        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProductos.SelectedItem != null)
                {
                    string traducido = cmbProductos.SelectedItem.ToString();
                    string Producto = LMG.ObtenerClaveDesdeValor(traducido) ?? traducido;
                    if(Producto==null)
                    {
                        List<_686DP_Plan> planes = bllp.TraerPlanesFiltrado(traducido);
                        DGPlan.DataSource = null;
                        DGPlan.DataSource = planes;
                        DGPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        List<_686DP_Plan> planes = bllp.TraerPlanesFiltrado(Producto);
                        DGPlan.DataSource = null;
                        DGPlan.DataSource = planes;
                        DGPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }

                    DGCobertura.DataSource = null;
                    DGCobertura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorInesperado") + "\n" + ex.Message, LMG.Traducir("TituloError") );

            }
        }

        private void TXTProductos_TextChanged(object sender, EventArgs e)
        {
            if (TXTProductos.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares. _686DPEsSoloLetras (TXTProductos.Text.ToString()))
                    {
                        MessageBox.Show(LMG.Traducir("SoloLetras"), LMG.Traducir("TituloError") );
                        TXTProductos.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message, LMG.Traducir("TituloError") );
                }
            }
        }

        private void TXTDescripcionCobertura_TextChanged(object sender, EventArgs e)
        {
            if (TXTDescripcionCobertura.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsSoloLetras(TXTDescripcionCobertura.Text.ToString()))
                    {
                        MessageBox.Show(LMG.Traducir("SoloLetras"), LMG.Traducir("TituloError") );
                        TXTDescripcionCobertura.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message, LMG.Traducir("TituloError") );
                }
            }
        }

        private void TXTFranquicia_TextChanged(object sender, EventArgs e)
        {
            if (TXTFranquicia.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(TXTFranquicia.Text.ToString()))
                    {
                        MessageBox.Show(LMG.Traducir("SoloNumeros"), LMG.Traducir("TituloError") );
                        TXTFranquicia.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message, LMG.Traducir("TituloError") );
                }
            }
        }

        private void TXTPrima_TextChanged(object sender, EventArgs e)
        {
            if (TXTPrima.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(TXTPrima.Text.ToString()))
                    {
                        MessageBox.Show(LMG.Traducir("SoloNumeros"), LMG.Traducir("TituloError") ); 
                        TXTPrima.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message, LMG.Traducir("TituloError") );
                }
            }
        }

        private void TXTSumaAsegurada_TextChanged(object sender, EventArgs e)
        {
            if (TXTSumaAsegurada.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(TXTSumaAsegurada.Text.ToString()))
                    {
                        MessageBox.Show(LMG.Traducir("SoloNumeros"), LMG.Traducir("TituloError") );
                        TXTSumaAsegurada.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message, LMG.Traducir("TituloError") );
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
