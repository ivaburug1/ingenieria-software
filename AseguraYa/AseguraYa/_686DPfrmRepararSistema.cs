using _686DP_BLL;
using _686DP_SERVICIOS.Observer;
using _686DP_SERVICIOS.Singleton;
using Org.BouncyCastle.Pqc.Crypto.Lms;
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
    public partial class _686DPfrmRepararSistema : Form
    {
        private readonly _686DP_BLLBackUpRestore BLLBR = new _686DP_BLLBackUpRestore();
        private readonly _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        private readonly _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        

        public _686DPfrmRepararSistema()
        {
            InitializeComponent();
            this.FormClosing += _686DPfrmRepararSistema_FormClosing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Restaurar();
                MessageBox.Show(LMG.Traducir("RestoreOK"), "✔️ " + LMG.Traducir("Restauración"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorRestore") + " " + ex.Message, "❌ " + LMG.Traducir("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            logout();
            this.Close();
        }

        private void Restaurar()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de Backup (*.bak)|*.bak";
                openFileDialog.Title = LMG.Traducir("SeleccionarArchivoBackup");

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivoBackup = openFileDialog.FileName;
                    Console.WriteLine(LMG.Traducir("RutaSeleccionada") + ": " + rutaArchivoBackup);

                    BLLBR.RealizarRestoreBD(rutaArchivoBackup);
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("NOSeleccion"), "⚠️ " + LMG.Traducir("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                BLLDV.CalcularTodos();
                BLLDV.grabarTodosDV();
                logout();
                MessageBox.Show(LMG.Traducir("DVRecalculadoOK"), "✔️ " + LMG.Traducir("Integridad"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorRecalcular") + ": " + ex.Message, "❌ " + LMG.Traducir("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        private void logout()
        {
            _686DP_Singleton.Instancia._686DPLogOut();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hasta luego, ojala lo soluciones");
            logout();
            this.Close();
        }

        private void _686DPfrmRepararSistema_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = _686DP_BLLDigitoVerificador.errores;
            listBox2.DataSource = _686DP_BLLDigitoVerificador.MppErrores;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _686DPfrmRepararSistema_FormClosing(object sender, FormClosingEventArgs e)
        {
            logout();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
