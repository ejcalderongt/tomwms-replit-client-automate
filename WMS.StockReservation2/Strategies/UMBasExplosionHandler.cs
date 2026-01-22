using WMS.EntityCore.Stock;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Strategies
{
    /// <summary>
    /// Handler 5: Explosión/UMBas.
    /// Maneja la conversión entre presentaciones y UMBas.
    /// Activo cuando IsExplosionModeEnabled o IsUMBasModeEnabled = true.
    /// Target: ~150 líneas
    /// </summary>
    public class UMBasExplosionHandler : BaseReservationHandler
    {
        private readonly DecimalQuantityHandler _decimalHandler;
        private readonly QuantityConverter _converter;

        public UMBasExplosionHandler(IReservationLogger logger)
            : base(logger)
        {
            _decimalHandler = new DecimalQuantityHandler();
            _converter = new QuantityConverter();
        }

        protected override bool CanProcess(ReservationContext context)
        {
            // Solo procesa si está habilitado el modo explosión o UMBas
            if (!context.IsExplosionModeEnabled && !context.IsUMBasModeEnabled)
                return false;

            // Verificar que haya stock disponible
            var availableStock = context.WorkingStockList?
                .Where(s => s.Cantidad > 0)
                .ToList();

            return availableStock != null && availableStock.Count > 0;
        }

        protected override HandlerResult ProcessInternal(ReservationContext context)
        {
            var result = new HandlerResult { Success = true, CaseCode = "CASO_EXPLOSION" };

            if (!CanProcess(context))
            {
                _logger.LogInfo("#CASO_EXPLOSION_SKIP - Modo no habilitado o sin stock");
                return result;
            }

            if (context.IsExplosionModeEnabled)
            {
                _logger.LogCheckpoint("#CASO_EXPLOSION_START");
                ProcessExplosion(context, result);
            }
            else if (context.IsUMBasModeEnabled)
            {
                _logger.LogCheckpoint("#CASO_UMBAS_START");
                ProcessUMBas(context, result);
            }

            return result;
        }

        private void ProcessExplosion(ReservationContext context, HandlerResult result)
        {
            // Explosión: romper cajas/pallets en unidades
            var presentationFactor = context.DefaultPresentation?.Factor ?? 1;

            // Convertir pending de presentación a UMBas
            double pendingInUMBas = _converter.ConvertToUMBas(
                context.PendingQuantity,
                presentationFactor);

            _logger.LogInfo($"Explosión: {context.PendingQuantity:F6} presentación = {pendingInUMBas:F6} UMBas");

            // Ordenar stock por FEFO y lic_plate
            var orderedStock = context.WorkingStockList
                .Where(s => s.Cantidad > 0)
                .OrderBy(s => s.Fecha_vence)
                .ThenBy(s => s.Lic_plate)
                .ToList();

            // Reservar en UMBas
            foreach (var stock in orderedStock)
            {
                if (pendingInUMBas <= 0.000001) break;

                double quantityToReserve = Math.Min(stock.Cantidad, pendingInUMBas);

                if (quantityToReserve <= 0.000001) continue;

                // Crear reserva en UMBas (IdPresentacion = 0)
                var reservation = CreateReservation(context, stock, quantityToReserve, isUMBas: true);

                // Actualizar stock
                stock.Cantidad -= quantityToReserve;
                pendingInUMBas -= quantityToReserve;
                result.ReservedQuantity += quantityToReserve;
                result.Reservations.Add(reservation);

                _logger.LogReservation(
                    reservation,
                    "CASO_EXPLOSION",
                    $"Explosión a UMBas | Stock: {stock.IdStock} | Cantidad: {quantityToReserve:F6}");
            }

            // Actualizar pending en presentación
            double reservedInPresentation = _converter.ConvertToPresentation(
                result.ReservedQuantity,
                presentationFactor);

            context.PendingQuantity -= reservedInPresentation;
            context.PendingQuantity = Math.Round(context.PendingQuantity, 6);

            _logger.LogCheckpoint($"#CASO_EXPLOSION_END - Reservado: {result.ReservedQuantity:F6} UMBas");
        }

        private void ProcessUMBas(ReservationContext context, HandlerResult result)
        {
            // UMBas: reservar en unidad base desde stock en presentación
            var presentationFactor = context.DefaultPresentation?.Factor ?? 1;

            _logger.LogInfo($"UMBas: Pendiente {context.PendingQuantity:F6} UMBas");

            // Ordenar stock por FEFO y lic_plate
            var orderedStock = context.WorkingStockList
                .Where(s => s.Cantidad > 0)
                .OrderBy(s => s.Fecha_vence)
                .ThenBy(s => s.Lic_plate)
                .ToList();

            // Reservar directamente en UMBas
            foreach (var stock in orderedStock)
            {
                if (context.PendingQuantity <= 0.000001) break;

                // Convertir stock disponible a UMBas
                double stockInUMBas = _converter.ConvertToUMBas(stock.Cantidad, presentationFactor);
                double quantityToReserve = Math.Min(stockInUMBas, context.PendingQuantity);

                if (quantityToReserve <= 0.000001) continue;

                // Crear reserva en UMBas
                var reservation = CreateReservation(context, stock, quantityToReserve, isUMBas: true);

                // Actualizar stock (descontar en presentación)
                double stockDecrease = _converter.ConvertToPresentation(quantityToReserve, presentationFactor);
                stock.Cantidad -= stockDecrease;
                context.PendingQuantity -= quantityToReserve;
                result.ReservedQuantity += quantityToReserve;
                result.Reservations.Add(reservation);

                _logger.LogReservation(
                    reservation,
                    "CASO_UMBAS",
                    $"UMBas | Stock: {stock.IdStock} | Cantidad: {quantityToReserve:F6}");
            }

            _logger.LogCheckpoint($"#CASO_UMBAS_END - Reservado: {result.ReservedQuantity:F6} UMBas");
        }

        private clsBeStock_res CreateReservation(
            ReservationContext context,
            clsBeStock stock,
            double quantity,
            bool isUMBas)
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
                
                // Presentación y cantidad (UMBas no tiene presentación)
                IdPresentacion = isUMBas ? 0 : context.Request.IdPresentacion,
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
