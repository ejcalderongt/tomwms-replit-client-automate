using WMS.EntityCore.Stock;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Strategies
{
    /// <summary>
    /// Handler 2: Paquetes Incompletos en zonas no-Picking (Clavaud).
    /// Target: ~100 líneas
    /// </summary>
    public class IncompletePackagesHandler : BaseReservationHandler
    {
        public IncompletePackagesHandler(IReservationLogger logger)
            : base(logger)
        {
        }

        protected override bool CanProcess(ReservationContext context)
        {
            if (!context.Configuration.Conservar_Zona_Picking_Clavaud)
                return false;

            var incompleteStock = context.StockListNonPickingZones?
                .Where(s => !s.Pallet_Completo &&
                           !s.UbicacionPicking &&
                           s.UbicacionNivel > 0 &&
                           s.Cantidad > 0)
                .ToList();

            return incompleteStock != null && incompleteStock.Count > 0;
        }

        protected override HandlerResult ProcessInternal(ReservationContext context)
        {
            var result = new HandlerResult { Success = true, CaseCode = "CASO_2" };

            if (!CanProcess(context))
            {
                _logger.LogInfo("#CASO_2_SKIP - No stock en pallets incompletos");
                return result;
            }

            _logger.LogCheckpoint("#CASO_2_START");

            // Filtrar stock de pallets incompletos
            var incompleteStock = context.StockListNonPickingZones
                .Where(s => !s.Pallet_Completo &&
                           !s.UbicacionPicking &&
                           s.UbicacionNivel > 0 &&
                           s.Cantidad > 0 &&
                           s.Fecha_vence == context.MinExpirationIncompletePalletsClavaud)
                .OrderBy(s => s.Fecha_vence)
                .ThenBy(s => s.Lic_plate)
                .ToList();

            if (incompleteStock.Count == 0)
            {
                _logger.LogInfo("#CASO_2_SKIP - No stock con fecha mínima");
                return result;
            }

            // Reservar de pallets incompletos
            foreach (var stock in incompleteStock)
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

                _logger.LogReservation(
                    reservation,
                    "CASO_2",
                    $"Pallet incompleto | Stock: {stock.IdStock} | Cantidad: {quantityToReserve:F6}");
            }

            _logger.LogCheckpoint($"#CASO_2_END - Reservado: {result.ReservedQuantity:F6}");

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
                Fecha_ingreso = stock.Fecha_ingreso,
                Fecha_vence = stock.Fecha_vence,
                Fecha_manufactura = stock.Fecha_manufactura,
                Añada = stock.Añada,
                
                // Flags
                Pallet_no_estandar = stock.Pallet_no_estandar,
                
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
