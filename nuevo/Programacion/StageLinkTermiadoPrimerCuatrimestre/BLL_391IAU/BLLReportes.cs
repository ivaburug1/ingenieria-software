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

    }
}
