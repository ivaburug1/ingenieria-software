using System;
using System.Collections.Generic;
using System.Data;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLLReporteInteligente
    {
        public DataTable ObtenerReporteInteligente()
        {
            return DAL.ObtenerReporteInteligente();
        }

        public DataTable ObtenerReporteInteligenteFiltrado(string artista, DateTime? fecha)
        {
            return DAL.ObtenerReporteInteligenteFiltrado(artista, fecha);
        }

        public List<string> ObtenerArtistas()
        {
            return DAL.ObtenerArtistasReporteInteligente();
        }

        public List<DateTime> ObtenerFechasPorArtista(string artista)
        {
            return DAL.ObtenerFechasReporteInteligentePorArtista(artista);
        }
    }
}