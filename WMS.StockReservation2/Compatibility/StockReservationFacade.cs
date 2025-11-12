using Microsoft.Data.SqlClient;
using WMS.EntityCore.Pedido;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Compatibility
{
    public static class StockReservationFacade
    {
        public static List<clsBeStock_res> Reserva_Stock_From_MI3(
            clsBeStock_res oBeStockResRequest,
            int IdProducto,
            clsBeI_nav_config_enc? oBeConfigEnc,
            SqlConnection cnnSql,
            SqlTransaction? trSql,
            clsBeTrans_pe_det? oBePedidoDet = null,
            clsBeI_nav_ped_traslado_det? oBeI_nav_ped_traslado_det = null,
            int Tarea_Reabasto = 0,
            bool EsDevolucion = false,
            int LineNumber = 0,
            string? MachineName = null)
        {
            var factory = new ServiceFactory();
            var pipeline = factory.CreateReservationPipeline();

            var context = new ReservationContext
            {
                Request = oBeStockResRequest,
                ProductId = IdProducto,
                Configuration = oBeConfigEnc,
                Connection = cnnSql,
                Transaction = trSql,
                PedidoDet = oBePedidoDet,
                TrasladoDet = oBeI_nav_ped_traslado_det,
                TareaReabasto = Tarea_Reabasto,
                EsDevolucion = EsDevolucion,
                LineNumber = LineNumber,
                MachineName = MachineName ?? Environment.MachineName
            };

            var result = pipeline.Execute(context);

            if (!result.Success && context.HasError)
            {
                throw new Exception($"Error en reserva de stock: {context.ErrorMessage}");
            }

            return result.Reservations;
        }

        public static List<clsBeStock_res> ReservarStock(
            int bodegaId,
            int productoId,
            double cantidad,
            int presentacionId,
            clsBeI_nav_config_enc configuracion,
            SqlConnection connection,
            SqlTransaction? transaction = null)
        {
            var request = new clsBeStock_res
            {
                IdBodega = bodegaId,
                IdProductoBodega = productoId,
                IdPresentacion = presentacionId,
                Cantidad = cantidad
            };

            return Reserva_Stock_From_MI3(request, productoId, configuracion, connection, transaction);
        }
    }
}