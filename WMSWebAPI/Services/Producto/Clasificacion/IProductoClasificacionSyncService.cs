using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Services.Producto.Clasificacion
{
    public interface IProductoClasificacionSyncService
    {

        void ProcesarClasificacionDesdeDto(ProductoClasificacionMi3Dto dto, SqlConnection connection, SqlTransaction transaction);
        List<clsBeProducto_clasificacion> Get_All();


    }

}
