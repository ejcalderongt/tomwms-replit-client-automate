using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Despacho;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Transacciones;
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
        void ProcesarSalidaDesde_3plDto(SalidaTrans_3plDto dto, SqlConnection conn, SqlTransaction tx);
        IEnumerable<clsBeI_nav_transacciones_out> Get_Salidas_Pendientes_De_Procesar(string? noPedido = null);

    }
}
