using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Services
{
    public interface IProductoMi3SyncService
    {
        void ProcesarProductoSingleDto(ProductoMi3Dto dto, SqlConnection connection, SqlTransaction transaction);
    }
}
