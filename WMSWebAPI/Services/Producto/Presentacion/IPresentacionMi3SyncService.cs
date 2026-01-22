using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Dtos.Productos;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Services.Producto.Presentacion
{
    public interface IPresentacionMi3SyncService
    {
        void ProcesarPresentacionMi3Dto(ProductoPresentacionMi3Dto dto, SqlConnection conn, SqlTransaction tx);
        // Firma para la interfaz IPresentacionMi3SyncService
        List<clsBeProducto_presentacion> Get_All();

    }
}
