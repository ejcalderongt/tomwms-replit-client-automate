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

            // 1) Validaciones base
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (context.Request is null) { context.SetError("Request nulo."); return; }
            if (context.Configuration is null) { context.SetError("Configuration nula."); return; }
            if (context.Connection is null) { context.SetError("Connection nula."); return; }
            if (context.Transaction is null) { context.SetError("Transaction nula."); return; }

            // 2) Días de vencimiento (ejemplo: 0 por defecto)
            var diasVencimiento = 0;
            
            var res = context.Request;          // clsBeStock_res
            var prod = context.Product;         // clsBeProducto? (puede venir nulo)

            // 4) Llamada segura a lStock
            var listPickingZone = clsLnStock.lStock(ref res,
                                                    ref prod,
                                                    diasVencimiento,
                                                    context.Configuration,
                                                    context.Connection,
                                                    context.Transaction,
                                                    pExcluirUbicacionPicking: false, //false, por defecto, analizar como mejorar después.
                                                    Conmutar_Umbas_A_Presentacion: false,
                                                    pTarea_Reabasto: context.TareaReabasto,   // si aplica
                                                    pEs_Devolucion: context.EsDevolucion      // si aplica
                                                );

            if (prod != null)
                // 5) Actualizar el Product del contexto con lo que pudo modificar lStock
                context.Product = prod;

            // 6) Asignar evitando CS8601 (si la propiedad es no-nullable)
            context.StockListPickingZone = listPickingZone ?? new List<clsBeStock>();

            // 7) (Opcional) si planeas usar res modificado:
            context.Request = res;

            // 8) Continuar flujo
            if (context.StockListPickingZone.Count > 0)
            {
                clsLnStock_res.Restar_Stock_Reservado(
                    context.StockListPickingZone,
                    context.Configuration,
                    context.Connection,
                    context.Transaction
                );

                // Filtra > 0 si corresponde
                context.StockListPickingZone = context.StockListPickingZone
                    .Where(s => s.Cantidad > 0).ToList();
            }

            _logger.LogInfo($"#Re-query PickingZone: {context.StockListPickingZone.Count} registros");
        }

    }
}