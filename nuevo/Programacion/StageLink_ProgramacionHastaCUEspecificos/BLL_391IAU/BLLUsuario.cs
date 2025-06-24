using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_391IAU;
using DAL_391IAU;
using SessionManager_391IAU;

namespace BLL_391IAU
{
    public class BLLUsuario
    {
        public bool CrearUsuario(string dni, string nombre, string apellido, string email, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(dni) ||
                string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido))
            {
                throw new ArgumentException("Completar los campos obligatorios.");
            }

            if (dni.Length != 8 || !dni.All(char.IsDigit))
            {
                throw new ArgumentException("El DNI debe tener 8 dígitos, en caso de tener 7, agregar un 0 adelante.");
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new ArgumentException("El e-Mail debe tener el formato correcto.");
            }

            if (string.IsNullOrWhiteSpace(contraseña))
            {
                contraseña = dni + nombre;
            }

            string contraseñaEncriptada = CriptoManager.EncriptarSHA256(contraseña);

            BEUsuario nuevoUsuario = new BEUsuario
            {
                DNI_391IAU = int.Parse(dni),
                Nombre_391IAU = nombre.Trim(),
                Apellido_391IAU = apellido.Trim(),
                eMail_391IAU = CriptoManager.EncriptarAES(email.Trim()),
                Contraseña_391IAU = contraseñaEncriptada,
                Idioma_391IAU = "Español",
                Activo_391IAU = true,
                Bloqueado_391IAU = false,
                Intentos_391IAU = 0
            };

            return DAL.InsertarUsuario(nuevoUsuario);
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
                throw new UnauthorizedAccessException("El usuario no está activo. Contacte al administrador.");

            if (usuario.Bloqueado_391IAU)
                throw new UnauthorizedAccessException("El usuario se encuentra bloqueado por múltiples intentos fallidos.");

            string hashIngresado = CriptoManager.EncriptarSHA256(contrasenia);

            if (usuario.Contraseña_391IAU != hashIngresado)
            {
                usuario.Intentos_391IAU++;

                if (usuario.Intentos_391IAU >= 3)
                {
                    usuario.Bloqueado_391IAU = true;
                    DAL.ActualizarUsuario(usuario);
                    throw new UnauthorizedAccessException("Ingreso mal sus datos 3 veces. Su cuenta ha sido bloqueada.");
                }
                else
                {
                    DAL.ActualizarUsuario(usuario);
                    throw new UnauthorizedAccessException($"Contraseña incorrecta. Intento {usuario.Intentos_391IAU}/3");
                }
            }

            DAL.ActualizarIntentos(usuario.DNI_391IAU, 0);
            sesion.IniciarSesion(usuario);
            return true;
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
                throw new InvalidOperationException("Para cambiar la contraseña el usuario primero debe haber iniciado sesión.");

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
                throw new InvalidOperationException("No hay una sesión activa para cerrar.");

            sesion.CerrarSesion();
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

    }
}
