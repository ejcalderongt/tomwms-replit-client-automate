using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Producto
{
    public class clsBeProducto_presentacionMi3
    {        

        [Column("codigo_presentacion")]
        [DisplayName("codigo_presentacion")]
        public string Codigo_presentacion { get; set; } = "";

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string Codigo_barra { get; set; } = "";

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("codigo_producto")]
        [DisplayName("codigo_producto")]
        public string Codigo_producto { get; set; } = "";       

        [Column("factor")]
        [DisplayName("factor")]
        public double Factor { get; set; } = 0;    

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("EsPallet")]
        [DisplayName("EsPallet")]
        public bool EsPallet { get; set; } = false;

        //[Column("CamasPorTarima")]
        [DisplayName("CamasPorTarima")]
        public double CamasPorTarima { get; set; } = 0;

        //[Column("CajasPorCama")]
        [DisplayName("CajasPorCama")]
        public double CajasPorCama { get; set; } = 0;

        [Column("genera_lp_auto")]
        [DisplayName("genera_lp_auto")]
        public bool Genera_lp_auto { get; set; } = false;
      
        public clsBeProducto_presentacionMi3() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
