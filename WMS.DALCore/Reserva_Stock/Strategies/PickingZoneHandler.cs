
using WMS.EntityCore.Stock;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Strategies
{
    /// <summary>
    /// Handler 3: Reserva en Zona Picking.
    /// Prioriza stock en picking zone (FEFO).
    /// Target: ~100 líneas
    /// </summary>
    public class PickingZoneHandler : BaseReservationHandler
    {
        public PickingZoneHandler(IReservationLogger logger)
            : base(logger)
        {
        }

        protected override bool CanProcess(ReservationContext context)
        {
            var pickingStock = context.StockListPickingZone?
                .Where(s => s.Cantidad > 0)
                .ToList();

            return pickingStock != null && pickingStock.Count > 0;
        }

        protected override HandlerResult ProcessInternal(ReservationContext context)
        {
            var result = new HandlerResult { Success = true, CaseCode = "CASO_3" };

            if (!CanProcess(context))
            {
                if(_logger !=null)
                _logger.LogInfo("#CASO_3_SKIP - No stock en zona picking");
                return result;
            }
            if (_logger != null)
                _logger.LogCheckpoint("#CASO_3_START");

            // Filtrar stock de picking zone con fecha mínima
            var pickingStock = context.StockListPickingZone
                .Where(s => s.Cantidad > 0 &&
                           s.Fecha_vence == context.MinExpirationDatePickingZone)
                .OrderBy(s => s.Fecha_vence)
                .ThenBy(s => s.Lic_plate)
                .ToList();

            if (pickingStock.Count == 0)
            {
                if (_logger != null)
                    _logger.LogInfo("#CASO_3_SKIP - No stock con fecha mínima");
                return result;
            }

            // Reservar de picking zone
            foreach (var stock in pickingStock)
            {
                if (context.PendingQuantity <= 0.000001) break;

                double quantityToReserve = Math.Min(stock.Cantidad, context.PendingQuantity);

                if (quantityToReserve <= 0.000001) continue;

                // Crear reserva
                var reservation = CreateReservation(context, stock, quantityToReserve);

                // Actualizar stock y contexto
                stock.Cantidad -= quantityToReserve;
                context.PendingQuantity -= quantityToReserve;
                result.ReservedQuantity += quantityToReserve;
                result.Reservations.Add(reservation);
                if (_logger != null)
                    _logger.LogReservation(
                    reservation,
                    "CASO_3",
                    $"Picking zone | Stock: {stock.IdStock} | Cantidad: {quantityToReserve:F6}");
            }
            if (_logger != null)
                _logger.LogCheckpoint($"#CASO_3_END - Reservado: {result.ReservedQuantity:F6}");

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
