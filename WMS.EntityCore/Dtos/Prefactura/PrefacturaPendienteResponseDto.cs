namespace WMS.EntityCore.Dtos.Prefactura
{
    /// <summary>
    /// Respuesta paginada para Odoo
    /// </summary>
    public class PrefacturaPendienteResponseDto
    {
        /// <summary>
        /// Indica si la operación fue exitosa
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Mensaje informativo
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Lista de prefacturas pendientes
        /// </summary>
        public List<PrefacturaPendienteDto> Data { get; set; } = new List<PrefacturaPendienteDto>();

        /// <summary>
        /// Total de registros (sin paginación)
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Página actual
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Tamaño de página
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total de páginas
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Timestamp de la respuesta
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}