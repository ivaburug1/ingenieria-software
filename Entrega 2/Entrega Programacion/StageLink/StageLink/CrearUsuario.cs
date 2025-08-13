using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BLL_391IAU;

namespace StageLink
{
    public partial class CrearUsuario : Form
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        private void LBLExplicacionContraseña_Click(object sender, EventArgs e)
        {
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = TXTDNI.Text.Trim();
                string nombre = TXTNombre.Text.Trim();
                string apellido = TXTApellido.Text.Trim();
                string email = TXTeMail.Text.Trim();

                if (!Regex.IsMatch(dni, @"^\d{8}$"))
                    throw new ArgumentException("El DNI debe tener exactamente 8 dígitos numéricos. En caso de tener 7, agregar un 0 al principio de este.");

                if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                    throw new ArgumentException("El nombre solo puede contener letras.");

                if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                    throw new ArgumentException("El apellido solo puede contener letras.");

                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new ArgumentException("El e-Mail no tiene un formato válido.");

                BLLUsuario bll = new BLLUsuario();
                string contraseñaGenerada = dni + nombre;

                bool resultado = bll.CrearUsuario(dni, nombre, apellido, email, contraseñaGenerada);

                if (resultado)
                {
                    MessageBox.Show("Usuario creado correctamente.\nLa contraseña inicial es DNI + Nombre.", "Éxito");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo crear el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearUsuario_Load(object sender, EventArgs e)
        {
        }
    }
}
