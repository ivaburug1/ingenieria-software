using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_391IAU;
using BE_391IAU;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class GestionDeUsuarios : Form, IObserver_391IAU
    {
        public GestionDeUsuarios()
        {
            InitializeComponent();

            DGVMuestraUsuarios.CellValueChanged += DGVMuestraUsuarios_CellValueChanged;
            DGVMuestraUsuarios.CurrentCellDirtyStateChanged += DGVMuestraUsuarios_CurrentCellDirtyStateChanged;
            DGVMuestraUsuarios.SelectionChanged += DGVMuestraUsuarios_SelectionChanged;

            CMBRolesEdicion.SelectedIndexChanged += CMBRolesEdicion_SelectedIndexChanged;

            CargarUsuarios();

            CargarRolesEnCombos();
            
            DGVMuestraUsuarios.Columns["DNI"].ReadOnly = true;
            DGVMuestraUsuarios.Columns["DNI"].DefaultCellStyle.BackColor = Color.LightGray;

            DGVMuestraUsuarios.Columns["Activo"].ReadOnly = true;
            DGVMuestraUsuarios.Columns["Activo"].DefaultCellStyle.BackColor = Color.LightGray;

            DGVMuestraUsuarios.Columns["Bloqueado"].ReadOnly = true;
            DGVMuestraUsuarios.Columns["Bloqueado"].DefaultCellStyle.BackColor = Color.LightGray;

            DGVMuestraUsuarios.Columns["Intentos"].ReadOnly = true;
            DGVMuestraUsuarios.Columns["Intentos"].DefaultCellStyle.BackColor = Color.LightGray;

            DGVMuestraUsuarios.Columns["IDRol"].Visible = false;
            DGVMuestraUsuarios.Columns["IDRol"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private DataTable dtUsuarios;
        private bool hayCambios = false;
        private bool emailsDesencriptados = false;
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        private void GestionDeUsuarios_Load(object sender, EventArgs e)
        {
            //BLLUsuario bll = new BLLUsuario();
            //bll.ReencriptarTodosLosEmails();

            //MessageBox.Show("Correos reencriptados correctamente.");
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
        }

        private void DGVMuestraUsuarios_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            hayCambios = true;
        }
        private void DGVMuestraUsuarios_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DGVMuestraUsuarios.IsCurrentCellDirty)
            {
                DGVMuestraUsuarios.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void CargarUsuarios()
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                BLLUsuario bll = new BLLUsuario();
                var lista = bll.ObtenerTodosLosUsuarios();

                dtUsuarios = new DataTable();
                dtUsuarios.Columns.Add("DNI", typeof(int));
                dtUsuarios.Columns.Add("Nombre");
                dtUsuarios.Columns.Add("Apellido");
                dtUsuarios.Columns.Add("Email");
                dtUsuarios.Columns.Add("Activo", typeof(bool));
                dtUsuarios.Columns.Add("Bloqueado", typeof(bool));
                dtUsuarios.Columns.Add("Intentos", typeof(int));
                dtUsuarios.Columns.Add("Idioma");
                dtUsuarios.Columns.Add("Contraseña");
                dtUsuarios.Columns.Add("Rol");
                dtUsuarios.Columns.Add("IDRol", typeof(int));

                dtUsuarios.PrimaryKey = new DataColumn[] { dtUsuarios.Columns["DNI"] };

                foreach (var u in lista)
                {
                    dtUsuarios.Rows.Add(
                        u.DNI_391IAU,
                        u.Nombre_391IAU,
                        u.Apellido_391IAU,
                        u.eMail_391IAU,
                        u.Activo_391IAU,
                        u.Bloqueado_391IAU,
                        u.Intentos_391IAU,
                        u.Idioma_391IAU,
                        u.Contraseña_391IAU,
                        u.RolNombre,
                        u.IDRol_391IAU
                    );
                }

                DGVMuestraUsuarios.DataSource = dtUsuarios;

                DGVMuestraUsuarios.Columns["Contraseña"].Visible = false;

                DGVMuestraUsuarios.Columns["DNI"].HeaderText = sm.Traducir("GestionUsuarios_Col_DNI");
                DGVMuestraUsuarios.Columns["Nombre"].HeaderText = sm.Traducir("GestionUsuarios_Col_Nombre");
                DGVMuestraUsuarios.Columns["Apellido"].HeaderText = sm.Traducir("GestionUsuarios_Col_Apellido");
                DGVMuestraUsuarios.Columns["Email"].HeaderText = sm.Traducir("GestionUsuarios_Col_Email");
                DGVMuestraUsuarios.Columns["Activo"].HeaderText = sm.Traducir("GestionUsuarios_Col_Activo");
                DGVMuestraUsuarios.Columns["Bloqueado"].HeaderText = sm.Traducir("GestionUsuarios_Col_Bloqueado");
                DGVMuestraUsuarios.Columns["Intentos"].HeaderText = sm.Traducir("GestionUsuarios_Col_Intentos");
                DGVMuestraUsuarios.Columns["Idioma"].HeaderText = sm.Traducir("GestionUsuarios_Col_Idioma");
                DGVMuestraUsuarios.Columns["Rol"].HeaderText = sm.Traducir("GestionUsuarios_Col_Rol");

                DGVMuestraUsuarios.Columns["IDRol"].HeaderText = sm.Traducir("GestionUsuarios_Col_IDRol");

                hayCambios = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_ErrorCargarUsuarios") + " " + ex.Message,
                    sm.Traducir("GestionUsuarios_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void BTNModificar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Modificar_SinSeleccion"),
                    sm.Traducir("GestionUsuarios_Titulo_Advertencia"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];

            try
            {
                string emailPlano = fila.Cells["Email"].Value?.ToString().Trim();

                BEUsuario u = new BEUsuario
                {
                    DNI_391IAU = Convert.ToInt32(fila.Cells["DNI"].Value),
                    Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                    Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                    eMail_391IAU = BLLUsuario.EncriptarAES(emailPlano),
                    Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                    Activo_391IAU = Convert.ToBoolean(fila.Cells["Activo"].Value),
                    Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                    Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                    IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
                };

                BLLUsuario bll = new BLLUsuario();
                if (bll.ModificarUsuario(u))
                {
                    MessageBox.Show(
                        sm.Traducir("GestionUsuarios_Modificar_OK"),
                        sm.Traducir("GestionUsuarios_Titulo_OK"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show(
                        sm.Traducir("GestionUsuarios_Modificar_Fallo"),
                        sm.Traducir("GestionUsuarios_Titulo_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Modificar_Error") + " " + ex.Message,
                    sm.Traducir("GestionUsuarios_Titulo_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (hayCambios)
            {
                DialogResult result = MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Cancelar_Advertencia"),
                    sm.Traducir("GestionUsuarios_Cancelar_Titulo"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                    return;
            }

            this.Close();
        }
        private void BTNActivar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Activar_SinSeleccion"));
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];
            int dni = Convert.ToInt32(fila.Cells["DNI"].Value);

            if (Convert.ToBoolean(fila.Cells["Activo"].Value))
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Activar_YaActivo"));
                return;
            }

            fila.Cells["Activo"].Value = true;

            string emailPlano = fila.Cells["Email"].Value?.ToString().Trim();

            BEUsuario u = new BEUsuario
            {
                DNI_391IAU = dni,
                Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                eMail_391IAU = BLLUsuario.EncriptarAES(emailPlano),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                Activo_391IAU = true,
                Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
            };

            BLLUsuario bll = new BLLUsuario();
            if (bll.ModificarUsuario(u))
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Activar_Exito"));
                CargarUsuarios();
            }
        }
        private void BTNDesactivar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Desactivar_SinSeleccion"));
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];
            int dni = Convert.ToInt32(fila.Cells["DNI"].Value);

            if (!Convert.ToBoolean(fila.Cells["Activo"].Value))
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Desactivar_YaDesactivado"));
                return;
            }

            fila.Cells["Activo"].Value = false;

            string emailPlano = fila.Cells["Email"].Value?.ToString().Trim();

            BEUsuario u = new BEUsuario
            {
                DNI_391IAU = dni,
                Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                eMail_391IAU = BLLUsuario.EncriptarAES(emailPlano),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                Activo_391IAU = false,
                Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
            };

            BLLUsuario bll = new BLLUsuario();
            if (bll.ModificarUsuario(u))
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Desactivar_Exito"));
                CargarUsuarios();
            }
        }
        private void BTNDesbloquear_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Desbloquear_SinSeleccion"));
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];

            if (!Convert.ToBoolean(fila.Cells["Bloqueado"].Value))
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Desbloquear_NoBloqueado"));
                return;
            }

            fila.Cells["Bloqueado"].Value = false;
            fila.Cells["Intentos"].Value = 0;

            string emailPlano = fila.Cells["Email"].Value?.ToString().Trim();

            BEUsuario u = new BEUsuario
            {
                DNI_391IAU = Convert.ToInt32(fila.Cells["DNI"].Value),
                Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                eMail_391IAU = BLLUsuario.EncriptarAES(emailPlano),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                Activo_391IAU = Convert.ToBoolean(fila.Cells["Activo"].Value),
                Bloqueado_391IAU = false,
                Intentos_391IAU = 0,
                IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
            };

            BLLUsuario bll = new BLLUsuario();

            if (bll.ModificarUsuario(u))
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Desbloquear_Exito"));
                CargarUsuarios();
            }
        }
        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            int cambiosAplicados = 0;
            BLLUsuario bll = new BLLUsuario();

            foreach (DataGridViewRow fila in DGVMuestraUsuarios.Rows)
            {
                if (fila.IsNewRow) continue;

                try
                {
                    string emailCelda = fila.Cells["Email"].Value?.ToString().Trim();
                    string emailAEnviar;

                    if (!BLLUsuario.EsBase64(emailCelda))
                    {
                        emailAEnviar = BLLUsuario.EncriptarAES(emailCelda);
                    }
                    else
                    {
                        emailAEnviar = emailCelda;
                    }

                    BEUsuario u = new BEUsuario
                    {
                        DNI_391IAU = Convert.ToInt32(fila.Cells["DNI"].Value),
                        Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                        Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                        eMail_391IAU = emailAEnviar,
                        Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                        Activo_391IAU = Convert.ToBoolean(fila.Cells["Activo"].Value),
                        Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                        Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                        IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value
                            ? Convert.ToInt32(fila.Cells["IDRol"].Value)
                            : (int?)null
                    };

                    if (bll.ModificarUsuario(u))
                        cambiosAplicados++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        sm.Traducir("GestionUsuarios_Aplicar_ErrorUsuario")
                        + fila.Cells["DNI"].Value + ": " + ex.Message);
                }
            }

            if (cambiosAplicados > 0)
            {
                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Aplicar_CambiosAplicados")
                    .Replace("{N}", cambiosAplicados.ToString())
                );

                CargarUsuarios();
            }
            else
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Aplicar_SinCambios"));
            }
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            if (dtUsuarios == null)
                return;

            List<string> filtros = new List<string>();

            if (CBActivoNoActivo.SelectedItem != null)
            {
                if (CBActivoNoActivo.SelectedItem.ToString() == "Activo")
                    filtros.Add("Activo = true");
                else if (CBActivoNoActivo.SelectedItem.ToString() == "No Activo")
                    filtros.Add("Activo = false");
            }

            if (CBUsuariosBloqueados.SelectedItem != null)
            {
                if (CBUsuariosBloqueados.SelectedItem.ToString() == "Bloqueado")
                    filtros.Add("Bloqueado = true");
                else if (CBUsuariosBloqueados.SelectedItem.ToString() == "Desbloqueado")
                    filtros.Add("Bloqueado = false");
            }

            if (CMBRolesFiltro.SelectedItem != null)
            {
                string nombreRol = CMBRolesFiltro.Text;
                if (nombreRol != "")
                    filtros.Add($"Rol = '{nombreRol}'");
            }

            string filtroFinal = string.Join(" AND ", filtros);

            DataView vistaFiltrada = new DataView(dtUsuarios);
            vistaFiltrada.RowFilter = filtroFinal;

            DGVMuestraUsuarios.DataSource = vistaFiltrada;
        }

        private void BTNLimpiarFiltros_Click(object sender, EventArgs e)
        {
            CBActivoNoActivo.SelectedIndex = -1;
            CBUsuariosBloqueados.SelectedIndex = -1;
            DGVMuestraUsuarios.DataSource = dtUsuarios;
        }

        private void BTNBuscar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (dtUsuarios == null)
                return;

            List<string> filtros = new List<string>();

            if (!string.IsNullOrWhiteSpace(TXTDNI.Text))
                filtros.Add($"Convert(DNI, 'System.String') LIKE '%{TXTDNI.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTNombre.Text))
                filtros.Add($"Nombre LIKE '%{TXTNombre.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTApellido.Text))
                filtros.Add($"Apellido LIKE '%{TXTApellido.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(TXTCorreo.Text))
                filtros.Add($"Email LIKE '%{TXTCorreo.Text.Trim()}%'");

            if (CBIdioma.SelectedItem != null)
                filtros.Add($"Idioma = '{CBIdioma.SelectedItem.ToString()}'");

            string filtroFinal = string.Join(" AND ", filtros);

            DataView vistaFiltrada = new DataView(dtUsuarios);
            vistaFiltrada.RowFilter = filtroFinal;

            if (vistaFiltrada.Count == 0)
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Buscar_SinResultados"));
            }

            DGVMuestraUsuarios.DataSource = vistaFiltrada;
        }


        private void BTNLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            TXTDNI.Clear();
            TXTNombre.Clear();
            TXTApellido.Clear();
            TXTCorreo.Clear();
            CBIdioma.SelectedIndex = -1;

            DGVMuestraUsuarios.DataSource = dtUsuarios;
        }

        private void BTNEliminar_Click(object sender, EventArgs e)
        {

        }

        private void DGVMuestraUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void DesencriptarEmails()
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    string correoEncriptado = row["Email"].ToString();
                    string correoDesencriptado = BLLUsuario.DesencriptarAES(correoEncriptado);
                    row["Email"] = correoDesencriptado;
                }

                DGVMuestraUsuarios.Refresh();
                MessageBox.Show(sm.Traducir("GestionUsuarios_Emails_Desencriptados"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(sm.Traducir("GestionUsuarios_Emails_ErrorDesencriptar") + ex.Message);
            }
        }

        private void BTNDesencriptar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (!emailsDesencriptados)
            {
                DesencriptarEmails();
                BTNDesencriptar.Text = sm.Traducir("GestionUsuarios_Emails_Boton_Encriptar");
                emailsDesencriptados = true;
            }
            else
            {
                CargarUsuarios();
                BTNDesencriptar.Text = sm.Traducir("GestionUsuarios_Emails_Boton_Desencriptar");
                emailsDesencriptados = false;
            }
        }
        private void CargarRolesEnCombos()
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            try
            {
                BLLPerfil bllPerfil = new BLLPerfil();
                List<BEPerfil> roles = bllPerfil.TraerPerfiles();

                CMBRolesFiltro.DataSource = roles.ToList();
                CMBRolesFiltro.DisplayMember = "Nombre_391IAU";
                CMBRolesFiltro.ValueMember = "IDRol_391IAU";
                CMBRolesFiltro.SelectedIndex = -1;

                CMBRolesEdicion.DataSource = roles.ToList();
                CMBRolesEdicion.DisplayMember = "Nombre_391IAU";
                CMBRolesEdicion.ValueMember = "IDRol_391IAU";
                CMBRolesEdicion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_ErrorCargarRoles") + ex.Message
                );
            }
        }
        private void DGVMuestraUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
                return;

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];

            if (fila.Cells["IDRol"].Value == DBNull.Value)
            {
                CMBRolesEdicion.SelectedIndex = -1;
                return;
            }

            int idRol = Convert.ToInt32(fila.Cells["IDRol"].Value);

            CMBRolesEdicion.SelectedValue = idRol;
        }
        private void CMBRolesEdicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
                return;

            if (CMBRolesEdicion.SelectedValue == null)
                return;

            int nuevoRolID = Convert.ToInt32(CMBRolesEdicion.SelectedValue);

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];
            fila.Cells["IDRol"].Value = nuevoRolID;

            fila.Cells["Rol"].Value = CMBRolesEdicion.Text;

            hayCambios = true;
        }
    }
}
