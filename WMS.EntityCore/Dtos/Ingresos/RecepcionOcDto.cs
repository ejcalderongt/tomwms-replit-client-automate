namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionOcDto
    {
        public int IdRecepcionOc { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public bool? Recepcion_Ciega { get; set; }
        public bool? Recepcion_Manual { get; set; }
        public string? No_Docto { get; set; }
        public DateTime? Hora_Ini_Hh { get; set; }
        public DateTime? Hora_Fin_Hh { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public byte[]? Firma_Operador { get; set; } = Array.Empty<byte>();
    }
}