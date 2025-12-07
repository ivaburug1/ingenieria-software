using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DP_Usuarios
    {
        public int DP686_DNI { get; set; }
        public string DP686_Nombre { get; set; }
        public string DP686_Apellido { get; set; }
        public string DP686_Email { get; set; }
        public string DP686_Rol { get; set; }
        public string DP686_Usuario { get; set; }
        public string DP686_Contraseña { get; set; }
        public bool DP686_Activo { get; set; }
        public bool DP686_Bloqueado { get; set; }
        public bool DP686_CambiarContraseña { get; set; }
        public string DP686_Idioma { get; set; }



        public _686DP_Usuarios(int dni, string nombre, string apellido, string email, string rol, string usuario, string contraseña, bool activo, bool bloqueado, bool cambiarcontra)
        {
            DP686_DNI = dni;
            DP686_Nombre = nombre;
            DP686_Apellido = apellido;
            DP686_Email = email;
            DP686_Rol = rol;
            DP686_Usuario = usuario;
            DP686_Contraseña = contraseña;
            DP686_Activo = activo;
            DP686_Bloqueado = bloqueado;
            DP686_CambiarContraseña = cambiarcontra;
        }
    }
}
