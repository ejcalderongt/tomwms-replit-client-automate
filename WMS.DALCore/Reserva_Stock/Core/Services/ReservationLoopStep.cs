using WMS.EntityCore.Producto;
using WMSWebAPI.Be;
using static clsBeI_nav_config_enc;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Paso 5: Loop de reserva iterativo con re-entry y fallbacks.
    /// Este es el componente más crítico que reproduce el control flow del MI3 original.
    /// Target: ~180 líneas
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

                // PASO 2: ✅ RECONSTRUIR cadena según punto de inicio
                IReservationHandler handlerChain = _factory.BuildHandlerChain(
                    startingPoint,
                    context,
                    _logger);

                // PASO 3: Ejecutar cadena (puede procesar parcialmente)
                var result = handlerChain.Handle(context);

                // PASO 4: Actualizar contexto
                if (result.Success && result.ReservedQuantity > 0)
                {
                    context.PendingQuantity -= result.ReservedQuantity;
                    context.PendingQuantity = Math.Round(context.PendingQuantity, 6);

                    if (result.Reservations != null && result.Reservations.Count > 0)
                    {
                        context.CreatedReservations.AddRange(result.Reservations);
                    }

                    _logger.LogCheckpoint(
                        $"#MI3_PROGRESS | Reservado: {result.ReservedQuantity:F6} | Casos: {result.CaseCode}");
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

                    // No hay más fallbacks
                    _logger.LogCheckpoint("#MI3_NO_MORE_OPTIONS");
                    break;
                }

                // PASO 6: Re-calcular fechas mínimas para próxima iteración
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

            // === LÓGICA CLAVAUD (pallets completos/incompletos) ===
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

            // === LÓGICA ZONA PICKING ===
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

            // === LÓGICA ALMACÉN GENERAL ===
            if (context.MinExpirationDateNonPickingZones > defaultDate &&
                context.StockListNonPickingZones != null &&
                context.StockListNonPickingZones.Count > 0)
            {
                return 4; // INICIAR_EN_4
            }

            return startingPoint; // 0 = última lista (default)
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
            // Crear request para explosión (sin presentación)
            clsBeStock_res explosionRequest = context.Request;

            // NOTA: Si clsBeStock_res no tiene IdPresentacion como propiedad settable,
            // el método lStock debe filtrar por IdPresentacion = 0 internamente
            // cuando el request lo requiera
            clsBeProducto? producto = context.Product;

            if (producto != null && context.Configuration != null && context.Transaction != null) {

                var newStock = clsLnStock.lStock(
                ref explosionRequest,
                ref producto,
                0, // diasVencimiento - valor por defecto
                context.Configuration,
                context.Connection,
                context.Transaction,
                pExcluirUbicacionPicking: false,
                Conmutar_Umbas_A_Presentacion: false,
                pTarea_Reabasto: context.TareaReabasto,
                context.EsDevolucion);

                if (newStock != null && newStock.Count > 0)
                {
                    clsLnStock_res.Restar_Stock_Reservado(newStock, context.Configuration,
                                                           context.Connection, context.Transaction);
                    newStock = newStock.Where(s => s.Cantidad > 0).ToList();

                    context.WorkingStockList = newStock;
                    _logger.LogInfo($"Re-query explosión: {newStock.Count} registros");
                }
            }
            
        }

        private void ReQueryStockForUMBas(ReservationContext context)
        {
            // Crear request para UMBas (sin presentación)
            clsBeStock_res umbasRequest = context.Request;

            // NOTA: Si clsBeStock_res no tiene IdPresentacion como propiedad settable,
            // el método lStock debe filtrar por IdPresentacion = 0 o NULL internamente
            // cuando el request lo requiera                        

            // Variable temporal para producto
            var productoTmp = context.Product;

            var newStock = clsLnStock.lStock(
                ref umbasRequest,
                ref productoTmp, // ← variable temporal ref
                DiasVencimiento: 0,
                pBeConfigEnc: context.Configuration,
                lConnection: context.Connection!,
                ltransaction: context.Transaction!,
                pExcluirUbicacionPicking: false,
                Conmutar_Umbas_A_Presentacion: false,
                pTarea_Reabasto: false,
                pEs_Devolucion: false
            );

            if (productoTmp != null) 
            // Actualizar el producto del contexto con la referencia modificada
            context.Product = productoTmp;

            if (newStock != null && newStock.Count > 0)
            {
                if (context.Connection != null && context.Transaction !=null) 
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