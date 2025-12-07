using _686DP_SERVICIOS.Observer;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AseguraYa
{
    public class GeneradorDeSiniestro
    {
        public static void GenerarSiniestroBasico(
           int numeroPoliza, DateTime fecha, double valorBien, double valorReparacion,
           string descripcion, string idioma, _686DP_LanguajeManager LMG)
        {
            try
            {
                // 📁 Ruta destino del PDF
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Siniestro_{DateTime.Now:yyyyMMddHHmm}.pdf");

                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                // 🖋️ Fuentes y estilos
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.RED);
                var subTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                // 🔹 Encabezado
                Paragraph titulo = new Paragraph("Asegura YA - Reporte de Siniestro", titleFont);
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.SpacingAfter = 20;
                doc.Add(titulo);

                // 🔹 Datos del siniestro
                Paragraph subtitulo = new Paragraph(LMG.Traducir("Detalles del Siniestro"), subTitleFont);
                subtitulo.SpacingAfter = 10;
                doc.Add(subtitulo);

                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 2f, 3f });

                var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                var headerBgColor = new BaseColor(0, 102, 204);
                var cellBgColor = new BaseColor(245, 245, 245);

                void AddRow(string label, string value)
                {
                    PdfPCell cell1 = new PdfPCell(new Phrase(label, headerFont)) { BackgroundColor = headerBgColor, Padding = 5 };
                    PdfPCell cell2 = new PdfPCell(new Phrase(value, normalFont)) { BackgroundColor = cellBgColor, Padding = 5 };
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                }

                AddRow(LMG.Traducir("Fecha"), fecha.ToString("dd/MM/yyyy HH:mm"));
                AddRow(LMG.Traducir("Descripcion"), descripcion ?? "Sin descripción");
                AddRow(LMG.Traducir("Valor del Bien"), $"${valorBien:N2}");
                AddRow(LMG.Traducir("Valor de Reparacion"), $"${valorReparacion:N2}");
                AddRow(LMG.Traducir("Estado"), LMG.Traducir("Pendiente de Aprobación"));

                doc.Add(table);

                // 🔹 Observaciones
                doc.Add(new Paragraph("\n" + LMG.Traducir("LeyendaSiniestro"), normalFont));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(LMG.Traducir("Este reporte ha sido generado automáticamente por el sistema AseguraYA."), normalFont));

                // 🏁 Cierre
                doc.Close();

                MessageBox.Show(LMG.Traducir("SiniestroGeneradoOK") + $"\n{path}", LMG.Traducir("TituloPDFGenerado"));
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message, "Error");
            }
        }
    }
}
