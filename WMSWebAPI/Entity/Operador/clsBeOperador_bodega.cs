using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeOperador_bodega : ICloneable
    {
        [Column("IdOperadorBodega")]
        [DisplayName("IdOperadorBodega")]
        public int IdOperadorBodega { get; set; } = 0;

        [Column("IdOperador")]
        [DisplayName("IdOperador")]
        public int IdOperador { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

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

        public clsBeOperador_bodega() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}