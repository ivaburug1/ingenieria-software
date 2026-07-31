using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BLL_391IAU;
using BE_391IAU;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class CrearUsuario : Form, IObserver_391IAU
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNCrearUsuario_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            int dniOperador = sm.UsuarioActual?.DNI_391IAU ?? 0;
            string operador = sm.UsuarioActual != null
                ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                : "Usuario";

            void Log1(string detalle)
            {
                try
                {
                    BLLBitacoraEventos bllBit = new BLLBitacoraEventos();
                    bllBit.RegistrarEvento(
                        dniOperador,
                        1,
                        "CrearUsuario",
                        $"Operador: {operador} (DNI {dniOperador}). {detalle}"
                    );
                }
                catch { }
            }

            try
            {
                string dni = TXTDNI.Text.Trim();
                string nombre = TXTNombre.Text.Trim();
                string apellido = TXTApellido.Text.Trim();
                string email = TXTeMail.Text.Trim();

                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                {
                    Log1($"Validación fallida: DNI inválido '{dni}'.");
                    throw new ArgumentException(sm.Traducir("CrearUsuario_DNIInvalido"));
                }

                if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    Log1($"Validación fallida: Nombre inválido '{nombre}'. DNI nuevo usuario: {dni}.");
                    throw new ArgumentException(sm.Traducir("CrearUsuario_NombreInvalido"));
                }

                if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    Log1($"Validación fallida: Apellido inválido '{apellido}'. DNI nuevo usuario: {dni}.");
                    throw new ArgumentException(sm.Traducir("CrearUsuario_ApellidoInvalido"));
                }

                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    Log1($"Validación fallida: Email inválido '{email}'. DNI nuevo usuario: {dni}.");
                    throw new ArgumentException(sm.Traducir("CrearUsuario_EmailInvalido"));
                }

                int? rolSeleccionado = null;
                if (CBMListadoRoles.SelectedValue != null &&
                    Convert.ToInt32(CBMListadoRoles.SelectedValue) != 0)
                {
                    rolSeleccionado = Convert.ToInt32(CBMListadoRoles.SelectedValue);
                }

                if (rolSeleccionado == null || rolSeleccionado == 0)
                {
                    Log1($"Validación fallida: Rol inválido. DNI nuevo usuario: {dni}. SelectedValue='{CBMListadoRoles.SelectedValue}'.");
                    throw new ArgumentException(sm.Traducir("CrearUsuario_RolInvalido"));
                }

                BLLUsuario bll = new BLLUsuario();

                bool resultado = bll.CrearUsuario(
                    dni, nombre, apellido, email, rolSeleccionado
                );

                if (resultado)
                {
                    Log1($"Éxito: se creó el usuario DNI {dni}, Nombre {nombre} {apellido}, Email {email}, RolID {rolSeleccionado}.");

                    MessageBox.Show(
                        sm.Traducir("CrearUsuario_Exito"),
                        sm.Traducir("CrearUsuario_ExitoTitulo"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    this.Close();
                }
                else
                {
                    Log1($"Fallo: BLLUsuario.CrearUsuario retornó false. DNI {dni}, Email {email}, RolID {rolSeleccionado}.");

                    MessageBox.Show(
                        sm.Traducir("CrearUsuario_ErrorCrear"),
                        sm.Traducir("GestionClientes_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                Log1($"Excepción al crear usuario. Detalle: {ex.Message}");

                MessageBox.Show(
                    sm.Traducir("CrearUsuario_ErrorGeneral") + " " + ex.Message,
                    sm.Traducir("CrearUsuario_ExcepcionTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CrearUsuario_Load(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            int dniOperador = sm.UsuarioActual?.DNI_391IAU ?? 0;
            string operador = sm.UsuarioActual != null
                ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                : "Usuario";

            try
            {
                BLLPerfil bllPerfil = new BLLPerfil();
                List<BEPerfil> lista = bllPerfil.TraerPerfiles();

                lista.Insert(0, new BEPerfil
                {
                    IDRol_391IAU = 0,
                    Nombre_391IAU = "<Sin Rol>"
                });

                CBMListadoRoles.DataSource = lista;
                CBMListadoRoles.DisplayMember = "Nombre_391IAU";
                CBMListadoRoles.ValueMember = "IDRol_391IAU";
            }
            catch (Exception ex)
            {
                try
                {
                    BLLBitacoraEventos bllBit = new BLLBitacoraEventos();
                    bllBit.RegistrarEvento(
                        dniOperador,
                        1,
                        "CrearUsuario",
                        $"Operador: {operador} (DNI {dniOperador}). Error al cargar roles/perfiles en pantalla CrearUsuario. Detalle: {ex.Message}"
                    );
                }
                catch { }

                MessageBox.Show(
                    sm.Traducir("CrearUsuario_ErrorCargarRoles") + " " + ex.Message,
                    sm.Traducir("CrearUsuario_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
        }
    }
}