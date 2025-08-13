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

namespace StageLink
{
    public partial class AgregarEventos : Form
    {
        public AgregarEventos()
        {
            InitializeComponent();
        }

        private void BTNConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TXTNombreDelArtista.Text))
                {
                    MessageBox.Show("Por favor ingrese el nombre del artista.");
                    return;
                }

                if (CBSeleccionEsatio.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione un estadio.");
                    return;
                }

                DateTime fechaEvento = DTPFechaEvento.Value.Date;
                string nombreArtista = TXTNombreDelArtista.Text.Trim();
                string nombreEstadio = CBSeleccionEsatio.SelectedItem.ToString();

                if (fechaEvento <= DateTime.Today)
                {
                    MessageBox.Show("La fecha ya pasó, elegir una fecha válida.");
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
                        throw new Exception("Estadio inválido.");
                }

                BLL_Evento bll = new BLL_Evento();
                if (bll.EventoExiste(fechaEvento, codigoEstadio))
                {
                    MessageBox.Show("Ya hay un evento cargado para este estadio en esta fecha. Elegir otra fecha u otro estadio.");
                    return;
                }

                var evento = new BEEvento_391IAU(fechaEvento, codigoEstadio, nombreArtista);

                if (bll.InsertarEvento(evento))
                {
                    MessageBox.Show("Evento agregado correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hubo un error al agregar el evento.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
