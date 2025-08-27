using WMSWebAPI.Dtos.Movimientos.WMSWebAPI.Dto;
using WMSWebAPI.Dtos.Operador;
using WMSWebAPI.Dtos.Stock;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionDto
    {
        public RecepcionEncDto Encabezado { get; set; } = new RecepcionEncDto();
        public List<RecepcionDetDto> Detalle { get; set; } = new List<RecepcionDetDto>();
        public List<RecepcionOcDto>? OcsRelacionadas { get; set; }
        public List<RecepcionOperadorDto> OperadoresRec { get; set; } = new List<RecepcionOperadorDto>();
        public List<OperadorDto> Operadores { get; set; } = new List<OperadorDto>();
        public List<OperadorBodegaDto> OperadorBodega { get; set; } = new List<OperadorBodegaDto>();
        public List<RecepcionFacturaDto> Facturas { get; set; } = new List<RecepcionFacturaDto>();
        public List<RecepcionImagenDto> Imagenes { get; set; } = new List<RecepcionImagenDto>();
        public TipoRecDto TipoRec { get; set; } = new TipoRecDto();
        //public List<StockRecDto> StockRec { get; set; } = new List<StockRecDto>();
        //public List<StockDto> Stock { get; set; } = new List<StockDto>();
        //public List<MovimientosDto> Movimientos { get; set; } = new List<MovimientosDto>();
    }
}