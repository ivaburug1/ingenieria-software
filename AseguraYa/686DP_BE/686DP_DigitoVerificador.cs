using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DP_DigitoVerificador
    {
        public string DP686NombreTabla { get; set; }
        public string DP686DVH { get; set; }
        public string DP686DVV { get; set; }


        public _686DP_DigitoVerificador(string nombreTabla, string dvh, string dvv)
        {
            DP686NombreTabla = nombreTabla;
            DP686DVH = dvh;
            DP686DVV = dvv;
        }
    }
}
