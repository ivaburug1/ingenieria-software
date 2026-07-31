using System;
using System.Data;
using System.Windows.Forms;
using BLL_391IAU;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Drawing.Printing;

namespace StageLink
{
    public partial class ReporteRFN2 : Form
    {
        private readonly BLLReportes _bll = new BLLReportes();

        public ReporteRFN2()
        {
            InitializeComponent();
            this.Load += ReporteRFN2_Load;
        }

        private void ReporteRFN2_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrid();
                CargarCombos();
                CargarReporteCompleto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el Reporte RFN2: " + ex.Message);
            }
        }

        private void ConfigurarGrid()
        {
            DGVReporteRFN2.AutoGenerateColumns = true;
            DGVReporteRFN2.ReadOnly = true;
            DGVReporteRFN2.AllowUserToAddRows = false;
            DGVReporteRFN2.AllowUserToDeleteRows = false;
            DGVReporteRFN2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVReporteRFN2.MultiSelect = false;
        }

        private void CargarCombos()
        {
            CBTipoProducto.Items.Clear();
            CBTipoProducto.Items.Add("-- Todos --");
            foreach (var t in _bll.ObtenerTiposProductoRFN2())
                CBTipoProducto.Items.Add(t);
            CBTipoProducto.SelectedIndex = 0;

            CBNombreProducto.Items.Clear();
            CBNombreProducto.Items.Add("-- Todos --");
            foreach (var p in _bll.ObtenerNombresProductoRFN2())
                CBNombreProducto.Items.Add(p);
            CBNombreProducto.SelectedIndex = 0;

            CBNombreProveedor.Items.Clear();
            CBNombreProveedor.Items.Add("-- Todos --");
            foreach (var pr in _bll.ObtenerNombresProveedorRFN2())
                CBNombreProveedor.Items.Add(pr);
            CBNombreProveedor.SelectedIndex = 0;

            CBTipoProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            CBNombreProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            CBNombreProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarReporteCompleto()
        {
            DataTable dt = _bll.ObtenerReporteRFN2();
            DGVReporteRFN2.DataSource = dt;
        }

        private string GetFiltro(ComboBox cb)
        {
            if (cb.SelectedItem == null) return null;

            string val = cb.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(val)) return null;
            if (val == "-- Todos --") return null;

            return val.Trim();
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = GetFiltro(CBTipoProducto);
                string producto = GetFiltro(CBNombreProducto);
                string proveedor = GetFiltro(CBNombreProveedor);

                DataTable dt = _bll.ObtenerReporteRFN2Filtrado(proveedor, producto, tipo);
                DGVReporteRFN2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar el Reporte RFN2: " + ex.Message);
            }
        }

        private void BTNLimpiarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                CBTipoProducto.SelectedIndex = 0;
                CBNombreProducto.SelectedIndex = 0;
                CBNombreProveedor.SelectedIndex = 0;

                CargarReporteCompleto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar filtros: " + ex.Message);
            }
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReporteRFN2_Load_1(object sender, EventArgs e)
        {

        }

        private void BTNImprimirReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVReporteRFN2.DataSource == null || DGVReporteRFN2.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.");
                    return;
                }

                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"ReporteRFN2_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));

                doc.Open();

                var tituloFont = FontFactory.GetFont("Helvetica", 18, iTextSharp.text.Font.BOLD);
                var italica = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.ITALIC);

                Paragraph titulo = new Paragraph("Reporte RFN2", tituloFont);
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph("\n"));

                PdfPTable tabla = new PdfPTable(DGVReporteRFN2.Columns.Count);
                tabla.WidthPercentage = 100;

                foreach (DataGridViewColumn col in DGVReporteRFN2.Columns)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(col.HeaderText))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    tabla.AddCell(celda);
                }

                foreach (DataGridViewRow row in DGVReporteRFN2.Rows)
                {
                    if (row.IsNewRow) continue;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        tabla.AddCell(new Phrase(cell.Value?.ToString() ?? ""));
                    }
                }

                doc.Add(tabla);

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph($"Generado el: {DateTime.Now}", italica));

                doc.Close();

                MessageBox.Show($"PDF generado correctamente:\n{ruta}",
                                "Reporte RFN2",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF del Reporte RFN2: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}