using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_re_fact : ICloneable
    {
        [Column("IdFacturaRecepcion")]
        [DisplayName("IdFacturaRecepcion")]
        public int IdFacturaRecepcion { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("Orden")]
        [DisplayName("Orden")]
        public int Orden { get; set; } = 0;

        [Column("NoFactura")]
        [DisplayName("NoFactura")]
        public string NoFactura { get; set; } = "";

        [Column("Observacion")]
        [DisplayName("Observacion")]
        public string Observacion { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("Completa")]
        [DisplayName("Completa")]
        public bool Completa { get; set; } = false;

        public clsBeTrans_re_fact() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}