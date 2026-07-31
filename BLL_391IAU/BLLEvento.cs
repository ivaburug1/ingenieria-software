using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_391IAU;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLL_Evento
    {
        public bool InsertarEvento(BEEvento_391IAU evento)
        {
            try
            {
                return DAL.InsertarEvento(evento);
            }
            catch
            {
                return false;
            }
        }
        public bool EventoExiste(DateTime fecha, int codigoEstadio)
        {
            return DAL.ExisteEventoPorFechaYEstadio(fecha, codigoEstadio);
        }
        public bool ExisteEventoPorArtistaYFecha(string artista, DateTime fecha)
        {
            return DAL.ExisteEventoPorArtistaYFecha(artista, fecha);
        }
        public List<string> ObtenerEventos()
        {
            return DAL.ObtenerNombresEventos();
        }

        public List<DateTime> ObtenerFechasPorArtista(string nombreArtista)
        {
            return DAL.ObtenerFechasPorArtista(nombreArtista);
        }
        public (string estadio, string direccion) ObtenerEstadioYDireccion(string artista)
        {
            return DAL.ObtenerEstadioYDireccion(artista);
        }

        public (string estadio, string direccion) ObtenerEstadioYDireccion(string artista, DateTime fecha)
        {
            return DAL.ObtenerEstadioYDireccion(artista, fecha);
        }
        public int ObtenerCodigoEvento(string artista, DateTime fecha)
        {
            return DAL.ObtenerCodigoEvento(artista, fecha);
        }

    }
}
