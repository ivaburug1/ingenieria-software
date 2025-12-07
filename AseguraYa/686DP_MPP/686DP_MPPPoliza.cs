using _686DP_BE;
using _686DP_Dal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_MPP
{
    public class _686DP_MPPPoliza
    {

        _686DPDalGeneral dal = new _686DPDalGeneral();

        public void AsociarClientePoliza(int dNI, int numeroPoliza)
        {
            try
            {
                string consulta = "INSERT INTO [dbo].[686DPClientePoliza] (DP686_NPoliza, DP686_DNICliente) VALUES (@NPoliza, @DNICliente)";

                ArrayList parametros = new ArrayList
                {
                new SqlParameter("@NPoliza", numeroPoliza),
                new SqlParameter("@DNICliente", dNI)
                };

                _686DPDalGeneral acceso = new _686DPDalGeneral();
                acceso._686DPEscribir(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar cliente con póliza: " + ex.Message);
            }
        }

        public bool BuscarPoliza(int numeroDePoliza)
        {
            bool existe = false;
            DataTable dt = new DataTable();
            string consulta = "SELECT [DP686_NPoliza]\r\n      ,[DP686_Estado]\r\n      ,[DP686_valorTotal]\r\n      ,[DP686_FechaVencimiento]\r\n      ,[DP686_Endoso]\r\n      ,[DP686_CodSeguro]\r\n      ,[DP686_CodPlan]\r\n FROM [dbo].[686DP_Poliza] WHERE [DP686_NPoliza] = @Poliza";
            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@Poliza", numeroDePoliza)
            };
            dt = dal._686DPConsultar(consulta, parametros);
            if(dt.Rows.Count > 0)
            {
                existe = true;
            }
            return existe;
        }

        public int CrearPoliza(bool estado, decimal valorfinal, DateTime fechaVencimiento, int endoso, int codSeguro, int codigoPlan)
        {
            try
            {
                string consulta = @"
                INSERT INTO [dbo].[686DP_Poliza]
                (DP686_Estado, DP686_valorTotal, DP686_FechaVencimiento, DP686_Endoso, DP686_CodSeguro, DP686_CodPlan)
                VALUES (@Estado, @ValorTotal, @FechaVencimiento, @Endoso, @CodSeguro, @CodPlan);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@Estado", estado),
                    new SqlParameter("@ValorTotal", valorfinal),
                    new SqlParameter("@FechaVencimiento", fechaVencimiento),
                    new SqlParameter("@Endoso", endoso),
                    new SqlParameter("@CodSeguro", codSeguro),
                    new SqlParameter("@CodPlan", codigoPlan)
                };


                object resultado = dal._686DPEscalar(consulta, parametros);
                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la póliza: " + ex.Message);
            }
        }

        public void eliminarPoliza(string motivo, int npoliza)
        {
            try
            {
                string actualizar = @"
                UPDATE [dbo].[686DP_Poliza]
                SET DP686_Estado = 0
                WHERE DP686_NPoliza = @NPoliza";

                ArrayList parametrosUpdate = new ArrayList
                {
                    new SqlParameter("@NPoliza", npoliza)
                };

                dal._686DPEscribir(actualizar, parametrosUpdate);

                string insertar = @"
                INSERT INTO [dbo].[686DPPolizaCancelacion] (DP686_NPoliza, Motivo)
                VALUES (@NPoliza, @Motivo)";

                ArrayList parametrosInsert = new ArrayList
                {
                    new SqlParameter("@NPoliza",npoliza),
                    new SqlParameter("@Motivo", motivo)
                };

                dal._686DPEscribir(insertar, parametrosInsert);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la póliza: " + ex.Message);
            }
        }

        public List<_686DP_Poliza> Filtrar(int? codpoliza, string seguro, decimal? prima, decimal? franquicia, bool? estado, DateTime? fecha)
        {
            ArrayList parametros = new ArrayList();

            string query = @"
            SELECT P.*
            FROM [dbo].[686DP_Poliza] AS P
            JOIN [dbo].[686DP_Seguro] AS S ON P.[DP686_CodSeguro] = S.[DP686_CodSeguro]
            JOIN [dbo].[686DP_Plan] AS PL ON P.[DP686_CodPlan] = PL.[DP686_CodigoPlan]
            WHERE 1=1";

            if (codpoliza.HasValue)
            {
                query += " AND P.[DP686_NPoliza] = @nPoliza";
                parametros.Add(new SqlParameter("@nPoliza", codpoliza.Value));
            }

            if (!string.IsNullOrEmpty(seguro))
            {
                query += " AND S.[DP686_ProductoNombre] = @seguro";
                parametros.Add(new SqlParameter("@seguro", seguro));
            }

            if (prima.HasValue)
            {
                query += " AND PL.[DP686_Prima] = @prima";
                parametros.Add(new SqlParameter("@prima", prima.Value));
            }

            if (franquicia.HasValue)
            {
                query += " AND PL.[DP686_Franquicia] = @franquicia";
                parametros.Add(new SqlParameter("@franquicia", franquicia.Value));
            }

            if (estado.HasValue)
            {
                query += " AND P.[DP686_Estado] = @estado";
                parametros.Add(new SqlParameter("@estado", estado.Value));
            }
            if (fecha.HasValue)
            {
                query += " AND CAST(P.[DP686_FechaVencimiento] AS DATE) = @fechaVencimiento";
                parametros.Add(new SqlParameter("@fechaVencimiento", fecha.Value.Date));
            }

            DataTable dt = dal._686DPConsultar(query, parametros);
            List<_686DP_Poliza> lista = new List<_686DP_Poliza>();

            foreach (DataRow row in dt.Rows)
            {
                var poliza = new _686DP_Poliza
                (
                    Convert.ToInt32(row["DP686_NPoliza"]),
                    Convert.ToBoolean(row["DP686_Estado"]),
                    Convert.ToDecimal(row["DP686_valorTotal"]),
                    Convert.ToDateTime(row["DP686_FechaVencimiento"]),
                    Convert.ToInt32(row["DP686_Endoso"]),
                    Convert.ToInt32(row["DP686_CodSeguro"]),
                    Convert.ToInt32(row["DP686_CodPlan"])
                );
                lista.Add(poliza);
            }

            return lista;
        }


        public void ModificarPoliza(_686DP_Poliza poliza)
        {

            try
            {
                string consulta = @"
                UPDATE [dbo].[686DP_Poliza]
                SET DP686_Estado = @Estado,
                DP686_valorTotal = @ValorTotal,
                DP686_FechaVencimiento = @FechaVencimiento,
                DP686_Endoso = @Endoso,
                DP686_CodSeguro = @CodSeguro,
                DP686_CodPlan = @CodPlan
                WHERE DP686_NPoliza = @NPoliza";

                ArrayList parametros = new ArrayList
                {
                new SqlParameter("@Estado", poliza.DP686_Estado),
                new SqlParameter("@ValorTotal", poliza.DP686_valorTotal),
                new SqlParameter("@FechaVencimiento", poliza.DP686_FechaVencimiento),
                new SqlParameter("@Endoso", poliza.DP686_Endoso),
                new SqlParameter("@CodSeguro", poliza.DP686_CodSeguro),
                new SqlParameter("@CodPlan", poliza.DP686_CodPlan),
                new SqlParameter("@NPoliza", poliza.DP686_NPoliza)
                };

                dal._686DPEscribir(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la póliza: " + ex.Message);
            }
        }

        public _686DP_Poliza TraerDatosPoliza(int numeroDePoliza)
        {
            _686DP_Poliza poliza = null;
            DataTable dt = new DataTable();
            string consulta = "SELECT [DP686_NPoliza]\r\n      ,[DP686_Estado]\r\n      ,[DP686_valorTotal]\r\n      ,[DP686_FechaVencimiento]\r\n      ,[DP686_Endoso]\r\n      ,[DP686_CodSeguro]\r\n      ,[DP686_CodPlan]\r\n  \r\n FROM [dbo].[686DP_Poliza] WHERE [DP686_NPoliza] = @Poliza";
            ArrayList parametros = new ArrayList
                {
                new SqlParameter("@Poliza", numeroDePoliza)
                };
            dt = dal._686DPConsultar(consulta, parametros);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                    poliza = new _686DP_Poliza(
                    nPoliza: Convert.ToInt32(row["DP686_NPoliza"]),
                    estado: Convert.ToBoolean(row["DP686_Estado"]),
                    valorTotal: Convert.ToDecimal(row["DP686_valorTotal"]),
                    fechaVencimiento: Convert.ToDateTime(row["DP686_FechaVencimiento"]),
                    endoso: Convert.ToInt32(row["DP686_Endoso"]),
                    codSeguro: Convert.ToInt32(row["DP686_CodSeguro"]),
                    codPlan: Convert.ToInt32(row["DP686_CodPlan"])
                );
            }
            return poliza;
        }

        public List<_686DP_Poliza> TraerPolizas()
        {
            List<_686DP_Poliza> lista = new List<_686DP_Poliza>();
            string consulta = "SELECT [DP686_NPoliza]\r\n      ,[DP686_Estado]\r\n      ,[DP686_valorTotal]\r\n      ,[DP686_FechaVencimiento]\r\n      ,[DP686_Endoso]\r\n      ,[DP686_CodSeguro]\r\n      ,[DP686_CodPlan]\r\n  FROM [dbo].[686DP_Poliza]";
            ArrayList parametros = new ArrayList();
            DataTable dt = dal._686DPConsultar(consulta, parametros);

            foreach (DataRow row in dt.Rows)
            {
                var poliza = new _686DP_Poliza(
                    nPoliza: Convert.ToInt32(row["DP686_NPoliza"]),
                    estado: Convert.ToBoolean(row["DP686_Estado"]),
                    valorTotal: Convert.ToDecimal(row["DP686_valorTotal"]),
                    fechaVencimiento: Convert.ToDateTime(row["DP686_FechaVencimiento"]),
                    endoso: Convert.ToInt32(row["DP686_Endoso"]),
                    codSeguro: Convert.ToInt32(row["DP686_CodSeguro"]),
                    codPlan: Convert.ToInt32(row["DP686_CodPlan"])
                );

                lista.Add(poliza);
            }

            return lista;
        }
    }
}
