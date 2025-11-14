namespace WMS.StockReservation.Strategies
{
    /// <summary>
    /// Handler base para la cadena de responsabilidad.
    /// Implementa el patrón Chain of Responsibility con paso al siguiente handler.
    /// Target: ~70 líneas
    /// </summary>
    public abstract class BaseReservationHandler : IReservationHandler
    {
        protected IReservationHandler _nextHandler;
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

            // Intentar procesar localmente
            var result = ProcessInternal(context);

            // Si completamos la reserva, retornar directamente
            if (context.IsQuantityFullyReserved())
            {
                _logger.LogCheckpoint($"#{GetType().Name}_COMPLETED");
                return result;
            }

            // Si aún hay cantidad pendiente, pasar al siguiente handler
            if (_nextHandler != null && context.PendingQuantity > 0.000001)
            {
                _logger.LogCheckpoint($"#{GetType().Name}_PASS_TO_NEXT");
                
                var nextResult = _nextHandler.Handle(context);

                // Combinar resultados
                result.ReservedQuantity += nextResult.ReservedQuantity;
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
