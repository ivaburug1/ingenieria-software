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
    public partial class CambiarContraseña : Form
    {
        public CambiarContraseña()
        {
            InitializeComponent();
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNCambiarContraseña_Click_1(object sender, EventArgs e)
        {
            try
            {
                string actual = TXTContraseñaActual.Text;
                string nueva = TXTContraseñaNueva.Text;
                string confirmacion = TXTContraseñaConfirmacion.Text;

                if (string.IsNullOrWhiteSpace(actual) || string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmacion))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                if (nueva != confirmacion)
                {
                    MessageBox.Show("La nueva contraseña y su confirmación no coinciden.");
                    return;
                }

                BLLUsuario bll = new BLLUsuario();

                if (!bll.ValidarContraseñaActual(actual))
                {
                    MessageBox.Show("La contraseña actual no es correcta.");
                    return;
                }

                if (bll.ContraseñaYaFueUsada(nueva))
                {
                    MessageBox.Show("No puede reutilizar una contraseña anterior.");
                    return;
                }

                bll.CambiarContraseña(nueva);
                MessageBox.Show("Contraseña actualizada correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar la contraseña: " + ex.Message);
            }
        }

        private void CambiarContraseña_Load(object sender, EventArgs e)
        {

        }
    }
}