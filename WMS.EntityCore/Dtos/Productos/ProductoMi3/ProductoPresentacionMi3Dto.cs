using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.EntityCore.Dtos.Catalogos
{
    public class ProductoPresentacionMi3Dto
    {
        //[Column("IdPresentacion")]
        //[DisplayName("IdPresentacion")]
        //public int IdPresentacion { get; set; } = 0;

        //[Column("IdProducto")]
        //[DisplayName("IdProducto")]
        //public int IdProducto { get; set; } = 0;

        public string Codigo_presentacion { get; set; } = "";
        public string Codigo_barra { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Codigo_producto { get; set; } = "";

        
        //public bool Imprime_barra { get; set; } = false;

        //public double Peso { get; set; } = 0;
        
        //public double Alto { get; set; } = 0;
        
        //public double Largo { get; set; } = 0;
        
        //public double Ancho { get; set; } = 0;
        
        public double Factor { get; set; } = 0;

        //public double MinimoExistencia { get; set; } = 0;
        //public double MaximoExistencia { get; set; } = 0;
        //public string User_agr { get; set; } = "";
        //public DateTime Fec_agr { get; set; } = DateTime.Now;
        //public string User_mod { get; set; } = "";
        //public DateTime Fec_mod { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = false;
        public bool EsPallet { get; set; } = false;

        //public double Precio { get; set; } = 0;
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

        //public bool Permitir_paletizar { get; set; } = false;
        //public bool Sistema { get; set; } = false;
        //public int IdPresentacionPallet { get; set; } = 0;

    }
}
