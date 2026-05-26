namespace WMS.StockReservation.Strategies
{
    /// <summary>
    /// Handler base para la cadena de responsabilidad.
    /// Implementa el patrón Chain of Responsibility con paso al siguiente handler.
    /// Target: ~70 líneas
    /// </summary>
    public abstract class BaseReservationHandler : IReservationHandler
    {
        protected IReservationHandler _nextHandler=null!;
        protected IReservationLogger _logger;

        protected BaseReservationHandler(IReservationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IReservationHandler SetNext(IReservationHandler next)
        {
            _nextHandler = next;
            return next;
        }

        public HandlerResult Handle(ReservationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var handlerName = GetType().Name;
            var handlerWatch = System.Diagnostics.Stopwatch.StartNew();
            var pendingBefore = context.PendingQuantity;
            var reservationsBefore = context.CreatedReservations?.Count ?? 0;

            // Intentar procesar localmente
            var result = ProcessInternal(context);

            handlerWatch.Stop();
            _logger.LogInfo(
                $"#MI3_PERF_HANDLER | Handler={handlerName} | Ms={handlerWatch.ElapsedMilliseconds} | " +
                $"PendingBefore={pendingBefore:F6} | PendingAfter={context.PendingQuantity:F6} | " +
                $"ResultReserved={result.ReservedQuantity:F6} | ResultReservas={result.Reservations?.Count ?? 0} | " +
                $"ContextReservasBefore={reservationsBefore} | ContextReservasAfter={context.CreatedReservations?.Count ?? 0}");

            // Si completamos la reserva, retornar directamente
            if (context.IsQuantityFullyReserved())
            {
                _logger.LogCheckpoint($"#{handlerName}_COMPLETED");
                return result;
            }

            // Si aún hay cantidad pendiente, pasar al siguiente handler
            if (_nextHandler != null && context.PendingQuantity > 0.000001)
            {
                _logger.LogCheckpoint($"#{handlerName}_PASS_TO_NEXT");
                
                var nextResult = _nextHandler.Handle(context);

                // Combinar resultados
                result.ReservedQuantity += nextResult.ReservedQuantity;
                result.Reservations ??= new();
                if (nextResult.Reservations != null)
                    result.Reservations.AddRange(nextResult.Reservations);
                result.CaseCode += (string.IsNullOrEmpty(result.CaseCode) ? "" : "+") + nextResult.CaseCode;
                result.Message += (string.IsNullOrEmpty(result.Message) ? "" : " | ") + nextResult.Message;
            }

            return result;
        }

        /// <summary>
        /// Procesamiento específico del handler. Debe ser implementado por clases hijas.
        /// </summary>
        protected abstract HandlerResult ProcessInternal(ReservationContext context);

        /// <summary>
        /// Verifica si el handler puede procesar en el contexto actual.
        /// Retorna true si hay stock disponible para este handler.
        /// </summary>
        protected abstract bool CanProcess(ReservationContext context);
    }
}
