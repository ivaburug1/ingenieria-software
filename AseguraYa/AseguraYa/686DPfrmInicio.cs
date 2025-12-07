using _686DP_SERVICIOS.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_SERVICIOS;
using System.Windows.Forms;
using _686DP_SERVICIOS.Observer;
using _686DP_BLL;
using _686DP_BE;
using System.IO;

namespace AseguraYa
{
    public partial class _686DPfrmInicio : Form
    {
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma idioma = new _686DP_Idioma();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        public string IdiomaLocal;
        _686DP_BLLDigitoVerificador DGV = new _686DP_BLLDigitoVerificador();
        public _686DPfrmInicio()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RecomendacionAumento.Visible= false;
            _686DP_Desactivar();
            RegistrarFormularioParaIdioma(this);
            if (IdiomaLocal != null)
            {
                CambiarIdioma(IdiomaLocal);
                LMG.CargarMensajesGlobales(IdiomaLocal);

            }
            else
            {
                CambiarIdioma("Español");
                LMG.CargarMensajesGlobales("Español");

            }

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
                    {
                        ActivarItemMenu(item, nombre);
                    }
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
                {
                    ActivarItemMenu(subItem, nombre);
                }
            }
        }

        public void RegistrarFormularioParaIdioma(Form form1)
        {
            if (!LMG.Forms.Contains(form1))
                LMG.RegistrarForm(form1);
        }

        public void _686DP_Desactivar()
        {
            RecomendacionAumento.Enabled = false;
            DP_Admin.Enabled = false;
            DP_Contratacion.Enabled = false;
            Siniestros.Enabled = false;
            DP_Maestro.Enabled = false;
            DP_CambiarContraseña.Enabled = false;
            DP_CambiarIdioma.Enabled = false;
            DP_CerrarSesion.Enabled = false;
            DP_Polizas.Enabled = false;
            DP_GestionDeUsuarios.Enabled = false;
            DP_GestionDePerfiles.Enabled = false;
            DP_BitacoraDeEventos.Enabled = false;
            DP_GestionDeRespaldo.Enabled = false;
            DP_GestionDeClientes.Enabled = false;
            DP_GestionDeProductos.Enabled = false;
            DP_GenerarContratacion.Enabled = false;
            DP_ModificarSeguro.Enabled = false;
            DP_EliminarSeguro.Enabled = false;
            DP_Contratacion.Enabled = false;
            DP_CambiarContraseña.Enabled = false;
            ReporteSiniestro.Enabled = false;
        }

        private void DP_IniciarSesion_Click(object sender, EventArgs e)
        {
            _686DPfrmLogIn logIn = new _686DPfrmLogIn(IdiomaLocal);
            logIn.MdiParent = this;
            logIn.Show();
        }

        private void DP_GestionDeUsuarios_Click(object sender, EventArgs e)
        {
            _686DPfrmGestionUsuarios GestionUsuarios = new _686DPfrmGestionUsuarios(IdiomaLocal);
            GestionUsuarios.MdiParent = this;
            GestionUsuarios.Show();
        }

        private void DP_CambiarContraseña_Click(object sender, EventArgs e)
        {
            _686DPfrmCambiarContraseña cambiarcontraseña = new _686DPfrmCambiarContraseña(IdiomaLocal);
            cambiarcontraseña.MdiParent = this;
            cambiarcontraseña.Show();
        }

        internal void Activar()
        {
            DP_Admin.Enabled = true;
            DP_Contratacion.Enabled = true;
            Siniestros.Enabled = true;
            DP_CambiarContraseña.Enabled = true;
            DP_CambiarIdioma.Enabled = true;
            Dp_Ayuda.Enabled = true;
            DP_Maestro.Enabled = true;
            DP_CerrarSesion.Enabled = true;
            DP_Polizas.Enabled = true;
        }

        private void DP_CerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
            LMG.Traducir("ConfirmarCerrarSesion"),
            LMG.Traducir("TituloCerrarSesion"),
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );


            if (resultado == DialogResult.Yes)
            {
                if (_686DP_Singleton.Instancia._686DPIsLogged())
                {
                    _686DP_BLLUsuario bllu = new _686DP_BLLUsuario();
                    bllu.GuardarIdioma(_686DP_Singleton.Instancia.Usuario._686DPIdioma);
                    _686DP_Singleton.Instancia._686DPLogOut();
                    MessageBox.Show(LMG.Traducir("SesionCerrada"), LMG.Traducir("TituloCerrarSesion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //DGV.grabarTodosDV();
                    //blle.RegistrarEvento(_686DP_Singleton.Instancia.Usuario._686DPDNI, this.Name, "LogOut", 1);

                    _686DP_Desactivar();
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("NoSesionActiva"), LMG.Traducir("TituloCerrarSesion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DP_CambiarIdioma_Click(object sender, EventArgs e)
        {
            _686DPfrmIdioma idioma = new _686DPfrmIdioma(IdiomaLocal);
            idioma.MdiParent = this;
            idioma.Show();
        }

        private void Dp_Ayuda_Click(object sender, EventArgs e)
        {
        }

        internal void ActivarRol()
        {
            DP_Admin.Enabled = true;
            DP_Contratacion.Enabled = true;
            Siniestros.Enabled = true;
            DP_CambiarContraseña.Enabled = false;
        }

        private void generarContratacion_Click(object sender, EventArgs e)
        {
            _686DPfrmGenerarContratación contratacion = new _686DPfrmGenerarContratación(IdiomaLocal);
            contratacion.MdiParent = this;
            contratacion.Show();
        }



        private void registrarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _686DPfrmGestionarClientes rc = new _686DPfrmGestionarClientes(IdiomaLocal);
            rc.MdiParent = this;
            rc.Show();
        }

        private void gestionDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _686DP_frmCrearProducto cp = new _686DP_frmCrearProducto(IdiomaLocal);
            cp.MdiParent = this;
            cp.Show();
        }

        private void modificarSeguroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _686DPfrmModificarSeguro ms = new _686DPfrmModificarSeguro(IdiomaLocal);
            ms.MdiParent = this;
            ms.Show();
        }

        private void eliminarSeguroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _686DPfrmCancelarSeguro cs = new _686DPfrmCancelarSeguro(IdiomaLocal);
            cs.MdiParent = this;
            cs.Show();
        }

        private void DP_GestionDePerfiles_Click(object sender, EventArgs e)
        {
            _686DPfrmCrearPerfil cp = new _686DPfrmCrearPerfil(IdiomaLocal);
            cp.MdiParent = this;
            cp.Show();
        }

        internal void CambiarIdioma(string idi)
        {
            if (string.IsNullOrWhiteSpace(idi))
                idi = "Español";

            if (!idioma.ContieneObservador(LMG))
            {
                idioma.AgregarObsevador(LMG);
            }
            if (!LMG.Forms.Contains(this))
            {
                LMG.Forms.Add(this);
            }

            foreach (Form hijo in this.MdiChildren)
            {
                if (!LMG.Forms.Contains(hijo))
                {
                    LMG.Forms.Add(hijo);
                }
            }
            idioma.CambiarIdioma(idi);
            IdiomaLocal = idi;
            LMG.CargarMensajesGlobales(idi);

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void polizasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _686DP_frmPolizas po = new _686DP_frmPolizas(IdiomaLocal);
            po.MdiParent = this;
            po.Show();
        }

        private void DP_GestionDeRespaldo_Click(object sender, EventArgs e)
        {
            GestionDeRespaldo gr = new GestionDeRespaldo(IdiomaLocal);
            gr.MdiParent = this;
            gr.Show();
        }

        private void DP_BitacoraDeEventos_Click(object sender, EventArgs e)
        {
            _686DPfrmBitacoraDeEventos BE = new _686DPfrmBitacoraDeEventos(IdiomaLocal);
            BE.MdiParent = this;
            BE.Show();
        }

        private void Dp_Siniestros_Click(object sender, EventArgs e)
        {

        }

        private void RegistrarSiniestro_Click(object sender, EventArgs e)
        {
            _686DPfrmRegistrarSiniestro rs = new _686DPfrmRegistrarSiniestro(IdiomaLocal);
            rs.MdiParent = this;
            rs.Show();
        }

        private void auditarSiniestroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _686DPfrmAuditarSiniestro AS = new _686DPfrmAuditarSiniestro(IdiomaLocal);
            AS.MdiParent = this;
            AS.Show();
        }

        private void ReporteSiniestro_Click(object sender, EventArgs e)
        {
            _686DP_frmReporteSiniestro rs = new _686DP_frmReporteSiniestro(IdiomaLocal);
            rs.MdiParent = this;
            rs.Show();
        }

        private void bitacoraDeCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _686DPfrmBitacoraCambio bc = new _686DPfrmBitacoraCambio(IdiomaLocal);
            bc.MdiParent = this;
            bc.Show();
        }

        private void _686DPfrmInicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_686DP_Singleton.Instancia._686DPIsLogged())
                {
                    DGV.grabarTodosDV();
                    MessageBox.Show(LMG.Traducir("DVActualizados"), "✔️ " + LMG.Traducir("Integridad"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorDV") + ": " + ex.Message, "❌ " + LMG.Traducir("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Paginas de ayuda
        private void maestroDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    // Ruta completa del archivo dentro de bin\Debug\Ayuda
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "CrearProducto.html");

                    if (File.Exists(ruta))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = ruta,
                            UseShellExecute = true // Usa el navegador predeterminado
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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    // Ruta completa del archivo dentro de bin\Debug\Ayuda
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "CrearProductoIngles.html");

                    if (File.Exists(ruta))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = ruta,
                            UseShellExecute = true // Usa el navegador predeterminado
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

        private void maestroDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    // Ruta completa del archivo dentro de bin\Debug\Ayuda
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "GestionarCliente.html");

                    if (File.Exists(ruta))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = ruta,
                            UseShellExecute = true // Usa el navegador predeterminado
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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    // Ruta completa del archivo dentro de bin\Debug\Ayuda
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "GestionarClienteIngles.html");

                    if (File.Exists(ruta))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = ruta,
                            UseShellExecute = true // Usa el navegador predeterminado
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

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    // Ruta completa del archivo dentro de bin\Debug\Ayuda
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "GestionarUsuarios.html");

                    if (File.Exists(ruta))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = ruta,
                            UseShellExecute = true // Usa el navegador predeterminado
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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    // Ruta completa del archivo dentro de bin\Debug\Ayuda
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "GestionarUsuariosIngles.html");

                    if (File.Exists(ruta))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = ruta,
                            UseShellExecute = true // Usa el navegador predeterminado
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

        private void gestionDePerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "GestionPerfiles.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "GestionPerfilesIngles.html");

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

        private void bitacoraDeCambiosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "BitacoraCambio.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "BitacoraCambioIngles.html");

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

        private void bitacoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "BitacoraEvento.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "BitacoraEventoIngles.html");

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

        private void respaldoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "Respaldo.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "RespaldoIngles.html");

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

        private void generarContratacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "GenerarContratacion.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "GenerarContratacionIngles.html");

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

        private void modificarSeguroToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "ModificarSeguro.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "ModificarSeguroIngles.html");

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


        private void eliminarSeguroToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "CancelarSeguro.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "CancelarSeguroIngles.html");

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

        private void registrarSiniestroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "RegistrarSiniestro.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "RegistrarSiniestroIngles.html");

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

        private void auditarSiniestroToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "AuditarSiniestro.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "AuditarSiniestroIngles.html");

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

        private void polizasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "Polizas.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "PolizasIngles.html");

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

        private void reporteSiniestrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "ReporteSiniestro.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "ReporteSiniestroIngles.html");

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

        private void recomendaciónDeAumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _686DPfrmRecomendacionDeAumento rda = new _686DPfrmRecomendacionDeAumento(IdiomaLocal);
            rda.MdiParent = this;
            rda.Show();
        }

        private void prediccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IdiomaLocal == "Español")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "ReporteInteligente.html");

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
            else if (IdiomaLocal == "Ingles")
            {
                try
                {
                    string ruta = Path.Combine(Application.StartupPath, "Ayuda", "ReporteInteligenteIngles.html");

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
