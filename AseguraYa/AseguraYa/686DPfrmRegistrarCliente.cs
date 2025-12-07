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
    public partial class _686DPfrmRegistrarCliente : Form
    {
        _686DP_BLLCLlientes clientes = new _686DP_BLLCLlientes();
        public _686DP_Cliente ClienteCreado { get; private set; }
        string idioma = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        int dni; 
        public _686DPfrmRegistrarCliente(string idi, int dNI)
        {
            idioma = idi;
            InitializeComponent();
            cambiarIdioma();
            dni = dNI;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DP_TXTDNI.Text) || string.IsNullOrWhiteSpace(DP_TXTNombre.Text) ||string.IsNullOrWhiteSpace(DP_TXTApellido.Text))
            {
                MessageBox.Show(LMG.Traducir("CamposIncompletos"), LMG.Traducir("TituloCamposIncompletos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int dni = Convert.ToInt32(DP_TXTDNI.Text);
                bool existe = clientes.ValidarNuevo(dni); 

                if (existe)
                {
                    MessageBox.Show(LMG.Traducir("ClienteYaRegistrado"));
                    return;
                }
                ClienteCreado = new _686DP_Cliente(dni, DP_TXTNombre.Text, DP_TXTApellido.Text);
                clientes.crear(ClienteCreado);
                BLLDV.CalcularDigitoVerificador("Cliente");
                MessageBox.Show(LMG.Traducir("ClienteCreadoExito"));
                this.DialogResult = DialogResult.OK;
                blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Cliente " + DP_TXTNombre.Text + " creado de forma basica", 2);
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show(LMG.Traducir("DNIInvalido"), LMG.Traducir("TituloDNIInvalido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorInesperado") + ": " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void _686DPfrmRegistrarCliente_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idioma);
            cambiarIdioma();
            DP_TXTDNI.Text=dni.ToString();
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idioma);
        }
    }
}
