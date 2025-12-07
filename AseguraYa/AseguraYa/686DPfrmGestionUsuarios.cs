using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_BLL;
using _686DP_SERVICIOS;
using _686DP_BE;
using System.Text.RegularExpressions;
using _686DP_SERVICIOS.Observer;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using _686DP_SERVICIOS.Singleton;

namespace AseguraYa
{
    public partial class _686DPfrmGestionUsuarios : Form
    {
        string modo;
        _686DP_BLLUsuario _686DP_BLLUsuario;
        _686DP_ExpresionesRegulares _686DP_ExpresionesRegulares;
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        private bool esModoCrear = false;
        string dni = "";
        string nombre = "";
        string apellido = "";
        string email = "";
        string rol = "";
        string usuario = "";
        string contraseña = "";
        bool activo = true;
        bool bloqueado = false;
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        public List<string> roles;
        public _686DPfrmGestionUsuarios(string idiomaLocal)
        {
            InitializeComponent();
            this.DP_TXTEmail.Validating += new CancelEventHandler(this.DP_TXTEmail_Validating);

            _686DP_BLLUsuario = new _686DP_BLLUsuario();
            _686DP_ExpresionesRegulares = new _686DP_ExpresionesRegulares();
            idi = idiomaLocal;
            cambiarIdioma();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void DP_TXTEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DP_TXTEmail.Text) && !_686DP_ExpresionesRegulares._686DPEsEmail(DP_TXTEmail.Text))
            {
                MessageBox.Show(LMG.Traducir("EmailInvalido"));
                DP_TXTEmail.Clear();
                e.Cancel = true;
            }
        }

        private void DP_BTNCrear_Click(object sender, EventArgs e)
        {
            modo = "Crear";
            DP_TXTMessage.Text = LMG.Traducir("Modo")+" "+LMG.Traducir(modo);
            BTNModificar.Enabled = false;
            DP_BTNDesbloquear.Enabled = false;
            DP_BTNActivarEliminar.Enabled = false;
            DP_BTNCrear.Enabled = true;

            DP_BTNCancelar.Enabled = true;
            DP_BTNAplicar.Enabled = true;
            DP_TXTApellido.Enabled = true;
            DP_TXTDni.Enabled = true;
            DP_TXTEmail.Enabled = true;
            DP_TXTNombre.Enabled = true;
            DP_CMBRol.Enabled = true;

            DP_BTNAplicar.Enabled = true;
            DP_BTNCancelar.Enabled = true;
        }

        private void _686DPfrmGestionUsuarios_Load(object sender, EventArgs e)
        {   LMG.CargarMensajesGlobales(idi);
            cargarDataGrid();
            LLenarCombo();
            Resetear();
            
            int cantidadFilas = DP_Datagrid.Rows.Count;
            label11.Text =LMG.Traducir("CantUsuarios") + cantidadFilas.ToString();
            this.FormClosing += new FormClosingEventHandler(_686DPfrmGestionUsuarios_FormClosing);
            cambiarIdioma();
            DP_CMBActDact.DropDownStyle = ComboBoxStyle.DropDownList;
            DP_CMBBloqueados.DropDownStyle = ComboBoxStyle.DropDownList;
            DP_CMBRolesFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }
        private void cargarDataGrid()
        {
            this.DP_Datagrid.DataSource = null;
            this.DP_Datagrid.DataSource = _686DP_BLLUsuario._686DPTraerTodos();
            this.DP_Datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            if (this.DP_Datagrid.Columns.Contains("DP686_Contraseña"))
                this.DP_Datagrid.Columns["DP686_Contraseña"].Visible = false;

            if (this.DP_Datagrid.Columns.Contains("DP686_Usuario"))
                this.DP_Datagrid.Columns["DP686_Usuario"].Visible = false;

            if(this.DP_Datagrid.Columns.Contains("DP686_CambiarContraseña"))
                this.DP_Datagrid.Columns["DP686_CambiarContraseña"].Visible=false;

            foreach (DataGridViewColumn col in DP_Datagrid.Columns)
            {
                if (col.HeaderText != null)
                {
                    col.HeaderText = LMG.Traducir(col.HeaderText);
                }
            }
            foreach (DataGridViewRow fila in DP_Datagrid.Rows)
            {
                if (fila.Cells["DP686_Rol"].Value != null)
                {
                    string valorOriginal = fila.Cells["DP686_Rol"].Value.ToString();
                    string traducido = LMG.Traducir(valorOriginal);
                    string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                    if (limpio == valorOriginal)
                    {

                        fila.Cells["DP686_Rol"].Value = limpio;
                    }
                    else
                    {

                        fila.Cells["DP686_Rol"].Value = traducido;
                    }
                }
                if (fila.Cells["DP686_Idioma"].Value != null)
                {

                    string idioma = fila.Cells["DP686_Idioma"].Value.ToString();
                    string ITraducido = LMG.Traducir(idioma);
                    string Ilimpio = ITraducido.Replace("[", "").Replace("]", "").Trim();

                    if (Ilimpio == idioma)
                    {

                        fila.Cells["DP686_Idioma"].Value = Ilimpio;
                    }
                    else
                    {
                        fila.Cells["DP686_Idioma"].Value = ITraducido;
                    }
                }
            }

        }

        private void Resetear()
        {
            DP_Datagrid.ReadOnly = true;
            DP_TXTMessage.Text = LMG.Traducir("SeleccionarModo");
            BTNModificar.Enabled = true;
            DP_BTNDesbloquear.Enabled = true;
            DP_BTNActivarEliminar.Enabled = true;
            DP_BTNCrear.Enabled = true;

            DP_BTNCancelar.Enabled = true;
            DP_BTNAplicar.Enabled = true;
            DP_TXTApellido.Enabled = false;
            DP_TXTDni.Enabled = false;
            DP_TXTEmail.Enabled = false;
            DP_TXTNombre.Enabled = false;
            DP_CMBRol.Enabled = false;

            DP_BTNAplicar.Enabled = false;

            DP_TXTDni.Text = "";
            DP_TXTApellido.Text = "";
            DP_TXTNombre.Text = "";
            DP_TXTEmail.Text = "";
            DP_CMBRol.SelectedIndex = -1;
        }

        private void _686DPfrmGestionUsuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void _686DPActualizarFilaSeleccionada(object sender, EventArgs e)
        {
            if (DP_Datagrid.SelectedRows.Count == 0) return;

            DataGridViewRow fila = DP_Datagrid.SelectedRows[0];

            fila.Cells["DP686_Nombre"].Value = DP_TXTNombre.Text;
            fila.Cells["DP686_Apellido"].Value = DP_TXTApellido.Text;
            fila.Cells["DP686_Email"].Value = DP_TXTEmail.Text;
            fila.Cells["DP686_Rol"].Value = DP_CMBRol.SelectedItem?.ToString() ?? "";
        }

        private void LLenarCombo()
        {
            roles = _686DP_BLLUsuario._686DPtraerRoles()
                                    .Distinct()
                                    .ToList();

            DP_CMBRolesFiltro.Items.Clear();
            DP_CMBRol.Items.Clear();

            foreach (string rol in roles)
            {
                string RolTraducido = LMG.Traducir(rol);
                string limpio = RolTraducido.Replace("[", "").Replace("]", "").Trim();
                if (limpio == rol)
                {
                    DP_CMBRolesFiltro.Items.Add(rol);
                    DP_CMBRol.Items.Add(rol);
                }
                else
                {
                    DP_CMBRolesFiltro.Items.Add(RolTraducido);
                    DP_CMBRol.Items.Add(RolTraducido);
                }
            }

            DP_CMBBloqueados.Items.Clear();
            DP_CMBBloqueados.Items.Add(LMG.Traducir("Bloqueados"));
            DP_CMBBloqueados.Items.Add(LMG.Traducir("No Bloqueados"));

            DP_CMBActDact.Items.Clear();
            DP_CMBActDact.Items.Add(LMG.Traducir("Activados"));
            DP_CMBActDact.Items.Add(LMG.Traducir("Desactivados"));
        }

        private void DP_BTNDesbloquear_Click(object sender, EventArgs e)
        {
           
            modo = "desbloqueo";
            DP_TXTMessage.Text = LMG.Traducir("Modo") + " " + LMG.Traducir(modo);
            DP_BTNCrear.Enabled = false;
            BTNModificar.Enabled = false;
            DP_BTNDesbloquear.Enabled = true;
            DP_BTNActivarEliminar.Enabled = false;

            DP_BTNCancelar.Enabled = false;
            DP_BTNAplicar.Enabled = false;
            DP_TXTApellido.Enabled = false;
            DP_TXTDni.Enabled = false;
            DP_TXTEmail.Enabled = false;
            DP_TXTNombre.Enabled = false;
            DP_CMBRol.Enabled = false;

            DP_BTNAplicar.Enabled = true;
            DP_BTNCancelar.Enabled = true;
        }

        private void BTNModificar_Click(object sender, EventArgs e)
        {
            modo = "Modificar";
            DP_TXTMessage.Text = LMG.Traducir("Modo") + " " + LMG.Traducir(modo);
            DP_BTNCrear.Enabled = false;
            BTNModificar.Enabled = true;
            DP_BTNDesbloquear.Enabled = false;
            DP_BTNActivarEliminar.Enabled = false;

            DP_BTNCancelar.Enabled = true;
            DP_BTNAplicar.Enabled = true;
            DP_TXTApellido.Enabled = true;
            DP_TXTDni.Enabled = false;
            DP_TXTEmail.Enabled = true;
            DP_TXTNombre.Enabled = true;
            DP_CMBRol.Enabled = true;

            DP_BTNAplicar.Enabled = true;
            DP_BTNCancelar.Enabled = true;

            modo = "Modificar";
            if (DP_Datagrid.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = DP_Datagrid.SelectedRows[0];

                DP_TXTDni.Text = fila.Cells["DP686_DNI"].Value.ToString();
                DP_TXTNombre.Text = fila.Cells["DP686_Nombre"].Value.ToString();
                DP_TXTApellido.Text = fila.Cells["DP686_Apellido"].Value.ToString();
                DP_TXTEmail.Text = fila.Cells["DP686_Email"].Value.ToString();
                DP_CMBRol.SelectedItem = fila.Cells["DP686_Rol"].Value.ToString();
            }
            else
            {
                MessageBox.Show(LMG.Traducir("CamposIncompletos"));
 
                 BTNModificar.Enabled = true;
            }
        }

        private void DP_BTNActivarEliminar_Click(object sender, EventArgs e)
        {
            DP_Datagrid.ReadOnly = false;
            modo = "Activar";
            DP_TXTMessage.Text = LMG.Traducir("Modo") + " " + LMG.Traducir(modo);
            DP_BTNCrear.Enabled = false;
            BTNModificar.Enabled = false;
            DP_BTNDesbloquear.Enabled = false;
            DP_BTNActivarEliminar.Enabled = true;

            DP_BTNCancelar.Enabled = false;
            DP_BTNAplicar.Enabled = false;
            DP_TXTApellido.Enabled = false;
            DP_TXTDni.Enabled = false;
            DP_TXTEmail.Enabled = false;
            DP_TXTNombre.Enabled = false;
            DP_CMBRol.Enabled = false;
            DP_BTNAplicar.Enabled = true;
            DP_BTNCancelar.Enabled = true;
        }

        private void DP_BTNCancelar_Click(object sender, EventArgs e)
        {
            Resetear();
        }

        private void DP_BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void DP_BTNFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                bool? activo = null;
                bool? bloqueado = null;
                string rol = null;

                
                if (DP_CMBActDact.SelectedItem != null)
                {
                    string seleccionado = DP_CMBActDact.SelectedItem.ToString();
                    if (seleccionado == LMG.Traducir("Activados")) activo = true;
                    else if (seleccionado == LMG.Traducir("Desactivados")) activo = false;
                }

                
                if (DP_CMBBloqueados.SelectedItem != null)
                {
                    string seleccionado = DP_CMBBloqueados.SelectedItem.ToString();
                    if (seleccionado == LMG.Traducir("Bloqueados")) bloqueado = true;
                    else if (seleccionado == LMG.Traducir("No bloqueados")) bloqueado = false;
                }

                
                if (DP_CMBRolesFiltro.SelectedItem != null)
                {
                    string traducido = DP_CMBRolesFiltro.SelectedItem.ToString();
                    string clave = LMG.ObtenerClaveDesdeValor(traducido);
                    if(clave == null)
                    {
                        rol = traducido;
                    }
                    else
                    {
                        rol = clave;
                    }
                }


                DP_Datagrid.DataSource = _686DP_BLLUsuario._686DPFiltrarGridFlexible(rol, activo, bloqueado);
                foreach (DataGridViewRow fila in DP_Datagrid.Rows)
                {
                    if (fila.Cells["DP686_Rol"].Value != null)
                    {
                        string valorOriginal = fila.Cells["DP686_Rol"].Value.ToString();
                        string traducido = LMG.Traducir(valorOriginal);
                        string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                        if (limpio == valorOriginal)
                        {

                            fila.Cells["DP686_Rol"].Value = limpio;
                        }
                        else
                        {

                            fila.Cells["DP686_Rol"].Value = traducido;
                        }
                    }
                    if (fila.Cells["DP686_Idioma"].Value != null)
                    {

                        string idioma = fila.Cells["DP686_Idioma"].Value.ToString();
                        string ITraducido = LMG.Traducir(idioma);
                        string Ilimpio = ITraducido.Replace("[", "").Replace("]", "").Trim();
                        
                        if (Ilimpio == idioma)
                        {

                            fila.Cells["DP686_Idioma"].Value =Ilimpio;
                        }
                        else
                        {
                            fila.Cells["DP686_Idioma"].Value =ITraducido;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos al intentar filtrar:\n" + ex.Message, "Error SQL" );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al filtrar:\n" + ex.Message, "Error general" );
            }
        }


        private void DP_Datagrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = DP_Datagrid.Rows[e.RowIndex];

                dni = fila.Cells["DP686_DNI"].Value.ToString();
                nombre = fila.Cells["DP686_Nombre"].Value.ToString();
                apellido = fila.Cells["DP686_Apellido"].Value.ToString();
                email = fila.Cells["DP686_Email"].Value.ToString();
                rol = fila.Cells["DP686_Rol"].Value.ToString();
                usuario = fila.Cells["DP686_Usuario"].Value.ToString();
                contraseña = fila.Cells["DP686_Contraseña"].Value.ToString();
                activo = Convert.ToBoolean(fila.Cells["DP686_Activo"].Value);
                bloqueado = Convert.ToBoolean(fila.Cells["DP686_Bloqueado"].Value);
            }
        }

        private void DP_BTNAplicar_Click(object sender, EventArgs e)
        {
            if (modo == "Crear" || modo == "Modificar")
            {
                if (DP_TXTDni.Text == "" || DP_TXTApellido.Text == "" || DP_TXTNombre.Text == "" || DP_CMBRol.SelectedIndex == -1)
                {
                    MessageBox.Show(LMG.Traducir("CamposIncompletos"));
                    return;
                }
            }
            try
            {
                switch (modo)
                {
                    case "Crear":
                        {
                            try
                            {
                                int dni = int.Parse(DP_TXTDni.Text);


                                bool dniExiste = _686DP_BLLUsuario.ListaDeUsuarios.Any(emp => emp.DP686_DNI == dni);
                                if (dniExiste)
                                {
                                    MessageBox.Show(LMG.Traducir("UsuarioYaExiste"));
                                    return;
                                }

                                _686DPCriptoManager cm = new _686DPCriptoManager();
                                string usuario = DP_TXTNombre.Text + "." + DP_TXTApellido.Text;
                                string contra = DP_TXTDni.Text + "." + DP_TXTApellido.Text;
                                string contraHash = cm._686DPGetSHA256(contra);
                                string traducido = DP_CMBRol.SelectedItem.ToString();
                                string rol = LMG.ObtenerClaveDesdeValor(traducido);
                                if (rol == null)
                                {
                                    rol = traducido;
                                }

                                _686DP_Usuarios nuevo = new _686DP_Usuarios(
                                    dni,
                                    DP_TXTNombre.Text,
                                    DP_TXTApellido.Text,
                                    DP_TXTEmail.Text,
                                    rol,
                                    usuario,
                                    contraHash,
                                    true,
                                    false,
                                    false
                                );
                                nuevo.DP686_Idioma = "Español";
                                CrearUsuario(nuevo);
                                _686DP_BLLUsuario.ListaDeUsuarios.Add(nuevo);
                                this.DP_Datagrid.DataSource = null;
                                this.DP_Datagrid.DataSource = _686DP_BLLUsuario.ListaDeUsuarios;
                                this.DP_Datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                                _686DPGuardar();
                                MessageBox.Show(LMG.Traducir("UsuarioCreadoOK"));
                                Resetear();
                                return;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(LMG.Traducir("ErrorCrearUsuario") + ex.Message);
                            }
                            blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se creo el usuario " + usuario, 1);
                            break;
                        }
                    case "desbloqueo":
                        {
                            if (DP_Datagrid.SelectedRows.Count == 0)
                            {
                                MessageBox.Show(LMG.Traducir("DebeSeleccionarParaDesbloquear"));
                                return;
                            }

                            DataGridViewRow filaSeleccionada = DP_Datagrid.SelectedRows[0];
                            object valorBloqueado = filaSeleccionada.Cells["DP686_Bloqueado"].Value;

                            if (valorBloqueado == null || valorBloqueado == DBNull.Value)
                            {
                                MessageBox.Show(LMG.Traducir("CampoBloqueadoInvalido"));
                                return;
                            }

                            bool yaEstabaDesbloqueado = !Convert.ToBoolean(valorBloqueado);

                            if (yaEstabaDesbloqueado)
                            {
                                MessageBox.Show(LMG.Traducir("UsuarioYaDesbloqueado"));
                                return;
                            }

                            filaSeleccionada.Cells["DP686_Bloqueado"].Value = false;

                            string ContraseñaAnterior = filaSeleccionada.Cells["DP686_Contraseña"].Value.ToString();

                            string apellido = filaSeleccionada.Cells["DP686_Apellido"].Value.ToString();
                            int dni = Convert.ToInt32(filaSeleccionada.Cells["DP686_DNI"].Value);
                            string nuevaContraseña = dni + "." + apellido;
                            string usuario = filaSeleccionada.Cells["DP686_Usuario"].Value.ToString();

                            _686DP_BLLUsuario.GuardarContraseña(ContraseñaAnterior, dni);

                            _686DPCriptoManager cripto = new _686DPCriptoManager();
                            string nuevaContraseñaHash = cripto._686DPGetSHA256(nuevaContraseña);

                            filaSeleccionada.Cells["DP686_Contraseña"].Value = nuevaContraseñaHash;
                            _686DP_BLLUsuario._686DPReestablecerIntentos(dni);
                            _686DP_BLLUsuario._Cambiarcontraobligatorio(dni);

                            _686DPGuardar();
                            MessageBox.Show(LMG.Traducir("DesbloqueoExitoso") + nuevaContraseña);
                            Resetear();
                            blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se bloqueo/ desbloqueo el usuario" + usuario, 1);
                            break;
                        }
                    case "Modificar":
                        {
                            if (DP_Datagrid.SelectedRows.Count == 0)
                            {
                                MessageBox.Show(LMG.Traducir("DebeSeleccionarFila"));
                                return;
                            }

                            DataGridViewRow fila = DP_Datagrid.SelectedRows[0];
                            fila.Cells["DP686_Nombre"].Value = DP_TXTNombre.Text;
                            fila.Cells["DP686_Apellido"].Value = DP_TXTApellido.Text;
                            fila.Cells["DP686_Email"].Value = DP_TXTEmail.Text;
                            fila.Cells["DP686_Rol"].Value = DP_CMBRol.SelectedItem?.ToString() ?? "";

                            _686DPGuardar();

                            MessageBox.Show(LMG.Traducir("UsuarioModificadoOK"));
                            Resetear();
                            blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se modificó el usuario" + usuario, 1);
                            break;
                        }
                    case "Activar":
                        {

                            if (DP_Datagrid.SelectedRows.Count == 0)
                            {
                                MessageBox.Show(LMG.Traducir("DebeSeleccionarParaActivar"));
                                return;
                            }

                            DataGridViewRow filaSeleccionada = DP_Datagrid.SelectedRows[0];
                            object valorActivo = filaSeleccionada.Cells["DP686_Activo"].Value;

                            if (valorActivo == null || valorActivo == DBNull.Value)
                            {
                                MessageBox.Show(LMG.Traducir("CampoActivoInvalido"));
                                return;
                            }

                            bool estadoActual = Convert.ToBoolean(valorActivo);
                            filaSeleccionada.Cells["DP686_Activo"].Value = !estadoActual;

                            _686DPGuardar();
                            string mensaje = LMG.Traducir(estadoActual ? "UsuarioDesactivado" : "UsuarioActivado");
                            MessageBox.Show(mensaje);
                            Resetear();
                            blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se activo/desactivo el usuario " + usuario, 1);
                            break;
                        }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void CrearUsuario(_686DP_Usuarios nuevo)
        {
            _686DP_BLLUsuario bll = new _686DP_BLLUsuario();
            bll._686DPActualizarUsuarioExistente(nuevo);
        }

        private void _686DPGuardar()
        {
            try
            {
                _686DPCriptoManager cripto = new _686DPCriptoManager();

                foreach (DataGridViewRow fila in DP_Datagrid.Rows)
                {
                    if (fila.IsNewRow) continue;

                    int dni = Convert.ToInt32(fila.Cells["DP686_DNI"].Value);

                    string DP686_Rol = fila.Cells["DP686_Rol"].Value.ToString();



                    _686DP_Usuarios usr = _686DP_BLLUsuario.ListaDeUsuarios.FirstOrDefault(usuario => usuario.DP686_DNI == dni);

                    if (usr != null)
                    {
                        
                        usr.DP686_Nombre = fila.Cells["DP686_Nombre"].Value.ToString();
                        usr.DP686_Apellido = fila.Cells["DP686_Apellido"].Value.ToString();
                        usr.DP686_Email = fila.Cells["DP686_Email"].Value.ToString();
                        string rolGrid = fila.Cells["DP686_Rol"].Value.ToString();
                        usr.DP686_Rol = LMG.ObtenerClaveDesdeValor(rolGrid) ?? rolGrid;
                        usr.DP686_Rol = usr.DP686_Rol.Replace("[", "").Replace("]", "").Trim();
                        usr.DP686_Usuario = fila.Cells["DP686_Usuario"].Value.ToString();
                        usr.DP686_Contraseña = fila.Cells["DP686_Contraseña"].Value.ToString();
                        usr.DP686_Activo = Convert.ToBoolean(fila.Cells["DP686_Activo"].Value);
                        usr.DP686_Bloqueado = Convert.ToBoolean(fila.Cells["DP686_Bloqueado"].Value);
                        string idiomaGrid = fila.Cells["DP686_Idioma"].Value.ToString();
                        usr.DP686_Idioma = LMG.ObtenerClaveDesdeValor(idiomaGrid) ?? idiomaGrid;
                        usr.DP686_Idioma = usr.DP686_Idioma.Replace("[", "").Replace("]", "").Trim();

                        _686DP_BLLUsuario._686DPActualizarUsuarioExistente(usr);
                    }
                }

                MessageBox.Show(LMG.Traducir("CambiosAplicados"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorAplicarCambios") + ex.Message);
            }
        }

        private void DP_TXTDni_TextChanged(object sender, EventArgs e)
        {
            if (DP_TXTDni.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(DP_TXTDni.Text.ToString()))
                    {
                        MessageBox.Show(LMG.Traducir("SoloNumeros"));
                        DP_TXTDni.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message);
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
                        MessageBox.Show(LMG.Traducir("SoloLetras"));
                        DP_TXTApellido.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message);
                }
            }
        }

        private void DP_TXTNombre_TextChanged(object sender, EventArgs e)
        {
            if (DP_TXTNombre.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsSoloLetras(DP_TXTNombre.Text))
                    {
                        MessageBox.Show(LMG.Traducir("SoloLetras"));
                        DP_TXTNombre.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message);
                }
            }
        }
        private void DP_TXTEmail_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void DP_TXTEmail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DP_TXTEmail.Text))
            {
                try
                {
                    if (!_686DP_ExpresionesRegulares._686DPEsEmail(DP_TXTEmail.Text))
                    {
                        MessageBox.Show(LMG.Traducir("EmailInvalido"));
                        DP_TXTEmail.Focus();
                        DP_TXTEmail.SelectAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ex.Message);
                }
            }
        }



        private void DP_CMBRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DP_Datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
