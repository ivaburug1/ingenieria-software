using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_BE;
using _686DP_Dal;
using _686DP_SERVICIOS;
using System.Net;
using _686DP_SERVICIOS.Singleton;

namespace _686DP_MPP
{
    public class _686DPMPPUsuarios
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();
        _686DPCriptoManager criptoManager = new _686DPCriptoManager();
        public void _686DPAgregarALita(string contraseñaActual, int dni)
        {
            string consultaInsert = @"INSERT INTO [686DP UsuarioContraseñas] (DP686_DNI, DP686_Contraseña)
                                  VALUES (@DNI, @Contraseña)";
            ArrayList parametrosInsert = new ArrayList
            {
            new SqlParameter("@DNI", dni),
            new SqlParameter("@Contraseña", contraseñaActual)
            };
            _686DPDalGeneral dal = new _686DPDalGeneral();
            dal._686DPEscribir(consultaInsert, parametrosInsert);

        }

        public void _686DPGrabarContraseñaNueva(string text, int dni)
        {

            string consulta = @"UPDATE [686DP_Usuario]
                            SET DP686_Contraseña = @Contraseña
                            WHERE DP686_DNI = @DNI";

            ArrayList parametros = new ArrayList
        {
            new SqlParameter("@Contraseña", text),
            new SqlParameter("@DNI", dni)
        };

            _686DPDalGeneral dal = new _686DPDalGeneral();
            dal._686DPEscribir(consulta, parametros);
        }

        public List<string> _686DPVerificarContraseñas(int dni)
        {
            try
            {
                List<string> contraseñas = new List<string>();
                _686DPDalGeneral dal = new _686DPDalGeneral();

                string consulta = @"
                SELECT ec.DP686_Contraseña
                FROM [dbo].[686DP UsuarioContraseñas] ec
                INNER JOIN [686DP_Usuario] e ON ec.DP686_DNI = e.DP686_DNI
                WHERE e.DP686_DNI = @dni";

                ArrayList parametros = new ArrayList
        {
            new SqlParameter("@dni", dni)
        };

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                foreach (DataRow row in dt.Rows)
                {
                    contraseñas.Add(row["DP686_Contraseña"].ToString());
                }

                return contraseñas;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al verificar contraseñas anteriores: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al verificar contraseñas anteriores: " + ex.Message, ex);
            }
        }

        public void _686DPActualizarUsuarioExistente(_686DP_Usuarios emp)
        {
            try
            {
                string nombreSP = "up686DP_InsertOrUpdateUsuario";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@DNI", emp.DP686_DNI),
                    new SqlParameter("@Nombre", emp.DP686_Nombre),
                    new SqlParameter("@Apellido", emp.DP686_Apellido),
                    new SqlParameter("@Email", emp.DP686_Email),
                    new SqlParameter("@Rol", emp.DP686_Rol),
                    new SqlParameter("@Usuario", emp.DP686_Usuario),
                    new SqlParameter("@Contraseña", emp.DP686_Contraseña),
                    new SqlParameter("@Activo", emp.DP686_Activo),
                    new SqlParameter("@Bloqueado", emp.DP686_Bloqueado),
                    new SqlParameter("@Contra", emp.DP686_CambiarContraseña),
                    new SqlParameter("@Idioma", emp.DP686_Idioma)
                };

                _686DPDalGeneral dal = new _686DPDalGeneral();
                dal._686DPEjecutar(nombreSP, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario con DNI {emp.DP686_DNI}: {ex.Message}", ex);
            }
        }

        public void _686DPBloquearUsuario(int DNI)
        {
            try
            {
                string consulta = "UPDATE [dbo].[686DP_Usuario] SET DP686_Bloqueado = 1 WHERE DP686_DNI = @DNI";
                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", DNI) };

                _686DPDalGeneral dal = new _686DPDalGeneral();
                dal._686DPEscribir(consulta, parametros);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de SQL al intentar bloquear al usuario '{DNI}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al bloquear al usuario '{DNI}': {ex.Message}", ex);
            }
        }

