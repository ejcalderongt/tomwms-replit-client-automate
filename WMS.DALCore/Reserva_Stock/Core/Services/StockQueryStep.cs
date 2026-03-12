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
            if (context == null) throw new ArgumentNullException(nameof(context));

            _logger.LogCheckpoint("#MI3_STOCK_QUERY_START");

            // Null-safety / precondiciones del pipeline
            if (context.Request == null)
            {
                context.SetError("Request null en ReservationContext");
                return;
            }

            if (context.Product == null)
            {
                context.SetError("Product null en ReservationContext");
                return;
            }

            if (context.Configuration == null)
            {
                context.SetError("Configuration null en ReservationContext");
                return;
            }

            if (context.Connection == null)
            {
                context.SetError("Connection null en ReservationContext");
                return;
            }

            if (context.Connection.State != ConnectionState.Open)
            {
                context.SetError("Connection debe estar abierta (State=Open) para consultar stock");
                return;
            }

            // El método lStock requiere int, convertimos desde double preservando el valor
            int diasVencimiento = (int)Math.Round(context.DiasVencimiento);
            if (diasVencimiento < 0) diasVencimiento = 0;

            // Variables temporales para ref (las propiedades no se pueden pasar con ref)
            var tempRequest = context.Request;
            var tempProduct = context.Product;

            // =========================
            // Consultar stock Picking
            // =========================
            context.StockListPickingZone = clsLnStock.lStock(ref tempRequest,
                                                            ref tempProduct,
                                                            diasVencimiento,
                                                            context.Configuration,
                                                            context.Connection,
                                                            context.Transaction,
                                                            pExcluirUbicacionPicking: false,
                                                            Conmutar_Umbas_A_Presentacion: false,
                                                            pTarea_Reabasto: context.TareaReabasto,
                                                            pEs_Devolucion: context.EsDevolucion,
                                                            pEsManufactura: context.EsManufactura);

            // Re-asignar (por si lStock modifica las referencias)
            context.Request = tempRequest;
            context.Product = tempProduct;

            // Restar stock ya reservado de la zona Picking
            if (context.StockListPickingZone != null && context.StockListPickingZone.Count > 0)
            {
                clsLnStock_res.Restar_Stock_Reservado(context.StockListPickingZone,
                                                        context.Configuration,
                                                        context.Connection,
                                                        context.Transaction);

                // IMPORTANTE: Filtrar SOLO ubicaciones de picking
                // La consulta con pExcluirUbicacionPicking=false trae TODO el stock,
                // debemos filtrar aquí para separar correctamente las zonas
                context.StockListPickingZone = context.StockListPickingZone
                    .Where(s => s != null && s.Cantidad > 0 && s.UbicacionPicking)
                    .ToList();
            }

            // =========================
            // Consultar stock NO Picking
            // =========================
            tempRequest = context.Request!;
            tempProduct = context.Product!;

            context.StockListNonPickingZones = clsLnStock.lStock(
                ref tempRequest,
                ref tempProduct,
                diasVencimiento,
                context.Configuration,
                context.Connection,
                context.Transaction,
                pExcluirUbicacionPicking: true,
                Conmutar_Umbas_A_Presentacion: false,
                pTarea_Reabasto: context.TareaReabasto,
                pEs_Devolucion: context.EsDevolucion,
                pEsManufactura: context.EsManufactura);

            context.Request = tempRequest;
            context.Product = tempProduct;

            // Restar stock ya reservado de zonas no-Picking
            if (context.StockListNonPickingZones != null && context.StockListNonPickingZones.Count > 0)
            {
                clsLnStock_res.Restar_Stock_Reservado(
                    context.StockListNonPickingZones,
                    context.Configuration,
                    context.Connection,
                    context.Transaction);

                // Filtrar SOLO ubicaciones NO picking (redundante pero seguro)
                // La consulta ya excluye picking con pExcluirUbicacionPicking=true
                context.StockListNonPickingZones = context.StockListNonPickingZones
                    .Where(s => s != null && s.Cantidad > 0 && !s.UbicacionPicking)
                    .ToList();
            }

            // =========================
            // Validar fecha_manufactura (post-query)
            // =========================
            if (context.EsManufactura)
            {
                FilterInvalidManufacturingDates(context);
            }

            // =========================
            // Unificar lista de trabajo
            // =========================
            context.WorkingStockList = new List<clsBeStock>();

            if (context.StockListPickingZone != null && context.StockListPickingZone.Count > 0)
                context.WorkingStockList.AddRange(context.StockListPickingZone.Where(s => s != null));

            if (context.StockListNonPickingZones != null && context.StockListNonPickingZones.Count > 0)
                context.WorkingStockList.AddRange(context.StockListNonPickingZones.Where(s => s != null));

            int pickingCount = context.StockListPickingZone?.Count ?? 0;
            int nonPickingCount = context.StockListNonPickingZones?.Count ?? 0;
            int totalCount = context.WorkingStockList.Count;

            _logger.LogCheckpoint(
                $"#MI3_STOCK_QUERY_OK - Picking: {pickingCount}, " +
                $"NonPicking: {nonPickingCount}, " +
                $"Total: {totalCount}");

            DetectStockFailures(context, pickingCount, nonPickingCount);
        }

        private void DetectStockFailures(ReservationContext context, int pickingCount, int nonPickingCount)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (pickingCount == 0 && nonPickingCount == 0)
            {
                if (context.EsManufactura)
                {
                    context.AddFailure(
                        Interfaces.ReservationFailureCode.MANUFACTURING_DATE_INVALID,
                        $"No hay stock disponible con fecha de manufactura válida para el producto (ID: {context.ProductId}).",
                        context.PendingQuantity);
                    _logger.LogInfo("#FAILURE_DETECTED: MANUFACTURING_DATE_INVALID");
                    return;
                }

                if (context.HasSpecificLot)
                {
                    var lotNo = context.SpecificLotNo;
                    if (string.IsNullOrWhiteSpace(lotNo))
                    {
                        context.AddFailure(
                            Interfaces.ReservationFailureCode.NO_STOCK,
                            $"No hay stock disponible para el producto (ID: {context.ProductId}). (Lote específico no definido)",
                            context.PendingQuantity);
                        _logger.LogInfo("#FAILURE_DETECTED: NO_STOCK (SpecificLotNo null)");
                        return;
                    }

                    context.AddLotFailure(lotNo, context.PendingQuantity);
                    _logger.LogInfo($"#FAILURE_DETECTED: LOT_NOT_FOUND - Lote: {lotNo}");
                }
                else
                {
                    context.AddFailure(
                        Interfaces.ReservationFailureCode.NO_STOCK,
                        $"No hay stock disponible para el producto (ID: {context.ProductId}).",
                        context.PendingQuantity);
                    _logger.LogInfo("#FAILURE_DETECTED: NO_STOCK");
                }
                return;
            }

            if (pickingCount == 0 && nonPickingCount > 0)
            {
                context.AddFailure(
                    Interfaces.ReservationFailureCode.PICKING_ZONE_REQUIRED_NO_STOCK,
                    "Stock disponible solo en zona de almacenaje (no picking).",
                    context.PendingQuantity);
                _logger.LogInfo("#FAILURE_WARNING: NO_PICKING_STOCK");
            }
            else if (nonPickingCount == 0 && pickingCount > 0)
            {
                context.AddFailure(
                    Interfaces.ReservationFailureCode.NON_PICKING_ZONE_REQUIRED_NO_STOCK,
                    "Stock disponible solo en zona de picking.",
                    context.PendingQuantity);
                _logger.LogInfo("#FAILURE_WARNING: NO_NON_PICKING_STOCK");
            }
        }

        private void FilterInvalidManufacturingDates(ReservationContext context)
        {
            var invalidDate = new DateTime(1900, 1, 1);
            int removedPicking = 0;
            int removedNonPicking = 0;

            if (context.StockListPickingZone != null && context.StockListPickingZone.Count > 0)
            {
                int before = context.StockListPickingZone.Count;
                context.StockListPickingZone = context.StockListPickingZone
                    .Where(s => s.Fecha_Manufactura > invalidDate)
                    .ToList();
                removedPicking = before - context.StockListPickingZone.Count;
            }

            if (context.StockListNonPickingZones != null && context.StockListNonPickingZones.Count > 0)
            {
                int before = context.StockListNonPickingZones.Count;
                context.StockListNonPickingZones = context.StockListNonPickingZones
                    .Where(s => s.Fecha_Manufactura > invalidDate)
                    .ToList();
                removedNonPicking = before - context.StockListNonPickingZones.Count;
            }

            if (removedPicking > 0 || removedNonPicking > 0)
            {
                _logger.LogInfo($"#MANUFACTURA_FILTER | Excluidos por fecha_manufactura inválida: " +
                               $"Picking={removedPicking}, NonPicking={removedNonPicking}");
            }
        }
    }
}