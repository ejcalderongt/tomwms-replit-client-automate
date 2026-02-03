namespace WMS.EntityCore.Dtos.KPI
{
    public class KpiTendenciaDespachoRowDto
    {
        public DateTime Periodo { get; set; }              // Día / inicio de semana / primer día del mes
        public string Nivel { get; set; } = "";            // "PRODUCTO" o "FAMILIA"

        public string? Codigo_Producto { get; set; }       // null cuando Nivel=FAMILIA
        public string? Nombre_Producto { get; set; }       // null cuando Nivel=FAMILIA

        public string Familia { get; set; } = "ND";
        public decimal Cantidad_Despachada { get; set; }
    }
}
