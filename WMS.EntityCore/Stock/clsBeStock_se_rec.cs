using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeStock_se_rec : ICloneable
    {
        [Column("IdStockSeRec")]
        [DisplayName("IdStockSeRec")]
        public int IdStockSeRec { get; set; } = 0;

        [Column("IdStockRec")]
        [DisplayName("IdStockRec")]
        public int IdStockRec { get; set; } = 0;

        [Column("IdProductoBodega")]
        [DisplayName("IdProductoBodega")]
        public int IdProductoBodega { get; set; } = 0;

        [Column("NoSerie")]
        [DisplayName("NoSerie")]
        public string NoSerie { get; set; } = "";

        [Column("NoSerieInicial")]
        [DisplayName("NoSerieInicial")]
        public string NoSerieInicial { get; set; } = "";

        [Column("NoSerieFinal")]
        [DisplayName("NoSerieFinal")]
        public string NoSerieFinal { get; set; } = "";

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

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("regularizado")]
        [DisplayName("regularizado")]
        public bool Regularizado { get; set; } = false;

        [Column("fecha_regularizacion")]
        [DisplayName("fecha_regularizacion")]
        public DateTime Fecha_regularizacion { get; set; } = DateTime.Now;
        public bool IsNew { get; set; } = true;

        public clsBeStock_se_rec() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
