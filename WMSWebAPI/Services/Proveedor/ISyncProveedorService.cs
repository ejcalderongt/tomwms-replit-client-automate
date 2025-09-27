using Microsoft.Data.SqlClient;
using WMS.EntityCore.Proveedor;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Ingresos;

namespace WMSWebAPI.Services.Proveedor
{
    public interface ISyncProveedorService
    {
        void ProcesarProveedorListDto(List<ProveedorDto> listaDto, SqlConnection conn, SqlTransaction tx);
    }
}
