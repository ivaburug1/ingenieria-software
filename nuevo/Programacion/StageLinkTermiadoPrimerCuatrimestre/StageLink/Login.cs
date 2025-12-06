using BE_391IAU;
using BLL_391IAU;
using SessionManager_391IAU;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StageLink
{
    public partial class Login : Form, IObserver_391IAU
    {
        public Login()
        {
            InitializeComponent();
            TXTDNI.Text = "45679391";
            TXTContraseña.Text = "ivaburug1525a";
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
        }

        private void BTNLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = TXTDNI.Text.Trim();
                string contrasenia = TXTContraseña.Text;
                var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                {
                    MessageBox.Show(
                        sm.Traducir("Login_DNIInvalido"),
                        sm.Traducir("Login_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                BLLUsuario bll = new BLLUsuario();
                bool loginExitoso = bll.Login(dni, contrasenia);

                if (loginExitoso)
                {
                    string nombreCompleto = bll.ObtenerNombreUsuarioLogueado();
                    MessageBox.Show(
                        sm.Traducir("Login_Bienvenido") + " " + nombreCompleto,
                        sm.Traducir("Login_ExitosoTitulo"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    string idioma = sm.UsuarioActual.Idioma_391IAU;

                    if (string.IsNullOrWhiteSpace(idioma))
                        idioma = "Español";

                    sm.CambiarIdioma(idioma); 

                    PantallaPrincipal.ResetMenuStripLogout();
                    PantallaPrincipal.Instancia.ActivarPermisosDeSesion();

                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        sm.Traducir("Login_NoSePudoIniciarSesion"),
                        sm.Traducir("Login_ErrorTitulo"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                }
            }
            catch (UnauthorizedAccessException ex)
            {
                var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

                MessageBox.Show(
                    sm.Traducir("Login_CredencialesInvalidas") + " " + ex.Message,
                    sm.Traducir("Login_ErrorAutenticacionTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

            }
            catch (InvalidOperationException ex)
            {
                var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

                MessageBox.Show(
                    sm.Traducir("Login_NoSePuedeIniciarSesion") + " " + ex.Message,
                    sm.Traducir("Login_SesionActivaTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

            }
            catch (Exception ex)
            {
                var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

                MessageBox.Show(
                    sm.Traducir("Login_ErrorInesperado") + " " + ex.Message,
                    sm.Traducir("Login_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void BTNMostrarContraseña_Click(object sender, EventArgs e)
        {
            TXTContraseña.UseSystemPasswordChar = !TXTContraseña.UseSystemPasswordChar;
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (TXTContraseña.UseSystemPasswordChar)
            {
                BTNMostrarContraseña.Text = sm.Traducir("Login_MostrarContrasenia");
            }
            else
            {
                BTNMostrarContraseña.Text = sm.Traducir("Login_OcultarContrasenia");
            }
        }

        private void TXTContraseña_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
