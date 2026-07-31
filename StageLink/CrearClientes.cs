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

            int dniUsuario = sm.UsuarioActual?.DNI_391IAU ?? 0;
            string nombreUsuario = sm.UsuarioActual != null
                ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                : "Usuario";

            try
            {
                string dni = TXTDNICliente.Text.Trim();
                string nombre = TXTNombreCliente.Text.Trim();
                string apellido = TXTApellidoCliente.Text.Trim();
                string correo = TXTCorreoCliente.Text.Trim();

                void LogValidacion(string detalle)
                {
                    try
                    {
                        BLLBitacoraEventos bllBit = new BLLBitacoraEventos();
                        bllBit.RegistrarEvento(
                            dniUsuario,
                            3,
                            "CrearCliente",
                            $"Validación fallida. Operador: {nombreUsuario} (DNI {dniUsuario}). {detalle}"
                        );
                    }
                    catch { }
                }

                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                {
                    LogValidacion($"DNI cliente inválido: '{dni}'.");

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
                    LogValidacion($"Nombre cliente inválido: '{nombre}'. DNI cliente: {dni}.");

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
                    LogValidacion($"Apellido cliente inválido: '{apellido}'. DNI cliente: {dni}.");

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
                    LogValidacion($"Email cliente inválido: '{correo}'. DNI cliente: {dni}.");

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
                    try
                    {
                        BLLBitacoraEventos bllBit = new BLLBitacoraEventos();
                        bllBit.RegistrarEvento(
                            dniUsuario,
                            3,
                            "CrearCliente",
                            $"Éxito. Operador: {nombreUsuario} (DNI {dniUsuario}). Cliente creado: DNI {dniInt}, Nombre {nombre} {apellido}, Email {correo}."
                        );
                    }
                    catch { }

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
                    try
                    {
                        BLLBitacoraEventos bllBit = new BLLBitacoraEventos();
                        bllBit.RegistrarEvento(
                            dniUsuario,
                            3,
                            "CrearCliente",
                            $"Falló InsertarCliente (retornó false). Operador: {nombreUsuario} (DNI {dniUsuario}). Cliente: DNI {dniInt}, Nombre {nombre} {apellido}, Email {correo}."
                        );
                    }
                    catch { }

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
                try
                {
                    BLLBitacoraEventos bllBit = new BLLBitacoraEventos();
                    bllBit.RegistrarEvento(
                        dniUsuario,
                        3,
                        "CrearCliente",
                        $"Excepción. Operador: {nombreUsuario} (DNI {dniUsuario}). Detalle: {ex.Message}"
                    );
                }
                catch { }

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