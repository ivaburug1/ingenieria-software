using BLL_391IAU;
using System;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class CobrarBoleto : Form, IObserver_391IAU
    {
        private string artista;
        private DateTime fecha;
        private int sectorCodigo;
        private decimal precioSector;
        private int cantidad;
        private int dniCliente;
        private int codigoEvento;
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        public CobrarBoleto(int dniCliente, int codigoEvento, string artista, DateTime fecha, int sectorCodigo, decimal precio, int cantidad)
        {
            InitializeComponent();

            this.dniCliente = dniCliente;
            this.codigoEvento = codigoEvento;
            this.artista = artista;
            this.fecha = fecha;
            this.sectorCodigo = sectorCodigo;
            this.precioSector = precio;
            this.cantidad = cantidad;
        }

        private void CobrarBoleto_Load(object sender, EventArgs e)
        {
            CargarRecibo();
            CBMedioDePago.SelectedIndex = 0;
            TXTNumeroDeTarjeta.Enabled = false;
            CBCantCuotas.Enabled = false;
            CBMedioDePago.SelectedIndexChanged += CBMedioDePago_SelectedIndexChanged;
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);

        }

        private void CargarRecibo()
        {
            LVMostrarRecibo.Items.Clear();
            LVMostrarRecibo.View = View.Details;
            LVMostrarRecibo.Columns.Clear();

            LVMostrarRecibo.Columns.Add("Campo", 150);
            LVMostrarRecibo.Columns.Add("Detalle", 250);

            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Artista", artista }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Fecha", fecha.ToShortDateString() }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Sector", sectorCodigo.ToString() }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Cantidad de Entradas", cantidad.ToString() }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Precio por Entrada", precioSector.ToString("C") }));
            LVMostrarRecibo.Items.Add(new ListViewItem(new string[] { "Total", (precioSector * cantidad).ToString("C") }));
        }

        private void CBMedioDePago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string medioPago = CBMedioDePago.SelectedItem?.ToString();

            if (medioPago == "Tarjeta Debito")
            {
                TXTNumeroDeTarjeta.Enabled = true;
                CBCantCuotas.Enabled = false;
                CBCantCuotas.SelectedIndex = -1;
            }
            else if (medioPago == "Tarjeta Credito")
            {
                TXTNumeroDeTarjeta.Enabled = true;
                CBCantCuotas.Enabled = true;
            }
            else
            {
                TXTNumeroDeTarjeta.Enabled = false;
                TXTNumeroDeTarjeta.Text = string.Empty;
                CBCantCuotas.Enabled = false;
                CBCantCuotas.SelectedIndex = -1;
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNConfirmarCobro_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            string medioPago = CBMedioDePago.SelectedItem?.ToString();
            string nroTarjeta = TXTNumeroDeTarjeta.Text.Trim();
            string cuotas = CBCantCuotas.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(medioPago) ||
               ((medioPago == "Tarjeta Debito" || medioPago == "Tarjeta Credito") && string.IsNullOrEmpty(nroTarjeta)) ||
               (medioPago == "Tarjeta Credito" && string.IsNullOrEmpty(cuotas)))
            {
                MessageBox.Show("Completar todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((medioPago == "Tarjeta Debito" || medioPago == "Tarjeta Credito") &&
                !System.Text.RegularExpressions.Regex.IsMatch(nroTarjeta, @"^\d{4}-\d{4}-\d{4}-\d{4}$"))
            {
                MessageBox.Show("El número de tarjeta debe tener el formato XXXX-XXXX-XXXX-XXXX.", "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de que quiere finalizar la compra?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                BLLBoleto bll = new BLLBoleto();
                bool exito = bll.RegistrarCompra(artista, fecha, sectorCodigo, precioSector, cantidad, dniCliente, codigoEvento);

                if (!exito)
                {
                    try
                    {
                        int dniVendedor = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        string vendedor = sm.UsuarioActual != null
                            ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                            : "Usuario";

                        BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();
                        bllBitacora.RegistrarEvento(
                            dniVendedor,
                            3,
                            "CobrarBoleto",
                            $"Error al registrar compra de boletos. Vendedor {vendedor}. Cliente {dniCliente}. Evento {codigoEvento}. Artista {artista}. Fecha {fecha:yyyy-MM-dd}. Sector {sectorCodigo}. Cantidad {cantidad}. MedioPago {medioPago}."
                        );
                    }
                    catch { }

                    MessageBox.Show("No se pudo registrar la compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    int dniVendedor = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    string vendedor = sm.UsuarioActual != null
                        ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                        : "Usuario";

                    BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();
                    bllBitacora.RegistrarEvento(
                        dniVendedor,
                        4,
                        "CobrarBoleto",
                        $"El usuario {vendedor} cobró una compra de boletos. Cliente {dniCliente}. Evento {codigoEvento}. Artista {artista}. Fecha {fecha:yyyy-MM-dd}. Sector {sectorCodigo}. Cantidad {cantidad}. Total {(precioSector * cantidad):C}. MedioPago {medioPago}."
                    );
                }
                catch { }

                MessageBox.Show("¡Compra realizada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    GenerarReciboPDF();
                }
                catch
                {
                }

                this.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    int dniVendedor = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();
                    bllBitacora.RegistrarEvento(
                        dniVendedor,
                        3,
                        "CobrarBoleto",
                        "Error inesperado al intentar cobrar boleto."
                    );
                }
                catch { }

                MessageBox.Show("Error al procesar el cobro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarReciboPDF()
        {
            try
            {
                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"Recibo_{dniCliente}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));

                doc.Open();

                iTextSharp.text.Font tituloFont = FontFactory.GetFont("Helvetica", 20, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font textoItalica = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.ITALIC);

                Paragraph titulo = new Paragraph("Recibo de Compra - StageLink", tituloFont);
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                doc.Add(new Paragraph("\n"));

                PdfPTable tabla = new PdfPTable(2);
                tabla.WidthPercentage = 100;

                void AddFila(string campo, string valor)
                {
                    tabla.AddCell(new PdfPCell(new Phrase(campo)) { BackgroundColor = BaseColor.LIGHT_GRAY });
                    tabla.AddCell(new PdfPCell(new Phrase(valor)));
                }

                AddFila("Artista", artista);
                AddFila("Fecha", fecha.ToShortDateString());
                AddFila("Sector", sectorCodigo.ToString());
                AddFila("Cantidad de Entradas", cantidad.ToString());
                AddFila("Precio por Entrada", precioSector.ToString("C"));
                AddFila("Total", (precioSector * cantidad).ToString("C"));
                AddFila("DNI Cliente", dniCliente.ToString());
                AddFila("Código Evento", codigoEvento.ToString());

                doc.Add(tabla);

                doc.Add(new Paragraph("\n\nGracias por su compra en StageLink.", textoItalica));

                doc.Close();

                MessageBox.Show($"PDF generado correctamente en:\n{ruta}",
                    "PDF creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LVMostrarRecibo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}