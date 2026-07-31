using System;
using System.Data;
using System.Windows.Forms;
using BLL_391IAU;

namespace StageLink
{
    public partial class ValidarStock : Form
    {
        private readonly BLLStock _bllStock = new BLLStock();

        private const string DEFAULT_NOMBRE = "- Seleccione un Nombre -";
        private const string DEFAULT_TIPO = "- Seleccione un Producto -";

        public ValidarStock()
        {
            InitializeComponent();
        }

        private void ValidarStock_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarStockInicial();
        }

        private void CargarStockInicial()
        {
            try
            {
                DGVStockProductos.DataSource = _bllStock.ListarStockProductos();
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar stock: " + ex.Message);
            }
        }

        private void CargarCombos()
        {
            try
            {
                var dtNombres = _bllStock.ObtenerNombresProductos();
                CBNombreProducto.Items.Clear();
                CBNombreProducto.Items.Add(DEFAULT_NOMBRE);
                foreach (DataRow row in dtNombres.Rows)
                    CBNombreProducto.Items.Add(row["Nombre_391IAU"].ToString());
                CBNombreProducto.SelectedIndex = 0;

                var dtTipos = _bllStock.ObtenerTiposProductos();
                CBTipoProducto.Items.Clear();
                CBTipoProducto.Items.Add(DEFAULT_TIPO);
                foreach (DataRow row in dtTipos.Rows)
                    CBTipoProducto.Items.Add(row["TipoProducto_391IAU"].ToString().Trim());
                CBTipoProducto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar filtros: " + ex.Message);
            }
        }

        private void ConfigurarGrilla()
        {
            DGVStockProductos.AutoGenerateColumns = true;
            DGVStockProductos.ReadOnly = true;
            DGVStockProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVStockProductos.MultiSelect = false;
            DGVStockProductos.AllowUserToAddRows = false;
            DGVStockProductos.AllowUserToDeleteRows = false;
            DGVStockProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void BTNFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = (CBNombreProducto.SelectedItem?.ToString() == DEFAULT_NOMBRE)
                    ? null
                    : CBNombreProducto.SelectedItem?.ToString();

                string tipo = (CBTipoProducto.SelectedItem?.ToString() == DEFAULT_TIPO)
                    ? null
                    : CBTipoProducto.SelectedItem?.ToString();

                DataTable dt = (nombre == null && tipo == null)
                    ? _bllStock.ListarStockProductos()
                    : _bllStock.FiltrarStock(nombre, tipo);

                DGVStockProductos.DataSource = dt;
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }

        private void BTNComprarProducto_Click(object sender, EventArgs e)
        {
            LBLProductoCantidad comprarProducto = new LBLProductoCantidad();
            comprarProducto.Show();
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}