using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeTrans_re_op : ICloneable
    {
        [Column("IdOperadorRec")]
        [DisplayName("IdOperadorRec")]
        public int IdOperadorRec { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("IdOperadorBodega")]
        [DisplayName("IdOperadorBodega")]
        public int IdOperadorBodega { get; set; } = 0;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        public clsBeTrans_re_op() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
