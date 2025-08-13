using BE_391IAU;
using BLL_391IAU;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StageLink
{
    public partial class SeleccionarEvento : Form
    {
        private int dniCliente;

        public SeleccionarEvento(int dni)
        {
            InitializeComponent();
            dniCliente = dni;
        }

        private void SeleccionarEvento_Load(object sender, EventArgs e)
        {
            BLL_Evento bllEvento = new BLL_Evento();
            var eventos = bllEvento.ObtenerEventos();

            CBSeleccionarEvento.DataSource = eventos;
            CBSeleccionarEvento.SelectedIndex = -1;
            CBSeleccionarFecha.DataSource = null;
            CBSeleccionarSector.DataSource = null;

            LBLNotasEstadio.Text = "Estadio:";
            LBLNotasDireccion.Text = "Dirección:";
            LBLNotasSector.Text = "Sector:";

            CBSeleccionarEvento.SelectedIndexChanged += CBSeleccionarEvento_SelectedIndexChanged;
            CBSeleccionarFecha.SelectedIndexChanged += CBSeleccionarFecha_SelectedIndexChanged;
            CBSeleccionarSector.SelectedIndexChanged += CBSeleccionarSector_SelectedIndexChanged;
        }

        private void CBSeleccionarEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string artistaSeleccionado = CBSeleccionarEvento.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(artistaSeleccionado)) return;

            BLL_Evento bll = new BLL_Evento();
            var fechas = bll.ObtenerFechasPorArtista(artistaSeleccionado);
            CBSeleccionarFecha.DataSource = fechas;

            var (estadio, direccion) = bll.ObtenerEstadioYDireccion(artistaSeleccionado);
            LBLNotasEstadio.Text = $"Estadio: El estadio es {estadio}.";
            LBLNotasDireccion.Text = $"Dirección: La dirección del estadio es {direccion}.";
        }

        private void CBSeleccionarFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBSeleccionarEvento.SelectedItem == null || CBSeleccionarFecha.SelectedItem == null)
                return;

            string artista = CBSeleccionarEvento.SelectedItem.ToString();
            DateTime fecha = (DateTime)CBSeleccionarFecha.SelectedItem;

            BLLSector bllSector = new BLLSector();
            var sectores = bllSector.ObtenerSectoresPorEvento(artista, fecha);

            CBSeleccionarSector.DataSource = sectores;
            CBSeleccionarSector.DisplayMember = "Nombre";
            CBSeleccionarSector.ValueMember = "Codigo";
        }


        private void BTNCobrarBoleto_Click(object sender, EventArgs e)
        {
            if (CBSeleccionarEvento.SelectedItem == null ||
                CBSeleccionarFecha.SelectedItem == null ||
                CBSeleccionarSector.SelectedItem == null ||
                CBCantEntradas.SelectedItem == null)
            {
                MessageBox.Show("Completar todos los campos antes de continuar.");
                return;
            }

            string artista = CBSeleccionarEvento.SelectedItem.ToString();
            DateTime fecha = (DateTime)CBSeleccionarFecha.SelectedItem;

            var sector = CBSeleccionarSector.SelectedItem as BESector;
            if (sector == null)
            {
                MessageBox.Show("Error al obtener el sector.");
                return;
            }

            int sectorCodigo = sector.Codigo;
            decimal precio = sector.Precio;

            int cantidad = int.Parse(CBCantEntradas.SelectedItem.ToString());

            BLL_Evento bllEvento = new BLL_Evento();
            int codigoEvento = bllEvento.ObtenerCodigoEvento(artista, fecha);

            CobrarBoleto cobrarBoleto = new CobrarBoleto(dniCliente, codigoEvento, artista, fecha, sectorCodigo, precio, cantidad);
            cobrarBoleto.Show();
        }

        private void CBSeleccionarSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBSeleccionarSector.SelectedItem is BESector sector)
            {
                LBLNotasSector.Text = $"El Sector seleccionado tiene un precio de {sector.Precio:C}.";
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}