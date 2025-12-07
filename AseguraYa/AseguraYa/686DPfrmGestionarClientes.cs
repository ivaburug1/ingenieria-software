using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_BE;
using _686DP_BLL;
using _686DP_SERVICIOS;
using _686DP_SERVICIOS.Observer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AseguraYa
{
    public partial class _686DPfrmGestionarClientes : Form
    {
        private bool estaEncriptado = false;

        _686DP_ExpresionesRegulares _686DP_ExpresionesRegulares = new _686DP_ExpresionesRegulares();
        public _686DP_Cliente ClienteCreado { get; private set; }
        _686DP_BLLCLlientes bll = new _686DP_BLLCLlientes();
        string modo;
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DPCriptoManager cripto = new _686DPCriptoManager();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();  
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        public _686DPfrmGestionarClientes(string idiomaLocal)
        {
            InitializeComponent();
            this.TXTEmail.Validating += new CancelEventHandler(this.TXTEmail_Validating);

            idi = idiomaLocal;
        }

        private void _686DPfrmGestionarClientes_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idi);
            dataGridView1.ReadOnly = true;
            cargarDG();
            button4.Enabled = false;
            textBox1.Text = LMG.Traducir("SeleccionarModo");
            dataGridView1.ReadOnly = true ;
            cambiarIdioma();
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }
        private void cargarDG()
        {
            try
            {
                dataGridView1.DataSource = null;
                List<_686DP_Cliente> clientes = bll.TraerClientes();
                dataGridView1.DataSource = clientes;
                foreach (var cli in clientes)
                {
                    if (!string.IsNullOrWhiteSpace(cli.DP686_Email))
                        cli.DP686_Email = cripto._686DPGetAES256(cli.DP686_Email);
                }
                estaEncriptado = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    string traducido = LMG.Traducir(col.HeaderText);
                    if (!string.IsNullOrEmpty(traducido))
                        col.HeaderText = traducido;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message  );
            }
        }
        private void TXTEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTEmail.Text) && !_686DP_ExpresionesRegulares._686DPEsEmail(TXTEmail.Text))
            {
                MessageBox.Show(LMG.Traducir("EmailInvalido"));
                TXTEmail.Clear();
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modo = "Modificar";
            dataGridView1.ReadOnly = false;
            textBox1.Text = LMG.Traducir("Modo") + " " + LMG.Traducir(modo);
            button4.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = false;
            DP_TXTDNI.ReadOnly = true;

            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show(LMG.Traducir("DebeSeleccionarFila"));
                    return;
                }

                DataGridViewRow fila = dataGridView1.SelectedRows[0];

                // Rellenar los TextBox directamente desde las celdas
                DP_TXTDNI.Text = fila.Cells["DP686_DNI"].Value?.ToString() ?? "";
                DP_TXTNombre.Text = fila.Cells["DP686_Nombre"].Value?.ToString() ?? "";
                DP_TXTApellido.Text = fila.Cells["DP686_Apellido"].Value?.ToString() ?? "";
                TXTEmail.Text = fila.Cells["DP686_Email"].Value?.ToString() ?? "";
                TXTDomicilio.Text = fila.Cells["DP686_Domicilio"].Value?.ToString() ?? "";
                TXTCodigoPostal.Text = fila.Cells["DP686DP_CodigoPostal"].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorCargarClientes") + ex.Message  );
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DP_TXTApellido.Enabled = true;
            DP_TXTDNI.Enabled = true;
            DP_TXTNombre.Enabled = true;
            modo = "Crear";
            textBox1.Text = LMG.Traducir("Modo") + " " + LMG.Traducir(modo);

            button4.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                switch (modo)
                {
                    case "Crear":
                        if (string.IsNullOrWhiteSpace(DP_TXTDNI.Text) ||
                            string.IsNullOrWhiteSpace(DP_TXTNombre.Text) ||
                            string.IsNullOrWhiteSpace(DP_TXTApellido.Text) ||
                            string.IsNullOrWhiteSpace(TXTCodigoPostal.Text) ||
                            string.IsNullOrWhiteSpace(TXTEmail.Text) ||
                            string.IsNullOrWhiteSpace(TXTDomicilio.Text))
                            {
                            MessageBox.Show(LMG.Traducir("CamposIncompletos"), LMG.Traducir("CamposIncompletosTitulo"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        try
                        {
                            int dni = Convert.ToInt32(DP_TXTDNI.Text);
                            bool existe = bll.ValidarNuevo(dni);

                            if (existe)
                            {
                                MessageBox.Show(LMG.Traducir("ClienteYaRegistrado"));
                                return;
                            }
                            ClienteCreado = new _686DP_Cliente(dni, DP_TXTNombre.Text, DP_TXTApellido.Text);
                            ClienteCreado.DP686DP_CodigoPostal = Convert.ToInt32(TXTCodigoPostal.Text);
                            ClienteCreado.DP686_Email = TXTEmail.Text;
                            ClienteCreado.DP686_Domicilio = TXTDomicilio.Text;
                            bll.CrearCompleto(ClienteCreado);
                            MessageBox.Show(LMG.Traducir("ClienteCreado"));
                            int dniActual = _686DP_SERVICIOS.Singleton._686DP_Singleton.Instancia.Usuario._686DPDNI;
                            blle.RegistrarEvento(dniActual, this.Name, "Cliente creado con exito", 2);
                            cargarDG();
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show(LMG.Traducir("DNIInvalido"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(LMG.Traducir("ErrorInesperado") + ex.Message);
                        }
                        break;

                    case "Modificar":
                        try
                        {
                            if (dataGridView1.SelectedRows.Count == 0)
                            {
                                MessageBox.Show(LMG.Traducir("DebeSeleccionarFila"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                            DataGridViewRow fila = dataGridView1.SelectedRows[0];
                            int dniSeleccionado = Convert.ToInt32(fila.Cells["DP686_DNI"].Value);
                            _686DP_Cliente cliente = bll.clientes.FirstOrDefault(c => c.DP686_DNI == dniSeleccionado);

                            if (cliente == null)
                            {
                                MessageBox.Show(LMG.Traducir("NoSeEncontroCliente"));
                                break;
                            }


                            cliente.DP686_Nombre = DP_TXTNombre.Text;
                            cliente.DP686_Apellido = DP_TXTApellido.Text;
                            cliente.DP686_DNI = dniSeleccionado;
                            cliente.DP686_Domicilio = TXTDomicilio.Text;
                            cliente.DP686DP_CodigoPostal= Convert.ToInt32(TXTCodigoPostal.Text);
                            cliente.DP686_Estado = true;
                            cliente.DP686_Email = TXTEmail.Text;
                            bll.GrabarCliente(cliente);
                            int dniActual = _686DP_SERVICIOS.Singleton._686DP_Singleton.Instancia.Usuario._686DPDNI;
                            blle.RegistrarEvento(dniActual, this.Name, "Cliente modificado con exito", 2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(LMG.Traducir("ErrorModificar") + ex.Message);
                        }
                        break;

                    case "Eliminar":
                        try
                        {
                            if (dataGridView1.SelectedRows.Count == 0)
                            {
                                MessageBox.Show(LMG.Traducir("DebeSeleccionarParaEliminar"), LMG.Traducir("SeleccionarFila"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
                            object valorActivo = filaSeleccionada.Cells["DP686_Estado"].Value;

                            if (valorActivo == null || valorActivo == DBNull.Value)
                            {
                                MessageBox.Show(LMG.Traducir("CampoEstadoInvalido"), LMG.Traducir("ErrorDatos"));
                                return;
                            }


                            bool estadoActual = Convert.ToBoolean(valorActivo);
                            if (!estadoActual)
                            {
                                MessageBox.Show(LMG.Traducir("ClienteYaEliminado"), LMG.Traducir("ClienteEliminado"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            bool nuevoEstado = !estadoActual;

                            filaSeleccionada.Cells["DP686_Estado"].Value = nuevoEstado;

                            int dni = Convert.ToInt32(filaSeleccionada.Cells["DP686_DNI"].Value);

                            bll.eliminadologico(dni, nuevoEstado);

                            string mensaje = nuevoEstado ? LMG.Traducir("UsuarioActivado") : LMG.Traducir("UsuarioDesactivado");
                            MessageBox.Show(mensaje, LMG.Traducir("CambioEstado"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int dniActual = _686DP_SERVICIOS.Singleton._686DP_Singleton.Instancia.Usuario._686DPDNI;
                            blle.RegistrarEvento(dniActual, this.Name, "Cliente eliminado", 2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(LMG.Traducir("ErrorCambioEstado") + ex.Message);
                        }
                        break;

                    default:
                        MessageBox.Show(LMG.Traducir("SeleccioneModo"));
                        break;
                }
                BLLDV.CalcularDigitoVerificador("Cliente");
                cargarDG();
                textBox1.Text = LMG.Traducir("SeleccionarModo");
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorInesperado") + ex.Message);
            }
        
        }

        private void limpiar()
        {
            TXTCodigoPostal.Text = "";
            TXTDomicilio.Text = "";
            TXTEmail.Text = "";
            DP_TXTApellido.Text = "";
            DP_TXTDNI.Text = "";
            DP_TXTNombre.Text = "";
            button4.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            modo = "Eliminar";
            textBox1.Text = LMG.Traducir("Modo") + " " + LMG.Traducir(modo);
            button4.Enabled = true;
            button2.Enabled = false;
            button1.Enabled = false;
        }

        private void DP_TXTNombre_TextChanged(object sender, EventArgs e)
        {
            if (DP_TXTNombre.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsSoloLetras(DP_TXTNombre.Text))
                    {
                        MessageBox.Show("Solo se permiten caracteres alfabéticos"  );
                        DP_TXTNombre.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de validación: " + ex.Message  );
                }
            }
        }

        private void DP_TXTApellido_TextChanged(object sender, EventArgs e)
        {
            if (DP_TXTApellido.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsSoloLetras(DP_TXTApellido.Text))
                    {
                        MessageBox.Show("Solo se permiten caracteres alfabéticos"  );
                        DP_TXTApellido.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de validación: " + ex.Message  );
                }
            }
        }

        private void DP_TXTDNI_TextChanged(object sender, EventArgs e)
        {
            if (DP_TXTDNI.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(DP_TXTDNI.Text.ToString()))
                    {
                        MessageBox.Show(LMG.Traducir("SoloNumeros"));
                        DP_TXTDNI.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message  );
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                _686DPCriptoManager cripto = new _686DPCriptoManager();

                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    if (fila.IsNewRow) continue;

                    var valorCelda = fila.Cells["DP686_Email"].Value;
                    if (valorCelda == null) continue;

                    string contenido = valorCelda.ToString();

                    if (!string.IsNullOrWhiteSpace(contenido))
                    {
                        try
                        {
                            if (!estaEncriptado)
                            {
                                string encriptado = cripto._686DPGetAES256(contenido);
                                fila.Cells["DP686_Email"].Value = encriptado;
                            }
                            else
                            {
                                string desencriptado = cripto._686DPGetAESDecrypt(contenido).ToString();
                                fila.Cells["DP686_Email"].Value = desencriptado;
                            }
                        }
                        catch
                        {
                            string claveError = estaEncriptado ? "ErrorAlDesencriptar" : "ErrorAlEncriptar";
                            MessageBox.Show(LMG.Traducir(claveError) + contenido);
                        }
                    }
                }

                estaEncriptado = !estaEncriptado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorGeneralEncriptacion") + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TXTEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXTDomicilio_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXTCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTCodigoPostal.Text) && !_686DP_ExpresionesRegulares._686DPEsNumero(TXTCodigoPostal.Text))
            {
                MessageBox.Show(LMG.Traducir("SoloNumeros") );
                TXTCodigoPostal.Clear();
                TXTCodigoPostal.Focus();
            }
        }
        private void TXTEmail_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTEmail.Text) && !_686DP_ExpresionesRegulares._686DPEsEmail(TXTEmail.Text))
            {
                MessageBox.Show("Email inválido."  );
                TXTEmail.Clear();
                TXTEmail.Focus();
            }
        }

        private void TXTDomicilio_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTDomicilio.Text) && !_686DP_ExpresionesRegulares._686DPEsSoloLetras(TXTDomicilio.Text))
            {
                MessageBox.Show(LMG.Traducir("SoloLetras"));
                TXTDomicilio.Clear();
                TXTDomicilio.Focus();
            }
        }

        private void TXTCodigoPostal_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TXTCodigoPostal.Text) && !_686DP_ExpresionesRegulares._686DPEsNumero(TXTCodigoPostal.Text))
            {
                MessageBox.Show(LMG.Traducir("SoloNumeros"));
                TXTCodigoPostal.Clear();
                TXTCodigoPostal.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show(LMG.Traducir("DebeSeleccionarFila"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                saveFileDialog.Title = LMG.Traducir("GuardarArchivo");


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    List<_686DP_Cliente> clientesSeleccionados = new List<_686DP_Cliente>();


                    foreach (DataGridViewRow fila in dataGridView1.SelectedRows)
                    {
                        if (fila.IsNewRow) continue;


                        _686DP_Cliente cliente = new _686DP_Cliente (Convert.ToInt32(fila.Cells["DP686_DNI"].Value), fila.Cells["DP686_Nombre"].Value?.ToString(), fila.Cells["DP686_Apellido"].Value?.ToString());
                        cliente.DP686_Domicilio = fila.Cells["DP686_Domicilio"].Value?.ToString();
                        cliente.DP686DP_CodigoPostal = fila.Cells["DP686DP_CodigoPostal"].Value != null ? Convert.ToInt32(fila.Cells["DP686DP_CodigoPostal"].Value) : 0;
                        cliente.DP686_Email = fila.Cells["DP686_Email"].Value?.ToString();

                        clientesSeleccionados.Add(cliente);
                    }


                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<_686DP_Cliente>));
                    using (System.IO.FileStream fs = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                    {
                        serializer.Serialize(fs, clientesSeleccionados);
                    }


                    MessageBox.Show(LMG.Traducir("SerializacionExitosa"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorSerializar") + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.Title = LMG.Traducir("AbrirArchivo");


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<_686DP_Cliente>));
                    using (System.IO.FileStream fs = new System.IO.FileStream(openFileDialog.FileName, System.IO.FileMode.Open))
                    {
                        List<_686DP_Cliente> clientes = (List<_686DP_Cliente>)serializer.Deserialize(fs);
                        dataGridView2.DataSource = clientes;
                    }


                    MessageBox.Show(LMG.Traducir("DeserializacionExitosa"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorDeserializar") + ex.Message);
            }
        }
    }
}
