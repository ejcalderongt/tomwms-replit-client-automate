using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Entity.Producto
{
    public class clsBeProducto_tipo : ICloneable
    {
        [Column("IdTipoProducto")]
        [DisplayName("IdTipoProducto")]
        public int IdTipoProducto { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("NombreTipoProducto")]
        [DisplayName("NombreTipoProducto")]
        public string NombreTipoProducto { get; set; } = "";

        [Column("Activo")]
        [DisplayName("Activo")]
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

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        public clsBeProducto_tipo() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}