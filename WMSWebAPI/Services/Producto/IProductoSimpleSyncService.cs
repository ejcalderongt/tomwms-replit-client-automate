using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Services
{
    public interface IProductoSimpleSyncService
    {
        void ProcesarProductoSingleDto(ProductoSimpleDto dto, SqlConnection connection, SqlTransaction transaction);
    }
}
