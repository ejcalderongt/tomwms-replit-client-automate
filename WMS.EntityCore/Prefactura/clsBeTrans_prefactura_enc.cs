namespace WMS.EntityCore.Prefactura
{
    public class clsBeTrans_prefactura_enc : ICloneable
    {
        #region Propiedades

        public int IdTransPrefacturaEnc { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdClienteBodega { get; set; } = 0;
        public int IdBodega { get; set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public DateTime Fecha_desde { get; set; } = DateTime.Now;
        public DateTime Fecha_hasta { get; set; } = DateTime.Now;
        public double Tipo_Cambio { get; set; } = 1.0;
        public string Observacion { get; set; } = "";
        public bool procesado_erp { get; set; } = false;
        public bool anulada { get; set; } = false;
        public bool es_consolidador { get; set; } = false;
        public string poliza_oc_numero_orden { get; set; } = "";
        public string poliza_pe_numero_orden { get; set; } = "";
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public int IdTipoCuenta { get; set; } = 0;

        /// <summary>
        /// Indica si es un nuevo registro (para saber si insertar o actualizar)
        /// </summary>
        public bool IsNew { get; set; } = true;

        #endregion

        #region Constructores

        public clsBeTrans_prefactura_enc()
        {
        }

        public clsBeTrans_prefactura_enc(
            int idTransPrefacturaEnc,
            int idPropietarioBodega,
            int idBodega,
            DateTime fecha,
            DateTime fechaDesde,
            DateTime fechaHasta)
        {
            IdTransPrefacturaEnc = idTransPrefacturaEnc;
            IdPropietarioBodega = idPropietarioBodega;
            IdBodega = idBodega;
            Fecha = fecha;
            Fecha_desde = fechaDesde;
            Fecha_hasta = fechaHasta;
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