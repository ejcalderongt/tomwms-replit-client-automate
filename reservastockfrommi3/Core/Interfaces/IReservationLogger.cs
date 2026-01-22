using WMSWebAPI.Be;

namespace WMS.StockReservation.Core.Interfaces
{
    /// <summary>
    /// Interfaz para logging de operaciones de reserva.
    /// Mantiene compatibilidad con el sistema de logging original (CASO_#XX, checkpoints).
    /// </summary>
    public interface IReservationLogger
    {
        /// <summary>
        /// Registra un checkpoint del proceso (e.g., #MI3_VALIDATION_OK).
        /// </summary>
        void LogCheckpoint(string checkpoint);
        
        /// <summary>
        /// Registra información general.
        /// </summary>
        void LogInfo(string message);
        
        /// <summary>
        /// Registra una advertencia.
        /// </summary>
        void LogWarning(string message);
        
        /// <summary>
        /// Registra un error.
        /// </summary>
        void LogError(string message);
        
        /// <summary>
        /// Registra una reserva específica con su caso y mensaje detallado.
        /// Mapea a Agregar_Log_Reserva del original.
        /// </summary>
        void LogReservation(clsBeStock_res reservation, string caseCode, string message);
        
        /// <summary>
        /// Obtiene todos los mensajes registrados.
        /// </summary>
        List<string> GetMessages();
    }
}
