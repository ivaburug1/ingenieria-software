using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_SERVICIOS;
using _686DP_SERVICIOS.Observer;
using _686DP_SERVICIOS.Singleton;
using System.Text.Json.Serialization;
using _686DP_BLL;

namespace AseguraYa
{
    public partial class _686DPfrmIdioma : Form
    {
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        public _686DPfrmIdioma(string idiomaLocal)
        {
            idi = idiomaLocal;
            InitializeComponent();
            cambiarIdioma();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show(LMG.Traducir("DebeSeleccionarIdioma"));
                return;
            }

            idi = LMG.ObtenerClaveDesdeValor(comboBox1.SelectedItem.ToString());
            (this.MdiParent as _686DPfrmInicio)?.CambiarIdioma(idi);
            var usuario = _686DP_Singleton.Instancia.Usuario;
            usuario._686DPIdioma = idi;
        }

        private void _686DPfrmIdioma_Load(object sender, EventArgs e)
        {
            try
            {
                LMG.CargarMensajesGlobales(idi);
                string jsonPath = "Idiomas.json";
                string json = File.ReadAllText(jsonPath);
                Dictionary<string, string> idiomas = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                comboBox1.Items.Clear();
                foreach (var kvp in idiomas)
                {
                    string traduccion = LMG.Traducir(kvp.Key);
                    string mostrar = string.IsNullOrWhiteSpace(traduccion) ? kvp.Key : traduccion;
                    comboBox1.Items.Add(mostrar);
                }

                cambiarIdioma();
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se cambio el idioma" + idi, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando idiomas: " + ex.Message);
            }
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }
    }
}
