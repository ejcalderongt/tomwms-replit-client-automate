using WMS.EntityCore.Producto;

namespace WMS.StockReservation.Core.Services
{
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

            // --- Cargar Bodega (validar antes de asignar a la propiedad) ---
            var bodega = clsLnBodega.GetSingle_By_Idbodega(context.Request.IdBodega,
                                                           context.Connection,
                                                           context.Transaction);

            if (bodega is null)
            {
                context.SetError($"Bodega {context.Request.IdBodega} no encontrada");
                return;
            }

            context.Bodega = bodega;
        
            var product = clsLnProducto.Get_Single_By_IdProductoBodega(context.ProductId,
                                                                       context.Connection,
                                                                       context.Transaction);

            if (product is null)
            {
                context.SetError($"Producto {context.ProductId} no encontrado");
                return;
            }

            context.Product = product; // <- seguro

            // --- Cargar Presentación por defecto (puede ser null y está bien) ---
            clsBeProducto_presentacion? defaultPresentation = clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(context.ProductId,
                                                                                                                                context.Connection,
                                                                                                                                context.Transaction);

            if (defaultPresentation != null) 
            context.DefaultPresentation = defaultPresentation; // esta puede ser nullable en el contexto

            // Manejo seguro de la presentación por defecto
            var safePresentation = defaultPresentation ?? new clsBeProducto_presentacion();
            if (context.CachedPresentations != null && !context.CachedPresentations.Contains(safePresentation))
            {
                context.CachedPresentations.Add(safePresentation);
            }

            // Log seguro
            string productCode = context.Product?.codigo ?? "N/A";
            string warehouseCode = context.Bodega?.Codigo ?? "N/A";
            bool hasDefaultPresentation = context.DefaultPresentation != null;

            _logger.LogCheckpoint(
                $"#MI3_ENTITY_LOADING_OK - Producto: {productCode}, " +
                $"Bodega: {warehouseCode}, " +
                $"Presentación defecto: {(hasDefaultPresentation ? "Sí" : "No")}");
        }

    }
}