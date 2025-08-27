using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Entity.Producto
{
    public class clsBeProducto_estado : ICloneable
    {
        [Column("IdEstado")]
        [DisplayName("IdEstado")]
        public int IdEstado { get; set; } = 0;

        [Column("IdPropietario")]
        [DisplayName("IdPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("IdUbicacionDefecto")]
        [DisplayName("IdUbicacionDefecto")]
        public int IdUbicacionDefecto { get; set; } = 0;

        [Column("utilizable")]
        [DisplayName("utilizable")]
        public bool Utilizable { get; set; } = false;

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

        [Column("dañado")]
        [DisplayName("dañado")]
        public bool Dañado { get; set; } = false;

        [Column("codigo_bodega_erp")]
        [DisplayName("codigo_bodega_erp")]
        public string Codigo_bodega_erp { get; set; } = "";

        [Column("sistema")]
        [DisplayName("sistema")]
        public bool Sistema { get; set; } = false;

        [Column("dias_vencimiento_clasificacion")]
        [DisplayName("dias_vencimiento_clasificacion")]
        public int Dias_vencimiento_clasificacion { get; set; } = 0;

        [Column("tolerancia_dias_vencimiento")]
        [DisplayName("tolerancia_dias_vencimiento")]
        public int Tolerancia_dias_vencimiento { get; set; } = 0;

        public clsBeProducto_estado() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
