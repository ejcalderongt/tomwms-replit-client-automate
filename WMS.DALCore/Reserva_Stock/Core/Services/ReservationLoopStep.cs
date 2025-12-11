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

                // PASO 4: Actualizar contexto
                if (result.Success && result.ReservedQuantity > 0)
                {
                    context.PendingQuantity -= result.ReservedQuantity;
                    context.PendingQuantity = Math.Round(context.PendingQuantity, 6);

                    if (result.Reservations != null && result.Reservations.Count > 0)
                    {
                        context.CreatedReservations.AddRange(result.Reservations);
                        
                        _logger.LogCheckpoint(
                            $"#MI3_RESERVATIONS_ADDED | " +
                            $"Count: {result.Reservations.Count} | " +
                            $"TotalInContext: {context.CreatedReservations.Count}");
                    }
                    else
                    {
                        _logger.LogCheckpoint(
                            $"#WARNING_NO_RESERVATIONS | " +
                            $"Success=true pero Reservations es null/empty");
                    }

                    _logger.LogCheckpoint(
                        $"#MI3_PROGRESS | Reservado: {result.ReservedQuantity:F6} | Casos: {result.CaseCode}");
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
            if (context.IsExplosionModeEnabled) return false;
            if (!context.Configuration.Explosion_Automatica) return false;
            if (context.Request.IdPresentacion == 0) return false;
            if (context.DefaultPresentation == null) return false;

            context.IsExplosionModeEnabled = true;
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
                newStock = newStock.Where(s => s.Cantidad > 0).ToList();

                context.WorkingStockList = newStock;
                _logger.LogInfo($"Re-query explosion: {newStock.Count} registros");
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
                newStock = newStock.Where(s => s.Cantidad > 0).ToList();

                context.WorkingStockList = newStock;
                _logger.LogInfo($"Re-query UMBas: {newStock.Count} registros");
            }
        }

        private void RecalculateMinimumDates(ReservationContext context)
        {
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
