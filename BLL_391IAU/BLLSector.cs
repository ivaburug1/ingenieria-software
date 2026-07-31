using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLLSector
    {
        public List<BESector> ObtenerSectoresPorEvento(string nombreArtista, DateTime fecha)
        {
            return DAL.ObtenerSectoresPorEvento(nombreArtista, fecha);
        }
        public decimal ObtenerPrecioSector(string nombreSector)
        {
            return DAL.ObtenerPrecioSector(nombreSector);
        }
        public int ObtenerCapacidadSector(string nombreSector)
        {
            return DAL.ObtenerCapacidadPorSector(nombreSector);
        }

        public int ObtenerDisponibles(int codigoEvento, int codigoSector)
        {
            int capacidad = DAL.ObtenerCapacidadPorCodigoSector(codigoSector);
            int vendidas = DAL.ObtenerEntradasVendidas(codigoEvento, codigoSector);
            return Math.Max(0, capacidad - vendidas);
        }
    }
}
