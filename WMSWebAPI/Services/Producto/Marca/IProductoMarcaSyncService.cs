using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Services.Producto.Marca
{
    public interface IProductoMarcaSyncService
    {
        void ProcesarMarcaDesdeDto(ProductoMarcaDto dto, SqlConnection connection, SqlTransaction transaction);
    }
}
