using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                string query = "INSERT INTO Eventos_391IAU (Fecha_391IAU, CodigoDeEstadio_391IAU, NombreArtista_391IAU) " +
                               "VALUES (@Fecha, @CodigoEstadio, @NombreArtista)";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@Fecha", evento.Fecha_391IAU),
                    new SqlParameter("@CodigoEstadio", evento.CodigoDeEstadio_391IAU),
                    new SqlParameter("@NombreArtista", evento.NombreArtista_391IAU)
                };

                return DAL.EjecutarInsert(query, parametros) > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool EventoExiste(DateTime fecha, int codigoEstadio)
        {
            string query = "SELECT COUNT(*) FROM Eventos_391IAU WHERE Fecha_391IAU = @Fecha AND CodigoDeEstadio_391IAU = @CodigoEstadio";

            SqlParameter[] parametros =
            {
                new SqlParameter("@Fecha", fecha),
                new SqlParameter("@CodigoEstadio", codigoEstadio)
            };

            int cantidad = DAL.EjecutarScalar(query, parametros);

            return cantidad > 0;
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
        public int ObtenerCodigoEvento(string artista, DateTime fecha)
        {
            string query = "SELECT CodigoDeEvento_391IAU FROM Eventos_391IAU WHERE NombreArtista_391IAU = @Artista AND Fecha_391IAU = @Fecha";

            SqlParameter[] parametros =
            {
                new SqlParameter("@Artista", artista),
                new SqlParameter("@Fecha", fecha)
            };

            return DAL.EjecutarScalar(query, parametros);
        }

    }
}
