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

namespace StageLink
{
    public partial class GestionDeUsuarios : Form
    {
        public GestionDeUsuarios()
        {
            InitializeComponent();

            DGVMuestraUsuarios.CellValueChanged += DGVMuestraUsuarios_CellValueChanged;
            DGVMuestraUsuarios.CurrentCellDirtyStateChanged += DGVMuestraUsuarios_CurrentCellDirtyStateChanged;

            CargarUsuarios();
        }
        private DataTable dtUsuarios;
        private bool hayCambios = false;
        private void GestionDeUsuarios_Load(object sender, EventArgs e)
        {

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

                dtUsuarios.PrimaryKey = new DataColumn[] { dtUsuarios.Columns["DNI"] };

                foreach (var u in lista)
                {
                    dtUsuarios.Rows.Add(
                        u.DNI_391IAU,
                        u.Nombre_391IAU,
                        u.Apellido_391IAU,
                        BLLUsuario.DesencriptarAES(u.eMail_391IAU),
                        u.Activo_391IAU,
                        u.Bloqueado_391IAU,
                        u.Intentos_391IAU,
                        u.Idioma_391IAU,
                        u.Contraseña_391IAU
                    );

                }
                DGVMuestraUsuarios.DataSource = dtUsuarios;
                DGVMuestraUsuarios.Columns["Contraseña"].Visible = false;
                hayCambios = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message);
            }
        }

        private void BTNModificar_Click(object sender, EventArgs e)
        {
            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un usuario para modificar.");
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];
            try
            {
                BEUsuario u = new BEUsuario
                {
                    DNI_391IAU = Convert.ToInt32(fila.Cells["DNI"].Value),
                    Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                    Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                    eMail_391IAU = BLLUsuario.EncriptarAES(fila.Cells["Email"].Value?.ToString().Trim()),
                    Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),

                    Activo_391IAU = Convert.ToBoolean(fila.Cells["Activo"].Value),
                    Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                    Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value)
                };

                BLLUsuario bll = new BLLUsuario();
                if (bll.ModificarUsuario(u))
                {
                    MessageBox.Show("Usuario modificado con éxito.");
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el usuario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar usuario: " + ex.Message);
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            if (hayCambios)
            {
                DialogResult result = MessageBox.Show(
                    "Esta seguro de cancelar? Los cambios no fueron guardados.\n\nPara guardar, presione el botón \"Aplicar\".",
                    "Confirmar cancelación",
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
            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un usuario.");
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];
            int dni = Convert.ToInt32(fila.Cells["DNI"].Value);
            bool estaActivo = Convert.ToBoolean(fila.Cells["Activo"].Value);

            if (estaActivo)
            {
                MessageBox.Show("El usuario se encuentra activo, si desea desactivarlo presione el botón \"Desactivar\".");
                return;
            }

            fila.Cells["Activo"].Value = true;

            BEUsuario u = new BEUsuario
            {
                DNI_391IAU = dni,
                Activo_391IAU = true,
                Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                eMail_391IAU = BLLUsuario.EncriptarAES(fila.Cells["Email"].Value?.ToString().Trim()),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim()
            };

            BLLUsuario bll = new BLLUsuario();
            if (bll.ModificarUsuario(u))
            {
                string nombre = fila.Cells["Nombre"].Value?.ToString();
                MessageBox.Show($"El usuario {nombre} fue activado con éxito.");
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("No se pudo activar el usuario.");
            }
        }

        private void BTNDesactivar_Click(object sender, EventArgs e)
        {
            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un usuario.");
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];
            int dni = Convert.ToInt32(fila.Cells["DNI"].Value);
            bool estaActivo = Convert.ToBoolean(fila.Cells["Activo"].Value);

            if (!estaActivo)
            {
                MessageBox.Show("El usuario se encuentra desactivado, si desea activarlo presione el botón \"Activar\".");
                return;
            }

            fila.Cells["Activo"].Value = false;

            BEUsuario u = new BEUsuario
            {
                DNI_391IAU = dni,
                Activo_391IAU = false,
                Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value),
                Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                eMail_391IAU = BLLUsuario.EncriptarAES(fila.Cells["Email"].Value?.ToString().Trim()),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim()
            };

            BLLUsuario bll = new BLLUsuario();
            if (bll.ModificarUsuario(u))
            {
                string nombre = fila.Cells["Nombre"].Value?.ToString();
                MessageBox.Show($"El usuario {nombre} fue desactivado con éxito.");
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("No se pudo desactivar el usuario.");
            }
        }

        private void BTNDesbloquear_Click(object sender, EventArgs e)
        {
            if (DGVMuestraUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un usuario.");
                return;
            }

            DataGridViewRow fila = DGVMuestraUsuarios.SelectedRows[0];
            int dni = Convert.ToInt32(fila.Cells["DNI"].Value);
            bool estaBloqueado = Convert.ToBoolean(fila.Cells["Bloqueado"].Value);

            string nombreCompleto = $"{fila.Cells["Nombre"].Value} {fila.Cells["Apellido"].Value}";

            if (!estaBloqueado)
            {
                MessageBox.Show($"El usuario {nombreCompleto} no se encuentra bloqueado.");
                return;
            }

            fila.Cells["Bloqueado"].Value = false;
            fila.Cells["Intentos"].Value = 0;

            BEUsuario u = new BEUsuario
            {
                DNI_391IAU = dni,
                Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                eMail_391IAU = BLLUsuario.EncriptarAES(fila.Cells["Email"].Value?.ToString().Trim()),
                Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                Activo_391IAU = Convert.ToBoolean(fila.Cells["Activo"].Value),
                Bloqueado_391IAU = false,
                Intentos_391IAU = 0
            };

            BLLUsuario bll = new BLLUsuario();
            if (bll.ModificarUsuario(u))
            {
                MessageBox.Show($"El usuario {nombreCompleto} fue desbloqueado con éxito.");
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("No se pudo desbloquear al usuario.");
            }
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            int cambiosAplicados = 0;

            BLLUsuario bll = new BLLUsuario();

            foreach (DataGridViewRow fila in DGVMuestraUsuarios.Rows)
            {
                if (fila.IsNewRow) continue;

                try
                {
                    BEUsuario u = new BEUsuario
                    {
                        DNI_391IAU = Convert.ToInt32(fila.Cells["DNI"].Value),
                        Nombre_391IAU = fila.Cells["Nombre"].Value?.ToString().Trim(),
                        Apellido_391IAU = fila.Cells["Apellido"].Value?.ToString().Trim(),
                        eMail_391IAU = BLLUsuario.EncriptarAES(fila.Cells["Email"].Value?.ToString().Trim()),
                        Idioma_391IAU = fila.Cells["Idioma"].Value?.ToString().Trim(),
                        Activo_391IAU = Convert.ToBoolean(fila.Cells["Activo"].Value),
                        Bloqueado_391IAU = Convert.ToBoolean(fila.Cells["Bloqueado"].Value),
                        Intentos_391IAU = Convert.ToInt32(fila.Cells["Intentos"].Value)
                    };

                    if (bll.ModificarUsuario(u))
                        cambiosAplicados++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar al usuario con DNI {fila.Cells["DNI"].Value}: {ex.Message}");
                }
            }

            if (cambiosAplicados > 0)
            {
                MessageBox.Show($"Se aplicaron los cambios a {cambiosAplicados} usuario(s).");
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("No se aplicaron cambios.");
            }
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            if (dtUsuarios == null)
                return;

            string filtro = "";

            if (CBActivoNoActivo.SelectedItem != null)
            {
                if (CBActivoNoActivo.SelectedItem.ToString() == "Activo")
                    filtro += "Activo = true";
                else if (CBActivoNoActivo.SelectedItem.ToString() == "No Activo")
                    filtro += "Activo = false";
            }

            if (CBUsuariosBloqueados.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(filtro)) filtro += " AND ";

                if (CBUsuariosBloqueados.SelectedItem.ToString() == "Bloqueado")
                    filtro += "Bloqueado = true";
                else if (CBUsuariosBloqueados.SelectedItem.ToString() == "Desbloqueado")
                    filtro += "Bloqueado = false";
            }

            DataView vistaFiltrada = new DataView(dtUsuarios);
            vistaFiltrada.RowFilter = filtro;

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
                MessageBox.Show("No se encontró ningún usuario en la búsqueda.");
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
    }
}
