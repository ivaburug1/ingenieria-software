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
using Servicios_391IAU.Composite;
using SessionManager_391IAU;

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

        public void ActivarControlPorNombre(string nombre)
        {
            Control[] controles = this.Controls.Find(nombre, true);
            foreach (Control c in controles)
            {
                c.Enabled = true;
                c.Visible = true;
            }

            foreach (var control in this.Controls)
            {
                if (control is MenuStrip menu)
                {
                    foreach (ToolStripItem item in menu.Items)
                        ActivarItemMenu(item, nombre);
                }
            }
        }

        private void ActivarItemMenu(ToolStripItem item, string nombre)
        {
            if (item.Name == nombre)
            {
                item.Enabled = true;
                item.Visible = true;
            }

            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                    ActivarItemMenu(subItem, nombre);
            }
        }

        private void ActivarPermisoUsuario(IComponentePermiso_391IAU comp)
        {
            if (comp is PermisoSimple_391IAU permiso)
            {
                ActivarControlPorNombre(permiso.Nombre);
            }
            else if (comp is Familia_391IAU familia)
            {
                ActivarControlPorNombre(familia.Nombre);

                foreach (var hijo in familia.ObtenerHijos())
                    ActivarPermisoUsuario(hijo);
            }
        }

        public void ActivarPermisosDeSesion()
        {
            var sesion = BLL_391IAU.SessionManager_391IAU.ObtenerInstancia();
            foreach (var comp in sesion.Permisos)
                ActivarPermisoUsuario(comp);

            cambiarContraseñaToolStripMenuItem.Enabled = true;
            logoutToolStripMenuItem.Enabled = true;
            loginToolStripMenuItem.Enabled = false;
        }


        public static void ResetMenuStripLogout()
        {
            if (Instancia == null) return;

            Instancia.administradorToolStripMenuItem.Enabled = false;
            Instancia.maestrosToolStripMenuItem.Enabled = false;
            Instancia.vendedorToolStripMenuItem.Enabled = false;
            Instancia.ayudaToolStripMenuItem.Enabled = true;

            Instancia.gestionDeUsuariosToolStripMenuItem.Enabled = false;
            Instancia.gestionDeUsuariosToolStripMenuItem1.Enabled = false;
            Instancia.agregarConciertosToolStripMenuItem.Enabled = false;
            Instancia.crearClientesToolStripMenuItem.Enabled = false;
            Instancia.gestionDeClientesToolStripMenuItem.Enabled = false;
            Instancia.venderBoletoToolStripMenuItem.Enabled = false;

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
            administradorToolStripMenuItem.Enabled = true;
            maestrosToolStripMenuItem.Enabled = true;
            vendedorToolStripMenuItem.Enabled = true;
            ayudaToolStripMenuItem.Enabled = true;

            gestionDeUsuariosToolStripMenuItem.Enabled = true;
            gestionDeUsuariosToolStripMenuItem1.Enabled = true;

            agregarConciertosToolStripMenuItem.Enabled = true;
            crearClientesToolStripMenuItem.Enabled = true;
            gestionDeClientesToolStripMenuItem.Enabled = true;

            venderBoletoToolStripMenuItem.Enabled = true;

            cambiarContraseñaToolStripMenuItem.Enabled = true;

            logoutToolStripMenuItem.Enabled = true;
            loginToolStripMenuItem.Enabled = false;
        }
    }
}
