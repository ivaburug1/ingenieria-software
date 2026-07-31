using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_391IAU;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class GestorRespaldo : Form
    {
        private readonly BLLGestionRespaldo _bll = new BLLGestionRespaldo();

        public GestorRespaldo()
        {
            InitializeComponent();
        }

        private void GestorRespaldo_Load(object sender, EventArgs e)
        {
        }

        private async void BTNBackUP_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;
            BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();

            int dni = 0;
            try
            {
                if (sm.UsuarioActual != null)
                    dni = sm.UsuarioActual.DNI_391IAU;
            }
            catch { }

            try
            {
                const string carpetaStageLink = @"C:\StageLink";
                string dirInicial = Directory.Exists(carpetaStageLink)
                    ? carpetaStageLink
                    : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string fileName = $"StageLink_{DateTime.Now:dd-MM-yyyy_HH-mm}.bak";

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Title = "Guardar Backup";
                    sfd.Filter = "Backup SQL Server (*.bak)|*.bak";
                    sfd.InitialDirectory = dirInicial;
                    sfd.FileName = fileName;
                    sfd.OverwritePrompt = true;

                    if (sfd.ShowDialog() != DialogResult.OK)
                    {
                        try { bllBitacora.RegistrarEvento(dni, 2, "Gestión de Respaldo", "Se canceló la generación de BACKUP (no se seleccionó ubicación)."); } catch { }
                        return;
                    }

                    ToggleUI(false);

                    await Task.Run(() => _bll.GenerarBackup(sfd.FileName));

                    try { bllBitacora.RegistrarEvento(dni, 2, "Gestión de Respaldo", $"Se generó BACKUP correctamente. Archivo: {sfd.FileName}"); } catch { }

                    MessageBox.Show(
                        "Backup generado correctamente.",
                        "Respaldo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                try { bllBitacora.RegistrarEvento(dni, 3, "Gestión de Respaldo", $"Error al generar BACKUP. Detalle: {ex.Message}"); } catch { }

                // 1) Mostrar el error
                MessageBox.Show(
                    "No se pudo generar el backup:\n\n" + ex.Message +
                    "\n\nEl archivo lo escribe el servicio de SQL Server con sus propios permisos, " +
                    "no los del usuario de Windows. Guardá en una carpeta accesible, como C:\\StageLink.",
                    "Error de Respaldo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // 2) Ofrecer crear C:\StageLink por separado
                const string carpetaStageLink = @"C:\StageLink";
                if (!Directory.Exists(carpetaStageLink))
                {
                    var resp = MessageBox.Show(
                        "¿Querés crear la carpeta C:\\StageLink?\n" +
                        "SQL Server suele tener permisos de escritura en esa ubicación.",
                        "Crear carpeta de backups",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (resp == DialogResult.Yes)
                    {
                        try
                        {
                            Directory.CreateDirectory(carpetaStageLink);
                            MessageBox.Show(
                                "Carpeta C:\\StageLink creada correctamente.\nUsala como destino en el próximo backup.",
                                "Carpeta creada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                        catch (Exception exDir)
                        {
                            MessageBox.Show(
                                "No se pudo crear la carpeta:\n" + exDir.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
            finally
            {
                ToggleUI(true);
            }
        }

        private async void BTNRestore_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;
            BLLBitacoraEventos bllBitacora = new BLLBitacoraEventos();

            int dni = 0;
            try
            {
                if (sm.UsuarioActual != null)
                    dni = sm.UsuarioActual.DNI_391IAU;
            }
            catch { }

            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Seleccionar Backup para Restaurar";
                    ofd.Filter = "Backup SQL Server (*.bak)|*.bak";
                    ofd.Multiselect = false;

                    if (ofd.ShowDialog() != DialogResult.OK)
                    {
                        try
                        {
                            bllBitacora.RegistrarEvento(
                                dni,
                                2,
                                "Gestión de Respaldo",
                                "Se canceló la restauración (no se seleccionó archivo .bak)."
                            );
                        }
                        catch { }

                        return;
                    }

                    var confirm = MessageBox.Show(
                        "Restaurar un backup reemplazará la base actual.\n¿Deseás continuar?",
                        "Confirmación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirm != DialogResult.Yes)
                    {
                        try
                        {
                            bllBitacora.RegistrarEvento(
                                dni,
                                2,
                                "Gestión de Respaldo",
                                $"Se canceló la restauración por confirmación del usuario. Archivo: {ofd.FileName}"
                            );
                        }
                        catch { }

                        return;
                    }

                    ToggleUI(false);

                    await Task.Run(() => _bll.RestaurarBackup(ofd.FileName));

                    try
                    {
                        bllBitacora.RegistrarEvento(
                            dni,
                            2,
                            "Gestión de Respaldo",
                            $"Se restauró la base correctamente desde el BACKUP. Archivo: {ofd.FileName}"
                        );
                    }
                    catch { }

                    MessageBox.Show(
                        "Base restaurada correctamente.",
                        "Respaldo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                try
                {
                    bllBitacora.RegistrarEvento(
                        dni,
                        3,
                        "Gestión de Respaldo",
                        $"Error al restaurar BACKUP. Detalle: {ex.Message}"
                    );
                }
                catch { }

                MessageBox.Show(
                    "Error al restaurar backup:\n" + ex.Message,
                    "Respaldo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                ToggleUI(true);
            }
        }

        private void ToggleUI(bool enabled)
        {
            BTNBackUP.Enabled = enabled;
            BTNRestore.Enabled = enabled;
        }
    }
}