using WMS.EntityCore.Stock;
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

        // ===== CONFIGURACION CLAVAUD DINAMICO =====
        // TODO: Parametrizar estos valores en tabla de configuracion (i_nav_config_enc o bodega)
        // PICKING_BUFFER_FACTOR: Factor de seguridad para conservar picking
        // Ejemplo: 1.2 significa que picking debe tener 20% mas de lo solicitado para conservarlo
        private const double PICKING_BUFFER_FACTOR = 1.0;  // 1.0 = sin buffer, 1.2 = 20% buffer
        
        // PICKING_MINIMUM_RESERVE_FACTOR: Porcentaje minimo de picking a conservar
        // Cuando picking no tiene suficiente, tomar de almacenaje para mantener este % en picking
        // Ejemplo: 0.3 significa conservar al menos 30% del stock de picking
        private const double PICKING_MINIMUM_RESERVE_FACTOR = 0.0;  // 0.0 = desactivado, 0.3 = 30%

        public ReservationLoopStep(IServiceFactory factory, IReservationLogger logger)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(ReservationContext context)
        {
            _logger.LogCheckpoint("#MI3_LOOP_START");

            // ===== LOTES ESPECÍFICOS (prioridad absoluta para transferencias) =====
            if (context.HasSpecificLots)
            {
                ProcessSpecificLots(context);
                
                if (context.PendingQuantity <= 0.000001)
                {
                    _logger.LogCheckpoint("#MI3_LOOP_END_SPECIFIC_LOTS | Reserva completa con lotes específicos");
                    return;
                }
                
                _logger.LogInfo($"#SPECIFIC_LOTS_PARTIAL | Pendiente: {context.PendingQuantity:F2}u, continuando con FEFO");
            }

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

            // === LOGICA FEFO: Comparar fechas Picking vs NonPicking (Almacenamiento) ===
            bool hasPickingStock = context.StockListPickingZone != null && 
                                   context.StockListPickingZone.Count > 0 &&
                                   context.MinExpirationDatePickingZone > defaultDate;
            
            bool hasNonPickingStock = context.StockListNonPickingZones != null && 
                                       context.StockListNonPickingZones.Count > 0 &&
                                       context.MinExpirationDateNonPickingZones > defaultDate;

            // Obtener configuración de la bodega
            bool reservarPrimeroAlmacenaje = context.Bodega?.Reservar_primero_almacenaje ?? false;

            // CASO: Hay stock en ambas zonas - aplicar FEFO
            if (hasPickingStock && hasNonPickingStock)
            {
                bool fechasIguales = context.MinExpirationDatePickingZone.Date == context.MinExpirationDateNonPickingZones.Date;
                
                // FEFO: Si Almacenamiento vence ANTES que Picking → NonPicking primero
                if (context.MinExpirationDateNonPickingZones < context.MinExpirationDatePickingZone)
                {
                    _logger.LogInfo($"#FEFO_DECISION: NonPicking ({context.MinExpirationDateNonPickingZones:yyyy-MM-dd}) vence ANTES que Picking ({context.MinExpirationDatePickingZone:yyyy-MM-dd}) → INICIAR_EN_4");
                    return 4; // INICIAR_EN_4 (NonPicking/Almacenamiento primero)
                }
                
                // Si Picking vence ANTES que Almacenamiento → Picking primero
                if (context.MinExpirationDatePickingZone < context.MinExpirationDateNonPickingZones)
                {
                    _logger.LogInfo($"#FEFO_DECISION: Picking ({context.MinExpirationDatePickingZone:yyyy-MM-dd}) vence ANTES que NonPicking ({context.MinExpirationDateNonPickingZones:yyyy-MM-dd}) → INICIAR_EN_3");
                    return 3; // INICIAR_EN_3 (Picking primero)
                }
                
                // === FECHAS IGUALES: Aplicar lógica Clavaud dinámica ===
                if (fechasIguales)
                {
                    // CLAVAUD DINAMICO: Evaluar si conservar picking basado en disponibilidad
                    if (context.Configuration.Conservar_Zona_Picking_Clavaud)
                    {
                        int clavaudDecision = EvaluateClavaudDynamic(context);
                        if (clavaudDecision > 0)
                        {
                            return clavaudDecision;
                        }
                    }
                    
                    // Fallback a configuración de bodega
                    if (reservarPrimeroAlmacenaje)
                    {
                        _logger.LogInfo($"#FEFO_DECISION: Fechas iguales ({context.MinExpirationDatePickingZone:yyyy-MM-dd}), Bodega configurada para Almacenaje primero → INICIAR_EN_4");
                        return 4; // INICIAR_EN_4 (NonPicking/Almacenamiento primero)
                    }
                    else
                    {
                        _logger.LogInfo($"#FEFO_DECISION: Fechas iguales ({context.MinExpirationDatePickingZone:yyyy-MM-dd}), Priorizar Picking → INICIAR_EN_3");
                        return 3; // INICIAR_EN_3 (Picking primero)
                    }
                }
                
                // Default: Picking primero
                return 3;
            }
            
            // CASO: Solo hay stock en Picking
            if (hasPickingStock && !hasNonPickingStock)
            {
                return 3; // INICIAR_EN_3
            }
            
            // CASO: Solo hay stock en NonPicking (Almacenamiento)
            if (!hasPickingStock && hasNonPickingStock)
            {
                return 4; // INICIAR_EN_4
            }

            return startingPoint; // 0 = default (cadena completa)
        }

        /// <summary>
        /// Lógica Clavaud Dinámica: Decide si conservar picking basado en disponibilidad real.
        /// 
        /// REGLA PRINCIPAL:
        /// Si picking tiene suficiente stock para cubrir el pedido (con buffer), 
        /// Y hay stock disponible en almacenaje con LA MISMA FECHA,
        /// entonces usar almacenaje primero para no desabastecer picking.
        /// 
        /// ESTRATEGIA ADICIONAL (Reserva Proporcional):
        /// Si picking no tiene suficiente pero tiene stock, calcular cuánto tomar
        /// de almacenaje para mantener un mínimo en picking.
        /// 
        /// NOTA: Esta lógica SOLO se ejecuta cuando fechasIguales == true (verificado en DetermineStartingPoint)
        /// </summary>
        private int EvaluateClavaudDynamic(ReservationContext context)
        {
            // IMPORTANTE: Esta función se llama SOLO cuando fechas son iguales entre zonas
            // Usamos la fecha común (MinExpirationDatePickingZone que es igual a MinExpirationDateNonPickingZones)
            DateTime fechaComun = context.MinExpirationDatePickingZone.Date;
            
            // Calcular cantidad disponible en picking CON LA FECHA COMUN
            double cantidadPickingMismaFecha = context.StockListPickingZone?
                .Where(s => s.Fecha_vence.Date == fechaComun && s.Cantidad > 0)
                .Sum(s => s.Cantidad) ?? 0;

            // Calcular cantidad disponible en almacenaje CON LA MISMA FECHA COMUN
            double cantidadAlmacenajeMismaFecha = context.StockListNonPickingZones?
                .Where(s => s.Fecha_vence.Date == fechaComun && s.Cantidad > 0)
                .Sum(s => s.Cantidad) ?? 0;

            double cantidadSolicitada = context.PendingQuantity;
            double cantidadRequeridaConBuffer = cantidadSolicitada * PICKING_BUFFER_FACTOR;

            _logger.LogInfo($"#CLAVAUD_EVAL | Fecha común: {fechaComun:yyyy-MM-dd} | Solicitado: {cantidadSolicitada:F2} | " +
                           $"Picking disponible: {cantidadPickingMismaFecha:F2} | " +
                           $"Almacenaje disponible: {cantidadAlmacenajeMismaFecha:F2} | " +
                           $"Buffer factor: {PICKING_BUFFER_FACTOR}");

            // Verificar que hay stock en almacenaje con la misma fecha
            if (cantidadAlmacenajeMismaFecha <= 0)
            {
                _logger.LogInfo($"#CLAVAUD_DECISION: No hay stock en almacenaje con fecha {fechaComun:yyyy-MM-dd} → Delegar a lógica estándar");
                return 0; // No hay almacenaje disponible, delegar
            }

            // REGLA CLAVAUD: Picking tiene suficiente (con buffer) → Conservar picking, usar almacenaje
            if (cantidadPickingMismaFecha >= cantidadRequeridaConBuffer)
            {
                _logger.LogInfo($"#CLAVAUD_DECISION: Picking tiene suficiente ({cantidadPickingMismaFecha:F2} >= {cantidadRequeridaConBuffer:F2}) " +
                               $"→ Usar ALMACENAJE primero para conservar picking → INICIAR_EN_4");
                return 4; // INICIAR_EN_4 (NonPicking/Almacenaje primero - respeta FEFO)
            }

            // ESTRATEGIA PROPORCIONAL: Si está activada y hay stock en ambas zonas
            if (PICKING_MINIMUM_RESERVE_FACTOR > 0 && cantidadPickingMismaFecha > 0)
            {
                double minimoAConservar = cantidadPickingMismaFecha * PICKING_MINIMUM_RESERVE_FACTOR;
                double disponibleParaPedido = cantidadPickingMismaFecha - minimoAConservar;
                
                if (disponibleParaPedido < cantidadSolicitada)
                {
                    // Hay que tomar de almacenaje para no agotar picking completamente
                    _logger.LogInfo($"#CLAVAUD_PROPORTIONAL: Conservar {minimoAConservar:F2}u en picking ({PICKING_MINIMUM_RESERVE_FACTOR*100}%) " +
                                   $"→ Iniciar con almacenaje → INICIAR_EN_4");
                    return 4; // INICIAR_EN_4
                }
            }

            // Ninguna condición de conservación aplica → usar lógica normal
            _logger.LogInfo($"#CLAVAUD_DECISION: Picking insuficiente para conservar ({cantidadPickingMismaFecha:F2} < {cantidadRequeridaConBuffer:F2}) → Delegar a lógica estándar");
            return 0; // Delegar a la lógica normal (picking primero si fechas iguales)
        }

        private bool TryEnableExplosionFallback(ReservationContext context)
        {
            // EXPLOSION: Romper cajas en unidades
            // Aplica cuando:
            //   1. Piden UNIDADES (IdPresentacion == 0) y solo hay stock en cajas
            //   2. Piden CANTIDAD DECIMAL de presentación (ej: 0.5 cajas) y no hay unidades sueltas
            //      → Ya fue convertida a unidades, necesita explosionar para obtenerlas
            
            if (context.IsExplosionModeEnabled) return false;
            if (!context.Configuration.Explosion_Automatica) return false;
            if (context.DefaultPresentation == null) return false;
            
            // Caso 1: Piden unidades directamente (IdPresentacion == 0)
            bool directUnitsRequest = (context.Request.IdPresentacion == 0);
            
            // Caso 2: Pidieron cantidad en presentación pero fue convertida a unidades
            // Ejemplo: 0.5 cajas × 24 = 12 unidades → puede necesitar explosionar si no hay sueltas
            bool convertedFromPresentation = context.WasQuantityInPresentation;
            
            // Si no es ninguno de los casos, no explosionar
            if (!directUnitsRequest && !convertedFromPresentation) return false;
            
            // VALIDACIÓN ADICIONAL: Solo explosionar si NO hay suficientes unidades sueltas
            // Esto evita explosionar cuando hay unidades disponibles
            double totalUnidadesDisponibles = 0;
            if (context.WorkingStockList != null)
            {
                totalUnidadesDisponibles = context.WorkingStockList
                    .Where(s => s.Cantidad > 0)
                    .Sum(s => s.Cantidad);
            }
            
            // Si hay suficientes unidades disponibles, no activar explosión todavía
            // La explosión solo debería activarse cuando realmente no hay suficiente stock
            if (totalUnidadesDisponibles >= context.PendingQuantity)
            {
                _logger.LogInfo($"#EXPLOSION_SKIP - Hay {totalUnidadesDisponibles:F2}u disponibles >= {context.PendingQuantity:F2}u pendientes");
                return false;
            }

            context.IsExplosionModeEnabled = true;
            
            if (convertedFromPresentation)
            {
                _logger.LogInfo($"#EXPLOSION_ENABLED - Cantidad convertida de presentación " +
                               $"({context.OriginalRequestedQuantity:F6} × {context.ConversionFactor:F0} = {context.PendingQuantity:F6}u), " +
                               $"disponible: {totalUnidadesDisponibles:F2}u, buscando cajas para explosionar");
            }
            else
            {
                _logger.LogInfo($"#EXPLOSION_ENABLED - Pedido en unidades, disponible: {totalUnidadesDisponibles:F2}u, buscando cajas para explosionar");
            }
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

        // ===== LOTES ESPECÍFICOS (para transferencias) =====
        /// <summary>
        /// Procesa lotes específicos definidos en TrasladoDet.Lotes_Detalle.
        /// Reserva SOLO los lotes indicados por Batch_No, ignorando FEFO.
        /// 
        /// IMPORTANTE: 
        /// - Quantity_Base en Lotes_Detalle debe estar en UNIDADES BASE (mismas que Request.Cantidad)
        /// - Se ejecuta ANTES del loop FEFO con prioridad absoluta
        /// - Si un lote no se encuentra, se registra warning pero continúa
        /// - Si quedan unidades pendientes, el loop FEFO las completa
        /// </summary>
        private void ProcessSpecificLots(ReservationContext context)
        {
            context.IsSpecificLotModeEnabled = true;
            _logger.LogCheckpoint($"#SPECIFIC_LOTS_START | Procesando {context.SpecificLots.Count} lotes específicos");

            foreach (var loteDet in context.SpecificLots)
            {
                if (context.PendingQuantity <= 0.000001) break;

                string batchNo = loteDet.Batch_No?.Trim() ?? "";
                double quantityRequired = loteDet.Quantity_Base;

                if (string.IsNullOrEmpty(batchNo) || quantityRequired <= 0)
                {
                    _logger.LogInfo($"#SPECIFIC_LOT_SKIP | Lote vacío o cantidad 0");
                    continue;
                }

                _logger.LogInfo($"#SPECIFIC_LOT_SEARCH | Lote: {batchNo}, Cantidad: {quantityRequired:F2}u");

                // Buscar stock con este lote específico
                var matchingStock = FindStockByBatchNo(context, batchNo);

                if (matchingStock == null || matchingStock.Count == 0)
                {
                    _logger.LogWarning($"#SPECIFIC_LOT_NOT_FOUND | Lote {batchNo} no encontrado en stock disponible");
                    continue;
                }

                // Reservar del stock encontrado
                double reservedFromLot = ReserveFromSpecificLot(context, matchingStock, quantityRequired, batchNo);

                if (reservedFromLot < quantityRequired - 0.000001)
                {
                    _logger.LogWarning($"#SPECIFIC_LOT_INSUFFICIENT | Lote {batchNo}: " +
                                      $"Requerido: {quantityRequired:F2}u, Reservado: {reservedFromLot:F2}u");
                }
            }

            context.IsSpecificLotModeEnabled = false;
            _logger.LogCheckpoint($"#SPECIFIC_LOTS_END | Pendiente después de lotes específicos: {context.PendingQuantity:F2}u");
        }

        /// <summary>
        /// Busca stock por Batch_No aplicando filtros básicos de consistencia.
        /// NOTA: Para transferencias, los filtros son más permisivos ya que el origen
        /// especifica exactamente qué lotes mover. Se mantienen filtros de:
        /// - Batch_No exacto
        /// - Disponibilidad (Cantidad - Reservado > 0)
        /// - Mismo IdProductoBodega (seguridad)
        /// </summary>
        private List<clsBeStock> FindStockByBatchNo(ReservationContext context, string batchNo)
        {
            var allStock = new List<clsBeStock>();
            if (context.StockListPickingZone != null) allStock.AddRange(context.StockListPickingZone);
            if (context.StockListNonPickingZones != null) allStock.AddRange(context.StockListNonPickingZones);

            int idProductoBodega = context.Request.IdProductoBodega;

            return allStock
                .Where(s => (s.Lote?.Trim() ?? "") == batchNo 
                         && (s.Cantidad - s.Cantidad_Reservada) > 0
                         && s.IdProductoBodega == idProductoBodega)
                .OrderBy(s => s.Fecha_vence)
                .ThenByDescending(s => s.Cantidad - s.Cantidad_Reservada)
                .ToList();
        }

        private double ReserveFromSpecificLot(ReservationContext context, List<clsBeStock> stockList, 
                                               double quantityRequired, string batchNo)
        {
            double totalReserved = 0;

            foreach (var stock in stockList)
            {
                if (quantityRequired <= 0.000001 || context.PendingQuantity <= 0.000001) break;

                double available = stock.Cantidad - stock.Cantidad_Reservada;
                if (available <= 0) continue;

                double toReserve = Math.Min(Math.Min(available, quantityRequired), context.PendingQuantity);

                // Crear reserva
                var reservation = new clsBeStock_res
                {
                    IdStock = stock.IdStock,
                    IdBodega = context.Request.IdBodega,
                    IdProductoBodega = context.Request.IdProductoBodega,
                    IdUbicacionAbastecerCon = stock.IdUbicacion,
                    Cantidad = toReserve,
                    IdPedidoDet = context.Request.IdPedidoDet,
                    IdPresentacion = context.Request.IdPresentacion,
                    IdUnidadMedida = context.Request.IdUnidadMedida,
                    IdProductoEstado = context.Request.IdProductoEstado,
                    IdPropietarioBodega = context.Request.IdPropietarioBodega,
                    IdTransaccion = context.IdPedidoEnc,
                    IdUbicacion = stock.IdUbicacion,
                    Lote = batchNo,
                    Fecha_vence = stock.Fecha_vence
                };

                context.CreatedReservations.Add(reservation);

                // Actualizar contadores
                stock.Cantidad_Reservada += toReserve;
                context.PendingQuantity -= toReserve;
                quantityRequired -= toReserve;
                totalReserved += toReserve;

                _logger.LogInfo($"#SPECIFIC_LOT_RESERVED | Lote: {batchNo}, IdStock: {stock.IdStock}, " +
                               $"Ubicación: {stock.IdUbicacion}, Cantidad: {toReserve:F2}u");
            }

            return totalReserved;
        }
    }
}
