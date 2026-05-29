using WMSWebAPI.Be;

namespace WMS.StockReservation.Infrastructure.Logging
{
    /// <summary>
    /// Implementación del logger de reservas.
    /// Mantiene compatibilidad con el sistema de logging original (CASO_#XX).
    /// </summary>
    public class ReservationLogger : IReservationLogger
    {
        private readonly List<string> _messages = new List<string>();
        private static readonly object FileLock = new object();

        public void LogCheckpoint(string checkpoint)
        {
            var message = $"[CHECKPOINT] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {checkpoint}";
            _messages.Add(message);
            Console.WriteLine(message);
            WriteTraceFile(message);
        }

        public void LogInfo(string message)
        {
            var logMessage = $"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
            WriteTraceFile(logMessage);
        }

        public void LogWarning(string message)
        {
            var logMessage = $"[WARNING] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
            WriteTraceFile(logMessage);
        }

        public void LogError(string message)
        {
            var logMessage = $"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.Error.WriteLine(logMessage);
            WriteTraceFile(logMessage);
        }

        public void LogReservation(clsBeStock_res reservation, string caseCode, string message)
        {
            var logMessage = $"[RESERVATION] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {caseCode} - " +
                           $"IdStockRes: {reservation.IdStockRes} - {message}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
            WriteTraceFile(logMessage);
        }

        public List<string> GetMessages()
        {
            return new List<string>(_messages);
        }

        private static void WriteTraceFile(string message)
        {
            try
            {
                var logDir = Path.Combine(AppContext.BaseDirectory, "Logs");
                Directory.CreateDirectory(logDir);

                var path = Path.Combine(logDir, $"reserva-mi3-trace-{DateTime.Now:yyyyMMdd}.txt");
                lock (FileLock)
                {
                    File.AppendAllText(path, message + Environment.NewLine);
                }
            }
            catch
            {
                // El trace no debe afectar el proceso de reserva.
            }
        }
    }
}
