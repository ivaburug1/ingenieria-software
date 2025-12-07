using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_BE;
using _686DP_BLL;
using _686DP_SERVICIOS.Observer;
using _686DP_SERVICIOS.Singleton;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AseguraYa
{
    public partial class _686DPfrmGenerarContratación : Form
    {
        _686DP_ExpresionesRegulares _686DP_ExpresionesRegulares = new _686DP_ExpresionesRegulares();
        _686DP_Cliente nuevoCliente = null;
        private int codigoPlanSeleccionado = -1;
        private decimal prima = 0;
        _686DP_BLLCLlientes bll = new _686DP_BLLCLlientes();
        _686DPBLLSeguro blls = new _686DPBLLSeguro();
        _686DP_BLLPoliza bllp = new _686DP_BLLPoliza();
        _686DP_BLLPlan bLLPlan = new _686DP_BLLPlan();
        _686DP_BLLCobertura bllc = new _686DP_BLLCobertura();
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        List<_686DP_Cobertura> coberurasfinales = new List<_686DP_Cobertura>();
        _686DP_GeneradorDePolizas gp = new _686DP_GeneradorDePolizas();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();

        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        public _686DPfrmGenerarContratación(string idiomaLocal)
        {
            idi = idiomaLocal;
            InitializeComponent();
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    int DNI = Convert.ToInt32(textBox1.Text);
                    bool existe = bll.ValidarNuevo(DNI);
                    if (!existe)
                    {
                        MessageBox.Show(LMG.Traducir("ClienteNoExiste"));
                        _686DPfrmRegistrarCliente rc = new _686DPfrmRegistrarCliente(idi, DNI);
                        if (rc.ShowDialog() == DialogResult.OK)
                        {
                            nuevoCliente = rc.ClienteCreado;
                        }
                    }
                    else
                    {
                        nuevoCliente = bll.TraerCliente(DNI);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorBuscarCliente") + ex.Message);
            }
        }

        private void _686DPfrmGenerarContratación_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idi);
            try
            {
                CargarCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorCargarCombos") + ex.Message  );
            }
            cambiarIdioma();
            CMBProducto.DropDownStyle = ComboBoxStyle.DropDownList;

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
            try
            {
                CMBProducto.Items.Clear();
                List<string> productos = blls.TraerProductos();
                foreach (string producto in productos)
                {
                    string traducido = LMG.Traducir(producto);
                    string limpio = traducido.Replace("[", "").Replace("]", "").Trim();

                    if (limpio == producto)
                    {
                        CMBProducto.Items.Add(producto);
                    }
                    else
                    {
                        CMBProducto.Items.Add(traducido);
                    }
                }


                DGCoberturas.DataSource = null;
                DGCoberturas.DataSource = bllc.traerCoberturas();
                DGCoberturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DGPlan.DataSource = null;
                DGPlan.DataSource = bLLPlan.TraerPlanes();
                DGPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                foreach (DataGridViewColumn col in DGCoberturas.Columns)
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
                MessageBox.Show(LMG.Traducir("ErrorCargarPlanesCoberturas") + ex.Message );
            }
        }

        private void CMBProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string seleccionado = CMBProducto.SelectedItem.ToString();
                string limpio = seleccionado.Replace("[", "").Replace("]", "").Trim();
                string clave = LMG.ObtenerClaveDesdeValor(seleccionado);
                string producto = string.IsNullOrEmpty(clave) ? limpio : clave;

                List<_686DP_Plan> planes = bLLPlan.TraerPlanesFiltrado(producto);
                DGPlan.DataSource = null;
                DGPlan.DataSource = planes;
                DGPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DGCoberturas.DataSource = null;
                DGCoberturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                foreach (DataGridViewColumn col in DGCoberturas.Columns)
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
                MessageBox.Show(LMG.Traducir("ErrorFiltrarPlanesCoberturas")+ ex.Message );
            }
        }

        private void DGPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = DGPlan.Rows[e.RowIndex];
                    codigoPlanSeleccionado = Convert.ToInt32(fila.Cells["DP686_CodigoPlan"].Value);
                    prima = Convert.ToDecimal(fila.Cells["DP686_Prima"].Value);
                    DGCoberturas.DataSource = null;
                    coberurasfinales = bllc.TraerCoberturasFiltrado(codigoPlanSeleccionado);
                    DGCoberturas.DataSource = coberurasfinales;
                    foreach (DataGridViewColumn col in DGPlan.Columns)
                    {
                        
                        string traducido = LMG.Traducir(col.HeaderText);
                        string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                        col.HeaderText = limpio;
                    }

                    foreach (DataGridViewColumn col in DGCoberturas.Columns)
                    {
                        string limpio = col.HeaderText.Replace("[", "").Replace("]", "").Trim();
                        col.HeaderText = LMG.Traducir(limpio);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorSeleccionarPlan") + ex.Message  );
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show(LMG.Traducir("FaltaDNI"));
                    return;
                }

                if (codigoPlanSeleccionado == -1)
                {
                    MessageBox.Show(LMG.Traducir("PlanNoSeleccionado"));
                    return;
                }
                int DNI = Convert.ToInt32(textBox1.Text);

                var cliente = nuevoCliente ?? bll.TraerCliente(DNI);

                if (cliente.DP686_Estado.HasValue && cliente.DP686_Estado.Value == false)
                {
                    MessageBox.Show(LMG.Traducir("ClienteBloqueado"));
                    return;
                }
                List<string> faltantes = new List<string>();

                if (string.IsNullOrWhiteSpace(cliente.DP686_Email)) faltantes.Add("Email");
                if (string.IsNullOrWhiteSpace(cliente.DP686_Domicilio)) faltantes.Add("Domicilio");
                if (cliente.DP686DP_CodigoPostal == 0) faltantes.Add("Código Postal");

                if (faltantes.Count > 0)
                {
                    _686DPfrmDatosExtra datosextra = new _686DPfrmDatosExtra(DNI, idi);
                    var result = datosextra.ShowDialog(); 

                    if (result != DialogResult.OK)
                    {
                        MessageBox.Show(LMG.Traducir("NoSeCompletaronDatosCliente"));
                        return;
                    }
                    cliente = bll.TraerCliente(DNI);

                    List<string> faltantesDespues = new List<string>();
                    if (string.IsNullOrWhiteSpace(cliente.DP686_Email)) faltantesDespues.Add("Email");
                    if (string.IsNullOrWhiteSpace(cliente.DP686_Domicilio)) faltantesDespues.Add("Domicilio");
                    if (cliente.DP686DP_CodigoPostal == 0) faltantesDespues.Add("Código Postal");

                    if (faltantesDespues.Count > 0)
                    {
                        MessageBox.Show(LMG.Traducir("DatosIncompletosCliente"));
                        return;
                    }
                }
                string traducido = LMG.ObtenerClaveDesdeValor(CMBProducto.SelectedItem.ToString());
                int codSeguro;
                if (traducido == null)
                {
                    codSeguro = blls.ObtenerCodSeguroPorProducto(CMBProducto.SelectedItem.ToString());
                }
                else
                {
                    codSeguro = blls.ObtenerCodSeguroPorProducto(traducido);
                }
                int CodPoliza = bllp.CrearPoliza(codSeguro, prima, DNI, codigoPlanSeleccionado);
                BLLDV.CalcularDigitoVerificador("Polizas");
                
                MessageBox.Show(LMG.Traducir("PolizaGenerada"));
                _686DP_GeneradorDePolizas.GenerarPolizaBasica(cliente.DP686_Nombre, cliente.DP686_Apellido, cliente.DP686_DNI, cliente.DP686_Domicilio, cliente.DP686_Email, traducido, prima, coberurasfinales, idi, LMG, CodPoliza);
                int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                blle.RegistrarEvento(dni, this.Name, "Se generó una contratacion de polizas", 3);
                blle.RegistrarEvento(dni, this.Name, "Se imprimio una poliza", 4);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorGenerarContratacion") + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(textBox1.Text.ToString()))
                    {
                        MessageBox.Show(LMG.Traducir("SoloNumeros"));
                        textBox1.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion"));
                }
            }
        }

        private void CMBProducto_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void DGPlan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
