using WMS.EntityCore.Producto;
using WMS.StockReservation.Core.Domain;
using WMS.StockReservation.Core.Interfaces;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Paso 2: Carga de entidades (Bodega, Producto, Presentacion por defecto).
    /// Target: ~80 lineas
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
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _logger?.LogCheckpoint("#MI3_ENTITY_LOADING_START");

            if (context.Request == null)
            {
                context.SetError("Request null en ReservationContext");
                return;
            }

            if (context.Connection == null)
            {
                context.SetError("Connection null en ReservationContext");
                return;
            }

            if (context.Connection.State != System.Data.ConnectionState.Open)
            {
                context.SetError("Connection debe estar abierta (State=Open) para cargar entidades");
                return;
            }

            if (context.Request.IdBodega <= 0)
            {
                context.SetError($"IdBodega invalido: {context.Request.IdBodega}");
                return;
            }

            if (context.ProductId <= 0)
            {
                context.SetError($"ProductId invalido: {context.ProductId}");
                return;
            }

            context.CachedPresentations ??= new List<clsBeProducto_presentacion>();

            context.Bodega = clsLnBodega.GetSingle_By_Idbodega(
                context.Request.IdBodega,
                context.Connection,
                context.Transaction);

            if (context.Bodega == null)
            {
                context.SetError($"Bodega {context.Request.IdBodega} no encontrada");
                return;
            }

            context.Product = clsLnProducto.Get_Single_By_IdProducto(
                context.ProductId,
                context.Connection,
                context.Transaction);

            if (context.Product == null)
            {
                context.SetError($"Producto {context.ProductId} no encontrado");
                return;
            }

            context.DefaultPresentation = clsLnProducto_presentacion
                .Get_Presentacion_Defecto_By_IdProducto(
                    context.ProductId,
                    context.Connection,
                    context.Transaction);

            if (context.DefaultPresentation != null)
            {
                context.CachedPresentations.Add(context.DefaultPresentation);
            }

            try
            {
                ConvertQuantityToUnits(context);
            }
            catch (Exception ex)
            {
                context.SetError($"Error convirtiendo cantidad a unidades: {ex.Message}");
                return;
            }

            var productCode = context.Product?.codigo ?? "(sin codigo)";
            var bodegaCode = context.Bodega?.Codigo ?? "(sin codigo)";
            var hasDefaultPresentation = context.DefaultPresentation != null ? "Si" : "No";

            _logger?.LogCheckpoint(
                $"#MI3_ENTITY_LOADING_OK - Producto: {productCode}, " +
                $"Bodega: {bodegaCode}, " +
                $"Presentacion defecto: {hasDefaultPresentation}");
        }

        private void ConvertQuantityToUnits(ReservationContext context)
        {
            context.OriginalRequestedQuantity = context.Request.Cantidad;
            
            // Si NO hay presentacion (IdPresentacion <= 0), cantidad ya esta en unidades
            if (context.Request.IdPresentacion <= 0 || context.DefaultPresentation == null)
            {
                context.WasQuantityInPresentation = false;
                context.ConversionFactor = 1;
                _logger?.LogInfo($"#CONVERSION_SKIP | IdPresentacion=0, cantidad ya en unidades: {context.Request.Cantidad:F6}");
                return;
            }

            // Si HAY presentacion (IdPresentacion > 0), SIEMPRE convertir a unidades
            // Ejemplo: 96 cajas x 24 unidades/caja = 2304 unidades
            double factor = context.DefaultPresentation.Factor;
            if (factor <= 0) factor = 1;
            
            context.ConversionFactor = factor;
            
            double quantityInUnits = Math.Round(context.Request.Cantidad * factor, 6);
            
            context.PendingQuantity = quantityInUnits;
            context.Request.Cantidad = quantityInUnits;
            context.WasQuantityInPresentation = true;
            
            _logger?.LogInfo($"#CONVERSION_APPLIED | {context.OriginalRequestedQuantity:F6} presentaciones x Factor: {factor:F0} = {quantityInUnits:F6} unidades");
        }
    }
}
