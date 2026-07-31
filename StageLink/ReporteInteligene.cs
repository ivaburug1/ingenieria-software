using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL_391IAU;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace StageLink
{
    public partial class ReporteInteligene : Form
    {
        private readonly BLLReporteInteligente _bll = new BLLReporteInteligente();

        public ReporteInteligene()
        {
            InitializeComponent();
            this.Load += ReporteInteligene_Load;
        }

        private void ReporteInteligene_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrid();
                CargarComboArtistas();

                DataTable dt = CargarReporteCompleto();
                MostrarRecomendacionInicial(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Reporte Inteligente: " + ex.Message);
            }
        }

        private void ConfigurarGrid()
        {
            DGVReporteInteligente.AutoGenerateColumns = true;
            DGVReporteInteligente.ReadOnly = true;
            DGVReporteInteligente.AllowUserToAddRows = false;
            DGVReporteInteligente.AllowUserToDeleteRows = false;
            DGVReporteInteligente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVReporteInteligente.MultiSelect = false;
        }

        private void CargarComboArtistas()
        {
            CBEvento.Items.Clear();
            CBEvento.Items.Add("-- Todos --");

            List<string> artistas = _bll.ObtenerArtistas();
            foreach (var a in artistas)
                CBEvento.Items.Add(a);

            CBEvento.SelectedIndex = 0;
            CBEvento.DropDownStyle = ComboBoxStyle.DropDownList;

            if (ExisteControl("CBFecha"))
            {
                ComboBox cbFecha = (ComboBox)this.Controls.Find("CBFecha", true).FirstOrDefault();
                if (cbFecha != null)
                {
                    cbFecha.Items.Clear();
                    cbFecha.Items.Add("-- Todas --");
                    cbFecha.SelectedIndex = 0;
                    cbFecha.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }

        private void CargarComboFechasPorArtista(string artista)
        {
            ComboBox cbFecha = (ComboBox)this.Controls.Find("CBFecha", true).FirstOrDefault();
            if (cbFecha == null) return;

            cbFecha.Items.Clear();
            cbFecha.Items.Add("-- Todas --");

            var fechas = _bll.ObtenerFechasPorArtista(artista);

            foreach (var f in fechas)
                cbFecha.Items.Add(f);

            cbFecha.SelectedIndex = 0;
        }

        private DataTable CargarReporteCompleto()
        {
            DataTable dt = _bll.ObtenerReporteInteligente();
            DGVReporteInteligente.DataSource = dt;

            DGVReporteInteligente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            return dt;
        }

        private string GetFiltroArtista()
        {
            if (CBEvento.SelectedItem == null) return null;

            string val = CBEvento.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(val)) return null;
            if (val == "-- Todos --") return null;

            return val.Trim();
        }

        private DateTime? GetFiltroFecha()
        {
            ComboBox cbFecha = (ComboBox)this.Controls.Find("CBFecha", true).FirstOrDefault();
            if (cbFecha == null) return null;

            if (cbFecha.SelectedItem == null) return null;

            if (cbFecha.SelectedItem is DateTime dt)
                return dt;

            string txt = cbFecha.SelectedItem.ToString();
            if (txt == "-- Todas --") return null;

            if (DateTime.TryParse(txt, out DateTime parsed))
                return parsed;

            return null;
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                string artista = GetFiltroArtista();
                DateTime? fecha = GetFiltroFecha();

                DataTable dt = _bll.ObtenerReporteInteligenteFiltrado(artista, fecha);
                DGVReporteInteligente.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar Reporte Inteligente: " + ex.Message);
            }
        }

        private void BTNLimpiarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                CBEvento.SelectedIndex = 0;

                ComboBox cbFecha = (ComboBox)this.Controls.Find("CBFecha", true).FirstOrDefault();
                if (cbFecha != null)
                {
                    cbFecha.Items.Clear();
                    cbFecha.Items.Add("-- Todas --");
                    cbFecha.SelectedIndex = 0;
                }

                CargarReporteCompleto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar filtros: " + ex.Message);
            }
        }

        private void CBEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string artista = GetFiltroArtista();

                if (ExisteControl("CBFecha"))
                    CargarComboFechasPorArtista(artista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar fechas: " + ex.Message);
            }
        }

        private void CBNombreProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void DGVReporteInteligente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private bool ExisteControl(string nombre)
        {
            return this.Controls.Find(nombre, true).FirstOrDefault() != null;
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReporteInteligene_Load_1(object sender, EventArgs e)
        {

        }
        private void MostrarRecomendacionInicial(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            string msg =
                "Reporte Inteligente\n\n" +
                "Los eventos listados en esta pantalla NO tienen productos asociados (extras).\n\n" +
                "Recomendación: Vender productos a estos eventos para habilitar la venta de extras.";

            MessageBox.Show(msg, "Reporte Inteligente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTNImprimirReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVReporteInteligente.DataSource == null || DGVReporteInteligente.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para imprimir.", "Reporte Inteligente",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"ReporteInteligente_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));

                doc.Open();

                var tituloFont = FontFactory.GetFont("Helvetica", 18, iTextSharp.text.Font.BOLD);
                var italica = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.ITALIC);

                Paragraph titulo = new Paragraph("Reporte Inteligente - Eventos sin productos", tituloFont);
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph("\n"));

                int cols = DGVReporteInteligente.Columns.Count;
                PdfPTable tabla = new PdfPTable(cols);
                tabla.WidthPercentage = 100;

                foreach (DataGridViewColumn col in DGVReporteInteligente.Columns)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(col.HeaderText))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    tabla.AddCell(celda);
                }

                foreach (DataGridViewRow row in DGVReporteInteligente.Rows)
                {
                    if (row.IsNewRow) continue;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        tabla.AddCell(new Phrase(cell.Value?.ToString() ?? ""));
                    }
                }

                doc.Add(tabla);

                doc.Add(new Paragraph("\n"));

                doc.Add(new Paragraph(
                    "Recomendación: Vender productos a estos eventos para habilitar la venta de extras.",
                    italica));

                doc.Add(new Paragraph($"{DateTime.Now}", italica));

                doc.Close();

                MessageBox.Show($"PDF generado correctamente:\n{ruta}",
                    "Reporte Inteligente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}