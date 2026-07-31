using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BE_391IAU;
using System.Runtime.CompilerServices;
using Servicios_391IAU.Composite;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;


namespace DAL_391IAU
{
    public class DAL
    {
        private static readonly string connectionString = @"Server=.;Database=A_StageLink;Integrated Security=True;";
        //private static string connectionString;

        //public SqlConnection conn;
        //public SqlCommand cmd;
        public DAL()
        {
            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            string database = Environment.GetEnvironmentVariable("DB_DATABASE");
            string user = Environment.GetEnvironmentVariable("DB_USER");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            string auth = Environment.GetEnvironmentVariable("DB_AUTH");

            //connectionString = $"Data Source={server};Initial Catalog=A_StageLink;Integrated Security=True;";

            //conn = new SqlConnection(connectionString);
        }

        public static bool InsertarUsuario(BEUsuario usuario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    string queryUsuario = @"INSERT INTO Usuario_391IAU
                                  (DNI_391IAU, Nombre_391IAU, Apellido_391IAU, eMail_391IAU, 
                                   Contraseña_391IAU, Idioma_391IAU, Activo_391IAU, 
                                   Bloqueado_391IAU, Intentos_391IAU, IDRol_391IAU)
                                  VALUES
                                  (@DNI, @Nombre, @Apellido, @Email, 
                                   @Contrasenia, @Idioma, @Activo, 
                                   @Bloqueado, @Intentos, @Rol)";

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
                        cmd.Parameters.AddWithValue("@Rol",
                                 usuario.IDRol_391IAU.HasValue ? (object)usuario.IDRol_391IAU.Value : DBNull.Value
                        );

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
                            Intentos_391IAU = Convert.ToInt32(reader["Intentos_391IAU"]),
                            IDRol_391IAU = reader["IDRol_391IAU"] == DBNull.Value
                                ? (int?)null
                                : Convert.ToInt32(reader["IDRol_391IAU"])
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
                string query = @"
            SELECT 
                U.DNI_391IAU,
                U.Nombre_391IAU,
                U.Apellido_391IAU,
                U.eMail_391IAU,
                U.Activo_391IAU,
                U.Bloqueado_391IAU,
                U.Intentos_391IAU,
                U.Idioma_391IAU,
                U.Contraseña_391IAU,
                U.IDRol_391IAU,
                P.Nombre_391IAU AS RolNombre
            FROM Usuario_391IAU U
            LEFT JOIN GestionDePerfiles.Perfil_391IAU P 
                ON P.IDRol_391IAU = U.IDRol_391IAU";

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
                            Contraseña_391IAU = reader.GetString(8),

                            IDRol_391IAU = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9),

                            RolNombre = reader.IsDBNull(10) ? "Sin rol" : reader.GetString(10)
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
                             Intentos_391IAU = @Intentos,
                             IDRol_391IAU = @Rol
                         WHERE DNI_391IAU = @DNI";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Nombre", u.Nombre_391IAU);
                cmd.Parameters.AddWithValue("@Apellido", u.Apellido_391IAU);
                cmd.Parameters.AddWithValue("@Email", u.eMail_391IAU);
                cmd.Parameters.AddWithValue("@Idioma", u.Idioma_391IAU);
                cmd.Parameters.AddWithValue("@Activo", u.Activo_391IAU);
                cmd.Parameters.AddWithValue("@Bloqueado", u.Bloqueado_391IAU);
                cmd.Parameters.AddWithValue("@Intentos", u.Intentos_391IAU);

