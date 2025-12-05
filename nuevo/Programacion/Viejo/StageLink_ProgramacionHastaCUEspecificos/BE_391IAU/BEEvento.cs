using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_391IAU
{
    public class BEEvento_391IAU
    {
        public DateTime Fecha_391IAU { get; set; }
        public int CodigoDeEstadio_391IAU { get; set; }
        public string NombreArtista_391IAU { get; set; }

        public BEEvento_391IAU(DateTime fecha, int codigoEstadio, string nombreArtista)
        {
            Fecha_391IAU = fecha;
            CodigoDeEstadio_391IAU = codigoEstadio;
            NombreArtista_391IAU = nombreArtista;
        }
    }
}
