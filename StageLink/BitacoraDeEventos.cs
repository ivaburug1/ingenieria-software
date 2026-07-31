using BE_391IAU;
using BLL_391IAU;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace StageLink
{
    public partial class but : Form
    {
        private readonly BLLBitacoraEventos _bll = new BLLBitacoraEventos();
        private const string MODULO_AUDITORIA = "AuditoriaEventos";

        private bool _cargandoGrid = false;

        public but()
        {
            InitializeComponent();

            CBModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            CBCriticidad.DropDownStyle = ComboBoxStyle.DropDownList;

            DGVMuestraBitacora.SelectionChanged += DGVMuestraBitacora_SelectionChanged;

            DGVMuestraBitacora.MultiSelect = false;
            DGVMuestraBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVMuestraBitacora.ReadOnly = true;
            DGVMuestraBitacora.AllowUserToAddRows = false;
        }

        private void but_Load(object sender, EventArgs e)
        {
            try
            {
                InicializarCombos();
                CargarDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InicializarCombos()
        {
            CBCriticidad.Items.Clear();
            CBCriticidad.Items.Add("");
            for (int i = 1; i <= 4; i++) CBCriticidad.Items.Add(i.ToString());
            CBCriticidad.SelectedIndex = 0;

            CBModulo.Items.Clear();
            CBModulo.Items.Add("");

            var modulos = _bll.ObtenerModulos(MODULO_AUDITORIA);
            foreach (var m in modulos) CBModulo.Items.Add(m);

            CBModulo.SelectedIndex = 0;
        }

        private void CargarDefault()
        {
            DTPFechaDesde.Value = DateTime.Now.Date.AddDays(-30);
            DTPFechaHasta.Value = DateTime.Now.Date;

            TXTDNI.Text = "";
            TXTNombre.Text = "";
            TXTApellido.Text = "";

            CBModulo.SelectedIndex = 0;
            CBCriticidad.SelectedIndex = 0;

            FiltrarYRefrescar();
        }

        private void FiltrarYRefrescar()
        {
            var filtro = new BEFiltroBitacoraEventos
            {
                FechaDesde = DTPFechaDesde.Value.Date,
                FechaHasta = DTPFechaHasta.Value.Date
            };

            var modulo = (CBModulo.SelectedItem ?? "").ToString();
            filtro.Modulo = string.IsNullOrWhiteSpace(modulo) ? null : modulo;

            var crit = (CBCriticidad.SelectedItem ?? "").ToString();
            if (int.TryParse(crit, out int c)) filtro.Criticidad = c;

            var dniTxt = (TXTDNI.Text ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(dniTxt))
            {
                if (!int.TryParse(dniTxt, out int dni))
                    throw new Exception("El DNI debe ser numérico.");
                filtro.DNI = dni;
            }

            filtro.Nombre = null;
            filtro.Apellido = null;

            _cargandoGrid = true;

            DataTable dt = _bll.Consultar(filtro);

            DGVMuestraBitacora.AutoGenerateColumns = true;
            DGVMuestraBitacora.DataSource = dt;

            LBLCantidad.Text = $"Cantidad: {dt.Rows.Count}";

            _cargandoGrid = false;

            if (DGVMuestraBitacora.Rows.Count > 0)
            {
                DGVMuestraBitacora.ClearSelection();
                DGVMuestraBitacora.Rows[0].Selected = true;
            }
        }

        private void DGVMuestraBitacora_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargandoGrid) return;
            if (DGVMuestraBitacora.CurrentRow == null) return;
            if (DGVMuestraBitacora.CurrentRow.IsNewRow) return;

            try
            {
                var row = DGVMuestraBitacora.CurrentRow;

                string colDni = null;

                if (DGVMuestraBitacora.Columns.Contains("DNI")) colDni = "DNI";
                else if (DGVMuestraBitacora.Columns.Contains("DNI_391IAU")) colDni = "DNI_391IAU";
                else if (DGVMuestraBitacora.Columns.Contains("DNIUsuario_391IAU")) colDni = "DNIUsuario_391IAU";
                else if (DGVMuestraBitacora.Columns.Contains("DNIUsuario")) colDni = "DNIUsuario";

                if (colDni == null) return;

                var val = row.Cells[colDni].Value;
                if (val == null) return;

                if (!int.TryParse(val.ToString(), out int dni))
                    return;

                TXTDNI.Text = dni.ToString();

                BLLUsuario bllUsuario = new BLLUsuario();
                var u = bllUsuario.ObtenerUsuarioPorDNI(dni);

                if (u == null)
                {
                    TXTNombre.Text = "";
                    TXTApellido.Text = "";
                    return;
                }

                TXTNombre.Text = u.Nombre_391IAU;
                TXTApellido.Text = u.Apellido_391IAU;
            }
            catch
            {
                TXTNombre.Text = "";
                TXTApellido.Text = "";
            }
        }

        private void BTNFiltrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                FiltrarYRefrescar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Filtro inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BTNLimpiarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                CargarDefault();
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