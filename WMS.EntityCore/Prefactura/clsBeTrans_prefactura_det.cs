namespace WMS.EntityCore.Prefactura
{
    public class clsBeTrans_prefactura_det : ICloneable
    {
        #region Propiedades

        public int IdTransPrefacturaDet { get; set; } = 0;
        public int IdTransPrefacturaEnc { get; set; } = 0;
        public int IdAcuerdoEnc { get; set; } = 0;
        public int Codigo_acuerdo_enc { get; set; } = 0;
        public string Codigo_producto_acuerdo_det { get; set; } = "";
        public int IdAcuerdoDet { get; set; } = 0;
        public int Correlativo_detalle_acuerdo { get; set; } = 0;
        public decimal Numero_unidades_acuerdo_det { get; set; } = 0;
        public double Servicio { get; set; } = 0;
        public string Descripcion { get; set; } = "";
        public double Monto { get; set; } = 0;
        public double Porcentaje { get; set; } = 0;
        public int Dias_eventos { get; set; } = 0;
        public double Valor { get; set; } = 0;
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
        public double Monto_Erp { get; set; } = 0;

        #endregion

        #region Constructores

        public clsBeTrans_prefactura_det()
        {
        }

        public clsBeTrans_prefactura_det(
            int idTransPrefacturaDet,
            int idTransPrefacturaEnc,
            int idAcuerdoEnc,
            string codigoProducto)
        {
            IdTransPrefacturaDet = idTransPrefacturaDet;
            IdTransPrefacturaEnc = idTransPrefacturaEnc;
            IdAcuerdoEnc = idAcuerdoEnc;
            Codigo_producto_acuerdo_det = codigoProducto ?? "";
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}