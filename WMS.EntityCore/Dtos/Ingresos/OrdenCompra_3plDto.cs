using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Movimientos.WMSWebAPI.Dto;
using WMSWebAPI.Dtos.Stock;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class OrdenCompra_3plDto
    {
        public OrdenCompraEncDto? Encabezado { get; set; }
        public List<OrdenCompraDetDto>? Detalle { get; set; }
        public List<OrdenCompraPolizaDto>? Polizas { get; set; }
        public TipoIngresoOcDto? TipoIngreso { get; set; }
        public List<Recepcion_3plDto>? Recepciones { get; set; }
        public List<StockRecDto>? stockRec { get; set; }
        public List<Stock_3plDto>? stock { get; set; }
        public List<MovimientosDto>? movimientos { get; set; }
        public List<ProveedorDto>? Proveedores { get; set; }
        public List<ProveedorBodegaDto>? ProveedoresBodega { get; set; }
    }
}