using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_391IAU;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class CambiarContraseña : Form, IObserver_391IAU
    {
        public CambiarContraseña()
        {
            InitializeComponent();
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        private void BTNCambiarContraseña_Click_1(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;
            try
            {
                string actual = TXTContraseñaActual.Text;
                string nueva = TXTContraseñaNueva.Text;
                string confirmacion = TXTContraseñaConfirmacion.Text;

                if (string.IsNullOrWhiteSpace(actual) || string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmacion))
                {
                    MessageBox.Show(
                        sm.Traducir("CambiarPass_CamposObligatorios"),
                        sm.Traducir("CambiarPass_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (nueva != confirmacion)
                {
                    MessageBox.Show(
                        sm.Traducir("CambiarPass_NoCoinciden"),
                        sm.Traducir("CambiarPass_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                BLLUsuario bll = new BLLUsuario();

                if (!bll.ValidarContraseñaActual(actual))
                {
                    MessageBox.Show(
                        sm.Traducir("CambiarPass_ActualIncorrecta"),
                        sm.Traducir("CambiarPass_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                if (bll.ContraseñaYaFueUsada(nueva))
                {
                    MessageBox.Show(
                        sm.Traducir("CambiarPass_ContraseñaReutilizada"),
                        sm.Traducir("CambiarPass_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                bll.CambiarContraseña(nueva);
                MessageBox.Show(
                    sm.Traducir("CambiarPass_Actualizada"),
                    sm.Traducir("CambiarPass_Exito"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();

                bll.Logout();
                MessageBox.Show(
                    sm.Traducir("CambiarPass_SesionCerrada"),
                    sm.Traducir("LogoutCorrecto_Titulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                PantallaPrincipal.ResetMenuStripLogout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    sm.Traducir("CambiarPass_ErrorGeneral") + " " + ex.Message,
                    sm.Traducir("CambiarPass_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CambiarContraseña_Load(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
        }
    }
}