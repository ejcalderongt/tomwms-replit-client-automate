using WMSWebAPI.Dtos.Movimientos.WMSWebAPI.Dto;
using WMSWebAPI.Dtos.Operador;
using WMSWebAPI.Dtos.Stock;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class Recepcion_3plDto
    {
        public RecepcionEnc_3plDto Encabezado { get; set; } = new RecepcionEnc_3plDto();
        public List<RecepcionDet_3plDto> Detalle { get; set; } = new List<RecepcionDet_3plDto>();
        public List<RecepcionOcDto>? OcsRelacionadas { get; set; }
        public List<RecepcionOperador_3plDto> OperadoresRec { get; set; } = new List<RecepcionOperador_3plDto>();
        public List<OperadorDto> Operadores { get; set; } = new List<OperadorDto>();
        public List<OperadorBodegaDto> OperadorBodega { get; set; } = new List<OperadorBodegaDto>();
        public List<RecepcionFacturaDto> Facturas { get; set; } = new List<RecepcionFacturaDto>();
        public List<RecepcionImagenDto> Imagenes { get; set; } = new List<RecepcionImagenDto>();
        //public TipoRecDto TipoRec { get; set; } = new TipoRecDto();
        
    }
}