using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Producto
{
    public class clsBeProducto_codigos_barra : ICloneable
    {
        [Column("IdProductoCodigoBarra")]
        [DisplayName("IdProductoCodigoBarra")]
        public int IdProductoCodigoBarra { get; set; } = 0;

        [Column("IdProducto")]
        [DisplayName("IdProducto")]
        public int IdProducto { get; set; } = 0;

        [Column("IdProveedor")]
        [DisplayName("IdProveedor")]
        public int IdProveedor { get; set; } = 0;

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string Codigo_barra { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        public clsBeProducto_codigos_barra() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
