using BLL_391IAU;
using System;
using System.Windows.Forms;

namespace StageLink
{
    public partial class CobrarBoleto : Form
    {
        private string artista;
        private DateTime fecha;
        private int sectorCodigo;
        private decimal precioSector;
        private int cantidad;
        private int dniCliente;
        private int codigoEvento;

        public CobrarBoleto(int dniCliente, int codigoEvento, string artista, DateTime fecha, int sectorCodigo, decimal precio, int cantidad)
        {
            InitializeComponent();

            this.dniCliente = dniCliente;
            this.codigoEvento = codigoEvento;
            this.artista = artista;
            this.fecha = fecha;
            this.sectorCodigo = sectorCodigo;
            this.precioSector = precio;
            this.cantidad = cantidad;
        }

        private void CobrarBoleto_Load(object sender, EventArgs e)
        {
            CargarRecibo();
            CBMedioDePago.SelectedIndex = 0;
            TXTNumeroDeTarjeta.Enabled = false;
            CBCantCuotas.Enabled = false;
            CBMedioDePago.SelectedIndexChanged += CBMedioDePago_SelectedIndexChanged;
        }

        private void CargarRecibo()
        {
            LVMostrarRecibo.Items.Clear();
            LVMostrarRecibo.View = View.Details;
            LVMostrarRecibo.Columns.Clear();

            LVMostrarRecibo.Columns.Add("Campo", 150);
            LVMostrarRecibo.Columns.Add("Detalle", 250);

            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Artista", artista }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Fecha", fecha.ToShortDateString() }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Sector", sectorCodigo.ToString() }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Cantidad de Entradas", cantidad.ToString() }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Precio por Entrada", precioSector.ToString("C") }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Total", (precioSector * cantidad).ToString("C") }));
        }

        private void CBMedioDePago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string medioPago = CBMedioDePago.SelectedItem?.ToString();

            if (medioPago == "Tarjeta Debito")
            {
                TXTNumeroDeTarjeta.Enabled = true;
                CBCantCuotas.Enabled = false;
                CBCantCuotas.SelectedIndex = -1;
            }
            else if (medioPago == "Tarjeta Credito")
            {
                TXTNumeroDeTarjeta.Enabled = true;
                CBCantCuotas.Enabled = true;
            }
            else
            {
                TXTNumeroDeTarjeta.Enabled = false;
                TXTNumeroDeTarjeta.Text = string.Empty;
                CBCantCuotas.Enabled = false;
                CBCantCuotas.SelectedIndex = -1;
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNConfirmarCobro_Click(object sender, EventArgs e)
        {
            string medioPago = CBMedioDePago.SelectedItem?.ToString();
            string nroTarjeta = TXTNumeroDeTarjeta.Text.Trim();
            string cuotas = CBCantCuotas.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(medioPago) ||
               ((medioPago == "Tarjeta Debito" || medioPago == "Tarjeta Credito") && string.IsNullOrEmpty(nroTarjeta)) ||
               (medioPago == "Tarjeta Credito" && string.IsNullOrEmpty(cuotas)))
            {
                MessageBox.Show("Completar todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((medioPago == "Tarjeta Debito" || medioPago == "Tarjeta Credito") &&
                !System.Text.RegularExpressions.Regex.IsMatch(nroTarjeta, @"^\d{4}-\d{4}-\d{4}-\d{4}$"))
            {
                MessageBox.Show("El número de tarjeta debe tener el formato XXXX-XXXX-XXXX-XXXX.", "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de que quiere finalizar la compra?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                BLLBoleto bll = new BLLBoleto();
                bool exito = bll.RegistrarCompra(artista, fecha, sectorCodigo, precioSector, cantidad, dniCliente, codigoEvento);
                MessageBox.Show("¡Compra realizada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
