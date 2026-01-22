using WMS.EntityCore.Datos_Maestros;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Servicio para manejo de cantidades decimales.
    /// Maneja la lógica de split decimal (parte entera vs decimal) y validaciones.
    /// </summary>
    public class DecimalQuantityHandler
    {
        /// <summary>
        /// Separa un valor en su parte entera y decimal.
        /// Ejemplo: 2.75 → (2, 0.75)
        /// Mapea a la función Split_Decimal del original.
        /// </summary>
        public (int wholePart, double decimalPart) SplitDecimal(double value)
        {
            int whole = (int)Math.Floor(value);
            double decimalPart = value - whole;
            decimalPart = Math.Round(decimalPart, 6);

            return (whole, decimalPart);
        }

        /// <summary>
        /// Convierte la parte decimal de una presentación a UMBas, redondeando hacia arriba.
        /// Ejemplo: 0.5 cajas × 12 = 6 unidades
        /// </summary>
        public double CeilingDecimalToUMBas(double decimalPart, double presentationFactor)
        {
            if (decimalPart < 0 || decimalPart >= 1)
                throw new ArgumentException("decimalPart debe estar entre 0 y 1", nameof(decimalPart));

            if (presentationFactor <= 0)
                throw new ArgumentException("presentationFactor debe ser > 0", nameof(presentationFactor));

            double umbasQuantity = decimalPart * presentationFactor;
            return Math.Ceiling(Math.Round(umbasQuantity, 2));
        }

        /// <summary>
        /// Redondea una cantidad al número de decimales especificado.
        /// Por defecto usa 6 decimales (estándar del sistema).
        /// </summary>
        public double RoundQuantity(double quantity, int decimals = 6)
        {
            return Math.Round(quantity, decimals);
        }

        /// <summary>
        /// Valida si se permite cantidad decimal según la bodega.
        /// Lanza excepción si hay decimal cuando la bodega no lo permite.
        /// </summary>
        public void ValidateDecimalAllowed(double quantity, clsBeBodega bodega)
        {
            if (bodega == null)
                throw new ArgumentNullException(nameof(bodega));

            if (!bodega.Permitir_decimales)
            {
                var (whole, decimalPart) = SplitDecimal(quantity);

                if (decimalPart > 0.000001)
                {
                    throw new InvalidOperationException(
                        $"La bodega '{bodega.Codigo}' no permite cantidades decimales. " +
                        $"Cantidad recibida: {quantity}");
                }
            }
        }
    }
}