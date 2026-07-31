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

                dtUsuarios.AcceptChanges();
                emailsDesencriptados = false;
                BTNDesencriptar.Text = sm.Traducir("GestionUsuarios_Emails_Boton_Desencriptar");

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

            int dniObjetivo = 0;
            string nombreObjetivo = "-";
            string apellidoObjetivo = "-";

            try
            {
                dniObjetivo = Convert.ToInt32(fila.Cells["DNI"].Value);
                nombreObjetivo = fila.Cells["Nombre"].Value?.ToString().Trim() ?? "-";
                apellidoObjetivo = fila.Cells["Apellido"].Value?.ToString().Trim() ?? "-";

                string emailPlano = fila.Cells["Email"].Value?.ToString().Trim();

                BEUsuario u = new BEUsuario
                {
                    DNI_391IAU = dniObjetivo,
                    Nombre_391IAU = nombreObjetivo,
                    Apellido_391IAU = apellidoObjetivo,
                    eMail_391IAU = PrepararEmailParaGuardar(emailPlano),
                    Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                    Activo_391IAU = Convert.ToBoolean(fila.Cells["Activo"].Value),
                    Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                    Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                    IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
                };

                BLLUsuario bll = new BLLUsuario();
                bool ok = bll.ModificarUsuario(u);

                if (ok)
                {
                    MessageBox.Show(
                        sm.Traducir("GestionUsuarios_Modificar_OK"),
                        sm.Traducir("GestionUsuarios_Titulo_OK"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        string nombreAdmin = sm.UsuarioActual != null
                            ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                            : "Usuario";

                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"El usuario {nombreAdmin} modificó al usuario DNI {dniObjetivo} ({nombreObjetivo} {apellidoObjetivo})."
                        );
                    }
                    catch { }

                    CargarUsuarios();
                }
                else
                {
                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"Error BD al modificar usuario DNI {dniObjetivo} ({nombreObjetivo} {apellidoObjetivo})."
                        );
                    }
                    catch { }

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
                try
                {
                    int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    new BLLBitacoraEventos().RegistrarEvento(
                        dniAdmin,
                        1,
                        "GestionUsuarios",
                        $"Excepción al modificar usuario DNI {dniObjetivo} ({nombreObjetivo} {apellidoObjetivo}). Detalle: {ex.Message}"
                    );
                }
                catch { }

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

            string nombre = fila.Cells["Nombre"].Value?.ToString().Trim() ?? "-";
            string apellido = fila.Cells["Apellido"].Value?.ToString().Trim() ?? "-";

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
                Nombre_391IAU = nombre,
                Apellido_391IAU = apellido,
                eMail_391IAU = BLLUsuario.EncriptarAES(emailPlano),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                Activo_391IAU = true,
                Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
            };

            try
            {
                BLLUsuario bll = new BLLUsuario();
                bool ok = bll.ModificarUsuario(u);

                if (ok)
                {
                    MessageBox.Show(sm.Traducir("GestionUsuarios_Activar_Exito"));

                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        string nombreAdmin = sm.UsuarioActual != null
                            ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                            : "Usuario";

                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"El usuario {nombreAdmin} activó al usuario DNI {dni} ({nombre} {apellido})."
                        );
                    }
                    catch { }

                    CargarUsuarios();
                }
                else
                {
                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"Error BD al activar usuario DNI {dni} ({nombre} {apellido})."
                        );
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    new BLLBitacoraEventos().RegistrarEvento(
                        dniAdmin,
                        1,
                        "GestionUsuarios",
                        $"Excepción al activar usuario DNI {dni} ({nombre} {apellido}). Detalle: {ex.Message}"
                    );
                }
                catch { }

                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Modificar_Error") + " " + ex.Message,
                    sm.Traducir("GestionUsuarios_Titulo_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

            string nombre = fila.Cells["Nombre"].Value?.ToString().Trim() ?? "-";
            string apellido = fila.Cells["Apellido"].Value?.ToString().Trim() ?? "-";

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
                Nombre_391IAU = nombre,
                Apellido_391IAU = apellido,
                eMail_391IAU = BLLUsuario.EncriptarAES(emailPlano),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                Activo_391IAU = false,
                Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
            };

            try
            {
                BLLUsuario bll = new BLLUsuario();
                bool ok = bll.ModificarUsuario(u);

                if (ok)
                {
                    MessageBox.Show(sm.Traducir("GestionUsuarios_Desactivar_Exito"));

                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        string nombreAdmin = sm.UsuarioActual != null
                            ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                            : "Usuario";

                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"El usuario {nombreAdmin} desactivó al usuario DNI {dni} ({nombre} {apellido})."
                        );
                    }
                    catch { }

                    CargarUsuarios();
                }
                else
                {
                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"Error BD al desactivar usuario DNI {dni} ({nombre} {apellido})."
                        );
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    new BLLBitacoraEventos().RegistrarEvento(
                        dniAdmin,
                        1,
                        "GestionUsuarios",
                        $"Excepción al desactivar usuario DNI {dni} ({nombre} {apellido}). Detalle: {ex.Message}"
                    );
                }
                catch { }

                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Modificar_Error") + " " + ex.Message,
                    sm.Traducir("GestionUsuarios_Titulo_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

            int dni = Convert.ToInt32(fila.Cells["DNI"].Value);
            string nombre = fila.Cells["Nombre"].Value?.ToString().Trim() ?? "-";
            string apellido = fila.Cells["Apellido"].Value?.ToString().Trim() ?? "-";

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
                DNI_391IAU = dni,
                Nombre_391IAU = nombre,
                Apellido_391IAU = apellido,
                eMail_391IAU = BLLUsuario.EncriptarAES(emailPlano),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                Activo_391IAU = Convert.ToBoolean(fila.Cells["Activo"].Value),
                Bloqueado_391IAU = false,
                Intentos_391IAU = 0,
                IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
            };

            try
            {
                BLLUsuario bll = new BLLUsuario();
                bool ok = bll.ModificarUsuario(u);

                if (ok)
                {
                    MessageBox.Show(sm.Traducir("GestionUsuarios_Desbloquear_Exito"));

                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        string nombreAdmin = sm.UsuarioActual != null
                            ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                            : "Usuario";

                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"El usuario {nombreAdmin} desbloqueó al usuario DNI {dni} ({nombre} {apellido})."
                        );
                    }
                    catch { }

                    CargarUsuarios();
                }
                else
                {
                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"Error BD al desbloquear usuario DNI {dni} ({nombre} {apellido})."
                        );
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    new BLLBitacoraEventos().RegistrarEvento(
                        dniAdmin,
                        1,
                        "GestionUsuarios",
                        $"Excepción al desbloquear usuario DNI {dni} ({nombre} {apellido}). Detalle: {ex.Message}"
                    );
                }
                catch { }

                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Modificar_Error") + " " + ex.Message,
                    sm.Traducir("GestionUsuarios_Titulo_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            var filasModificadas = dtUsuarios.Rows.Cast<DataRow>()
                .Where(r => r.RowState == DataRowState.Modified)
                .ToList();

            if (!filasModificadas.Any())
            {
                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Aplicar_SinCambios"),
                    sm.Traducir("GestionUsuarios_Titulo_Advertencia"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int cambiosAplicados = 0;
            int fallosBD = 0;
            int excepciones = 0;

            BLLUsuario bll = new BLLUsuario();

            foreach (DataRow dr in filasModificadas)
            {
                int dniObjetivo = 0;
                string nombre = "-";
                string apellido = "-";

                try
                {
                    dniObjetivo = Convert.ToInt32(dr["DNI"]);
                    nombre = dr["Nombre"]?.ToString().Trim() ?? "-";
                    apellido = dr["Apellido"]?.ToString().Trim() ?? "-";

                    string emailCelda = dr["Email"]?.ToString().Trim();

                    BEUsuario u = new BEUsuario
                    {
                        DNI_391IAU = dniObjetivo,
                        Nombre_391IAU = nombre,
                        Apellido_391IAU = apellido,
                        eMail_391IAU = PrepararEmailParaGuardar(emailCelda),
                        Idioma_391IAU = dr["Idioma"]?.ToString().Trim(),
                        Activo_391IAU = Convert.ToBoolean(dr["Activo"]),
                        Bloqueado_391IAU = Convert.ToBoolean(dr["Bloqueado"]),
                        Intentos_391IAU = Convert.ToInt32(dr["Intentos"]),
                        IDRol_391IAU = dr["IDRol"] != DBNull.Value
                            ? Convert.ToInt32(dr["IDRol"])
                            : (int?)null
                    };

                    bool ok = bll.ModificarUsuario(u);

                    if (ok)
                        cambiosAplicados++;
                    else
                    {
                        fallosBD++;
                        try
                        {
                            int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                            new BLLBitacoraEventos().RegistrarEvento(
                                dniAdmin, 1, "GestionUsuarios",
                                $"Error BD al aplicar cambios sobre usuario DNI {dniObjetivo} ({nombre} {apellido}).");
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    excepciones++;
                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin, 1, "GestionUsuarios",
                            $"Excepción al aplicar cambios sobre usuario DNI {dniObjetivo} ({nombre} {apellido}). Detalle: {ex.Message}");
                    }
                    catch { }

                    MessageBox.Show(
                        sm.Traducir("GestionUsuarios_Aplicar_ErrorUsuario") + dniObjetivo + ": " + ex.Message);
                }
            }

            if (cambiosAplicados > 0)
            {
                dtUsuarios.AcceptChanges();

                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Aplicar_OK"),
                    sm.Traducir("GestionUsuarios_Titulo_OKOK"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                try
                {
                    int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                    string nombreAdmin = sm.UsuarioActual != null
                        ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                        : "Usuario";

                    new BLLBitacoraEventos().RegistrarEvento(
                        dniAdmin, 1, "GestionUsuarios",
                        $"El usuario {nombreAdmin} aplicó cambios masivos. Exitos={cambiosAplicados}, FallosBD={fallosBD}, Excepciones={excepciones}.");
                }
                catch { }

                CargarUsuarios();
            }
            else
            {
                if (fallosBD > 0 || excepciones > 0)
                {
                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin, 1, "GestionUsuarios",
                            $"Aplicar cambios masivos sin éxitos. Exitos=0, FallosBD={fallosBD}, Excepciones={excepciones}.");
                    }
                    catch { }
                }

                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Aplicar_SinCambios"),
                    sm.Traducir("GestionUsuarios_Titulo_Advertencia"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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

            if (!string.IsNullOrWhiteSpace(TXTCorreo.Text) && !emailsDesencriptados)
            {
                DesencriptarEmails();
                emailsDesencriptados = true;
                BTNDesencriptar.Text = sm.Traducir("GestionUsuarios_Emails_Boton_Encriptar");
            }

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
                MessageBox.Show(sm.Traducir("GestionUsuarios_Buscar_SinResultados"));

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

        private string PrepararEmailParaGuardar(string emailCelda)
        {
            if (emailsDesencriptados)
                return BLLUsuario.EncriptarAES(emailCelda);
            return emailCelda;
        }

        private void BTNEliminar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Eliminar_SinSeleccion"),
                    sm.Traducir("GestionUsuarios_Titulo_Advertencia"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];
            int dni = Convert.ToInt32(fila.Cells["DNI"].Value);
            string nombre = fila.Cells["Nombre"].Value?.ToString().Trim() ?? "-";
            string apellido = fila.Cells["Apellido"].Value?.ToString().Trim() ?? "-";

            DialogResult resp = MessageBox.Show(
                sm.Traducir("GestionUsuarios_Eliminar_Pregunta"),
                sm.Traducir("GestionUsuarios_Eliminar_Titulo"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp != DialogResult.Yes) return;

            string emailCelda = fila.Cells["Email"].Value?.ToString().Trim();

            BEUsuario u = new BEUsuario
            {
                DNI_391IAU = dni,
                Nombre_391IAU = nombre,
                Apellido_391IAU = apellido,
                eMail_391IAU = PrepararEmailParaGuardar(emailCelda),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                Activo_391IAU = false,
                Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                IDRol_391IAU = fila.Cells["IDRol"].Value != DBNull.Value ? Convert.ToInt32(fila.Cells["IDRol"].Value) : (int?)null
            };

            try
            {
                BLLUsuario bll = new BLLUsuario();
                bool ok = bll.ModificarUsuario(u);

                if (ok)
                {
                    MessageBox.Show(
                        sm.Traducir("GestionUsuarios_Eliminar_OK"),
                        sm.Traducir("GestionUsuarios_Titulo_OK"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    try
                    {
                        int dniAdmin = sm.UsuarioActual?.DNI_391IAU ?? 0;
                        string nombreAdmin = sm.UsuarioActual != null
                            ? sm.UsuarioActual.Nombre_391IAU + " " + sm.UsuarioActual.Apellido_391IAU
                            : "Usuario";

                        new BLLBitacoraEventos().RegistrarEvento(
                            dniAdmin,
                            1,
                            "GestionUsuarios",
                            $"El usuario {nombreAdmin} eliminó (desactivó) al usuario DNI {dni} ({nombre} {apellido}).");
                    }
                    catch { }

                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show(
                        sm.Traducir("GestionUsuarios_Modificar_Fallo"),
                        sm.Traducir("GestionUsuarios_Titulo_Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    sm.Traducir("GestionUsuarios_Modificar_Error") + " " + ex.Message,
                    sm.Traducir("GestionUsuarios_Titulo_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
