using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Log;
using WMS.EntityCore.Pedido;
using WMS.StockReservation.Core.Domain;
using WMS.StockReservation.Infrastructure;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Compatibility
{
    /// <summary>
    /// Facade moderno de reserva de stock con pipeline refactorizado.
    /// Soporta tanto la firma VB.NET original (con ref) como API moderna sin ref.
    /// Utiliza internamente la arquitectura del pipeline (no codigo procedural legacy).
    /// </summary>
    public static class StockReservationFacade
    {
        /// <summary>
        /// SOBRECARGA LEGACY: Mantiene compatibilidad con el estilo VB (con ref).
        /// Nota C#: no se permiten parametros opcionales antes de un parametro ref.
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
            clsBeI_nav_ped_traslado_det? pBeTrasladoDet = null,
            bool pEsManufactura = false
        )
        {
            pListStockResOUT ??= new List<clsBeStock_res>();
            pCantidadDisponibleStock = 0;

            try
            {
                if (pStockResSolicitud == null)
                    throw new ArgumentNullException(nameof(pStockResSolicitud));
                if (pBeConfigEnc == null)
                    throw new ArgumentNullException(nameof(pBeConfigEnc));
                if (lConnection == null)
                    throw new ArgumentNullException(nameof(lConnection));

                var machineName = string.IsNullOrWhiteSpace(MaquinaQueSolicita)
                    ? Environment.MachineName
                    : MaquinaQueSolicita.Trim();

                pListStockResOUT ??= new List<clsBeStock_res>();

                ValidateLegacyRequest(pStockResSolicitud, pBeConfigEnc);

                if (pIdPropietarioBodega > 0)
                    pStockResSolicitud.IdPropietarioBodega = pIdPropietarioBodega;

                var reservations = Reserva_Stock_Internal(
                    oBeStockResRequest: pStockResSolicitud,
                    IdProducto: 0,
                    oBeConfigEnc: pBeConfigEnc,
                    cnnSql: lConnection,
                    trSql: ltransaction,
                    oBePedidoDet: pBePedidoDet,
                    oBeI_nav_ped_traslado_det: pBeTrasladoDet,
                    Tarea_Reabasto: pTarea_Reabasto,
                    EsDevolucion: false,
                    LineNumber: No_Linea,
                    MachineName: machineName,
                    DiasVencimiento: DiasVencimiento,
                    EsManufactura: pEsManufactura
                );

                pListStockResOUT = reservations ?? new List<clsBeStock_res>();

                double total = 0;
                foreach (var reserva in pListStockResOUT)
                    total += reserva?.Cantidad ?? 0;

                pCantidadDisponibleStock = total;

                if (pBeTrasladoDet != null)
                    pBeTrasladoDet.Qty_to_Receive = pCantidadDisponibleStock;

                return pListStockResOUT.Count > 0;
            }
            catch (Exception ex)
            {
                SafeTrace(ex);

                TryLogErrorToDb(
                    ex: ex,
                    cnn: lConnection,
                    tr: ltransaction,
                    cfg: pBeConfigEnc,
                    lineNo: No_Linea,
                    cantidad: pStockResSolicitud?.Cantidad ?? 0
                );

                pListStockResOUT = new List<clsBeStock_res>();
                pCantidadDisponibleStock = 0;
                return false;
            }
        }

        /// <summary>
        /// SOBRECARGA LEGACY ALTERNATIVA: Para llamadas desde clsLnTrans_pe_det
        /// con firma: (ref stock, dias, maquina, config, ref qty, propietario, ref list, conn, trans, lineNo, tareaReabasto, trasladoDet, pedidoDet)
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
            int No_Linea,
            bool pTarea_Reabasto,
            clsBeI_nav_ped_traslado_det pBeTrasladoDet,
            clsBeTrans_pe_det pBePedidoDet,
            bool pEsManufactura = false
        )
        {
            pListStockResOUT ??= new List<clsBeStock_res>();
            pCantidadDisponibleStock = 0;

            try
            {
                if (pStockResSolicitud == null)
                    throw new ArgumentNullException(nameof(pStockResSolicitud));
                if (pBeConfigEnc == null)
                    throw new ArgumentNullException(nameof(pBeConfigEnc));
                if (lConnection == null)
                    throw new ArgumentNullException(nameof(lConnection));

                var machineName = string.IsNullOrWhiteSpace(MaquinaQueSolicita)
                    ? Environment.MachineName
                    : MaquinaQueSolicita.Trim();

                if (pIdPropietarioBodega > 0)
                    pStockResSolicitud.IdPropietarioBodega = pIdPropietarioBodega;

                var reservations = Reserva_Stock_Internal(
                    oBeStockResRequest: pStockResSolicitud,
                    IdProducto: 0,
                    oBeConfigEnc: pBeConfigEnc,
                    cnnSql: lConnection,
                    trSql: ltransaction,
                    oBePedidoDet: pBePedidoDet,
                    oBeI_nav_ped_traslado_det: pBeTrasladoDet,
                    Tarea_Reabasto: pTarea_Reabasto,
                    EsDevolucion: false,
                    LineNumber: No_Linea,
                    MachineName: machineName,
                    DiasVencimiento: DiasVencimiento,
                    EsManufactura: pEsManufactura
                );

                pListStockResOUT = reservations ?? new List<clsBeStock_res>();

                double total = 0;
                foreach (var res in pListStockResOUT)
                    total += res.Cantidad;

                pCantidadDisponibleStock = total;

                if (pBeTrasladoDet != null)
                    pBeTrasladoDet.Qty_to_Receive = pCantidadDisponibleStock;

                return pListStockResOUT.Count > 0;
            }
            catch (Exception ex)
            {
                SafeTrace(ex);
                TryLogErrorToDb(ex, lConnection, ltransaction, pBeConfigEnc, No_Linea, pStockResSolicitud?.Cantidad ?? 0);
                pListStockResOUT = new List<clsBeStock_res>();
                pCantidadDisponibleStock = 0;
                return false;
            }
        }

        /// <summary>
        /// SOBRECARGA MODERNA: API simplificada sin parametros ref.
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
            double DiasVencimiento = 0,
            bool EsManufactura = false
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
                DiasVencimiento: DiasVencimiento,
                EsManufactura: EsManufactura
            );
        }

        /// <summary>
        /// Implementacion interna compartida que ejecuta el pipeline de reserva.
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
            double DiasVencimiento = 0,
            bool EsManufactura = false
        )
        {
            if (oBeStockResRequest == null)
                throw new ArgumentNullException(nameof(oBeStockResRequest), "El request de reserva no puede ser null");
            if (oBeConfigEnc == null)
                throw new ArgumentNullException(nameof(oBeConfigEnc), "La configuracion no puede ser null");
            if (cnnSql == null)
                throw new ArgumentNullException(nameof(cnnSql), "La conexion SQL no puede ser null");

            if (cnnSql.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexion SQL debe estar abierta (State=Open) para reservar stock");

            if (oBeStockResRequest.Cantidad <= 0)
                throw new ArgumentOutOfRangeException(nameof(oBeStockResRequest.Cantidad), "La cantidad a reservar debe ser > 0");

            var resolvedBodegaId = oBeStockResRequest.IdBodega > 0 ? oBeStockResRequest.IdBodega : oBeConfigEnc.Idbodega;
            if (resolvedBodegaId <= 0)
                throw new ArgumentException("Debe especificar IdBodega en el request o en la configuracion");

            oBeStockResRequest.IdBodega = resolvedBodegaId;

            var machineName = string.IsNullOrWhiteSpace(MachineName)
                ? Environment.MachineName
                : MachineName.Trim();

            if (DiasVencimiento < 0)
                DiasVencimiento = 0;

            if (IdProducto <= 0 && oBeStockResRequest.IdProductoBodega <= 0)
                throw new ArgumentException("Debe especificar IdProducto o IdProductoBodega en el request");

            var orderNumber = ResolveOrderNumber(oBePedidoDet, oBeI_nav_ped_traslado_det);

            try
            {
                orderNumber ??= string.Empty;
                machineName = string.IsNullOrWhiteSpace(machineName)
                    ? Environment.MachineName
                    : machineName.Trim();

                var factory = new ServiceFactory();

                var pipeline = factory.CreateReservationPipeline()
                    ?? throw new InvalidOperationException(
                        $"CreateReservationPipeline devolvio null (orderNumber='{orderNumber}').");

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
                    PedidoDet = oBePedidoDet,
                    TrasladoDet = oBeI_nav_ped_traslado_det,
                    TareaReabasto = Tarea_Reabasto,
                    EsDevolucion = EsDevolucion,
                    LineNumber = LineNumber,
                    MachineName = machineName,
                    DiasVencimiento = DiasVencimiento < 0 ? 0 : DiasVencimiento,
                    SpecificLotNo = oBeI_nav_ped_traslado_det?.Lote_No,
                    EsManufactura = EsManufactura
                };

                var result = pipeline.Execute(context)
                    ?? throw new InvalidOperationException(
                        $"Pipeline.Execute devolvio null (orderNumber='{orderNumber}', line={LineNumber}).");

                if (context.HasError)
                    throw new Exception($"Error en reserva de stock: {context.ErrorMessage ?? "Sin detalle"}");

                var reservations = result.Reservations ?? new List<clsBeStock_res>();

                if (reservations.Count == 0 && context.FailureReasons?.Count > 0)
                    oBeStockResRequest.UltimoMensajeFallo = context.FailureReasons.First().Message;

                return reservations;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error ejecutando Reserva_Stock_From_MI3 (Producto={IdProducto}, Line={LineNumber}): {ex.Message}",
                    ex);
            }
        }

        /// <summary>
        /// Version simplificada para casos basicos (sin pedidos/traslados).
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

        private static void ValidateLegacyRequest(clsBeStock_res req, clsBeI_nav_config_enc cfg)
        {
            if (req.Cantidad <= 0)
                throw new ArgumentOutOfRangeException(nameof(req.Cantidad), "La cantidad a reservar debe ser > 0");

            if (req.IdProductoBodega <= 0)
            {
                throw new ArgumentException("El request legacy debe incluir IdProductoBodega (>0)", nameof(req.IdProductoBodega));
            }

            var bodegaId = req.IdBodega > 0 ? req.IdBodega : cfg.Idbodega;
            if (bodegaId <= 0)
                throw new ArgumentException("Debe especificar IdBodega en el request o configuracion");
        }

        private static string? ResolveOrderNumber(clsBeTrans_pe_det? pedido, clsBeI_nav_ped_traslado_det? traslado)
        {
            if (traslado != null && !string.IsNullOrEmpty(traslado.NoEnc))
                return traslado.NoEnc;

            if (pedido != null && pedido.IdPedidoEnc > 0)
                return $"PED_{pedido.IdPedidoEnc}";

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

                var idEmpresa = 1;
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
            }
        }
    }
}
