using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace _686DP_Dal
{
    public class _686DPDalGeneral
    {

        private string cadenaConexion;
        
        public SqlConnection conn;
        public SqlCommand cmd;

        public _686DPDalGeneral()
        {
            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            string database = Environment.GetEnvironmentVariable("DB_DATABASE");
            string user = Environment.GetEnvironmentVariable("DB_USER");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            string auth = Environment.GetEnvironmentVariable("DB_AUTH");

            cadenaConexion = ConstruirConnectionString(server, database, user, password, auth);
            
            conn = new SqlConnection(cadenaConexion);
        }

        private string ConstruirConnectionString(string server, string database, string user, string password, string auth)
        {
            bool windowsAuth = !string.IsNullOrEmpty(auth) && auth == "1";

                return $"Data Source={server};Initial Catalog={database};Integrated Security=True;";
        }

        public DataTable _686DPConsultar(string consulta, ArrayList parametros)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    if (parametros != null)
                    {
                        foreach (SqlParameter dato in parametros)
                        {
                            cmd.Parameters.AddWithValue(dato.ParameterName, dato.Value ?? DBNull.Value);
                        }
                    }

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                    {
                        DA.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL: Hubo un error al realizar la consulta");
            }
            catch (Exception ex)
            {
                throw new Exception("🛑 Error inesperado en la operación de consulta: " + ex.Message, ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return dt;
        }

        public DataTable _686DPConsultarSP(string nombreSP, ArrayList parametros)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand(nombreSP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parametros != null)
                    {
                        foreach (SqlParameter dato in parametros)
                        {
                            cmd.Parameters.AddWithValue(dato.ParameterName, dato.Value ?? DBNull.Value);
                        }
                    }

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                    {
                        DA.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("⚠️ Error SQL al ejecutar Stored Procedure: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("🛑 Error general al ejecutar Stored Procedure: " + ex.Message, ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return dt;
        }

        public void _686DPEjecutar(string nombreSP, ArrayList parametros)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(nombreSP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parametros != null)
                    {
                        foreach (SqlParameter dato in parametros)
                        {
                            cmd.Parameters.AddWithValue(dato.ParameterName, dato.Value ?? DBNull.Value);
                        }
                    }

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("⚠️ Error SQL al ejecutar SP: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("🛑 Error general al ejecutar el SP: " + ex.Message, ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public object _686DPEscalar(string consulta, ArrayList parametros)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    if (parametros != null)
                    {
                        foreach (SqlParameter dato in parametros)
                        {
                            cmd.Parameters.AddWithValue(dato.ParameterName, dato.Value ?? DBNull.Value);
                        }
                    }

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    return cmd.ExecuteScalar(); 
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("⚠️ Error SQL (escalar): " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("🛑 Error general en ExecuteScalar: " + ex.Message, ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void _686DPEscribir(string consulta, ArrayList parametros)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    if (parametros != null)
                    {
                        foreach (SqlParameter dato in parametros)
                        {
                            cmd.Parameters.AddWithValue(dato.ParameterName, dato.Value ?? DBNull.Value);
                        }
                    }

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("⚠️ Error SQL: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("🛑 Error general al ejecutar la instrucción SQL: " + ex.Message, ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

    }
}
