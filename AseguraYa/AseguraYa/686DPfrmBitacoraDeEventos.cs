using _686DP_BE;
using _686DP_BLL;
using _686DP_SERVICIOS.Singleton;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using _686DP_SERVICIOS.Observer;
using _686DP_SERVICIOS;

namespace AseguraYa
{
    public partial class _686DPfrmBitacoraDeEventos : Form
    {
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        public _686DPfrmBitacoraDeEventos(string idiomaLocal)
        {
            InitializeComponent();
            idi = idiomaLocal;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void _686DPfrmBitacoraDeEventos_Load(object sender, EventArgs e)
        {
            datagridSinFiltros();
            dateTimePicker1.ShowCheckBox = true;
            dateTimePicker2.ShowCheckBox = true;
            LMG.CargarMensajesGlobales(idi);
        }

        private void datagridSinFiltros()
        {
            List<_686DP_Evento> eventos = blle.TraerEventos();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = eventos;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            foreach (var crit in eventos.Select(ev => ev.DP686_Criticidad).Distinct())
            {
                CMBCriticidad.Items.Add(crit);
            }

            foreach (var mod in eventos.Select(ev => ev.DP686_Modulo).Distinct())
            {
                CMBModulo.Items.Add(mod);
            }
        }

        private void BTNLimpiarFiltros_Click(object sender, EventArgs e)
        {
            datagridSinFiltros();
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            DateTime? fechaDesde = dateTimePicker1.Checked ? (DateTime?)dateTimePicker1.Value.Date : null;
            DateTime? fechaHasta = dateTimePicker2.Checked ? (DateTime?)dateTimePicker2.Value.Date : null;
            string modulo = !string.IsNullOrWhiteSpace(CMBModulo.Text) ? CMBModulo.Text : null;
            int? criticidad = CMBCriticidad.SelectedItem != null ? (int?)Convert.ToInt32(CMBCriticidad.SelectedItem) : null;
            int? dni = int.TryParse(TXTDNI.Text, out int d) ? (int?)d : null;

            List<_686DP_Evento> filtrados = blle.Filtrar(fechaDesde, fechaHasta, modulo, criticidad, dni);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = filtrados;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            clean();
        }

        private void clean()
        {
            TXTDNI.Text = "";
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
            CMBModulo.SelectedItem = null;
            CMBCriticidad.SelectedItem = null;
        }

        private void BTNImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar como PDF";
                saveFileDialog.FileName = "BitacoraDeEvento.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filepath = saveFileDialog.FileName;
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(filepath, FileMode.Create));
                    pdfDoc.Open();
                    pdfDoc.Add(new Paragraph("Bitacora de eventos"));
                    pdfDoc.Add(new Paragraph(" "));

                    PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
                    pdfTable.WidthPercentage = 100;

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                        pdfTable.AddCell(new PdfPCell(new Phrase(column.HeaderText)));

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                        foreach (DataGridViewCell cell in row.Cells)
                            pdfTable.AddCell(cell.Value?.ToString());

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();

                    MessageBox.Show(LMG.Traducir("PDFExito"));
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Registros impresos", 3);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                int dni = Convert.ToInt32(fila.Cells["DP686_DNI"].Value);

                _686DP_BLLUsuario bllu = new _686DP_BLLUsuario();
                _686DP_Usuarios usuario = bllu.TraerUsuarioCompleto(dni);

                if (usuario != null)
                {
                    TXTNombre.Text = usuario.DP686_Nombre;
                    TXTApellido.Text = usuario.DP686_Apellido;
                }
            }
        }
    }
}
