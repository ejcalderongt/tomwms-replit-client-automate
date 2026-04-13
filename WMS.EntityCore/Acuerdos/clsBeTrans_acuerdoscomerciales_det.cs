namespace WMS.EntityCore.AcuerdosComerciales
{
    public class clsBeTrans_acuerdoscomerciales_det : ICloneable
    {
        public int IdAcuerdoDet { get; set; } = 0;
        public int? IdAcuerdoEnc { get; set; } = null;
        public string Codigo_producto { get; set; } = "";
        public string Servicio { get; set; } = "";
        public string Nemonico { get; set; } = "";
        public int? Codigo_acuerdo { get; set; } = null;
        public int? Correlativo_detalleacuerdo { get; set; } = null;
        public string Descripcion { get; set; } = "";
        public decimal? Numero_unidades { get; set; } = 0;
        public decimal? Monto { get; set; } = 0;
        public decimal? Porcentaje { get; set; } = 0;
        public int? Dias_eventos { get; set; } = null;
        public int? Corre_cbcatalogoproductos { get; set; } = null;
        public bool? Estado { get; set; } = true;
        public byte? Prioridad { get; set; } = 0;
        public int? IdBodega { get; set; } = null;
        public int? IdTipoCobro { get; set; } = null;
        public int? User_agr { get; set; } = null;
        public DateTime? Fec_agr { get; set; } = DateTime.Now;
        public int? User_mod { get; set; } = null;
        public DateTime? Fec_mod { get; set; } = DateTime.Now;

        /// <summary>
        /// Campo adicional para control de envío a ERP
        /// </summary>
        public bool Enviado { get; set; } = false;

        public clsBeTrans_acuerdoscomerciales_det()
        {
        }

        public clsBeTrans_acuerdoscomerciales_det(
            int idAcuerdoDet,
            string codigo_producto,
            string descripcion,
            int? idAcuerdoEnc = null)
        {
            IdAcuerdoDet = idAcuerdoDet;
            Codigo_producto = codigo_producto ?? "";
            Descripcion = descripcion ?? "";
            IdAcuerdoEnc = idAcuerdoEnc;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}