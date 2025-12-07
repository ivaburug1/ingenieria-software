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
using System.Dynamic;

namespace _686DP_MPP
{
    public class _686DP_MPPSiniestro
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();

        public void AprobarSiniestro(int codSiniestro)
        {
            string consulta = @"UPDATE [dbo].[686DP_Siniestro]
                        SET Estado = 1
                        WHERE CodSiniestro = @CodSiniestro";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@CodSiniestro", codSiniestro)
            };

            dal._686DPEscribir(consulta, parametros);
        }

        public void DenegarSiniestro(object codSiniestro)
        {
            string consulta = @"UPDATE [dbo].[686DP_Siniestro]
                        SET Estado = 0
                        WHERE CodSiniestro = @CodSiniestro";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@CodSiniestro", codSiniestro)
            };

            dal._686DPEscribir(consulta, parametros);
        }

        public void Pagar(int codSiniestro, DateTime dia)
        {
            string consulta = @"
                INSERT INTO [dbo].[686DP_Factura] (CodSiniestro, Fecha)
                    VALUES (@CodSiniestro, @Fecha)";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@CodSiniestro", codSiniestro),
                new SqlParameter("@Fecha", dia)
            };

            dal._686DPEscribir(consulta, parametros);
        }

        public void RegistrarCorrelacion(int npoliza, int codiSiniestro)
        {
            string consulta = @"
            INSERT INTO [dbo].[686DP_PolizaSiniestro] (DP686_NPoliza, CodSiniestro)
                VALUES (@NPoliza, @CodSiniestro);";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@NPoliza", npoliza),
                new SqlParameter("@CodSiniestro", codiSiniestro)
            };

            dal._686DPEscribir(consulta, parametros);
        }

        public int registrarSiniestro(_686DP_Siniestro siniestro)
        {
            string consulta = @"
            INSERT INTO [dbo].[686DP_Siniestro] 
                (Fecha, Valor, ValorDeReparacion, ValorDelBien, Estado, Descripcion)
            VALUES 
                (@Fecha, @Valor, @ValorDeReparar, @ValorDelBien, @Estado, @Descripcion);
            SELECT SCOPE_IDENTITY();";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@Fecha", siniestro.Fecha),
                new SqlParameter("@Valor",
                    siniestro.Valor == 0 ? (object)DBNull.Value : siniestro.Valor),
                new SqlParameter("@ValorDeReparar", siniestro.ValorDeReparar),
                new SqlParameter("@ValorDelBien", siniestro.ValorDelBien),
                new SqlParameter("@Estado", siniestro.Estado),
                new SqlParameter("@Descripcion", siniestro.descripcion)
            };

            return Convert.ToInt32(dal._686DPEscalar(consulta, parametros));
        }

        public object TraerDatosVista()
        {
            string consulta = "[dbo].[upEvaluarYActualizarSiniestros_686DP]";
            ArrayList parametros = new ArrayList(); 

            return dal._686DPConsultarSP(consulta, parametros);
        }

        public List<_686DP_Siniestro> TraerSiniestros()
        {
            string consulta = @"
             SELECT 
                CodSiniestro,
                Fecha,
                Valor,
                ValorDeReparacion,
                ValorDelBien,
                Estado,
                Descripcion
            FROM [dbo].[686DP_Siniestro]
            WHERE Estado = 1";

            ArrayList parametros = new ArrayList();

            DataTable dt = dal._686DPConsultar(consulta, parametros);

            List<_686DP_Siniestro> lista = new List<_686DP_Siniestro>();

            foreach (DataRow row in dt.Rows)
            {
                _686DP_Siniestro siniestro = new _686DP_Siniestro
                (
                   Convert.ToDateTime(row["Fecha"]), Convert.ToDouble(row["ValorDeReparacion"]), Convert.ToDouble(row["ValorDelBien"]), Convert.ToBoolean(row["Estado"]), row["Descripcion"].ToString()
                );

                lista.Add(siniestro);
            }

            return lista;
        }

        public object traerSiniestrosMayoresA5()
        {
            string consulta = @"
            SELECT 
            c.DP686_DNI,
            c.DP686_Nombre,
            c.DP686_Apellido,
            p.DP686_NPoliza,
            p.DP686_valorTotal AS 'Couta Mensual',
             COUNT(s.CodSiniestro) AS 'Cantidad de siniestros'

            FROM [dbo].[686DP_Siniestro] AS s
            INNER JOIN [dbo].[686DP_PolizaSiniestro] AS ps
	            ON ps.CodSiniestro = s.CodSiniestro
            INNER JOIN [dbo].[686DP_Poliza] AS p
	            ON p.DP686_NPoliza = ps.DP686_NPoliza
            INNER JOIN [dbo].[686DPClientePoliza] AS cp
	            ON p.DP686_NPoliza = cp.DP686_NPoliza
            INNER JOIN [686DP_Cliente].[686DP_Clientes] AS c
	            ON c.DP686_DNI = cp.DP686_DNICliente

            GROUP BY
            c.DP686_DNI,
            c.DP686_Nombre,
            c.DP686_Apellido,
            p.DP686_NPoliza,
            p.DP686_valorTotal

            HAVING COUNT(s.CodSiniestro) >= 5
            ";
            return dal._686DPConsultar(consulta, null);
        }

        public object TraerSiniestrosFiltrado(
            int? cantidadMinima,
            double? cuotaDesde,
            double? cuotaHasta,
            int? dni)
                {
            ArrayList parametros = new ArrayList();

            string query = @"
                            SELECT 
                                c.DP686_DNI,
                                c.DP686_Nombre,
                                c.DP686_Apellido,
                                p.DP686_NPoliza,
                                p.DP686_valorTotal AS 'Cuota Mensual',
                                COUNT(s.CodSiniestro) AS 'Cantidad de siniestros'
                            FROM [dbo].[686DP_Siniestro] AS s
                                INNER JOIN [dbo].[686DP_PolizaSiniestro] AS ps
                                    ON ps.CodSiniestro = s.CodSiniestro
                                INNER JOIN [dbo].[686DP_Poliza] AS p
                                    ON p.DP686_NPoliza = ps.DP686_NPoliza
                                INNER JOIN [dbo].[686DPClientePoliza] AS cp
                                    ON p.DP686_NPoliza = cp.DP686_NPoliza
                                INNER JOIN [686DP_Cliente].[686DP_Clientes] AS c
                                    ON c.DP686_DNI = cp.DP686_DNICliente
                            WHERE 1=1
                            ";
            if (dni.HasValue)
            {
                query += " AND c.DP686_DNI = @DNI";
                parametros.Add(new SqlParameter("@DNI", dni.Value));
            }

            if (cuotaDesde.HasValue)
            {
                query += " AND p.DP686_valorTotal >= @CuotaDesde";
                parametros.Add(new SqlParameter("@CuotaDesde", cuotaDesde.Value));
            }

            if (cuotaHasta.HasValue)
            {
                query += " AND p.DP686_valorTotal <= @CuotaHasta";
                parametros.Add(new SqlParameter("@CuotaHasta", cuotaHasta.Value));
            }

            query += @"
                    GROUP BY
                        c.DP686_DNI,
                        c.DP686_Nombre,
                        c.DP686_Apellido,
                        p.DP686_NPoliza,
                        p.DP686_valorTotal
                    ";

            if (cantidadMinima.HasValue)
            {
                query += " HAVING COUNT(s.CodSiniestro) >= @CantMin";
                parametros.Add(new SqlParameter("@CantMin", cantidadMinima.Value));
            }

            return dal._686DPConsultar(query, parametros);
        }

        public void CambiarCuota(int poliza, double nuevaCuota)
        {
            string query = @"
                UPDATE [dbo].[686DP_Poliza]
                SET DP686_valorTotal = @NuevaCuota
                WHERE DP686_NPoliza = @Poliza;
            ";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@NuevaCuota", nuevaCuota),
                new SqlParameter("@Poliza", poliza)
            };

            dal._686DPEscribir(query, parametros);
        }

    }
}
