namespace WMSWebAPI.Dtos.WebResponseDto
{
    public class ReEncWebResponseDto
    {
        public int IdRecepcion { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string Usuario { get; set; } = "";
        public List<RecDetWRDto> Detalles { get; set; } = new();
    }
    public class RecDetWRDto
    {
        public int no_Linea { get; set; }
        public string codigo_producto { get; set; } = "";
        public string nombre_producto { get; set; } = "";
        public double cantidad_recibida { get; set; }
        public string nombre_unidad_medida { get; set; } = "";
        public string nombre_producto_estado { get; set; } = "";
        public string Lote { get; set; } = "";
        public DateTime? fecha_vence { get; set; }
        public DateTime? fecha_ingreso { get; set; }
        public double peso { get; set; }
        public string lic_plate { get; set; } = "";
    }
}