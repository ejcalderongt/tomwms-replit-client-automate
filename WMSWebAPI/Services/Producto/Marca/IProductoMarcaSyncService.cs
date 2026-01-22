using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Services.Producto.Marca
{
    public interface IProductoMarcaSyncService
    {
        void ProcesarMarcaDesdeDto(ProductoMarcaMi3Dto dto, SqlConnection connection, SqlTransaction transaction);
        // Firma para la interfaz IProductoMarcaSyncService
        List<clsBeProducto_marca> Get_All();

    }
}
