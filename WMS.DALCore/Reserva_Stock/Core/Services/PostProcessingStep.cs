using Microsoft.Extensions.Configuration;
using WMS.DALCore.I_nav_ped_traslado_det;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Paso 6: Post-procesamiento (actualizar trasladoDet, logs finales).
    /// Target: ~100 lineas
    /// </summary>
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

            // Actualizar TrasladoDet si existe
            if (context.TrasladoDet != null && context.CreatedReservations.Count > 0)
            {
                foreach (var reservation in context.CreatedReservations)
                {
                    if (reservation.IdPresentacion == 0)
                    {
                        // Reserva en UMBas
                        context.TrasladoDet.Quantity_Reserved_WMS += reservation.Cantidad;
                    }
                    else
                    {
                        // Reserva en presentacion
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

                // Actualizar en base de datos
                clsLnI_nav_ped_traslado_det.Actualizar_Quantity_Reserved_WMS(
                    context.TrasladoDet,
                    context.Product,
                    context.Connection,
                    context.Transaction);

                _logger.LogCheckpoint(
                    $"#MI3_TRASLADO_UPDATED - Cantidad: {context.TrasladoDet.Quantity_Reserved_WMS:F6}");
            }

            // Log de resumen
            var totalReserved = context.Request.Cantidad - context.PendingQuantity;

            // INSERTAR RESERVAS EN stock_res
            if (context.CreatedReservations.Count > 0)
            {
                var config = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();

                foreach (var reservation in context.CreatedReservations)
                {
                    try
                    {
                        clsLnStock_res.Insertar(
                            config,
                            reservation,
                            context.Connection,
                            context.Transaction);

                        _logger.LogCheckpoint(
                            $"#MI3_STOCK_RES_INSERTED - " +
                            $"IdProductoBodega: {reservation.IdProductoBodega}, " +
                            $"Cantidad: {reservation.Cantidad:F6}, " +
                            $"IdStock: {reservation.IdStock}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error insertando en stock_res: {ex.Message}");
                        throw;
                    }
                }
            }

            _logger.LogCheckpoint(
                $"#MI3_SUMMARY - " +
                $"Solicitado: {context.Request.Cantidad:F6}, " +
                $"Reservado: {totalReserved:F6}, " +
                $"Pendiente: {context.PendingQuantity:F6}, " +
                $"Reservas creadas: {context.CreatedReservations.Count}");

            // Validar invariantes finales en DEBUG
#if DEBUG
            context.ValidateInvariants("POST_PROCESSING_END");
#endif

            _logger.LogCheckpoint("#MI3_POST_PROCESSING_END");
        }
    }
}
