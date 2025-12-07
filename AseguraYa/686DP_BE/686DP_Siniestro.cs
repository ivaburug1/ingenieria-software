using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DP_Siniestro
    {
        public DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public double ValorDeReparar { get; set; }
        public double ValorDelBien { get; set; }
        public bool Estado { get; set; }
        
        public string descripcion { get; set; }

        public _686DP_Siniestro(DateTime fecha, double valorDeReparar, double valorDelBien, bool estado, string descripcion)
        {
            Fecha = fecha;
            ValorDeReparar = valorDeReparar;
            ValorDelBien = valorDelBien;
            Estado = estado;
            this.descripcion = descripcion;
        }
    }
}
