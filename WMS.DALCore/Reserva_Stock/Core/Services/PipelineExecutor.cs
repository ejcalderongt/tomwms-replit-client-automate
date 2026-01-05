using System;
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

            foreach (var step in _steps)
            {
                _logger.LogInfo($"Ejecutando: {step.GetType().Name}");

                try
                {
                    step.Execute(context);
                }
                catch (Exception ex)
                {
                    context.SetError($"Error en {step.GetType().Name}: {ex.Message}");
                    _logger.LogError($"Error en {step.GetType().Name}: {ex.Message}");
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

            _logger.LogCheckpoint("=== FIN PIPELINE RESERVA ===");

            return new ReservationResult
            {
                Success = !context.HasError && context.IsQuantityFullyReserved(),
                Reservations = context.CreatedReservations,
                RemainingQuantity = context.PendingQuantity,
                Messages = _logger.GetMessages()
            };
        }
    }
}
