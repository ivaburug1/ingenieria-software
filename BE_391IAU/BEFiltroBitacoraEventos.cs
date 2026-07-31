using System;

namespace BE_391IAU
{
    public class BEFiltroBitacoraEventos
    {
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        public string Modulo { get; set; }
        public int? Criticidad { get; set; }

        public int? DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}