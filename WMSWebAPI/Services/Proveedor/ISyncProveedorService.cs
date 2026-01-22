using Microsoft.Data.SqlClient;
using WMS.EntityCore.Proveedor;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Ingresos;

namespace WMSWebAPI.Services.Proveedor
{
    public interface ISyncProveedorService
    {
        void ProcesarProveedorListDto(List<ProveedorDto> listaDto, SqlConnection conn, SqlTransaction tx);

        void ProcesarProveedorDto(ProveedorDto dto, SqlConnection conn, SqlTransaction tx);
        // Firma para la interfaz ISyncProveedorService
        List<clsBeProveedor> Get_All();

    }
}
