using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _686DP_SERVICIOS;
using _686DP_BLL;
using _686DP_SERVICIOS.Singleton;
using _686DP_SERVICIOS.Observer;
using System.Net.NetworkInformation;
using _686DP_SERVICIOS.Composite;
using System.Net;
using System.IO;


namespace AseguraYa
{
    public partial class _686DPfrmLogIn : Form
    {
        private bool MostrarPassword;
        private int Intentos;
        _686DP_BLLUsuario _686DP_BLLUsuario;
        _686DPCriptoManager _686DPCriptoManager;
        _686DP_ExpresionesRegulares _686DP_ExpresionesRegulares;
        _686DP_BLLPerfil bllp = new _686DP_BLLPerfil();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        
        string idi = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLDigitoVerificador BLLDV = new _686DP_BLLDigitoVerificador();
        public _686DPfrmLogIn(string idiomaLocal)
        {
            InitializeComponent();
            idi = idiomaLocal;
            _686DP_BLLUsuario = new _686DP_BLLUsuario();
            _686DPCriptoManager = new _686DPCriptoManager();
            _686DP_ExpresionesRegulares = new _686DP_ExpresionesRegulares();
        }

        private void DP_BTNIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (_686DP_Singleton.Instancia._686DPIsLogged())
                {
                    MessageBox.Show(LMG.Traducir("SesionYaActiva"), LMG.Traducir("TituloSesionActiva"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int DNI = Convert.ToInt32(DP_TXTUsuario.Text);
                string Contraseña = DP_TXTContraseña.Text;
                string ContraseñaHash = _686DPCriptoManager._686DPGetSHA256(Contraseña);
                if (DP_TXTUsuario.Text != "" || DP_TXTContraseña.Text != "")
                {
                    string Usuario = _686DP_BLLUsuario._686DPTraerUsuario(DNI);
                    if(Usuario != "")
                    {   string Idioma = _686DP_BLLUsuario.TraerIdiomaUsuario(DNI);
                        cambiarIdioma(Idioma);
                        bool BaseIntegra = BLLDV.CalcularTodos();
                        if(BaseIntegra)
                        {
                            inisioSesion(DNI, Contraseña, ContraseñaHash, Usuario, Idioma);
                        }
                        else
                        {
                            _686DP_Usuario usuarioCompleto = _686DP_BLLUsuario._686DPGenerarUsuarioSingleton(Usuario, ContraseñaHash, DNI, Idioma);
                            _686DP_Perfil perfilUsuario = bllp.TraerPerfilDelUsuario(usuarioCompleto._686DPDNI);
                            string ContraseñaBD = _686DP_BLLUsuario._686DPTraerContraseña(DNI);
                            if (ContraseñaHash == ContraseñaBD)
                            {
                                _686DP_Singleton.Instancia._686DPLogIN(usuarioCompleto);
                                MessageBox.Show(LMG.Traducir("SesionIniciada"));
                                _686DP_BLLUsuario._686DPReestablecerIntentos(DNI);
                                blle.RegistrarEvento(DNI, this.Name, "Sesion iniciada correctamente", 1);

                                if (perfilUsuario.Nombre == "Administrador General")
                                {
                                    _686DPfrmRepararSistema rs = new _686DPfrmRepararSistema();
                                    rs.Show();
                                }
                                else
                                {
                                    MessageBox.Show(LMG.Traducir("ErrorDB"));
                                }

                                this.Close();
                            }
                            else
                            {
                                _686DP_BLLUsuario.RegistrarError(DNI);
                                int intentos = _686DP_BLLUsuario._686DPTraerIntentos(DNI);
                                MessageBox.Show(string.Format(LMG.Traducir("ErrorIntentosRestantes"), 3 - intentos), LMG.Traducir("TituloErrorLogin"));
                                blle.RegistrarEvento(DNI, this.Name, "Contraseña incorrecta intentos restantes" + (3 - intentos).ToString(), 1);
                                if (intentos >= 3)
                                {
                                    _686DP_BLLUsuario._686DPBloquearUsuario(DNI);
                                    MessageBox.Show(LMG.Traducir("UsuarioBloqueado"), LMG.Traducir("TituloErrorLogin"));
                                    blle.RegistrarEvento(DNI, this.Name, "Usuario bloqueado", 1);
                                }
                                return;
                            }
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show(LMG.Traducir("UsuarioIncorrecto"), LMG.Traducir("TituloErrorLogin"));
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("FaltanDatos"), LMG.Traducir("TituloErrorLogin"));
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorInesperado") + ":\n" + ex.Message);
            }
        }

        private void inisioSesion(int DNI, string Contraseña, string ContraseñaHash, string Usuario, string Idioma)
        {
            bool Activo = _686DP_BLLUsuario._686DPTraerEstado(DNI);
            if (Activo)
            {
                bool Bloqueado = _686DP_BLLUsuario._686DPCuentaBloqueada(DNI);
                if (!Bloqueado)
                {
                    string ContraseñaBD = _686DP_BLLUsuario._686DPTraerContraseña(DNI);
                    if (ContraseñaHash == ContraseñaBD)
                    {
                        _686DP_Usuario usuarioCompleto = _686DP_BLLUsuario._686DPGenerarUsuarioSingleton(Usuario, ContraseñaHash, DNI, Idioma);
                        _686DP_Perfil perfilUsuario = bllp.TraerPerfilDelUsuario(usuarioCompleto._686DPDNI);
                        List<_686DP_Composite> componentes = perfilUsuario.ObtenerPermisos();
                        _686DP_Singleton.Instancia._686DPLogIN(usuarioCompleto);
                        MessageBox.Show(LMG.Traducir("SesionIniciada"));
                        _686DP_BLLUsuario._686DPReestablecerIntentos(DNI);
                        blle.RegistrarEvento(DNI, this.Name, "Sesion iniciada correctamente", 1);

                        foreach (var comp in componentes)
                        {
                            ActivarPermisos(comp);
                        }

                        bool cambiarContraseña = _686DP_BLLUsuario._686DPCambiarContraseña(DNI);
                        if (cambiarContraseña)
                        {
                            _686DPfrmCambiarContraseña cambiarcontra = new _686DPfrmCambiarContraseña(idi);
                            cambiarcontra.Show();
                            (this.MdiParent as _686DPfrmInicio)?._686DP_Desactivar();
                            this.Close();
                        }

                        this.Close();
                    }
                    else
                    {

                        _686DP_BLLUsuario.RegistrarError(DNI);
                        int intentos = _686DP_BLLUsuario._686DPTraerIntentos(DNI);
                        MessageBox.Show(string.Format(LMG.Traducir("ErrorIntentosRestantes"), 3 - intentos), LMG.Traducir("TituloErrorLogin"));
                        blle.RegistrarEvento(DNI, this.Name, "Contraseña incorrecta intentos restantes" + (3 - intentos).ToString(), 1);
                        if (intentos >= 3)
                        {
                            _686DP_BLLUsuario._686DPBloquearUsuario(DNI);
                            MessageBox.Show(LMG.Traducir("UsuarioBloqueado"), LMG.Traducir("TituloErrorLogin"));
                            blle.RegistrarEvento(DNI, this.Name, "Usuario bloqueado", 1);
                        }
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("CuentaBloqueada"));
                    blle.RegistrarEvento(DNI, this.Name, "Intento de inicio de sesion en una cuenta bloqueada", 1);
                }
            }
            else
            {
                MessageBox.Show(LMG.Traducir("UsuarioDesactivadoALERTA"));
                blle.RegistrarEvento(DNI, this.Name, "intento de inicio de sesion de una cuenta desactivada", 1);
                return;
            }
        }

        void ActivarPermisos(_686DP_Composite comp)
        {
            if (comp is _686DP_PermisoSimple permiso)
            {
                (this.MdiParent as _686DPfrmInicio)?.ActivarControlPorNombre(permiso.Nombre);
            }
            else if (comp is _686DP_Familia familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                {
                    (this.MdiParent as _686DPfrmInicio)?.ActivarControlPorNombre(comp.Nombre);
                    ActivarPermisos(hijo);
                }
            }
        }

        private void DP_BTNIniciarSesion_MouseHover(object sender, EventArgs e)
        {
            DP_BTNIniciarSesion.BackColor = Color.White;
            DP_BTNIniciarSesion.ForeColor = Color.Black;
        }

        private void DP_BTNIniciarSesion_MouseLeave(object sender, EventArgs e)
        {
            DP_BTNIniciarSesion.BackColor = Color.DarkSlateGray;
            DP_BTNIniciarSesion.ForeColor= Color.White;
        }

        private void _686DPfrmLogIn_Load(object sender, EventArgs e)
        {
            DP_TXTUsuario.BorderStyle = BorderStyle.None;
            DP_TXTContraseña.BorderStyle = BorderStyle.None;
            DP_TXTContraseña.Text = "46198686.Perelmuter";
            DP_TXTUsuario.Text = "46198686";

            cambiarIdioma(idi);
        }

        private void cambiarIdioma(string idioma)
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idioma);
            (this.MdiParent as _686DPfrmInicio).IdiomaLocal = idioma;
            (this.MdiParent as _686DPfrmInicio).CambiarIdioma(idioma);
            LMG.CargarMensajesGlobales(idioma);

        }

    

        private void DP_TXTUsuario_TextChanged(object sender, EventArgs e)
        {
            if (DP_TXTUsuario.Text != "")
            {
                try
                {

                    if (!_686DP_ExpresionesRegulares._686DPEsNumero(DP_TXTUsuario.Text))
                    {
                        MessageBox.Show(LMG.Traducir("SoloNumeros"));
                        DP_TXTUsuario.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorValidacion") + ": " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarPassword = !MostrarPassword;
            DP_TXTContraseña.PasswordChar = MostrarPassword ? '\0' : '*';
        }

        private void DP_TXTContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (idi == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "IniciarSesion.html");

                    if (File.Exists(ruta))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = ruta,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el archivo de ayuda en: " + ruta,
                                        "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al intentar abrir la ayuda: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (idi == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "IniciarSesionIngles.html");

                    if (File.Exists(ruta))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = ruta,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el archivo de ayuda en: " + ruta,
                                        "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al intentar abrir la ayuda: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
