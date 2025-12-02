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
        public static PantallaPrincipal Instancia { get; private set; }

        public PantallaPrincipal()
        {
            InitializeComponent();
            Instancia = this;
            ResetMenuStripLogout();
        }

        public static void ResetMenuStripLogout()
        {
            if (Instancia == null) return;

            Instancia.administradorToolStripMenuItem.Enabled = false;
            Instancia.maestrosToolStripMenuItem.Enabled = false;
            Instancia.vendedorToolStripMenuItem.Enabled = false;
            Instancia.ayudaToolStripMenuItem.Enabled = true;

            Instancia.logoutToolStripMenuItem.Enabled = false;
            Instancia.cambiarContraseñaToolStripMenuItem.Enabled = false;

            Instancia.loginToolStripMenuItem.Enabled = true;
        }

        private void PantallaPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.MdiParent = this;
            login.StartPosition = FormStartPosition.CenterScreen;
            login.Show();
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
                    ResetMenuStripLogout();
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
            cambiarContraseña.MdiParent = this;
            cambiarContraseña.Show();
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

        private void administradorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void maestrosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestionDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionDeClientes gestionDeClientes = new GestionDeClientes();
            gestionDeClientes.ShowDialog();
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void ActivarMenuPostLogin()
        {
            var bll = new BLLUsuario();

            administradorToolStripMenuItem.Enabled =
                bll.UsuarioTienePermiso("ADMINISTRAR_USUARIOS") ||
                bll.UsuarioTienePermiso("ADMINISTRAR_SISTEMA");

            gestionDeUsuariosToolStripMenuItem.Enabled =
                bll.UsuarioTienePermiso("GESTION_USUARIOS");

            maestrosToolStripMenuItem.Enabled =
                bll.UsuarioTienePermiso("GESTION_EVENTOS") ||
                bll.UsuarioTienePermiso("GESTION_CLIENTES");

            agregarConciertosToolStripMenuItem.Enabled =
                bll.UsuarioTienePermiso("GESTION_EVENTOS");

            crearClientesToolStripMenuItem.Enabled =
                bll.UsuarioTienePermiso("GESTION_CLIENTES");
            gestionDeClientesToolStripMenuItem.Enabled =
                bll.UsuarioTienePermiso("GESTION_CLIENTES");

            vendedorToolStripMenuItem.Enabled =
                bll.UsuarioTienePermiso("VENTA_BOLETOS");

            venderBoletoToolStripMenuItem.Enabled =
                bll.UsuarioTienePermiso("VENTA_BOLETOS");

            cambiarContraseñaToolStripMenuItem.Enabled = true;
            ayudaToolStripMenuItem.Enabled = true;

            logoutToolStripMenuItem.Enabled = true;
            loginToolStripMenuItem.Enabled = false;
        }

    }
}
