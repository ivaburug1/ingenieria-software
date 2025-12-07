using _686DP_BE;
using _686DP_BLL;
using _686DP_SERVICIOS.Observer;
using _686DP_SERVICIOS.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AseguraYa
{
    public partial class _686DPfrmCancelarSeguro : Form
    {
        _686DP_ExpresionesRegulares _686DP_ExpresionesRegulares = new _686DP_ExpresionesRegulares();
        _686DP_BLLPoliza bll = new _686DP_BLLPoliza();
        _686DP_Poliza poliza = null;
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        public _686DPfrmCancelarSeguro(string idiomaLocal)
        {
            idi = idiomaLocal;
            InitializeComponent();
            cambiarIdioma();
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
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message);
                }
            }
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void BTNAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) &&
                    !string.IsNullOrEmpty(textBox2.Text) &&
                    poliza != null)
                {
                    string motivo = textBox2.Text;
                    bll.eliminarPoliza(motivo, poliza);
                    MessageBox.Show(LMG.Traducir("PolizaEliminada"));
                    BLLDV.CalcularDigitoVerificador("Polizas");
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Poliza " + textBox1.Text +" eliminada", 3);
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("CompletarCamposCancelar"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorEliminarPoliza") + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsSoloLetras(textBox2.Text))
                    {
                        MessageBox.Show(LMG.Traducir("SoloLetras"));
                        textBox2.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message);
                }
            }
        }

        private void _686DPfrmCancelarSeguro_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    if (int.TryParse(textBox1.Text, out int numeroDePoliza))
                    {
                        bool existe = bll.BuscarPoliza(numeroDePoliza);
                        if (!existe)
                        {
                            MessageBox.Show(LMG.Traducir("PolizaNoExiste"));
                        }
                        else
                        {
                            poliza = bll.traerDatosPoliza(numeroDePoliza);
                            dataGridView1.DataSource = new List<_686DP_Poliza> { poliza };
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                col.HeaderText = LMG.Traducir(col.HeaderText);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(LMG.Traducir("FormatoInvalidoPoliza"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorBuscarPoliza") + ex.Message);
            }
        }
    }
}
