using System;
using WMS.StockReservation.Core.Domain;
using WMS.StockReservation.Core.Interfaces;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Paso 2: Carga de entidades (Bodega, Producto, Presentación por defecto).
    /// Target: ~80 líneas
    /// </summary>
    public class EntityLoadingStep : IPipelineStep
    {
        private readonly IReservationLogger _logger;

        public EntityLoadingStep(IReservationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(ReservationContext context)
        {
            _logger.LogCheckpoint("#MI3_ENTITY_LOADING_START");

            // Cargar Bodega
            context.Bodega = clsLnBodega.GetSingle_By_Idbodega(
                context.Request.IdBodega,
                context.Connection,
                context.Transaction);

            if (context.Bodega == null)
            {
                context.SetError($"Bodega {context.Request.IdBodega} no encontrada");
                return;
            }

            // Cargar Producto
            context.Product = clsLnProducto.Get_Single_By_IdProducto(
                context.ProductId,
                context.Connection,
                context.Transaction);

            if (context.Product == null)
            {
                context.SetError($"Producto {context.ProductId} no encontrado");
                return;
            }

            // Cargar Presentación por defecto
            context.DefaultPresentation = clsLnProducto_presentacion
                .Get_Presentacion_Defecto_By_IdProducto(
                    context.ProductId,
                    context.Connection,
                    context.Transaction);

            if (context.DefaultPresentation != null)
            {
                // Agregar al caché de presentaciones
                context.CachedPresentations.Add(context.DefaultPresentation);
            }

            // CONVERSIÓN DE CANTIDAD: Presentación → Unidades
            // Si el request tiene IdPresentacion > 0, la cantidad viene en presentación
            // y debe convertirse a unidades multiplicando por el factor
            ConvertQuantityToUnits(context);

            _logger.LogCheckpoint(
                $"#MI3_ENTITY_LOADING_OK - Producto: {context.Product.codigo}, " +
                $"Bodega: {context.Bodega.Codigo}, " +
                $"Presentación defecto: {(context.DefaultPresentation != null ? "Sí" : "No")}");
        }

        /// <summary>
        /// Convierte la cantidad solicitada de presentación a unidades si aplica.
        /// 
        /// ESCENARIOS:
        /// 1. Request.IdPresentacion > 0 y Cantidad decimal (ej: 0.5 cajas) → Convertir a unidades
        /// 2. Request.IdPresentacion > 0 y Cantidad entera (ej: 2 cajas) → Convertir a unidades
        /// 3. Request.IdPresentacion = 0 (ya en unidades) → No convertir
        /// 
        /// EJEMPLO:
        /// - Cantidad: 0.5, Factor: 24 → PendingQuantity = 12 unidades
        /// - Cantidad: 1.25, Factor: 24 → PendingQuantity = 30 unidades (1 caja + 6 unidades)
        /// 
        /// NOTA: Actualiza tanto PendingQuantity como Request.Cantidad para mantener
        /// consistencia con invariantes y logging.
        /// </summary>
        private void ConvertQuantityToUnits(ReservationContext context)
        {
            // Guardar cantidad original ANTES de cualquier modificación
            context.OriginalRequestedQuantity = context.Request.Cantidad;
            
            // Si no hay presentación o IdPresentacion = 0, la cantidad ya está en unidades
            if (context.Request.IdPresentacion <= 0 || context.DefaultPresentation == null)
            {
                context.WasQuantityInPresentation = false;
                context.ConversionFactor = 1;
                _logger.LogInfo($"#CONVERSION_SKIP | Cantidad ya en unidades: {context.Request.Cantidad:F6}");
                return;
            }

            // Obtener factor de conversión
            double factor = context.DefaultPresentation.Factor;
            if (factor <= 0) factor = 1;
            
            context.ConversionFactor = factor;
            
            // Heurística: Si la cantidad solicitada es >= factor, probablemente ya está en unidades
            // (Ej: piden 100 unidades con factor 24 → NO convertir)
            // Si la cantidad es < factor O es decimal → convertir
            bool needsConversion = (context.Request.Cantidad < factor) || 
                                   (context.Request.Cantidad % 1 != 0);
            
            if (!needsConversion)
            {
                context.WasQuantityInPresentation = false;
                _logger.LogInfo($"#CONVERSION_SKIP | Cantidad {context.Request.Cantidad:F6} >= Factor {factor}, asumiendo ya en unidades");
                return;
            }
            
            // Convertir de presentación a unidades
            double quantityInUnits = Math.Round(context.Request.Cantidad * factor, 6);
            
            // IMPORTANTE: Actualizar AMBOS para mantener consistencia con invariantes
            context.PendingQuantity = quantityInUnits;
            context.Request.Cantidad = quantityInUnits;  // Sincronizar para invariantes y logging
            context.WasQuantityInPresentation = true;
            
            _logger.LogInfo($"#CONVERSION_APPLIED | Original: {context.OriginalRequestedQuantity:F6} presentaciones × Factor: {factor:F0} = {quantityInUnits:F6} unidades");
        }
    }
}
