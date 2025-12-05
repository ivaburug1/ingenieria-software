using BLL_391IAU;
using SessionManager_391IAU;
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
    public partial class VenderBoleto : Form, IObserver_391IAU
    {
        public VenderBoleto()
        {
            InitializeComponent();
        }
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        private void VenderBoleto_Load(object sender, EventArgs e)
        {
            TXTDNICliente.Text = "46198686"; 
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);

        }
        private void BTNSeleccionarEvento_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            string dniTexto = TXTDNICliente.Text.Trim();

            if (string.IsNullOrWhiteSpace(dniTexto))
            {
                MessageBox.Show(
                    sm.Traducir("VenderBoleto_DNIRequerido"),
                    sm.Traducir("VenderBoleto_ErrorValidacion"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!int.TryParse(dniTexto, out int dni))
            {
                MessageBox.Show(
                    sm.Traducir("VenderBoleto_DNIInvalido"),
                    sm.Traducir("VenderBoleto_ErrorValidacion"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            bool existe = BLLCliente.ExisteClientePorDNI(dni);

            if (!existe)
            {
                DialogResult respuesta = MessageBox.Show(
                    sm.Traducir("VenderBoleto_ClienteNoExistePregunta"),
                    sm.Traducir("VenderBoleto_ClienteNoExisteTitulo"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    CrearClientes crearClientes = new CrearClientes();
                    crearClientes.ShowDialog();
                }
                return;
            }

            this.Close();
            SeleccionarEvento seleccionarEvento = new SeleccionarEvento(dni);
            seleccionarEvento.ShowDialog();
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
