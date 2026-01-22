using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoMi3Dto
    {
        public int IdProducto { get; set; } = 0;
        public int IdPropietario { get; set; } = 0;
        public string CodigoClasificacion { get; set; } = "";
        public string CodigoFamilia { get; set; } = "";
        public string CodigoMarca { get; set; } = "";
        public string CodigoTipoProducto { get; set; } = "";
        public string CodigoUmBas { get; set; } = "";
        public int IdTipoRotacion { get; set; } = 0;
        public string Codigo { get; set; } = "";
        public string nombre { get; set; } = "";
        public string codigo_barra { get; set; } = "";
        public bool? Genera_Lote { get; set; } = false;
        public bool? Genera_Lp_Old { get; set; }= false;
        public bool? Control_Vencimiento { get; set; } = false;
        public bool? Control_Lote { get; set; } = false;
        //public bool? FechaManufactura { get; set; }        
        public bool? Control_Peso { get; set; } = false;
        //public int? IdTipoEtiqueta { get; set; }
        public bool? activo { get; set; } = true;

    }
}