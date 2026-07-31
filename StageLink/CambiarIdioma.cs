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
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (CBSeleccionIdiomas.SelectedItem == null)
            {
                MessageBox.Show(sm.Traducir("CambiarIdioma_SeleccioneIdioma"));
                return;
            }

            string idiomaNuevo = CBSeleccionIdiomas.SelectedItem.ToString();
            string idiomaAnterior = sm.IdiomaActual;

            sm.CambiarIdioma(idiomaNuevo);

            try
            {
                if (sm.UsuarioActual != null)
                    new BLL_391IAU.BLLUsuario().ActualizarIdiomaUsuario(idiomaNuevo);
            }
            catch { }

            try
            {
                int dniActual = sm.UsuarioActual?.DNI_391IAU ?? 0;
                string nombreUsuario = sm.UsuarioActual != null
                    ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                    : "Usuario";

                BLL_391IAU.BLLBitacoraEventos bllBitacora = new BLL_391IAU.BLLBitacoraEventos();

                bllBitacora.RegistrarEvento(
                    dniActual,
                    4,
                    "CambiarIdioma",
                    $"El usuario {nombreUsuario} cambió el idioma de {idiomaAnterior} a {idiomaNuevo}."
                );
            }
            catch { }
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
