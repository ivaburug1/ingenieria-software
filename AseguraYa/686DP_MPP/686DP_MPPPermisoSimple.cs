using _686DP_BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using _686DP_Dal;
using System.Threading.Tasks;
using _686DP_SERVICIOS;
using _686DP_SERVICIOS.Composite;
using System.Data.SqlClient;

namespace _686DP_MPP
{
    public class _686DP_MPPPermisoSimple
    {
        _686DPDalGeneral dal = new _686DPDalGeneral();

        public List<_686DP_PermisoSimple> TraerPermisoDeFamilia(int idFamilia)
        {
            List<_686DP_PermisoSimple> lista = new List<_686DP_PermisoSimple>();

            try
            {
                string consulta = @"
                SELECT
                ps.[DP686_PermisoID],
                ps.[DP686_Nombre]
                FROM [dbo].[686DP_PermisoSimple] as ps
                INNER JOIN [dbo].[686DP_FamiliaPermiso] as fp
                ON fp.DP686_PermisoID = ps.DP686_PermisoID
                WHERE fp.DP686_FamiliaID = @FamiliaID";


                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@FamiliaID", idFamilia)
                };

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                foreach (DataRow row in dt.Rows)
                {
                    int DP686_PermisoSimpleID = Convert.ToInt32(row["DP686_PermisoID"]);
                    string Nombre = row["DP686_Nombre"].ToString();
                    _686DP_PermisoSimple permiso = new _686DP_PermisoSimple(DP686_PermisoSimpleID, Nombre);
                    
                    lista.Add(permiso);
                }

                return lista;

            }
            catch(Exception ex)
            {
                throw new Exception("Error al traer los permisos simples: " + ex.Message, ex);
            }
        }

        public List<_686DP_PermisoSimple> TraerPermisos()
        {
            try
            {
                string consulta = @"
                SELECT [DP686_PermisoID], DP686_Nombre 
                FROM [dbo].[686DP_PermisoSimple]";

                ArrayList parametros = new ArrayList(); 

                DataTable dt = dal._686DPConsultar(consulta, parametros);

                List<_686DP_PermisoSimple> lista = new List<_686DP_PermisoSimple>();

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["DP686_PermisoID"]);
                    string Nombre = Convert.ToString(row["DP686_Nombre"]);
                    _686DP_PermisoSimple permiso = new _686DP_PermisoSimple(id, Nombre);

                    lista.Add(permiso);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer los permisos simples: " + ex.Message, ex);
            }
        }
    }
}
