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
                BLLUsuario bll = new BLLUsuario();
                string contraseñaGenerada = TXTDNI.Text + TXTNombre.Text;

                bool resultado = bll.CrearUsuario(
                    TXTDNI.Text,
                    TXTNombre.Text,
                    TXTApellido.Text,
                    TXTeMail.Text,
                    contraseñaGenerada
                );

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
