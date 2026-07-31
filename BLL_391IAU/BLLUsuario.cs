using BE_391IAU;
using BLL_391IAU;
using DAL_391IAU;
using Servicios_391IAU.Composite;
using SessionManager_391IAU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios_391IAU;

namespace BLL_391IAU
{
    public class BLLUsuario
    {
        BEUsuario usuario = new BEUsuario();
        private static List<IComponentePermiso_391IAU> Permisos = new List<IComponentePermiso_391IAU>();
        public bool CrearUsuario(string dni, string nombre, string apellido, string email, int? rolID = null)
        {
            if (string.IsNullOrWhiteSpace(dni) ||
                string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Completar todos los campos obligatorios.");
            }

            int dniInt = int.Parse(dni);
            BEUsuario existente = DAL.ObtenerUsuarioPorDNI(dniInt);

            if (existente != null)
                throw new InvalidOperationException("Ya existe un usuario registrado con ese DNI.");

            string contraseña = dni + apellido;

            string contraseñaEncriptada = CriptoManager.EncriptarSHA256(contraseña);

            BEUsuario nuevoUsuario = new BEUsuario
            {
                DNI_391IAU = dniInt,
                Nombre_391IAU = nombre.Trim(),
                Apellido_391IAU = apellido.Trim(),
                eMail_391IAU = CriptoManager.EncriptarAES(email.Trim()),
                Contraseña_391IAU = contraseñaEncriptada,
                Idioma_391IAU = "Español",
                Activo_391IAU = true,
                Bloqueado_391IAU = false,
                Intentos_391IAU = 0,
                IDRol_391IAU = rolID
            };

            try
            {
                return DAL.InsertarUsuario(nuevoUsuario);
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    throw new InvalidOperationException("Ya existe un usuario registrado con ese DNI.");

                throw new InvalidOperationException("Ocurrió un error al intentar crear el usuario.");
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Ocurrió un error inesperado. Intente nuevamente.");
            }
        }
        public bool Login(string dni, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(contrasenia))
                throw new ArgumentException("Debe ingresar usuario y contraseña.");

            var sesion = SessionManager_391IAU.SessionManager_391IAU.ObtenerInstancia();
            if (sesion.HaySesionActiva())
                throw new InvalidOperationException("Ya hay una sesión iniciada.");

            if (!int.TryParse(dni, out int dniInt))
                throw new ArgumentException("El DNI ingresado no es válido.");

            usuario = DAL.ObtenerUsuarioPorDNI(dniInt);

            if (usuario == null)
                throw new UnauthorizedAccessException("El DNI ingresado no corresponde a un usuario registrado.");

            if (!usuario.Activo_391IAU)
                throw new UnauthorizedAccessException("El usuario no está activo.");

            if (usuario.Bloqueado_391IAU)
                throw new UnauthorizedAccessException("El usuario se encuentra bloqueado.");

            string hashIngresado = CriptoManager.EncriptarSHA256(contrasenia);

            if (usuario.Contraseña_391IAU != hashIngresado)
            {
                usuario.Intentos_391IAU++;

                if (usuario.Intentos_391IAU >= 3)
                {
                    usuario.Bloqueado_391IAU = true;
                    DAL.ActualizarUsuario(usuario);
                    throw new UnauthorizedAccessException("Ingreso mal sus datos 3 veces. Cuenta bloqueada.");
                }
                else
                {
                    DAL.ActualizarUsuario(usuario);
                    throw new UnauthorizedAccessException($"Contraseña incorrecta. Intento {usuario.Intentos_391IAU}/3");
                }
            }

            DAL.ActualizarIntentos(usuario.DNI_391IAU, 0);

            Permisos.Clear();

            if (usuario.IDRol_391IAU.HasValue)
            {
                BLLPerfil bllPerfil = new BLLPerfil();
                var perfil = bllPerfil.TraerPerfilCompleto(usuario.IDRol_391IAU.Value);
                Permisos.AddRange(perfil.Permisos);
            }


            sesion.IniciarSesion(usuario);
            CargarPermisos();
            return true;
        }


        public string ObtenerNombreUsuarioLogueado()
        {
            var sesion = SessionManager_391IAU.SessionManager_391IAU.ObtenerInstancia();
            if (!sesion.HaySesionActiva()) return "";
            return $"{sesion.UsuarioActual.Nombre_391IAU} {sesion.UsuarioActual.Apellido_391IAU}";
        }

        public bool ValidarContraseñaActual(string actual)
        {
            var sesion = SessionManager_391IAU.SessionManager_391IAU.ObtenerInstancia();

            if (!sesion.HaySesionActiva())
                throw new InvalidOperationException("Debe iniciar sesión.");

            string hashActual = CriptoManager.EncriptarSHA256(actual);
            return sesion.UsuarioActual.Contraseña_391IAU == hashActual;
        }

