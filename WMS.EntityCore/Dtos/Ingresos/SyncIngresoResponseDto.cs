namespace WMS.EntityCore.Dtos.Ingresos
{
    public class IngresoAsociacionResponseDto
    {        
        public string NoDocumentoOrigen { get; set; } = default!;
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public int? IdOrdenCompraEnc { get; set; } = 0;
        public List<IngresoAsociacionLineaDto> Lineas { get; set; } = new();
    }

    public class IngresoAsociacionLineaDto
    {
        public int NoLinea { get; set; }
        public string CodigoArticulo { get; set; } = default!;
        public double CantidadArticulo { get; set; }
        public List<IngresoAsociacionLoteDto> Lotes { get; set; } = new();
    }

    public class IngresoAsociacionLoteDto
    {
        public string CodigoLote { get; set; } = default!;
        public double CantidadLote { get; set; }
    }
}
