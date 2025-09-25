using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Services.Producto.Clasificacion
{
    public interface IProductoClasificacionSyncService
    {

        void ProcesarClasificacionDesdeDto(ProductoClasificacionDto dto, SqlConnection connection, SqlTransaction transaction);

    }

}
