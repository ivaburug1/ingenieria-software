using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using _686DP_Dal;
using _686DP_MPP;
using _686DP_BE;

namespace _686DP_MPP
{
    public class _686MPPClientes
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();
        public void CrearCliente(_686DP_Cliente cliente)
        {
            try
            {
                string consulta = "INSERT INTO [686DP_Cliente].[686DP_Clientes] (\r\n    DP686_DNI,\r\n    DP686_Nombre,\r\n    DP686_Apellido\r\n)\r\nVALUES (\r\n    @DNI,\r\n    @Nombre,\r\n    @Apellido\r\n)";
                ArrayList parametros = new ArrayList
                {
                     new SqlParameter("@DNI", cliente.DP686_DNI),
                     new SqlParameter("@Nombre", cliente.DP686_Nombre),
                     new SqlParameter("@Apellido", cliente.DP686_Apellido)
                };
                dal._686DPEscribir(consulta,parametros);
            }
            catch(Exception ex) 
            {
                throw new Exception("Error al insertar el cliente en la base de datos: " + ex.Message, ex);
            }
        }

        public void CrearCompleto(_686DP_Cliente cliente)
        {
            string consulta = @"
            INSERT INTO [686DP_Cliente].[686DP_Clientes] (
            DP686_DNI,
            DP686_Nombre,
            DP686_Apellido,
            DP686_Email,
            DP686_Domicilio,
            DP686DP_CodigoPostal
            )
            VALUES (
            @DNI,
            @Nombre,
            @Apellido,
            @Email,
            @Domicilio,
            @CodigoPostal
            );";

            ArrayList parametros = new ArrayList
            {  
            new SqlParameter("@DNI", cliente.DP686_DNI),
            new SqlParameter("@Nombre", cliente.DP686_Nombre),
            new SqlParameter("@Apellido", cliente.DP686_Apellido),
            new SqlParameter("@Email", cliente.DP686_Email),
            new SqlParameter("@Domicilio", cliente.DP686_Domicilio),
            new SqlParameter("@CodigoPostal", cliente.DP686DP_CodigoPostal)
            };
            dal._686DPEscribir(consulta, parametros);
        }

        public void EliminadoLogico(int dNi)
        {

            try
            {
                string consulta = "UPDATE [686DP_Cliente].[686DP_Clientes] SET DP686_Estado = 0 WHERE DP686_DNI = @DNI";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@DNI", dNi)
                };

                _686DPDalGeneral acceso = new _686DPDalGeneral();
                acceso._686DPEscribir(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar el eliminado lógico del cliente: " + ex.Message);
            }
        }

        public void GrabarCliente(_686DP_Cliente cliente)
        {
            try
            {
                string storedProcedure = "[686DP_Cliente].[spInsertarCliente_686DP]";
                ArrayList parametros = new ArrayList();

                parametros.Add(new SqlParameter("@DNI", cliente.DP686_DNI));
                parametros.Add(new SqlParameter("@Nombre", cliente.DP686_Nombre));
                parametros.Add(new SqlParameter("@Apellido", cliente.DP686_Apellido));
                parametros.Add(new SqlParameter("@Email",cliente.DP686_Email));
                parametros.Add(new SqlParameter("@Domicilio", cliente.DP686_Domicilio));
                parametros.Add(new SqlParameter("@CodigoPostal", cliente.DP686DP_CodigoPostal));
                //parametros.Add(new SqlParameter("@Estado", cliente.DP686_Estado == null ? DBNull.Value : (object)cliente.DP686_Estado));
                

                dal._686DPEjecutar(storedProcedure, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al grabar cliente: " + ex.Message);
            }
        }

        public void ReemplazarCliente(_686DPCliente_C seleccionado)
        {
            try
            {
                string consulta = @"
                    UPDATE [686DP_Cliente].[686DP_Clientes]
                    SET 
                        DP686_Estado = @Estado,
                        DP686_Nombre = @Nombre,
                        DP686_Apellido = @Apellido,
                        DP686_Email = @Email,
                        DP686_Domicilio = @Domicilio,
                        DP686DP_CodigoPostal = @CodigoPostal
                    WHERE DP686_DNI = @DNI";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@Estado", seleccionado.DP686_Estado),
                    new SqlParameter("@Nombre", seleccionado.DP686_Nombre),
                    new SqlParameter("@Apellido", seleccionado.DP686_Apellido),
                    new SqlParameter("@Email", seleccionado.DP686_Email),
                    new SqlParameter("@Domicilio", seleccionado.DP686_Domicilio),
                    new SqlParameter("@CodigoPostal", seleccionado.DP686DP_CodigoPostal),
                    new SqlParameter("@DNI", seleccionado.DP686_DNI)
                };

                dal._686DPEscribir(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reemplazar el cliente en 686DP_Clientes: " + ex.Message);
            }
        }

