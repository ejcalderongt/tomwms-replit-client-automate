namespace WMS.EntityCore.Dtos.Prefactura
{
    /// <summary>
    /// DTO para el detalle de prefactura que Odoo consumirá
    /// </summary>
    public class PrefacturaPendienteDetDto
    {
        /// <summary>
        /// Correlativo del detalle del acuerdo
        /// </summary>
        public int CorrelativoDetalleAcuerdo { get; set; }

        /// <summary>
        /// Código del producto/servicio
        /// </summary>
        public string CodigoProducto { get; set; } = string.Empty;

        /// <summary>
        /// Días del evento
        /// </summary>
        public int Dias { get; set; }

        /// <summary>
        /// Monto a facturar
        /// </summary>
        public double Monto { get; set; }

        /// <summary>
        /// Descripción del servicio
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Número de unidades
        /// </summary>
        public decimal NumeroUnidades { get; set; }
    }
}