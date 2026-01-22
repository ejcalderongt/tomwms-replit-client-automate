using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_re_det_parametros : ICloneable
    {
        [Column("IdParametroDet")]
        [DisplayName("IdParametroDet")]
        public int IdParametroDet { get; set; } = 0;

        [Column("IdRecepcionDet")]
        [DisplayName("IdRecepcionDet")]
        public int IdRecepcionDet { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("IdProductoParametro")]
        [DisplayName("IdProductoParametro")]
        public int IdProductoParametro { get; set; } = 0;

        [Column("valor_texto")]
        [DisplayName("valor_texto")]
        public string Valor_texto { get; set; } = "";

        [Column("valor_numerico")]
        [DisplayName("valor_numerico")]
        public double Valor_numerico { get; set; } = 0;

        [Column("valor_fecha")]
        [DisplayName("valor_fecha")]
        public DateTime Valor_fecha { get; set; } = DateTime.Now;

        [Column("valor_logico")]
        [DisplayName("valor_logico")]
        public bool Valor_logico { get; set; } = false;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public bool IsNew { get; set; } = true;

        public clsBeTrans_re_det_parametros() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
