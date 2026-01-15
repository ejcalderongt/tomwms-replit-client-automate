using Microsoft.Data.SqlClient;
using WMS.EntityCore.Trans_oc;
using WMS.EntityCore.Transacciones;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.WebResponseDto;

namespace WMSWebAPI.Services.Ingresos
{
    public interface ISyncIngresosService
    {
        void ProcesarDocumentosIngreso(List<OrdenCompraDto> listaDto, SqlConnection conn, SqlTransaction tx);
        List<clsBeVWOrdenCompra> ObtenerDocumentosDeIngreso(bool activo, DateTime fechaInicio, DateTime fechaFin, int idBodega, int idPropietario);
        List<clsBeTrans_oc_det> ObtenerDetalleOrdenCompra(int IdOrdenCompraEnc);
        List<ReEncWebResponseDto> ObtenerDetalleRecepcion(int IdOrdenCompraEnc);
        public int Insert(clsBeI_nav_ped_compra_enc beINavPedCompraEnc);
        public List<clsBeI_nav_transacciones_out> Get_Ingresos_Pendientes_De_Procesar();
        public int Marcar_Ingresos_Como_Enviados(List<int> idTransacciones);
    }
}