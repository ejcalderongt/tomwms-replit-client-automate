using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeProducto_talla_color : ICloneable
    {
        [Column("IdProductoTallaColor")]
        [DisplayName("IdProductoTallaColor")]
        public int IdProductoTallaColor { get; set; } = 0;

        [Column("IdProducto")]
        [DisplayName("IdProducto")]
        public int IdProducto { get; set; } = 0;

        [Column("IdTalla")]
        [DisplayName("IdTalla")]
        public int IdTalla { get; set; } = 0;

        [Column("IdColor")]
        [DisplayName("IdColor")]
        public int IdColor { get; set; } = 0;

        [Column("IdCampaña")]
        [DisplayName("IdCampaña")]
        public int IdCampaña { get; set; } = 0;

        [Column("CodigoSKU")]
        [DisplayName("CodigoSKU")]
        public string CodigoSKU { get; set; } = "";

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

        public clsBeProducto_talla_color() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}