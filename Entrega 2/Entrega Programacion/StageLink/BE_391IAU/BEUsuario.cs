using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_391IAU
{
    public class BEUsuario
    {
        public int DNI_391IAU { get; set; }
        public string Nombre_391IAU { get; set; }
        public string Apellido_391IAU { get; set; }
        public string eMail_391IAU { get; set; }
        public string Contraseña_391IAU { get; set; }
        public string Idioma_391IAU { get; set; }
        public bool Activo_391IAU { get; set; }
        public bool Bloqueado_391IAU { get; set; }
        public int Intentos_391IAU { get; set; }
    }
}

