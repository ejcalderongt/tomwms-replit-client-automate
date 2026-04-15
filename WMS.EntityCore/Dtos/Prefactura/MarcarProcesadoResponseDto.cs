namespace WMS.EntityCore.Dtos.Prefactura
{
    /// <summary>
    /// Respuesta al marcar una prefactura como procesada
    /// </summary>
    public class MarcarProcesadoResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int IdPrefacturaEnc { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}