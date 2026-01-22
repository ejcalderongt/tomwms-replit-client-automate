using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Producto
{
    public class clsBeProducto_estado_ubic : ICloneable
    {
        [Column("IdProductoEstadUbic")]
        [DisplayName("IdProductoEstadUbic")]
        public int IdProductoEstadUbic { get; set; } = 0;

        [Column("IdEstado")]
        [DisplayName("IdEstado")]
        public int IdEstado { get; set; } = 0;

        [Column("IdUbicacionDefecto")]
        [DisplayName("IdUbicacionDefecto")]
        public int IdUbicacionDefecto { get; set; } = 0;

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

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        public clsBeProducto_estado_ubic() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
