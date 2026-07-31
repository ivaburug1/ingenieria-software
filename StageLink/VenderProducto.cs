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
    public partial class VenderProducto : Form
    {
        public VenderProducto()
        {
            InitializeComponent();
        }

        private readonly BLL_391IAU.BLLVentaProducto _bll = new BLL_391IAU.BLLVentaProducto();
        private bool _cargando = false;

        private void VenderProducto_Load(object sender, EventArgs e)
        {
            CargarProductos();
            CargarEventos();
            LBLCantStockActual.Text = "-";
        }

        private void CargarProductos()
        {
            _cargando = true;

            DataTable dt = _bll.ListarProductosParaVenta();

            DataRow dr = dt.NewRow();
            dr["IDProducto_391IAU"] = 0;
            dr["Nombre_391IAU"] = "- Seleccione un Producto -";
            dr["StockActualInt"] = 0;
            dt.Rows.InsertAt(dr, 0);

            CBProducto.DataSource = dt;
            CBProducto.DisplayMember = "Nombre_391IAU";
            CBProducto.ValueMember = "IDProducto_391IAU";
            CBProducto.SelectedIndex = 0;

            _cargando = false;
        }

        private void CargarEventos()
        {
            _cargando = true;

            DataTable dt = _bll.ListarEventos();

            if (!dt.Columns.Contains("Display"))
                dt.Columns.Add("Display", typeof(string));

            foreach (DataRow r in dt.Rows)
            {
                string artista = r["NombreArtista_391IAU"].ToString();
                DateTime fecha = Convert.ToDateTime(r["Fecha_391IAU"]);
                r["Display"] = $"{artista} - {fecha:yyyy-MM-dd}";
            }

            DataRow dr = dt.NewRow();
            dr["CodigoDeEvento_391IAU"] = 0;
            dr["Display"] = "- Seleccione un Evento -";
            dt.Rows.InsertAt(dr, 0);

            CBEvento.DataSource = dt;
            CBEvento.DisplayMember = "Display";
            CBEvento.ValueMember = "CodigoDeEvento_391IAU";
            CBEvento.SelectedIndex = 0;

            _cargando = false;
        }
        private void CBProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            if (CBProducto.SelectedValue == null) return;
            if (CBProducto.SelectedValue is DataRowView) return;

            int idProd = Convert.ToInt32(CBProducto.SelectedValue);

            if (idProd <= 0)
            {
                LBLCantStockActual.Text = "-";
                return;
            }

            var row = (CBProducto.SelectedItem as DataRowView);
            int stock = 0;
            if (row != null && row.Row.Table.Columns.Contains("StockActualInt"))
                stock = Convert.ToInt32(row["StockActualInt"]);

            LBLCantStockActual.Text = stock.ToString();
        }
        private void BTNVenderProducto_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (CBProducto.SelectedValue == null || CBProducto.SelectedValue is DataRowView)
                    throw new Exception("Debe seleccionar un producto.");

                if (CBEvento.SelectedValue == null || CBEvento.SelectedValue is DataRowView)
                    throw new Exception("Debe seleccionar un evento.");

                int idProducto = Convert.ToInt32(CBProducto.SelectedValue);
                int codigoEvento = Convert.ToInt32(CBEvento.SelectedValue);

                if (idProducto <= 0) throw new Exception("Debe seleccionar un producto.");
                if (codigoEvento <= 0) throw new Exception("Debe seleccionar un evento.");

                if (!int.TryParse((TXTCantVender.Text ?? "").Trim(), out int cantidad) || cantidad <= 0)
                    throw new Exception("La cantidad a vender debe ser un número entero mayor a 0.");

                int stockLabel = 0;
                int.TryParse((LBLCantStockActual.Text ?? "0").Trim(), out stockLabel);
                if (cantidad > stockLabel)
                    throw new Exception($"No hay stock suficiente. Stock actual: {stockLabel}");

                _bll.VenderProductoAEvento(idProducto, codigoEvento, cantidad);

                MessageBox.Show("Venta de stock realizada correctamente.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarProductos();
                CargarEventos();
                LBLCantStockActual.Text = "-";
                TXTCantVender.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
