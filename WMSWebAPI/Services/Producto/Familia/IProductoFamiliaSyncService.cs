using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Services.Producto.Familia
{
    public interface IProductoFamiliaSyncService
    {
        void ProcesarFamiliaDesdeDto(ProductoFamiliaMi3Dto dto, SqlConnection connection, SqlTransaction transaction);
        //  Firma para la interfaz IProductoFamiliaSyncService
        List<clsBeProducto_familia> Get_All();

    }
}
