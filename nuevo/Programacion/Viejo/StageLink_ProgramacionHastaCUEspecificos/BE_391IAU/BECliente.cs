using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_391IAU
{
    public class BECliente
    {
        public int DNI_391IAU { get; set; }
        public string Nombre_391IAU { get; set; }
        public string Apellido_391IAU { get; set; }
        public string Correo_391IAU { get; set; }

        public BECliente(int dni, string nombre, string apellido, string correo)
        {
            DNI_391IAU = dni;
            Nombre_391IAU = nombre;
            Apellido_391IAU = apellido;
            Correo_391IAU = correo;
        }
    }
}
