using Microsoft.Data.SqlClient;
using WMS.EntityCore.Despacho;
using WMS.EntityCore.Pedido;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Salidas;

namespace WMSWebAPI.Services.Salidas
{
    public interface ISyncSalidasService
    {
        List<clsBeTrans_despacho_enc> ObtenerDespachos(int idPedidoEnc, SqlConnection? connection, SqlTransaction? transaction);
        List<clsBeTrans_pe_det> ObtenerDetallePedido(int IdOrdenCompraEnc);        
        List<PedidoSalidaDto> ObtenerDocumentosDeSalida(bool activo, DateTime fechaInicio, DateTime fechaFin, int idBodega, int idPropietario);
        void ProcesarSalidaDesdeDto(SalidaTransDto dto, SqlConnection conn, SqlTransaction tx);
    }
}
