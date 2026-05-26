using Newtonsoft.Json;

namespace WMSWebAPI.Dtos.Salidas
{
    public class AnularSalidaRequestDto
    {
        public int? IdPedidoEnc { get; set; }
        public string? Referencia { get; set; }
        public int? IdTipoPedido { get; set; }
        public string? CodigoEmpresaERP { get; set; }
        public int IdMotivoAnulacionBodega { get; set; }
        public string? Usuario { get; set; }
        public string? Observacion { get; set; }
    }

    public class AnularSalidaResultDto
    {
        public bool Exito { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public int IdPedidoEnc { get; set; }
        public string Referencia { get; set; } = string.Empty;
        public string EstadoAnterior { get; set; } = string.Empty;
        public string EstadoActual { get; set; } = string.Empty;
        public int IdMotivoAnulacionBodega { get; set; }
        public int StockReservadoLiberado { get; set; }
        public int PolizasAnuladas { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AnularSalidaFailureDto>? Fallos { get; set; }
    }

    public class AnularSalidaFailureDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public string Campo { get; set; } = string.Empty;
    }
}
