
namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Servicio para conversión de cantidades entre UMBas y Presentaciones.
    /// Maneja los factores de conversión (e.g., 1 caja = 12 unidades).
    /// </summary>
    public class QuantityConverter
    {
        /// <summary>
        /// Convierte cantidad de presentación a UMBas.
        /// Ejemplo: 2 cajas × 12 = 24 unidades
        /// </summary>
        public double ConvertToUMBas(double quantityInPresentation, double presentationFactor)
        {
            if (presentationFactor <= 0)
                throw new ArgumentException("presentationFactor debe ser > 0", nameof(presentationFactor));
            
            return Math.Round(quantityInPresentation * presentationFactor, 6);
        }

        /// <summary>
        /// Convierte cantidad de UMBas a presentación.
        /// Ejemplo: 24 unidades ÷ 12 = 2 cajas
        /// </summary>
        public double ConvertToPresentation(double quantityInUMBas, double presentationFactor)
        {
            if (presentationFactor <= 0)
                throw new ArgumentException("presentationFactor debe ser > 0", nameof(presentationFactor));
            
            return Math.Round(quantityInUMBas / presentationFactor, 6);
        }

        /// <summary>
        /// Calcula la cantidad pendiente en presentación.
        /// Ejemplo: 30 unidades pendientes ÷ 12 = 2.5 cajas
        /// </summary>
        public double CalculatePendingInPresentation(double pendingInUMBas, double presentationFactor)
        {
            if (presentationFactor <= 0)
                throw new ArgumentException("presentationFactor debe ser > 0", nameof(presentationFactor));
            
            return Math.Round(pendingInUMBas / presentationFactor, 6);
        }
    }
}
