using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_BE;
using _686DP_SERVICIOS.Observer;
using System.Numerics;

namespace AseguraYa
{
    public class _686DP_GeneradorDePolizas
    {
        public static void GenerarPolizaBasica(
    string nombre, string apellido, int dni, string domicilio, string email,
    string seguro, decimal prima, List<_686DP_Cobertura> coberturas,
    string idioma, _686DP_LanguajeManager LMG, int numeroPoliza)
        {
            try
            {
                // Normalización de strings
                nombre = nombre ?? string.Empty;
                apellido = apellido ?? string.Empty;
                domicilio = domicilio ?? string.Empty;
                email = email ?? string.Empty;
                seguro = string.IsNullOrWhiteSpace(seguro) ? "Seguro no informado" : seguro;

                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);

                // USAR System.IO.Path
                string path = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Poliza_{(apellido ?? "SinApellido")}_{dni}.pdf"
                );

                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                // Estilos
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.BLUE);
                var subTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                // Título
                var titulo = new Paragraph("Asegura YA", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                doc.Add(titulo);

                // Datos principales
                doc.Add(new Paragraph($"Asegurado: {nombre} {apellido}", normalFont));
                doc.Add(new Paragraph($"DNI: {dni}", normalFont));
                doc.Add(new Paragraph($"Número de Póliza: {numeroPoliza}", normalFont));
                doc.Add(new Paragraph($"Domicilio: {domicilio}", normalFont));
                doc.Add(new Paragraph($"Email: {email}", normalFont));
                doc.Add(new Paragraph($"Seguro Contratado: {seguro}", normalFont));
                doc.Add(new Paragraph($"Prima: ${prima:N2}", normalFont));
                doc.Add(new Paragraph($"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy}", normalFont));
                doc.Add(new Paragraph(" "));

                // Coberturas
                if (coberturas != null && coberturas.Count > 0)
                {
                    var subtitulo = new Paragraph("Coberturas Incluidas", subTitleFont)
                    {
                        SpacingAfter = 10
                    };
                    doc.Add(subtitulo);

                    PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 2f, 1f });

                    var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                    var headerBg = new BaseColor(0, 102, 204);
                    var cellBg = new BaseColor(245, 245, 245);

                    // Encabezados
                    table.AddCell(new PdfPCell(new Phrase("Cobertura", headerFont)) { BackgroundColor = headerBg, Padding = 5 });
                    table.AddCell(new PdfPCell(new Phrase("Suma Asegurada", headerFont)) { BackgroundColor = headerBg, Padding = 5 });

                    // Filas
                    foreach (var c in coberturas)
                    {
                        string descripcion = c?.DP686_Descripcion ?? "Sin descripción";
                        decimal suma = c?.DP686_SumaAsegurada ?? 0m;

                        table.AddCell(new PdfPCell(new Phrase(descripcion, normalFont)) { BackgroundColor = cellBg, Padding = 5 });
                        table.AddCell(new PdfPCell(new Phrase($"${suma:N2}", normalFont)) { BackgroundColor = cellBg, Padding = 5 });
                    }

                    doc.Add(table);
                }
                else
                {
                    doc.Add(new Paragraph("Sin coberturas registradas.", normalFont));
                }

                // Leyenda final
                doc.Add(new Paragraph("\nEsta póliza certifica la contratación del seguro seleccionado.", normalFont));

                doc.Close();

                // Abrir archivo
                MessageBox.Show($"Póliza generada correctamente:\n{path}", "PDF generado");

                var psi = new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = path,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message, "Error");
            }
        }


    }
}