        public _686DP_Cliente TraerCliente(int dNI)
        {
            try
            {
                string consulta = @"
                SELECT [DP686_DNI]
                      ,[DP686_Nombre]
                      ,[DP686_Apellido]
                      ,[DP686_Email]
                      ,[DP686_Domicilio]
                      ,[DP686DP_CodigoPostal]
                      ,[DP686_Estado]
                  FROM [DBAseguraYADemo].[686DP_Cliente].[686DP_Clientes]

                WHERE DP686_DNI = @DNI";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@DNI", dNI)
                };

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count == 0)
                    return null;

                DataRow row = dt.Rows[0];

                _686DP_Cliente cliente = new _686DP_Cliente(dNI, row["DP686_Nombre"].ToString(), row["DP686_Apellido"].ToString());

                if (row["DP686_Email"] != DBNull.Value)
                    cliente.DP686_Email = row["DP686_Email"].ToString();

                if (row["DP686_Domicilio"] != DBNull.Value)
                    cliente.DP686_Domicilio = row["DP686_Domicilio"].ToString();

                if (row["DP686DP_CodigoPostal"] != DBNull.Value)
                    cliente.DP686DP_CodigoPostal = Convert.ToInt32(row["DP686DP_CodigoPostal"]);

                if (row["DP686_Estado"] != DBNull.Value)
                    cliente.DP686_Estado = Convert.ToBoolean(row["DP686_Estado"]);

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer cliente por DNI: " + ex.Message, ex);
            }
        }

        public _686DP_Cliente TraerClientePoliza(int dP686_NPoliza)
        {
            string query = @"SELECT C.[DP686_DNI]
                ,C.[DP686_Nombre]
                ,C.[DP686_Apellido]
                ,C.[DP686_Email]
                ,C.[DP686_Domicilio]
                ,C.[DP686DP_CodigoPostal]
                ,C.[DP686_Estado]
            FROM [DBAseguraYADemo].[686DP_Cliente].[686DP_Clientes] AS C

            INNER JOIN [dbo].[686DPClientePoliza] AS CP ON C.DP686_DNI = CP.DP686_DNICliente
            WHERE CP.DP686_NPoliza  = @Poliza";

            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@Poliza", dP686_NPoliza)
            };

            DataTable dt = dal._686DPConsultar(query, parametros);
            if (dt.Rows.Count > 0)
            {
                _686DP_Cliente cliente = new _686DP_Cliente (Convert.ToInt32(dt.Rows[0]["DP686_DNI"]), dt.Rows[0]["DP686_Nombre"].ToString(), dt.Rows[0]["DP686_Apellido"].ToString());
                cliente.DP686_Email = dt.Rows[0]["DP686_Email"].ToString();
                cliente.DP686_Domicilio = dt.Rows[0]["DP686_Domicilio"].ToString();
                cliente.DP686DP_CodigoPostal = Convert.ToInt32(dt.Rows[0]["DP686DP_CodigoPostal"]);
                cliente.DP686_Estado = Convert.ToBoolean(dt.Rows[0]["DP686_Estado"]);
                return cliente;
            }
            else
            {
                throw new Exception("No se encontró cliente para la póliza seleccionada.");
            }
        }


        public List<_686DP_Cliente> TraerClientes()
        {
            List<_686DP_Cliente> lista = new List<_686DP_Cliente>();
            string consulta = "SELECT [DP686_DNI]\r\n      ,[DP686_Nombre]\r\n      ,[DP686_Apellido]\r\n      ,[DP686_Email]\r\n      ,[DP686_Domicilio]\r\n      ,[DP686DP_CodigoPostal]\r\n      ,[DP686_Estado]\r\n  FROM [686DP_Cliente].[686DP_Clientes]\r\n";

            try
            {
                ArrayList parametros = new ArrayList();
                _686DPDalGeneral acceso = new _686DPDalGeneral();
                DataTable tabla = acceso._686DPConsultar(consulta, parametros);

                foreach (DataRow row in tabla.Rows)
                {
                    _686DP_Cliente c = new _686DP_Cliente(
                        Convert.ToInt32(row["DP686_DNI"]),
                        row["DP686_Nombre"]?.ToString(),
                        row["DP686_Apellido"]?.ToString()
                    )
                    {
                        DP686_Email = row["DP686_Email"] == DBNull.Value ? null : row["DP686_Email"].ToString(),
                        DP686_Domicilio = row["DP686_Domicilio"] == DBNull.Value ? null : row["DP686_Domicilio"].ToString(),
                        DP686DP_CodigoPostal = row["DP686DP_CodigoPostal"] == DBNull.Value ? 0 : Convert.ToInt32(row["DP686DP_CodigoPostal"]),
                        DP686_Estado = row["DP686_Estado"] != DBNull.Value && Convert.ToBoolean(row["DP686_Estado"]),
                        
                    };
                    lista.Add(c);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer clientes: " + ex.Message);
            }
        }

        public bool ValidarNuevo(int dni)
        {
            bool existe = false;
            try
            {
                DataTable dt;
                string consulta = "SElECT [DP686_DNI] from [686DP_Cliente].[686DP_Clientes] where [DP686_DNI] = @DNI";
                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", dni) };

                dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return existe = true;
                }

                return existe;
            }
            catch (Exception)
            {
                throw new Exception($"Error al buscar el nombre de usuario '{dni}'.");
            }
        }

    }
}
