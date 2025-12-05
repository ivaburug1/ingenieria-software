using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class CambiarIdioma : Form, IObserver_391IAU
    {
        public CambiarIdioma()
        {
            InitializeComponent();
        }

        private void CambiarIdioma_Load(object sender, EventArgs e)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.AgregarObservador(this);

            SessionManager_391IAU.SessionManager_391IAU.Instancia.RegistrarFormulario(this);

            CargarIdiomasDisponiblesComboBox();

            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
        }
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }
        private void BTNCambiarIdioma_Click(object sender, EventArgs e)
        {
            if (CBSeleccionIdiomas.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un idioma.");
                return;
            }

            string idioma = CBSeleccionIdiomas.SelectedItem.ToString();

            SessionManager_391IAU.SessionManager_391IAU.Instancia.CambiarIdioma(idioma);
        }

        private void CargarIdiomasDisponiblesComboBox()
        {
            string carpetaIdiomas = Path.Combine(Application.StartupPath, "Idiomas");

            if (!Directory.Exists(carpetaIdiomas))
            {
                MessageBox.Show("No se encontró la carpeta de idiomas: " + carpetaIdiomas);
                return;
            }

            string[] carpetas = Directory.GetDirectories(carpetaIdiomas);

            List<string> idiomas = new List<string>();

            foreach (var carpeta in carpetas)
            {
                string nombre = Path.GetFileName(carpeta);
                idiomas.Add(nombre);
            }

            CBSeleccionIdiomas.DataSource = idiomas;
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
