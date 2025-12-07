using _686DP_BE;
using _686DP_Dal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_MPP
{
    public class _686DPMPPCliente_C
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();

        public void ActualizarClienteC(_686DPCliente_C duplicadoActivo)
        {
            try
            {
                string consulta = @"
                    UPDATE [686DP_Cliente].[686DP_Clienctes_C]
                    SET 
                        DP686_Activo = @Activo
                    WHERE ID = @ID";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@Activo", duplicadoActivo.DP686_Activo),
                    new SqlParameter("@ID", duplicadoActivo.ID)
                };

                dal._686DPEscribir(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente en 686DP_Cliente_C: " + ex.Message);
            }
        }

        public List<_686DPCliente_C> TraerCambios()
        {
            const string consulta = @"
            SELECT 
                ID,
                DP686_Estado,
                DP686_DNI,
                DP686_Nombre,
                DP686_Apellido,
                DP686_Email,
                DP686_Domicilio,
                DP686DP_CodigoPostal,
                DP686_Fecha,
                DP686_Activo
            FROM [686DP_Cliente].[686DP_Clienctes_C]
            ORDER BY DP686_Fecha DESC";

            var dt = dal._686DPConsultar(consulta, new ArrayList());
            var lista = new List<_686DPCliente_C>();

            foreach (DataRow fila in dt.Rows)
            {
                int ToInt(object v) { return v == DBNull.Value ? 0 : Convert.ToInt32(v); }
                bool ToBool(object v) { return v == DBNull.Value ? false : Convert.ToBoolean(v); }
                DateTime ToDate(object v) { return v == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(v); }
                string ToStr(object v) { return v == DBNull.Value ? string.Empty : v.ToString(); }

                var cliente = new _686DPCliente_C(
                    ToInt(fila["ID"]),
                    ToBool(fila["DP686_Estado"]),
                    ToInt(fila["DP686_DNI"]),
                    ToStr(fila["DP686_Nombre"]),
                    ToStr(fila["DP686_Apellido"]),
                    ToStr(fila["DP686_Email"]),
                    ToStr(fila["DP686_Domicilio"]),
                    ToInt(fila["DP686DP_CodigoPostal"]),
                    ToDate(fila["DP686_Fecha"]),
                    ToBool(fila["DP686_Activo"])
                );

                lista.Add(cliente);
            }

            return lista;
        }
    }
}
