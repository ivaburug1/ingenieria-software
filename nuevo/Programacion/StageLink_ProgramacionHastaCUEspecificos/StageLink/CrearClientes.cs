using BE_391IAU;
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
    public partial class CrearClientes : Form
    {
        public CrearClientes()
        {
            InitializeComponent();
        }

        private void LBLTitulo_Click(object sender, EventArgs e)
        {

        }

        private void BTNCrearCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TXTDNICliente.Text) ||
                    string.IsNullOrWhiteSpace(TXTNombreCliente.Text) ||
                    string.IsNullOrWhiteSpace(TXTApellidoCliente.Text) ||
                    string.IsNullOrWhiteSpace(TXTCorreoCliente.Text))
                {
                    MessageBox.Show("Todos los campos deben estar completos.");
                    return;
                }

                if (!int.TryParse(TXTDNICliente.Text.Trim(), out int dni))
                {
                    MessageBox.Show("El DNI debe ser un número válido.");
                    return;
                }

                string nombre = TXTNombreCliente.Text.Trim();
                string apellido = TXTApellidoCliente.Text.Trim();
                string correo = TXTCorreoCliente.Text.Trim();

                if (!correo.Contains("@"))
                {
                    MessageBox.Show("El mail debe tener un formato válido.");
                    return;
                }

                BECliente cliente = new BECliente(dni, nombre, apellido, correo);
                BLLCliente bll = new BLLCliente();

                if (bll.InsertarCliente(cliente))
                {
                    MessageBox.Show("Cliente creado correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hubo un error al crear el cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CrearClientes_Load(object sender, EventArgs e)
        {

        }
    }
}
