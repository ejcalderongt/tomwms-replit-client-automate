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
            context.Bodega = clsLnBodega.Get_Single_By_IdBodega(
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

            _logger.LogCheckpoint(
                $"#MI3_ENTITY_LOADING_OK - Producto: {context.Product.Codigo}, " +
                $"Bodega: {context.Bodega.Codigo}, " +
                $"Presentación defecto: {(context.DefaultPresentation != null ? "Sí" : "No")}");
        }
    }
}
