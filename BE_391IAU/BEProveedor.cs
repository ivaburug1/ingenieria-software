using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_391IAU
{
    public class BEProveedorEdicion
    {
        public long CUIT_391IAU { get; set; }
        public string Nombre_391IAU { get; set; }
        public string Correo_391IAU { get; set; }
    }

    public class BEProductoProveedorEdicion
    {
        public int IDProducto_391IAU { get; set; }
        public string Nombre_391IAU { get; set; }
        public int PrecioVenta_391IAU { get; set; }
        public string TipoProducto_391IAU { get; set; }
    }
}
