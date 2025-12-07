using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DP_Seguro
    {
        public string DP686_TipoProducto { get; set; }
        public List<_686DP_Plan> Planes { get; set; } = new List<_686DP_Plan>();
        public int cantidadVendida { get; set; }
        public _686DP_Seguro(string tipoProducto)
        {
            DP686_TipoProducto = tipoProducto;
        }
    }
}
