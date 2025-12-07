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

namespace AseguraYa
{
    public partial class _686DPfrmDatosExtra : Form
    {
        private int dni;
        _686DP_ExpresionesRegulares regex = new _686DP_ExpresionesRegulares();
        private _686DP_Cliente cliente;
        private _686DP_BLLCLlientes bll = new _686DP_BLLCLlientes();
        string idioma = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        public _686DPfrmDatosExtra(int dNI, string idi)
        {
            InitializeComponent();
            this.TXTEmail.Validating += new CancelEventHandler(this.TXTEmail_Validating);
            this.FormClosing += _686DPfrmDatosExtra_FormClosing;
            this.dni = dNI;
            cliente = bll.TraerCliente(dni);
            idioma = idi;
        }

        private void _686DPfrmDatosExtra_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idioma);
            label1.Text = "DNI: " + dni.ToString();
            cambiarIdioma();
        }
        private void TXTEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTEmail.Text) && !regex._686DPEsEmail(TXTEmail.Text))
            {
                MessageBox.Show(LMG.Traducir("EmailInvalido"));
                TXTEmail.Clear();
                e.Cancel = true;
            }
        }


        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idioma);
        }

        private bool DatosCompletos()
        {
            return !string.IsNullOrWhiteSpace(TXTEmail.Text) &&
                   !string.IsNullOrWhiteSpace(TXTDomicilio.Text) &&
                   !string.IsNullOrWhiteSpace(TXTCodigoPostal.Text) &&
                   !string.IsNullOrWhiteSpace(TXTNTarjeta.Text) &&
                   !string.IsNullOrWhiteSpace(TXTMedioDePago.Text);
        }

        private void _686DPfrmDatosExtra_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK && !DatosCompletos())
            {
                MessageBox.Show(LMG.Traducir("DebeCompletarDatos"));
                e.Cancel = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatosCompletos())
                {
                    MessageBox.Show(LMG.Traducir("CompletarDatos"), LMG.Traducir("Faltantes"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cliente != null)
                {
                    cliente.DP686_Email = TXTEmail.Text;
                    cliente.DP686_Domicilio = TXTDomicilio.Text;
                    cliente.DP686DP_CodigoPostal = Convert.ToInt32(TXTCodigoPostal.Text);

                    bll.GrabarCliente(cliente);
                    BLLDV.CalcularDigitoVerificador("Cliente");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(LMG.Traducir("CodigoPostalInvalido"), LMG.Traducir("ErrorFormato") );
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorGuardarDatos"));
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TXTEmail_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTEmail.Text) && !regex._686DPEsEmail(TXTEmail.Text))
            {
                MessageBox.Show(LMG.Traducir("EmailInvalido")); 
                TXTEmail.Clear();
                TXTEmail.Focus();
            }
        }

        private void TXTDomicilio_MouseLeave(object sender, EventArgs e)
        {
        }

        private void TXTCodigoPostal_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTCodigoPostal.Text) && !regex._686DPEsNumero(TXTCodigoPostal.Text))
            {
                MessageBox.Show(LMG.Traducir("SoloNumeros")  );
                TXTCodigoPostal.Clear();
                TXTCodigoPostal.Focus();
            }
        }

       
        private void TXTiva_MouseLeave(object sender, EventArgs e)
        {
            
        }


        private void TXTMedioDePago_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTMedioDePago.Text) && !regex._686DPEsSoloLetras(TXTMedioDePago.Text))
            {
                MessageBox.Show(LMG.Traducir("SoloLetras"));
                TXTMedioDePago.Clear();
                TXTMedioDePago.Focus();
            }
        }

        private void TXTNTarjeta_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTNTarjeta.Text) && !regex._686DPEsTarjetaCredito(TXTNTarjeta.Text))
            {
                MessageBox.Show(LMG.Traducir("TarjetaInvalida"));
                TXTNTarjeta.Clear();
                TXTNTarjeta.Focus();
            }
        }

        private void TXTNTarjeta_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXTDomicilio_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
