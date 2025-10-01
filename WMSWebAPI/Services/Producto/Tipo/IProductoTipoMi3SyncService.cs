using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Productos;

namespace WMSWebAPI.Services.Producto.Tipo
{
    public interface IProductoTipoMi3SyncService
    {
        void ProcesarTipoProductoMi3Dto(Producto_tipoMi3Dto dto, SqlConnection conn, SqlTransaction tx);
    }
}