                cmd.Parameters.AddWithValue("@Rol",
                    u.IDRol_391IAU.HasValue ? (object)u.IDRol_391IAU.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@DNI", u.DNI_391IAU);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        private static int EjecutarInsert(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddRange(parametros);
                return cmd.ExecuteNonQuery();
            }
        }
        private static int EjecutarScalar(string query, SqlParameter[] parametros)
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

        public static (string, string) ObtenerEstadioYDireccion(string nombreArtista, DateTime fecha)
        {
            string query = @"SELECT ES.NombreEstadio_391IAU, ES.DireccionEstadio_391IAU
                             FROM Eventos_391IAU E
                             INNER JOIN Estadios_391IAU ES ON E.CodigoDeEstadio_391IAU = ES.CodigoDeEstadio_391IAU
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
                        if (reader.Read())
                            return (reader.GetString(0), reader.GetString(1));
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

        public static int ObtenerCapacidadPorCodigoSector(int codigoSector)
        {
            string query = "SELECT Capacidad_391IAU FROM Sectores_391IAU WHERE CodigoDeSector_391IAU = @Codigo";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigoSector;
                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public static int ObtenerEntradasVendidas(int codigoEvento, int codigoSector)
        {
            string query = "SELECT COUNT(*) FROM Entradas_391IAU WHERE CodigoDeEvento_391IAU = @Evento AND CodigoDeSector_391IAU = @Sector";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@Evento", SqlDbType.Int).Value = codigoEvento;
                    cmd.Parameters.Add("@Sector", SqlDbType.Int).Value = codigoSector;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static int InsertarEntrada(BEBoleto boleto)
        {
            int nuevoId = 0;

            EjecutarConDV((cn, tran) =>
            {
                string query = @"
            INSERT INTO Entradas_391IAU (Sector_391IAU, Fecha_391IAU, Precio_391IAU)
            VALUES (@Sector, @Fecha, @Precio);
            SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, cn, tran))
                {
                    cmd.Parameters.Add("@Sector", SqlDbType.Int).Value = boleto.Sector;
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime2).Value = boleto.FechaEvento;
                    cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = boleto.Precio;

                    nuevoId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            },
            "Entradas_391IAU");

            return nuevoId;
        }

        public static bool InsertarClienteEntrada(int idCliente, int idEntrada)
        {
            bool ok = false;

            EjecutarConDV((cn, tran) =>
            {
                string query = @"INSERT INTO ClientesEntradas_391IAU (IDCliente_391IAU, IDEntrada_391IAU)
                         VALUES (@Cliente, @Entrada);";

                using (SqlCommand cmd = new SqlCommand(query, cn, tran))
                {
                    cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = idCliente;
                    cmd.Parameters.Add("@Entrada", SqlDbType.Int).Value = idEntrada;

                    ok = cmd.ExecuteNonQuery() > 0;
                }
            },
            "ClientesEntradas_391IAU"); 
            return ok;
        }

        public static bool InsertarEventoCliente(int idEvento, int idCliente)
        {
            bool ok = false;

            EjecutarConDV((cn, tran) =>
            {
                string query = @"
            INSERT INTO EventosClientes_391IAU (IDEvento_391IAU, IDCliente_391IAU)
            VALUES (@Evento, @Cliente);";

                using (SqlCommand cmd = new SqlCommand(query, cn, tran))
                {
                    cmd.Parameters.Add("@Evento", SqlDbType.Int).Value = idEvento;
                    cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = idCliente;

                    ok = cmd.ExecuteNonQuery() > 0;
                }
            },
            "EventosClientes_391IAU");

            return ok;
        }

        public static bool GuardarEntradasYRelaciones(List<BEBoleto> lista)
        {
            bool exito = true;

            EjecutarConDV((cn, tran) =>
            {
                try
                {
                    foreach (BEBoleto boleto in lista)
                    {
                        string sqlInsertEntrada = @"
                        INSERT INTO Entradas_391IAU (CodigoDeEvento_391IAU, CodigoDeSector_391IAU, Precio_391IAU)
                        VALUES (@CodigoEvento, @CodigoSector, @Precio);
                        SELECT SCOPE_IDENTITY();";

                        int nuevoCodigoEntrada;

                        using (SqlCommand cmdInsert = new SqlCommand(sqlInsertEntrada, cn, tran))
                        {
                            cmdInsert.Parameters.Add("@CodigoEvento", SqlDbType.Int).Value = boleto.CodigoDeEvento;
                            cmdInsert.Parameters.Add("@CodigoSector", SqlDbType.Int).Value = boleto.Sector;
                            cmdInsert.Parameters.Add("@Precio", SqlDbType.Decimal).Value = boleto.Precio;

                            nuevoCodigoEntrada = Convert.ToInt32(cmdInsert.ExecuteScalar());
                        }

                        string sqlClienteEntrada = @"
                        INSERT INTO ClientesEntradas_391IAU (CodigoEntrada_391IAU, DNI_391IAU)
                        VALUES (@CodigoEntrada, @DNI);";

                        using (SqlCommand cmdClienteEntrada = new SqlCommand(sqlClienteEntrada, cn, tran))
                        {
                            cmdClienteEntrada.Parameters.Add("@CodigoEntrada", SqlDbType.Int).Value = nuevoCodigoEntrada;
                            cmdClienteEntrada.Parameters.Add("@DNI", SqlDbType.Int).Value = boleto.DNICliente;
                            cmdClienteEntrada.ExecuteNonQuery();
                        }

                        string sqlEventoCliente = @"
                        IF NOT EXISTS (
                            SELECT 1
                            FROM EventosClientes_391IAU
                            WHERE CodigoDeEvento_391IAU = @CodigoEvento
                              AND DNI_391IAU = @DNI
                        )
                        BEGIN
                            INSERT INTO EventosClientes_391IAU (CodigoDeEvento_391IAU, DNI_391IAU)
                            VALUES (@CodigoEvento, @DNI);
                        END";

                        using (SqlCommand cmdEventoCliente = new SqlCommand(sqlEventoCliente, cn, tran))
                        {
                            cmdEventoCliente.Parameters.Add("@CodigoEvento", SqlDbType.Int).Value = boleto.CodigoDeEvento;
                            cmdEventoCliente.Parameters.Add("@DNI", SqlDbType.Int).Value = boleto.DNICliente;
                            cmdEventoCliente.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {
                    exito = false;
                    throw;
                }
            },
            "Entradas_391IAU",
            "ClientesEntradas_391IAU",
            "EventosClientes_391IAU");
            return exito;
        }
        public static List<BECliente> ObtenerTodosLosClientes()
        {
            List<BECliente> lista = new List<BECliente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT DNI_391IAU, Nombre_391IAU, Apellido_391IAU, Correo_391IAU 
                         FROM Clientes_391IAU";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BECliente c = new BECliente
                    {
                        DNI_391IAU = reader.GetInt32(0),
                        Nombre_391IAU = reader.GetString(1),
                        Apellido_391IAU = reader.GetString(2),
                        Correo_391IAU = reader.GetString(3)
                    };

                    lista.Add(c);
                }
            }

            return lista;
        }

        public static bool ModificarCliente(BECliente c)
        {
            bool ok = false;

            EjecutarConDV((cn, tran) =>
            {
                string query = @"UPDATE Clientes_391IAU
                         SET Nombre_391IAU = @Nombre,
                             Apellido_391IAU = @Apellido
                         WHERE DNI_391IAU = @DNI";

                using (SqlCommand cmd = new SqlCommand(query, cn, tran))
                {
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = c.Nombre_391IAU ?? "";
                    cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 100).Value = c.Apellido_391IAU ?? "";
                    cmd.Parameters.Add("@DNI", SqlDbType.Int).Value = c.DNI_391IAU;

                    ok = cmd.ExecuteNonQuery() > 0;
                }
            },
            "Clientes_391IAU");
            return ok;
        }
        public static bool InsertarCliente(BECliente c)
        {
            bool ok = false;

            EjecutarConDV((cn, tran) =>
            {
                string query = @"INSERT INTO Clientes_391IAU (DNI_391IAU, Nombre_391IAU, Apellido_391IAU, Correo_391IAU)
                         VALUES (@DNI, @Nombre, @Apellido, @Correo)";

                using (SqlCommand cmd = new SqlCommand(query, cn, tran))
                {
                    cmd.Parameters.Add("@DNI", SqlDbType.Int).Value = c.DNI_391IAU;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = c.Nombre_391IAU ?? "";
                    cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 100).Value = c.Apellido_391IAU ?? "";
                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 200).Value = c.Correo_391IAU ?? "";

                    ok = cmd.ExecuteNonQuery() > 0;
                }
            }, "Clientes_391IAU");

            return ok;
        }

        public static bool ExisteClientePorDNI(int dni)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Clientes_391IAU WHERE DNI_391IAU = @DNI";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@DNI", SqlDbType.Int).Value = dni;

                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public static bool InsertarEvento(BEEvento_391IAU e)
        {
            bool ok = false;

            EjecutarConDV((cn, tran) =>
            {
                string query = @"INSERT INTO Eventos_391IAU (Fecha_391IAU, CodigoDeEstadio_391IAU, NombreArtista_391IAU)
                         VALUES (@Fecha, @CodigoEstadio, @NombreArtista)";

                using (SqlCommand cmd = new SqlCommand(query, cn, tran))
                {
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = e.Fecha_391IAU.Date;
                    cmd.Parameters.Add("@CodigoEstadio", SqlDbType.Int).Value = e.CodigoDeEstadio_391IAU;
                    cmd.Parameters.Add("@NombreArtista", SqlDbType.VarChar, 100).Value = e.NombreArtista_391IAU ?? "";

                    ok = cmd.ExecuteNonQuery() > 0;
                }
            }, "Eventos_391IAU");

            return ok;
        }

        public static bool ExisteEventoPorFechaYEstadio(DateTime fecha, int codigoEstadio)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Eventos_391IAU WHERE Fecha_391IAU = @Fecha AND CodigoDeEstadio_391IAU = @CodigoEstadio";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;
                    cmd.Parameters.Add("@CodigoEstadio", SqlDbType.Int).Value = codigoEstadio;

                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public static bool ExisteEventoPorArtistaYFecha(string artista, DateTime fecha)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Eventos_391IAU WHERE NombreArtista_391IAU = @Artista AND Fecha_391IAU = @Fecha";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Artista", SqlDbType.VarChar, 100).Value = artista ?? "";
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;

                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public static int ObtenerCodigoEvento(string artista, DateTime fecha)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = "SELECT CodigoDeEvento_391IAU FROM Eventos_391IAU WHERE NombreArtista_391IAU = @Artista AND Fecha_391IAU = @Fecha";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Artista", SqlDbType.VarChar, 100).Value = artista ?? "";
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;

                    cn.Open();
                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public static List<BEPerfil> ObtenerPerfiles()
        {
            List<BEPerfil> lista = new List<BEPerfil>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IDRol_391IAU, Nombre_391IAU FROM GestionDePerfiles.Perfil_391IAU";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BEPerfil perfil = new BEPerfil
                        {
                            IDRol_391IAU = reader.GetInt32(0),
                            Nombre_391IAU = reader.GetString(1)
                        };

                        lista.Add(perfil);
                    }
                }
            }

            return lista;
        }

        public static void ActualizarEmail(string dni, string emailEncriptado)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuario_391IAU
                         SET eMail_391IAU = @Email
                         WHERE DNI_391IAU = @DNI";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    cmd.Parameters.AddWithValue("@Email", emailEncriptado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<BEPermiso> ObtenerPermisosDePerfil(int idRol)
        {
            var lista = new List<BEPermiso>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT P.IDPermiso_391IAU, P.Nombre_391IAU
            FROM GestionDePerfiles.PerfilPermiso_391IAU PF
            JOIN GestionDePerfiles.Permiso_391IAU P 
                ON PF.IDPermiso_391IAU = P.IDPermiso_391IAU
            WHERE PF.IDRol_391IAU = @Rol", cn);

                cmd.Parameters.AddWithValue("@Rol", idRol);

                cn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new BEPermiso
                    {
                        IDPermiso_391IAU = dr.GetInt32(0),
                        NombrePermiso_391IAU = dr.GetString(1),
                        EsFamilia_391IAU = false
                    });
                }
            }

            return lista;
        }
        public static List<BEPermiso> ObtenerTodosLosPermisosSimples()
        {
            List<BEPermiso> lista = new List<BEPermiso>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT IDPermiso_391IAU, Nombre_391IAU FROM GestionDePerfiles.Permiso_391IAU", cn);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new BEPermiso
                    {
                        IDPermiso_391IAU = Convert.ToInt32(reader["IDPermiso_391IAU"]),
                        NombrePermiso_391IAU = reader["Nombre_391IAU"].ToString(),
                        EsFamilia_391IAU = false
                    });
                }
            }
            return lista;
        }

        public static BEPermiso ObtenerPermisoPorID(int idPermiso)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT IDPermiso_391IAU, Nombre_391IAU FROM GestionDePerfiles.Permiso_391IAU WHERE IDPermiso_391IAU = @ID", cn);

                cmd.Parameters.AddWithValue("@ID", idPermiso);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new BEPermiso
                    {
                        IDPermiso_391IAU = Convert.ToInt32(reader["IDPermiso_391IAU"]),
                        NombrePermiso_391IAU = reader["Nombre_391IAU"].ToString(),
                        EsFamilia_391IAU = false
                    };
                }

                return null;
            }
        }
        public static BEPerfil ObtenerPerfilPorID(int idPerfil)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT IDRol_391IAU, Nombre_391IAU FROM GestionDePerfiles.Perfil_391IAU WHERE IDRol_391IAU = @ID", cn);

                cmd.Parameters.AddWithValue("@ID", idPerfil);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new BEPerfil
                    {
                        IDRol_391IAU = Convert.ToInt32(reader["IDRol_391IAU"]),
                        Nombre_391IAU = reader["Nombre_391IAU"].ToString()
                    };
                }

                return null;
            }
        }

        public static List<int> ObtenerFamiliasDePerfil(int idRol)
        {
            List<int> lista = new List<int>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
                        SELECT IDFamilia_391IAU
                        FROM GestionDePerfiles.PerfilFamilia_391IAU
                        WHERE IDRol_391IAU = @Rol", cn);

                cmd.Parameters.AddWithValue("@Rol", idRol);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(dr.GetInt32(0));
                }
            }
            return lista;
        }
        public static List<BEPermiso> ObtenerPermisosDeFamilia(int idFamilia)
        {
            List<BEPermiso> lista = new List<BEPermiso>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT P.IDPermiso_391IAU, P.Nombre_391IAU
                    FROM GestionDePerfiles.PermisosFamilia_391IAU PF
                    JOIN GestionDePerfiles.Permiso_391IAU P
                        ON PF.IDPermiso_391IAU = P.IDPermiso_391IAU
                    WHERE PF.IDFamilia_391IAU = @Fam", cn);

                cmd.Parameters.AddWithValue("@Fam", idFamilia);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new BEPermiso
                    {
                        IDPermiso_391IAU = dr.GetInt32(0),
                        NombrePermiso_391IAU = dr.GetString(1),
                        EsFamilia_391IAU = false
                    });
                }
            }

            return lista;
        }
        public static string ObtenerNombreFamilia(int idFamilia)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT NombreFamilia_391IAU FROM GestionDePerfiles.Familia_391IAU WHERE IDFamilia_391IAU = @ID",
                    cn);

                cmd.Parameters.AddWithValue("@ID", idFamilia);

                cn.Open();
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        public static List<int> ObtenerSubfamilias(int idFamilia)
        {
            List<int> lista = new List<int>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
                        SELECT IDCompuesto_391IAU
                        FROM GestionDePerfiles.FamiliaFamilia_391IAU
                        WHERE IDFamiliaPadre_391IAU = @ID", cn);

                cmd.Parameters.AddWithValue("@ID", idFamilia);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(dr.GetInt32(0));
                }
            }

            return lista;
        }
        public static List<Familia_391IAU> ObtenerTodasLasFamilias()
        {
            List<Familia_391IAU> lista = new List<Familia_391IAU>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
                        SELECT IDFamilia_391IAU, NombreFamilia_391IAU
                        FROM GestionDePerfiles.Familia_391IAU", cn);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nombre = dr.GetString(1);

                    lista.Add(new Familia_391IAU(id, nombre));
                }
            }

            return lista;
        }
        public static int InsertarPerfil(string nombre)
        {
            string query = @"
                INSERT INTO GestionDePerfiles.Perfil_391IAU (Nombre_391IAU)
                VALUES (@Nombre);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] p =
            {
                new SqlParameter("@Nombre", nombre)
            };

            return EjecutarScalar(query, p);
        }

        public static void InsertarPerfilPermiso(int idPerfil, int idPermiso)
        {
            string query = @"
                INSERT INTO GestionDePerfiles.PerfilPermiso_391IAU (IDRol_391IAU, IDPermiso_391IAU)
                VALUES (@Perfil, @Permiso);";

            SqlParameter[] p =
            {
                new SqlParameter("@Perfil", idPerfil),
                new SqlParameter("@Permiso", idPermiso)
            };

            EjecutarInsert(query, p);
        }
        public static void EliminarPermisosDePerfil(int idPerfil)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.PerfilPermiso_391IAU
                         WHERE IDRol_391IAU = @ID";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@ID", idPerfil);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void EliminarFamiliasDePerfil(int idPerfil)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.PerfilFamilia_391IAU
                         WHERE IDRol_391IAU = @ID";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@ID", idPerfil);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void AsociarPermisoAPerfil(int idPerfil, int idPermiso)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO GestionDePerfiles.PerfilPermiso_391IAU
                         (IDRol_391IAU, IDPermiso_391IAU)
                         VALUES (@Perfil, @Permiso)";

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@Perfil", idPerfil);
                cmd.Parameters.AddWithValue("@Permiso", idPermiso);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void AsociarFamiliaAPerfil(int idPerfil, int idFamilia)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO GestionDePerfiles.PerfilFamilia_391IAU
                         (IDRol_391IAU, IDFamilia_391IAU)
                         VALUES (@Perfil, @Familia)";

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@Perfil", idPerfil);
                cmd.Parameters.AddWithValue("@Familia", idFamilia);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static int CrearPerfil(string nombrePerfil)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
                        INSERT INTO GestionDePerfiles.Perfil_391IAU (Nombre_391IAU)
                        VALUES (@Nombre);
                        SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Nombre", nombrePerfil);

                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public static void EliminarPermisosDeFamilia(int idFamilia)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.PermisosFamilia_391IAU
                         WHERE IDFamilia_391IAU = @ID";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@ID", idFamilia);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void AsociarPermisoAFamilia(int idFamilia, int idPermiso)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO GestionDePerfiles.PermisosFamilia_391IAU
                         (IDFamilia_391IAU, IDPermiso_391IAU)
                         VALUES (@Familia, @Permiso)";

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@Familia", idFamilia);
                cmd.Parameters.AddWithValue("@Permiso", idPermiso);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static int CrearFamilia(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
                        INSERT INTO GestionDePerfiles.Familia_391IAU (NombreFamilia_391IAU)
                        VALUES (@Nombre);

                        SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Nombre", nombre);

                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public static void EliminarFamilia(int idFamilia)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.Familia_391IAU
                         WHERE IDFamilia_391IAU = @ID";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@ID", idFamilia);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void EliminarFamiliaDeTodosLosPerfiles(int idFamilia)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.PerfilFamilia_391IAU
                         WHERE IDFamilia_391IAU = @ID";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@ID", idFamilia);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static bool EliminarPerfil(int idPerfil)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.Perfil_391IAU
                         WHERE IDRol_391IAU = @ID";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@ID", idPerfil);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool EliminarUnaFamiliaDePerfil(int idPerfil, int idFamilia)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.PerfilFamilia_391IAU
                         WHERE IDRol_391IAU = @Perfil AND IDFamilia_391IAU = @Familia";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Perfil", idPerfil);
                cmd.Parameters.AddWithValue("@Familia", idFamilia);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool EliminarUnPermisoDePerfil(int idPerfil, int idPermiso)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.PerfilPermiso_391IAU
                         WHERE IDRol_391IAU = @Perfil AND IDPermiso_391IAU = @Permiso";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Perfil", idPerfil);
                cmd.Parameters.AddWithValue("@Permiso", idPermiso);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool EliminarRelacionFamiliaSubfamilia(int idPadre, int idHija)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.FamiliaFamilia_391IAU
                         WHERE IDFamiliaPadre_391IAU = @Padre
                           AND IDCompuesto_391IAU = @Hija";

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@Padre", idPadre);
                cmd.Parameters.AddWithValue("@Hija", idHija);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool EliminarPermisoDeFamilia(int idFamilia, int idPermiso)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM GestionDePerfiles.PermisosFamilia_391IAU
                         WHERE IDFamilia_391IAU = @FAM
                           AND IDPermiso_391IAU = @PERM";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@FAM", idFamilia);
                cmd.Parameters.AddWithValue("@PERM", idPermiso);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static void AsociarSubfamilia(int idPadre, int idHija)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO GestionDePerfiles.FamiliaFamilia_391IAU
            (IDFamiliaPadre_391IAU, IDCompuesto_391IAU)
            VALUES (@Padre, @Hija)";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Padre", idPadre);
                cmd.Parameters.AddWithValue("@Hija", idHija);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void EliminarTodasLasRelacionesFamiliaSubfamilia(int idFamiliaPadre)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                        DELETE FROM GestionDePerfiles.FamiliaFamilia_391IAU
                        WHERE IDFamiliaPadre_391IAU = @Padre";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Padre", idFamiliaPadre);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataTable ObtenerReporteVentas()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM uvReporteVentas_391IAU";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public static List<string> ObtenerFechasEvento()
        {
            List<string> lista = new List<string>();
            string query = "SELECT DISTINCT CONVERT(varchar(10), FechaEvento, 120) AS Fecha FROM uvReporteVentas_391IAU";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
                }
            }
            return lista;
        }

        public static List<string> ObtenerNombresCompradores()
        {
            List<string> lista = new List<string>();
            string query = "SELECT DISTINCT Nombre FROM uvReporteVentas_391IAU";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
                }
            }
            return lista;
        }

        public static List<string> ObtenerArtistas()
        {
            List<string> lista = new List<string>();
            string query = "SELECT DISTINCT Artista FROM uvReporteVentas_391IAU";

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
                }
            }
            return lista;
        }
        public static bool ActualizarIdioma(int dni, string idioma)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Usuario_391IAU
                    SET Idioma_391IAU = @Idioma
                    WHERE DNI_391IAU = @DNI";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Idioma", idioma);
                cmd.Parameters.AddWithValue("@DNI", dni);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static DataTable ObtenerStockProductos()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT
                p.IDProducto_391IAU,
                p.Nombre_391IAU,
                p.TipoProducto_391IAU,
                p.StockActual_391IAU,
                p.PrecioVenta_391IAU
            FROM VentaExtras.Productos_391IAU p
            ORDER BY p.TipoProducto_391IAU, p.Nombre_391IAU;
        ";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public static DataTable ObtenerNombresProductosVentaExtras()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT DISTINCT Nombre_391IAU
            FROM VentaExtras.Productos_391IAU
            ORDER BY Nombre_391IAU;";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerTiposProductosVentaExtras()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT DISTINCT TipoProducto_391IAU
            FROM VentaExtras.Productos_391IAU
            WHERE TipoProducto_391IAU IS NOT NULL
            ORDER BY TipoProducto_391IAU;";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerStockProductosFiltrado(string nombreProducto, string tipoProducto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT
                p.IDProducto_391IAU,
                p.Nombre_391IAU,
                p.TipoProducto_391IAU,
                p.StockActual_391IAU,
                p.PrecioVenta_391IAU
            FROM VentaExtras.Productos_391IAU p
            WHERE
                (@Nombre IS NULL OR p.Nombre_391IAU = @Nombre)
                AND (@Tipo IS NULL OR p.TipoProducto_391IAU = @Tipo)
            ORDER BY p.TipoProducto_391IAU, p.Nombre_391IAU;
        ";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value =
                        (object)nombreProducto ?? DBNull.Value;

                    cmd.Parameters.Add("@Tipo", SqlDbType.NChar, 10).Value =
                        (object)tipoProducto ?? DBNull.Value;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
        public static DataTable ObtenerProveedoresVentaExtras()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT CUIT_391IAU, Nombre_391IAU
                    FROM VentaExtras.Proveedor_391IAU
                    ORDER BY Nombre_391IAU;
                ";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable ObtenerProductosPorProveedorVentaExtras(long proveedorId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.IDProducto_391IAU, p.Nombre_391IAU
                    FROM VentaExtras.ProveedoresProductos_391IAU pp
                    INNER JOIN VentaExtras.Productos_391IAU p
                        ON p.IDProducto_391IAU = pp.IDProducto_391IAU
                    WHERE pp.CUIT_391IAU = @ProveedorId
                    ORDER BY p.Nombre_391IAU;
                ";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@ProveedorId", SqlDbType.BigInt).Value = proveedorId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static (int StockActual, int PrecioPallet) ObtenerStockYPrecioProductoVentaExtras(int productoId)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT
                ISNULL(TRY_CONVERT(int, StockActual_391IAU), 0) AS StockActual,
                ISNULL(PrecioVenta_391IAU, 0) AS PrecioPallet
            FROM VentaExtras.Productos_391IAU
            WHERE IDProducto_391IAU = @ProductoId;
        ";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoId;

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int stock = dr.GetInt32(0);
                            int precio = dr.GetInt32(1);
                            return (stock, precio);
                        }
                    }
                }
            }

            return (0, 0);
        }

        public static int SumarStockProductoVentaExtras(int productoId, int pallets)
        {
            int nuevoStock = 0;

            EjecutarConDV((cn, tran) =>
            {
                string query = @"
            UPDATE VentaExtras.Productos_391IAU
            SET StockActual_391IAU = CONVERT(varchar(100),
                ISNULL(TRY_CONVERT(int, StockActual_391IAU), 0) + @Pallets
            )
            WHERE IDProducto_391IAU = @ProductoId;

            SELECT ISNULL(TRY_CONVERT(int, StockActual_391IAU), 0)
            FROM VentaExtras.Productos_391IAU
            WHERE IDProducto_391IAU = @ProductoId;";

                using (SqlCommand cmd = new SqlCommand(query, cn, tran))
                {
                    cmd.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoId;
                    cmd.Parameters.Add("@Pallets", SqlDbType.Int).Value = pallets;

                    object result = cmd.ExecuteScalar();
                    nuevoStock = (result != null) ? Convert.ToInt32(result) : 0;
                }

            }, "Productos_391IAU");

            return nuevoStock;
        }

        public static int InsertarProveedorVentaExtras(string nombreProveedor, string correoProveedor)
        {
            int idProveedor = 0;

            EjecutarConDV((cn, tran) =>
            {
                string query = @"
            INSERT INTO VentaExtras.Proveedor_391IAU (Nombre_391IAU, Correo_391IAU)
            VALUES (@Nombre, @Correo);

            SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, cn, tran))
                {
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreProveedor ?? "";
                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correoProveedor ?? "";

                    idProveedor = Convert.ToInt32(cmd.ExecuteScalar());
                }

            }, "Proveedor_391IAU");

            return idProveedor;
        }

        public static int ObtenerProductoPorNombreVentaExtras(string nombreProducto, SqlConnection con, SqlTransaction tran)
        {
            string query = @"
        SELECT TOP 1 IDProducto_391IAU
        FROM VentaExtras.Productos_391IAU
        WHERE Nombre_391IAU = @Nombre;";

            using (SqlCommand cmd = new SqlCommand(query, con, tran))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombreProducto);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    return Convert.ToInt32(result);

                return 0;
            }
        }

        public static int InsertarProductoVentaExtras(
        string nombreProducto,
        int precio,
        string tipoProducto,
        SqlConnection con,
        SqlTransaction tran)
        {
            string query = @"
        INSERT INTO VentaExtras.Productos_391IAU
            (Nombre_391IAU, TipoProducto_391IAU, StockActual_391IAU, PrecioVenta_391IAU)
        VALUES
            (@Nombre, @Tipo, @Stock, @Precio);

        SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(query, con, tran))
            {
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreProducto ?? "";
                cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 30).Value = tipoProducto ?? "";
                cmd.Parameters.Add("@Stock", SqlDbType.VarChar, 100).Value = "0";
                cmd.Parameters.Add("@Precio", SqlDbType.Int).Value = precio;

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static void AsociarProductoAProveedorVentaExtras(int proveedorId, int productoId, SqlConnection con, SqlTransaction tran)
        {
            string query = @"
        IF NOT EXISTS (
            SELECT 1
            FROM VentaExtras.ProveedoresProductos_391IAU
            WHERE CUIT_391IAU = @Proveedor AND IDProducto_391IAU = @Producto
        )
        BEGIN
            INSERT INTO VentaExtras.ProveedoresProductos_391IAU (CUIT_391IAU, IDProducto_391IAU)
            VALUES (@Proveedor, @Producto);
        END";

            using (SqlCommand cmd = new SqlCommand(query, con, tran))
            {
                cmd.Parameters.AddWithValue("@Proveedor", proveedorId);
                cmd.Parameters.AddWithValue("@Producto", productoId);
                cmd.ExecuteNonQuery();
            }
        }

        public static int RegistrarProveedorConProductosVentaExtras(
        long cuitProveedor,
        string nombreProveedor,
        string correoProveedor,
        List<(string NombreProducto, int Precio, string TipoProducto)> productos)
        {
            int cuitRet = 0;

            EjecutarConDV((cn, tran) =>
            {
                string queryProveedor = @"
            INSERT INTO VentaExtras.Proveedor_391IAU 
                (CUIT_391IAU, Nombre_391IAU, Correo_391IAU)
            VALUES 
                (@CUIT, @Nombre, @Correo);";

                using (SqlCommand cmdProv = new SqlCommand(queryProveedor, cn, tran))
                {
                    cmdProv.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = cuitProveedor;
                    cmdProv.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreProveedor ?? "";
                    cmdProv.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correoProveedor ?? "";
                    cmdProv.ExecuteNonQuery();
                }

                if (productos != null)
                {
                    foreach (var p in productos)
                    {
                        int productoId = ObtenerProductoPorNombreVentaExtras(p.NombreProducto, cn, tran);

                        if (productoId <= 0)
                        {
                            productoId = InsertarProductoVentaExtras(
                                p.NombreProducto,
                                p.Precio,
                                p.TipoProducto,
                                cn,
                                tran);
                        }

                        AsociarProductoAProveedorVentaExtras(
                            (int)cuitProveedor,
                            productoId,
                            cn,
                            tran);
                    }
                }

                cuitRet = (int)cuitProveedor;

            }, "Proveedor_391IAU", "Productos_391IAU", "ProveedoresProductos_391IAU");

            return cuitRet;
        }
        public class ProductoProveedorUpsert
        {
            public int IdProducto { get; set; }
            public string Nombre { get; set; }
            public int Precio { get; set; }
            public string TipoProducto { get; set; }
        }

        public static BEProveedorEdicion ObtenerProveedorPorCuitVentaExtras(long cuit)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT CUIT_391IAU, Nombre_391IAU, Correo_391IAU
            FROM VentaExtras.Proveedor_391IAU
            WHERE CUIT_391IAU = @CUIT;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = cuit;

                    cn.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new BEProveedorEdicion
                            {
                                CUIT_391IAU = Convert.ToInt64(dr["CUIT_391IAU"]),
                                Nombre_391IAU = dr["Nombre_391IAU"].ToString(),
                                Correo_391IAU = dr["Correo_391IAU"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static List<BEProductoProveedorEdicion> ObtenerProductosDeProveedorVentaExtras(long cuit)
        {
            var lista = new List<BEProductoProveedorEdicion>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT p.IDProducto_391IAU,
                       p.Nombre_391IAU,
                       p.PrecioVenta_391IAU,
                       p.TipoProducto_391IAU
                FROM VentaExtras.ProveedoresProductos_391IAU pp
                INNER JOIN VentaExtras.Productos_391IAU p
                    ON p.IDProducto_391IAU = pp.IDProducto_391IAU
                WHERE pp.CUIT_391IAU = @CUIT
                ORDER BY p.Nombre_391IAU;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = cuit;

                    cn.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new BEProductoProveedorEdicion
                            {
                                IDProducto_391IAU = Convert.ToInt32(dr["IDProducto_391IAU"]),
                                Nombre_391IAU = dr["Nombre_391IAU"].ToString(),
                                PrecioVenta_391IAU = Convert.ToInt32(dr["PrecioVenta_391IAU"]),
                                TipoProducto_391IAU = dr["TipoProducto_391IAU"].ToString().Trim()
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public static void ActualizarProveedorConProductosVentaExtras(
        long cuit,
        string nombreProveedor,
        string correoProveedor,
        List<ProductoProveedorUpsert> productos)
        {
            EjecutarConDV((cn, tran) =>
            {
                string sqlUpdProv = @"
                UPDATE VentaExtras.Proveedor_391IAU
                SET Nombre_391IAU = @Nombre,
                    Correo_391IAU = @Correo
                WHERE CUIT_391IAU = @CUIT;";

                using (SqlCommand cmd = new SqlCommand(sqlUpdProv, cn, tran))
                {
                    cmd.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = cuit;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreProveedor ?? "";
                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correoProveedor ?? "";

                    int rows = cmd.ExecuteNonQuery();
                    if (rows <= 0) throw new Exception("No se encontró el proveedor para actualizar.");
                }

                var actuales = new HashSet<int>();
                string sqlActuales = @"
                SELECT IDProducto_391IAU
                FROM VentaExtras.ProveedoresProductos_391IAU
                WHERE CUIT_391IAU = @CUIT;";

                using (SqlCommand cmd = new SqlCommand(sqlActuales, cn, tran))
                {
                    cmd.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = cuit;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                            actuales.Add(Convert.ToInt32(dr[0]));
                    }
                }

                var nuevosAsociados = new HashSet<int>();

                foreach (var p in productos ?? new List<ProductoProveedorUpsert>())
                {
                    int idProducto = p.IdProducto;

                    if (idProducto > 0)
                    {
                        string sqlUpdProd = @"
                        UPDATE VentaExtras.Productos_391IAU
                        SET Nombre_391IAU = @Nombre,
                            PrecioVenta_391IAU = @Precio,
                            TipoProducto_391IAU = @Tipo
                        WHERE IDProducto_391IAU = @ID;";

                        using (SqlCommand cmd = new SqlCommand(sqlUpdProd, cn, tran))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = idProducto;
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = p.Nombre ?? "";
                            cmd.Parameters.Add("@Precio", SqlDbType.Int).Value = p.Precio;
                            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 30).Value = p.TipoProducto ?? "";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string sqlInsProd = @"
                        INSERT INTO VentaExtras.Productos_391IAU
                            (Nombre_391IAU, TipoProducto_391IAU, StockActual_391IAU, PrecioVenta_391IAU)
                        VALUES
                            (@Nombre, @Tipo, @Stock, @Precio);

                        SELECT SCOPE_IDENTITY();";

                        using (SqlCommand cmd = new SqlCommand(sqlInsProd, cn, tran))
                        {
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = p.Nombre ?? "";
                            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 30).Value = p.TipoProducto ?? "";
                            cmd.Parameters.Add("@Stock", SqlDbType.VarChar, 100).Value = "0";
                            cmd.Parameters.Add("@Precio", SqlDbType.Int).Value = p.Precio;

                            idProducto = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }

                    string sqlRel = @"
                    IF NOT EXISTS (
                        SELECT 1 FROM VentaExtras.ProveedoresProductos_391IAU
                        WHERE CUIT_391IAU = @CUIT AND IDProducto_391IAU = @ID
                    )
                    BEGIN
                        INSERT INTO VentaExtras.ProveedoresProductos_391IAU (CUIT_391IAU, IDProducto_391IAU)
                        VALUES (@CUIT, @ID);
                    END";

                    using (SqlCommand cmd = new SqlCommand(sqlRel, cn, tran))
                    {
                        cmd.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = cuit;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = idProducto;
                        cmd.ExecuteNonQuery();
                    }

                    nuevosAsociados.Add(idProducto);
                }

                var eliminados = actuales.Except(nuevosAsociados).ToList();

                foreach (var idElim in eliminados)
                {
                    string sqlDelRel = @"
                    DELETE FROM VentaExtras.ProveedoresProductos_391IAU
                    WHERE CUIT_391IAU = @CUIT AND IDProducto_391IAU = @ID;";

                    using (SqlCommand cmd = new SqlCommand(sqlDelRel, cn, tran))
                    {
                        cmd.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = cuit;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = idElim;
                        cmd.ExecuteNonQuery();
                    }
                }

            }, "Proveedor_391IAU", "Productos_391IAU", "ProveedoresProductos_391IAU");
        }
        public static DataTable ObtenerProductosVentaExtrasParaCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT
                    p.IDProducto_391IAU,
                    p.Nombre_391IAU,
                    ISNULL(TRY_CONVERT(int, p.StockActual_391IAU), 0) AS StockActualInt
                FROM VentaExtras.Productos_391IAU p
                ORDER BY p.Nombre_391IAU;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public static DataTable ObtenerEventosParaCombo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT
                    CodigoDeEvento_391IAU,
                    NombreArtista_391IAU,
                    Fecha_391IAU
                FROM Eventos_391IAU
                ORDER BY Fecha_391IAU DESC, NombreArtista_391IAU;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public static int ObtenerStockActualProductoVentaExtras(int idProducto)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT ISNULL(TRY_CONVERT(int, StockActual_391IAU), 0)
                FROM VentaExtras.Productos_391IAU
                WHERE IDProducto_391IAU = @ID;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = idProducto;
                    cn.Open();
                    object r = cmd.ExecuteScalar();
                    return (r != null) ? Convert.ToInt32(r) : 0;
                }
            }
        }
        public static void VenderProductoAEventoVentaExtras(int idProducto, int codigoEvento, int cantidad)
        {
            if (idProducto <= 0) throw new Exception("Producto inválido.");
            if (codigoEvento <= 0) throw new Exception("Evento inválido.");

            EjecutarConDV((cn, tran) =>
            {
                int stockActual;

                string sqlStock = @"
                SELECT ISNULL(TRY_CONVERT(int, StockActual_391IAU), 0)
                FROM VentaExtras.Productos_391IAU
                WHERE IDProducto_391IAU = @ID;";

                using (SqlCommand cmd = new SqlCommand(sqlStock, cn, tran))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = idProducto;
                    object r = cmd.ExecuteScalar();
                    stockActual = (r != null) ? Convert.ToInt32(r) : 0;
                }

                if (cantidad <= 0) throw new Exception("La cantidad a vender debe ser mayor a 0.");
                if (cantidad > stockActual) throw new Exception($"No hay stock suficiente. Stock actual: {stockActual}");

                string sqlUpsert = @"
                IF EXISTS (
                    SELECT 1
                    FROM VentaExtras.ProductosEventos_391IAU
                    WHERE IDProducto_391IAU = @IDProd
                      AND CodigoDeEvento_391IAU = @CodEve
                )
                BEGIN
                    UPDATE VentaExtras.ProductosEventos_391IAU
                    SET StockParaEvento_391IAU = StockParaEvento_391IAU + @Cant
                    WHERE IDProducto_391IAU = @IDProd
                      AND CodigoDeEvento_391IAU = @CodEve;
                END
                ELSE
                BEGIN
                    INSERT INTO VentaExtras.ProductosEventos_391IAU
                        (IDProducto_391IAU, CodigoDeEvento_391IAU, StockParaEvento_391IAU)
                    VALUES
                        (@IDProd, @CodEve, @Cant);
                END";

                using (SqlCommand cmd = new SqlCommand(sqlUpsert, cn, tran))
                {
                    cmd.Parameters.Add("@IDProd", SqlDbType.Int).Value = idProducto;
                    cmd.Parameters.Add("@CodEve", SqlDbType.Int).Value = codigoEvento;
                    cmd.Parameters.Add("@Cant", SqlDbType.Int).Value = cantidad;
                    cmd.ExecuteNonQuery();
                }

                string sqlRestar = @"
                UPDATE VentaExtras.Productos_391IAU
                SET StockActual_391IAU = CONVERT(varchar(100),
                    ISNULL(TRY_CONVERT(int, StockActual_391IAU), 0) - @Cant
                )
                WHERE IDProducto_391IAU = @ID;";

                using (SqlCommand cmd = new SqlCommand(sqlRestar, cn, tran))
                {
                    cmd.Parameters.Add("@Cant", SqlDbType.Int).Value = cantidad;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = idProducto;

                    int rows = cmd.ExecuteNonQuery();
                    if (rows <= 0) throw new Exception("No se encontró el producto para descontar stock.");
                }

            }, "ProductosEventos_391IAU", "Productos_391IAU");
        }
        public static DataTable ConsultarBitacoraEventos(BEFiltroBitacoraEventos filtro)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                var sql = @"
                        SELECT
                            B.CodigoDeEvento_391IAU,
                            B.FechaEvento_391IAU,
                            B.Modulo_391IAU,
                            B.Descripcion_391IAU,
                            B.Criticidad_391IAU,
                            U.DNI_391IAU,
                            U.Nombre_391IAU,
                            U.Apellido_391IAU
                        FROM BitacoraEventos_391IAU B
                        INNER JOIN Usuario_391IAU U ON U.DNI_391IAU = B.DNI_391IAU
                        WHERE 1 = 1
                        ";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;

                    if (filtro.FechaDesde.HasValue)
                    {
                        sql += " AND B.FechaEvento_391IAU >= @FechaDesde ";
                        cmd.Parameters.Add("@FechaDesde", SqlDbType.DateTime2).Value = filtro.FechaDesde.Value.Date;
                    }

                    if (filtro.FechaHasta.HasValue)
                    {
                        DateTime hasta = filtro.FechaHasta.Value.Date.AddDays(1);
                        sql += " AND B.FechaEvento_391IAU < @FechaHasta ";
                        cmd.Parameters.Add("@FechaHasta", SqlDbType.DateTime2).Value = hasta;
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Modulo))
                    {
                        sql += " AND B.Modulo_391IAU = @Modulo ";
                        cmd.Parameters.Add("@Modulo", SqlDbType.NVarChar, 80).Value = filtro.Modulo.Trim();
                    }

                    if (filtro.Criticidad.HasValue)
                    {
                        sql += " AND B.Criticidad_391IAU = @Criticidad ";
                        cmd.Parameters.Add("@Criticidad", SqlDbType.TinyInt).Value = filtro.Criticidad.Value;
                    }

                    if (filtro.DNI.HasValue)
                    {
                        sql += " AND U.DNI_391IAU = @DNI ";
                        cmd.Parameters.Add("@DNI", SqlDbType.Int).Value = filtro.DNI.Value;
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                    {
                        sql += " AND U.Nombre_391IAU LIKE @Nombre ";
                        cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 120).Value = "%" + filtro.Nombre.Trim() + "%";
                    }

                    if (!string.IsNullOrWhiteSpace(filtro.Apellido))
                    {
                        sql += " AND U.Apellido_391IAU LIKE @Apellido ";
                        cmd.Parameters.Add("@Apellido", SqlDbType.NVarChar, 120).Value = "%" + filtro.Apellido.Trim() + "%";
                    }

                    sql += " ORDER BY B.FechaEvento_391IAU DESC;";

                    cmd.CommandText = sql;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
        public static List<string> ObtenerModulosBitacora(string excluirModuloExacto)
        {
            List<string> modulos = new List<string>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT DISTINCT Modulo_391IAU
                FROM BitacoraEventos_391IAU
                WHERE Modulo_391IAU IS NOT NULL
                  AND LTRIM(RTRIM(Modulo_391IAU)) <> ''
                  AND Modulo_391IAU <> @Excl
                ORDER BY Modulo_391IAU;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@Excl", SqlDbType.NVarChar, 80).Value = excluirModuloExacto ?? "";

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                            modulos.Add(dr.GetString(0));
                    }
                }
            }

            return modulos;
        }
        public static void InsertarBitacoraEvento(BEBitacoraEventos e)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                INSERT INTO BitacoraEventos_391IAU
                (DNI_391IAU, FechaEvento_391IAU, Descripcion_391IAU, Criticidad_391IAU, Modulo_391IAU)
                VALUES
                (@DNI, @Fecha, @Desc, @Crit, @Modulo);";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@DNI", SqlDbType.Int).Value = e.DNI_391IAU;
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime2).Value = e.FechaEvento_391IAU;
                    cmd.Parameters.Add("@Desc", SqlDbType.NVarChar, 250).Value = e.Descripcion_391IAU ?? "";
                    cmd.Parameters.Add("@Crit", SqlDbType.TinyInt).Value = e.Criticidad_391IAU;
                    cmd.Parameters.Add("@Modulo", SqlDbType.NVarChar, 80).Value = e.Modulo_391IAU ?? "";

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void BackupDatabase(string backupFilePath, Action<int> onProgress = null, CancellationToken ct = default)
        {
            var csb = new SqlConnectionStringBuilder(connectionString);
            string dbName = csb.InitialCatalog;

            if (string.IsNullOrWhiteSpace(dbName))
                throw new Exception("No se pudo determinar el nombre de la base desde el connectionString.");

            if (!backupFilePath.EndsWith(".bak", StringComparison.OrdinalIgnoreCase))
                throw new Exception("El archivo de backup debe tener extensión .bak");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.InfoMessage += (s, e) => TryReportPercent(e.Message, onProgress);

                con.Open();

                string sql = $@"
                BACKUP DATABASE [{dbName}]
                TO DISK = @Path
                WITH INIT, STATS = 5;";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@Path", SqlDbType.NVarChar, 260).Value = backupFilePath;

                    ct.ThrowIfCancellationRequested();
                    onProgress?.Invoke(0);

                    cmd.ExecuteNonQuery();

                    onProgress?.Invoke(100);
                }
            }
        }

        public static void RestoreDatabase(string backupFilePath, Action<int> onProgress = null, CancellationToken ct = default)
        {
            var csb = new SqlConnectionStringBuilder(connectionString);
            string dbName = csb.InitialCatalog;

            if (string.IsNullOrWhiteSpace(dbName))
                throw new Exception("No se pudo determinar el nombre de la base desde el connectionString.");

            if (!File.Exists(backupFilePath))
                throw new FileNotFoundException("No se encontró el archivo .bak indicado.", backupFilePath);

            var csbMaster = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            using (SqlConnection con = new SqlConnection(csbMaster.ConnectionString))
            {
                con.FireInfoMessageEventOnUserErrors = true;
                con.InfoMessage += (s, e) => TryReportPercent(e.Message, onProgress);

                con.Open();
                onProgress?.Invoke(0);

                ct.ThrowIfCancellationRequested();

                using (SqlCommand cmdSingle = new SqlCommand($@"
                ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", con))
                {
                    cmdSingle.CommandTimeout = 0;
                    cmdSingle.ExecuteNonQuery();
                }

                ct.ThrowIfCancellationRequested();

                string logicalData = null;
                string logicalLog = null;

                using (SqlCommand cmdFileList = new SqlCommand(@"
                RESTORE FILELISTONLY FROM DISK = @Path;", con))
                {
                    cmdFileList.CommandTimeout = 0;
                    cmdFileList.Parameters.Add("@Path", SqlDbType.NVarChar, 260).Value = backupFilePath;

                    using (var dr = cmdFileList.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string logicalName = dr["LogicalName"].ToString();
                            string type = dr["Type"].ToString(); 

                            if (type == "D" && logicalData == null) logicalData = logicalName;
                            if (type == "L" && logicalLog == null) logicalLog = logicalName;
                        }
                    }
                }

                if (logicalData == null || logicalLog == null)
                    throw new Exception("No se pudieron obtener los LogicalName (data/log) desde el backup.");

                ct.ThrowIfCancellationRequested();

                string dataPath = null;
                string logPath = null;

                using (SqlCommand cmdPaths = new SqlCommand(@"
                SELECT
                  CAST(SERVERPROPERTY('InstanceDefaultDataPath') AS nvarchar(4000)) AS DataPath,
                  CAST(SERVERPROPERTY('InstanceDefaultLogPath') AS nvarchar(4000)) AS LogPath;", con))
                using (var dr = cmdPaths.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        dataPath = dr["DataPath"]?.ToString();
                        logPath = dr["LogPath"]?.ToString();
                    }
                }

                if (string.IsNullOrWhiteSpace(dataPath) || string.IsNullOrWhiteSpace(logPath))
                {
                    dataPath = null;
                    logPath = null;
                }

                string mdfTarget = dataPath != null ? Path.Combine(dataPath, $"{dbName}.mdf") : null;
                string ldfTarget = logPath != null ? Path.Combine(logPath, $"{dbName}_log.ldf") : null;

                string restoreSql;

                if (mdfTarget != null && ldfTarget != null)
                {
                    restoreSql = $@"
                    RESTORE DATABASE [{dbName}]
                    FROM DISK = @Path
                    WITH REPLACE,
                         MOVE @LogicalData TO @MdfPath,
                         MOVE @LogicalLog  TO @LdfPath,
                         STATS = 5;";
                }
                else
                {
                    restoreSql = $@"
                    RESTORE DATABASE [{dbName}]
                    FROM DISK = @Path
                    WITH REPLACE,
                         STATS = 5;";
                }

                using (SqlCommand cmdRestore = new SqlCommand(restoreSql, con))
                {
                    cmdRestore.CommandTimeout = 0;
                    cmdRestore.Parameters.Add("@Path", SqlDbType.NVarChar, 260).Value = backupFilePath;

                    if (mdfTarget != null && ldfTarget != null)
                    {
                        cmdRestore.Parameters.Add("@LogicalData", SqlDbType.NVarChar, 200).Value = logicalData;
                        cmdRestore.Parameters.Add("@LogicalLog", SqlDbType.NVarChar, 200).Value = logicalLog;
                        cmdRestore.Parameters.Add("@MdfPath", SqlDbType.NVarChar, 4000).Value = mdfTarget;
                        cmdRestore.Parameters.Add("@LdfPath", SqlDbType.NVarChar, 4000).Value = ldfTarget;
                    }

                    ct.ThrowIfCancellationRequested();
                    cmdRestore.ExecuteNonQuery();
                }

                using (SqlCommand cmdMulti = new SqlCommand($@"
                    ALTER DATABASE [{dbName}] SET MULTI_USER;", con))
                {
                    cmdMulti.CommandTimeout = 0;
                    cmdMulti.ExecuteNonQuery();
                }

                onProgress?.Invoke(100);
            }
        }

        private static void TryReportPercent(string message, Action<int> onProgress)
        {
            if (onProgress == null || string.IsNullOrWhiteSpace(message))
                return;

            var m = Regex.Match(message, @"(\d+)\s+percent", RegexOptions.IgnoreCase);
            if (m.Success && int.TryParse(m.Groups[1].Value, out int p))
                onProgress(p);
        }

        public static DataTable ObtenerReporteRFN2()
        {
            return ObtenerReporteRFN2Filtrado(null, null, null);
        }

        public static DataTable ObtenerReporteRFN2Filtrado(string nombreProveedor, string nombreProducto, string tipoProducto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT
                pr.CUIT_391IAU,
                pr.Nombre_391IAU AS NombreProveedor,
                p.IDProducto_391IAU,
                p.Nombre_391IAU AS NombreProducto,
                LTRIM(RTRIM(p.TipoProducto_391IAU)) AS TipoProducto_391IAU,
                p.StockActual_391IAU,
                ISNULL(TRY_CONVERT(int, p.StockActual_391IAU), 0) AS StockActualInt,
                p.PrecioVenta_391IAU
            FROM VentaExtras.ProveedoresProductos_391IAU pp
            INNER JOIN VentaExtras.Proveedor_391IAU pr
                ON pr.CUIT_391IAU = pp.CUIT_391IAU
            INNER JOIN VentaExtras.Productos_391IAU p
                ON p.IDProducto_391IAU = pp.IDProducto_391IAU
            WHERE
                (@Prov IS NULL OR pr.Nombre_391IAU = @Prov)
                AND (@Prod IS NULL OR p.Nombre_391IAU = @Prod)
                AND (@Tipo IS NULL OR LTRIM(RTRIM(p.TipoProducto_391IAU)) = @Tipo)
            ORDER BY pr.Nombre_391IAU, p.Nombre_391IAU;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@Prov", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(nombreProveedor) ? (object)DBNull.Value : nombreProveedor.Trim();

                    cmd.Parameters.Add("@Prod", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(nombreProducto) ? (object)DBNull.Value : nombreProducto.Trim();

                    cmd.Parameters.Add("@Tipo", SqlDbType.NVarChar, 30).Value =
                        string.IsNullOrWhiteSpace(tipoProducto) ? (object)DBNull.Value : tipoProducto.Trim();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        public static List<string> ObtenerTiposProductoRFN2()
        {
            List<string> lista = new List<string>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT DISTINCT LTRIM(RTRIM(TipoProducto_391IAU)) AS Tipo
            FROM VentaExtras.Productos_391IAU
            WHERE TipoProducto_391IAU IS NOT NULL
              AND LTRIM(RTRIM(TipoProducto_391IAU)) <> ''
            ORDER BY Tipo;";

                cn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
                }
            }

            return lista;
        }

        public static List<string> ObtenerNombresProductoRFN2()
        {
            List<string> lista = new List<string>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT DISTINCT Nombre_391IAU
            FROM VentaExtras.Productos_391IAU
            WHERE Nombre_391IAU IS NOT NULL
              AND LTRIM(RTRIM(Nombre_391IAU)) <> ''
            ORDER BY Nombre_391IAU;";

                cn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
                }
            }

            return lista;
        }

        public static List<string> ObtenerNombresProveedorRFN2()
        {
            List<string> lista = new List<string>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT DISTINCT Nombre_391IAU
            FROM VentaExtras.Proveedor_391IAU
            WHERE Nombre_391IAU IS NOT NULL
              AND LTRIM(RTRIM(Nombre_391IAU)) <> ''
            ORDER BY Nombre_391IAU;";

                cn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
                }
            }

            return lista;
        }
        public static DataTable ObtenerProductosCambios(DateTime? desde, DateTime? hasta, string nombre)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT
                        IDProducto_391IAU,
                        Nombre_391IAU,
                        StockActual_391IAU,
                        PrecioVenta_391IAU,
                        TipoProducto_391IAU,
                        Estado,
                        Fecha
                    FROM VentaExtras.ProductosCambio_391IAU
                    WHERE 1 = 1
                ";      

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;

                    if (desde.HasValue)
                    {
                        sql += " AND Fecha >= @Desde ";
                        cmd.Parameters.Add("@Desde", SqlDbType.Date).Value = desde.Value.Date;
                    }

                    if (hasta.HasValue)
                    {
                        sql += " AND Fecha <= @Hasta ";
                        cmd.Parameters.Add("@Hasta", SqlDbType.Date).Value = hasta.Value.Date;
                    }

                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        sql += " AND Nombre_391IAU LIKE @Nombre ";
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = "%" + nombre.Trim() + "%";
                    }

                    sql += " ORDER BY Fecha DESC, IDProducto_391IAU ASC;";

                    cmd.CommandText = sql;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
        public static bool ExisteActivoDistintoAlSeleccionado(
        int idProducto,
        DateTime fechaSel,
        string nombreSel,
        string stockSel,
        int precioSel,
        string tipoSel)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT COUNT(*)
                FROM VentaExtras.ProductosCambio_391IAU
                WHERE IDProducto_391IAU = @ID
                  AND Estado = 1
                  AND NOT (
                       CONVERT(date, Fecha) = @Fecha
                   AND Nombre_391IAU = @Nombre
                   AND StockActual_391IAU = @Stock
                   AND PrecioVenta_391IAU = @Precio
                   AND LTRIM(RTRIM(TipoProducto_391IAU)) = LTRIM(RTRIM(@Tipo))
                  );";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = idProducto;
                    cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = fechaSel.Date;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreSel ?? "";
                    cmd.Parameters.Add("@Stock", SqlDbType.VarChar, 100).Value = stockSel ?? "";
                    cmd.Parameters.Add("@Precio", SqlDbType.Int).Value = precioSel;
                    cmd.Parameters.Add("@Tipo", SqlDbType.NChar, 10).Value = (object)tipoSel ?? "";

                    cn.Open();
                    int c = Convert.ToInt32(cmd.ExecuteScalar());
                    return c > 0;
                }
            }
        }
        public static void ActivarVersionProductoDesdeBitacora(
        int idProducto,
        DateTime fechaSel,
        string nombreSel,
        string stockSel,
        int precioSel,
        string tipoSel)
        {
            EjecutarConDV((cn, tran) =>
            {
                string sqlUpdMain = @"
            UPDATE VentaExtras.Productos_391IAU
            SET Nombre_391IAU = @Nombre,
                StockActual_391IAU = @Stock,
                PrecioVenta_391IAU = @Precio,
                TipoProducto_391IAU = @Tipo
            WHERE IDProducto_391IAU = @ID;";

                using (SqlCommand cmd = new SqlCommand(sqlUpdMain, cn, tran))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = idProducto;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreSel ?? "";
                    cmd.Parameters.Add("@Stock", SqlDbType.VarChar, 100).Value = stockSel ?? "";
                    cmd.Parameters.Add("@Precio", SqlDbType.Int).Value = precioSel;
                    cmd.Parameters.Add("@Tipo", SqlDbType.NChar, 10).Value = (object)(tipoSel ?? "") ?? "";

                    int rows = cmd.ExecuteNonQuery();
                    if (rows <= 0) throw new Exception("No se encontró el producto en la tabla principal.");
                }

            }, "Productos_391IAU");
        }
        public struct DigitoVerificadorPersistido
        {
            public string DVH { get; set; }
            public string DVV { get; set; }
            public int CantFilas { get; set; }
        }


        public static IReadOnlyList<string> ObtenerClavesTablasDV()
        {
            return DV_CONFIG.Keys.OrderBy(x => x).ToList();
        }

        public static void RecalcularDVPorClave(string claveTabla)
        {
            if (string.IsNullOrWhiteSpace(claveTabla))
                throw new ArgumentException("Clave de tabla inválida.", nameof(claveTabla));

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();

                try
                {
                    RecalcularDVTabla(claveTabla, cn, tran);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public static void RecalcularDVDeTodasLasTablas()
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();

                try
                {
                    foreach (var clave in DV_CONFIG.Keys.OrderBy(k => k))
                    {
                        RecalcularDVTabla(clave, cn, tran);
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public static bool ValidarTablaPorClave(string claveTabla)
        {
            if (string.IsNullOrWhiteSpace(claveTabla))
                throw new ArgumentException("Clave de tabla inválida.", nameof(claveTabla));

            if (!DV_CONFIG.TryGetValue(claveTabla, out var cfg))
                throw new Exception($"No hay DV_CONFIG para la tabla: {claveTabla}");

            var persistido = ObtenerDigitoVerificador(cfg.Tabla);
            if (persistido == null) return false;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                DataTable dt = ObtenerDatosTablaParaDV(cfg.Tabla, cfg.OrderBy, cfg.ColumnasEnOrden);

                string dvhActual = CalcularDVH(dt);
                string dvvActual = CalcularDVV(dt);

                return string.Equals(persistido.Value.DVH, dvhActual, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(persistido.Value.DVV, dvvActual, StringComparison.OrdinalIgnoreCase);
            }
        }

        public static List<(string ClaveTabla, bool Ok, string Error)> ValidarTodasLasTablasDV()
        {
            var res = new List<(string, bool, string)>();

            foreach (var clave in DV_CONFIG.Keys.OrderBy(k => k))
            {
                try
                {
                    bool ok = ValidarTablaPorClave(clave);
                    res.Add((clave, ok, ok ? null : "DV no coincide o no existe DV persistido."));
                }
                catch (Exception ex)
                {
                    res.Add((clave, false, ex.Message));
                }
            }

            return res;
        }
        public static DataTable ObtenerDatosTablaParaDV(string tabla, string[] orderByCols, params string[] columnasEnOrden)
        {
            if (string.IsNullOrWhiteSpace(tabla)) throw new ArgumentException("Tabla inválida.");
            if (orderByCols == null || orderByCols.Length == 0) throw new ArgumentException("Debe indicar ORDER BY.");
            if (columnasEnOrden == null || columnasEnOrden.Length == 0) throw new ArgumentException("Debe indicar columnas.");

            bool EsIdentificadorSeguro(string s) =>
                !string.IsNullOrWhiteSpace(s) &&
                s.All(ch => char.IsLetterOrDigit(ch) || ch == '_' || ch == '.' || ch == '[' || ch == ']');

            if (!EsIdentificadorSeguro(tabla)) throw new Exception("Nombre de tabla inválido.");

            foreach (var c in columnasEnOrden)
                if (!EsIdentificadorSeguro(c)) throw new Exception("Nombre de columna inválido: " + c);

            foreach (var o in orderByCols)
                if (!EsIdentificadorSeguro(o)) throw new Exception("Nombre de ORDER BY inválido: " + o);

            DataTable dt = new DataTable();

            string columnas = string.Join(", ", columnasEnOrden.Select(c => $"[{c}]"));
            string orderBy = string.Join(", ", orderByCols.Select(o => $"[{o}]"));

            string sql = $@"
            SELECT {columnas}
            FROM {tabla}
            ORDER BY {orderBy};";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                da.Fill(dt);
            }

            return dt;
        }
        public static void UpsertDigitoVerificador(string tabla, string dvh, string dvv, int cantFilas)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();

                try
                {
                    string sql = @"
                    IF EXISTS (
                        SELECT 1
                        FROM dbo.DigitoVerificador_391IAU
                        WHERE Tabla_391IAU = @Tabla
                    )
                    BEGIN
                        UPDATE dbo.DigitoVerificador_391IAU
                        SET DigitoVerificadorHorizontal_391IAU = @DVH,
                            DigitoVerificadorVertical_391IAU = @DVV,
                            CantFilas_391IAU = @CantFilas
                        WHERE Tabla_391IAU = @Tabla;
                    END
                    ELSE
                    BEGIN
                        INSERT INTO dbo.DigitoVerificador_391IAU
                            (Tabla_391IAU,
                             DigitoVerificadorHorizontal_391IAU,
                             DigitoVerificadorVertical_391IAU,
                             CantFilas_391IAU)
                        VALUES
                            (@Tabla, @DVH, @DVV, @CantFilas);
                    END";

                    using (SqlCommand cmd = new SqlCommand(sql, cn, tran))
                    {
                        cmd.Parameters.Add("@Tabla", SqlDbType.VarChar, 100).Value = tabla.Trim();
                        cmd.Parameters.Add("@DVH", SqlDbType.VarChar, 255).Value = dvh ?? "";
                        cmd.Parameters.Add("@DVV", SqlDbType.VarChar, 255).Value = dvv ?? "";
                        cmd.Parameters.Add("@CantFilas", SqlDbType.Int).Value = cantFilas;

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        public static DigitoVerificadorPersistido? ObtenerDigitoVerificador(string tabla)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT
                    DigitoVerificadorHorizontal_391IAU,
                    DigitoVerificadorVertical_391IAU,
                    CantFilas_391IAU
                FROM dbo.DigitoVerificador_391IAU
                WHERE Tabla_391IAU = @Tabla;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@Tabla", SqlDbType.VarChar, 100).Value = tabla.Trim();

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new DigitoVerificadorPersistido
                            {
                                DVH = dr["DigitoVerificadorHorizontal_391IAU"].ToString(),
                                DVV = dr["DigitoVerificadorVertical_391IAU"].ToString(),
                                CantFilas = dr["CantFilas_391IAU"] != DBNull.Value
                                    ? Convert.ToInt32(dr["CantFilas_391IAU"])
                                    : 0
                            };
                        }
                    }
                }
            }

            return null;
        }
        public static DataTable ObtenerDatosTablaParaDV(
        string tabla,
        string[] orderByCols,
        SqlConnection cn,
        SqlTransaction tran,
        params string[] columnasEnOrden)
        {
            DataTable dt = new DataTable();

            string columnas = string.Join(", ", columnasEnOrden.Select(c => $"[{c}]"));
            string orderBy = string.Join(", ", orderByCols.Select(o => $"[{o}]"));

            string sql = $@"
            SELECT {columnas}
            FROM {tabla}
            ORDER BY {orderBy};";

            using (SqlCommand cmd = new SqlCommand(sql, cn, tran))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            return dt;
        }

        public static void UpsertDigitoVerificador(
        string tabla,
        string dvh,
        string dvv,
        int cantFilas,
        SqlConnection cn,
        SqlTransaction tran)
        {
            string sql = @"
                IF EXISTS (SELECT 1 FROM dbo.DigitoVerificador_391IAU WHERE Tabla_391IAU = @Tabla)
                BEGIN
                    UPDATE dbo.DigitoVerificador_391IAU
                    SET DigitoVerificadorHorizontal_391IAU = @DVH,
                        DigitoVerificadorVertical_391IAU = @DVV,
                        CantFilas_391IAU = @CantFilas
                    WHERE Tabla_391IAU = @Tabla;
                END
                ELSE
                BEGIN
                    INSERT INTO dbo.DigitoVerificador_391IAU
                        (Tabla_391IAU, DigitoVerificadorHorizontal_391IAU, DigitoVerificadorVertical_391IAU, CantFilas_391IAU)
                    VALUES
                        (@Tabla, @DVH, @DVV, @CantFilas);
                END";

            using (SqlCommand cmd = new SqlCommand(sql, cn, tran))
            {
                cmd.Parameters.Add("@Tabla", SqlDbType.VarChar, 100).Value = tabla.Trim();
                cmd.Parameters.Add("@DVH", SqlDbType.VarChar, 255).Value = dvh ?? "";
                cmd.Parameters.Add("@DVV", SqlDbType.VarChar, 255).Value = dvv ?? "";
                cmd.Parameters.Add("@CantFilas", SqlDbType.Int).Value = cantFilas;
                cmd.ExecuteNonQuery();
            }
        }
        private static void EjecutarConDV(Action<SqlConnection, SqlTransaction> work, params string[] tablasARecalcular)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();

                try
                {
                    work(cn, tran);

                    foreach (string tabla in tablasARecalcular.Distinct())
                    {
                        RecalcularDVTabla(tabla, cn, tran);
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        private class DVConfig
        {
            public string Tabla { get; set; }
            public string[] OrderBy { get; set; }
            public string[] ColumnasEnOrden { get; set; }
            public string[] PKColumnas { get; set; }
        }

        private static readonly Dictionary<string, DVConfig> DV_CONFIG =
        new Dictionary<string, DVConfig>(StringComparer.OrdinalIgnoreCase)
        {
            ["Clientes_391IAU"] = new DVConfig
            {
                Tabla = "dbo.Clientes_391IAU",
                OrderBy = new[] { "DNI_391IAU" },
                ColumnasEnOrden = new[] { "DNI_391IAU", "Nombre_391IAU", "Apellido_391IAU", "Correo_391IAU" },
                PKColumnas = new[] { "DNI_391IAU" }
            },

            ["ClientesEntradas_391IAU"] = new DVConfig
            {
                Tabla = "dbo.ClientesEntradas_391IAU",
                OrderBy = new[] { "CodigoEntrada_391IAU", "DNI_391IAU" },
                ColumnasEnOrden = new[] { "CodigoEntrada_391IAU", "DNI_391IAU" },
                PKColumnas = new[] { "CodigoEntrada_391IAU", "DNI_391IAU" }
            },

            ["Entradas_391IAU"] = new DVConfig
            {
                Tabla = "dbo.Entradas_391IAU",
                OrderBy = new[] { "CodigoEntrada_391IAU" },
                ColumnasEnOrden = new[] { "CodigoEntrada_391IAU", "CodigoDeEvento_391IAU", "CodigoDeSector_391IAU", "Precio_391IAU" },
                PKColumnas = new[] { "CodigoEntrada_391IAU" }
            },

            ["Estadios_391IAU"] = new DVConfig
            {
                Tabla = "dbo.Estadios_391IAU",
                OrderBy = new[] { "CodigoDeEstadio_391IAU" },
                ColumnasEnOrden = new[] { "CodigoDeEstadio_391IAU", "NombreEstadio_391IAU", "DireccionEstadio_391IAU", "CapacidadDeEstadio" },
                PKColumnas = new[] { "CodigoDeEstadio_391IAU" }
            },

            ["Eventos_391IAU"] = new DVConfig
            {
                Tabla = "dbo.Eventos_391IAU",
                OrderBy = new[] { "CodigoDeEvento_391IAU" },
                ColumnasEnOrden = new[] { "CodigoDeEvento_391IAU", "Fecha_391IAU", "CodigoDeEstadio_391IAU", "NombreArtista_391IAU" },
                PKColumnas = new[] { "CodigoDeEvento_391IAU" }
            },

            ["EventosClientes_391IAU"] = new DVConfig
            {
                Tabla = "dbo.EventosClientes_391IAU",
                OrderBy = new[] { "CodigoDeEvento_391IAU", "DNI_391IAU" },
                ColumnasEnOrden = new[] { "CodigoDeEvento_391IAU", "DNI_391IAU" },
                PKColumnas = new[] { "CodigoDeEvento_391IAU", "DNI_391IAU" }
            },

            ["Sectores_391IAU"] = new DVConfig
            {
                Tabla = "dbo.Sectores_391IAU",
                OrderBy = new[] { "CodigoDeSector_391IAU" },
                ColumnasEnOrden = new[]
            {
                "CodigoDeSector_391IAU","NombreSector_391IAU","Capacidad_391IAU","CodigoDeEstadio_391IAU","PrecioDeSector_391IAU"
            },
                PKColumnas = new[] { "CodigoDeSector_391IAU" }
            },

            ["Productos_391IAU"] = new DVConfig
            {
                Tabla = "VentaExtras.Productos_391IAU",
                OrderBy = new[] { "IDProducto_391IAU" },
                ColumnasEnOrden = new[]
            {
                "IDProducto_391IAU","Nombre_391IAU","StockActual_391IAU","PrecioVenta_391IAU","TipoProducto_391IAU"
            },
                PKColumnas = new[] { "IDProducto_391IAU" }
            },

            ["ProductosEventos_391IAU"] = new DVConfig
            {
                Tabla = "VentaExtras.ProductosEventos_391IAU",
                OrderBy = new[] { "IDProducto_391IAU", "CodigoDeEvento_391IAU" },
                ColumnasEnOrden = new[] { "IDProducto_391IAU", "CodigoDeEvento_391IAU", "StockParaEvento_391IAU" },
                PKColumnas = new[] { "IDProducto_391IAU", "CodigoDeEvento_391IAU" }
            },

            ["Proveedor_391IAU"] = new DVConfig
            {
                Tabla = "VentaExtras.Proveedor_391IAU",
                OrderBy = new[] { "CUIT_391IAU" },
                ColumnasEnOrden = new[] { "CUIT_391IAU", "Nombre_391IAU", "Correo_391IAU" },
                PKColumnas = new[] { "CUIT_391IAU" }
            },

            ["ProveedoresProductos_391IAU"] = new DVConfig
            {
                Tabla = "VentaExtras.ProveedoresProductos_391IAU",
                OrderBy = new[] { "CUIT_391IAU", "IDProducto_391IAU" },
                ColumnasEnOrden = new[] { "CUIT_391IAU", "IDProducto_391IAU" },
                PKColumnas = new[] { "CUIT_391IAU", "IDProducto_391IAU" }
            },
        };
        private static void RecalcularDVTabla(string claveTabla, SqlConnection cn, SqlTransaction tran)
        {
            if (!DV_CONFIG.TryGetValue(claveTabla, out var cfg))
                throw new Exception($"No hay DV_CONFIG para la tabla: {claveTabla}");

            DataTable dt = ObtenerDatosTablaParaDV(cfg.Tabla, cfg.OrderBy, cn, tran, cfg.ColumnasEnOrden);

            string dvh = CalcularDVH(dt);
            string dvv = CalcularDVV(dt);
            int cantFilas = dt.Rows.Count;

            ActualizarDVHFilas(cfg, cn, tran);

            UpsertDigitoVerificador(cfg.Tabla, dvh, dvv, cantFilas, cn, tran);
        }
        private static string CalcularDVH(DataTable dt)
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));

            var filasHash = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                var fila = new StringBuilder();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) fila.Append("|");
                    fila.Append(NormalizarValor(row[i]));
                }

                string hashFila = HashSha256(fila.ToString());
                filasHash.Append(hashFila).Append(";");
            }

            return HashSha256(filasHash.ToString());
        }

        private static string CalcularDVV(DataTable dt)
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));

            var columnasHash = new StringBuilder();

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                var col = new StringBuilder();

                foreach (DataRow row in dt.Rows)
                {
                    col.Append(NormalizarValor(row[c])).Append("|");
                }

                string hashCol = HashSha256(col.ToString());
                columnasHash.Append(hashCol).Append(";");
            }

            return HashSha256(columnasHash.ToString());
        }

        private static string CalcularDVHFila(DataRow row, DataTable dt)
        {
            var fila = new StringBuilder();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i > 0) fila.Append("|");
                fila.Append(NormalizarValor(row[i]));
            }
            return HashSha256(fila.ToString());
        }

        private static void ActualizarDVHFilas(DVConfig cfg, SqlConnection cn, SqlTransaction tran)
        {
            if (cfg.PKColumnas == null || cfg.PKColumnas.Length == 0) return;

            DataTable dt = ObtenerDatosTablaParaDV(cfg.Tabla, cfg.OrderBy, cn, tran, cfg.ColumnasEnOrden);

            string where = string.Join(" AND ", cfg.PKColumnas.Select(pk => $"[{pk}] = @PK_{pk}"));
            string sqlUpdate = $"UPDATE {cfg.Tabla} SET DVH_Fila_391IAU = @DVHFila WHERE {where}";

            foreach (DataRow row in dt.Rows)
            {
                string dvhFila = CalcularDVHFila(row, dt);

                using (SqlCommand cmd = new SqlCommand(sqlUpdate, cn, tran))
                {
                    cmd.Parameters.Add("@DVHFila", SqlDbType.VarChar, 255).Value = dvhFila;
                    foreach (string pk in cfg.PKColumnas)
                    {
                        object pkVal = row[pk];
                        cmd.Parameters.AddWithValue($"@PK_{pk}", pkVal == DBNull.Value ? (object)DBNull.Value : pkVal);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<BECambioDetectado> DetectarCambios()
        {
            var cambios = new List<BECambioDetectado>();

            foreach (var kvp in DV_CONFIG)
            {
                string claveTabla = kvp.Key;
                DVConfig cfg = kvp.Value;

                var persistido = ObtenerDigitoVerificador(cfg.Tabla);
                if (persistido == null) continue;

                DataTable dt = ObtenerDatosTablaParaDV(cfg.Tabla, cfg.OrderBy, cfg.ColumnasEnOrden);
                int cantFilasActuales = dt.Rows.Count;
                int cantFilasEsperadas = persistido.Value.CantFilas;

                string dvhActual = CalcularDVH(dt);
                bool dvhCambiado = !string.Equals(persistido.Value.DVH, dvhActual, StringComparison.OrdinalIgnoreCase);

                if (!dvhCambiado) continue;

                if (cantFilasActuales > cantFilasEsperadas)
                {
                    cambios.Add(new BECambioDetectado
                    {
                        ClaveTabla = claveTabla,
                        TipoCambio = "Insercion",
                        ClavePrimaria = null,
                        FilasEsperadas = cantFilasEsperadas,
                        FilasActuales = cantFilasActuales
                    });
                }
                else if (cantFilasActuales < cantFilasEsperadas)
                {
                    cambios.Add(new BECambioDetectado
                    {
                        ClaveTabla = claveTabla,
                        TipoCambio = "Eliminacion",
                        ClavePrimaria = null,
                        FilasEsperadas = cantFilasEsperadas,
                        FilasActuales = cantFilasActuales
                    });
                }
                else if (cfg.PKColumnas != null && cfg.PKColumnas.Length > 0)
                {
                    // Misma cantidad de filas pero DVH distinto → Edicion
                    // Comparar DVH_Fila almacenado contra el calculado, fila a fila
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        cn.Open();

                        string pkCols = string.Join(", ", cfg.PKColumnas.Select(pk => $"[{pk}]"));
                        string orderBy = string.Join(", ", cfg.OrderBy.Select(o => $"[{o}]"));
                        string sqlFilas = $"SELECT {pkCols}, [DVH_Fila_391IAU] FROM {cfg.Tabla} ORDER BY {orderBy};";

                        DataTable dtFilas = new DataTable();
                        using (SqlCommand cmd = new SqlCommand(sqlFilas, cn))
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtFilas);
                        }

                        // dt y dtFilas están ordenadas igual (mismo ORDER BY) → comparación 1:1 por índice
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string dvhFilaActual = CalcularDVHFila(dt.Rows[i], dt);
                            string dvhFilaAlmacenada = dtFilas.Rows[i]["DVH_Fila_391IAU"] != DBNull.Value
                                ? dtFilas.Rows[i]["DVH_Fila_391IAU"].ToString()
                                : "";

                            if (!string.Equals(dvhFilaActual, dvhFilaAlmacenada, StringComparison.OrdinalIgnoreCase))
                            {
                                string pk = string.Join("|", cfg.PKColumnas.Select(p =>
                                    dtFilas.Rows[i][p] != DBNull.Value ? dtFilas.Rows[i][p].ToString() : "NULL"));

                                cambios.Add(new BECambioDetectado
                                {
                                    ClaveTabla = claveTabla,
                                    TipoCambio = "Edicion",
                                    ClavePrimaria = pk,
                                    FilasEsperadas = cantFilasEsperadas,
                                    FilasActuales = cantFilasActuales
                                });
                            }
                        }
                    }
                }
                else
                {
                    cambios.Add(new BECambioDetectado
                    {
                        ClaveTabla = claveTabla,
                        TipoCambio = "Edicion",
                        ClavePrimaria = null,
                        FilasEsperadas = cantFilasEsperadas,
                        FilasActuales = cantFilasActuales
                    });
                }
            }

            return cambios;
        }

        private static string HashSha256(string input)
        {
            if (input == null) input = "";

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha.ComputeHash(bytes);

                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private static string NormalizarValor(object value)
        {
            if (value == null || value == DBNull.Value) return "NULL";

            if (value is string s) return s.Trim();

            if (value is DateTime dt)
                return dt.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            if (value is bool b)
                return b ? "1" : "0";

            if (value is decimal dec)
                return dec.ToString(CultureInfo.InvariantCulture);

            if (value is double dbl)
                return dbl.ToString("R", CultureInfo.InvariantCulture);

            if (value is float flt)
                return flt.ToString("R", CultureInfo.InvariantCulture);

            if (value is IFormattable formattable)
                return formattable.ToString(null, CultureInfo.InvariantCulture);

            return value.ToString().Trim();
        }
        public static DataTable ObtenerReporteInteligente()
        {
            return ObtenerReporteInteligenteFiltrado(null, null);
        }

        public static DataTable ObtenerReporteInteligenteFiltrado(string artista, DateTime? fecha)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT
                    e.CodigoDeEvento_391IAU,
                    e.Fecha_391IAU,
                    e.NombreArtista_391IAU
                FROM dbo.Eventos_391IAU e
                LEFT JOIN VentaExtras.ProductosEventos_391IAU pe
                    ON pe.CodigoDeEvento_391IAU = e.CodigoDeEvento_391IAU
                WHERE pe.CodigoDeEvento_391IAU IS NULL
                  AND (@Artista IS NULL OR e.NombreArtista_391IAU = @Artista)
                  AND (@Fecha IS NULL OR CONVERT(date, e.Fecha_391IAU) = CONVERT(date, @Fecha))
                ORDER BY e.Fecha_391IAU DESC, e.NombreArtista_391IAU;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@Artista", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(artista) ? (object)DBNull.Value : artista.Trim();

                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value =
                        fecha.HasValue ? (object)fecha.Value : DBNull.Value;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
        public static List<string> ObtenerArtistasReporteInteligente()
        {
            List<string> lista = new List<string>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT DISTINCT e.NombreArtista_391IAU
                FROM dbo.Eventos_391IAU e
                LEFT JOIN VentaExtras.ProductosEventos_391IAU pe
                    ON pe.CodigoDeEvento_391IAU = e.CodigoDeEvento_391IAU
                WHERE pe.CodigoDeEvento_391IAU IS NULL
                  AND e.NombreArtista_391IAU IS NOT NULL
                  AND LTRIM(RTRIM(e.NombreArtista_391IAU)) <> ''
                ORDER BY e.NombreArtista_391IAU;";

                cn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
                }
            }

            return lista;
        }
        public static List<DateTime> ObtenerFechasReporteInteligentePorArtista(string artista)
        {
            List<DateTime> lista = new List<DateTime>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string sql = @"
                SELECT DISTINCT e.Fecha_391IAU
                FROM dbo.Eventos_391IAU e
                LEFT JOIN VentaExtras.ProductosEventos_391IAU pe
                    ON pe.CodigoDeEvento_391IAU = e.CodigoDeEvento_391IAU
                WHERE pe.CodigoDeEvento_391IAU IS NULL
                  AND (@Artista IS NULL OR e.NombreArtista_391IAU = @Artista)
                ORDER BY e.Fecha_391IAU DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@Artista", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(artista) ? (object)DBNull.Value : artista.Trim();

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                            lista.Add(dr.GetDateTime(0));
                    }
                }
            }

            return lista;
        }
    }
}