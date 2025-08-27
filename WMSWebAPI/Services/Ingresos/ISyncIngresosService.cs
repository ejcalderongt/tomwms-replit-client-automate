using Microsoft.Data.SqlClient;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.WebResponseDto;
using WMSWebAPI.Entity.Trans_oc;

namespace WMSWebAPI.Services.Ingresos
{
    public interface ISyncIngresosService
    {
        void ProcesarDocumentosIngreso(List<OrdenCompraDto> listaDto, SqlConnection conn, SqlTransaction tx);
        List<clsBeVWOrdenCompra> ObtenerDocumentosDeIngreso(bool activo, DateTime fechaInicio, DateTime fechaFin, int idBodega, int idPropietario);
        List<clsBeTrans_oc_det> ObtenerDetalleOrdenCompra(int IdOrdenCompraEnc);
        List<ReEncWebResponseDto> ObtenerDetalleRecepcion(int IdOrdenCompraEnc);

    }
}