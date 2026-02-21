namespace WMS.EntityCore.Dtos.Cambio_Estado
{    
    public class CambioEstadoEncabezadoDto
    {
        public int IdTareaUbicacionEnc { get; set; }
        public string Bodega { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public List<CambioEstadoDetalleDto> Detalles { get; set; } = new();
    }
    public class CambioEstadoDetalleDto
    {
        // Producto
        public string CodigoProducto { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;

        // Unidad de Medida
        public string UMBas { get; set; } = string.Empty;

        // Presentación
        public string Presentacion { get; set; } = string.Empty;
        public double Cantidad { get; set; } = 0;

        // Trazabilidad
        public string Lote { get; set; } = string.Empty;
        public string Licencia { get; set; } = string.Empty;
        public DateOnly? FechaVence { get; set; }
        public DateOnly? FechaProduccion { get; set; }
        public DateOnly? FechaIngreso { get; set; }
        // Estados
        public string EstadoOrigen { get; set; } = string.Empty;
        public string EstadoDestino { get; set; } = string.Empty;

        // Ubicaciones
        public string UbicacionOrigen { get; set; } = string.Empty;
        public string UbicacionDestino { get; set; } = string.Empty;
    }
}
