using BLL_391IAU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BE_391IAU;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class RegistrarProveedor : Form
    {
        private readonly BLLProveedor _bll = new BLLProveedor();

        private static readonly string[] TIPOS_PRODUCTO = new[]
        {
            "Bebida",
            "Comida",
            "Golosinas",
            "Refrigerados (Helados)",
            "Otros"
        };

        public RegistrarProveedor()
        {
            InitializeComponent();
        }

        private bool _cargandoEdicion = false;

        private ProveedorSnapshot _snapshotOriginal = null;

        private class ProveedorSnapshot
        {
            public long Cuit;
            public string Nombre;
            public string Correo;
            public List<ProductoSnapshot> Productos = new List<ProductoSnapshot>();
        }

        private class ProductoSnapshot
        {
            public int IdProducto;
            public string Nombre;
            public int Precio;
            public string Tipo;
        }
        private void RegistrarProveedor_Load(object sender, EventArgs e)
        {
            ConfigurarGridProductos();
            CargarComboEditarProveedores();
        }
        private void CargarComboEditarProveedores()
        {
            _cargandoEdicion = true;

            DataTable dt = _bll.ListarProveedores();

            DataRow dr = dt.NewRow();
            dr["CUIT_391IAU"] = 0;
            dr["Nombre_391IAU"] = "- Seleccione un Proveedor -";
            dt.Rows.InsertAt(dr, 0);

            CBElegirProveedor.DataSource = dt;
            CBElegirProveedor.DisplayMember = "Nombre_391IAU";
            CBElegirProveedor.ValueMember = "CUIT_391IAU";
            CBElegirProveedor.SelectedIndex = 0;

            _cargandoEdicion = false;
        }
        private void CBElegirProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoEdicion) return;

            try
            {
                if (CBElegirProveedor.SelectedValue == null) return;
                if (CBElegirProveedor.SelectedValue is DataRowView) return;

                long cuit = Convert.ToInt64(CBElegirProveedor.SelectedValue);
                if (cuit <= 0)
                {
                    LimpiarFormularioParaAlta();
                    return;
                }

                _cargandoEdicion = true;

                var prov = _bll.TraerProveedorPorCuit(cuit);
                if (prov == null) throw new Exception("No se encontró el proveedor seleccionado.");

                TXTProveedorCUIT.Text = prov.CUIT_391IAU.ToString();
                TXTNombreProveedor.Text = prov.Nombre_391IAU;
                TXTCorreoProveedor.Text = prov.Correo_391IAU;

                var productos = _bll.ListarProductosDeProveedor(cuit);

                DGVCargarProductos.Rows.Clear();

                AsegurarColumnaIdProducto();

                foreach (var p in productos)
                {
                    int rowIndex = DGVCargarProductos.Rows.Add();
                    var row = DGVCargarProductos.Rows[rowIndex];

                    row.Cells["IDProducto"].Value = p.IDProducto_391IAU;
                    row.Cells["NombreProducto"].Value = p.Nombre_391IAU;
                    row.Cells["Precio"].Value = p.PrecioVenta_391IAU;
                    row.Cells["TipoProducto"].Value = p.TipoProducto_391IAU.Trim();
                }

                _snapshotOriginal = CrearSnapshotActual();

                _cargandoEdicion = false;
            }
            catch (Exception ex)
            {
                _cargandoEdicion = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AsegurarColumnaIdProducto()
        {
            if (DGVCargarProductos.Columns.Contains("IDProducto")) return;

            var colId = new DataGridViewTextBoxColumn
            {
                Name = "IDProducto",
                HeaderText = "IDProducto",
                Visible = false
            };

            DGVCargarProductos.Columns.Insert(0, colId);
        }
        private ProveedorSnapshot CrearSnapshotActual()
        {
            long.TryParse((TXTProveedorCUIT.Text ?? "").Trim(), out long cuit);

            var snap = new ProveedorSnapshot
            {
                Cuit = cuit,
                Nombre = (TXTNombreProveedor.Text ?? "").Trim(),
                Correo = (TXTCorreoProveedor.Text ?? "").Trim(),
                Productos = new List<ProductoSnapshot>()
            };

            var filas = DGVCargarProductos.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .ToList();

            foreach (var row in filas)
            {
                int id = 0;
                if (row.Cells["IDProducto"]?.Value != null)
                    int.TryParse(row.Cells["IDProducto"].Value.ToString(), out id);

                string nombre = (row.Cells["NombreProducto"].Value ?? "").ToString().Trim();
                string precioStr = (row.Cells["Precio"].Value ?? "").ToString().Trim();
                int.TryParse(precioStr, out int precio);
                string tipo = (row.Cells["TipoProducto"].Value ?? "").ToString().Trim();

                if (string.IsNullOrWhiteSpace(nombre) && precio == 0 && string.IsNullOrWhiteSpace(tipo))
                    continue;

                snap.Productos.Add(new ProductoSnapshot
                {
                    IdProducto = id,
                    Nombre = nombre,
                    Precio = precio,
                    Tipo = tipo
                });
            }

            snap.Productos = snap.Productos
                .OrderBy(p => p.IdProducto == 0 ? int.MaxValue : p.IdProducto)
                .ThenBy(p => p.Nombre)
                .ToList();

            return snap;
        }
        private void ConfigurarGridProductos()
        {
            DGVCargarProductos.AutoGenerateColumns = false;
            DGVCargarProductos.Columns.Clear();

            var colNombre = new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Nombre Producto",
                DataPropertyName = "NombreProducto",
                Width = 220
            };

            var colPrecio = new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                Width = 120
            };

            var colTipo = new DataGridViewComboBoxColumn
            {
                Name = "TipoProducto",
                HeaderText = "Tipo de Producto",
                DataPropertyName = "TipoProducto",
                Width = 180,
                FlatStyle = FlatStyle.Flat,
                DataSource = TIPOS_PRODUCTO.ToList()
            };

            DGVCargarProductos.Columns.Add(colNombre);
            DGVCargarProductos.Columns.Add(colPrecio);
            DGVCargarProductos.Columns.Add(colTipo);

            DGVCargarProductos.AllowUserToAddRows = true;
            DGVCargarProductos.AllowUserToDeleteRows = true;
            DGVCargarProductos.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void BTNRegistrarProveedor_Click_1(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            long cuitLog = 0;
            string nombreProvLog = "-";
            int cantProductosLog = 0;

            try
            {
                string cuit = (TXTProveedorCUIT.Text ?? "").Trim();
                string nombreProv = (TXTNombreProveedor.Text ?? "").Trim();
                string correo = (TXTCorreoProveedor.Text ?? "").Trim();

                nombreProvLog = nombreProv;
                long.TryParse(cuit, out cuitLog);

                if (string.IsNullOrWhiteSpace(cuit) ||
                    string.IsNullOrWhiteSpace(nombreProv) ||
                    string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show(
                        "No se puede registrar el Proveedor porque sus campos no estan completos.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var filas = DGVCargarProductos.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .ToList();

                if (filas.Count == 0)
                {
                    MessageBox.Show(
                        "No se puede agregar un proveedor nuevo sin ningun producto asociado.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                List<ProductoAltaProveedor> productos = new List<ProductoAltaProveedor>();

                foreach (var row in filas)
                {
                    string nombreProd = (row.Cells["NombreProducto"].Value ?? "").ToString().Trim();
                    string precioStr = (row.Cells["Precio"].Value ?? "").ToString().Trim();
                    object tipoObj = row.Cells["TipoProducto"].Value;

                    string tipo = (tipoObj ?? "").ToString().Trim();

                    if (string.IsNullOrWhiteSpace(nombreProd) ||
                        string.IsNullOrWhiteSpace(precioStr) ||
                        string.IsNullOrWhiteSpace(tipo))
                    {
                        MessageBox.Show(
                            "Completar todos los campos del DataGridView",
                            "Validación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(precioStr, out int precio) || precio <= 0)
                    {
                        MessageBox.Show(
                            "El campo Precio debe ser un número entero mayor a 0.",
                            "Validación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (!TIPOS_PRODUCTO.Contains(tipo))
                    {
                        MessageBox.Show(
                            "Tipo de producto inválido. Use solo: Bebida, Comida, Golosinas, Refrigerados (Helados), Otros.",
                            "Validación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    productos.Add(new ProductoAltaProveedor
                    {
                        Nombre = nombreProd,
                        PrecioPallet = precio,
                        TipoProducto = tipo
                    });
                }

                cantProductosLog = productos.Count;

                int idProveedor = _bll.RegistrarProveedorConProductos(cuit, nombreProv, correo, productos);

                MessageBox.Show(
                    $"Proveedor registrado correctamente. ID: {idProveedor}",
                    "OK",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                try
                {
                    int dniUsuario = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    string nombreUsuario = sm.UsuarioActual != null
                        ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                        : "Usuario";

                    new BLLBitacoraEventos().RegistrarEvento(
                        dniUsuario,
                        3,
                        "RegistrarProveedor",
                        $"El usuario {nombreUsuario} registró el proveedor '{nombreProv}' (CUIT {cuit}) con {cantProductosLog} productos. IDProveedor={idProveedor}."
                    );
                }
                catch { }

                TXTProveedorCUIT.Clear();
                TXTNombreProveedor.Clear();
                TXTCorreoProveedor.Clear();
                DGVCargarProductos.Rows.Clear();

                try { CargarComboEditarProveedores(); } catch { }
            }
            catch (Exception ex)
            {
                try
                {
                    int dniUsuario = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    string nombreUsuario = sm.UsuarioActual != null
                        ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                        : "Usuario";

                    new BLLBitacoraEventos().RegistrarEvento(
                        dniUsuario,
                        3,
                        "RegistrarProveedor",
                        $"Excepción al registrar proveedor '{nombreProvLog}' (CUIT {cuitLog}) con {cantProductosLog} productos. Detalle: {ex.Message}"
                    );
                }
                catch { }

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNActualizarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                if (_snapshotOriginal == null)
                {
                    MessageBox.Show("Debe seleccionar un proveedor para editar.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cuitStr = (TXTProveedorCUIT.Text ?? "").Trim();
                string nombre = (TXTNombreProveedor.Text ?? "").Trim();
                string correo = (TXTCorreoProveedor.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(cuitStr) ||
                    string.IsNullOrWhiteSpace(nombre) ||
                    string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show("No se puede actualizar el Proveedor porque sus campos no estan completos.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!long.TryParse(cuitStr, out long cuit) || cuit <= 0)
                {
                    MessageBox.Show("El CUIT ingresado no es válido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var filas = DGVCargarProductos.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .ToList();

                if (filas.Count == 0)
                {
                    MessageBox.Show("No se puede dejar un proveedor sin ningun producto asociado.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<ProductoAltaProveedor> productos = new List<ProductoAltaProveedor>();

                foreach (var row in filas)
                {
                    int idProducto = 0;
                    if (DGVCargarProductos.Columns.Contains("IDProducto") && row.Cells["IDProducto"].Value != null)
                        int.TryParse(row.Cells["IDProducto"].Value.ToString(), out idProducto);

                    string nombreProd = (row.Cells["NombreProducto"].Value ?? "").ToString().Trim();
                    string precioStr = (row.Cells["Precio"].Value ?? "").ToString().Trim();
                    string tipo = (row.Cells["TipoProducto"].Value ?? "").ToString().Trim();

                    if (string.IsNullOrWhiteSpace(nombreProd) ||
                        string.IsNullOrWhiteSpace(precioStr) ||
                        string.IsNullOrWhiteSpace(tipo))
                    {
                        MessageBox.Show("Completar todos los campos del DataGridView",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(precioStr, out int precio) || precio <= 0)
                    {
                        MessageBox.Show("El campo Precio debe ser un número entero mayor a 0.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    productos.Add(new ProductoAltaProveedor
                    {
                        IdProducto = idProducto,
                        Nombre = nombreProd,
                        PrecioPallet = precio,
                        TipoProducto = tipo
                    });
                }

                var snapshotActual = CrearSnapshotActual();

                if (!HayCambios(_snapshotOriginal, snapshotActual))
                {
                    MessageBox.Show("No se detectaron cambios para reflejar en la base de datos.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _bll.ActualizarProveedorConProductos(cuit, nombre, correo, productos);

                MessageBox.Show($"Los cambios fueron guardados en la base de datos para el proveedor {nombre}.",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _snapshotOriginal = CrearSnapshotActual();
                CargarComboEditarProveedores();
                SeleccionarProveedorEnCombo(cuit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool HayCambios(ProveedorSnapshot a, ProveedorSnapshot b)
        {
            if (a == null || b == null) return true;

            if (!string.Equals(a.Nombre ?? "", b.Nombre ?? "", StringComparison.Ordinal)) return true;
            if (!string.Equals(a.Correo ?? "", b.Correo ?? "", StringComparison.Ordinal)) return true;

            if (a.Productos.Count != b.Productos.Count) return true;

            for (int i = 0; i < a.Productos.Count; i++)
            {
                var p1 = a.Productos[i];
                var p2 = b.Productos[i];

                if (p1.IdProducto != p2.IdProducto) return true;
                if (!string.Equals(p1.Nombre ?? "", p2.Nombre ?? "", StringComparison.Ordinal)) return true;
                if (p1.Precio != p2.Precio) return true;
                if (!string.Equals(p1.Tipo ?? "", p2.Tipo ?? "", StringComparison.Ordinal)) return true;
            }

            return false;
        }

        private void SeleccionarProveedorEnCombo(long cuit)
        {
            _cargandoEdicion = true;
            CBElegirProveedor.SelectedValue = cuit;
            _cargandoEdicion = false;
        }
        private void LimpiarFormularioParaAlta()
        {
            _cargandoEdicion = true;

            TXTProveedorCUIT.Clear();
            TXTNombreProveedor.Clear();
            TXTCorreoProveedor.Clear();

            DGVCargarProductos.Rows.Clear();

            AsegurarColumnaIdProducto();

            _snapshotOriginal = null;

            _cargandoEdicion = false;
        }
    }
}