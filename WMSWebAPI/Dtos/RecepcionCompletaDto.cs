using WMSWebAPI.Dtos.Ingresos;

namespace WMSWebAPI.Dtos
{
    public class RecepcionCompletaDto
    {
        public Guid IdTransaccion { get; set; }
        public OrdenCompraDto? OrdenCompra { get; set; } = new();
        //public List<ProductoDto>? Productos { get; set; } = new();
    }
}