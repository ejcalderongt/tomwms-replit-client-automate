namespace WMS.EntityCore.Picking
{
    public class clsBeTrans_picking_det_parametros : ICloneable, IDisposable
    {
        public int IdParametroPicking { get; set; } = 0;
        public int IdPickingDet { get; set; } = 0;
        public int IdProductoParametro { get; set; } = 0;
        public string Valor_texto { get; set; } = "";
        public double? Valor_numerico { get; set; }
        public DateTime? Valor_fecha { get; set; }
        public bool? Valor_logico { get; set; }
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public bool IsNew { get; set; } = false;

        public clsBeTrans_picking_det_parametros()
        {
        }

        public clsBeTrans_picking_det_parametros(int IdParametroPicking, int IdPickingDet, int IdProductoParametro, string valor_texto, double? valor_numerico, DateTime? valor_fecha, bool? valor_logico, string user_agr, DateTime fec_agr)
        {
            this.IdParametroPicking = IdParametroPicking;
            this.IdPickingDet = IdPickingDet;
            this.IdProductoParametro = IdProductoParametro;
            this.Valor_texto = valor_texto;
            this.Valor_numerico = valor_numerico;
            this.Valor_fecha = valor_fecha;
            this.Valor_logico = valor_logico;
            this.User_agr = user_agr;
            this.Fec_agr = fec_agr;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Dispose()
        {
            // Dispose implementation if needed
        }
    }
}
