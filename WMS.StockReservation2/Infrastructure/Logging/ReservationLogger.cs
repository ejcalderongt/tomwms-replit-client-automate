using System;
using System.Collections.Generic;
using WMS.StockReservation.Core.Interfaces;
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

        public void LogCheckpoint(string checkpoint)
        {
            var message = $"[CHECKPOINT] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {checkpoint}";
            _messages.Add(message);
            Console.WriteLine(message);
        }

        public void LogInfo(string message)
        {
            var logMessage = $"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
        }

        public void LogWarning(string message)
        {
            var logMessage = $"[WARNING] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
        }

        public void LogError(string message)
        {
            var logMessage = $"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            _messages.Add(logMessage);
            Console.Error.WriteLine(logMessage);
        }

        public void LogReservation(clsBeStock_res reservation, string caseCode, string message)
        {
            var logMessage = $"[RESERVATION] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {caseCode} - " +
                           $"IdStockRes: {reservation.IdStockRes} - {message}";
            _messages.Add(logMessage);
            Console.WriteLine(logMessage);
        }

        public List<string> GetMessages()
        {
            return new List<string>(_messages);
        }
    }
}
