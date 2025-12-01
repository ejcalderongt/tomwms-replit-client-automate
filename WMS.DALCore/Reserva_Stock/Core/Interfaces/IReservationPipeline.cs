using WMSWebAPI.Be;

namespace WMS.StockReservation.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el ejecutor del pipeline de reserva.
    /// </summary>
    public interface IReservationPipeline
    {
        /// <summary>
        /// Ejecuta todos los pasos del pipeline en secuencia.
        /// </summary>
        /// <param name="context">Contexto compartido con información de la operación</param>
        /// <returns>Resultado final de la reserva</returns>
        ReservationResult Execute(ReservationContext context);
    }

    /// <summary>
    /// Resultado final de la ejecución del pipeline completo.
    /// </summary>
    public class ReservationResult
    {
        public bool Success { get; set; }
        public List<clsBeStock_res> Reservations { get; set; } = new List<clsBeStock_res>();
        public double RemainingQuantity { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
