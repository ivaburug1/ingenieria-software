using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_Dal;

namespace _686DP_MPP
{
    public class _686DP_MPPBackUpRestore
    {
        _686DPDalGeneral DAL = new _686DPDalGeneral();
        public void RealizarBackupBD(string rutaArchivo)
        {
            try
            {
                string rutaEscapada = rutaArchivo.Replace(@"\", @"\\");

                string consulta = $@"
                    BACKUP DATABASE [DBAseguraYADemo]
                    TO DISK = @Ruta
                    WITH INIT;"; 

                ArrayList parametros = new ArrayList
                {
                    new SqlParameter("@Ruta", rutaArchivo)
                };
                DAL._686DPEscribir(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar el backup: " + ex.Message);
            }
        }

        public void RealizarRestoreBD(string rutaArchivoBackup)
        {
            try

            {
                string backup = rutaArchivoBackup;
                string conultarFileList = "RESTORE FILELISTONLY FROM DISK = @Ruta";
                ArrayList parametrosFileList = new ArrayList
                {
                    new SqlParameter("@Ruta", backup)
                };
                DataTable dt = DAL._686DPConsultar(conultarFileList, parametrosFileList);
                if (dt.Rows.Count < 2)
                    throw new Exception("El back up no tiene los datos deseados???");
                string LogicalName = dt.Rows[0]["LogicalName"].ToString();
                string logicalNameLog = dt.Rows[1]["LogicalName"].ToString();
                string consultaPath = @"
                    SELECT SERVERPROPERTY ('InstanceDefaultDataPath') AS DataPath,
                    SERVERPROPERTY ('InstanceDefaultLogPath') AS LogPath;";

                DataTable dtPath = DAL._686DPConsultar(consultaPath, null);
                string mdfPath = Path.Combine(dtPath.Rows[0]["DataPath"].ToString(), "DBAseguraYADemo.mdf");
                string ldfPath = Path.Combine(dtPath.Rows[0]["LogPath"].ToString(), "DBAseguraYADemo.ldf");
                string consultaRestore = @"
                    USE master;
                    RESTORE DATABASE DBAseguraYADemo
                    FROM DISK = @Ruta
                    WITH
                        MOVE @LogicalData TO @Mdf,
                        MOVE @LogicalLog TO @Ldf,
                        REPLACE,
                        STATS = 5;
                        ALTER DATABASE DBAseguraYADemo SET MULTI_USER;
                    ";
                ArrayList parametrosRestore = new ArrayList
                {
                    new SqlParameter("@Ruta", backup),
                    new SqlParameter("@LogicalData",LogicalName),
                    new SqlParameter("@LogicalLog", logicalNameLog),
                    new SqlParameter("@Mdf", mdfPath),
                    new SqlParameter("@Ldf", ldfPath)
                };

                DAL._686DPEscribir(consultaRestore, parametrosRestore);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar el restore: " + ex.Message);
            }
        }
    }
}
