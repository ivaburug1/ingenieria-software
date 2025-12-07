using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_SERVICIOS
{
    public class _686DP_Usuario
    { 
        public int _686DPDNI { get; set; }
        public string _686DPNombreUsuario { get; set; }
        public string _686DPPassword { get; set; }
        public string _686DPIdioma { get; set; }

        public override string ToString()
        {
            return _686DPNombreUsuario;
        }

    }
}
