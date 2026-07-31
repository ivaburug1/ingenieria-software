using System;
using System.Threading;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLLGestionRespaldo
    {
        public void GenerarBackup(string fullPathBak, IProgress<int> progress = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(fullPathBak))
                throw new ArgumentException("Ruta de backup inválida.");

            DAL.BackupDatabase(fullPathBak, p => progress?.Report(p), ct);
        }

        public void RestaurarBackup(string fullPathBak, IProgress<int> progress = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(fullPathBak))
                throw new ArgumentException("Ruta de backup inválida.");

            DAL.RestoreDatabase(fullPathBak, p => progress?.Report(p), ct);
        }
    }
}