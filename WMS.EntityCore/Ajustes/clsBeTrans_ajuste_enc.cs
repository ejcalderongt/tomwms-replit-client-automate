namespace WMS.EntityCore.Ajustes
{
    public class clsBeTrans_ajuste_enc : ICloneable
    {
        public int Idajusteenc { get; set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int Idusuario { get; set; } = 0;
        public string Referencia { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_agr { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public int IdBodega { get; set; } = 0;
        public int IdProductoFamilia { get; set; } = 0;
        public bool Enviado_A_ERP { get; set; } = false;
        public int IdPropietarioBodega { get; set; } = 0;
        public int Ajuste_Por_Inventario { get; set; } = 0;

        /// <summary>
        /// EJC: Agregó el centro de costo
        /// </summary>
        public int IdCentroCosto { get; set; } = 0;

        /// <summary>
        /// EJC20240719: Auditado, determina si el ajuste ya se validó previo a ser enviado a ERP.
        /// </summary>
        public bool Auditado { get; set; } = false;

        /// <summary>
        /// CKFK20251030: Agregamos estos tres campos para la integración con ERP 
        /// ya que centro de costo maneja centro de costo dirección y departamento
        /// </summary>
        public string Centro_Costo_Erp { get; set; } = "";
        public string Centro_Costo_Dir_Erp { get; set; } = "";
        public string Centro_Costo_Dep_Erp { get; set; } = "";

        public clsBeTrans_ajuste_enc()
        {
        }

        public clsBeTrans_ajuste_enc(
            int idajusteenc,
            DateTime fecha,
            int idusuario,
            string referencia,
            DateTime fec_agr,
            string user_agr,
            DateTime fec_mod,
            string user_mod,
            int idBodega)
        {
            Idajusteenc = idajusteenc;
            Fecha = fecha;
            Idusuario = idusuario;
            Referencia = referencia ?? "";
            Fec_agr = fec_agr;
            User_agr = user_agr ?? "";
            Fec_mod = fec_mod;
            User_mod = user_mod ?? "";
            IdBodega = idBodega;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}