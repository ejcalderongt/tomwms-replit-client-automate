using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Productos;
using WMS.EntityCore.Producto;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Services.Producto.Umbas
{
    public interface IUmbasMi3SyncService
    {
        List<clsBeUnidad_medida> Get_All();
        void ProcesarUmbasMi3Dto(UnidadMedidaMi3Dto dto, SqlConnection conn, SqlTransaction tx);

    }
}
