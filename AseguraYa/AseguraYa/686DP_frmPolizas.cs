using _686DP_BE;
using _686DP_BLL;
using _686DP_SERVICIOS.Observer;
using System;
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
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using _686DP_SERVICIOS.Singleton;


namespace AseguraYa
{
    public partial class _686DP_frmPolizas : Form
    {
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        public _686DP_frmPolizas(string idiomaLocal)
        {
            InitializeComponent();
            idi = idiomaLocal;
        }
        _686DPBLLSeguro blls = new _686DPBLLSeguro();
        _686DP_BLLPoliza bllp = new _686DP_BLLPoliza();
        _686DP_BLLPlan bllpl = new _686DP_BLLPlan();
        _686DP_BLLCobertura bllc = new _686DP_BLLCobertura();
        _686DP_BLLCLlientes bllcli = new _686DP_BLLCLlientes();
        
        private void _686DP_frmPolizas_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idi);
            List<_686DP_Seguro> seguros = new List<_686DP_Seguro>();
            seguros = blls.Top3Productos();
            chart1.DataSource = seguros;
            chart1.Series.Clear();
            chart1.Series.Clear();
            var serie = chart1.Series.Add(LMG.Traducir("Ventas")); ;
            serie.ChartType = SeriesChartType.Column;
            serie["PointWidth"] = "0.5";

            int index = 0;
            foreach (var s in seguros)
            {
                int pointIndex = serie.Points.AddXY(LMG.Traducir(s.DP686_TipoProducto), s.cantidadVendida);
                serie.Points[pointIndex].Color = Color.FromArgb(100 + index * 50, 150, 200); 
                index++;
            }
            chart1.DataBind();

            List<_686DP_Poliza> polizas = bllp.TraerPolizas();
            int total = polizas.Count;
            int inactivas = polizas.Count(p => p.DP686_Estado == false);
            int activas = total - inactivas;

