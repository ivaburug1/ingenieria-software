using BE_391IAU;
using BLL_391IAU;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StageLink
{
    public partial class CrearClientes : Form
    {
        public CrearClientes()
        {
            InitializeComponent();
        }

        private void BTNCrearCliente_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = TXTDNICliente.Text.Trim();
                string nombre = TXTNombreCliente.Text.Trim();
                string apellido = TXTApellidoCliente.Text.Trim();
                string correo = TXTCorreoCliente.Text.Trim();

                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                {
                    MessageBox.Show("El DNI debe tener exactamente 8 dígitos numéricos. En caso de tener 7, agregar un 0 al principio de este.");
                    return;
                }

                if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    MessageBox.Show("El nombre solo puede contener letras.");
                    return;
                }

                if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    MessageBox.Show("El apellido solo puede contener letras.");
                    return;
                }

                if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("El e-Mail no tiene un formato válido.");
                    return;
                }

                int dniInt = int.Parse(dni);
                BECliente cliente = new BECliente(dniInt, nombre, apellido, correo);
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

        private void LBLTitulo_Click(object sender, EventArgs e)
        {
        }
    }
}
