using _686DP_BLL;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using _686DP_SERVICIOS.Observer;
using System.Text.RegularExpressions;

namespace AseguraYa
{
    public partial class _686DPfrmRecomendacionDeAumento : Form
    {
        _686DP_BLLSiniestro blls = new _686DP_BLLSiniestro();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        List<object> list = new List<object>();
        string idioma;
        public _686DPfrmRecomendacionDeAumento(string idiomaLocal)
        {
            InitializeComponent();
            idioma = idiomaLocal;
        }

        private void _686DPfrmRecomendacionDeAumento_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idioma);
            cambiarIdioma();
            dataGridView1.DataSource = blls.traerSiniestrosMayoresA5();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idioma);
        }

        private void AumentarCuota_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(LMG.Traducir("SeleccionarFilaCliente"),
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow fila = dataGridView1.SelectedRows[0];

            int dni = Convert.ToInt32(fila.Cells["DP686_DNI"].Value);
            int poliza = Convert.ToInt32(fila.Cells["DP686_NPoliza"].Value);
            double cuotaActual = Convert.ToDouble(fila.Cells["Couta Mensual"].Value);
            
            string valorIngresado = "";
            using (frmInputBox frm = new frmInputBox(LMG.Traducir("IngresePorcentaje"), LMG.Traducir("AumentoPoliza")))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    valorIngresado = frm.Resultado;
                }
                else
                {
                    return;
                }
            }

            if (!double.TryParse(valorIngresado, out double porcentaje))
            {
                MessageBox.Show(LMG.Traducir("NValido"), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double nuevaCuota = cuotaActual + (cuotaActual * porcentaje / 100.0);

            var confirmar = MessageBox.Show(
                $"DNI: {dni}\nPóliza: {poliza}\nCuota Actual: ${cuotaActual}\n" +
                $"Aumento: {porcentaje}%\nNueva Cuota: ${nuevaCuota:F2}\n\n" +
                $"¿Aplicar el aumento?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar == DialogResult.No)
                return;

            blls.CambiarCuota(poliza, nuevaCuota);

            MessageBox.Show(LMG.Traducir("AumentoAplicado"),
                "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = blls.traerSiniestrosMayoresA5();
            BLLDV.CalcularDigitoVerificador("Polizas");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(TXTDni.Text, @"^\d*$"))
            {
                TXTDni.Text = Regex.Replace(TXTDni.Text, @"[^\d]", "");
                TXTDni.SelectionStart = TXTDni.Text.Length;
                MessageBox.Show(LMG.Traducir("SoloNumeros"));
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(TXTCuotaHasta.Text, @"^\d*$"))
            {
                TXTCuotaHasta.Text = Regex.Replace(TXTCuotaHasta.Text, @"[^\d]", "");
                TXTCuotaHasta.SelectionStart = TXTCuotaHasta.Text.Length;
                MessageBox.Show(LMG.Traducir("SoloNumeros"));
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            int? dni = null;
            if (int.TryParse(TXTDni.Text, out int dniParseado))
                dni = dniParseado;

            double? cuotaDesde = null;
            if (double.TryParse(TXTCuotaDesde.Text, out double desdeParseado))
                cuotaDesde = desdeParseado;

            double? cuotaHasta = null;
            if (double.TryParse(TXTCuotaHasta.Text, out double hastaParseado))
                cuotaHasta = hastaParseado;

            int? cantidadMin = null;
            if (int.TryParse(CantSiniestro.Value.ToString(), out int cantParseada))
                cantidadMin = cantParseada;

            dataGridView1.DataSource =
                blls.traerSiniestrosFiltrados(dni, cuotaDesde, cuotaHasta, cantidadMin);
        }

        private void BTNLimpiarFiltro_Click(object sender, EventArgs e)
        {
            TXTDni.Text = "";
            TXTCuotaHasta.Text = "";
            TXTCuotaDesde.Text = "";
            CantSiniestro.Value = 0;
            dataGridView1.DataSource = blls.traerSiniestrosMayoresA5();
        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = $"Reporte_Recomendacion_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    fileName
                );

                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.BLUE);
                var subFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 11);

                var headerBg = new BaseColor(0, 102, 204);
                var cellBg = new BaseColor(245, 245, 245);

                var titulo = new Paragraph("Asegura YA", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 15
                };
                doc.Add(titulo);

                doc.Add(new Paragraph("Reporte de Recomendación de Aumento", subFont));
                doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), normalFont));
                doc.Add(new Paragraph("\n"));

                PdfPTable table = new PdfPTable(dataGridView1.Columns.Count)
                {
                    WidthPercentage = 100
                };

                float[] widths = dataGridView1.Columns
                    .Cast<DataGridViewColumn>()
                    .Select(c => (float)c.Width)
                    .ToArray();

                table.SetWidths(widths);

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, headerFont))
                    {
                        BackgroundColor = headerBg,
                        Padding = 5
                    };
                    table.AddCell(cell);
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    foreach (DataGridViewCell celda in row.Cells)
                    {
                        string texto = celda.Value?.ToString() ?? "";

                        PdfPCell cell = new PdfPCell(new Phrase(texto, normalFont))
                        {
                            BackgroundColor = cellBg,
                            Padding = 5
                        };

                        table.AddCell(cell);
                    }
                }

                doc.Add(table);

                doc.Close();

                MessageBox.Show(LMG.Traducir("PDFOK") + path, LMG.Traducir("Creado"));
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("PDFNOOK") + ex.Message, "Error");
            }
        }

        private void LBLDNI_Click(object sender, EventArgs e)
        {

        }

        private void TXTCuotaDesde_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(TXTCuotaDesde.Text, @"^\d*$"))
            {
                TXTCuotaDesde.Text = Regex.Replace(TXTCuotaDesde.Text, @"[^\d]", "");
                TXTCuotaDesde.SelectionStart = TXTCuotaDesde.Text.Length;
                MessageBox.Show(LMG.Traducir("SoloNumeros"));
            }
        }
    }
}
