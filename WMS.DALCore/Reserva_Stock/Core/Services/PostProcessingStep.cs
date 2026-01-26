using Microsoft.Extensions.Configuration;
using WMS.DALCore.I_nav_ped_traslado_det;

namespace WMS.StockReservation.Core.Services
{
    public class PostProcessingStep : IPipelineStep
    {
        private readonly IReservationLogger _logger;

        public PostProcessingStep(IReservationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(ReservationContext context)
        {
            _logger.LogCheckpoint("#MI3_POST_PROCESSING_START");

            // PASO 1: INSERTAR RESERVAS EN stock_res (ANTES de actualizar TrasladoDet)
            if (context.CreatedReservations.Count > 0)
            {
                _logger.LogCheckpoint($"#MI3_INSERTING_STOCK_RES - Count: {context.CreatedReservations.Count}");

                var config = new ConfigurationBuilder().Build();

                // Obtener IdPedidoEnc para asignar a IdTransaccion
                // Prioridad: 1) context.Request.IdPedido, 2) 0
                int idPedidoEnc = context.Request.IdPedido > 0 
                    ? context.Request.IdPedido
                    : (context.Request?.IdPedido ?? 0);

                foreach (var reservation in context.CreatedReservations)
                {
                    try
                    {
                        // Calcular MaxID ANTES de cada INSERT
                        int maxId = clsLnStock_res.MaxID(context.Connection, context.Transaction);
                        reservation.IdStockRes = maxId + 1;

                        // Asignar IdTransaccion = IdPedidoEnc
                        reservation.IdTransaccion = idPedidoEnc;

                        _logger.LogCheckpoint(
                            $"#MI3_STOCK_RES_MAXID - MaxID: {maxId}, NewIdStockRes: {reservation.IdStockRes}, IdTransaccion: {idPedidoEnc}");

                        int rowsAffected = clsLnStock_res.Insertar(
                            config,
                            reservation,
                            context.Connection,
                            context.Transaction);

                        _logger.LogCheckpoint(
                            $"#MI3_STOCK_RES_INSERTED - " +
                            $"IdStockRes: {reservation.IdStockRes}, " +
                            $"IdStock: {reservation.IdStock}, " +
                            $"IdProductoBodega: {reservation.IdProductoBodega}, " +
                            $"Cantidad: {reservation.Cantidad:F6}, " +
                            $"IdUbicacion: {reservation.IdUbicacion}, " +
                            $"IdTransaccion: {reservation.IdTransaccion}, " +
                            $"RowsAffected: {rowsAffected}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCheckpoint($"#MI3_STOCK_RES_ERROR - IdStock: {reservation.IdStock}, Error: {ex.Message}");
                        throw;
                    }
                }
            }

            // PASO 2: Actualizar TrasladoDet.Quantity_Reserved_WMS
            if (context.TrasladoDet != null && context.CreatedReservations.Count > 0)
            {
                foreach (var reservation in context.CreatedReservations)
                {
                    if (reservation.IdPresentacion == 0)
                    {
                        context.TrasladoDet.Quantity_Reserved_WMS += reservation.Cantidad;
                    }
                    else
                    {
                        if (context.PedidoDet?.IdPresentacion == 0)
                        {
                            context.TrasladoDet.Quantity_Reserved_WMS += reservation.Cantidad;
                        }
                        else
                        {
                            var factor = context.DefaultPresentation?.Factor ?? 1;
                            context.TrasladoDet.Quantity_Reserved_WMS +=
                                Math.Round(reservation.Cantidad / factor, 6);
                        }
                    }
                }

                clsLnI_nav_ped_traslado_det.Actualizar_Quantity_Reserved_WMS(
                    context.TrasladoDet,
                    context.Product,
                    context.Connection,
                    context.Transaction);

                _logger.LogCheckpoint(
                    $"#MI3_TRASLADO_UPDATED - Cantidad: {context.TrasladoDet.Quantity_Reserved_WMS:F6}");
            }

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Request == null)
                throw new InvalidOperationException("ReservationContext.Request es null en el resumen.");

            var pending = context.PendingQuantity; // si es decimal no-nullable

            // Si PendingQuantity es decimal?
            // var pending = context.PendingQuantity ?? 0m;

            var totalReserved = context.Request.Cantidad - pending;

            _logger.LogCheckpoint(
                $"#MI3_SUMMARY - " +
                $"Solicitado: {context.Request.Cantidad:F6}, " +
                $"Reservado: {totalReserved:F6}, " +
                $"Pendiente: {context.PendingQuantity:F6}, " +
                $"Reservas creadas: {context.CreatedReservations.Count}");

#if DEBUG
            context.ValidateInvariants("POST_PROCESSING_END");
#endif

            _logger.LogCheckpoint("#MI3_POST_PROCESSING_END");
        }
    }
}
