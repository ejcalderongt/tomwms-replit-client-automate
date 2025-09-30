using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;

namespace WMSWebAPI.Services.Producto.Familia
{
    public interface IProductoFamiliaSyncService
    {
        void ProcesarFamiliaDesdeDto(ProductoFamiliaMi3Dto dto, SqlConnection connection, SqlTransaction transaction);
    }
}
