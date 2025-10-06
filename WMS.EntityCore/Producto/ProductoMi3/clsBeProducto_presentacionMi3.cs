using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Producto
{
    public class clsBeProducto_presentacionMi3
    {

        //[Column("IdPresentacion")]
        //[DisplayName("IdPresentacion")]
        //public int IdPresentacion { get; set; } = 0;

        //[Column("IdProducto")]
        //[DisplayName("IdProducto")]
        //public int IdProducto { get; set; } = 0;

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

        //[Column("imprime_barra")]
        //[DisplayName("imprime_barra")]
        //public bool Imprime_barra { get; set; } = false;

        //[Column("peso")]
        //[DisplayName("peso")]
        //public double Peso { get; set; } = 0;

        //[Column("alto")]
        //[DisplayName("alto")]
        //public double Alto { get; set; } = 0;

        //[Column("largo")]
        //[DisplayName("largo")]
        //public double Largo { get; set; } = 0;

        //[Column("ancho")]
        //[DisplayName("ancho")]
        //public double Ancho { get; set; } = 0;

        [Column("factor")]
        [DisplayName("factor")]
        public double Factor { get; set; } = 0;

        //[Column("MinimoExistencia")]
        //[DisplayName("MinimoExistencia")]
        //public double MinimoExistencia { get; set; } = 0;

        //[Column("MaximoExistencia")]
        //[DisplayName("MaximoExistencia")]
        //public double MaximoExistencia { get; set; } = 0;

        //[Column("user_agr")]
        //[DisplayName("user_agr")]
        //public string User_agr { get; set; } = "";

        //[Column("fec_agr")]
        //[DisplayName("fec_agr")]
        //public DateTime Fec_agr { get; set; } = DateTime.Now;

        //[Column("user_mod")]
        //[DisplayName("user_mod")]
        //public string User_mod { get; set; } = "";

        //[Column("fec_mod")]
        //[DisplayName("fec_mod")]
        //public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("EsPallet")]
        [DisplayName("EsPallet")]
        public bool EsPallet { get; set; } = false;

        //[Column("Precio")]
        //[DisplayName("Precio")]
        //public double Precio { get; set; } = 0;

        //[Column("MinimoPeso")]
        //[DisplayName("MinimoPeso")]
        //public double MinimoPeso { get; set; } = 0;

        //[Column("MaximoPeso")]
        //[DisplayName("MaximoPeso")]
        //public double MaximoPeso { get; set; } = 0;

        //[Column("Costo")]
        //[DisplayName("Costo")]
        //public double Costo { get; set; } = 0;

        //[Column("CamasPorTarima")]
        //[DisplayName("CamasPorTarima")]
        //public double CamasPorTarima { get; set; } = 0;

        //[Column("CajasPorCama")]
        //[DisplayName("CajasPorCama")]
        //public double CajasPorCama { get; set; } = 0;

        [Column("genera_lp_auto")]
        [DisplayName("genera_lp_auto")]
        public bool Genera_lp_auto { get; set; } = false;

        //[Column("permitir_paletizar")]
        //[DisplayName("permitir_paletizar")]
        //public bool Permitir_paletizar { get; set; } = false;

        //[Column("sistema")]
        //[DisplayName("sistema")]
        //public bool Sistema { get; set; } = false;

        //[Column("IdPresentacionPallet")]
        //[DisplayName("IdPresentacionPallet")]
        //public int IdPresentacionPallet { get; set; } = 0;

      
        public clsBeProducto_presentacionMi3() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
