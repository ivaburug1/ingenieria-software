using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _686DP_Dal;
using System.Threading.Tasks;
using _686DP_SERVICIOS.Composite;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace _686DP_MPP
{
    public class _686DP_MPPPerfil
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();
        _686DP_MPPPermisoSimple MPPPermiso = new _686DP_MPPPermisoSimple();
        _686DP_MPPFamilia mppf = new _686DP_MPPFamilia();

        public void AsociarFamiliaAlPerfil(int perfilID, int familiaID)
        {
            try
            {
                string insertFamilia = @"
                IF NOT EXISTS (
                    SELECT 1 FROM [dbo].[686DP_PerfilFamilia]
                    WHERE DP686_PerfilID = @PerfilID AND DP686_FamiliaID = @FamiliaID
                )
                BEGIN
                    INSERT INTO [dbo].[686DP_PerfilFamilia] ([DP686_PerfilID],[DP686_FamiliaID])
                    VALUES (@PerfilID, @FamiliaID)
                END";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@PerfilID", perfilID),
                    new SqlParameter("@FamiliaID", familiaID)
                };

                dal._686DPEscribir(insertFamilia, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar familia al perfil: " + ex.Message, ex);
            }
        }



        public void AsociarPermisoAlPerfil(int perfilID, int permisoID)
        {
            try
            {
                string insertPermiso = @"
                IF NOT EXISTS (
                    SELECT 1 FROM [dbo].[686DP_PerfilPermiso]
                    WHERE DP686_PerfilID = @PerfilID AND DP686_PermisoID = @PermisoID
                )
                BEGIN
                    INSERT INTO [dbo].[686DP_PerfilPermiso] (DP686_PerfilID, DP686_PermisoID)
                    VALUES (@PerfilID, @PermisoID)
                END";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@PerfilID", perfilID),
                    new SqlParameter("@PermisoID", permisoID)
                };

                dal._686DPEscribir(insertPermiso, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar permiso al perfil: " + ex.Message, ex);
            }
        }



        public int CrearPerfilBase(_686DP_Perfil perfil)
        {
            try
            {
                int idExistente = traerCodigoPerfil(perfil.Nombre);

                if (idExistente > 0)
                {
                    string update = @"
                    UPDATE [dbo].[686DP_Perfil]
                    SET DP686_Nombre = @Nombre
                    WHERE DP686_PerfilID = @ID
                    ";

                    ArrayList parametrosUpdate = new ArrayList
                    {
                        new SqlParameter("@Nombre", perfil.Nombre),
                        new SqlParameter("@ID", idExistente)
                    };

                    dal._686DPEscalar(update, parametrosUpdate);
                    return idExistente;
                }
                else
                {
                    string insert = @"
                    INSERT INTO [dbo].[686DP_Perfil] (DP686_Nombre)
                    OUTPUT INSERTED.DP686_PerfilID
                    VALUES (@Nombre)
                    ";

                    ArrayList parametrosInsert = new ArrayList
                    {
                        new SqlParameter("@Nombre", perfil.Nombre)
                    };

                    return (int)dal._686DPEscalar(insert, parametrosInsert);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear o actualizar el perfil base: " + ex.Message, ex);
            }
        }


        public List<_686DP_Perfil> TraerPerfiles()
        {
            List<_686DP_Perfil> perfiles = new List<_686DP_Perfil>();

            try
            {
                string consulta = @"SELECT [DP686_PerfilID], [DP686_Nombre] 
                            FROM [dbo].[686DP_Perfil]";

                DataTable dtPerfiles = dal._686DPConsultar(consulta, new ArrayList());

                foreach (DataRow row in dtPerfiles.Rows)
                {
                    int perfilID = Convert.ToInt32(row["DP686_PerfilID"]);
                    string nombre = row["DP686_Nombre"].ToString();

                    _686DP_Perfil perfil = new _686DP_Perfil(nombre);

                    List<_686DP_Familia> familias = TraerFamiliasDelPerfil(perfilID);
                    foreach (var fam in familias)
                    {
                        CargarComponentesRecursivos(fam);
                        perfil.AgregarPermiso(fam);
                    }
                    List<_686DP_PermisoSimple> permisosSimples = TraerPermisosSimplesDelPerfil(perfilID);
                    foreach (var permiso in permisosSimples)
                    {
                        perfil.AgregarPermiso(permiso);
                    }

                    perfiles.Add(perfil);
                }

                return perfiles;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer los perfiles: " + ex.Message);
            }
        }

        public List<_686DP_PermisoSimple> TraerPermisosSimplesDelPerfil(int perfilID)
        {

            List<_686DP_PermisoSimple> lista = new List<_686DP_PermisoSimple>();

            try
            {
                string consulta = @"
                SELECT PS.DP686_PermisoID, PS.DP686_Nombre
                FROM [686DP_PerfilPermiso] PP
                JOIN [686DP_PermisoSimple] PS ON PP.DP686_PermisoID = PS.DP686_PermisoID
                WHERE PP.DP686_PerfilID = @PerfilID";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@PerfilID", perfilID)
                };

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["DP686_PermisoID"]);
                    string nombre = row["DP686_Nombre"].ToString();

                    lista.Add(new _686DP_PermisoSimple(id, nombre));
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer los permisos simples del perfil: " + ex.Message);
            }
        }

        private void CargarComponentesRecursivos(_686DP_Familia fam, HashSet<int> visitados = null)
        {
            if (visitados == null)
                visitados = new HashSet<int>();

            if (visitados.Contains(fam.idFamilia))
                return;

            visitados.Add(fam.idFamilia);

            List<_686DP_PermisoSimple> permisos = MPPPermiso.TraerPermisoDeFamilia(fam.idFamilia);
            foreach (var permiso in permisos)
                fam.Agregar(permiso);

            List<_686DP_Familia> familiasHijas = mppf.TraerFamiliasDeFamilia(fam.idFamilia);
            foreach (var subFamilia in familiasHijas)
            {
                CargarComponentesRecursivos(subFamilia, visitados);
                fam.Agregar(subFamilia);
            }
        }

        public List<_686DP_Familia> TraerFamiliasDelPerfil(int perfilID)
        {
            List<_686DP_Familia> lista = new List<_686DP_Familia>();

            try
            {
                string consulta = @"
                SELECT f.DP686_FamiliaID, f.DP686_Nombre
                FROM [dbo].[686DP_Familia] AS f
                INNER JOIN [dbo].[686DP_PerfilFamilia] AS pf
                    ON pf.DP686_FamiliaID = f.DP686_FamiliaID
                WHERE pf.DP686_PerfilID = @PerfilID";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@PerfilID", perfilID)
                };

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                foreach (DataRow row in dt.Rows)
                {
                    int idFamilia = Convert.ToInt32(row["DP686_FamiliaID"]);
                    string Nombre = row["DP686_Nombre"].ToString();
                    _686DP_Familia familia = new _686DP_Familia(Nombre, idFamilia);
                    lista.Add(familia);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer familias del perfil: " + ex.Message);
            }
        }

        public bool ValidarUnico(Func<string> toLower)
        {
            try
            {
                string nombre = toLower();

                string consulta = @"
                SELECT COUNT(*) 
                FROM [dbo].[686DP_Perfil] 
                WHERE LOWER(DP686_Nombre) = @Nombre";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@Nombre", nombre)
                };

                int cantidad = (int)dal._686DPEscalar(consulta, parametros);
                return cantidad > 0; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar unicidad del perfil: " + ex.Message, ex);
            }
        }

        public void EliminarRelacionPerfilFamilia(string PermisoNombre, int familiaID)
        {
            try
            {
                string consultaID = @"
                SELECT DP686_PerfilID 
                FROM [dbo].[686DP_Perfil] 
                WHERE LOWER(DP686_Nombre) = @Nombre";

                ArrayList parametrosID = new ArrayList
                {
                    new SqlParameter("@Nombre", PermisoNombre.ToLower())
                };

                int perfilID = (int)dal._686DPEscalar(consultaID, parametrosID);

                string deleteQuery = @"
                DELETE FROM [dbo].[686DP_PerfilFamilia] 
                WHERE DP686_PerfilID = @PerfilID AND DP686_FamiliaID = @FamiliaID";

                ArrayList parametrosDelete = new ArrayList
                {
                    new SqlParameter("@PerfilID", perfilID),
                    new SqlParameter("@FamiliaID", familiaID)
                };

                dal._686DPEscribir(deleteQuery, parametrosDelete);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar relación Perfil-Familia: " + ex.Message, ex);
            }
        }

        public void EliminarRelacionPerfilPermiso(string nombre, int dP686_PermisoSimpleID)
        {
            try
            {
                string consultaID = @"
                SELECT DP686_PerfilID 
                FROM [dbo].[686DP_Perfil] 
                WHERE LOWER(DP686_Nombre) = @Nombre";

                ArrayList parametrosID = new ArrayList
                {
                    new SqlParameter("@Nombre", nombre.ToLower())
                };

                int perfilID = (int)dal._686DPEscalar(consultaID, parametrosID);

                string deleteQuery = @"
                DELETE FROM [dbo].[686DP_PerfilPermiso] 
                WHERE DP686_PerfilID = @PerfilID AND DP686_PermisoID = @PermisoID";

                ArrayList parametrosDelete = new ArrayList
                {
                    new SqlParameter("@PerfilID", perfilID),
                    new SqlParameter("@PermisoID", dP686_PermisoSimpleID)
                };

                dal._686DPEscribir(deleteQuery, parametrosDelete);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar relación Perfil-Permiso: " + ex.Message, ex);
            }
        }

        public void EliminarPerfil(string nombrePerfil)
        {
            try
            {
                string obtenerID = @"
                SELECT DP686_PerfilID 
                FROM [dbo].[686DP_Perfil] 
                WHERE LOWER(DP686_Nombre) = @Nombre";

                ArrayList parametrosID = new ArrayList
                {
                    new SqlParameter("@Nombre", nombrePerfil.ToLower())
                };

                int perfilID = (int)dal._686DPEscalar(obtenerID, parametrosID);

                string eliminarPerfil = @"
                DELETE FROM [dbo].[686DP_Perfil]
                WHERE DP686_PerfilID = @ID";

                ArrayList parametrosDelete = new ArrayList
                {
                new SqlParameter("@ID", perfilID)
                };

                dal._686DPEscribir(eliminarPerfil, parametrosDelete);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el perfil: " + ex.Message, ex);
            }
        }

        public _686DP_Perfil TraerPerfil(string nombre)
        {
            string query = "SELECT * FROM [686DP_Perfil] WHERE LOWER(DP686_Nombre) = @Nombre";
            ArrayList parametros = new ArrayList
            {
                new SqlParameter("@Nombre", nombre.ToLower())
            };

            DataTable dt = dal._686DPConsultar(query, parametros);
            if (dt.Rows.Count == 0)
                throw new Exception("Perfil no encontrado en la base de datos.");

            DataRow row = dt.Rows[0];
            return new _686DP_Perfil(row["DP686_Nombre"].ToString());
        }

        public int traerCodigoPerfil(string nombre)
        {
            string query = "SELECT DP686_PerfilID FROM [686DP_Perfil] WHERE LOWER(DP686_Nombre) = @Nombre";
            ArrayList parametros = new ArrayList
    {
        new SqlParameter("@Nombre", nombre.ToLower())
    };

            object resultado = dal._686DPEscalar(query, parametros);
            if (resultado == null || resultado == DBNull.Value)
                return 0;

            return Convert.ToInt32(resultado);
        }

        public _686DP_Perfil TraerPerfilUsuario(int dni)
        {
            try
            {
                string query = "SELECT [DP686_PerfilID] FROM [dbo].[686DP_Usuario] WHERE DP686_DNI = @DNI";
                ArrayList parametros = new ArrayList { new SqlParameter("@DNI", dni) };
                object resultado = dal._686DPEscalar(query, parametros);

                if (resultado == null || resultado == DBNull.Value)
                    throw new Exception("El usuario no tiene perfil asignado.");

                int perfilID = Convert.ToInt32(resultado);


                string queryPerfil = "SELECT [DP686_Nombre] FROM [dbo].[686DP_Perfil] WHERE DP686_PerfilID = @ID";
                ArrayList paramPerfil = new ArrayList { new SqlParameter("@ID", perfilID) };
                DataTable dt = dal._686DPConsultar(queryPerfil, paramPerfil);
                if (dt.Rows.Count == 0)
                    throw new Exception("Perfil no encontrado.");

                string nombrePerfil = dt.Rows[0]["DP686_Nombre"].ToString();
                _686DP_Perfil perfil = new _686DP_Perfil(nombrePerfil);


                List<_686DP_Familia> familias = TraerFamiliasDelPerfil(perfilID);
                foreach (var familia in familias)
                {
                    CargarComponentesRecursivos(familia); 
                    perfil.AgregarPermiso(familia);       
                }

                List<_686DP_PermisoSimple> permisosSimples = TraerPermisosSimplesDelPerfil(perfilID);
                foreach (var permiso in permisosSimples)
                {
                    perfil.AgregarPermiso(permiso);
                }

                return perfil;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer el perfil del usuario: " + ex.Message, ex);
            }
        }

        public List<_686DP_Perfil> TraerPerfilesDeFamilia(int idFamilia)
        {
            try
            {
                string consulta = @"
                SELECT P.DP686_PerfilID, P.DP686_Nombre
                FROM [dbo].[686DP_PerfilFamilia] AS PF
                INNER JOIN [dbo].[686DP_Perfil] AS P 
			        ON PF.DP686_PerfilID = P.DP686_PerfilID
                WHERE PF.DP686_FamiliaID = @idFamilia";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@idFamilia", idFamilia)
                };

                DataTable dt = dal._686DPConsultar(consulta, parametros);
                List<_686DP_Perfil> lista = new List<_686DP_Perfil>();

                foreach (DataRow row in dt.Rows)
                {
                    _686DP_Perfil perfil = new _686DP_Perfil(row["DP686_Nombre"].ToString());
                    lista.Add(perfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer los perfiles de la familia: " + ex.Message, ex);
            }
        }
    }
}
