using Microsoft.Data.SqlClient;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Salidas;
using WMSWebAPI.Entity.Despacho;

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
