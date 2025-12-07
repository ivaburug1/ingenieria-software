using _686DP_BE;
using _686DP_BLL;
using _686DP_SERVICIOS.Singleton;
using iTextSharp.text.pdf;
using iTextSharp.text;
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
using iTextSharp.text.pdf.parser;
using _686DP_SERVICIOS.Observer;

namespace AseguraYa
{
    public partial class _686DP_frmReporteSiniestro : Form
    {
        private List<_686DP_Siniestro> listaSiniestrosOriginal;
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        public string idi = "";
        public _686DP_frmReporteSiniestro(string idiomaLocal)
        {
            LMG.CargarMensajesGlobales(idi);
            InitializeComponent();
            idi = idiomaLocal;
            cambiarIdioma();
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }

        _686DP_BLLSiniestro blls = new _686DP_BLLSiniestro();
        private void _686DP_frmReporteSiniestro_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            listaSiniestrosOriginal = blls.traerSiniestros(); 

            // Mostrás la lista en el DataGridView
            dataGridView1.DataSource = listaSiniestrosOriginal;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            comboBox1.Items.Clear(); // Limpia por si ya tenía datos

            foreach (string desc in listaSiniestrosOriginal
                                     .Where(s => !string.IsNullOrEmpty(s.descripcion))
                                     .Select(s => s.descripcion)
                                     .Distinct()
                                     .OrderBy(d => d)) // opcional: orden alfabético
            {
                comboBox1.Items.Add(desc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = blls.traerSiniestros();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            if (listaSiniestrosOriginal == null || !listaSiniestrosOriginal.Any())
                return;

            DateTime? fecha = DTFechaVencimiento.Checked ? (DateTime?)DTFechaVencimiento.Value.Date : null;
            string descripcion = comboBox1.SelectedItem?.ToString() ?? string.Empty;

            // Copiamos la lista original para filtrar
            var filtrada = listaSiniestrosOriginal.AsEnumerable();

            if (fecha != null)
                filtrada = filtrada.Where(s => s.Fecha.Date == fecha.Value.Date);

            if (!string.IsNullOrEmpty(descripcion))
            {
                filtrada = filtrada.Where(s =>
                    !string.IsNullOrEmpty(s.descripcion) &&
                    s.descripcion.IndexOf(descripcion, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Mostramos los resultados filtrados sin tocar la BD
            dataGridView1.DataSource = filtrada.ToList();

        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar Reporte de Siniestros";
                saveFileDialog.FileName = "ReporteSiniestros.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filepath = saveFileDialog.FileName;

                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(filepath, FileMode.Create));
                    pdfDoc.Open();

                    // 🔹 Título
                    pdfDoc.Add(new Paragraph("Reporte de Siniestros", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD)));
                    pdfDoc.Add(new Paragraph($"Generado el: {DateTime.Now}", FontFactory.GetFont("Arial", 10)));
                    pdfDoc.Add(new Paragraph(" "));

                    // 🔹 Tabla
                    PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
                    pdfTable.WidthPercentage = 100;

                    // Encabezados
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)));
                        cell.BackgroundColor = new BaseColor(230, 230, 230);
                        pdfTable.AddCell(cell);
                    }

                    // Filas
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string value = cell.Value != null ? cell.Value.ToString() : "";
                                pdfTable.AddCell(new Phrase(value, FontFactory.GetFont("Arial", 9)));
                            }
                        }
                    }

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Add(new Paragraph(" "));

                    // 🔹 Resumen general (sin EvaluacionSistema)
                    int total = dataGridView1.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
                    decimal promedioValor = 0;
                    decimal totalValor = 0;

                    if (total > 0)
                    {
                        totalValor = dataGridView1.Rows.Cast<DataGridViewRow>()
                            .Where(r => r.Cells["Valor"].Value != null)
                            .Sum(r => Convert.ToDecimal(r.Cells["Valor"].Value));

                        promedioValor = totalValor / total;
                    }

                    pdfDoc.Add(new Paragraph($"Total de siniestros: {total}", FontFactory.GetFont("Arial", 11)));
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph("Generado automáticamente por el sistema AseguraYA.", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.ITALIC)));

                    pdfDoc.Close();

                    MessageBox.Show(LMG.Traducir("PDFOK"), "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 🔹 Registrar evento
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    _686DP_BLLEvento blle = new _686DP_BLLEvento();
                    blle.RegistrarEvento(dni, this.Name, "Se generó el reporte de siniestros", 3);
                    System.Diagnostics.Process.Start(filepath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
