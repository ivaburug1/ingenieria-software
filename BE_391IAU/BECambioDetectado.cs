namespace BE_391IAU
{
    public struct BECambioDetectado
    {
        public string ClaveTabla { get; set; }
        public string TipoCambio { get; set; }   // "Insercion", "Eliminacion", "Edicion"
        public string ClavePrimaria { get; set; } // PK de la fila afectada (null para Eliminacion/Insercion)
        public int FilasEsperadas { get; set; }
        public int FilasActuales { get; set; }
    }
}
