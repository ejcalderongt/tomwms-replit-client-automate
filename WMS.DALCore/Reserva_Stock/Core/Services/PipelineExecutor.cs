using System;
using System.Diagnostics;
using System.Linq;
using WMS.StockReservation.Core.Domain;
using WMS.StockReservation.Core.Interfaces;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Ejecutor del pipeline de reserva.
    /// Ejecuta los pasos en secuencia, permitiendo salida temprana si hay error o si la cantidad ya fue reservada.
    /// Target: ~60 líneas
    /// </summary>
    public class PipelineExecutor : IReservationPipeline
    {
        private readonly IPipelineStep[] _steps;
        private readonly IReservationLogger _logger;

        public PipelineExecutor(IPipelineStep[] steps, IReservationLogger logger)
        {
            _steps = steps ?? throw new ArgumentNullException(nameof(steps));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ReservationResult Execute(ReservationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _logger.LogCheckpoint("=== INICIO PIPELINE RESERVA ===");

            bool shouldStop = false;
            var totalWatch = Stopwatch.StartNew();

            foreach (var step in _steps)
            {
                var stepName = step.GetType().Name;
                var stepWatch = Stopwatch.StartNew();
                var pendingBefore = context.PendingQuantity;
                var reservationsBefore = context.CreatedReservations?.Count ?? 0;

                _logger.LogInfo(
                    $"#MI3_PERF_STEP_START | Step={stepName} | " +
                    $"Pending={pendingBefore:F6} | Reservas={reservationsBefore} | " +
                    $"Picking={context.StockListPickingZone?.Count ?? 0} | " +
                    $"NonPicking={context.StockListNonPickingZones?.Count ?? 0}");

                try
                {
                    step.Execute(context);
                }
                catch (Exception ex)
                {
                    context.SetError($"Error en {step.GetType().Name}: {ex.Message}");
                    _logger.LogError($"Error en {step.GetType().Name}: {ex.Message}");
                }
                finally
                {
                    stepWatch.Stop();
                    _logger.LogInfo(
                        $"#MI3_PERF_STEP_END | Step={stepName} | Ms={stepWatch.ElapsedMilliseconds} | " +
                        $"PendingBefore={pendingBefore:F6} | PendingAfter={context.PendingQuantity:F6} | " +
                        $"ReservasBefore={reservationsBefore} | ReservasAfter={context.CreatedReservations?.Count ?? 0}");
                }

                if (context.HasError)
                {
                    _logger.LogError($"Pipeline detenido por error: {context.ErrorMessage}");
                    break;
                }

                // PostProcessingStep SIEMPRE debe ejecutarse - no hacer break antes
                if (step.GetType().Name == "PostProcessingStep")
                {
                    _logger.LogInfo("Post-procesamiento completado.");
                    break;
                }

                // Marcar para salida después de post-procesamiento
                if (context.IsQuantityFullyReserved() && !shouldStop)
                {
                    _logger.LogInfo("Cantidad completamente reservada. Ejecutando post-procesamiento...");
                    shouldStop = true;
                }
            }

            totalWatch.Stop();
            _logger.LogInfo(
                $"#MI3_PERF_PIPELINE_TOTAL | Ms={totalWatch.ElapsedMilliseconds} | " +
                $"Pending={context.PendingQuantity:F6} | Reservas={context.CreatedReservations?.Count ?? 0}");

            _logger.LogCheckpoint("=== FIN PIPELINE RESERVA ===");

            MarkZoneUsage(context);

            return new ReservationResult
            {
                Success = !context.HasError && context.IsQuantityFullyReserved(),
                Reservations = context.CreatedReservations,
                RemainingQuantity = context.PendingQuantity,
                Messages = _logger.GetMessages()
            };
        }

        public ReservationResultDto ExecuteWithDetails(ReservationContext context)
        {
            var legacyResult = Execute(context);
            return ReservationResultDto.FromContext(context);
        }

        private void MarkZoneUsage(ReservationContext context)
        {
            if (context.CreatedReservations == null || context.CreatedReservations.Count == 0)
                return;

            foreach (var res in context.CreatedReservations)
            {
                var stock = context.WorkingStockList?.FirstOrDefault(s => s.IdStock == res.IdStock);
                if (stock == null) continue;

                bool isPickingLocation = context.StockListPickingZone?
                    .Any(s => s.IdUbicacion == stock.IdUbicacion) ?? false;

                if (isPickingLocation)
                    context.UsedPickingZone = true;
                else
                    context.UsedNonPickingZone = true;
            }

            context.ExplosionModeEnabled = context.IsExplosionModeEnabled;
        }
    }
}
