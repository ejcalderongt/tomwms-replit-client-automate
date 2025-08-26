using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Producto
{
    public class clsBeProducto_clasificacion : ICloneable
    {
        [Column("IdClasificacion")]
        [DisplayName("IdClasificacion")]
        public int IdClasificacion { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

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

        public clsBeProducto_clasificacion() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}