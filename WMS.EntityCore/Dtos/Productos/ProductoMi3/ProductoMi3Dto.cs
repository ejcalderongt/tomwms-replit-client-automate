namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoMi3Dto
    {
        public int IdProducto { get; set; }
        public int IdPropietario { get; set; }
        public string? CodigoClasificacion { get; set; }
        public string? CodigoFamilia { get; set; }
        public string? CodigoMarca { get; set; }
        public string? CodigoTipoProducto { get; set; }
        public string? CodigoUmBas { get; set; }        
        public int? IdTipoRotacion { get; set; }                
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo_Barra { get; set; }                                                               
        public bool? Genera_Lote { get; set; }
        public bool? Genera_Lp_Old { get; set; }
        public bool? Control_Vencimiento { get; set; }
        public bool? Control_Lote { get; set; }                                                        
        public bool? FechaManufactura { get; set; }        
        public bool? Control_Peso { get; set; }                
        public int? IdTipoEtiqueta { get; set; }
        
    }
}