using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Services.Producto
{
    public interface IProductoMhsSyncService
    {
        void ProcesarProductoSingleDto(ProductoMhsDto dto, SqlConnection connection, SqlTransaction transaction);
    }
}
