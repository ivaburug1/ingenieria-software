using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BE_391IAU;
using System.Runtime.CompilerServices;


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

        public static int EjecutarInsert(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddRange(parametros);
                return cmd.ExecuteNonQuery();
            }
        }
        public static int EjecutarScalar(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parametros);
                    object result = cmd.ExecuteScalar();
                    return (result != null && int.TryParse(result.ToString(), out int valor)) ? valor : 0;
                }
            }
        }
        public static List<string> ObtenerNombresEventos()
        {
            List<string> nombres = new List<string>();
            string query = "SELECT DISTINCT NombreArtista_391IAU FROM Eventos_391IAU";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nombres.Add(reader.GetString(0));
                    }
                }
            }

            return nombres;
        }

        public static List<DateTime> ObtenerFechasPorArtista(string nombreArtista)
        {
            List<DateTime> fechas = new List<DateTime>();
            string query = "SELECT DISTINCT Fecha_391IAU FROM Eventos_391IAU WHERE NombreArtista_391IAU = @nombre";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreArtista);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fechas.Add(reader.GetDateTime(0));
                        }
                    }
                }
            }

            return fechas;
        }

        public static List<BESector> ObtenerSectoresPorEvento(string nombreArtista, DateTime fecha)
        {
            List<BESector> sectores = new List<BESector>();

            string query = @"SELECT DISTINCT S.CodigoDeSector_391IAU, S.NombreSector_391IAU, S.PrecioDeSector_391IAU
                                FROM Eventos_391IAU E
                                INNER JOIN Estadios_391IAU ES ON E.CodigoDeEstadio_391IAU = ES.CodigoDeEstadio_391IAU
                                INNER JOIN Sectores_391IAU S ON ES.CodigoDeEstadio_391IAU = S.CodigoDeEstadio_391IAU
                                WHERE E.NombreArtista_391IAU = @nombreArtista AND E.Fecha_391IAU = @fecha";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreArtista", nombreArtista);
                    cmd.Parameters.AddWithValue("@fecha", fecha);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codigo = reader.GetInt32(0);
                            string nombre = reader.GetString(1);
                            decimal precio = reader.GetDecimal(2);

                            sectores.Add(new BESector(codigo, nombre, precio));
                        }
                    }
                }
            }

            return sectores;
        }

        public static (string, string) ObtenerEstadioYDireccion(string nombreArtista)
        {
            string query = @"SELECT TOP 1 ES.NombreEstadio_391IAU, ES.DireccionEstadio_391IAU
                             FROM Eventos_391IAU E
                             INNER JOIN Estadios_391IAU ES ON E.CodigoDeEstadio_391IAU = ES.CodigoDeEstadio_391IAU
                             WHERE E.NombreArtista_391IAU = @nombreArtista";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreArtista", nombreArtista);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string estadio = reader.GetString(0);
                            string direccion = reader.GetString(1);
                            return (estadio, direccion);
                        }
                    }
                }
            }

            return ("", "");
        }
        public static decimal ObtenerPrecioSector(string nombreSector)
        {
            string query = @"SELECT PrecioDeSector_391IAU FROM Sectores_391IAU WHERE NombreSector_391IAU = @nombreSector";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreSector", nombreSector);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }
        public static int ObtenerCapacidadPorSector(string nombreSector)
        {
            string query = "SELECT Capacidad_391IAU FROM Sectores_391IAU WHERE NombreSector_391IAU = @nombre";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreSector);
                    object result = cmd.ExecuteScalar();
                    return (result != null && int.TryParse(result.ToString(), out int capacidad)) ? capacidad : 0;
                }
            }
        }

        public static int InsertarEntrada(BEBoleto boleto)
        {
            string query = @"INSERT INTO Entradas_391IAU (Sector_391IAU, Fecha_391IAU, Precio_391IAU)
                     VALUES (@Sector, @Fecha, @Precio);
                     SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Sector", boleto.Sector),
                new SqlParameter("@Fecha", boleto.FechaEvento),
                new SqlParameter("@Precio", boleto.Precio)
            };

            return EjecutarScalar(query, parameters);
        }

        public static bool InsertarClienteEntrada(int idCliente, int idEntrada)
        {
            string query = @"INSERT INTO ClientesEntradas_391IAU (IDCliente_391IAU, IDEntrada_391IAU)
                     VALUES (@Cliente, @Entrada);";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Cliente", idCliente),
                new SqlParameter("@Entrada", idEntrada)
            };

            return EjecutarInsert(query, parameters) > 0;
        }

        public static bool InsertarEventoCliente(int idEvento, int idCliente)
        {
            string query = @"INSERT INTO EventosClientes_391IAU (IDEvento_391IAU, IDCliente_391IAU)
                     VALUES (@Evento, @Cliente);";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Evento", idEvento),
                new SqlParameter("@Cliente", idCliente)
            };

            return EjecutarInsert(query, parameters) > 0;
        }

        public static bool GuardarEntradasYRelaciones(List<BEBoleto> lista)
        {
            bool exito = true;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaccion = conn.BeginTransaction();

                try
                {
                    foreach (BEBoleto boleto in lista)
                    {
                        SqlCommand cmdInsert = new SqlCommand(@"INSERT INTO Entradas_391IAU (CodigoDeEvento_391IAU, CodigoDeSector_391IAU, Precio_391IAU)
                                                                VALUES (@CodigoEvento, @CodigoSector, @Precio);
                                                                SELECT SCOPE_IDENTITY();", conn, transaccion);

                        cmdInsert.Parameters.AddWithValue("@CodigoEvento", boleto.CodigoDeEvento);
                        cmdInsert.Parameters.AddWithValue("@CodigoSector", boleto.Sector);
                        cmdInsert.Parameters.AddWithValue("@Precio", boleto.Precio);

                        int nuevoCodigoEntrada = Convert.ToInt32(cmdInsert.ExecuteScalar());

                        SqlCommand cmdClienteEntrada = new SqlCommand(@"INSERT INTO ClientesEntradas_391IAU (CodigoEntrada_391IAU, DNI_391IAU)
                                                                          VALUES (@CodigoEntrada, @DNI);", conn, transaccion);

                        cmdClienteEntrada.Parameters.AddWithValue("@CodigoEntrada", nuevoCodigoEntrada);
                        cmdClienteEntrada.Parameters.AddWithValue("@DNI", boleto.DNICliente);
                        cmdClienteEntrada.ExecuteNonQuery();

                        SqlCommand cmdEventoCliente = new SqlCommand(@"IF NOT EXISTS (
                                                                        SELECT 1 FROM EventosClientes_391IAU 
                                                                        WHERE CodigoDeEvento_391IAU = @CodigoEvento AND DNI_391IAU = @DNI)
                                                                    BEGIN
                                                                        INSERT INTO EventosClientes_391IAU (CodigoDeEvento_391IAU, DNI_391IAU)
                                                                        VALUES (@CodigoEvento, @DNI);
                                                                    END", conn, transaccion);
                        cmdEventoCliente.Parameters.AddWithValue("@CodigoEvento", boleto.CodigoDeEvento);
                        cmdEventoCliente.Parameters.AddWithValue("@DNI", boleto.DNICliente);
                        cmdEventoCliente.ExecuteNonQuery();
                    }
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    exito = false;
                }
            }
            return exito;
        }

    }
}