using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void LBLExplicacionContraseña_Click(object sender, EventArgs e)
        {
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNCrearUsuario_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                string dni = TXTDNI.Text.Trim();
                string nombre = TXTNombre.Text.Trim();
                string apellido = TXTApellido.Text.Trim();
                string email = TXTeMail.Text.Trim();

                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                    throw new ArgumentException(sm.Traducir("CrearUsuario_DNIInvalido"));

                if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                    throw new ArgumentException(sm.Traducir("CrearUsuario_NombreInvalido"));

                if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                    throw new ArgumentException(sm.Traducir("CrearUsuario_ApellidoInvalido"));

                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new ArgumentException(sm.Traducir("CrearUsuario_EmailInvalido"));

                int? rolSeleccionado = null;
                if (CBMListadoRoles.SelectedValue != null &&
                    Convert.ToInt32(CBMListadoRoles.SelectedValue) != 0)
                {
                    rolSeleccionado = Convert.ToInt32(CBMListadoRoles.SelectedValue);
                }

                BLLUsuario bll = new BLLUsuario();
                string contraseñaGenerada = dni + nombre;

                bool resultado = bll.CrearUsuario(
                    dni, nombre, apellido, email, contraseñaGenerada, rolSeleccionado
                );

                if (resultado)
                {
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
