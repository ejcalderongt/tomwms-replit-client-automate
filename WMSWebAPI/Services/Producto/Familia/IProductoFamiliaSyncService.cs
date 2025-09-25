using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Services.Producto.Familia
{
    public interface IProductoFamiliaSyncService
    {
        void ProcesarFamiliaDesdeDto(ProductoFamiliaDto dto, SqlConnection connection, SqlTransaction transaction);
    }
}
