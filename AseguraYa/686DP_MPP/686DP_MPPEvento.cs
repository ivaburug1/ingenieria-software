using _686DP_BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_Dal;
using System.Data;

namespace _686DP_MPP
{
    public class _686DP_MPPEvento
    {
        _686DPDalGeneral dal= new _686DPDalGeneral();

        public List<_686DP_Evento> Filtrar(DateTime? fechaDesde, DateTime? fechaHasta, string modulo, int? criticidad, int? dni)
        {
            ArrayList parametros = new ArrayList();

            string query = @"
            SELECT E.*
            FROM [dbo].[686DP_Eventos] AS E
            WHERE 1=1";

            if (fechaDesde.HasValue)
            {
                query += " AND E.[DP686_Fecha] >= @FechaDesde";
                parametros.Add(new SqlParameter("@FechaDesde", fechaDesde.Value.Date));
            }

            if (fechaHasta.HasValue)
            {
                query += " AND E.[DP686_Fecha] <= @FechaHasta";
                parametros.Add(new SqlParameter("@FechaHasta", fechaHasta.Value.Date));
            }

            if (!string.IsNullOrEmpty(modulo))
            {
                query += " AND E.[DP686_Modulo] = @Modulo";
                parametros.Add(new SqlParameter("@Modulo", modulo));
            }

            if (criticidad.HasValue)
            {
                query += " AND E.[DP686_Criticidad] = @Criticidad";
                parametros.Add(new SqlParameter("@Criticidad", criticidad.Value));
            }

            if (dni.HasValue)
            {
                query += " AND E.[DP686_DNI] = @DNI";
                parametros.Add(new SqlParameter("@DNI", dni.Value));
            }

            query += " ORDER BY E.[DP686_Fecha] DESC";

            DataTable dt = dal._686DPConsultar(query, parametros);
            List<_686DP_Evento> lista = new List<_686DP_Evento>();

            foreach (DataRow row in dt.Rows)
            {
                var evento = new _686DP_Evento
                (
                    Convert.ToInt32(row["DP686_DNI"]),
                    row["DP686_CodEvento"].ToString(),
                    Convert.ToDateTime(row["DP686_Fecha"]),
                    row["DP686_Modulo"].ToString(),
                    row["DP686_Descripcion"].ToString(),
                    Convert.ToInt32(row["DP686_Criticidad"])
                );
                lista.Add(evento);
            }

            return lista;
        }

        public void RegistrarEvento(_686DP_Evento evento)
        {
            try
            {
                string query = @"
                INSERT INTO [dbo].[686DP_Eventos] 
                (DP686_DNI, DP686_CodEvento, DP686_Fecha, DP686_Modulo, DP686_Descripcion, DP686_Criticidad)
                VALUES (@DNI, @CodEvento, @Fecha, @Modulo, @Descripcion, @Criticidad)";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@DNI", evento.DP686_DNI),
                    new SqlParameter("@CodEvento", evento.DP686_CodEvento),
                    new SqlParameter("@Fecha", evento.DP686_Fecha),
                    new SqlParameter("@Modulo", evento.DP686_Modulo),
                    new SqlParameter("@Descripcion", evento.DP686_Descripcion),
                    new SqlParameter("@Criticidad", evento.DP686_Criticidad)
                };

                dal._686DPEscribir(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el evento: " + ex.Message, ex);
            }
        }

        public List<_686DP_Evento> traerEventos()
        {
            try
            {
                string consulta = @"
                SELECT 
                    DP686_DNI,
                    DP686_CodEvento,
                    DP686_Fecha,
                    DP686_Modulo,
                    DP686_Descripcion,
                    DP686_Criticidad
                FROM [dbo].[686DP_Eventos]
                WHERE DP686_Fecha >= DATEADD(DAY, -3, CAST(GETDATE() AS DATE))
                ORDER BY DP686_CodEvento DESC";

                ArrayList parametros = new ArrayList();
                DataTable dt = dal._686DPConsultar(consulta, parametros);

                List<_686DP_Evento> lista = new List<_686DP_Evento>();

                foreach (DataRow row in dt.Rows)
                {
                    _686DP_Evento evento = new _686DP_Evento(
                        Convert.ToInt32(row["DP686_DNI"]),
                        row["DP686_CodEvento"].ToString(),
                        Convert.ToDateTime(row["DP686_Fecha"]),
                        row["DP686_Modulo"].ToString(),
                        row["DP686_Descripcion"].ToString(),
                        Convert.ToInt32(row["DP686_Criticidad"])
                    );

                    lista.Add(evento);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer los eventos: " + ex.Message, ex);
            }
        }

        public _686DP_Evento TraerUltimoEvento()
        {
            try
            {
                string consulta = @"
                SELECT TOP 1 
                    DP686_DNI, 
                    DP686_CodEvento, 
                    DP686_Fecha, 
                    DP686_Modulo, 
                    DP686_Descripcion, 
                    DP686_Criticidad
                FROM [dbo].[686DP_Eventos]
                ORDER BY DP686_CodEvento DESC;";

                ArrayList parametros = new ArrayList();
                DataTable dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new _686DP_Evento(
                        Convert.ToInt32(row["DP686_DNI"]),
                        row["DP686_CodEvento"].ToString(),
                        Convert.ToDateTime(row["DP686_Fecha"]),
                        row["DP686_Modulo"].ToString(),
                        row["DP686_Descripcion"].ToString(),
                        Convert.ToInt32(row["DP686_Criticidad"])
                    );
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer el último evento: " + ex.Message, ex);
            }
        }
    }
}
