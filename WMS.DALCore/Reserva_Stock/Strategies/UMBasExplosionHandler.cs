using System;
using System.Linq;
using WMS.StockReservation.Core.Domain;
using WMS.StockReservation.Core.Interfaces;
using WMS.StockReservation.Core.Services;
using WMSWebAPI.Be;
using WMS.EntityCore.Stock;

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
            if (context == null) return false;

            // Solo procesa si esta habilitado el modo explosion o UMBas
            if (!context.IsExplosionModeEnabled && !context.IsUMBasModeEnabled)
                return false;

            // Verificar que haya stock disponible
            var availableStock = context.WorkingStockList?
                .Where(s => s != null && s.Cantidad > 0)
                .ToList();

            return availableStock != null && availableStock.Count > 0;
        }

        protected override HandlerResult ProcessInternal(ReservationContext context)
        {
            if (context == null)
                return new HandlerResult { Success = false, CaseCode = "CASO_EXPLOSION_NULL_CONTEXT" };

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
            // EXPLOSIÓN OPTIMIZADA: Minimizar explosión reservando cajas completas primero
            var presentationFactor = context.DefaultPresentation?.Factor ?? 1;
            double pendingUnits = context.PendingQuantity;

            _logger.LogInfo($"#EXPLOSION_OPTIMIZADA | Pedido: {pendingUnits:F6} unidades | Factor: {presentationFactor}");

            // PASO 1: Calcular cuántas CAJAS COMPLETAS se necesitan
            double completeBoxesNeeded = Math.Floor(pendingUnits / presentationFactor);
            double residualUnits = pendingUnits - (completeBoxesNeeded * presentationFactor);

            _logger.LogInfo($"#EXPLOSION_CALC | Cajas completas: {completeBoxesNeeded:F0} | Residuo: {residualUnits:F6} unidades");

            // Ordenar stock por FEFO
            var orderedStock = context.WorkingStockList
                .Where(s => s.Cantidad > 0)
                .OrderBy(s => s.Fecha_vence)
                .ThenBy(s => s.Lic_plate)
                .ToList();

            double pendingBoxes = completeBoxesNeeded;
            double pendingResidual = residualUnits;

            // PASO 2: Reservar CAJAS COMPLETAS (sin explosionar)
            if (completeBoxesNeeded > 0)
            {
                _logger.LogCheckpoint($"#EXPLOSION_BOXES_START | Reservando {completeBoxesNeeded:F0} cajas completas");

                foreach (var stock in orderedStock)
                {
                    if (pendingBoxes <= 0.000001) break;

                    // stock.Cantidad está en unidades, convertir a cajas
                    double stockInBoxes = Math.Floor(stock.Cantidad / presentationFactor);
                    if (stockInBoxes <= 0) continue;

                    double boxesToReserve = Math.Min(stockInBoxes, pendingBoxes);
                    double unitsFromBoxes = boxesToReserve * presentationFactor;

                    // Crear reserva con Cantidad en UNIDADES (consistente con el resto del sistema)
                    // IdPresentacion > 0 indica que la reserva proviene de stock con presentación
                    var reservation = CreateReservation(context, stock, unitsFromBoxes, isUMBas: false);

                    // Actualizar stock (descontar unidades)
                    stock.Cantidad -= unitsFromBoxes;
                    pendingBoxes -= boxesToReserve;
                    
                    // result.ReservedQuantity debe ser en UNIDADES para el flujo principal
                    result.ReservedQuantity += unitsFromBoxes;
                    result.Reservations.Add(reservation);

                    _logger.LogReservation(
                        reservation,
                        "CASO_EXPLOSION_BOXES",
                        $"Cajas completas | Stock: {stock.IdStock} | Cajas: {boxesToReserve:F0} = {unitsFromBoxes:F0} unidades");
                }

                _logger.LogCheckpoint($"#EXPLOSION_BOXES_END | Reservado: {(completeBoxesNeeded - pendingBoxes):F0} cajas");
            }

            // PASO 3: Explosionar SOLO para el RESIDUO
            if (residualUnits > 0.000001)
            {
                _logger.LogCheckpoint($"#EXPLOSION_RESIDUAL_START | Explosionando {residualUnits:F6} unidades");

                foreach (var stock in orderedStock)
                {
                    if (pendingResidual <= 0.000001) break;
                    if (stock.Cantidad <= 0.000001) continue;

                    double quantityToReserve = Math.Min(stock.Cantidad, pendingResidual);

                    // Crear reserva en UMBas (IdPresentacion = 0)
                    var reservation = CreateReservation(context, stock, quantityToReserve, isUMBas: true);

                    // Actualizar stock
                    stock.Cantidad -= quantityToReserve;
                    pendingResidual -= quantityToReserve;
                    result.ReservedQuantity += quantityToReserve;
                    result.Reservations.Add(reservation);

                    _logger.LogReservation(
                        reservation,
                        "CASO_EXPLOSION_RESIDUAL",
                        $"Unidades sueltas | Stock: {stock.IdStock} | Cantidad: {quantityToReserve:F6}");
                }

                _logger.LogCheckpoint($"#EXPLOSION_RESIDUAL_END | Reservado: {(residualUnits - pendingResidual):F6} unidades");
            }

            _logger.LogCheckpoint($"#CASO_EXPLOSION_END | Total reservado: {result.ReservedQuantity:F6} unidades");
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
                // NO modificar context.PendingQuantity - lo hace ReservationLoopStep
                double stockDecrease = _converter.ConvertToPresentation(quantityToReserve, presentationFactor);
                stock.Cantidad -= stockDecrease;
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
