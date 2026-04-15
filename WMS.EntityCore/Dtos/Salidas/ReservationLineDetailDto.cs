using Newtonsoft.Json;

namespace WMSWebAPI.Dtos.Salidas
{
    public class ReservationLineDetailDto
    {
        public int LineNo { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public double QuantityRequested { get; set; }
        public double QuantityReserved { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<ReservationDetailDto> Reservations { get; set; } = new();
    }

    public class ReservationDetailDto
    {
        public string LotNo { get; set; } = string.Empty;
        public string ExpirationDate { get; set; } = string.Empty;
        public string LocationCode { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public int IdStock { get; set; }
        public int IdStockRes { get; set; }
    }

    public class LineaFalloDto
    {
        [JsonProperty("no_linea")]
        public int NoLinea { get; set; }

        [JsonProperty("codigo_producto")]
        public string CodigoProducto { get; set; } = string.Empty;

        [JsonProperty("variante")]
        public string Variante { get; set; } = string.Empty;

        [JsonProperty("unidad_medida")]
        public string UnidadMedida { get; set; } = string.Empty;

        [JsonProperty("solicitado")]
        public double Solicitado { get; set; }

        [JsonProperty("reservado")]
        public double Reservado { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; } = "Error";

        [JsonProperty("razon_fallo")]
        public string RazonFallo { get; set; } = string.Empty;
    }

    public class MI3ProcessingResultDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public int LineasProcesadas { get; set; }
        public string Resultado { get; set; } = string.Empty;
        public double TotalSolicitado { get; set; }
        public double TotalReservado { get; set; }
        public List<ReservationLineDetailDto> LineasDetalle { get; set; } = new();
        public List<LineaFalloDto> LineasFallo { get; set; } = new();
    }
}
