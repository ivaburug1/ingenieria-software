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
    public partial class LBLProductoCantidad : Form
    {
        private readonly BLLStock _bllStock = new BLLStock();

        private const string DEFAULT_PROV = "- Seleccione un Proveedor -";
        private const string DEFAULT_PROD = "- Seleccione un Producto -";

        private int _precioPalletActual = 0;

        public LBLProductoCantidad()
        {
            InitializeComponent();
        }

        private bool _cargando = false;
        private void LBLProductoCantidad_Load(object sender, EventArgs e)
        {
            _cargando = true;

            CargarProveedores();
            CargarCantidadPallets();
            ResetLabelsTodo();

            _cargando = false;
        }

        private void CargarProveedores()
        {
            DataTable dt = _bllStock.ListarProveedores();

            DataRow dr = dt.NewRow();
            dr["CUIT_391IAU"] = 0L;
            dr["Nombre_391IAU"] = DEFAULT_PROV;
            dt.Rows.InsertAt(dr, 0);

            CBProveedor.DataSource = dt;
            CBProveedor.DisplayMember = "Nombre_391IAU";
            CBProveedor.ValueMember = "CUIT_391IAU";
            CBProveedor.SelectedIndex = 0;

            CargarProductosDefault();
        }

        private void CargarProductosDefault()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IDProducto_391IAU", typeof(int));
            dt.Columns.Add("Nombre_391IAU", typeof(string));

            DataRow dr = dt.NewRow();
            dr["IDProducto_391IAU"] = 0;
            dr["Nombre_391IAU"] = DEFAULT_PROD;
            dt.Rows.Add(dr);

            CBProducto.DataSource = dt;
            CBProducto.DisplayMember = "Nombre_391IAU";
            CBProducto.ValueMember = "IDProducto_391IAU";
            CBProducto.SelectedIndex = 0;
        }

        private void CargarProductosPorProveedor(long proveedorId)
        {
            DataTable dt = _bllStock.ListarProductosPorProveedor(proveedorId);

            DataRow dr = dt.NewRow();
            dr["IDProducto_391IAU"] = 0;
            dr["Nombre_391IAU"] = DEFAULT_PROD;
            dt.Rows.InsertAt(dr, 0);

            CBProducto.DataSource = dt;
            CBProducto.DisplayMember = "Nombre_391IAU";
            CBProducto.ValueMember = "IDProducto_391IAU";
            CBProducto.SelectedIndex = 0;
        }

        private void CargarCantidadPallets()
        {
            CBCantComprar.Items.Clear();
            CBCantComprar.Items.Add("-");
            for (int i = 1; i <= 20; i++)
                CBCantComprar.Items.Add(i.ToString());

            CBCantComprar.SelectedIndex = 0;
        }

        private void ResetLabelsTodo()
        {
            LBLCantidadProducto.Text = "-";
            LBLPrecioTotalCantidad.Text = "$ -";
            _precioPalletActual = 0;

            if (CBCantComprar.Items.Count > 0)
                CBCantComprar.SelectedIndex = 0;
        }

        private void ResetLabelsCompra()
        {
            LBLPrecioTotalCantidad.Text = "$ -";
            _precioPalletActual = 0;

            if (CBCantComprar.Items.Count > 0)
                CBCantComprar.SelectedIndex = 0;
        }

        private void CBProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            try
            {
                if (CBProveedor.SelectedValue == null) return;
                if (CBProveedor.SelectedValue is DataRowView) return;

                long proveedorId = Convert.ToInt64(CBProveedor.SelectedValue);

                ResetLabelsTodo();

                _cargando = true;

                if (proveedorId <= 0)
                {
                    CargarProductosDefault();
                }
                else
                {
                    CargarProductosPorProveedor(proveedorId);
                }

                _cargando = false;
            }
            catch (Exception ex)
            {
                _cargando = false;
                MessageBox.Show(ex.Message, "Error al seleccionar proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CBProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            try
            {
                if (CBProducto.SelectedValue == null) return;
                if (CBProducto.SelectedValue is DataRowView) return;

                int productoId = Convert.ToInt32(CBProducto.SelectedValue);

                ResetLabelsCompra();

                if (productoId <= 0)
                    return;

                var detalle = _bllStock.TraerDetalleProducto(productoId);
                _precioPalletActual = detalle.PrecioPallet;

                LBLCantidadProducto.Text = detalle.StockActual.ToString();

                ActualizarPrecioTotal();
            }
            catch
            {
            }
        }

        private void CBCantComprar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarPrecioTotal();
        }

        private void ActualizarPrecioTotal()
        {
            if (_precioPalletActual <= 0)
            {
                LBLPrecioTotalCantidad.Text = "$ -";
                return;
            }

            if (CBCantComprar.SelectedItem == null || CBCantComprar.SelectedIndex <= 0)
            {
                LBLPrecioTotalCantidad.Text = "$ -";
                return;
            }

            if (!int.TryParse(CBCantComprar.SelectedItem.ToString(), out int pallets) || pallets <= 0)
            {
                LBLPrecioTotalCantidad.Text = "$ -";
                return;
            }

            long total = (long)_precioPalletActual * pallets;
            LBLPrecioTotalCantidad.Text = "$ " + total.ToString();
        }

        private void BTNRegistrarProveedor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pendiente: Registrar Nuevo Proveedor (CU003).",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTNComprar_Click_1(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                long proveedorId = Convert.ToInt64(CBProveedor.SelectedValue);
                int productoId = Convert.ToInt32(CBProducto.SelectedValue);

                if (CBCantComprar.SelectedIndex <= 0)
                    throw new Exception("Debe seleccionar la cantidad de pallets a comprar.");

                int pallets = int.Parse(CBCantComprar.SelectedItem.ToString());

                if (proveedorId <= 0)
                    throw new Exception("Debe seleccionar un proveedor.");

                if (productoId <= 0)
                    throw new Exception("Debe seleccionar un producto.");

                string proveedorNombre = CBProveedor.Text;
                string productoNombre = CBProducto.Text;

                long totalEstimado = 0;
                if (_precioPalletActual > 0)
                    totalEstimado = (long)_precioPalletActual * pallets;

                int nuevoStock = _bllStock.ComprarProducto(proveedorId, productoId, pallets);

                try
                {
                    int dniUsuario = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    string nombreUsuario = sm.UsuarioActual != null
                        ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                        : "Usuario";

                    BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();

                    bllBitacora.RegistrarEvento(
                        dniUsuario,
                        2,
                        "ComprarProducto",
                        $"El usuario {nombreUsuario} compró stock. Proveedor {proveedorNombre} (ID {proveedorId}), Producto {productoNombre} (ID {productoId}), Pallets {pallets}, TotalEstimado ${totalEstimado}, NuevoStock {nuevoStock}."
                    );
                }
                catch { }

                MessageBox.Show("Compra registrada correctamente.\nNuevo stock: " + nuevoStock,
                    "Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LBLCantidadProducto.Text = nuevoStock.ToString();
                ActualizarPrecioTotal();
            }
            catch (Exception ex)
            {
                try
                {
                    int dniUsuario = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();

                    string proveedorNombre = CBProveedor?.Text ?? "-";
                    string productoNombre = CBProducto?.Text ?? "-";
                    string palletsTxt = CBCantComprar?.SelectedItem?.ToString() ?? "-";

                    bllBitacora.RegistrarEvento(
                        dniUsuario,
                        2,
                        "ComprarProducto",
                        $"Error al intentar comprar stock. Proveedor {proveedorNombre}, Producto {productoNombre}, Cantidad {palletsTxt}. Detalle: {ex.Message}"
                    );
                }
                catch { }

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNRegistrarProveedor_Click_1(object sender, EventArgs e)
        {
            RegistrarProveedor registrarProveedor = new RegistrarProveedor();
            registrarProveedor.Show();
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
