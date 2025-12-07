using _686DP_MPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BLL
{
    public class _686DP_BLLBackUpRestore
    {
        _686DP_MPPBackUpRestore MPPBUR = new _686DP_MPPBackUpRestore();
        public void RealizarBackupBD(string rutaArchivo)
        {
            MPPBUR.RealizarBackupBD(rutaArchivo);
        }

        public void RealizarRestoreBD(string rutaArchivoBackup)
        {
            MPPBUR.RealizarRestoreBD(rutaArchivoBackup);
        }
    }
}