            chart2.Series.Clear();
            var serie2 = chart2.Series.Add(LMG.Traducir("Estado"));
            serie2.ChartType = SeriesChartType.Pie;
            serie2.Points.AddXY(LMG.Traducir("Activas"), activas);
            serie2.Points.AddXY(LMG.Traducir("Inactivas"), inactivas);
            chart2.DataBind();
            DTFechaVencimiento.Checked = false;
            CargarDG(); cambiarIdioma();
            CMBPlan.DropDownStyle = ComboBoxStyle.DropDownList;
            CMBEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            CMBPrima.DropDownStyle = ComboBoxStyle.DropDownList;
            CMBSeguro.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }

        private void CargarDG()
        {
            List<_686DP_Poliza> polizas = bllp.TraerPolizas();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            CMBEstado.Items.Clear();
            CMBPlan.Items.Clear();
            CMBPrima.Items.Clear();
            CMBSeguro.Items.Clear();

            dataGridView1.Columns.Add("NPoliza", LMG.Traducir("NPoliza"));
            dataGridView1.Columns.Add("DNI Titular", LMG.Traducir("DNI Titular"));
            dataGridView1.Columns.Add("Estado", LMG.Traducir("Estado"));
            dataGridView1.Columns.Add("Vencimiento", LMG.Traducir("FechaVencimiento"));
            dataGridView1.Columns.Add("Seguro", LMG.Traducir("Seguro"));
            dataGridView1.Columns.Add("Plan", LMG.Traducir("Plan"));
            dataGridView1.Columns.Add("Prima", LMG.Traducir("Prima"));
            dataGridView1.Columns.Add("ValorTotal", LMG.Traducir("ValorTotal"));
            foreach (_686DP_Poliza poliza in polizas)
            {
                _686DP_Seguro seg = bllp.TraerSeguro(poliza.DP686_CodSeguro);
                _686DP_Plan PLAN= bllp.TraerPlan(poliza.DP686_CodPlan);
                _686DP_Cliente cliente = bllcli.TraerClienteDePoliza(poliza.DP686_NPoliza);
                DateTime fechaVencimiento = poliza.DP686_FechaVencimiento;
                dataGridView1.Rows.Add(
                    poliza.DP686_NPoliza,
                    cliente.DP686_DNI,
                    poliza.DP686_Estado,
                    fechaVencimiento,
                    seg.DP686_TipoProducto,
                    PLAN.DP686_Franquicia,
                    PLAN.DP686_Prima,
                    poliza.DP686_valorTotal
                );
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Seguro"].Value != null)
                    {
                        string original = row.Cells["Seguro"].Value.ToString();
                        string traducido = LMG.Traducir(original);

                        if (!string.IsNullOrEmpty(traducido) && !traducido.StartsWith("[") && !traducido.EndsWith("]"))
                            row.Cells["Seguro"].Value = traducido;
                    }
                }

                int filaIndex = dataGridView1.Rows.Count - 2;
                //if (filaIndex >= 0 && fechaVencimiento.Date < DateTime.Today)
                //{
                //    dataGridView1.Rows[filaIndex].DefaultCellStyle.BackColor = Color.Red;
                //}
                string estadoTraducido = LMG.Traducir(poliza.DP686_Estado.ToString());
                if (!CMBEstado.Items.Contains(estadoTraducido))
                    CMBEstado.Items.Add(estadoTraducido);
                if (!CMBPlan.Items.Contains(PLAN.DP686_Franquicia)) CMBPlan.Items.Add(PLAN.DP686_Franquicia);
                if (!CMBPrima.Items.Contains(PLAN.DP686_Prima)) CMBPrima.Items.Add(PLAN.DP686_Prima);
                string tipoTraducido = LMG.Traducir(seg.DP686_TipoProducto);
                if (!CMBSeguro.Items.Contains(tipoTraducido))
                    CMBSeguro.Items.Add(tipoTraducido);
            }
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            DateTime? fecha = DTFechaVencimiento.Checked ? (DateTime?)DTFechaVencimiento.Value.Date : null;
            int? codpoliza = null;
            if (int.TryParse(TXTPoliza.Text, out int cod))
            {
                codpoliza = cod;
            }
            string seguroTraducido = CMBSeguro.SelectedItem?.ToString();
            string seguro = LMG.ObtenerClaveDesdeValor(seguroTraducido) ?? seguroTraducido;
            decimal? prima = CMBPrima.SelectedItem != null ? Convert.ToDecimal(CMBPrima.SelectedItem) : (decimal?)null;
            decimal? franquicia = CMBPlan.SelectedItem != null ? Convert.ToDecimal(CMBPlan.SelectedItem) : (decimal?)null;
            string estadoTraducido = CMBEstado.SelectedItem?.ToString();
            string estadoStr = LMG.ObtenerClaveDesdeValor(estadoTraducido) ?? estadoTraducido;
            bool? estado = !string.IsNullOrWhiteSpace(estadoStr) ? Convert.ToBoolean(estadoStr) : (bool?)null;

            List<_686DP_Poliza> resultado = bllp.Filtrar(codpoliza, seguro, prima, franquicia, estado, fecha);

            dataGridView1.Rows.Clear();
            foreach (_686DP_Poliza poliza in resultado)
            {
                _686DP_Seguro seg = bllp.TraerSeguro(poliza.DP686_CodSeguro);
                _686DP_Plan PLAN = bllp.TraerPlan(poliza.DP686_CodPlan);

                dataGridView1.Rows.Add(
                    poliza.DP686_NPoliza,
                    poliza.DP686_Estado,
                    poliza.DP686_FechaVencimiento.ToShortDateString(),
                    seg.DP686_TipoProducto,
                    PLAN.DP686_Franquicia,
                    PLAN.DP686_Prima,
                    poliza.DP686_valorTotal
                );
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Seguro"].Value != null)
                {
                    string original = row.Cells["Seguro"].Value.ToString();
                    string traducido = LMG.Traducir(original);

                    if (!string.IsNullOrEmpty(traducido) && !traducido.StartsWith("[") && !traducido.EndsWith("]"))
                        row.Cells["Seguro"].Value = traducido;
                }
            }
        }

        private void DTFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            DTFechaVencimiento.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar como PDF";
                saveFileDialog.FileName = "ReportePolizas.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filepath = saveFileDialog.FileName;
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(filepath, FileMode.Create));
                    pdfDoc.Open();
                    pdfDoc.Add(new Paragraph(LMG.Traducir("Reporte")));
                    pdfDoc.Add(new Paragraph(" "));

                    PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
                    pdfTable.WidthPercentage = 100;

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                        pdfTable.AddCell(new PdfPCell(new Phrase(column.HeaderText)));

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                        foreach (DataGridViewCell cell in row.Cells)
                            pdfTable.AddCell(cell.Value?.ToString());

                    pdfDoc.Add(pdfTable);
                    using (MemoryStream ms1 = new MemoryStream())
                    {
                        chart1.SaveImage(ms1, ChartImageFormat.Png);
                        iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(ms1.ToArray());
                        img1.ScaleToFit(500f, 300f);
                        img1.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(new Paragraph(LMG.Traducir("MasVendido")));
                        pdfDoc.Add(img1);
                    }

                    pdfDoc.Add(new Paragraph(" "));

                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        chart2.SaveImage(ms2, ChartImageFormat.Png);
                        iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(ms2.ToArray());
                        img2.ScaleToFit(500f, 300f);
                        img2.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(new Paragraph(LMG.Traducir("EstadoPoliza")));
                        pdfDoc.Add(img2);
                    }
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
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                var fila = dataGridView1.SelectedRows[0];
                int codPoliza = Convert.ToInt32(fila.Cells["NPoliza"].Value);
                _686DP_Poliza poliza = bllp.traerDatosPoliza(codPoliza);

                int codSeguro = poliza.DP686_CodSeguro;
                _686DP_Seguro seguro = blls.TraerProductosPorID(codSeguro);
                _686DP_Plan plan = bllpl.TraerPlanPorID(poliza.DP686_CodPlan);
                List<_686DP_Cobertura> coberturas = bllc.TraerCoberturasFiltrado(poliza.DP686_CodPlan);
                _686DP_Cliente cliente = bllcli.TraerClienteDePoliza(poliza.DP686_NPoliza);
                _686DP_GeneradorDePolizas.GenerarPolizaBasica(cliente.DP686_Nombre, cliente.DP686_Apellido, cliente.DP686_DNI, cliente.DP686_Domicilio, cliente.DP686_Email, seguro.DP686_TipoProducto, plan.DP686_Prima, coberturas, idi, LMG, poliza.DP686_NPoliza);
                int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                blle.RegistrarEvento(dni, this.Name, "Poliza impresa", 4);
            }
            else
            {
                MessageBox.Show("Seleccioná una fila.");
            }
        }

        private void TXTPoliza_TextChanged(object sender, EventArgs e)
        {
            _686DP_ExpresionesRegulares regex = new _686DP_ExpresionesRegulares();
            if(TXTPoliza.Text!="")
            {
                if (!regex._686DPEsNumero(TXTPoliza.Text))
                {
                    MessageBox.Show(LMG.Traducir("SoloNumeros"));
                    TXTPoliza.Text = "";
                }
            }
        }
    }
}