        public string _686DPBuscarContraseña(int DNI)
        {
            try
            {
                DataTable dt;
                _686DPDalGeneral dal = new _686DPDalGeneral();
                string consulta = "SELECT [DP686_Contraseña] FROM [dbo].[686DP_Usuario] WHERE DP686_DNI = @DNI;";
                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", DNI) };

                dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0][0]);
                }

                return string.Empty;
            }
            catch (Exception)
            {
                throw new Exception($"Error al buscar la contraseña del usuario '{DNI}'.");
            }
        }

        public string _686DPBuscarUsuario(int DNI)
        {
            try
            {
                DataTable dt;
                _686DPDalGeneral dal = new _686DPDalGeneral();
                string consulta = "SELECT [DP686_Usuario] FROM [dbo].[686DP_Usuario] WHERE DP686_DNI = @DNI";
                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", DNI) };

                dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0][0]);
                }

                return string.Empty;
            }
            catch (Exception)
            {
                throw new Exception($"Error al buscar el nombre de usuario '{DNI}'.");
            }
        }

        public object _686DPFiltrarUsuarios(string rol, bool? activo, bool? bloqueado)
        {
            try
            {
                List<string> condiciones = new List<string>();
                ArrayList parametros = new ArrayList();

                if (!string.IsNullOrEmpty(rol))
                {
                    string queryRol = "SELECT [DP686_PerfilID]" +
                                      "FROM [dbo].[686DP_Perfil]" +
                                      "WHERE [DP686_Nombre] = @Nombre";
                    SqlParameter paramRol = new SqlParameter("@Nombre", rol);

                    _686DPDalGeneral dalRol = new _686DPDalGeneral();
                    var tabla = (DataTable)dalRol._686DPConsultar(queryRol, new ArrayList { paramRol });

                    if (tabla.Rows.Count > 0)
                    {
                        int idPerfil = Convert.ToInt32(tabla.Rows[0]["DP686_PerfilID"]);
                        condiciones.Add("P.[DP686_PerfilID] = @PerfilID");
                        parametros.Add(new SqlParameter("@PerfilID", idPerfil));
                    }
                    else
                    {
                        return new DataTable();
                    }
                }

                if (activo.HasValue)
                {
                    condiciones.Add("DP686_Activo = @Activo");
                    parametros.Add(new SqlParameter("@Activo", activo.Value));
                }

                if (bloqueado.HasValue)
                {
                    condiciones.Add("DP686_Bloqueado = @Bloqueado");
                    parametros.Add(new SqlParameter("@Bloqueado", bloqueado.Value));
                }

                string whereClause = condiciones.Count > 0 ? "WHERE " + string.Join(" AND ", condiciones) : "";
                string consulta = $@"
                SELECT 
                U.DP686_DNI,
                U.DP686_Nombre,
                U.DP686_Apellido,
                U.DP686_Email,
                U.DP686_Activo,
                U.DP686_Bloqueado,
                U.DP686_Idioma, 
                P.DP686_Nombre AS DP686_Rol
                FROM [dbo].[686DP_Usuario] AS U
                LEFT JOIN [dbo].[686DP_Perfil] AS P ON U.[DP686_PerfilID] = P.[DP686_PerfilID]
                {whereClause}";


                _686DPDalGeneral dal = new _686DPDalGeneral();
                return dal._686DPConsultar(consulta, parametros);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al filtrar usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al filtrar usuario: " + ex.Message, ex);
            }
        }

        public bool _686DPTraerEstado(int DNI)
        {
            try
            {
                DataTable dt;
                _686DPDalGeneral dal = new _686DPDalGeneral();
                string consulta = "SELECT DP686_Activo FROM [dbo].[686DP_Usuario] WHERE DP686_DNI = @DNI";
                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", DNI) };

                dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt.Rows[0][0]);
                }

                throw new Exception("No se encontró el estado del usuario.");
            }
            catch (Exception)
            {
                throw new Exception($"Error al obtener el estado de actividad del usuario '{DNI}'.");
            }
        }
        public void _686DPAgregarIntento(int dNI)
        {
            try
            {
                string consulta = $"IF EXISTS (SELECT 1 FROM [dbo].[686DP_UsuarioIntentos] WHERE DP686_DNI = @DNI)\r\nBEGIN\r\n    UPDATE [dbo].[686DP_UsuarioIntentos]\r\n    SET DP686_intentos = ISNULL(DP686_intentos, 0) + 1\r\n    WHERE DP686_DNI = @DNI;\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO [dbo].[686DP_UsuarioIntentos] (DP686_DNI, DP686_intentos)\r\n    VALUES (@DNI, 1);\r\nEND\r\n";
                                 

                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", dNI) };
                _686DPDalGeneral dal = new _686DPDalGeneral();
                dal._686DPEscribir(consulta, parametros);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar los intentos del usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al intentar agregar intento.", ex);
            }
        }


        public List<string> _686DPTraerRoles()
        {
            try
            {
                DataTable dt;
                List<string> roles = new List<string>();
                _686DPDalGeneral dal = new _686DPDalGeneral();
                string consulta = @"
                SELECT DISTINCT DP686_Nombre 
                FROM [dbo].[686DP_Perfil] ";
                ArrayList parametros = new ArrayList();

                dt = dal._686DPConsultar(consulta, parametros);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        string rol = Convert.ToString(item[0]);
                        roles.Add(rol);
                    }
                    catch (Exception exFila)
                    {
                        throw new Exception("Error al procesar una fila del resultado de roles. Verificá los datos.", exFila);
                    }
                }

                return roles;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de SQL al intentar obtener la lista de roles: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener la lista de roles: " + ex.Message, ex);
            }
        }


        public List<_686DP_Usuarios> _686DPTraerTodos()
        {
            try
            {
                DataTable dt;
                _686DPCriptoManager _686DPCriptoManager = new _686DPCriptoManager();
                List<_686DP_Usuarios> usuarios = new List<_686DP_Usuarios>();
                _686DPDalGeneral dal = new _686DPDalGeneral();

                string consulta = @"
                SELECT 
                    U.DP686_DNI, 
                    U.DP686_Nombre, 
                    U.DP686_Apellido, 
                    U.DP686_Email, 
                    P.DP686_Nombre AS Rol,
                    U.DP686_Usuario, 
                    U.DP686_Contraseña, 
                    U.DP686_Activo, 
                    U.DP686_Bloqueado, 
                    U.DP686_CambiarContraseña,
                    U.DP686_Idioma
                FROM [dbo].[686DP_Usuario] U
                INNER JOIN [dbo].[686DP_Perfil] P ON U.DP686_PerfilID = P.DP686_PerfilID";

                ArrayList parametros = new ArrayList();
                dt = dal._686DPConsultar(consulta, parametros);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        int DNI = Convert.ToInt32(item["DP686_DNI"]);
                        string Nombre = Convert.ToString(item["DP686_Nombre"]);
                        string Apellido = Convert.ToString(item["DP686_Apellido"]);
                        string Email = Convert.ToString(item["DP686_Email"]);
                        string Rol = Convert.ToString(item["Rol"]);
                        string usuario = Convert.ToString(item["DP686_Usuario"]);
                        string contraseña = Convert.ToString(item["DP686_Contraseña"]);
                        bool Activo = Convert.ToBoolean(item["DP686_Activo"]);
                        bool Bloqueado = Convert.ToBoolean(item["DP686_Bloqueado"]);
                        bool cambiarContra = Convert.ToBoolean(item["DP686_CambiarContraseña"]);

                        _686DP_Usuarios Usuario = new _686DP_Usuarios(DNI, Nombre, Apellido, Email, Rol, usuario, contraseña, Activo, Bloqueado, cambiarContra);
                        Usuario.DP686_Idioma = Convert.ToString(item["DP686_Idioma"]);
                        usuarios.Add(Usuario);
                    }
                    catch (Exception exFila)
                    {
                        throw new Exception("Error al procesar una fila de usuario. Verificá los datos. " + exFila.Message, exFila);
                    }
                }

                return usuarios;
            }
            catch (SqlException exSql)
            {
                throw new Exception("Error de SQL al intentar obtener la lista de usuario: " + exSql.Message, exSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener la lista de usuario: " + ex.Message, ex);
            }
        }

        public string _686DPTraerRol(int DNI)
        {
            try
            {
                string consulta = @"
            SELECT P.DP686_Nombre 
            FROM [dbo].[686DP_Usuario] U
            INNER JOIN [dbo].[686DP_Perfil] P ON U.DP686_PerfilID = P.DP686_PerfilID
            WHERE U.DP686_DNI = @DNI";

                ArrayList parametros = new ArrayList
        {
            new SqlParameter("@DNI", DNI)
        };

                _686DPDalGeneral dal = new _686DPDalGeneral();
                DataTable dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0]["DP686_Nombre"]);
                }
                else
                {
                    throw new Exception("No se encontró el usuario solicitado.");
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al obtener el rol del usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener el rol del usuario: " + ex.Message, ex);
            }
        }


        public int _686DPTraerIntentos(int dNI)
        {
            try
            {
                DataTable dt = new DataTable();
                string consulta = "SELECT DP686_intentos " +
                  "FROM [dbo].[686DP_UsuarioIntentos] " +
                  "WHERE DP686_DNI = @DNI;";

                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", dNI) };
                _686DPDalGeneral dal = new _686DPDalGeneral();
                dt = dal._686DPConsultar(consulta, parametros);
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["DP686_intentos"]);
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener los intentos del usuario.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al intentar obtener intentos.", ex);
            }
        }

        public bool _686DPCambiarcontraseña(int dNI)
        {
            try
            {
                DataTable dt;
                _686DPDalGeneral dal = new _686DPDalGeneral();
                string consulta = "SELECT DP686_CambiarContraseña FROM [dbo].[686DP_Usuario] WHERE DP686_DNI = @DNI";
                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", dNI) };

                dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt.Rows[0]["DP686_CambiarContraseña"]);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void _686DPReestablecerIntentos(int dNI)
        {
            try
            {
                string consulta = $"UPDATE [dbo].[686DP_UsuarioIntentos]\r\n    SET DP686_intentos = 0\r\n    WHERE DP686_DNI = @DNI;";

                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", dNI) };
                _686DPDalGeneral dal = new _686DPDalGeneral();
                dal._686DPEscribir(consulta, parametros);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al reiniciar los intentos del usuario.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al reiniciar intentos.", ex);
            }
        }

        public bool _686DPCuentaBloqueada(int dNI)
        {
            try
            {
                DataTable dt;
                _686DPDalGeneral dal = new _686DPDalGeneral();
                string consulta = "SELECT DP686_Bloqueado FROM [dbo].[686DP_Usuario] WHERE DP686_DNI = @DNI";
                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", dNI) };

                dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt.Rows[0][0]);
                }

                throw new Exception("No se encontró el estado del usuario.");
            }
            catch (Exception)
            {
                throw new Exception($"Error al obtener el estado de actividad del usuario '{dNI}'.");
            }
        }

        public void _686DPCambiarcontraseñaObligatori(int dni)
        {
            try
            {
                string consulta = @"UPDATE [dbo].[686DP_Usuario]
                            SET DP686_CambiarContraseña = 1
                            WHERE DP686_DNI = @DNI";

                ArrayList parametros = new ArrayList
        {
            new SqlParameter("@DNI", dni)
        };

                _686DPDalGeneral dal = new _686DPDalGeneral();
                dal._686DPEscribir(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al establecer el cambio de contraseña obligatorio para el usuario '{dni}': {ex.Message}", ex);
            }
        }

        public void _ReestablecerObligatoriedadeContraseña(int dNI)
        {
            try
            {
                string consulta = @"UPDATE [dbo].[686DP_Usuario]
                            SET DP686_CambiarContraseña = 0
                            WHERE DP686_DNI = @DNI";

                ArrayList parametros = new ArrayList
        {
            new SqlParameter("@DNI", dNI)
        };

                _686DPDalGeneral dal = new _686DPDalGeneral();
                dal._686DPEscribir(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al establecer el cambio de contraseña obligatorio para el usuario '{dNI}': {ex.Message}", ex);
            }
        }

        public string TraerIdiomaUsuario(int dNI)
        {
            try
            {
                string consulta = @"SELECT DP686_Idioma FROM [dbo].[686DP_Usuario] WHERE DP686_DNI = @DNI";
                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@DNI", dNI)
                };

                _686DPDalGeneral dal = new _686DPDalGeneral();
                DataTable dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0]["DP686_Idioma"]);
                }
                else
                {
                    throw new Exception("No se encontró el usuario con el DNI especificado.");
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al obtener el idioma del usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener el idioma del usuario: " + ex.Message, ex);
            }
        }

        public void GuardarIdioma(string _686DPIdioma)
        {
            try
            {
                var usuario = _686DP_Singleton.Instancia.Usuario;

                if (usuario == null)
                    throw new Exception("No hay ningún usuario logueado.");

                string consulta = @"
                UPDATE [dbo].[686DP_Usuario]
                SET DP686_Idioma = @Idioma
                WHERE DP686_DNI = @DNI";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@Idioma", _686DPIdioma),
                    new SqlParameter("@DNI", usuario._686DPDNI)
                };

                _686DPDalGeneral dal = new _686DPDalGeneral();
                dal._686DPEscribir(consulta, parametros); 
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al guardar el idioma del usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al guardar el idioma del usuario: " + ex.Message, ex);
            }
        }

        public _686DP_Usuarios TraerUsuarioCompleto(int dni)
        {
            try
            {
                string consulta = @"
                SELECT 
                    U.DP686_DNI, 
                    U.DP686_Nombre, 
                    U.DP686_Apellido, 
                    U.DP686_Email, 
                    P.DP686_Nombre AS Rol,
                    U.DP686_Usuario, 
                    U.DP686_Contraseña, 
                    U.DP686_Activo, 
                    U.DP686_Bloqueado, 
                    U.DP686_CambiarContraseña,
                    U.DP686_Idioma
                FROM [dbo].[686DP_Usuario] U
                INNER JOIN [dbo].[686DP_Perfil] P ON U.DP686_PerfilID = P.DP686_PerfilID
                WHERE U.DP686_DNI = @DNI";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@DNI", dni)
                };

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    _686DP_Usuarios usuario = new _686DP_Usuarios(
                        Convert.ToInt32(row["DP686_DNI"]),
                        Convert.ToString(row["DP686_Nombre"]),
                        Convert.ToString(row["DP686_Apellido"]),
                        Convert.ToString(row["DP686_Email"]),
                        Convert.ToString(row["Rol"]),
                        Convert.ToString(row["DP686_Usuario"]),
                        Convert.ToString(row["DP686_Contraseña"]),
                        Convert.ToBoolean(row["DP686_Activo"]),
                        Convert.ToBoolean(row["DP686_Bloqueado"]),
                        Convert.ToBoolean(row["DP686_CambiarContraseña"])
                    );

                    usuario.DP686_Idioma = Convert.ToString(row["DP686_Idioma"]);
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al traer usuario completo: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al traer usuario completo: " + ex.Message, ex);
            }
        }
    }
}
