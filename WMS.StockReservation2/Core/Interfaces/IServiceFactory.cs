namespace WMS.StockReservation.Core.Interfaces
{
    /// <summary>
    /// Factory para crear servicios sin DI container (compatibilidad con VB.NET legacy).
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Crea una instancia del pipeline de reserva con todos sus servicios.
        /// </summary>
        IReservationPipeline CreateReservationPipeline();
        
        /// <summary>
        /// Construye dinámicamente la cadena de handlers según el punto de inicio.
        /// Se llama en CADA iteración del loop para permitir re-entry.
        /// </summary>
        /// <param name="startingPoint">Punto de inicio (0-4): 1=Complete, 2=Incomplete, 3=Picking, 4=NonPicking, 0=Default</param>
        /// <param name="context">Contexto actual de la operación</param>
        /// <param name="logger">Logger para los handlers</param>
        /// <returns>Primer handler de la cadena construida</returns>
        IReservationHandler BuildHandlerChain(int startingPoint, ReservationContext context, IReservationLogger logger);
    }
}
