using Microsoft.Data.SqlClient;
using WMS.EntityCore.Proveedor;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Ingresos;

namespace WMSWebAPI.Services.Proveedor
{
    public interface ISyncProveedorService
    {        

        void Procesarmi3ProveedorDto(mi3ProveedorDto dto, SqlConnection conn, SqlTransaction tx);
        List<clsBeProveedor> Get_All();

    }
}
