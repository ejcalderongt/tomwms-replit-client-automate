namespace WMS.StockReservation.Core.Interfaces
{
    /// <summary>
    /// Interfaz para cada paso del pipeline de reserva.
    /// Cada paso procesa el contexto compartido y lo modifica según su responsabilidad.
    /// </summary>
    public interface IPipelineStep
    {
        /// <summary>
        /// Ejecuta el paso del pipeline, modificando el contexto compartido.
        /// </summary>
        /// <param name="context">Contexto mutable compartido con toda la información de la operación</param>
        void Execute(ReservationContext context);
    }
}
