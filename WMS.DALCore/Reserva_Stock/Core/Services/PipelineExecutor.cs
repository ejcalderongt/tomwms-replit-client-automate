
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

            if (_logger !=null)
            _logger.LogCheckpoint("=== INICIO PIPELINE RESERVA ===");

            foreach (var step in _steps)
            {
                if (_logger != null)
                    _logger.LogInfo($"Ejecutando: {step.GetType().Name}");

                try
                {
                    step.Execute(context);
                }
                catch (Exception ex)
                {
                    context.SetError($"Error en {step.GetType().Name}: {ex.Message}");
                    if (_logger != null)
                        _logger.LogError($"Error en {step.GetType().Name}: {ex.Message}");
                }

                if (context.HasError)
                {
                    if (_logger != null)
                        _logger.LogError($"Pipeline detenido por error: {context.ErrorMessage}");
                    break;
                }

                // Salida temprana si la cantidad ya fue completamente reservada
                if (context.IsQuantityFullyReserved())
                {
                    if (_logger != null)
                        _logger.LogInfo("Cantidad completamente reservada. Fin pipeline.");
                    break;
                }
            }

            if (_logger != null)
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