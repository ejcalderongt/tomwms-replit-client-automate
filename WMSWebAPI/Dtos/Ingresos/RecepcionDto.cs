using WMSWebAPI.Dtos.Stock;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionDto
    {
        public RecepcionEncDto Encabezado { get; set; } = new RecepcionEncDto();
        public List<RecepcionDetDto> Detalle { get; set; } = new List<RecepcionDetDto>();
        public List<RecepcionOcDto>? OcsRelacionadas { get; set; }
        public List<RecepcionOperadorDto> Operadores { get; set; } = new List<RecepcionOperadorDto>();
        public List<RecepcionFacturaDto> Facturas { get; set; } = new List<RecepcionFacturaDto>();
        public List<RecepcionImagenDto> Imagenes { get; set; } = new List<RecepcionImagenDto>();
        public TipoRecDto TipoRec { get; set; } = new TipoRecDto();
        public List<StockRecDto> StockRec { get; set; } = new List<StockRecDto>();
    }
}