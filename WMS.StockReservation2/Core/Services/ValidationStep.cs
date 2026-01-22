using WMS.EntityCore.Producto;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Paso 1: Validación de parámetros y configuración inicial.
    /// Target: ~50 líneas
    /// </summary>
    public class ValidationStep : IPipelineStep
    {
        private readonly IReservationLogger _logger;

        public ValidationStep(IReservationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(ReservationContext context)
        {
            _logger.LogCheckpoint("#MI3_VALIDATION_START");

            // Validaciones de parámetros obligatorios
            if (context.Request == null)
                throw new ArgumentNullException(nameof(context.Request), "Request no puede ser null");

            if (context.Configuration == null)
                throw new ArgumentNullException(nameof(context.Configuration), "Configuration no puede ser null");

            if (context.Connection == null)
                throw new ArgumentNullException(nameof(context.Connection), "Connection no puede ser null");

            if (context.Request.Cantidad <= 0)
                throw new ArgumentException("Cantidad debe ser > 0", nameof(context.Request.Cantidad));

            // Resolver ProductId con fallback
            if (context.ProductId <= 0)
            {
                // Fallback 1: Desde request.IdProducto
                if (context.Request.IdProductoBodega > 0)
                {
                    context.ProductId = context.Request.IdProductoBodega;
                    _logger.LogInfo($"#MI3_PRODUCTID_FROM_REQUEST: {context.ProductId}");
                }
                // Fallback 2: Resolver desde IdProductoBodega
                else if (context.Request.IdProductoBodega > 0)
                {
                    var productoBodega = clsLnProducto.Get_Single_By_IdProductoBodega(
                        context.Request.IdProductoBodega,
                        context.Connection,
                        context.Transaction);
                    
                    if (productoBodega != null && productoBodega.IdProducto > 0)
                    {
                        context.ProductId = productoBodega.IdProducto;
                        _logger.LogInfo($"#MI3_PRODUCTID_FROM_PRODUCTOBODEGA: {context.ProductId}");
                    }
                    else
                    {
                        throw new ArgumentException($"No se pudo resolver ProductId desde IdProductoBodega: {context.Request.IdProductoBodega}");
                    }
                }
                else
                {
                    throw new ArgumentException("ProductId no especificado y no se puede resolver desde Request.IdProducto o Request.IdProductoBodega");
                }
            }

            // Inicializar valores en contexto
            context.PendingQuantity = context.Request.Cantidad;

            // Asegurar que las listas estén inicializadas
            if (context.CreatedReservations == null)
                context.CreatedReservations = new List<clsBeStock_res>();
            
            if (context.ProcessStateFlags == null)
                context.ProcessStateFlags = new List<int>();
            
            if (context.CachedLocations == null)
                context.CachedLocations = new List<clsBeBodega_ubicacion>();
            
            if (context.CachedPresentations == null)
                context.CachedPresentations = new List<clsBeProducto_presentacion>();

            _logger.LogCheckpoint($"#MI3_VALIDATION_OK - Cantidad Solicitada: {context.PendingQuantity:F6}");
        }
    }
}
