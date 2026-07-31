using System;
using System.Collections.Generic;
using System.Linq;
using DAL_391IAU;
using BE_391IAU;

namespace BLL_391IAU
{
    public class BLLDigitoVerificador
    {
        public IReadOnlyList<string> ObtenerTablasConfiguradas()
        {
            return DAL.ObtenerClavesTablasDV();
        }

        public void RecalcularTabla(string claveTabla)
        {
            DAL.RecalcularDVPorClave(claveTabla);
        }

        public void RecalcularTodas()
        {
            DAL.RecalcularDVDeTodasLasTablas();
        }

        public bool ValidarTabla(string claveTabla)
        {
            return DAL.ValidarTablaPorClave(claveTabla);
        }

        public List<(string ClaveTabla, bool Ok, string Error)> ValidarTodas()
        {
            return DAL.ValidarTodasLasTablasDV();
        }
        public void ValidarIntegridadDVOrThrow()
        {
            var resultados = DAL.ValidarTodasLasTablasDV();

            var fallas = resultados
                .Where(r => r.Ok == false)
                .ToList();

            if (fallas.Count > 0)
            {
                string detalle = string.Join(" | ",
                    fallas.Select(f => $"{f.ClaveTabla}: {f.Error ?? "DV inconsistente"}"));

                throw new Exception("Se detectó una inconsistencia en el Dígito Verificador. " + detalle);
            }
        }

        public List<BECambioDetectado> DetectarCambios()
        {
            return DAL_391IAU.DAL.DetectarCambios();
        }
    }
}