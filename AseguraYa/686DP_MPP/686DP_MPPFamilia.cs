using _686DP_SERVICIOS.Composite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using _686DP_Dal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _686DP_MPP
{
    public class _686DP_MPPFamilia
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();

        public void AsociarPermisoAFamilia(int familiaID, int dP686_PermisoSimpleID)
        {
            try
            {
                string existeQuery = @"
                SELECT COUNT(*) FROM [dbo].[686DP_FamiliaPermiso]
                WHERE DP686_FamiliaID = @FamiliaID AND DP686_PermisoID = @PermisoID";

                ArrayList parametrosExiste = new ArrayList
                {
                    new SqlParameter("@FamiliaID", familiaID),
                    new SqlParameter("@PermisoID", dP686_PermisoSimpleID)
                };

                int existe = (int)dal._686DPEscalar(existeQuery, parametrosExiste);
                if (existe > 0) return;

                string insertQuery = @"
                INSERT INTO [dbo].[686DP_FamiliaPermiso] (DP686_FamiliaID, DP686_PermisoID)
                VALUES (@FamiliaID, @PermisoID)";

                dal._686DPEscribir(insertQuery, parametrosExiste);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar el permiso a la familia: " + ex.Message, ex);
            }
        }

        public void AsociarSubfamiliaAFamilia(int familiaID, int idFamilia)
        {
            try
            {
                string validarQuery = @"
                SELECT COUNT(*) FROM [dbo].[686FP_FamiliaFamilia] 
                WHERE DP686_FamiliaPadre = @PadreID AND DP686_Componentes = @HijaID";

                ArrayList parametrosValidar = new ArrayList
                {
                    new SqlParameter("@PadreID", familiaID),
                    new SqlParameter("@HijaID", idFamilia)
                };

                int existe = (int)dal._686DPEscalar(validarQuery, parametrosValidar);
                if (existe > 0) return;

                string query = @"
                INSERT INTO [dbo].[686FP_FamiliaFamilia] (DP686_FamiliaPadre, DP686_Componentes)
                VALUES (@PadreID, @HijaID)";

                dal._686DPEscribir(query, parametrosValidar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar subfamilia a la familia: " + ex.Message, ex);
            }
        }

        public int crearFamilia(_686DP_Familia familia)
        {
            try
            {
                string buscarID = @"
                SELECT DP686_FamiliaID 
                FROM [dbo].[686DP_Familia] 
                WHERE LOWER(DP686_Nombre) = @Nombre";

                ArrayList parametrosBuscar = new ArrayList
                {
                    new SqlParameter("@Nombre", familia.Nombre.ToLower())
                };

                object resultado = dal._686DPEscalar(buscarID, parametrosBuscar);

                if (resultado != null)
                { 
                    int idExistente = (int)resultado;
                    string update = @"
                    UPDATE [dbo].[686DP_Familia] 
                    SET DP686_Profundidad = @Profundidad
                    WHERE DP686_FamiliaID = @ID";

                    ArrayList parametrosUpdate = new ArrayList
                    {
                        new SqlParameter("@Profundidad", "0"),
                        new SqlParameter("@ID", idExistente)
                    };
                        
                    dal._686DPEscribir(update, parametrosUpdate);
                    return idExistente;
                }
                else
                {
                    string insert = @"
                    INSERT INTO [dbo].[686DP_Familia] (DP686_Nombre, DP686_Profundidad)
                    OUTPUT INSERTED.DP686_FamiliaID
                    VALUES (@Nombre, @Profundidad)";

                    ArrayList parametrosInsert = new ArrayList
                    {
                        new SqlParameter("@Nombre", familia.Nombre),
                        new SqlParameter("@Profundidad", "0")
                    };

                    int nuevoID = (int)dal._686DPEscalar(insert, parametrosInsert);
                    return nuevoID;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear o actualizar la familia: " + ex.Message, ex);
            }
        }

        public void EliminarRelacionFamiliaPermiso(int idFamilia, int dP686_PermisoSimpleID)
        {
            try
            {
                string query = @"
                DELETE FROM [dbo].[686DP_FamiliaPermiso]
                WHERE DP686_FamiliaID = @FamiliaID AND [DP686_PermisoID] = @PermisoID";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@FamiliaID", idFamilia),
                    new SqlParameter("@PermisoID", dP686_PermisoSimpleID)
                };

                dal._686DPEscribir(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar relación Familia-Permiso: " + ex.Message, ex);
            }
        }

        public void EliminarFamiliaPorID(int idFamilia)
        {
            try
            {
                string query = "DELETE FROM [dbo].[686DP_Familia] WHERE DP686_FamiliaID = @ID";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@ID", idFamilia)
                };

                dal._686DPEscribir(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la familia: " + ex.Message, ex);
            }
        }

        public void ModificarProfundidad(int familiaID)
        {
            try
            {
                string query = @"
                UPDATE [dbo].[686DP_Familia]
                SET DP686_Profundidad = 1
                WHERE DP686_FamiliaID = @FamiliaID";

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@FamiliaID", familiaID)
                };

                dal._686DPEscribir(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la profundidad de la familia: " + ex.Message, ex);
            }
        }

        public List<_686DP_Familia> TraerFamilia()
        {
            try
            {
                string consulta = @"
                SELECT [DP686_FamiliaID]
                ,[DP686_Nombre]
                ,[DP686_Profundidad]
                FROM [dbo].[686DP_Familia]";

                ArrayList parametros = new ArrayList();

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                List<_686DP_Familia> lista = new List<_686DP_Familia>();

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["DP686_FamiliaID"]);
                    string Nombre = Convert.ToString(row["DP686_Nombre"]);
                    _686DP_Familia familia = new _686DP_Familia(Nombre, id);

                    lista.Add(familia);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer los permisos simples: " + ex.Message, ex);
            }
        }

        public List<_686DP_Familia> TraerFamiliasDeFamilia(int idFamilia)
        {
            try
            {
                List<_686DP_Familia> lista = new List<_686DP_Familia>();
                string consulta = @"
                SELECT
                f.DP686_FamiliaID,
                f.DP686_Nombre
                FROM [dbo].[686DP_Familia] AS f
                INNER JOIN [dbo].[686FP_FamiliaFamilia] AS ff
                    ON ff.DP686_Componentes = f.DP686_FamiliaID
                WHERE ff.DP686_FamiliaPadre = @FamiliaID";
                ArrayList parametros = new ArrayList
            {
                new SqlParameter("@FamiliaID", idFamilia)
            };

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                foreach (DataRow fila in dt.Rows)
                {
                    int idFami = Convert.ToInt32(fila["DP686_FamiliaID"]);
                    string Nombre = fila["DP686_Nombre"].ToString();
                    _686DP_Familia familia = new _686DP_Familia(Nombre, idFami);

                    lista.Add(familia);
                }

                return lista;
            }
            catch(Exception ex)
            {
                throw new Exception("Error al traer las familias hijas: " + ex.Message, ex);
            }
        }

        public _686DP_Familia TraerFamiliaPorID(int idFamilia)
        {
            try
            {
                string consulta = "SELECT [DP686_Nombre] FROM [dbo].[686DP_Familia] WHERE [DP686_FamiliaID] = @ID";
                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@ID", idFamilia)
                };

                DataTable dt = dal._686DPConsultar(consulta, parametros);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new _686DP_Familia(row["DP686_Nombre"].ToString(), idFamilia);
                }

                throw new Exception("No se encontró la familia.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la familia: " + ex.Message, ex);
            }
        }

        public List<_686DP_Composite> TraerHijosFamilia(int idFamilia)
        {
            List<_686DP_Composite> hijos = new List<_686DP_Composite>();

            string consultaFamilias = @"
                SELECT f.DP686_FamiliaID, f.DP686_Nombre
                FROM [dbo].[686FP_FamiliaFamilia] AS ff
                JOIN [dbo].[686DP_Familia] AS f ON ff.DP686_Componentes = f.DP686_FamiliaID
                WHERE ff.DP686_FamiliaPadre = @ID";
            var param = new ArrayList { new SqlParameter("@ID", idFamilia) };
            var dtFam = dal._686DPConsultar(consultaFamilias, param);

            foreach (DataRow row in dtFam.Rows)
            {
                var subFamilia = TraerFamiliaPorID(Convert.ToInt32(row["DP686_FamiliaID"]));
                hijos.Add(subFamilia);
            }
            string consultaPermisos = @"
                SELECT p.DP686_PermisoID, p.DP686_Nombre
                FROM [dbo].[686DP_FamiliaPermiso] AS fp
                JOIN [dbo].[686DP_PermisoSimple] AS p ON fp.DP686_PermisoID = p.DP686_PermisoID
                WHERE fp.DP686_FamiliaID = @ID";
            var dtPerm = dal._686DPConsultar(consultaPermisos, param);

            foreach (DataRow row in dtPerm.Rows)
            {
                hijos.Add(new _686DP_PermisoSimple(Convert.ToInt32(row["DP686_PermisoID"]), row["DP686_Nombre"].ToString()));
            }
            return hijos;
        }

        public void EliminarRelacionFamiliaFamilia(_686DP_Familia fam, int idFamilia)
        {
            try
            {
                string delete = @"
                DELETE FROM [dbo].[686FP_FamiliaFamilia]
                WHERE DP686_FamiliaPadre = @PadreID AND DP686_Componentes = @HijoID";

                ArrayList parametrosDelete = new ArrayList
                {
                    new SqlParameter("@PadreID", fam.idFamilia),
                    new SqlParameter("@HijoID", idFamilia)
                };

                dal._686DPEscribir(delete, parametrosDelete);

                string check = @"
                 SELECT COUNT(*) FROM [dbo].[686FP_FamiliaFamilia]
                 WHERE DP686_FamiliaPadre = @PadreID";

                ArrayList parametrosCheck = new ArrayList
                {
                    new SqlParameter("@PadreID", fam.idFamilia)
                };

                int cantidadHijos = Convert.ToInt32(dal._686DPEscalar(check, parametrosCheck));

                if (cantidadHijos == 0)
                {
                    string update = @"
                    UPDATE [dbo].[686DP_Familia]
                    SET DP686_Profundidad = 0
                    WHERE DP686_FamiliaID = @PadreID";

                    dal._686DPEscribir(update, parametrosCheck);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar relación entre familias: " + ex.Message, ex);
            }
        }

        public bool ValidarUnico(Func<string> value)
        {
            try
            {
                string nombre = value();

                string consulta = @"
                SELECT COUNT(*) 
                FROM [dbo].[686DP_Familia]
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
    }
}
