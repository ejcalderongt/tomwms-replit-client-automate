using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Services
{
    public interface IProductoMi3SyncService
    {
        int ProcesarProductoSingleDto(ProductoMi3Dto dto, SqlConnection connection, SqlTransaction transaction);        
        List<clsBeProducto> Get_All();

    }
}
