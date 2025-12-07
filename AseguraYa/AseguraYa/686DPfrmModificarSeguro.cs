using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_BE;
using _686DP_BLL;
using _686DP_SERVICIOS.Observer;
using _686DP_SERVICIOS.Singleton;

namespace AseguraYa
{
    public partial class _686DPfrmModificarSeguro : Form
    {
        _686DP_ExpresionesRegulares _686DP_ExpresionesRegulares = new _686DP_ExpresionesRegulares();
        private int codigoPlanSeleccionado = -1;
        _686DP_Poliza poliza = null;
        _686DP_Plan plan = null;
        _686DP_BLLPoliza bll = new _686DP_BLLPoliza();
        _686DPBLLSeguro blls = new _686DPBLLSeguro();
        _686DP_BLLPlan bLLPlan = new _686DP_BLLPlan();
        _686DP_BLLCobertura bllc = new _686DP_BLLCobertura();
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        public _686DPfrmModificarSeguro(string idiomaLocal)
        {
            idi = idiomaLocal;
            InitializeComponent();
            cambiarIdioma();
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void cargarDG()
        {
            try
            {
                DGPlan.DataSource = null;
                plan = bll.TraerPlan(poliza.DP686_CodPlan);
                DGPlan.DataSource = new List<_686DP_Plan> { plan };
                DGPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DGCoberura.DataSource = null;
                DGCoberura.DataSource = plan.Coberturas;
                DGCoberura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                _686DP_Seguro seguro = bll.TraerSeguro(poliza.DP686_CodSeguro);
                string traducido = LMG.Traducir(seguro.DP686_TipoProducto.ToString());
                string limpio = traducido.Replace("[", "").Replace("]", "").Trim();

                if (limpio == seguro.DP686_TipoProducto.ToString())
                {
                    TXTProducto.Text = seguro.DP686_TipoProducto.ToString();
                }
                else
                {
                    TXTProducto.Text = traducido;
                }
                foreach (DataGridViewColumn col in DGCoberura.Columns)
                {
                    col.HeaderText = LMG.Traducir(col.HeaderText);
                }
                foreach (DataGridViewColumn col in DGPlan.Columns)
                {
                    col.HeaderText = LMG.Traducir(col.HeaderText);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorCargarPoliza") + ": " + ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                try
                {
                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(textBox1.Text))
                    {
                        MessageBox.Show(LMG.Traducir("SoloEnteros"));
                        textBox1.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ": " + ex.Message);
                }
            }
        }

        private void BTNAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (poliza == null)
                {
                    MessageBox.Show(LMG.Traducir("SinPoliza"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TXTProducto.Text) || codigoPlanSeleccionado == -1)
                {
                    MessageBox.Show(LMG.Traducir("FaltanDatos"), LMG.Traducir("TituloFaltanDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string producto = TXTProducto.Text.Trim();
                int nPoliza = Convert.ToInt32(textBox1.Text);

                poliza.DP686_CodPlan = codigoPlanSeleccionado;
                poliza.DP686_CodSeguro = blls.ObtenerCodSeguroPorProducto(producto);
                poliza.DP686_FechaVencimiento = DateTime.Now.AddMonths(1);

                bll.ModificarPoliza(poliza);
                BLLDV.CalcularDigitoVerificador("Polizas");
                MessageBox.Show(LMG.Traducir("ModificacionExitosa"), LMG.Traducir("Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se modificó la poliza" , 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorModificarPoliza") + ": " + ex.Message);
            }
        }


        private void _686DPfrmModificarSeguro_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idi);
            cambiarIdioma();
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }
        private void TXTProducto_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                string producto = TXTProducto.Text.Trim();
                if (string.IsNullOrWhiteSpace(producto)) return;

                string product = blls.buscarProducto(producto);
                if (product != null)
                {
                    DGPlan.DataSource = null;
                    List<_686DP_Plan> planes = bLLPlan.TraerPlanesFiltrado(product);
                    DGPlan.DataSource = planes;
                    foreach (DataGridViewColumn col in DGPlan.Columns)
                    {
                        col.HeaderText = LMG.Traducir(col.HeaderText);
                    }
                    DGPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorBuscarPlanes") + ": " + ex.Message);
            }
        }

        private void DGPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && DGPlan.Rows[e.RowIndex].Cells["DP686_CodigoPlan"].Value != null)
                {
                    DataGridViewRow fila = DGPlan.Rows[e.RowIndex];
                    codigoPlanSeleccionado = Convert.ToInt32(fila.Cells["DP686_CodigoPlan"].Value);

                    List<_686DP_Cobertura> coberturasXPLAN = bllc.TraerCoberturasFiltrado(codigoPlanSeleccionado);
                    DGCoberura.DataSource = null;
                    DGCoberura.DataSource = coberturasXPLAN;
                    DGCoberura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    foreach (DataGridViewColumn col in DGCoberura.Columns)
                    {
                        col.HeaderText = LMG.Traducir(col.HeaderText);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorSeleccionarPlan") + ": " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(textBox1.Text))
                    {
                        MessageBox.Show(LMG.Traducir("SoloNumeros"));
                        textBox1.Clear();
                        return;
                    }

                    int numeroDePoliza = Convert.ToInt32(textBox1.Text);
                    bool existe = bll.BuscarPoliza(numeroDePoliza);

                    if (!existe)
                    {
                        MessageBox.Show(LMG.Traducir("PolizaNoExiste"), LMG.Traducir("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    poliza = bll.traerDatosPoliza(numeroDePoliza);
                    cargarDG();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorBuscarPoliza") + ": " + ex.Message);
            }
        }
    }
}
