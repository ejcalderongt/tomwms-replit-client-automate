using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Services
{
    public interface IProductoSyncService
    {
        void ProcesarProductoDesdeDto(ProductoTransDto dto, SqlConnection connection, SqlTransaction transaction);
    }

}
