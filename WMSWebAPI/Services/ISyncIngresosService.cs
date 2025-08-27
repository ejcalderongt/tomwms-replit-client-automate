using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Ingresos;

namespace WMSWebAPI.Services
{
    public interface ISyncIngresosService
    {
        void ProcesarDocumentosIngreso(List<OrdenCompraDto> listaDto, SqlConnection conn, SqlTransaction tx);
    }
}