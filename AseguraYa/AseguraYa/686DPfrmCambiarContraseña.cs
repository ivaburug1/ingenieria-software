using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_BLL;
using _686DP_SERVICIOS;
using _686DP_SERVICIOS.Observer;
using _686DP_SERVICIOS.Singleton;

namespace AseguraYa
{
    public partial class _686DPfrmCambiarContraseña : Form
    {
        _686DPCriptoManager _686DPCriptoManager;
        _686DP_BLLUsuario bll;
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        public _686DPfrmCambiarContraseña(string idiomaLocal)
        {
            InitializeComponent();
            _686DPCriptoManager = new _686DPCriptoManager();
            bll = new _686DP_BLLUsuario();
            idi = idiomaLocal;
            cambiarIdioma();
        }

        private void DP_BTNAplicar_MouseHover(object sender, EventArgs e)
        {
            DP_BTNAplicar.ForeColor = Color.Black;
            DP_BTNAplicar.BackColor = Color.White;
        }

        private void DP_BTNAplicar_MouseLeave(object sender, EventArgs e)
        {
            DP_BTNAplicar.ForeColor = Color.White;
            DP_BTNAplicar.BackColor = Color.DarkSlateGray;
        }

        private void _686DPfrmCambiarContraseña_Load(object sender, EventArgs e)
        {   LMG.CargarMensajesGlobales(idi);
            DP_TXTConfirmación.BorderStyle = BorderStyle.None;
            DP_TXTContraseñaActual.BorderStyle = BorderStyle.None;
            DP_TXTContraseñaNueva.BorderStyle = BorderStyle.None;

            cambiarIdioma();
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }

        private void _686DPfrmCambiarContraseña_FormClosing(object sender, FormClosingEventArgs e)
        {
            int DNI = _686DP_Singleton.Instancia.Usuario._686DPDNI;
            bool cambiarContraseña = bll._686DPCambiarContraseña(DNI);

            if (cambiarContraseña)
            {
                MessageBox.Show(LMG.Traducir("DebeCambiarContraseña"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void DP_BTNAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                string contraseñaActual = DP_TXTContraseñaActual.Text;
                string contraseñaNueva = DP_TXTContraseñaNueva.Text;
                string confirmacion = DP_TXTConfirmación.Text;
                int DNI = _686DP_Singleton.Instancia.Usuario._686DPDNI;

                string contraseñaActualHash = _686DPCriptoManager._686DPGetSHA256(contraseñaActual);
                string contraseñaBD = bll._686DPTraerContraseña(DNI);

                if (contraseñaActualHash != contraseñaBD)
                {
                    MessageBox.Show(LMG.Traducir("ContraseñaIncorrecta")  );
                    return;
                }

                if (contraseñaNueva != confirmacion)
                {
                    MessageBox.Show(LMG.Traducir("ContraseñasNoCoinciden")  );
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Contraseñas no coinciden", 1);
                    return;
                }

                string contraseñaNuevaHash = _686DPCriptoManager._686DPGetSHA256(contraseñaNueva);
                if(contraseñaNuevaHash == contraseñaActualHash)
                {
                    MessageBox.Show(LMG.Traducir("ContraseñaIgual")  );
                    return;
                }
                bll._686DPVerificarContraseñas(DNI);

                bool ok = bll._686DPCompararContraseñas(contraseñaNuevaHash, contraseñaBD, DNI);

                if (ok)
                {
                    bll._686DPNuevaContra(contraseñaNuevaHash, DNI);
                    bll._ReestablecerObligatoriedadeContraseña(DNI);
                    MessageBox.Show(LMG.Traducir("ContraseñaCambiadaOK"), LMG.Traducir("TituloExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_686DP_Singleton.Instancia._686DPIsLogged())
                    {
                        _686DP_Singleton.Instancia._686DPLogOut();
                        MessageBox.Show(LMG.Traducir("SesionCerrada"), LMG.Traducir("TituloCerrarSesion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Contraseña cambiada", 1);
                    this.Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(LMG.Traducir("FormatoContraseñaInvalido")  );
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorCambioContraseña") + ex.Message  );
            }

        }

        private void DP_TXTContraseñaActual_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
