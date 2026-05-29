namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Paso 4: Cálculo de fechas mínimas de vencimiento (FEFO/FIFO).
    /// Calcula las fechas que guiarán las decisiones de reserva.
    /// Target: ~90 líneas
    /// </summary>
    public class DateCalculationStep : IPipelineStep
    {
        private readonly IReservationLogger _logger;

        public DateCalculationStep(IReservationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(ReservationContext context)
        {
            _logger.LogCheckpoint("#MI3_DATE_CALC_START");

            var defaultDate = new DateTime(1900, 1, 1);

            // Calcular fecha mínima en zona Picking
            context.MinExpirationDatePickingZone = defaultDate;
            var stockPickingDisponible = context.StockListPickingZone?
                .Where(s => s != null && s.Cantidad > 0.000001)
                .ToList();
            if (stockPickingDisponible != null && stockPickingDisponible.Count > 0)
            {
                context.MinExpirationDatePickingZone = stockPickingDisponible.Min(s => s.Fecha_vence);
            }

            // Calcular fecha mínima en zonas no-Picking
            context.MinExpirationDateNonPickingZones = defaultDate;
            var stockNoPickingDisponible = context.StockListNonPickingZones?
                .Where(s => s != null && s.Cantidad > 0.000001)
                .ToList();
            if (stockNoPickingDisponible != null && stockNoPickingDisponible.Count > 0)
            {
                context.MinExpirationDateNonPickingZones = stockNoPickingDisponible.Min(s => s.Fecha_vence);
            }

            // Determinar fecha mínima global
            context.GlobalMinimumExpirationDate = defaultDate;
            var stockDisponible = context.WorkingStockList?
                .Where(s => s != null && s.Cantidad > 0.000001)
                .ToList();
            if (stockDisponible != null && stockDisponible.Count > 0)
            {
                context.GlobalMinimumExpirationDate = stockDisponible.Min(s => s.Fecha_vence);
            }

            // Para Clavaud: calcular fechas para pallets completos/incompletos
            context.MinExpirationCompletePalletsClavaud = defaultDate;
            context.MinExpirationIncompletePalletsClavaud = defaultDate;
            if (context.Configuration.Conservar_Zona_Picking_Clavaud)
            {
                // Pallets completos
                var completePallets = stockNoPickingDisponible?
                    .Where(s => s.Pallet_Completo &&
                               !s.UbicacionPicking &&
                               s.UbicacionNivel > 0)
                    .ToList();

                if (completePallets != null && completePallets.Count > 0)
                {
                    context.MinExpirationCompletePalletsClavaud = completePallets.Min(s => s.Fecha_vence);
                }

                // Pallets incompletos
                var incompletePallets = stockNoPickingDisponible?
                    .Where(s => !s.Pallet_Completo &&
                               !s.UbicacionPicking &&
                               s.UbicacionNivel > 0)
                    .ToList();

                if (incompletePallets != null && incompletePallets.Count > 0)
                {
                    context.MinExpirationIncompletePalletsClavaud = incompletePallets.Min(s => s.Fecha_vence);
                }
            }

            _logger.LogCheckpoint(
                $"#MI3_DATE_CALC_OK - " +
                $"Global: {context.GlobalMinimumExpirationDate:yyyy-MM-dd}, " +
                $"Picking: {context.MinExpirationDatePickingZone:yyyy-MM-dd}, " +
                $"NonPicking: {context.MinExpirationDateNonPickingZones:yyyy-MM-dd}");

            if (context.Configuration.Conservar_Zona_Picking_Clavaud)
            {
                _logger.LogInfo(
                    $"Clavaud - CompletePallets: {context.MinExpirationCompletePalletsClavaud:yyyy-MM-dd}, " +
                    $"IncompletePallets: {context.MinExpirationIncompletePalletsClavaud:yyyy-MM-dd}");
            }
        }
    }
}
