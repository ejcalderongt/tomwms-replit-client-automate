using WMS.EntityCore.Stock;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Strategies
{
    /// <summary>
    /// Handler 1: Paquetes Completos en zonas no-Picking (Clavaud).
    /// Intenta reservar pallets completos primero.
    /// Target: ~120 líneas
    /// </summary>
    public class CompletePackagesHandler : BaseReservationHandler
    {
        private readonly QuantityConverter _converter;

        public CompletePackagesHandler(IReservationLogger logger)
            : base(logger)
        {
            _converter = new QuantityConverter();
        }

        protected override bool CanProcess(ReservationContext context)
        {
            if (!context.Configuration.Conservar_Zona_Picking_Clavaud)
                return false;

            var completeStock = context.StockListNonPickingZones?
                .Where(s => s.Pallet_Completo &&
                           !s.UbicacionPicking &&
                           s.UbicacionNivel > 0 &&
                           s.Cantidad > 0)
                .ToList();

            return completeStock != null && completeStock.Count > 0;
        }

        protected override HandlerResult ProcessInternal(ReservationContext context)
        {
            var result = new HandlerResult { Success = true, CaseCode = "CASO_1" };

            if (!CanProcess(context))
            {
                if (_logger != null)
                    _logger.LogInfo("#CASO_1_SKIP - No stock en pallets completos");
                return result;
            }
            if (_logger != null)
                _logger.LogCheckpoint("#CASO_1_START");

            // Filtrar stock de pallets completos
            var completeStock = context.StockListNonPickingZones
                .Where(s => s.Pallet_Completo &&
                           !s.UbicacionPicking &&
                           s.UbicacionNivel > 0 &&
                           s.Cantidad > 0 &&
                           s.Fecha_vence == context.MinExpirationCompletePalletsClavaud)
                .OrderBy(s => s.Fecha_vence)
                .ThenBy(s => s.Lic_plate)
                .ToList();

            if (completeStock.Count == 0)
            {
                if (_logger != null)
                    _logger.LogInfo("#CASO_1_SKIP - No stock con fecha mínima");
                return result;
            }

            // Reservar de pallets completos
            foreach (var stock in completeStock)
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
                    "CASO_1",
                    $"Pallet completo | Stock: {stock.IdStock} | Cantidad: {quantityToReserve:F6}");
            }

            if (_logger != null)
                _logger.LogCheckpoint($"#CASO_1_END - Reservado: {result.ReservedQuantity:F6}");

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

            // Copiar detalles adicionales del pedido
            if (context.PedidoDet != null)
            {
                reservation.IdPedidoDet = context.PedidoDet.IdPedidoDet;
            }

            return reservation;
        }
    }
}
