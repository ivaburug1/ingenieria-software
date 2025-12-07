using _686DP_BLL;
using _686DP_SERVICIOS.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AseguraYa
{
    public partial class _686DPfrmAuditarSiniestro : Form
    {
        string idi;
        public _686DPfrmAuditarSiniestro(string idiomaLocal)
        {
            InitializeComponent();
            idi = idiomaLocal;
            registrarForm();
        }
        _686DP_BLLSiniestro bllsi = new _686DP_BLLSiniestro();
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void _686DPfrmAuditarSiniestro_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idi);
            cargar();

        }

        private void registrarForm()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }

        private void cargar()
        {
            dataGridView1.DataSource = bllsi.TraerDatosVista();
            dataGridView1.RowPrePaint += dataGridView1_RowPrePaint;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];

            if (row.Cells["EvaluacionSistema"].Value != null)

            {
                string evaluacion = row.Cells["EvaluacionSistema"].Value.ToString();

                if (evaluacion.StartsWith("CALIFICA"))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (evaluacion == "NO CALIFICA")
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void BTNAprobar_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(LMG.Traducir("FaltaSeleccion"), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = dataGridView1.SelectedRows[0];

            if (!dataGridView1.Columns.Contains("EvaluacionSistema"))
            {
                MessageBox.Show("No se encontró la columna 'EvaluacionSistema' en la grilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string evaluacion = fila.Cells["EvaluacionSistema"].Value?.ToString();

            if (string.IsNullOrEmpty(evaluacion))
            {
                MessageBox.Show("No se encontró información de evaluación para esta fila.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int codSiniestro = Convert.ToInt32(fila.Cells["CodSiniestro"].Value);

            if (evaluacion.StartsWith("CALIFICA"))
            {
                MessageBox.Show(LMG.Traducir("AprobadoOk"), "Aprobado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bllsi.AprobarSiniestro(codSiniestro);
                int nroPoliza = Convert.ToInt32(fila.Cells["DP686_NPoliza"].Value);
                string descripcion = fila.Cells["Descripcion"].Value?.ToString();
                double valor = Convert.ToDouble(fila.Cells["ValorRemunerar"].Value);
                BLLDV.CalcularDigitoVerificador("Siniestro");
                _686DPfrmPagar pagar = new _686DPfrmPagar(codSiniestro, nroPoliza, descripcion, valor, evaluacion, idi);
                pagar.ShowDialog();
                cargar();

            }
            else
            {
                MessageBox.Show(LMG.Traducir("AprobadoNOOK"), "No Aprobado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;

            CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            cm.SuspendBinding();

            if (checkBox1.Checked)
            {
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    if (fila.Cells["EvaluacionSistema"].Value != null)
                    {
                        string evaluacion = fila.Cells["EvaluacionSistema"].Value.ToString();
                        fila.Visible = evaluacion.StartsWith("CALIFICA");
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    fila.Visible = true;
                }
            }

            cm.ResumeBinding();
        }
    }
}
