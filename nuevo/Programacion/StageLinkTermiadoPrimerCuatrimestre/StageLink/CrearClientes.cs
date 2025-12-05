using BE_391IAU;
using BLL_391IAU;
using SessionManager_391IAU;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StageLink
{
    public partial class CrearClientes : Form, IObserver_391IAU
    {
        public CrearClientes()
        {
            InitializeComponent();
        }
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }
        private void BTNCrearCliente_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                string dni = TXTDNICliente.Text.Trim();
                string nombre = TXTNombreCliente.Text.Trim();
                string apellido = TXTApellidoCliente.Text.Trim();
                string correo = TXTCorreoCliente.Text.Trim();

                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                {
                    MessageBox.Show(
                        sm.Traducir("CrearCliente_DNIInvalido"),
                        sm.Traducir("CrearCliente_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    MessageBox.Show(
                        sm.Traducir("CrearCliente_NombreInvalido"),
                        sm.Traducir("CrearCliente_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    MessageBox.Show(
                        sm.Traducir("CrearCliente_ApellidoInvalido"),
                        sm.Traducir("CrearCliente_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show(
                        sm.Traducir("CrearCliente_EmailInvalido"),
                        sm.Traducir("CrearCliente_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                int dniInt = int.Parse(dni);
                BECliente cliente = new BECliente(dniInt, nombre, apellido, correo);
                BLLCliente bll = new BLLCliente();

                if (bll.InsertarCliente(cliente))
                {
                    MessageBox.Show(
                        sm.Traducir("CrearCliente_Exito"),
                        sm.Traducir("CrearCliente_ExitoTitulo"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        sm.Traducir("CrearCliente_ErrorInsertar"),
                        sm.Traducir("CrearCliente_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    sm.Traducir("CrearCliente_ErrorGeneral") + " " + ex.Message,
                    sm.Traducir("CrearCliente_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CrearClientes_Load(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
        }

        private void LBLTitulo_Click(object sender, EventArgs e)
        {
        }
    }
}
