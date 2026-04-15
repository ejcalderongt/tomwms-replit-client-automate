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
            context.StockListPickingZone = clsLnStock.lStock(
                ref tempRequest,
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
                clsLnStock_res.Restar_Stock_Reservado(
                    context.StockListPickingZone,
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
            // Filtrar stock vencido (post-query, con diagnóstico)
            // =========================
            FilterExpiredStock(context);

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

                if (context.HadExpiredStock)
                {
                    context.AddFailure(
                        Interfaces.ReservationFailureCode.ALL_STOCK_EXPIRED,
                        $"El stock del producto (ID: {context.ProductId}) existe pero está vencido. No se puede reservar stock vencido.",
                        context.PendingQuantity);
                    _logger.LogInfo("#FAILURE_DETECTED: ALL_STOCK_EXPIRED");
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

        /// <summary>
        /// Filtra stock vencido de ambas listas (Picking y NonPicking) usando LINQ en lugar
        /// de SQL, para poder diagnosticar si la causa de fallo fue el vencimiento del stock.
        /// Solo aplica cuando el producto controla vencimiento y la bodega no permite
        /// despachar producto vencido, y no es una devolución, y no está en modo manufactura.
        /// Establece context.HadExpiredStock = true si había stock pero fue eliminado por vencimiento.
        /// </summary>
        private void FilterExpiredStock(ReservationContext context)
        {
            if (!ShouldFilterExpiredStock(context))
                return;

            var now = DateTime.Now;
            int totalBefore = (context.StockListPickingZone?.Count ?? 0)
                            + (context.StockListNonPickingZones?.Count ?? 0);

            int removedPicking = 0;
            int removedNonPicking = 0;

            if (context.StockListPickingZone != null && context.StockListPickingZone.Count > 0)
            {
                int before = context.StockListPickingZone.Count;
                context.StockListPickingZone = context.StockListPickingZone
                    .Where(s => s.Fecha_vence > now)
                    .ToList();
                removedPicking = before - context.StockListPickingZone.Count;
            }

            if (context.StockListNonPickingZones != null && context.StockListNonPickingZones.Count > 0)
            {
                int before = context.StockListNonPickingZones.Count;
                context.StockListNonPickingZones = context.StockListNonPickingZones
                    .Where(s => s.Fecha_vence > now)
                    .ToList();
                removedNonPicking = before - context.StockListNonPickingZones.Count;
            }

            int totalAfter = (context.StockListPickingZone?.Count ?? 0)
                           + (context.StockListNonPickingZones?.Count ?? 0);

            if (removedPicking > 0 || removedNonPicking > 0)
            {
                _logger.LogInfo($"#EXPIRY_FILTER | Excluidos por vencimiento: " +
                               $"Picking={removedPicking}, NonPicking={removedNonPicking}");
            }

            if (totalBefore > 0 && totalAfter == 0)
            {
                context.HadExpiredStock = true;
                _logger.LogInfo("#EXPIRY_FILTER | Todo el stock disponible estaba vencido → HadExpiredStock=true");
            }
        }

        /// <summary>
        /// Determina si se debe aplicar el filtro de vencimiento en código (LINQ).
        /// Replica exactamente las mismas condiciones que tenía el SQL original:
        ///   IdUbicacionAbastecerCon == 0
        ///   AND BeBodega != null AND pBeProductoOutput != null
        ///   AND producto.control_vencimiento == true
        ///   AND !bodega.Despachar_producto_vencido
        ///   AND !pEs_Devolucion
        /// Además, no aplica en modo manufactura (fecha_vence no es relevante en ese caso).
        /// </summary>
        private static bool ShouldFilterExpiredStock(ReservationContext context)
        {
            if (context.EsManufactura) return false;
            if (context.Request?.IdUbicacionAbastecerCon != 0) return false;

            // Réplica exacta del null-guard del SQL original: BeBodega != null && pBeProductoOutput != null
            if (context.Bodega == null || context.Product == null) return false;

            if (!context.Product.control_vencimiento) return false;
            if (context.Bodega.Despachar_producto_vencido) return false;
            if (context.EsDevolucion) return false;

            return true;
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
