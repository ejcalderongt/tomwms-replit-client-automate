namespace WMS.EntityCore.Dtos.Prefactura
{
    /// <summary>
    /// DTO para la prefactura que Odoo consumirá
    /// </summary>
    public class PrefacturaPendienteDto
    {
        /// <summary>
        /// ID de la prefactura en WMS
        /// </summary>
        public int IdPrefacturaEnc { get; set; }

        /// <summary>
        /// NIT del cliente a facturar
        /// </summary>
        public string Nit { get; set; } = string.Empty;

        /// <summary>
        /// ID del cliente a facturar
        /// </summary>
        public int IdClienteFacturar { get; set; }

        /// <summary>
        /// Código del acuerdo comercial
        /// </summary>
        public int CodigoAcuerdo { get; set; }

        /// <summary>
        /// ID del cliente en WMS
        /// </summary>
        public int IdCliente { get; set; }

        /// <summary>
        /// Moneda (USD, EUR, etc.)
        /// </summary>
        public string Moneda { get; set; } = string.Empty;

        /// <summary>
        /// Período de la prefactura (ej: "Del 01/01/2026 al 31/01/2026")
        /// </summary>
        public string Periodo { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la mercadería/servicio
        /// </summary>
        public string Mercaderia { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de cambio aplicado
        /// </summary>
        public double TipoCambio { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de creación de la prefactura
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha desde del período
        /// </summary>
        public DateTime FechaDesde { get; set; }

        /// <summary>
        /// Fecha hasta del período
        /// </summary>
        public DateTime FechaHasta { get; set; }

        /// <summary>
        /// Detalles de la prefactura
        /// </summary>
        public List<PrefacturaPendienteDetDto> Detalle { get; set; } = new List<PrefacturaPendienteDetDto>();
    }
}