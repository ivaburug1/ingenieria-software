using _686DP_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_Dal;
using System.Collections;
using System.Data.SqlClient;

namespace _686DP_MPP
{
    public class _686DP_MPPCobertura
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();
        public List<_686DP_Cobertura> TraerCoberturas()
        {
            List<_686DP_Cobertura> coberturas = new List<_686DP_Cobertura>();
            try
            {
                DataTable dt = new DataTable();
                string consulta = "SELECT [DP686_Descripcion]\r\n      ,[DP686_SumaAsegurada]\r\n      ,[CodigoCobertura]\r\n  FROM [DBAseguraYADemo].[dbo].[686DP_Cobertura]";

                dt = dal._686DPConsultar(consulta, null);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string Descripcion = item[0].ToString();
                        decimal Suma_asegurada = Convert.ToDecimal(item[1].ToString());
                        int codCObertura = Convert.ToInt32(item[2].ToString());
                        _686DP_Cobertura cobertura = new _686DP_Cobertura(codCObertura, Descripcion, Suma_asegurada);
                        coberturas.Add(cobertura);
                    }
                }
                return coberturas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar las coberturas." + ex.Message);
            }
        }

        public List<_686DP_Cobertura> TraerCoberturasFiltrado(int codigoPlan)
        {

            List<_686DP_Cobertura> coberturas = new List<_686DP_Cobertura>();
            try
            {
                DataTable dt = new DataTable();
                string consulta = "SELECT C.[DP686_Descripcion]\r\n      ,C.[DP686_SumaAsegurada]\r\n      ,C.[CodigoCobertura]\r\nFROM [dbo].[686DP_Cobertura] C\r\nINNER JOIN [dbo].[686DP_PlanesCoberturas] PC ON C.CodigoCobertura = PC.CodigoCobertura\r\nWHERE PC.DP686_CodigoPlan = @CodigoPlan;\r\n";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@CodigoPlan", codigoPlan)
                };

                dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string Descripcion = item[0].ToString();
                        decimal Suma_asegurada = Convert.ToDecimal(item[1].ToString());
                        int codCObertura = Convert.ToInt32(item[2].ToString());
                        _686DP_Cobertura cobertura = new _686DP_Cobertura(codCObertura, Descripcion, Suma_asegurada);
                        coberturas.Add(cobertura);
                    }
                }
                return coberturas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar las coberturas." + ex.Message);
            }
        }

        public bool ExisteCoberturaEnPlan(int codPlan, string descripcion, decimal suma)
        {
            string consulta = @"
            SELECT COUNT(*) 
            FROM [dbo].[686DP_Cobertura] C
            INNER JOIN [dbo].[686DP_PlanesCoberturas] PC ON C.CodigoCobertura = PC.CodigoCobertura
            WHERE PC.DP686_CodigoPlan = @CodigoPlan
            AND C.DP686_Descripcion = @Descripcion
            AND C.DP686_SumaAsegurada = @Suma;";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@CodigoPlan", codPlan),
                new SqlParameter("@Descripcion", descripcion),
                new SqlParameter("@Suma", suma)
            };

            object resultado = dal._686DPEscalar(consulta, parametros);
            return Convert.ToInt32(resultado) > 0;
        }

        public int CrearCobertura(string descripcion, decimal suma)
        {
            string buscar = @"
            SELECT CodigoCobertura
            FROM [dbo].[686DP_Cobertura]
            WHERE DP686_Descripcion = @Descripcion AND DP686_SumaAsegurada = @Suma;";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@Descripcion", descripcion),
                new SqlParameter("@Suma", suma)
            };

            object resultado = dal._686DPEscalar(buscar, parametros);

            if (resultado != null && resultado != DBNull.Value)
                return Convert.ToInt32(resultado);


            string insertar = @"
            INSERT INTO [dbo].[686DP_Cobertura] (DP686_Descripcion, DP686_SumaAsegurada)
            VALUES (@Descripcion, @Suma);
            SELECT SCOPE_IDENTITY();";

            return Convert.ToInt32(dal._686DPEscalar(insertar, parametros));
        }

    }
}
