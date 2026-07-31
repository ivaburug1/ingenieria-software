using System;
using System.Data;
using System.Windows.Forms;
using BLL_391IAU;

namespace StageLink
{
    public partial class AuditoriaDeCambios : Form
    {
        private readonly BLLProductosCambio _bll = new BLLProductosCambio();

        public AuditoriaDeCambios()
        {
            InitializeComponent();
        }

        private void AuditoriaDeCambios_Load(object sender, EventArgs e)
        {
            DTPFechaDesde.Value = DateTime.Today.AddDays(-30);
            DTPFechaHasta.Value = DateTime.Today;

            CargarGrid();
        }

        private void CargarGrid(DateTime? desde = null, DateTime? hasta = null, string nombre = null)
        {
            DataTable dt = _bll.FiltrarCambios(desde, hasta, nombre);
            DGVCargaProductoCambio.DataSource = dt;

            DGVCargaProductoCambio.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVCargaProductoCambio.MultiSelect = false;
            DGVCargaProductoCambio.ReadOnly = true;
            DGVCargaProductoCambio.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime desde = DTPFechaDesde.Value.Date;
                DateTime hasta = DTPFechaHasta.Value.Date;

                if (desde > hasta)
                    throw new Exception("La Fecha Desde no puede ser mayor a la Fecha Hasta.");

                string nombre = string.IsNullOrWhiteSpace(TXTNombre.Text) ? null : TXTNombre.Text.Trim();

                CargarGrid(desde, hasta, nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BTNLimpiarFiltro_Click(object sender, EventArgs e)
        {
            TXTNombre.Clear();
            DTPFechaDesde.Value = DateTime.Today.AddDays(-30);
            DTPFechaHasta.Value = DateTime.Today;

            CargarGrid(DTPFechaDesde.Value.Date, DTPFechaHasta.Value.Date, null);
        }

        private void BTNActivarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVCargaProductoCambio.CurrentRow == null)
                    throw new Exception("Seleccione un producto de la bitácora.");

                var row = DGVCargaProductoCambio.CurrentRow;

                int idProducto = Convert.ToInt32(row.Cells["IDProducto_391IAU"].Value);
                string nombre = row.Cells["Nombre_391IAU"].Value?.ToString();
                string stock = row.Cells["StockActual_391IAU"].Value?.ToString();
                int precio = Convert.ToInt32(row.Cells["PrecioVenta_391IAU"].Value);
                string tipo = row.Cells["TipoProducto_391IAU"].Value?.ToString();
                DateTime fecha = Convert.ToDateTime(row.Cells["Fecha"].Value).Date;

                bool hayOtroActivo = _bll.ExisteOtroActivoMismoId(idProducto, fecha, nombre, stock, precio, tipo);

                if (hayOtroActivo)
                {
                    var r = MessageBox.Show(
                        $"Actualmente hay uno o mas productos con el mismo ID {idProducto}. Desea Reemplazarlo en la tabla de Productos?",
                        "Confirmación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (r != DialogResult.Yes)
                    {
                        MessageBox.Show("La operacion fue abortada.", "Activación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                _bll.ActivarProductoDesdeBitacora(idProducto, fecha, nombre, stock, precio, tipo);

                MessageBox.Show("Se reemplazo con exito.", "Activación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BTNFiltrar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Activación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}