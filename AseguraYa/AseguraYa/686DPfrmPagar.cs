using _686DP_BLL;
using _686DP_SERVICIOS.Observer;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace AseguraYa
{
    public partial class _686DPfrmPagar : Form
    {
        _686DP_BLLSiniestro blls = new _686DP_BLLSiniestro();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        _686DP_BLLSiniestro bllsi = new _686DP_BLLSiniestro();
        public int CodSiniestro { get; set; }
        public int NroPoliza { get; set; }
        public string Descripcion { get; set; }
        public double Valor { get; set; }
        public string EvaluacionSistema { get; set; }

        string idioma;

        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();

        private bool pagoRealizado = false;
        public _686DPfrmPagar(int codSiniestro, int nroPoliza, string descripcion, double valor, string evaluacion, string idi)
        {
            InitializeComponent();

            CodSiniestro = codSiniestro;
            NroPoliza = nroPoliza;
            Descripcion = descripcion;
            Valor = valor;
            EvaluacionSistema = evaluacion;

           
            txtCodSiniestro.Text = codSiniestro.ToString();
            txtNroPoliza.Text = nroPoliza.ToString();
            txtDescripcion.Text = descripcion;
            txtValor.Text ="$"+ valor.ToString();
            txtEvaluacion.Text = evaluacion;
            idioma = idi;
            registrarForm();
            this.FormClosing += _686DPfrmPagar_FormClosing;
        }

        public _686DPfrmPagar()
        {
            InitializeComponent();
        }

        private void _686DPfrmPagar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pagoRealizado) return;

            e.Cancel = true;

            var result = MessageBox.Show(
                LMG.Traducir("PagoRequerido"),
                LMG.Traducir("Requerido"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                try
                {
                    BTNPagar.PerformClick();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorPago")+ ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (result == DialogResult.No)
            {
                bllsi.DenegarSiniestro(CodSiniestro);
                BLLDV.CalcularDigitoVerificador("Factura");
            }
            else
            {
            }
        }



        private void _686DPfrmPagar_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idioma);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void BTNPagar_Click(object sender, EventArgs e)
        {
            blls.Pagar(CodSiniestro);
            pagoRealizado = true;
            Console.WriteLine(LMG.Traducir("PagoOk"));
            BLLDV.CalcularDigitoVerificador("Factura");
            GenerarPDF();
            this.Close();
        }

        private void GenerarPDF()
        {
            try
            {
                string carpeta = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "AseguraYa_Pagos"
                );

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string nombreArchivo = $"Pago_Siniestro_{CodSiniestro}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string rutaCompleta = System.IO.Path.Combine(carpeta, nombreArchivo);

                using (FileStream fs = new FileStream(rutaCompleta, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    var titulo = new Paragraph("Comprobante de Pago de Siniestro")
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(titulo);
                    doc.Add(new Paragraph("\n"));

                    PdfPTable tabla = new PdfPTable(2);
                    tabla.WidthPercentage = 100;
                    tabla.AddCell("Código de Siniestro:");
                    tabla.AddCell(CodSiniestro.ToString());
                    tabla.AddCell("Número de Póliza:");
                    tabla.AddCell(NroPoliza.ToString());
                    tabla.AddCell("Descripción:");
                    tabla.AddCell(Descripcion);
                    tabla.AddCell("Evaluación del Sistema:");
                    tabla.AddCell(EvaluacionSistema);
                    tabla.AddCell("Valor:");
                    tabla.AddCell($"${Valor}");
                    tabla.AddCell("Fecha de Pago:");
                    tabla.AddCell(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    doc.Add(tabla);

                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("El siniestro ha sido procesado y pagado exitosamente."));
                    doc.Close();
                    writer.Close();
                    
                }

                MessageBox.Show($"Comprobante generado correctamente en:\n{rutaCompleta}", "PDF generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(rutaCompleta);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void registrarForm()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idioma);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
