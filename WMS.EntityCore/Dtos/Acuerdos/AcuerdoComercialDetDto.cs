namespace WMS.EntityCore.Dtos.Acuerdos
{
    public class AcuerdoComercialDetDto
    {
        public int IdAcuerdoDet { get; set; } = 0;
        public int? IdAcuerdoEnc { get; set; } = null;
        public string codigo_producto { get; set; } = "";
        public string servicio { get; set; } = "";
        public string nemonico { get; set; } = "";
        public int? codigo_acuerdo { get; set; } = null;
        public int? correlativo_detalleacuerdo { get; set; } = null;
        public string descripcion { get; set; } = "";
        public decimal? numero_unidades { get; set; } = 0;
        public decimal? monto { get; set; } = 0;
        public decimal? porcentaje { get; set; } = 0;
        public int? dias_eventos { get; set; } = null;
        public int? corre_cbcatalogoproductos { get; set; } = null;
        public bool? estado { get; set; } = true;
        public byte? prioridad { get; set; } = 0;
        public int? IdBodega { get; set; } = null;
        public int? IdTipoCobro { get; set; } = null;
        public int? user_agr { get; set; } = null;
        public DateTime? fec_agr { get; set; } = DateTime.Now;
        public int? user_mod { get; set; } = null;
        public DateTime? fec_mod { get; set; } = null;
    }
}