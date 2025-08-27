using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class OrdenCompraDto
    {
        public OrdenCompraEncDto? Encabezado { get; set; }
        public List<OrdenCompraDetDto>? Detalle { get; set; }
        public List<OrdenCompraPolizaDto>? Polizas { get; set; }
        public TipoIngresoOcDto? TipoIngreso { get; set; }
        public List<RecepcionDto>? Recepciones { get; set; }
    }
}