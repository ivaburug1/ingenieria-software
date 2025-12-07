using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using _686DP_BE;
using _686DP_Dal;

namespace _686DP_MPP
{
    public class _686DPMPPSeguro
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();

        public void CrearProducto(string nProducto)
        {
            string consulta = "INSERT INTO [dbo].[686DP_Seguro] (DP686_ProductoNombre)   VALUES (@ProductoNombre);";
            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@ProductoNombre", nProducto)
            };

            dal._686DPEscribir(consulta, parametros);
        }

        public int ObtenerCodSeguroPorProducto(string producto)
        {

            string consulta = @"
            SELECT DP686_CodSeguro
            FROM [dbo].[686DP_Seguro]
            WHERE DP686_ProductoNombre = @ProductoNombre";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@ProductoNombre", producto)
            };

            object resultado = dal._686DPEscalar(consulta, parametros);
            return Convert.ToInt32(resultado);
        }

        public List<_686DP_Seguro> Top3Productos()
        {
            string consulta = @"
            SELECT TOP 3 
                S.[DP686_CodSeguro],
                S.[DP686_ProductoNombre],
                COUNT(*) AS Cantidad
            FROM [dbo].[686DP_Poliza] AS P
            JOIN [dbo].[686DP_Seguro] AS S ON P.DP686_CodSeguro = S.DP686_CodSeguro
            GROUP BY S.DP686_CodSeguro, S.DP686_ProductoNombre
            ORDER BY Cantidad DESC";

            ArrayList parametros = new ArrayList(); // No se usan en este query

            DataTable tabla = dal._686DPConsultar(consulta, parametros);
            List<_686DP_Seguro> lista = new List<_686DP_Seguro>();

            foreach (DataRow fila in tabla.Rows)
            {
                _686DP_Seguro seguro = new _686DP_Seguro (fila["DP686_ProductoNombre"].ToString());
                seguro.cantidadVendida = Convert.ToInt32(fila["Cantidad"]);
                lista.Add(seguro);
            }

            return lista;
        }

        public _686DP_Seguro TraerDatosSeguro(int dP686_CodSeguro)
        {

            string consulta = "SELECT [DP686_CodSeguro]\r\n      ,[DP686_ProductoNombre]\r\n FROM [dbo].[686DP_Seguro] WHERE DP686_CodSeguro = @CodSeguro";
            ArrayList parametros = new ArrayList { new SqlParameter("@CodSeguro", dP686_CodSeguro) };

            DataTable dt = dal._686DPConsultar(consulta, parametros);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            string productoNombre = row["DP686_ProductoNombre"].ToString();
            return new _686DP_Seguro(productoNombre);
        }

        public _686DP_Plan TraerPlan(int dP686_CodPlan)
        {
            string consulta = "SELECT [DP686_CodigoPlan]\r\n      ,[DP686_Franquicia]\r\n      ,[DP686_Prima]\r\n FROM [dbo].[686DP_Plan] WHERE DP686_CodigoPlan = @CodPlan";
            ArrayList parametros = new ArrayList { new SqlParameter("@CodPlan", dP686_CodPlan) };

            DataTable dt = dal._686DPConsultar(consulta, parametros);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            decimal franquicia = Convert.ToDecimal(row["DP686_Franquicia"]);
            decimal prima = Convert.ToDecimal(row["DP686_Prima"]);
            return new _686DP_Plan(dP686_CodPlan, franquicia, prima);
        }

        public List<string> TraerProductos()
        {
            List<string> productos = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                string consulta = "SELECT [DP686_CodSeguro]\r\n      ,[DP686_ProductoNombre]\r\n FROM [dbo].[686DP_Seguro]";

                dt = dal._686DPConsultar(consulta, null);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        productos.Add(item[1].ToString());
                    }
                }

                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar el Producto.");
            }
        }

        public bool ValidarProducto(string nProducto)
        {
            try
            {
                DataTable dt = new DataTable();
                bool existe = false;
                string consulta = "SELECT [DP686_CodSeguro]\r\n      ,[DP686_ProductoNombre]\r\n FROM [dbo].[686DP_Seguro]  WHERE DP686_ProductoNombre = @TipoProducto;";
                ArrayList parametros = new ArrayList { new SqlParameter("@TipoProducto", nProducto) };

                dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return existe = true;
                }

                return existe;
            }catch (Exception ex)
            {
                throw new Exception($"Error al buscar el Producto '{nProducto}'.");
            }
        }
        public bool YaExisteRelacionSeguroPlan(int codSeguro, int codigoPlan)
        {
            string consulta = @"
            SELECT COUNT(*) 
            FROM [dbo].[686DP_SeguroPlan]
            WHERE DP686_CodSeguro = @CodSeguro AND DP686_CodigoPlan = @CodigoPlan";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@CodSeguro", codSeguro),
                new SqlParameter("@CodigoPlan", codigoPlan)
            };

            object resultado = dal._686DPEscalar(consulta, parametros);
            return Convert.ToInt32(resultado) > 0;
        }
    }
}
