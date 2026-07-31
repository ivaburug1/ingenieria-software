using BLL_391IAU;
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
using SessionManager_391IAU;

namespace StageLink
{
    public partial class Reportes : Form, IObserver_391IAU
    {
        private BLLReportes reportesBLL = new BLLReportes();

        public Reportes()
        {
            InitializeComponent();
            this.Load += Reportes_Load;
        }
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            CargarReporteVentas(); CargarCombos(); 
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
        }
        private void CargarReporteVentas()
        {
            try
            {
                DataTable dt = reportesBLL.ObtenerReporteVentas();
                DGVReporteVentas.DataSource = dt;

                var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;
                LBLResultados.Text = $"{sm.Traducir("Reportes_Resultados")} {DGVReporteVentas.Rows.Count}";
            }
            catch (Exception ex)
            {
                var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;
                MessageBox.Show($"{sm.Traducir("Reportes_ErrorCargar")} {ex.Message}");
            }
        }
        private void DGVReporteVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CargarCombos()
        {
            CMBFechaEvento.Items.Clear();
            CMBFechaEvento.Items.Add("");
            foreach (var fecha in reportesBLL.ObtenerFechas())
                CMBFechaEvento.Items.Add(fecha);

            CMBNombreComprador.Items.Clear();
            CMBNombreComprador.Items.Add("");
            foreach (var nombre in reportesBLL.ObtenerCompradores())
                CMBNombreComprador.Items.Add(nombre);

            CMBListaArtistas.Items.Clear();
            CMBListaArtistas.Items.Add("");
            foreach (var artista in reportesBLL.ObtenerArtistas())
                CMBListaArtistas.Items.Add(artista);

            CMBFechaEvento.SelectedIndex = 0;
            CMBNombreComprador.SelectedIndex = 0;
            CMBListaArtistas.SelectedIndex = 0;
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BTNAplicarFiltro_Click(object sender, EventArgs e)
        {
            DataTable dt = reportesBLL.ObtenerReporteVentas();
            DataView dv = dt.DefaultView;

            string filtro = "";

            if (!string.IsNullOrWhiteSpace(CMBFechaEvento.Text))
                filtro += $"FechaEvento = '{CMBFechaEvento.Text}'";

            if (!string.IsNullOrWhiteSpace(CMBNombreComprador.Text))
            {
                if (filtro != "") filtro += " AND ";
                filtro += $"Nombre = '{CMBNombreComprador.Text}'";
            }

            if (!string.IsNullOrWhiteSpace(CMBListaArtistas.Text))
            {
                if (filtro != "") filtro += " AND ";
                filtro += $"Artista = '{CMBListaArtistas.Text}'";
            }

            dv.RowFilter = filtro;
            DGVReporteVentas.DataSource = dv;

            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;
            LBLResultados.Text = $"{sm.Traducir("Reportes_Resultados")} {DGVReporteVentas.Rows.Count}";
        }
        private void BTNLimpiarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                CargarReporteVentas();

                CMBFechaEvento.SelectedIndex = 0;
                CMBNombreComprador.SelectedIndex = 0;
                CMBListaArtistas.SelectedIndex = 0;

                if (DGVReporteVentas.DataSource is DataView dv)
                    dv.RowFilter = string.Empty;
            }
            catch (Exception ex)
            {
                var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;
                MessageBox.Show($"{sm.Traducir("Reportes_ErrorLimpiarFiltros")} {ex.Message}");
            }
        }

        private void Reportes_Load_1(object sender, EventArgs e)
        {

        }
        private void BTNPDFReporte_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"ReporteVentas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));

                doc.Open();

                var tituloFont = FontFactory.GetFont("Helvetica", 18, iTextSharp.text.Font.BOLD);
                var italica = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.ITALIC);

                Paragraph titulo = new Paragraph(sm.Traducir("Reportes_PDFTitulo"), tituloFont);
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph("\n"));

                PdfPTable tabla = new PdfPTable(DGVReporteVentas.Columns.Count);
                tabla.WidthPercentage = 100;

                foreach (DataGridViewColumn col in DGVReporteVentas.Columns)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(col.HeaderText))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    tabla.AddCell(celda);
                }

                foreach (DataGridViewRow row in DGVReporteVentas.Rows)
                {
                    if (row.IsNewRow) continue;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        tabla.AddCell(new Phrase(cell.Value?.ToString() ?? ""));
                    }
                }

                doc.Add(tabla);

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph($"{sm.Traducir("Reportes_PDFGeneradoEl")} {DateTime.Now}", italica));

                doc.Close();

                MessageBox.Show($"{sm.Traducir("Reportes_PDFGenerado")} \n{ruta}",
                                sm.Traducir("BTNPDFReporte"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{sm.Traducir("Reportes_ErrorPDF")} {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
