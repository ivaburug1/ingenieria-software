using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BE_391IAU;

namespace DAL_391IAU
{
    public static class DAL
    {
        private static readonly string connectionString = @"Server=DESKTOP-06GODJN\MSSQLSERVER01;Database=A_StageLink;Integrated Security=True;";

        public static bool InsertarUsuario(BEUsuario usuario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    string queryUsuario = @"INSERT INTO Usuario_391IAU
                (DNI_391IAU, Nombre_391IAU, Apellido_391IAU, eMail_391IAU, Contraseña_391IAU, Idioma_391IAU, Activo_391IAU, Bloqueado_391IAU, Intentos_391IAU)
                VALUES
                (@DNI, @Nombre, @Apellido, @Email, @Contrasenia, @Idioma, @Activo, @Bloqueado, @Intentos)";

                    using (SqlCommand cmd = new SqlCommand(queryUsuario, con, tran))
                    {
                        cmd.Parameters.AddWithValue("@DNI", usuario.DNI_391IAU);
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre_391IAU);
                        cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido_391IAU);
                        cmd.Parameters.AddWithValue("@Email", usuario.eMail_391IAU ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Contrasenia", usuario.Contraseña_391IAU);
                        cmd.Parameters.AddWithValue("@Idioma", usuario.Idioma_391IAU);
                        cmd.Parameters.AddWithValue("@Activo", usuario.Activo_391IAU);
                        cmd.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado_391IAU);
                        cmd.Parameters.AddWithValue("@Intentos", usuario.Intentos_391IAU);
                        cmd.ExecuteNonQuery();
                    }
                    InsertarContraseñaHistorica(con, tran, usuario.DNI_391IAU, usuario.Contraseña_391IAU);
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public static BEUsuario ObtenerUsuarioPorDNI(int dni)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Usuario_391IAU WHERE DNI_391IAU = @DNI";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@DNI", dni);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new BEUsuario
                        {
                            DNI_391IAU = Convert.ToInt32(reader["DNI_391IAU"]),
                            Nombre_391IAU = reader["Nombre_391IAU"].ToString(),
                            Apellido_391IAU = reader["Apellido_391IAU"].ToString(),
                            eMail_391IAU = reader["eMail_391IAU"].ToString(),
                            Contraseña_391IAU = reader["Contraseña_391IAU"].ToString(),
                            Idioma_391IAU = reader["Idioma_391IAU"].ToString(),
                            Activo_391IAU = Convert.ToBoolean(reader["Activo_391IAU"]),
                            Bloqueado_391IAU = Convert.ToBoolean(reader["Bloqueado_391IAU"]),
                            Intentos_391IAU = Convert.ToInt32(reader["Intentos_391IAU"])
                        };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por DNI: " + ex.Message);
            }
        }
        private static void InsertarContraseñaHistorica(SqlConnection con, SqlTransaction tran, int dni, string contraseñaHash)
        {
            string query = "INSERT INTO Contraseñas_391IAU (DNI_391IAU, Contraseñas_391IAU) VALUES (@DNI, @Pass)";

            using (SqlCommand cmd = new SqlCommand(query, con, tran))
            {
                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@Pass", contraseñaHash);
                cmd.ExecuteNonQuery();
            }
        }
        public static void GuardarContraseñaHistorica(int dni, string contraseñaHash, SqlConnection con = null, SqlTransaction tran = null)
        {
            bool abrirConexion = false;

            if (con == null)
            {
                con = new SqlConnection(connectionString);
                abrirConexion = true;
            }

            if (abrirConexion)
                con.Open();

            string query = "INSERT INTO Contraseñas_391IAU (DNI_391IAU, Contraseñas_391IAU) VALUES (@DNI, @Pass)";
            using (SqlCommand cmd = new SqlCommand(query, con, tran))
            {
                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@Pass", contraseñaHash);
                cmd.ExecuteNonQuery();
            }

            if (abrirConexion)
                con.Close();
        }
        public static bool ContraseñaFueUsada(int dni, string hash)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM Contraseñas_391IAU
                         WHERE DNI_391IAU = @DNI AND Contraseñas_391IAU = @Hash";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@Hash", hash);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public static void ActualizarContraseña(int dni, string hash)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuario_391IAU
                         SET Contraseña_391IAU = @Hash
                         WHERE DNI_391IAU = @DNI";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Hash", hash);
                cmd.Parameters.AddWithValue("@DNI", dni);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void GuardarContraseñaHistorial(int dni, string contraseñaEncriptada)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Contraseñas_391IAU (DNI_391IAU, Contraseñas_391IAU)
                         VALUES (@DNI, @Contrasenia)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@Contrasenia", contraseñaEncriptada);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static bool ActualizarIntentos(int dni, int intentos)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuario_391IAU 
                         SET Intentos_391IAU = @Intentos 
                         WHERE DNI_391IAU = @DNI";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Intentos", intentos);
                cmd.Parameters.AddWithValue("@DNI", dni);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public static void BloquearUsuario(int dni)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuario_391IAU 
                         SET Bloqueado_391IAU = 1 
                         WHERE DNI_391IAU = @DNI";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", dni);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static int ObtenerIntentosActuales(int dni)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT Intentos_391IAU FROM Usuario_391IAU WHERE DNI_391IAU = @DNI";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DNI", dni);

                con.Open();
                object result = cmd.ExecuteScalar();
                return (result != null) ? Convert.ToInt32(result) : 0;
            }
        }
        public static void ActualizarUsuario(BEUsuario usuario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuario_391IAU
                         SET Intentos_391IAU = @Intentos,
                             Bloqueado_391IAU = @Bloqueado
                         WHERE DNI_391IAU = @DNI";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Intentos", usuario.Intentos_391IAU);
                cmd.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado_391IAU);
                cmd.Parameters.AddWithValue("@DNI", usuario.DNI_391IAU);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static List<BEUsuario> ObtenerTodosLosUsuarios()
        {
            List<BEUsuario> lista = new List<BEUsuario>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT DNI_391IAU, Nombre_391IAU, Apellido_391IAU, eMail_391IAU, 
                                Activo_391IAU, Bloqueado_391IAU, Intentos_391IAU, 
                                Idioma_391IAU, Contraseña_391IAU
                                FROM Usuario_391IAU"
                ;

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BEUsuario u = new BEUsuario
                        {
                            DNI_391IAU = reader.GetInt32(0),
                            Nombre_391IAU = reader.GetString(1),
                            Apellido_391IAU = reader.GetString(2),
                            eMail_391IAU = reader.GetString(3),
                            Activo_391IAU = reader.GetBoolean(4),
                            Bloqueado_391IAU = reader.GetBoolean(5),
                            Intentos_391IAU = reader.GetInt32(6),
                            Idioma_391IAU = reader.GetString(7),
                            Contraseña_391IAU = reader.GetString(8)
                        };
                        lista.Add(u);
                    }
                }
            }
            return lista;
        }
        public static bool ModificarUsuario(BEUsuario u)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuario_391IAU
                         SET Nombre_391IAU = @Nombre,
                             Apellido_391IAU = @Apellido,
                             eMail_391IAU = @Email,
                             Idioma_391IAU = @Idioma,
                             Activo_391IAU = @Activo,
                             Bloqueado_391IAU = @Bloqueado,
                             Intentos_391IAU = @Intentos
                         WHERE DNI_391IAU = @DNI";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", u.Nombre_391IAU);
                cmd.Parameters.AddWithValue("@Apellido", u.Apellido_391IAU);
                cmd.Parameters.AddWithValue("@Email", u.eMail_391IAU);
                cmd.Parameters.AddWithValue("@Idioma", u.Idioma_391IAU);
                cmd.Parameters.AddWithValue("@DNI", u.DNI_391IAU);
                cmd.Parameters.AddWithValue("@Activo", u.Activo_391IAU);
                cmd.Parameters.AddWithValue("@Bloqueado", u.Bloqueado_391IAU);
                cmd.Parameters.AddWithValue("@Intentos", u.Intentos_391IAU);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}