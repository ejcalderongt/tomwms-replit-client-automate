using WMSWebAPI.Be;

namespace WMS.StockReservation.Infrastructure.Logging
{
    /// <summary>
    /// Implementación del logger de reservas.
    /// Mantiene compatibilidad con el sistema de logging original (CASO_#XX).
    /// Escribe a consola Y a archivos de texto:
    /// - Log diario: reservation_YYYYMMDD.log
    /// - Log por pedido: reservation_pedido_XXX.log (para auditorías)
    /// </summary>
    public class ReservationLogger : IReservationLogger
    {
        private readonly List<string> _messages = new List<string>();
        private static readonly object _fileLock = new object();
        
        // Configuración del archivo de log
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        private static readonly bool EnableFileLogging = true; // Cambiar a false para desactivar
        
        // Número de pedido actual para log por pedido
        private string _currentOrderNumber = "";

        /// <summary>
        /// Establece el número de pedido actual para generar log específico por pedido.
        /// Llamar al inicio de cada proceso de reserva.
        /// </summary>
        public void SetOrderNumber(string orderNumber)
        {
            var sanitized = SanitizeFileName(orderNumber);
            _currentOrderNumber = sanitized ?? string.Empty;
        }

        private static string? SanitizeFileName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            // Remover caracteres no válidos para nombres de archivo
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }

        private static string GetDailyLogFilePath()
        {
            return Path.Combine(LogDirectory, $"reservation_{DateTime.Now:yyyyMMdd}.log");
        }

        private string? GetOrderLogFilePath()
        {
            if (string.IsNullOrEmpty(_currentOrderNumber)) return string.Empty;
            return Path.Combine(LogDirectory, $"reservation_pedido_{_currentOrderNumber}.log");
        }

        private void WriteToFile(string message)
        {
            if (!EnableFileLogging) return;
            
            try
            {
                lock (_fileLock)
                {
                    if (!Directory.Exists(LogDirectory))
                        Directory.CreateDirectory(LogDirectory);
                    
                    // Escribir al log diario
                    File.AppendAllText(GetDailyLogFilePath(), message + Environment.NewLine);
                    
                    // Escribir al log por pedido (si está configurado)
                    var orderLogPath = GetOrderLogFilePath();
                    if (!string.IsNullOrEmpty(orderLogPath))
                    {
                        File.AppendAllText(orderLogPath, message + Environment.NewLine);
                    }
                }
            }
            catch { /* Ignorar errores de escritura a archivo */ }
        }

        public void LogCheckpoint(string checkpoint)
        {
            if (string.IsNullOrEmpty(checkpoint)) return;
            var message = $"[CHECKPOINT] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {checkpoint}";
            _messages.Add(message);
            Console.WriteLine(message);
            WriteToFile(message);
        }

        public void LogInfo(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            var logMessage = $"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
            WriteToFile(logMessage);
        }

        public void LogWarning(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            var logMessage = $"[WARNING] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
            WriteToFile(logMessage);
        }

        public void LogError(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            var logMessage = $"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.Error.WriteLine(logMessage);
            WriteToFile(logMessage);
        }

        public void LogReservation(clsBeStock_res reservation, string caseCode, string message)
        {
            if (reservation == null) return;
            var logMessage = $"[RESERVATION] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {caseCode ?? "UNKNOWN"} - " +
                           $"IdStockRes: {reservation.IdStockRes} - {message ?? ""}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
            WriteToFile(logMessage);
        }

        public List<string> GetMessages()
        {
            return new List<string>(_messages);
        }
    }
}
