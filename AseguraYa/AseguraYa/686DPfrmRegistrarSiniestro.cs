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
using System.Windows.Forms.DataVisualization.Charting;

namespace AseguraYa
{
    public partial class _686DPfrmRegistrarSiniestro : Form
    {
        string idi;
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        public _686DPfrmRegistrarSiniestro(string idiomaLocal)
        {
            InitializeComponent();
            idi = idiomaLocal;
            registrarForm();
        }

        private void registrarForm()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }

        int npoliza = 0;
        private bool botonBuscarPresionado = false;
        _686DP_BLLPoliza bllp = new _686DP_BLLPoliza();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        _686DP_BLLSiniestro bLLSiniestro = new _686DP_BLLSiniestro();
        _686DP_BLLCobertura bllc = new _686DP_BLLCobertura();
        _686DP_BLLPlan bllpl = new _686DP_BLLPlan();
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        List<_686DP_Cobertura>coberturas = new List<_686DP_Cobertura>();
        private void TXTNpoliza_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(TXTNpoliza.Text, @"^\d*$"))
            {
                MessageBox.Show(LMG.Traducir("SoloNumeros"));
                TXTNpoliza.Text = System.Text.RegularExpressions.Regex.Replace(TXTNpoliza.Text, @"[^\d]", "");
                TXTNpoliza.SelectionStart = TXTNpoliza.Text.Length;
            }
        }

        private void BTNBuscar_Click(object sender, EventArgs e)
        {
            botonBuscarPresionado = true;
            if (string.IsNullOrWhiteSpace(TXTNpoliza.Text))
            {
                Console.WriteLine(LMG.Traducir("TextboxVacio"));
                
                botonBuscarPresionado = false;
                TXTNpoliza.BackColor = SystemColors.Window;
            }
            else
            {
               npoliza = Convert.ToInt32(TXTNpoliza.Text);
            }
            
            bool existe = bllp.BuscarPoliza(npoliza);
            if (!existe)
            {
                MessageBox.Show(LMG.Traducir("PolizaNoExiste"));
                botonBuscarPresionado = false;
                TXTNpoliza.BackColor = Color.Pink;
            }
            else
            {

                MessageBox.Show(LMG.Traducir("PolizaOK"));
                TXTNpoliza.BackColor = Color.LightGreen;
                _686DP_Poliza poliza = bllp.traerDatosPoliza(npoliza);
                _686DP_Plan plan = bllpl.TraerPlanPorID(poliza.DP686_CodPlan);
                coberturas = bllc.TraerCoberturasFiltrado(plan.DP686_CodigoPlan);
                cmbCobreturas.Items.Clear();
                foreach(_686DP_Cobertura c in coberturas)
                {
                    cmbCobreturas.Items.Add(c.DP686_Descripcion);
                }

            }
        }

        private void BTNRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(TXTNpoliza.Text) ||
                cmbCobreturas.SelectedIndex < 0 || cmbCobreturas.SelectedItem == null)
            {
                MessageBox.Show(LMG.Traducir("TextboxVacio"), "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
  
            }

            else if (!botonBuscarPresionado)
            {
                Console.WriteLine(LMG.Traducir("BuscarPrimero"));
            }
            double valorBien = Convert.ToDouble(textBox1.Text);
            double VarorReparacion = Convert.ToDouble(textBox2.Text);
            DateTime fecha = dateTimePicker1.Value;
            string descripcion = cmbCobreturas.SelectedItem.ToString();

            bLLSiniestro.RegistrarSiniestro(npoliza, valorBien, VarorReparacion, fecha, descripcion);

            limpiar();
            Console.WriteLine(LMG.Traducir("CargaOK"));
            GeneradorDeSiniestro.GenerarSiniestroBasico(npoliza, fecha, valorBien, VarorReparacion, descripcion, idi, LMG);
            blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se registró un siniestro ", 3);
            BLLDV.CalcularDigitoVerificador("Siniestro");
            this.Close();
        }

        private void limpiar()
        {
            TXTNpoliza.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            botonBuscarPresionado = false;
        }

        private void _686DPfrmRegistrarSiniestro_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idi);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string texto = textBox1.Text;

            if (string.IsNullOrWhiteSpace(texto))
                return;

            texto = texto.Replace(',', '.');

            if (!System.Text.RegularExpressions.Regex.IsMatch(texto, @"^\d*\.?\d*$"))
            {
                MessageBox.Show(LMG.Traducir("SoloNumeros"));

                textBox1.Text = System.Text.RegularExpressions.Regex.Replace(texto, @"[^0-9.,]", "");
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string texto = textBox2.Text;

            if (string.IsNullOrWhiteSpace(texto))
                return;

            texto = texto.Replace(',', '.');

            if (!System.Text.RegularExpressions.Regex.IsMatch(texto, @"^\d*\.?\d*$"))
            {
                MessageBox.Show(LMG.Traducir("SoloNumeros"));

                textBox2.Text = System.Text.RegularExpressions.Regex.Replace(texto, @"[^0-9.,]", "");
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }
    }
}
