using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Services
{
    public interface IProductoSyncService
    {
        void ProcesarProductoDesdeDto(ProductoDto dto, SqlConnection connection, SqlTransaction transaction);
    }

}
