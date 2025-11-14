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

            // Resolver ProductId (necesario para EntityLoadingStep y handlers)
            if (context.ProductId <= 0)
            {
                // El request tiene IdProductoBodega, pero el pipeline necesita IdProducto
                // Traducir IdProductoBodega → IdProducto mediante consulta
                if (context.Request.IdProductoBodega > 0)
                {
                    var productoBodega = clsLnProducto.Get_Single_By_IdProductoBodega(
                        context.Request.IdProductoBodega,
                        context.Connection,
                        context.Transaction);

                    if (productoBodega != null && productoBodega.IdProducto > 0)
                    {
                        context.ProductId = productoBodega.IdProducto;
                        _logger.LogInfo($"#MI3_PRODUCTID_RESOLVED: IdProductoBodega={context.Request.IdProductoBodega} → IdProducto={context.ProductId}");
                    }
                    else
                    {
                        throw new ArgumentException($"No se pudo resolver IdProducto desde IdProductoBodega: {context.Request.IdProductoBodega}");
                    }
                }
                else
                {
                    throw new ArgumentException("IdProductoBodega no especificado en el request");
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
