using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionCompletaDto
    {
        public Guid IdTransaccion { get; set; }
        public OrdenCompraDto? OrdenCompra { get; set; } = new();
        //public List<ProductoDto>? Productos { get; set; } = new();
    }
}