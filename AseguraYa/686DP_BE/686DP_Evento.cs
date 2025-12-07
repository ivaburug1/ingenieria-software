using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DP_Evento
    {
        public int DP686_DNI { get; set; }
        public string DP686_CodEvento { get; set; }
        public DateTime DP686_Fecha { get; set; }
        public string DP686_Modulo { get; set; }
        public string DP686_Descripcion { get; set; }
        public int DP686_Criticidad { get; set; }

        public _686DP_Evento(int dni, string codEvento, DateTime fecha, string modulo, string descripcion, int criticidad)
        {
            DP686_DNI = dni;
            DP686_CodEvento = codEvento;
            DP686_Fecha = fecha;
            DP686_Modulo = modulo;
            DP686_Descripcion = descripcion;
            DP686_Criticidad = criticidad;
        }
    }
}
