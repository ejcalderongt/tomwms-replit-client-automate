namespace WMS.EntityCore.AcuerdosComerciales
{
    public class clsBeTrans_acuerdoscomerciales_enc : ICloneable
    {
        public int IdAcuerdoEnc { get; set; } = 0;
        public int IdCliente { get; set; } = 0;
        public int? Codigo_acuerdo { get; set; } = null;
        public string Descripcion { get; set; } = "";
        public string Tipo_cobro { get; set; } = "";
        public int? Cod_moneda { get; set; } = null;
        public string Moneda { get; set; } = "";
        public bool? Estado { get; set; } = true;
        public string User_agr { get; set; } = "";
        public DateTime? Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime? Fec_mod { get; set; } = DateTime.Now;
        public DateTime? Fec_erp { get; set; } = null;

        /// <summary>
        /// Campo adicional para control de envío a ERP
        /// </summary>
        public bool Enviado_A_ERP { get; set; } = false;

        /// <summary>
        /// Campo adicional para auditoría interna
        /// </summary>
        public bool Auditado { get; set; } = false;

        public clsBeTrans_acuerdoscomerciales_enc()
        {
        }

        public clsBeTrans_acuerdoscomerciales_enc(
            int idAcuerdoEnc,
            int idCliente,
            string descripcion,
            string moneda,
            string user_agr,
            DateTime fec_agr)
        {
            IdAcuerdoEnc = idAcuerdoEnc;
            IdCliente = idCliente;
            Descripcion = descripcion ?? "";
            Moneda = moneda ?? "";
            User_agr = user_agr ?? "";
            Fec_agr = fec_agr;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}