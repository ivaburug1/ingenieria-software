using _686DP_BLL;
using _686DP_SERVICIOS.Observer;
using _686DP_SERVICIOS.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AseguraYa
{
    public partial class GestionDeRespaldo : Form
    {
        _686DP_BLLBackUpRestore _686DP_BLLBackUpRestore;
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        public GestionDeRespaldo(string idiomaLocal)
        {
            LMG.CargarMensajesGlobales(idi);
            InitializeComponent();
            _686DP_BLLBackUpRestore = new _686DP_BLLBackUpRestore();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            { ElegirRuta();
                blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se realizo un backup de la base de datos", 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ElegirRuta()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivos de Backup (*.bak)|*.bak";
                saveFileDialog.Title = "Guardar archivo de Backup";
                saveFileDialog.DefaultExt = "bak";

                saveFileDialog.FileName = "BCK_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = saveFileDialog.FileName;

                    try
                    {
                        _686DP_BLLBackUpRestore.RealizarBackupBD(rutaArchivo);
                        MessageBox.Show(LMG.Traducir("BackUPOK") + rutaArchivo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LMG.Traducir("ErrorBackUP") + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("NOSeleccion"));
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            { Restaurar();
                MessageBox.Show(LMG.Traducir("RestoreOK"));
                blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "Se restauró la base de datos", 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Restaurar()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de Backup (*.bak)|*.bak";
                openFileDialog.Title = "Seleccionar archivo de backup";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivoBackup = openFileDialog.FileName; 
                    Console.WriteLine(LMG.Traducir("RutaSeleccionada") + rutaArchivoBackup);

                    _686DP_BLLBackUpRestore.RealizarRestoreBD(rutaArchivoBackup);
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("NOSeleccion"));
                }
            }
        }

        private void GestionDeRespaldo_Load(object sender, EventArgs e)
        {

        }
    }
}
