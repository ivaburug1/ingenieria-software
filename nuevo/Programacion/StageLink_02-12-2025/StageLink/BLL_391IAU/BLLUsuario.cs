using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_391IAU;
using DAL_391IAU;
using SessionManager_391IAU;
using System.Data.SqlClient;
using System.Data;

namespace BLL_391IAU
{
    public class BLLUsuario
    {
        public bool CrearUsuario(string dni, string nombre, string apellido, string email, string contraseña, int? rolID = null)
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

            if (string.IsNullOrWhiteSpace(contraseña))
                contraseña = dni + nombre;

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

            var sesion = SessionManager_391IAU.ObtenerInstancia();
            if (sesion.HaySesionActiva())
                throw new InvalidOperationException("Ya hay una sesión iniciada.");

            if (!int.TryParse(dni, out int dniInt))
                throw new ArgumentException("El DNI ingresado no es válido.");

            BEUsuario usuario = DAL.ObtenerUsuarioPorDNI(dniInt);

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

            var permisosFinales = new List<BEPermiso>();

            if (usuario.IDRol_391IAU.HasValue)
            {
                int idRol = usuario.IDRol_391IAU.Value;

                permisosFinales.AddRange(DAL.ObtenerPermisosDePerfil(idRol));

                var familias = DAL.ObtenerFamiliasDePerfil(idRol);

                foreach (var fam in familias)
                {
                    foreach (var p in DAL.ObtenerPermisosDeFamilia(fam))
                        if (!permisosFinales.Any(x => x.IDPermiso_391IAU == p.IDPermiso_391IAU))
                            permisosFinales.Add(p);

                    ExpandirFamilia(fam, permisosFinales);
                }
            }

            sesion.Permisos = permisosFinales;
            sesion.IniciarSesion(usuario);

            return true;
        }

        private void ExpandirFamilia(int idFamilia, List<BEPermiso> acumulado)
        {
            var permisos = DAL.ObtenerPermisosDeFamilia(idFamilia);

            foreach (var p in permisos)
                if (!acumulado.Any(x => x.IDPermiso_391IAU == p.IDPermiso_391IAU))
                    acumulado.Add(p);

            var subfamilias = DAL.ObtenerSubfamilias(idFamilia);

            foreach (var sub in subfamilias)
                ExpandirFamilia(sub, acumulado);
        }


        public string ObtenerNombreUsuarioLogueado()
        {
            var sesion = SessionManager_391IAU.ObtenerInstancia();
            if (!sesion.HaySesionActiva()) return "";
            return $"{sesion.UsuarioActual.Nombre_391IAU} {sesion.UsuarioActual.Apellido_391IAU}";
        }

        public bool ValidarContraseñaActual(string actual)
        {
            var sesion = SessionManager_391IAU.ObtenerInstancia();

            if (!sesion.HaySesionActiva())
                throw new InvalidOperationException("Debe iniciar sesión.");

            string hashActual = CriptoManager.EncriptarSHA256(actual);
            return sesion.UsuarioActual.Contraseña_391IAU == hashActual;
        }

        public bool ContraseñaYaFueUsada(string contraseñaNueva)
        {
            var sesion = SessionManager_391IAU.ObtenerInstancia();
            string hash = CriptoManager.EncriptarSHA256(contraseñaNueva);
            return DAL.ContraseñaFueUsada(sesion.UsuarioActual.DNI_391IAU, hash);
        }

        public void CambiarContraseña(string nuevaContraseña)
        {
            var sesion = SessionManager_391IAU.ObtenerInstancia();
            string hash = CriptoManager.EncriptarSHA256(nuevaContraseña);
            sesion.UsuarioActual.Contraseña_391IAU = hash;

            DAL.ActualizarContraseña(sesion.UsuarioActual.DNI_391IAU, hash);
            DAL.GuardarContraseñaHistorial(sesion.UsuarioActual.DNI_391IAU, hash);
        }

        public void Logout()
        {
            var sesion = SessionManager_391IAU.ObtenerInstancia();
            if (!sesion.HaySesionActiva())
                throw new InvalidOperationException("No hay sesión activa.");

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


        public static bool EsBase64(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            try { Convert.FromBase64String(input); return true; }
            catch { return false; }
        }

        public bool UsuarioTienePermiso(string permiso)
        {
            var sesion = SessionManager_391IAU.ObtenerSesion();
            if (sesion == null || sesion.UsuarioActual == null) return false;

            return sesion.Permisos.Any(p => p.NombrePermiso_391IAU == permiso);
        }
    }
}
