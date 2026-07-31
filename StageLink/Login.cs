using BE_391IAU;
using BLL_391IAU;
using Servicios_391IAU.Composite;
using SessionManager_391IAU;
using System;
using System.Collections.Generic;
using System.Linq;
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
            TXTContraseña.Text = "Ivaburug1525a";
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
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();
            string dni = TXTDNI.Text.Trim();
            string contrasenia = TXTContraseña.Text;

            int dniInt = 0;
            int.TryParse(dni, out dniInt);

            BLLUsuario bll = null;

            try
            {
                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                {
                    MessageBox.Show(
                        sm.Traducir("Login_DNIInvalido"),
                        sm.Traducir("Login_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    try { bllBitacora.RegistrarEvento(dniInt, 4, "Login", $"Intento de LogIn con DNI inválido: {dni}."); }
                    catch { }

                    return;
                }

                bll = new BLLUsuario();
                bool loginExitoso = bll.Login(dni, contrasenia);

                if (!loginExitoso)
                {
                    try { bllBitacora.RegistrarEvento(dniInt, 2, "Login", "Intento de LogIn fallido."); }
                    catch { }

                    MessageBox.Show(
                        sm.Traducir("Login_NoSePudoIniciarSesion"),
                        sm.Traducir("Login_ErrorTitulo"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                bool esAdmin = false;
                try
                {
                    var permisos = bll.TraerPermisos();
                    esAdmin = TienePermisoAdmin(permisos);
                }
                catch
                {
                    esAdmin = false;
                }

                try
                {
                    BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
                    bllDV.ValidarIntegridadDVOrThrow();
                }
                catch (Exception exDv)
                {
                    try
                    {
                        bllBitacora.RegistrarEvento(
                            dniInt,
                            1,
                            "Login",
                            "Se detectó inconsistencia en el Dígito Verificador. " + exDv.Message
                        );
                    }
                    catch { }

                    try { bll.Logout(); } catch { }

                    var detallesDV = ExtraerDetallesDV(exDv);

                    if (esAdmin)
                    {
                        MessageBox.Show(
                            "Se detectó una inconsistencia en el Dígito Verificador.\n\n" +
                            "Como tenés permisos de Administrador, podés ingresar al módulo de reparación.",
                            "Error de Integridad",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        RepararSistema reparar = new RepararSistema(detallesDV);
                        reparar.StartPosition = FormStartPosition.CenterScreen;
                        reparar.ShowDialog();

                        return;
                    }
                    else
                    {
                        MessageBox.Show(
                            "Se detectó una inconsistencia en el Dígito Verificador.\n\n" +
                            "No se puede iniciar sesión hasta que un Administrador repare el sistema.",
                            "Error de Integridad",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return;
                    }
                }

                string nombreCompleto = bll.ObtenerNombreUsuarioLogueado();

                try
                {
                    bllBitacora.RegistrarEvento(dniInt, 2, "Login", $"El usuario {nombreCompleto} hizo LogIn.");
                }
                catch { }

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
            catch (UnauthorizedAccessException ex)
            {
                try { bllBitacora.RegistrarEvento(dniInt, 2, "Login", "Intento de LogIn fallido (credenciales inválidas)."); }
                catch { }

                MessageBox.Show(
                    sm.Traducir("Login_CredencialesInvalidas") + " " + ex.Message,
                    sm.Traducir("Login_ErrorAutenticacionTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                try { bll?.Logout(); } catch { }
            }
            catch (InvalidOperationException ex)
            {
                try { bllBitacora.RegistrarEvento(dniInt, 2, "Login", "Se intentó iniciar sesión pero ya existe una sesión activa."); }
                catch { }

                MessageBox.Show(
                    sm.Traducir("Login_NoSePuedeIniciarSesion") + " " + ex.Message,
                    sm.Traducir("Login_SesionActivaTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                try { bllBitacora.RegistrarEvento(dniInt, 3, "Login", "Error inesperado al intentar LogIn."); }
                catch { }

                MessageBox.Show(
                    sm.Traducir("Login_ErrorInesperado") + " " + ex.Message,
                    sm.Traducir("Login_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                try { bll?.Logout(); } catch { }
            }
        }

        private List<string> ExtraerDetallesDV(Exception ex)
        {
            var items = new List<string>();

            void addMsg(string msg)
            {
                if (string.IsNullOrWhiteSpace(msg)) return;

                var partes = msg
                    .Split(new[] { "\r\n", "\n", ";", "|" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => x.Length > 0);

                items.AddRange(partes);
            }

            addMsg(ex.Message);

            if (ex.InnerException != null)
                addMsg(ex.InnerException.Message);

            if (items.Count == 0)
                items.Add("Se detectó inconsistencia DV, pero no se recibió detalle del error.");

            return items.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        private bool TienePermisoAdmin(IEnumerable<IComponentePermiso_391IAU> permisos)
        {
            if (permisos == null) return false;

            var nombresAdmin = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "administradorToolStripMenuItem",
                "Administrador",
                "administrador"
            };

            foreach (var p in permisos)
            {
                if (TienePermisoAdminRec(p, nombresAdmin))
                    return true;
            }

            return false;
        }

        private bool TienePermisoAdminRec(IComponentePermiso_391IAU comp, HashSet<string> nombresAdmin)
        {
            if (comp == null) return false;

            string nombre = null;

            if (comp is PermisoSimple_391IAU ps) nombre = ps.Nombre;
            else if (comp is Familia_391IAU fam) nombre = fam.Nombre;

            if (!string.IsNullOrWhiteSpace(nombre) && nombresAdmin.Contains(nombre.Trim()))
                return true;

            if (comp is Familia_391IAU familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                    if (TienePermisoAdminRec(hijo, nombresAdmin))
                        return true;
            }

            return false;
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