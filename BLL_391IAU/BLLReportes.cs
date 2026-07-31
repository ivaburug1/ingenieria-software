using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLLReportes
    {
        public DataTable ObtenerReporteVentas()
        {
            return DAL.ObtenerReporteVentas();
        }
        public List<string> ObtenerFechas()
        {
            return DAL.ObtenerFechasEvento();
        }

        public List<string> ObtenerCompradores()
        {
            return DAL.ObtenerNombresCompradores();
        }

        public List<string> ObtenerArtistas()
        {
            return DAL.ObtenerArtistas();
        }
        public DataTable ObtenerReporteRFN2()
        {
            return DAL.ObtenerReporteRFN2();
        }

        public DataTable ObtenerReporteRFN2Filtrado(string nombreProveedor, string nombreProducto, string tipoProducto)
        {
            return DAL.ObtenerReporteRFN2Filtrado(nombreProveedor, nombreProducto, tipoProducto);
        }

        public List<string> ObtenerTiposProductoRFN2()
        {
            return DAL.ObtenerTiposProductoRFN2();
        }

        public List<string> ObtenerNombresProductoRFN2()
        {
            return DAL.ObtenerNombresProductoRFN2();
        }

        public List<string> ObtenerNombresProveedorRFN2()
        {
            return DAL.ObtenerNombresProveedorRFN2();
        }
    }
}
