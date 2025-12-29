using WMSWebAPI.Be;
using static clsBeI_nav_config_enc;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Paso 5: Loop de reserva iterativo con re-entry y fallbacks.
    /// Este es el componente mas critico que reproduce el control flow del MI3 original.
    /// Target: ~180 lineas
    /// </summary>
    public class ReservationLoopStep : IPipelineStep
    {
        private readonly IServiceFactory _factory;
        private readonly IReservationLogger _logger;
        private const int MAX_ITERATIONS = 10;

        public ReservationLoopStep(IServiceFactory factory, IReservationLogger logger)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(ReservationContext context)
        {
            _logger.LogCheckpoint("#MI3_LOOP_START");

            int iteration = 0;
            bool hasProgress = true;

            // ===== LOOP PRINCIPAL CON RE-ENTRY =====
            while (context.PendingQuantity > 0.000001 &&
                   iteration < MAX_ITERATIONS &&
                   hasProgress)
            {
                iteration++;
                double previousPending = context.PendingQuantity;

                _logger.LogCheckpoint($"#MI3_ITERATION_{iteration} | Pendiente: {context.PendingQuantity:F6}");

                // PASO 1: Determinar punto de inicio (INICIAR_EN)
                int startingPoint = DetermineStartingPoint(context);
                context.StartingPoint = startingPoint;

                _logger.LogCheckpoint($"#MI3_STARTING_POINT_{startingPoint}");

                // PASO 2: RECONSTRUIR cadena segun punto de inicio
                IReservationHandler handlerChain = _factory.BuildHandlerChain(
                    startingPoint,
                    context,
                    _logger);

                // PASO 3: Ejecutar cadena (puede procesar parcialmente)
                var result = handlerChain.Handle(context);

                // DEBUG: Log completo del resultado
                _logger.LogCheckpoint(
                    $"#DEBUG_HANDLER_RESULT | " +
                    $"Success: {result.Success} | " +
                    $"ReservedQuantity: {result.ReservedQuantity:F6} | " +
                    $"Reservations.Count: {result.Reservations?.Count ?? 0} | " +
                    $"CaseCode: {result.CaseCode} | " +
                    $"Message: {result.Message}");

                // PASO 4: Actualizar contexto con CLAMP para evitar PendingQuantity negativo
                if (result.Success && result.ReservedQuantity > 0)
                {
                    // CRITICAL FIX: Limitar cantidad reservada a lo que realmente estaba pendiente
                    double allowed = Math.Min(previousPending, result.ReservedQuantity);
                    
                    if (result.ReservedQuantity > previousPending + 0.000001)
                    {
                        _logger.LogCheckpoint(
                            $"#WARNING_OVER_RESERVATION | " +
                            $"ReservedQuantity={result.ReservedQuantity:F6} > PreviousPending={previousPending:F6} | " +
                            $"Clamping to {allowed:F6}");
                    }
                    
                    context.PendingQuantity -= allowed;
                    context.PendingQuantity = Math.Max(0, Math.Round(context.PendingQuantity, 6));

                    if (result.Reservations != null && result.Reservations.Count > 0)
                    {
                        // CRITICAL FIX: Solo agregar reservas que cubran hasta 'allowed'
                        double cumulativeReserved = 0;
                        var validReservations = new List<clsBeStock_res>();
                        
                        foreach (var reservation in result.Reservations)
                        {
                            if (cumulativeReserved >= allowed)
                            {
                                _logger.LogCheckpoint(
                                    $"#DISCARDING_EXCESS_RESERVATION | " +
                                    $"IdStock={reservation.IdStock} | " +
                                    $"Cantidad={reservation.Cantidad:F6} | " +
                                    $"CumulativeAlready={cumulativeReserved:F6} >= Allowed={allowed:F6}");
                                continue;
                            }
                            
                            double canTake = Math.Min(reservation.Cantidad, allowed - cumulativeReserved);
                            
                            if (Math.Abs(canTake - reservation.Cantidad) > 0.000001)
                            {
                                // Ajustar la cantidad de la reserva
                                _logger.LogCheckpoint(
                                    $"#ADJUSTING_RESERVATION | " +
                                    $"IdStock={reservation.IdStock} | " +
                                    $"Original={reservation.Cantidad:F6} | " +
                                    $"Adjusted={canTake:F6}");
                                reservation.Cantidad = canTake;
                            }
                            
                            cumulativeReserved += canTake;
                            validReservations.Add(reservation);
                        }
                        
                        context.CreatedReservations.AddRange(validReservations);
                        
                        _logger.LogCheckpoint(
                            $"#MI3_RESERVATIONS_ADDED | " +
                            $"Count: {validReservations.Count} (of {result.Reservations.Count}) | " +
                            $"TotalInContext: {context.CreatedReservations.Count}");
                    }
                    else
                    {
                        _logger.LogCheckpoint(
                            $"#WARNING_NO_RESERVATIONS | " +
                            $"Success=true pero Reservations es null/empty");
                    }

                    _logger.LogCheckpoint(
                        $"#MI3_PROGRESS | Reservado: {allowed:F6} (clamped from {result.ReservedQuantity:F6}) | Casos: {result.CaseCode}");
                }
                else
                {
                    // Problema: No se procesa el resultado
                    string reason = !result.Success ? "Success es false" : "ReservedQuantity <= 0";
                    _logger.LogCheckpoint(
                        $"#WARNING_RESULT_REJECTED | " +
                        $"Success={result.Success} | " +
                        $"ReservedQuantity={result.ReservedQuantity:F6} | " +
                        $"Reason: {reason}");
                }

                // PASO 5: Evaluar progreso
                hasProgress = Math.Abs(previousPending - context.PendingQuantity) > 0.000001;

                if (!hasProgress)
                {
                    // Sin progreso: intentar fallbacks

                    if (TryEnableExplosionFallback(context))
                    {
                        _logger.LogCheckpoint("#MI3_FALLBACK_EXPLOSION");
                        ReQueryStockForExplosion(context);
                        hasProgress = true;
                        continue;
                    }

                    if (TryEnableUMBasFallback(context))
                    {
                        _logger.LogCheckpoint("#MI3_FALLBACK_UMBAS");
                        ReQueryStockForUMBas(context);
                        hasProgress = true;
                        continue;
                    }

                    // No hay mas fallbacks
                    _logger.LogCheckpoint("#MI3_NO_MORE_OPTIONS");
                    break;
                }

                // PASO 6: Re-calcular fechas minimas para proxima iteracion
                if (context.PendingQuantity > 0.000001)
                {
                    RecalculateMinimumDates(context);
                }

                // Validar invariantes en DEBUG
                #if DEBUG
                context.ValidateInvariants($"ITERATION_{iteration}_END");
                #endif
            }

            // Validaciones finales
            if (iteration >= MAX_ITERATIONS)
            {
                _logger.LogError($"#MI3_MAX_ITERATIONS | Alcanzadas {MAX_ITERATIONS} iteraciones");
            }

            if (context.PendingQuantity > 0.000001 &&
                context.Configuration.Rechazar_pedido_incompleto == tRechazarPedidoIncompleto.Si)
            {
                throw new Exception(
                    $"PEDIDO_INCOMPLETO | Solicitado: {context.Request.Cantidad:F6} | " +
                    $"Reservado: {context.Request.Cantidad - context.PendingQuantity:F6} | " +
                    $"Faltante: {context.PendingQuantity:F6}");
            }

            _logger.LogCheckpoint(
                $"#MI3_LOOP_END | Iteraciones: {iteration} | " +
                $"Total Reservado: {context.Request.Cantidad - context.PendingQuantity:F6}");
        }

        private int DetermineStartingPoint(ReservationContext context)
        {
            var defaultDate = new DateTime(1900, 1, 1);
            int startingPoint = 0;

            // === LOGICA CLAVAUD (pallets completos/incompletos) ===
            if (context.Configuration.Conservar_Zona_Picking_Clavaud)
            {
                if (context.MinExpirationCompletePalletsClavaud > context.GlobalMinimumExpirationDate &&
                    context.MinExpirationCompletePalletsClavaud > defaultDate)
                {
                    return 1; // INICIAR_EN_1
                }

                if (context.GlobalMinimumExpirationDate >= context.MinExpirationIncompletePalletsClavaud &&
                    context.MinExpirationIncompletePalletsClavaud > defaultDate)
                {
                    return 2; // INICIAR_EN_2
                }
            }

            // === LOGICA ZONA PICKING ===
            if (context.GlobalMinimumExpirationDate > context.MinExpirationDatePickingZone &&
                context.MinExpirationDatePickingZone > defaultDate)
            {
                return 3; // INICIAR_EN_3
            }

            if (context.GlobalMinimumExpirationDate > context.MinExpirationDateNonPickingZones &&
                context.MinExpirationDateNonPickingZones > defaultDate)
            {
                return 3; // INICIAR_EN_3
            }

            // === LOGICA ALMACEN GENERAL ===
            if (context.MinExpirationDateNonPickingZones > defaultDate &&
                context.StockListNonPickingZones != null &&
                context.StockListNonPickingZones.Count > 0)
            {
                return 4; // INICIAR_EN_4
            }

            return startingPoint; // 0 = ultima lista (default)
        }

        private bool TryEnableExplosionFallback(ReservationContext context)
        {
            // EXPLOSION: Romper cajas en unidades
            // Solo aplica cuando:
            //   1. Piden UNIDADES (IdPresentacion == 0)
            //   2. Pero solo hay stock en CAJAS
            // NUNCA explosionar si piden CAJAS (IdPresentacion > 0)
            
            if (context.IsExplosionModeEnabled) return false;
            if (!context.Configuration.Explosion_Automatica) return false;
            
            // CRITICAL: Si piden CAJAS, NO explosionar - solo reservar cajas disponibles
            if (context.Request.IdPresentacion > 0) return false;
            
            if (context.DefaultPresentation == null) return false;

            context.IsExplosionModeEnabled = true;
            _logger.LogInfo("#EXPLOSION_ENABLED - Pedido en unidades, buscando cajas para explosionar");
            return true;
        }

        private bool TryEnableUMBasFallback(ReservationContext context)
        {
            if (context.IsUMBasModeEnabled) return false;
            if (context.Request.IdPresentacion != 0) return false;
            if (context.DefaultPresentation == null) return false;

            context.IsUMBasModeEnabled = true;
            return true;
        }

        private void ReQueryStockForExplosion(ReservationContext context)
        {
            // NOTA: Pasamos el Request original directamente ya que lStock lo recibe por valor
            // (no se modificara). El filtrado por IdPresentacion se maneja en el SQL interno
            
            var tempProduct = context.Product;
            var tempRequest = context.Request;

            var newStock = clsLnStock.lStock(
                ref tempRequest,
                ref tempProduct,
                0,
                context.Configuration,
                context.Connection,
                context.Transaction,
                pExcluirUbicacionPicking: false,
                Conmutar_Umbas_A_Presentacion: false,
                pTarea_Reabasto: context.TareaReabasto,
                pEs_Devolucion: context.EsDevolucion);

            if (newStock != null && newStock.Count > 0)
            {
                clsLnStock_res.Restar_Stock_Reservado(newStock, context.Configuration,
                                                       context.Connection, context.Transaction);
                
                // CRITICAL: Restar tambien las reservas ya creadas en memoria (no en BD aun)
                // NOTA: En modo explosión, el stock ahora está en UMBas.
                // Si la reserva previa fue en presentación, debemos convertir al restar.
                double factor = context.DefaultPresentation?.Factor ?? 1;
                
                foreach (var reservation in context.CreatedReservations)
                {
                    var stockItem = newStock.FirstOrDefault(s => s.IdStock == reservation.IdStock);
                    if (stockItem != null)
                    {
                        // Si la reserva fue en presentación (IdPresentacion > 0) y ahora el stock
                        // está en UMBas, debemos multiplicar por el factor al restar
                        double cantidadARestar = reservation.Cantidad;
                        if (reservation.IdPresentacion > 0 && stockItem.IdPresentacion == 0)
                        {
                            // Reserva en cajas, stock en unidades → multiplicar
                            cantidadARestar = reservation.Cantidad * factor;
                            _logger.LogInfo($"Conversión al restar: {reservation.Cantidad} cajas = {cantidadARestar} unidades");
                        }
                        else if (reservation.IdPresentacion == 0 && stockItem.IdPresentacion > 0)
                        {
                            // Reserva en unidades, stock en cajas → dividir
                            cantidadARestar = reservation.Cantidad / factor;
                            _logger.LogInfo($"Conversión al restar: {reservation.Cantidad} unidades = {cantidadARestar} cajas");
                        }
                        
                        stockItem.Cantidad -= cantidadARestar;
                    }
                }
                
                newStock = newStock.Where(s => s.Cantidad > 0.000001).ToList();

                context.WorkingStockList = newStock;
                _logger.LogInfo($"Re-query explosion: {newStock.Count} registros (after subtracting {context.CreatedReservations.Count} in-memory reservations)");
            }
        }

        private void ReQueryStockForUMBas(ReservationContext context)
        {
            // NOTA: Pasamos el Request original directamente ya que lStock lo recibe por valor
            // (no se modificara). El parametro Conmutar_Umbas_A_Presentacion=true maneja la logica UMBas
            
            var tempProduct = context.Product;
            var tempRequest = context.Request;

            var newStock = clsLnStock.lStock(
                ref tempRequest,
                ref tempProduct,
                0,
                context.Configuration,
                context.Connection,
                context.Transaction,
                pExcluirUbicacionPicking: false,
                Conmutar_Umbas_A_Presentacion: true,
                pTarea_Reabasto: context.TareaReabasto,
                pEs_Devolucion: context.EsDevolucion);

            if (newStock != null && newStock.Count > 0)
            {
                clsLnStock_res.Restar_Stock_Reservado(newStock, context.Configuration,
                                                       context.Connection, context.Transaction);
                
                // CRITICAL: Restar tambien las reservas ya creadas en memoria (no en BD aun)
                // NOTA: En modo UMBas, el stock ahora está en presentación.
                // Si la reserva previa fue en UMBas, debemos convertir al restar.
                double factor = context.DefaultPresentation?.Factor ?? 1;
                
                foreach (var reservation in context.CreatedReservations)
                {
                    var stockItem = newStock.FirstOrDefault(s => s.IdStock == reservation.IdStock);
                    if (stockItem != null)
                    {
                        double cantidadARestar = reservation.Cantidad;
                        if (reservation.IdPresentacion > 0 && stockItem.IdPresentacion == 0)
                        {
                            cantidadARestar = reservation.Cantidad * factor;
                            _logger.LogInfo($"UMBas - Conversión: {reservation.Cantidad} cajas = {cantidadARestar} unidades");
                        }
                        else if (reservation.IdPresentacion == 0 && stockItem.IdPresentacion > 0)
                        {
                            cantidadARestar = reservation.Cantidad / factor;
                            _logger.LogInfo($"UMBas - Conversión: {reservation.Cantidad} unidades = {cantidadARestar} cajas");
                        }
                        
                        stockItem.Cantidad -= cantidadARestar;
                    }
                }
                
                newStock = newStock.Where(s => s.Cantidad > 0.000001).ToList();

                context.WorkingStockList = newStock;
                _logger.LogInfo($"Re-query UMBas: {newStock.Count} registros (after subtracting {context.CreatedReservations.Count} in-memory reservations)");
            }
        }

        private void RecalculateMinimumDates(ReservationContext context)
        {
            // CRITICAL: Recalcular TODAS las fechas mínimas por zona
            // De lo contrario, los handlers solo ven stock con la fecha anterior
            
            // Recalcular para zona Picking
            if (context.StockListPickingZone != null)
            {
                var validPicking = context.StockListPickingZone.Where(s => s.Cantidad > 0).ToList();
                if (validPicking.Count > 0)
                {
                    context.MinExpirationDatePickingZone = validPicking.Min(s => s.Fecha_vence);
                    _logger.LogInfo($"#RECALC MinExpPickingZone: {context.MinExpirationDatePickingZone:yyyy-MM-dd}");
                }
            }
            
            // Recalcular para zonas No-Picking
            if (context.StockListNonPickingZones != null)
            {
                var validNonPicking = context.StockListNonPickingZones.Where(s => s.Cantidad > 0).ToList();
                if (validNonPicking.Count > 0)
                {
                    context.MinExpirationDateNonPickingZones = validNonPicking.Min(s => s.Fecha_vence);
                    _logger.LogInfo($"#RECALC MinExpNonPickingZones: {context.MinExpirationDateNonPickingZones:yyyy-MM-dd}");
                    
                    // Recalcular también fechas Clavaud
                    var completePallets = validNonPicking.Where(s => s.Pallet_Completo && !s.UbicacionPicking && s.UbicacionNivel > 0).ToList();
                    if (completePallets.Count > 0)
                    {
                        context.MinExpirationCompletePalletsClavaud = completePallets.Min(s => s.Fecha_vence);
                    }
                    
                    var incompletePallets = validNonPicking.Where(s => !s.Pallet_Completo && !s.UbicacionPicking && s.UbicacionNivel > 0).ToList();
                    if (incompletePallets.Count > 0)
                    {
                        context.MinExpirationIncompletePalletsClavaud = incompletePallets.Min(s => s.Fecha_vence);
                    }
                }
            }
            
            // Recalcular fecha global
            if (context.WorkingStockList != null && context.WorkingStockList.Count > 0)
            {
                var validStock = context.WorkingStockList.Where(s => s.Cantidad > 0).ToList();
                if (validStock.Count > 0)
                {
                    context.GlobalMinimumExpirationDate = validStock.Min(s => s.Fecha_vence);
                }
            }
        }
    }
}
