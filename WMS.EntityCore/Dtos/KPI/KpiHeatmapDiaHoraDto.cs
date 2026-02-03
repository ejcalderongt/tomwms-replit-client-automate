namespace WMS.EntityCore.Dtos.KPI
{
    public class KpiHeatmapDiaHoraDto
    {
        public int DiaSemana { get; set; }          // 1..7 (según DATEFIRST)
        public string DiaNombre { get; set; } = ""; // Lunes, Martes, etc.
        public int Hora { get; set; }               // 0..23

        public int Despachos { get; set; }          // COUNT(DISTINCT IdDespachoEnc)
        public int Lineas { get; set; }             // COUNT(*) de líneas
        public decimal Cantidad { get; set; }       // SUM(CantidadDespachada)
    }
}
