using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;

namespace WMSWebAPI.Services.Producto.Clasificacion
{
    public interface IProductoClasificacionSyncService
    {

        void ProcesarClasificacionDesdeDto(ProductoClasificacionSimpleDto dto, SqlConnection connection, SqlTransaction transaction);

    }

}
