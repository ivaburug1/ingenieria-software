using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL_391IAU;
using BE_391IAU;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class GestionDeClientes : Form, IObserver_391IAU
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
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        private void GestionDeClientes_Load(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
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
                var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

                MessageBox.Show(
                    sm.Traducir("GestionClientes_ErrorCargar") + " " + ex.Message,
                    sm.Traducir("GestionClientes_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

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
                    MessageBox.Show(
                        sm.Traducir("GestionClientes_ErrorModificarCliente") + " "
                        + fila.Cells["DNI"].Value + ": " + ex.Message,
                        sm.Traducir("GestionClientes_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            if (cambios > 0)
            {
                MessageBox.Show(
                    string.Format(sm.Traducir("GestionClientes_CambiosAplicados"), cambios),
                    sm.Traducir("GestionClientes_Exito"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CargarClientes();
            }
            else
            {
                MessageBox.Show(
                    sm.Traducir("GestionClientes_SinCambios"),
                    sm.Traducir("GestionClientes_Exito"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
        private void BTNBuscar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

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
            {
                MessageBox.Show(
                    sm.Traducir("GestionClientes_NoEncontrados"),
                    sm.Traducir("GestionClientes_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            DGVMuestraClientes.DataSource = vista;
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (hayCambios)
            {
                var result = MessageBox.Show(
                    sm.Traducir("GestionClientes_CambiosSinGuardarPregunta"),
                    sm.Traducir("GestionClientes_CancelarTitulo"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                    return;
            }

            this.Close();
        }

        private void BTNAplicar_Click_1(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

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
                    MessageBox.Show(
                        sm.Traducir("GestionClientes_ErrorActualizarCliente") + " "
                        + fila.Cells["DNI"].Value + ": " + ex.Message,
                        sm.Traducir("GestionClientes_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            if (cambiosAplicados > 0)
            {
                MessageBox.Show(
                    string.Format(sm.Traducir("GestionClientes_CambiosAplicados2"), cambiosAplicados),
                    sm.Traducir("GestionClientes_Exito"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CargarClientes();
            }
            else
            {
                MessageBox.Show(
                    sm.Traducir("GestionClientes_NoSeAplicaronCambios"),
                    sm.Traducir("GestionClientes_Exito"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
        private void BTNCancelar_Click_1(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (hayCambios)
            {
                DialogResult result = MessageBox.Show(
                    sm.Traducir("GestionClientes_CambiosSinGuardarDetalle"),
                    sm.Traducir("GestionClientes_CancelarOperacionTitulo"),
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
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

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
                MessageBox.Show(
                    sm.Traducir("GestionClientes_FiltrosVacios"),
                    sm.Traducir("GestionClientes_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string filtroFinal = string.Join(" AND ", filtros);

            DataView vista = new DataView(dtClientes);
            vista.RowFilter = filtroFinal;

            if (vista.Count == 0)
            {
                MessageBox.Show(
                    sm.Traducir("GestionClientes_NoEncontradoEnBusqueda"),
                    sm.Traducir("GestionClientes_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            DGVMuestraClientes.DataSource = vista;
        }
        private List<BECliente> ObtenerClientesSeleccionados()
        {
            var lista = new List<BECliente>();

            if (DGVMuestraClientes.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DGVMuestraClientes.SelectedRows)
                {
                    if (row.IsNewRow) continue;

                    lista.Add(new BECliente
                    {
                        DNI_391IAU = Convert.ToInt32(row.Cells["DNI"].Value),
                        Nombre_391IAU = row.Cells["Nombre"].Value?.ToString(),
                        Apellido_391IAU = row.Cells["Apellido"].Value?.ToString(),
                        Correo_391IAU = row.Cells["Correo"].Value?.ToString()
                    });
                }

                return lista;
            }

            var filas = new HashSet<int>();
            foreach (DataGridViewCell cell in DGVMuestraClientes.SelectedCells)
                filas.Add(cell.RowIndex);

            foreach (int i in filas)
            {
                var row = DGVMuestraClientes.Rows[i];
                if (row.IsNewRow) continue;

                lista.Add(new BECliente
                {
                    DNI_391IAU = Convert.ToInt32(row.Cells["DNI"].Value),
                    Nombre_391IAU = row.Cells["Nombre"].Value?.ToString(),
                    Apellido_391IAU = row.Cells["Apellido"].Value?.ToString(),
                    Correo_391IAU = row.Cells["Correo"].Value?.ToString()
                });
            }

            return lista;
        }
        private void MostrarClientesDeserializadosEnGrid(List<BECliente> clientes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DNI", typeof(int));
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido");
            dt.Columns.Add("Correo");

            foreach (var c in clientes)
            {
                dt.Rows.Add(c.DNI_391IAU, c.Nombre_391IAU, c.Apellido_391IAU, c.Correo_391IAU);
            }

            DGVClientesSerializacion.DataSource = dt;

            foreach (DataGridViewColumn col in DGVClientesSerializacion.Columns)
                col.ReadOnly = true;
        }
        private void BTNSerializar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                var seleccionados = ObtenerClientesSeleccionados();

                if (seleccionados.Count == 0)
                {
                    MessageBox.Show(
                        "Debe seleccionar al menos un cliente para serializar.",
                        "Serialización",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Title = "Guardar clientes serializados";
                    sfd.Filter = "XML (*.xml)|*.xml";
                    sfd.DefaultExt = "xml";
                    sfd.AddExtension = true;
                    sfd.FileName = $"Clientes_{DateTime.Now:dd-MM-yyyy}.xml";

                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;

                    BLLCliente bll = new BLLCliente();
                    bool ok = bll.SerializarClientesXml(seleccionados, sfd.FileName);

                    if (!ok)
                        throw new Exception("No se pudo serializar el archivo XML.");

                    MessageBox.Show(
                        "Serialización realizada con éxito.",
                        "Serialización",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al serializar: " + ex.Message,
                    "Serialización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BTNDeserializar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Seleccionar archivo XML";
                    ofd.Filter = "XML (*.xml)|*.xml";
                    ofd.Multiselect = false;

                    if (ofd.ShowDialog() != DialogResult.OK)
                        return;

                    BLLCliente bll = new BLLCliente();
                    var clientes = bll.DeserializarClientesXml(ofd.FileName);

                    MostrarClientesDeserializadosEnGrid(clientes);

                    MessageBox.Show(
                        "Deserialización realizada con éxito.",
                        "Deserialización",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al deserializar: " + ex.Message,
                    "Deserialización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
