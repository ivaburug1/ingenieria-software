using BE_391IAU;
using BLL_391IAU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class AgregarEventos : Form, IObserver_391IAU
    {
        public AgregarEventos()
        {
            InitializeComponent();

            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);

            sm.TraducirFormulario(this);
        }
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        private void BTNConfirmar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                if (string.IsNullOrWhiteSpace(TXTNombreDelArtista.Text))
                {
                    MessageBox.Show(
                        sm.Traducir("AgregarEventos_IngresarArtista"),
                        sm.Traducir("AgregarEventos_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (CBSeleccionEsatio.SelectedItem == null)
                {
                    MessageBox.Show(
                        sm.Traducir("AgregarEventos_SeleccionarEstadio"),
                        sm.Traducir("AgregarEventos_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DateTime fechaEvento = DTPFechaEvento.Value.Date;
                string nombreArtista = TXTNombreDelArtista.Text.Trim();
                string nombreEstadio = CBSeleccionEsatio.SelectedItem.ToString();

                if (fechaEvento <= DateTime.Today)
                {
                    MessageBox.Show(
                        sm.Traducir("AgregarEventos_FechaInvalida"),
                        sm.Traducir("AgregarEventos_ErrorValidacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                int codigoEstadio;
                switch (nombreEstadio)
                {
                    case "Monumental River Plate":
                        codigoEstadio = 1;
                        break;
                    case "Complejo Art Media":
                        codigoEstadio = 2;
                        break;
                    case "Estadio Huracan":
                        codigoEstadio = 3;
                        break;
                    default:
                        throw new Exception(sm.Traducir("AgregarEventos_EstadioInvalido"));
                }

                BLL_Evento bll = new BLL_Evento();

                if (bll.ExisteEventoPorArtistaYFecha(nombreArtista, fechaEvento))
                {
                    MessageBox.Show(
                        sm.Traducir("AgregarEventos_ArtistaFechaDuplicada"),
                        sm.Traducir("AgregarEventos_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (bll.EventoExiste(fechaEvento, codigoEstadio))
                {
                    MessageBox.Show(
                        sm.Traducir("AgregarEventos_EventoDuplicado"),
                        sm.Traducir("AgregarEventos_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                var evento = new BEEvento_391IAU(fechaEvento, codigoEstadio, nombreArtista);

                if (bll.InsertarEvento(evento))
                {
                    try
                    {
                        int dniActual = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        string nombreUsuario = sm.UsuarioActual != null
                            ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                            : "Usuario";

                        BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();

                        bllBitacora.RegistrarEvento(
                            dniActual,
                            4,
                            "AgregarEventos",
                            $"El usuario {nombreUsuario} agregó un nuevo evento: Artista {nombreArtista}, Estadio {nombreEstadio}, Fecha {fechaEvento:yyyy-MM-dd}."
                        );
                    }
                    catch { }

                    MessageBox.Show(
                        sm.Traducir("AgregarEventos_Exito"),
                        sm.Traducir("AgregarEventos_ExitoTitulo"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.Close();
                }
                else
                {
                    try
                    {
                        int dniActual = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();

                        bllBitacora.RegistrarEvento(
                            dniActual,
                            3,
                            "AgregarEventos",
                            $"Error al insertar evento: Artista {nombreArtista}, Estadio {nombreEstadio}, Fecha {fechaEvento:yyyy-MM-dd}."
                        );
                    }
                    catch { }

                    MessageBox.Show(
                        sm.Traducir("AgregarEventos_ErrorInsertar"),
                        sm.Traducir("AgregarEventos_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                try
                {
                    int dniActual = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();

                    bllBitacora.RegistrarEvento(
                        dniActual,
                        3,
                        "AgregarEventos",
                        "Error inesperado en Agregar Evento."
                    );
                }
                catch { }

                MessageBox.Show(
                    sm.Traducir("AgregarEventos_ErrorGeneral") + " " + ex.Message,
                    sm.Traducir("AgregarEventos_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AgregarEventos_Load(object sender, EventArgs e)
        {

        }
    }
}
