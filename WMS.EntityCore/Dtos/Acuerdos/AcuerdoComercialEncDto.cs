namespace WMS.EntityCore.Dtos.Acuerdos
{
    public class AcuerdoComercialEncDto
    {
        public int IdAcuerdoEnc { get; set; } = 0;
        public int IdCliente { get; set; } = 0;
        public int? codigo_acuerdo { get; set; } = null;
        public string descripcion { get; set; } = "";
        public string tipo_cobro { get; set; } = "";
        public int? cod_moneda { get; set; } = null;
        public string moneda { get; set; } = "USD";
        public bool? estado { get; set; } = true;
        public string user_agr { get; set; } = "";
        public DateTime? fec_agr { get; set; } = DateTime.Now;
        public string user_mod { get; set; } = "";
        public DateTime? fec_mod { get; set; } = null;
        public DateTime? fec_erp { get; set; } = null;
        public List<AcuerdoComercialDetDto> Detalles { get; set; } = new();
    }
}