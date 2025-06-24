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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            TXTDNI.Text = "45679391";
        }

        private void TXTContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void BTNLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = TXTDNI.Text.Trim();
                string contrasenia = TXTContraseña.Text;

                BLLUsuario bll = new BLLUsuario();
                bool loginExitoso = bll.Login(dni, contrasenia);

                if (loginExitoso)
                {
                    string nombreCompleto = bll.ObtenerNombreUsuarioLogueado();
                    MessageBox.Show($"Bienvenido {nombreCompleto}", "Login exitoso");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo iniciar sesión.", "Error");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Credenciales inválidas. " + ex.Message, "Error de autenticación");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("No se puede iniciar sesión. " + ex.Message, "Sesión activa");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error");
            }
        }

        private void BTNMostrarContraseña_Click(object sender, EventArgs e)
        {
            TXTContraseña.UseSystemPasswordChar = !TXTContraseña.UseSystemPasswordChar;

            if (TXTContraseña.UseSystemPasswordChar)
            {
                BTNMostrarContraseña.Text = "Mostrar Contraseña";
            }
            else
            {
                BTNMostrarContraseña.Text = "Ocultar";
            }
        }
    }
}