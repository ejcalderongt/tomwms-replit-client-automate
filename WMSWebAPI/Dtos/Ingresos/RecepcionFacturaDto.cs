namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionFacturaDto
    {
        public int IdFacturaRecepcion { get; set; }
        public int? IdRecepcionEnc { get; set; }
        public int? Orden { get; set; }
        public string? NoFactura { get; set; }
        public string? Observacion { get; set; }

        public DateTime? Fec_Agr { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public string? User_Mod { get; set; }

        public bool? Completa { get; set; }
    }
}