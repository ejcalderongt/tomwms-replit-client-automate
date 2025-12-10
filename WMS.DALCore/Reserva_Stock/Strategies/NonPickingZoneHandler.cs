using WMSWebAPI.Be;
using WMS.EntityCore.Stock;

namespace WMS.StockReservation.Strategies
{
    /// <summary>
    /// Handler 4: Reserva en Zonas no-Picking.
    /// Última opción antes de fallback a explosión/UMBas.
    /// Target: ~100 líneas
    /// </summary>
    public class NonPickingZoneHandler : BaseReservationHandler
    {
        public NonPickingZoneHandler(IReservationLogger logger)
            : base(logger)
        {
        }

        protected override bool CanProcess(ReservationContext context)
        {
            var nonPickingStock = context.StockListNonPickingZones?
                .Where(s => s.Cantidad > 0)
                .ToList();

            return nonPickingStock != null && nonPickingStock.Count > 0;
        }

        protected override HandlerResult ProcessInternal(ReservationContext context)
        {
            var result = new HandlerResult { Success = true, CaseCode = "CASO_4" };

            if (!CanProcess(context))
            {
                _logger.LogInfo("#CASO_4_SKIP - No stock en zonas no-picking");
                return result;
            }

            _logger.LogCheckpoint("#CASO_4_START");

            // Filtrar stock de zonas no-picking con fecha mínima
            var nonPickingStock = context.StockListNonPickingZones
                .Where(s => s.Cantidad > 0 &&
                           s.Fecha_vence == context.MinExpirationDateNonPickingZones)
                .OrderBy(s => s.Fecha_vence)
                .ThenBy(s => s.Lic_plate)
                .ToList();

            if (nonPickingStock.Count == 0)
            {
                _logger.LogInfo("#CASO_4_SKIP - No stock con fecha mínima");
                return result;
            }

            // Reservar de zonas no-picking
            foreach (var stock in nonPickingStock)
            {
                if (context.PendingQuantity <= 0.000001) break;

                double quantityToReserve = Math.Min(stock.Cantidad, context.PendingQuantity);

                if (quantityToReserve <= 0.000001) continue;

                // Crear reserva
                var reservation = CreateReservation(context, stock, quantityToReserve);

                // Actualizar stock (NO modificar context.PendingQuantity - lo hace ReservationLoopStep)
                stock.Cantidad -= quantityToReserve;
                result.ReservedQuantity += quantityToReserve;
                result.Reservations.Add(reservation);

                _logger.LogReservation(
                    reservation,
                    "CASO_4",
                    $"Non-picking zone | Stock: {stock.IdStock} | Cantidad: {quantityToReserve:F6}");
            }

            _logger.LogCheckpoint($"#CASO_4_END - Reservado: {result.ReservedQuantity:F6}");

            return result;
        }

        private clsBeStock_res CreateReservation(
            ReservationContext context,
            clsBeStock stock,
            double quantity)
        {
            var reservation = new clsBeStock_res
            {
                // IDs principales
                IdStock = stock.IdStock,
                IdBodega = stock.IdBodega,
                IdProductoBodega = stock.IdProductoBodega,
                IdPropietarioBodega = stock.IdPropietarioBodega,
                IdProductoEstado = stock.IdProductoEstado,
                IdUbicacion = stock.IdUbicacion,
                
                // Presentación y cantidad
                IdPresentacion = context.Request.IdPresentacion,
                IdUnidadMedida = stock.IdUnidadMedida,
                Cantidad = quantity,
                
                // Trazabilidad del stock
                Lote = stock.Lote,
                Lic_plate = stock.Lic_plate,
                Serial = stock.Serial,
                Uds_lic_plate = stock.Uds_lic_plate,
                No_bulto = stock.No_bulto,
                
                // Fechas
                Fecha_ingreso = stock.Fecha_Ingreso,
                Fecha_vence = stock.Fecha_vence,
                Fecha_manufactura = stock.Fecha_Manufactura,
                Añada = stock.Añada,
                
                // Flags
                Pallet_no_estandar = stock.Pallet_No_Estandar,
                
                // Host/auditoría
                Host = context.MachineName ?? Environment.MachineName
            };

            if (context.PedidoDet != null)
            {
                reservation.IdPedidoDet = context.PedidoDet.IdPedidoDet;
            }

            return reservation;
        }
    }
}
