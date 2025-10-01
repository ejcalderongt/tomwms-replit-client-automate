using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Dtos.Productos;

namespace WMSWebAPI.Services.Producto.Presentacion
{
    public interface IPresentacionMi3SyncService
    {

        void ProcesarPresentacionMi3Dto(ProductoPresentacionMi3Dto dto, SqlConnection conn, SqlTransaction tx);
    }
}
