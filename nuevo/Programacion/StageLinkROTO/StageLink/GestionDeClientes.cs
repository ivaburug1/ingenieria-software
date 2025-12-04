using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL_391IAU;
using BE_391IAU;

namespace StageLink
{
    public partial class GestionDeClientes : Form
    {
        private DataTable dtClientes;
        private bool hayCambios = false;

        public GestionDeClientes()
        {
            InitializeComponent();

            DGVMuestraClientes.CellValueChanged += DGVMuestraClientes_CellValueChanged;
            DGVMuestraClientes.CurrentCellDirtyStateChanged += DGVMuestraClientes_CurrentCellDirtyStateChanged;

            CargarClientes();
        }
        private void GestionDeClientes_Load(object sender, EventArgs e)
        {

        }

        private void DGVMuestraClientes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DGVMuestraClientes.IsCurrentCellDirty)
                DGVMuestraClientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DGVMuestraClientes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            hayCambios = true;
        }

        private void CargarClientes()
        {
            try
            {
                BLLCliente bll = new BLLCliente();
                var lista = bll.ObtenerTodosLosClientes();

                dtClientes = new DataTable();
                dtClientes.Columns.Add("DNI", typeof(int));
                dtClientes.Columns.Add("Nombre");
                dtClientes.Columns.Add("Apellido");
                dtClientes.Columns.Add("Correo");

                dtClientes.PrimaryKey = new DataColumn[] { dtClientes.Columns["DNI"] };

                foreach (var c in lista)
                {
                    dtClientes.Rows.Add(
                        c.DNI_391IAU,
                        c.Nombre_391IAU,
                        c.Apellido_391IAU,
                        c.Correo_391IAU
                    );
                }

                DGVMuestraClientes.DataSource = dtClientes;

                DGVMuestraClientes.Columns["DNI"].ReadOnly = true;
                DGVMuestraClientes.Columns["Correo"].ReadOnly = true;

                DGVMuestraClientes.Columns["DNI"].DefaultCellStyle.BackColor = Color.LightGray;
                DGVMuestraClientes.Columns["Correo"].DefaultCellStyle.BackColor = Color.LightGray;

                hayCambios = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message);
            }
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            int cambios = 0;
            BLLCliente bll = new BLLCliente();

            foreach (DataGridViewRow fila in DGVMuestraClientes.Rows)
            {
                if (fila.IsNewRow) continue;

                try
                {
                    BECliente cli = new BECliente
                    {
                        DNI_391IAU = Convert.ToInt32(fila.Cells["DNI"].Value),
                        Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString()?.Trim(),
                        Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString()?.Trim(),
                        Correo_391IAU = fila.Cells["Correo"].Value?.ToString()?.Trim()
                    };

                    if (bll.ModificarCliente(cli))
                        cambios++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar cliente DNI "
                        + fila.Cells["DNI"].Value + ": " + ex.Message);
                }
            }

            if (cambios > 0)
            {
                MessageBox.Show($"Se aplicaron cambios a {cambios} cliente(s).");
                CargarClientes();
            }
            else
            {
                MessageBox.Show("No hay cambios para aplicar.");
            }
        }

        private void BTNBuscar_Click(object sender, EventArgs e)
        {
            List<string> filtros = new List<string>();

            if (!string.IsNullOrWhiteSpace(TXTDNI.Text))
                filtros.Add($"Convert(DNI, 'System.String') LIKE '%{TXTDNI.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTNombre.Text))
                filtros.Add($"Nombre LIKE '%{TXTNombre.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTApellido.Text))
                filtros.Add($"Apellido LIKE '%{TXTApellido.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTCorreo.Text))
                filtros.Add($"Correo LIKE '%{TXTCorreo.Text.Trim()}%'");

            string filtroFinal = string.Join(" AND ", filtros);

            DataView vista = new DataView(dtClientes);
            vista.RowFilter = filtroFinal;

            if (vista.Count == 0)
                MessageBox.Show("No se encontraron clientes.");

            DGVMuestraClientes.DataSource = vista;
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            if (hayCambios)
            {
                var result = MessageBox.Show(
                    "Hay cambios sin guardar. ¿Cancelar igual?",
                    "Cancelar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;
            }

            this.Close();
        }

        private void BTNAplicar_Click_1(object sender, EventArgs e)
        {
            int cambiosAplicados = 0;

            BLLCliente bll = new BLLCliente();

            foreach (DataGridViewRow fila in DGVMuestraClientes.Rows)
            {
                if (fila.IsNewRow) continue;

                try
                {
                    BECliente cli = new BECliente
                    {
                        DNI_391IAU = Convert.ToInt32(fila.Cells["DNI"].Value),
                        Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                        Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                        Correo_391IAU = fila.Cells["Correo"].Value?.ToString().Trim()
                    };

                    if (bll.ModificarCliente(cli))
                        cambiosAplicados++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar el cliente con DNI {fila.Cells["DNI"].Value}: {ex.Message}");
                }
            }

            if (cambiosAplicados > 0)
            {
                MessageBox.Show($"Se aplicaron los cambios a {cambiosAplicados} cliente(s).");
                CargarClientes();
            }
            else
            {
                MessageBox.Show("No se aplicaron cambios.");
            }
        }
        private void BTNCancelar_Click_1(object sender, EventArgs e)
        {
            if (hayCambios)
            {
                DialogResult result = MessageBox.Show(
                    "Hay cambios sin guardar. Si cancelás ahora, se perderán.\n\n" +
                    "Para guardar los cambios presioná el botón \"Aplicar\".\n\n" +
                    "¿Desea cancelar?",
                    "Cancelar operación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                    return;
            }

            this.Close();
        }
        private void BTNBuscar_Click_1(object sender, EventArgs e)
        {
            if (dtClientes == null)
                return;

            List<string> filtros = new List<string>();

            if (!string.IsNullOrWhiteSpace(TXTDNI.Text))
                filtros.Add($"Convert(DNI, 'System.String') LIKE '%{TXTDNI.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTNombre.Text))
                filtros.Add($"Nombre LIKE '%{TXTNombre.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTApellido.Text))
                filtros.Add($"Apellido LIKE '%{TXTApellido.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTCorreo.Text))
                filtros.Add($"Correo LIKE '%{TXTCorreo.Text.Trim()}%'");

            if (filtros.Count == 0)
            {
                MessageBox.Show("Completar al menos uno de los campos para poder buscar al cliente deseado.");
                return;
            }

            string filtroFinal = string.Join(" AND ", filtros);

            DataView vista = new DataView(dtClientes);
            vista.RowFilter = filtroFinal;

            if (vista.Count == 0)
            {
                MessageBox.Show("No se encontró ningún cliente en la búsqueda.");
            }

            DGVMuestraClientes.DataSource = vista;
        }

    }
}
