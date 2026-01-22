using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Productos;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Services.Producto.Tipo
{
    public interface IProductoTipoMi3SyncService
    {
        List<clsBeProducto_tipo> Get_All();
        void ProcesarTipoProductoMi3Dto(Producto_tipoMi3Dto dto, SqlConnection conn, SqlTransaction tx);
    }
}
