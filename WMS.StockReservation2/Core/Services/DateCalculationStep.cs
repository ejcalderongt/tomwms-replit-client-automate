using System;
using System.Linq;
using WMS.StockReservation.Core.Domain;
using WMS.StockReservation.Core.Interfaces;

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
            if (context.StockListPickingZone != null && context.StockListPickingZone.Count > 0)
            {
                context.MinExpirationDatePickingZone = context.StockListPickingZone.Min(s => s.Fecha_vence);
            }

            // Calcular fecha mínima en zonas no-Picking
            context.MinExpirationDateNonPickingZones = defaultDate;
            if (context.StockListNonPickingZones != null && context.StockListNonPickingZones.Count > 0)
            {
                context.MinExpirationDateNonPickingZones = context.StockListNonPickingZones.Min(s => s.Fecha_vence);
            }

            // Determinar fecha mínima global
            context.GlobalMinimumExpirationDate = defaultDate;
            if (context.WorkingStockList != null && context.WorkingStockList.Count > 0)
            {
                context.GlobalMinimumExpirationDate = context.WorkingStockList.Min(s => s.Fecha_vence);
            }

            // Para Clavaud: calcular fechas para pallets completos/incompletos
            if (context.Configuration.Conservar_Zona_Picking_Clavaud)
            {
                // Pallets completos
                var completePallets = context.StockListNonPickingZones?
                    .Where(s => s.Pallet_Completo &&
                               !s.UbicacionPicking &&
                               s.UbicacionNivel > 0)
                    .ToList();

                if (completePallets != null && completePallets.Count > 0)
                {
                    context.MinExpirationCompletePalletsClavaud = completePallets.Min(s => s.Fecha_vence);
                }

                // Pallets incompletos
                var incompletePallets = context.StockListNonPickingZones?
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
