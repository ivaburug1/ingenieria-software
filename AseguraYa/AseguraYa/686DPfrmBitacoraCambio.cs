using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_BE;
using _686DP_BLL;
using _686DP_SERVICIOS.Observer;

namespace AseguraYa
{
    public partial class _686DPfrmBitacoraCambio : Form
    {
        string idioma;
        public _686DPfrmBitacoraCambio(string idiomaLocal)
        {
            InitializeComponent();
            idioma = idiomaLocal;
        }
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        
        _686DPBLLClienteC bllcc = new _686DPBLLClienteC();
        List<_686DPCliente_C> clientesC = new List<_686DPCliente_C>();

        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        private void Aplicar_Click(object sender, EventArgs e)
        {
            if (clientesC == null || !clientesC.Any())
            {
                MessageBox.Show(LMG.Traducir("NoHayFiltro"));
                return;
            }

            var filtrada = clientesC.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (int.TryParse(textBox2.Text.Trim(), out int dni))
                {
                    filtrada = filtrada.Where(c => c.DP686_DNI == dni);
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("SoloNumeros"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (dateTimePicker1.Checked)
            {
                DateTime fechaDesde = dateTimePicker1.Value.Date;
                filtrada = filtrada.Where(c => c.DP686_Fecha.Date >= fechaDesde);
            }

            if (dateTimePicker2.Checked)
            {
                DateTime fechaHasta = dateTimePicker2.Value.Date;
                filtrada = filtrada.Where(c => c.DP686_Fecha.Date <= fechaHasta);
            }

            dataGridView1.DataSource = filtrada.ToList();

            if (!filtrada.Any())
                MessageBox.Show(LMG.Traducir("NoRegistro"));
        }

        private void _686DPfrmBitacoraCambio_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idioma);
            dateTimePicker1.ShowCheckBox = true;
            dateTimePicker2.ShowCheckBox = true;
            clientesC = bllcc.TraerCambios();
            dataGridView1.DataSource = clientesC;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clientesC;
            dataGridView1.DataSource = clientesC;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
        }

        private void Desbloquear_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(LMG.Traducir("SeleccionarCliente"));
                return;
            }

            _686DPCliente_C seleccionado = (_686DPCliente_C)dataGridView1.SelectedRows[0].DataBoundItem;

            var duplicadoActivo = clientesC
                .FirstOrDefault(c => c.DP686_DNI == seleccionado.DP686_DNI &&
                                     c.ID != seleccionado.ID &&
                                     c.DP686_Estado == true);

            if (duplicadoActivo != null)
            {
                duplicadoActivo.DP686_Estado = false;
                duplicadoActivo.DP686_Activo = false;
                bllcc.ActualizarClienteC(duplicadoActivo);
            }

            seleccionado.DP686_Estado = true;
            seleccionado.DP686_Activo = true;
            bllcc.ActualizarClienteC(seleccionado);

            _686DP_BLLCLlientes bllClientes = new _686DP_BLLCLlientes();
            bllClientes.ReemplazarCliente(seleccionado);

            clientesC = bllcc.TraerCambios();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = clientesC;

            BLLDV.CalcularDigitoVerificador("Cliente");

            MessageBox.Show(LMG.Traducir("ClienteOK"));
        }
    }
}
