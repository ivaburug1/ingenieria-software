using BLL_391IAU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StageLink
{
    public partial class VenderBoleto : Form
    {
        public VenderBoleto()
        {
            InitializeComponent();
        }
        private void VenderBoleto_Load(object sender, EventArgs e)
        {
            TXTDNICliente.Text = "46198686";
        }

        private void BTNSeleccionarEvento_Click(object sender, EventArgs e)
        {
            string dniTexto = TXTDNICliente.Text.Trim();

            if (string.IsNullOrWhiteSpace(dniTexto))
            {
                MessageBox.Show("Debe ingresar un DNI para continuar.");
                return;
            }

            if (!int.TryParse(dniTexto, out int dni))
            {
                MessageBox.Show("El DNI ingresado no es válido.");
                return;
            }

            bool existe = BLLCliente.ExisteClientePorDNI(dni);

            if (!existe)
            {
                DialogResult respuesta = MessageBox.Show(
                    "El cliente seleccionado no tiene cuenta. ¿Desea crear uno nuevo?",
                    "Cliente no encontrado",
                    MessageBoxButtons.YesNo);

                if (respuesta == DialogResult.Yes)
                {
                    CrearClientes crearClientes = new CrearClientes();
                    crearClientes.ShowDialog();
                }

                return;
            }

            SeleccionarEvento seleccionarEvento = new SeleccionarEvento(dni);
            seleccionarEvento.ShowDialog();
        }


        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