        public bool ContraseñaYaFueUsada(string contraseñaNueva)
        {
            var sesion = SessionManager_391IAU.SessionManager_391IAU.ObtenerInstancia();
            string hash = CriptoManager.EncriptarSHA256(contraseñaNueva);
            return DAL.ContraseñaFueUsada(sesion.UsuarioActual.DNI_391IAU, hash);
        }

        public void CambiarContraseña(string nuevaContraseña)
        {
            var sesion = SessionManager_391IAU.SessionManager_391IAU.ObtenerInstancia();
            string hash = CriptoManager.EncriptarSHA256(nuevaContraseña);
            sesion.UsuarioActual.Contraseña_391IAU = hash;

            DAL.ActualizarContraseña(sesion.UsuarioActual.DNI_391IAU, hash);
            DAL.GuardarContraseñaHistorial(sesion.UsuarioActual.DNI_391IAU, hash);
        }
        public void ActualizarIdiomaUsuario(string idioma)
        {
            var sesion = SessionManager_391IAU.SessionManager_391IAU.ObtenerInstancia();
            if (!sesion.HaySesionActiva()) return;
            DAL.ActualizarIdioma(sesion.UsuarioActual.DNI_391IAU, idioma);
        }

        public void Logout()
        {
            var sesion = SessionManager_391IAU.SessionManager_391IAU.ObtenerInstancia();

            if (!sesion.HaySesionActiva())
                throw new InvalidOperationException("No hay sesión activa.");

            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            DAL.ActualizarIdioma(sesion.UsuarioActual.DNI_391IAU, sm.IdiomaActual);

            sesion.CerrarSesion();
        }

        public List<BEUsuario> ObtenerUsuariosConRol()
        {
            return DAL.ObtenerTodosLosUsuarios();
        }

        public List<BEUsuario> ObtenerTodosLosUsuarios()
        {
            return DAL.ObtenerTodosLosUsuarios();
        }

        public bool ModificarUsuario(BEUsuario usuarioModificado)
        {
            return DAL.ModificarUsuario(usuarioModificado);
        }

        private void CargarPermisos()
        {
            Permisos.Clear();

            if (usuario == null || usuario.IDRol_391IAU == null)
                return;

            var bll = new BLLPerfil();
            var perfil = bll.TraerPerfilCompleto(usuario.IDRol_391IAU.Value);

            if (perfil != null)
                Permisos = perfil.ObtenerHijos();
        }
        public bool TienePermiso(string permisoBuscado)
        {
            if (usuario == null)
                return false;

            return PermisoRecursivo(Permisos, permisoBuscado);
        }

        private bool PermisoRecursivo(List<IComponentePermiso_391IAU> lista, string permiso)
        {
            foreach (var comp in lista)
            {
                if (comp is PermisoSimple_391IAU p)
                {
                    if (p.Nombre == permiso)
                        return true;
                }
                else if (comp is Familia_391IAU f)
                {
                    if (PermisoRecursivo(f.ObtenerHijos(), permiso))
                        return true;
                }
            }

            return false;
        }
        
        public static string EncriptarAES(string textoPlano)
        {
            return CriptoManager.EncriptarAES(textoPlano);
        }

        public static string DesencriptarAES(string textoEncriptado)
        {
            return CriptoManager.DesencriptarAES(textoEncriptado);
        }


        public void ReencriptarTodosLosEmails()
        {
            List<BEUsuario> usuarios = DAL.ObtenerTodosLosUsuarios();

            foreach (BEUsuario u in usuarios)
            {
                string dni = u.DNI_391IAU.ToString();
                string emailPlano = u.eMail_391IAU;

                if (CriptoManager.EsBase64(emailPlano))
                    continue;

                string emailEncriptado = CriptoManager.EncriptarAES(emailPlano);

                DAL.ActualizarEmail(dni, emailEncriptado);
            }
        }
        public BEUsuario ObtenerUsuarioPorDNI(int dni)
        {
            return DAL.ObtenerUsuarioPorDNI(dni);
        }

        public static bool EsBase64(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            try { Convert.FromBase64String(input); return true; }
            catch { return false; }
        }

        public bool UsuarioTienePermiso(string permiso)
        {
            var sesion = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (sesion == null || sesion.UsuarioActual == null)
                return false;

            return Permisos.Any(p => p.Nombre == permiso);
        }

        public List<IComponentePermiso_391IAU> TraerPermisos()
        {
            return Permisos;
        }
    }
}
