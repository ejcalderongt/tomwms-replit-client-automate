using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;

namespace WMSWebAPI.Services.Producto.Marca
{
    public interface IProductoMarcaSyncService
    {
        void ProcesarMarcaDesdeDto(ProductoMarcaMi3Dto dto, SqlConnection connection, SqlTransaction transaction);
    }
}
