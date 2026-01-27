using Microsoft.Data.SqlClient;
using WMS.EntityCore.Log;
using WMS.EntityCore.Pedido;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Compatibility
{
    /// <summary>
    /// Facade moderno de reserva de stock con pipeline refactorizado.
    /// Soporta tanto la firma VB.NET original (con ref) como API moderna sin ref.
    /// Utiliza internamente la arquitectura del pipeline (no código procedural legacy).
    /// </summary>
    public static class StockReservationFacade
    {
        /// <summary>
        /// SOBRECARGA LEGACY: Mantiene compatibilidad con el estilo VB (con ref).
        /// Nota C#: no se permiten parámetros opcionales antes de un parámetro ref.
        /// Por eso pBePedidoDet va antes de los opcionales.
        /// </summary>
        public static bool Reserva_Stock_From_MI3(
            ref clsBeStock_res pStockResSolicitud,
            double DiasVencimiento,
            string MaquinaQueSolicita,
            clsBeI_nav_config_enc pBeConfigEnc,
            ref double pCantidadDisponibleStock,
            int pIdPropietarioBodega,
            ref List<clsBeStock_res> pListStockResOUT,
            SqlConnection lConnection,
            SqlTransaction ltransaction,
            ref clsBeTrans_pe_det pBePedidoDet,
            int No_Linea = 0,
            bool pTarea_Reabasto = false,
            clsBeI_nav_ped_traslado_det? pBeTrasladoDet = null
        )
        {
            // Inicializar salidas de forma segura (aunque fallen validaciones)
            pListStockResOUT ??= new List<clsBeStock_res>();
            pCantidadDisponibleStock = 0;

            try
            {
                // Null safety / validaciones base
                if (pStockResSolicitud == null)
                    throw new ArgumentNullException(nameof(pStockResSolicitud));
                if (pBeConfigEnc == null)
                    throw new ArgumentNullException(nameof(pBeConfigEnc));
                if (lConnection == null)
                    throw new ArgumentNullException(nameof(lConnection));

                // Machine name seguro
                var machineName = string.IsNullOrWhiteSpace(MaquinaQueSolicita)
                    ? Environment.MachineName
                    : MaquinaQueSolicita.Trim();

                // Asegurar lista OUT no null
                pListStockResOUT ??= new List<clsBeStock_res>();

                // Validar request crítico
                ValidateLegacyRequest(pStockResSolicitud, pBeConfigEnc);

                // Mapear legacy → request
                if (pIdPropietarioBodega > 0)
                    pStockResSolicitud.IdPropietarioBodega = pIdPropietarioBodega;

                // Ejecutar pipeline
                var reservations = Reserva_Stock_Internal(
                    oBeStockResRequest: pStockResSolicitud,
                    IdProducto: 0, // ValidationStep resolverá desde request.IdProductoBodega
                    oBeConfigEnc: pBeConfigEnc,
                    cnnSql: lConnection,
                    trSql: ltransaction,
                    oBePedidoDet: pBePedidoDet,
                    oBeI_nav_ped_traslado_det: pBeTrasladoDet,
                    Tarea_Reabasto: pTarea_Reabasto,
                    EsDevolucion: false,
                    LineNumber: No_Linea,
                    MachineName: machineName,
                    DiasVencimiento: DiasVencimiento
                );

                pListStockResOUT = reservations ?? new List<clsBeStock_res>();

                // Sumar cantidad reservada de forma null-safe
                double total = 0;
                foreach (var reserva in pListStockResOUT)
                    total += reserva?.Cantidad ?? 0;

                pCantidadDisponibleStock = total;

                // Actualizar Qty_to_Receive si aplica
                if (pBeTrasladoDet != null)
                    pBeTrasladoDet.Qty_to_Receive = pCantidadDisponibleStock;

                return pListStockResOUT.Count > 0;
            }
            catch (Exception ex)
            {
                SafeTrace(ex);

                // Logging a BD (best-effort)
                TryLogErrorToDb(
                    ex: ex,
                    cnn: lConnection,
                    tr: ltransaction,
                    cfg: pBeConfigEnc,
                    lineNo: No_Linea,
                    cantidad: pStockResSolicitud?.Cantidad ?? 0
                );

                // Reset outputs por contrato legacy
                pListStockResOUT = new List<clsBeStock_res>();
                pCantidadDisponibleStock = 0;
                return false;
            }
        }

        /// <summary>
        /// SOBRECARGA MODERNA: API simplificada sin parámetros ref.
        /// </summary>
        public static List<clsBeStock_res> Reserva_Stock_From_MI3(
            clsBeStock_res oBeStockResRequest,
            int IdProducto,
            clsBeI_nav_config_enc oBeConfigEnc,
            SqlConnection cnnSql,
            SqlTransaction? trSql = null,
            clsBeTrans_pe_det? oBePedidoDet = null,
            clsBeI_nav_ped_traslado_det? oBeI_nav_ped_traslado_det = null,
            bool Tarea_Reabasto = false,
            bool EsDevolucion = false,
            int LineNumber = 0,
            string MachineName = "",
            double DiasVencimiento = 0
        )
        {
            return Reserva_Stock_Internal(
                oBeStockResRequest: oBeStockResRequest,
                IdProducto: IdProducto,
                oBeConfigEnc: oBeConfigEnc,
                cnnSql: cnnSql,
                trSql: trSql,
                oBePedidoDet: oBePedidoDet,
                oBeI_nav_ped_traslado_det: oBeI_nav_ped_traslado_det,
                Tarea_Reabasto: Tarea_Reabasto,
                EsDevolucion: EsDevolucion,
                LineNumber: LineNumber,
                MachineName: MachineName,
                DiasVencimiento: DiasVencimiento
            );
        }

        /// <summary>
        /// Implementación interna compartida que ejecuta el pipeline de reserva.
        /// </summary>
        private static List<clsBeStock_res> Reserva_Stock_Internal(
            clsBeStock_res oBeStockResRequest,
            int IdProducto,
            clsBeI_nav_config_enc oBeConfigEnc,
            SqlConnection cnnSql,
            SqlTransaction? trSql,
            clsBeTrans_pe_det? oBePedidoDet,
            clsBeI_nav_ped_traslado_det? oBeI_nav_ped_traslado_det,
            bool Tarea_Reabasto,
            bool EsDevolucion,
            int LineNumber,
            string MachineName,
            double DiasVencimiento = 0
        )
        {
            // Validaciones fuertes y null-safety
            if (oBeStockResRequest == null)
                throw new ArgumentNullException(nameof(oBeStockResRequest), "El request de reserva no puede ser null");
            if (oBeConfigEnc == null)
                throw new ArgumentNullException(nameof(oBeConfigEnc), "La configuración no puede ser null");
            if (cnnSql == null)
                throw new ArgumentNullException(nameof(cnnSql), "La conexión SQL no puede ser null");

            if (cnnSql.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("La conexión SQL debe estar abierta (State=Open) para reservar stock");

            if (oBeStockResRequest.Cantidad <= 0)
                throw new ArgumentOutOfRangeException(nameof(oBeStockResRequest.Cantidad), "La cantidad a reservar debe ser > 0");

            // Resolver bodega de forma consistente
            var resolvedBodegaId = oBeStockResRequest.IdBodega > 0 ? oBeStockResRequest.IdBodega : oBeConfigEnc.Idbodega;
            if (resolvedBodegaId <= 0)
                throw new ArgumentException("Debe especificar IdBodega en el request o en la configuración");

            oBeStockResRequest.IdBodega = resolvedBodegaId;

            // Machine name seguro
            var machineName = string.IsNullOrWhiteSpace(MachineName)
                ? Environment.MachineName
                : MachineName.Trim();

            // Días vencimiento: evitar negativos
            if (DiasVencimiento < 0)
                DiasVencimiento = 0;

            // Si IdProducto viene en 0, el pipeline puede resolverlo desde IdProductoBodega.
            // Aun así validamos que haya al menos una forma de identificar producto.
            if (IdProducto <= 0 && oBeStockResRequest.IdProductoBodega <= 0)
                throw new ArgumentException("Debe especificar IdProducto o IdProductoBodega en el request");

            // Para auditoría
            var orderNumber = ResolveOrderNumber(oBePedidoDet, oBeI_nav_ped_traslado_det);

            try
            {
                // orderNumber/machineName seguros
                orderNumber ??= string.Empty;
                machineName = string.IsNullOrWhiteSpace(machineName)
                    ? Environment.MachineName
                    : machineName.Trim();

                var factory = new ServiceFactory(); // si esto lanza, cae al catch

                // Null-safety: si por diseño pudiera retornar null
                var pipeline = factory.CreateReservationPipeline(orderNumber)
                    ?? throw new InvalidOperationException(
                        $"CreateReservationPipeline devolvió null (orderNumber='{orderNumber}').");

                // Contexto nunca es null, pero sí validamos entradas críticas si quieres fail-fast aquí también
                if (oBeStockResRequest == null) throw new ArgumentNullException(nameof(oBeStockResRequest));
                if (oBeConfigEnc == null) throw new ArgumentNullException(nameof(oBeConfigEnc));
                if (cnnSql == null) throw new ArgumentNullException(nameof(cnnSql));

                var context = new ReservationContext
                {
                    Request = oBeStockResRequest,
                    ProductId = IdProducto,
                    Configuration = oBeConfigEnc,
                    Connection = cnnSql,
                    Transaction = trSql,
                    PedidoDet = oBePedidoDet,                       // puede ser null OK
                    TrasladoDet = oBeI_nav_ped_traslado_det,        // puede ser null OK
                    TareaReabasto = Tarea_Reabasto,
                    EsDevolucion = EsDevolucion,
                    LineNumber = LineNumber,
                    MachineName = machineName,
                    DiasVencimiento = DiasVencimiento < 0 ? 0 : DiasVencimiento
                };

                // Null-safety del resultado
                var result = pipeline.Execute(context)
                    ?? throw new InvalidOperationException(
                        $"Pipeline.Execute devolvió null (orderNumber='{orderNumber}', line={LineNumber}).");

                // Si el pipeline marca error, lanzar con el mensaje (null-safe)
                if (context.HasError)
                    throw new Exception($"Error en reserva de stock: {context.ErrorMessage ?? "Sin detalle"}");

                // Retorno null-safe
                return result.Reservations ?? new List<clsBeStock_res>();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error ejecutando Reserva_Stock_From_MI3 (Producto={IdProducto}, Line={LineNumber}): {ex.Message}",
                    ex);
            }

        }

        /// <summary>
        /// Versión simplificada para casos básicos (sin pedidos/traslados).
        /// </summary>
        public static List<clsBeStock_res> ReservarStock(
            int bodegaId,
            int productoId,
            double cantidad,
            int presentacionId,
            clsBeI_nav_config_enc configuracion,
            SqlConnection connection,
            SqlTransaction? transaction = null,
            string machineName = "",
            double diasVencimiento = 0
        )
        {
            if (configuracion == null) throw new ArgumentNullException(nameof(configuracion));
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (bodegaId <= 0) throw new ArgumentOutOfRangeException(nameof(bodegaId));
            if (productoId <= 0) throw new ArgumentOutOfRangeException(nameof(productoId));
            if (cantidad <= 0) throw new ArgumentOutOfRangeException(nameof(cantidad));
            if (presentacionId <= 0) throw new ArgumentOutOfRangeException(nameof(presentacionId));

            var request = new clsBeStock_res
            {
                IdBodega = bodegaId,
                IdProductoBodega = productoId,
                IdPresentacion = presentacionId,
                Cantidad = cantidad
            };

            return Reserva_Stock_From_MI3(
                oBeStockResRequest: request,
                IdProducto: productoId,
                oBeConfigEnc: configuracion,
                cnnSql: connection,
                trSql: transaction,
                MachineName: machineName,
                DiasVencimiento: diasVencimiento
            );
        }

        // =========================
        // Helpers / Null safety
        // =========================

        private static void ValidateLegacyRequest(clsBeStock_res req, clsBeI_nav_config_enc cfg)
        {
            // req ya viene null-check arriba
            if (req.Cantidad <= 0)
                throw new ArgumentOutOfRangeException(nameof(req.Cantidad), "La cantidad a reservar debe ser > 0");

            // Permitir que el pipeline resuelva IdProducto si viene IdProductoBodega,
            // pero al menos debe venir uno.
            if (req.IdProductoBodega <= 0)
            {
                // Si tu request legacy trae otro campo para producto, valida aquí.
                // De momento, exigimos IdProductoBodega.
                throw new ArgumentException("El request legacy debe incluir IdProductoBodega (>0)", nameof(req.IdProductoBodega));
            }

            // Bodega: puede venir en req o en config
            var bodega = req.IdBodega > 0 ? req.IdBodega : cfg.Idbodega;
            if (bodega <= 0)
                throw new ArgumentException("Debe especificar IdBodega en el request o en la configuración");
        }

        private static string? ResolveOrderNumber(clsBeTrans_pe_det? pedidoDet, clsBeI_nav_ped_traslado_det? trasladoDet)
        {
            if (trasladoDet != null && !string.IsNullOrWhiteSpace(trasladoDet.NoEnc))
                return trasladoDet.NoEnc;

            if (pedidoDet != null && pedidoDet.IdPedidoEnc > 0)
                return $"PED_{pedidoDet.IdPedidoEnc}";

            return null;
        }

        private static void SafeTrace(Exception ex)
        {
            Debug.WriteLine($"[StockReservationFacade] ERROR: {ex.Message}");
            Debug.WriteLine($"[StockReservationFacade] StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Debug.WriteLine($"[StockReservationFacade] Inner: {ex.InnerException.Message}");
                Debug.WriteLine($"[StockReservationFacade] InnerStack: {ex.InnerException.StackTrace}");
            }
        }

        private static void TryLogErrorToDb(
            Exception ex,
            SqlConnection? cnn,
            SqlTransaction? tr,
            clsBeI_nav_config_enc? cfg,
            int lineNo,
            double cantidad
        )
        {
            try
            {
                if (cnn == null || cnn.State != ConnectionState.Open)
                    return;

                var idEmpresa = cfg?.Idempresa ?? 1;
                var idBodega = cfg?.Idbodega ?? 0;

                var nextId = clsLnLog_error_wms.MaxID(cnn, tr) + 1;

                var errorLog = new clsBeLog_error_wms
                {
                    IdError = nextId,
                    IdEmpresa = idEmpresa,
                    IdBodega = idBodega,
                    Fecha = DateTime.Now,
                    MensajeError = $"RESERVA_ERROR: {ex.Message}",
                    Line_No = lineNo,
                    Item_No = "",
                    Cantidad = cantidad,
                    Referencia_Documento = "StockReservationFacade"
                };

                clsLnLog_error_wms.Insertar(errorLog, cnn, tr);
            }
            catch
            {
                // Best-effort: no romper el flujo por logging
            }
        }
    }
}
