using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AseguraYa
{
    public partial class frmInputBox : Form
    {
        public string Resultado { get; private set; }
        public frmInputBox(string mensaje, string titulo = "Ingresar valor")
        {
            InitializeComponent();
            this.Text = titulo;
            label1.Text = mensaje;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Resultado = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmInputBox_Load(object sender, EventArgs e)
        {

        }
    }
}
