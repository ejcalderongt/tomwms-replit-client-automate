using WMS.EntityCore.Stock;

namespace WMS.StockReservation.Core.Services
{
    /// <summary>
    /// Paso 3: Consulta y preparación de stock disponible por zonas.
    /// Target: ~120 líneas
    /// </summary>
    public class StockQueryStep : IPipelineStep
    {
        private readonly IReservationLogger _logger;

        public StockQueryStep(IReservationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(ReservationContext context)
        {
            _logger.LogCheckpoint("#MI3_STOCK_QUERY_START");

            // Días de vencimiento: usar valor por defecto si no existe en Configuration
            int diasVencimiento = 0; // El método lStock usará este valor para filtrar por fecha de vencimiento

            // Variable temporal para ref (las propiedades no se pueden pasar con ref)
            var tempProduct = context.Product;

            // Consultar stock en zona Picking
            context.StockListPickingZone = clsLnStock.lStock(
                context.Request,
                ref tempProduct,                              // ✅ Usar variable local con ref
                diasVencimiento,
                context.Configuration,
                context.Connection,
                context.Transaction,
                pExcluirUbicacionPicking: false,             // ✅ false = NO excluir picking (solo picking)
                Conmutar_Umbas_A_Presentacion: false,        // ✅ Nombre correcto
                pTarea_Reabasto: context.TareaReabasto > 0,  // ✅ Convertir int a bool
                pEs_Devolucion: context.EsDevolucion);

            context.Product = tempProduct;  // ✅ Actualizar propiedad si fue modificada

            // Restar stock ya reservado de la zona Picking
            if (context.StockListPickingZone != null && context.StockListPickingZone.Count > 0)
            {
                clsLnStock_res.Restar_Stock_Reservado(
                    context.StockListPickingZone,
                    context.Configuration,
                    context.Connection,
                    context.Transaction);

                context.StockListPickingZone = context.StockListPickingZone
                    .Where(s => s.Cantidad > 0)
                    .ToList();
            }

            tempProduct = context.Product;  // Copiar a variable local

            // Consultar stock en zonas NO Picking
            context.StockListNonPickingZones = clsLnStock.lStock(
                                                 context.Request,
                                                 ref tempProduct,                          // ✅ Agregar ref
                                                 diasVencimiento,
                                                 context.Configuration,
                                                 context.Connection,
                                                 context.Transaction,
                                                 pExcluirUbicacionPicking: true,              // ✅ true = excluir picking (solo no-picking)
                                                 Conmutar_Umbas_A_Presentacion: false,        // ✅ Nombre correcto
                                                 pTarea_Reabasto: context.TareaReabasto > 0,  // ✅ Convertir int a bool
                                                 pEs_Devolucion: context.EsDevolucion);

            context.Product = tempProduct;  // Actualizar propiedad

            // Restar stock ya reservado de zonas no-Picking
            if (context.StockListNonPickingZones != null && context.StockListNonPickingZones.Count > 0)
            {
                clsLnStock_res.Restar_Stock_Reservado(
                    context.StockListNonPickingZones,
                    context.Configuration,
                    context.Connection,
                    context.Transaction);

                context.StockListNonPickingZones = context.StockListNonPickingZones
                    .Where(s => s.Cantidad > 0)
                    .ToList();
            }

            // Unificar lista de trabajo inicial (Picking + NonPicking)
            context.WorkingStockList = new List<clsBeStock>();
            
            if (context.StockListPickingZone != null)
            {
                context.WorkingStockList.AddRange(context.StockListPickingZone);
            }
            
            if (context.StockListNonPickingZones != null)
            {
                context.WorkingStockList.AddRange(context.StockListNonPickingZones);
            }

            _logger.LogCheckpoint(
                $"#MI3_STOCK_QUERY_OK - Picking: {context.StockListPickingZone?.Count ?? 0}, " +
                $"NonPicking: {context.StockListNonPickingZones?.Count ?? 0}, " +
                $"Total: {context.WorkingStockList?.Count ?? 0}");
        }
    }
}
