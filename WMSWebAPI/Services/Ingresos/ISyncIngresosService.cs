using Microsoft.Data.SqlClient;
using WMS.EntityCore.Trans_oc;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.WebResponseDto;

namespace WMSWebAPI.Services.Ingresos
{
    public interface ISyncIngresosService
    {
        void ProcesarDocumentosIngreso(List<OrdenCompraDto> listaDto, SqlConnection conn, SqlTransaction tx);
        void ProcesarDocumentosIngreso_3pl(List<OrdenCompra_3plDto> listaDto, SqlConnection conn, SqlTransaction tx);
        List<clsBeVWOrdenCompra> ObtenerDocumentosDeIngreso(bool activo, DateTime fechaInicio, DateTime fechaFin, int idBodega, int idPropietario);
        List<clsBeTrans_oc_det> ObtenerDetalleOrdenCompra(int IdOrdenCompraEnc);
        List<ReEncWebResponseDto> ObtenerDetalleRecepcion(int IdOrdenCompraEnc);
        public int Insert(clsBeI_nav_ped_compra_enc beINavPedCompraEnc);
    }
}