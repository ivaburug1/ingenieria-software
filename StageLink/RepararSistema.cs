using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_391IAU;
using SessionManager_391IAU;

namespace StageLink
{
    public partial class RepararSistema : Form
    {
        private readonly BLLDigitoVerificador _bllDV = new BLLDigitoVerificador();
        private readonly BLLGestionRespaldo _bllRespaldo = new BLLGestionRespaldo();

        private readonly List<string> _fallosDV;

        public RepararSistema()
        {
            InitializeComponent();
            _fallosDV = new List<string>();
        }

        public RepararSistema(List<string> fallosDV)
        {
            InitializeComponent();
            _fallosDV = fallosDV ?? new List<string>();
        }

        private void RepararSistema_Load(object sender, EventArgs e)
        {
            CargarFallosEnListBox(_fallosDV);
        }

        private void CargarFallosEnListBox(List<string> fallos)
        {
            LBListaDVForms.Items.Clear();

            if (fallos == null || fallos.Count == 0)
            {
                LBListaDVForms.Items.Add("No se recibió detalle de fallos DV.");
                return;
            }

            foreach (var f in fallos.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                LBListaDVForms.Items.Add(f.Trim());
            }
        }

        private void BTNRecalcularDV_Click(object sender, EventArgs e)
        {
            try
            {
                _bllDV.RecalcularTodas();

                var estado = new List<string>();

                try
                {
                    _bllDV.ValidarIntegridadDVOrThrow();
                    estado.Add("✅ Sin fallos DV detectados luego de recalcular.");
                }
                catch (Exception ex)
                {
                    estado.Add("⚠️ Se recalculó, pero la validación todavía falla:");
                    estado.AddRange(SepararMensaje(ex.Message));
                    if (ex.InnerException != null)
                        estado.AddRange(SepararMensaje(ex.InnerException.Message));
                }

                CargarFallosEnListBox(estado);

                MessageBox.Show(
                    "Se recalcularon correctamente los Dígitos Verificadores (DVH/DVV) de las 12 tablas.",
                    "Reparación completada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al recalcular Dígitos Verificadores: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            this.Close();
        }

        private async void BTNRestaurarBD_Click(object sender, EventArgs e)
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
                                "Reparar Sistema",
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
                                "Reparar Sistema",
                                $"Se canceló la restauración por confirmación del usuario. Archivo: {ofd.FileName}"
                            );
                        }
                        catch { }

                        return;
                    }

                    ToggleUI(false);

                    await Task.Run(() => _bllRespaldo.RestaurarBackup(ofd.FileName));

                    try
                    {
                        bllBitacora.RegistrarEvento(
                            dni,
                            2,
                            "Reparar Sistema",
                            $"Se restauró la base correctamente desde el BACKUP. Archivo: {ofd.FileName}"
                        );
                    }
                    catch { }

                    MessageBox.Show(
                        "Base restaurada correctamente.",
                        "Reparar Sistema",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    try
                    {
                        _bllDV.ValidarIntegridadDVOrThrow();
                        CargarFallosEnListBox(new List<string> { "✅ Sin fallos DV detectados luego de restaurar el backup." });
                    }
                    catch (Exception exVal)
                    {
                        var estado = new List<string>
                        {
                            "⚠️ Luego de restaurar, la validación DV todavía falla:"
                        };
                        estado.AddRange(SepararMensaje(exVal.Message));
                        if (exVal.InnerException != null)
                            estado.AddRange(SepararMensaje(exVal.InnerException.Message));

                        CargarFallosEnListBox(estado);
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    bllBitacora.RegistrarEvento(
                        dni,
                        3,
                        "Reparar Sistema",
                        $"Error al restaurar BACKUP. Detalle: {ex.Message}"
                    );
                }
                catch { }

                MessageBox.Show(
                    "Error al restaurar backup:\n" + ex.Message,
                    "Reparar Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                ToggleUI(true);
            }

            this.Close();
        }

        private List<string> SepararMensaje(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
                return new List<string>();

            return msg.Split(new[] { "\r\n", "\n", ";", "|" }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(x => x.Trim())
                      .Where(x => x.Length > 0)
                      .ToList();
        }

        private void BTNDetectarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                var cambios = _bllDV.DetectarCambios();
                var lineas = new List<string>();

                if (cambios.Count == 0)
                {
                    lineas.Add("Sin cambios detectados — integridad por fila OK.");
                }
                else
                {
                    foreach (var c in cambios)
                    {
                        string linea = $"[{c.TipoCambio}] {c.ClaveTabla}";
                        if (!string.IsNullOrEmpty(c.ClavePrimaria))
                            linea += $" — PK: {c.ClavePrimaria}";
                        if (c.TipoCambio == "Insercion" || c.TipoCambio == "Eliminacion")
                            linea += $" (esperadas: {c.FilasEsperadas}, actuales: {c.FilasActuales})";
                        lineas.Add(linea);
                    }
                }

                CargarFallosEnListBox(lineas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al detectar cambios: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ToggleUI(bool enabled)
        {
            BTNRecalcularDV.Enabled = enabled;
            BTNRestaurarBD.Enabled = enabled;
        }

        private void LBListaDVForms_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}