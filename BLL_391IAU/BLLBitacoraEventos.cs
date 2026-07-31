using System;
using System.Collections.Generic;
using System.Data;
using BE_391IAU;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLLBitacoraEventos
    {
        public DataTable Consultar(BEFiltroBitacoraEventos filtro)
        {
            if (filtro.Criticidad.HasValue && (filtro.Criticidad < 1 || filtro.Criticidad > 5))
                throw new Exception("La criticidad debe estar entre 1 y 5.");

            if (filtro.FechaDesde.HasValue && filtro.FechaHasta.HasValue &&
                filtro.FechaDesde.Value.Date > filtro.FechaHasta.Value.Date)
                throw new Exception("Fecha Desde no puede ser mayor que Fecha Hasta.");

            return DAL.ConsultarBitacoraEventos(filtro);
        }

        public List<string> ObtenerModulos(string moduloAuditoria)
        {
            return DAL.ObtenerModulosBitacora(moduloAuditoria);
        }

        public void RegistrarEvento(int dni, int criticidad, string modulo, string descripcion)
        {
            if (criticidad < 1 || criticidad > 4)
                throw new Exception("La criticidad debe estar entre 1 y 4.");

            var ev = new BEBitacoraEventos
            {
                DNI_391IAU = dni,
                FechaEvento_391IAU = DateTime.Now,
                Criticidad_391IAU = criticidad,
                Modulo_391IAU = modulo,
                Descripcion_391IAU = descripcion
            };

            DAL.InsertarBitacoraEvento(ev);
        }
    }
}