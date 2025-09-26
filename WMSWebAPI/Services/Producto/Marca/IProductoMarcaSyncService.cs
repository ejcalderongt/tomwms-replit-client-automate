using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;

namespace WMSWebAPI.Services.Producto.Marca
{
    public interface IProductoMarcaSyncService
    {
        void ProcesarMarcaDesdeDto(ProductoMarcaSimpleDto dto, SqlConnection connection, SqlTransaction transaction);
    }
}
