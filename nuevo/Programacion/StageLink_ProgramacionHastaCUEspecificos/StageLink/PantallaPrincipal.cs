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
    public partial class PantallaPrincipal : Form
    {
        public PantallaPrincipal()
        {
            InitializeComponent();
        }

        private void PantallaPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que quiere cerrar la sesión?",
                "Confirmar logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    BLLUsuario bll = new BLLUsuario();
                    bll.Logout();

                    MessageBox.Show("Sesión cerrada correctamente.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cerrar la sesión: " + ex.Message);
                }
            }
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearUsuario crearUsuario = new CrearUsuario();
            crearUsuario.ShowDialog();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarContraseña cambiarContraseña = new CambiarContraseña();
            cambiarContraseña.ShowDialog();
        }

        private void gestionDeUsuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GestionDeUsuarios gestionDeUsuarios = new GestionDeUsuarios();
            gestionDeUsuarios.ShowDialog();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionDeUsuarios gestionDeUsuarios = new GestionDeUsuarios();
            gestionDeUsuarios.ShowDialog();
        }

        private void agregarConciertosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarEventos agregarEventos = new AgregarEventos();
            agregarEventos.ShowDialog();
        }

        private void crearClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearClientes crearClietnes = new CrearClientes();
            crearClietnes.ShowDialog();
        }

        private void venderBoletoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VenderBoleto venderBoleto = new VenderBoleto();
            venderBoleto.ShowDialog();
        }
    }
}
