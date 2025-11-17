using WMSWebAPI.Be;

namespace WMS.StockReservation.Core.Interfaces
{
    /// <summary>
    /// Interfaz para handlers de la cadena de responsabilidad.
    /// Cada handler puede procesar parcialmente una reserva y pasar al siguiente.
    /// </summary>
    public interface IReservationHandler
    {
        /// <summary>
        /// Establece el siguiente handler en la cadena.
        /// </summary>
        IReservationHandler SetNext(IReservationHandler next);
        
        /// <summary>
        /// Maneja la reserva de stock. Puede procesar parcialmente y pasar al siguiente.
        /// </summary>
        /// <param name="context">Contexto compartido con información de la operación</param>
        /// <returns>Resultado con las reservas creadas y cantidad pendiente</returns>
        HandlerResult Handle(ReservationContext context);
    }
    
    /// <summary>
    /// Resultado de la ejecución de un handler.
    /// </summary>
    public class HandlerResult
    {
        public bool Success { get; set; }
        public double ReservedQuantity { get; set; }
        public List<clsBeStock_res> Reservations { get; set; } = new List<clsBeStock_res>();
        public string CaseCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
