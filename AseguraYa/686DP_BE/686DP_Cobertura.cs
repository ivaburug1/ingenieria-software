using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DP_Cobertura
    {
        public int condigoCobertura { get; set; }
        public string DP686_Descripcion { get; set; }
        public decimal DP686_SumaAsegurada { get; set; }

        public _686DP_Cobertura(int codCobertura, string descripcion, decimal sumaAsegurada)
        {
            condigoCobertura = codCobertura;
            DP686_Descripcion = descripcion;
            DP686_SumaAsegurada = sumaAsegurada;
        }
    }
}
