using Microsoft.Extensions.Configuration;
using WMS.DALCore.I_nav_ped_traslado_det;

namespace WMS.StockReservation.Core.Services
{
    public class PostProcessingStep : IPipelineStep
    {
        private readonly IReservationLogger _logger;

        public PostProcessingStep(IReservationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(ReservationContext context)
        {
            _logger.LogCheckpoint("#MI3_POST_PROCESSING_START");

            // PASO 1: INSERTAR RESERVAS EN stock_res (ANTES de actualizar TrasladoDet)
            if (context.CreatedReservations.Count > 0)
            {
                _logger.LogCheckpoint($"#MI3_INSERTING_STOCK_RES - Count: {context.CreatedReservations.Count}");

                var config = new ConfigurationBuilder().Build();

                foreach (var reservation in context.CreatedReservations)
                {
                    try
                    {
                        // Calcular MaxID ANTES de cada INSERT
                        int maxId = clsLnStock_res.MaxID(context.Connection, context.Transaction);
                        reservation.IdStockRes = maxId + 1;

                        _logger.LogCheckpoint(
                            $"#MI3_STOCK_RES_MAXID - MaxID: {maxId}, NewIdStockRes: {reservation.IdStockRes}, " +
                            $"IdTransaccion: {reservation.IdTransaccion}, IdPedido: {reservation.IdPedido}, " +
                            $"IdPedidoDet: {reservation.IdPedidoDet}, Indicador: {reservation.Indicador}, Estado: {reservation.Estado}");

                        int rowsAffected = clsLnStock_res.Insertar(
                            config,
                            reservation,
                            context.Connection,
                            context.Transaction);

                        _logger.LogCheckpoint(
                            $"#MI3_STOCK_RES_INSERTED - " +
                            $"IdStockRes: {reservation.IdStockRes}, " +
                            $"IdStock: {reservation.IdStock}, " +
                            $"IdProductoBodega: {reservation.IdProductoBodega}, " +
                            $"Cantidad: {reservation.Cantidad:F6}, " +
                            $"IdUbicacion: {reservation.IdUbicacion}, " +
                            $"IdTransaccion: {reservation.IdTransaccion}, " +
                            $"RowsAffected: {rowsAffected}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCheckpoint($"#MI3_STOCK_RES_ERROR - IdStock: {reservation.IdStock}, Error: {ex.Message}");
                        throw;
                    }
                }
            }

            // PASO 2: Actualizar TrasladoDet.Quantity_Reserved_WMS
            if (context.TrasladoDet != null && context.CreatedReservations.Count > 0)
            {
                foreach (var reservation in context.CreatedReservations)
                {
                    if (reservation.IdPresentacion == 0)
                    {
                        context.TrasladoDet.Quantity_Reserved_WMS += reservation.Cantidad;
                    }
                    else
                    {
                        if (context.PedidoDet?.IdPresentacion == 0)
                        {
                            context.TrasladoDet.Quantity_Reserved_WMS += reservation.Cantidad;
                        }
                        else
                        {
                            var factor = context.DefaultPresentation?.Factor ?? 1;
                            context.TrasladoDet.Quantity_Reserved_WMS +=
                                Math.Round(reservation.Cantidad / factor, 6);
                        }
                    }
                }

                clsLnI_nav_ped_traslado_det.Actualizar_Quantity_Reserved_WMS(
                    context.TrasladoDet,
                    context.Product,
                    context.Connection,
                    context.Transaction);

                _logger.LogCheckpoint(
                    $"#MI3_TRASLADO_UPDATED - Cantidad: {context.TrasladoDet.Quantity_Reserved_WMS:F6}");
            }

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Request == null)
                throw new InvalidOperationException("ReservationContext.Request es null en el resumen.");

            var pending = context.PendingQuantity;

            var totalReserved = context.Request.Cantidad - pending;

            if (context.PendingQuantity > 0.000001 && context.FailureReasons?.Count > 0)
            {
                foreach (var failure in context.FailureReasons)
                {
                    _logger.LogWarning(
                        $"EVENTO=NO_RESERVA | RESULTADO=NO_RESERVADO | " +
                        $"TIPO_NO_RESERVA={MapearTipoNoReserva(failure.Code)} | " +
                        $"PEDIDO={context.PedidoDet?.IdPedidoEnc ?? 0} | " +
                        $"DETALLE={context.PedidoDet?.IdPedidoDet ?? 0} | " +
                        $"LINEA={context.LineNumber} | " +
                        $"ITEM={context.TrasladoDet?.Item_No ?? context.Product?.codigo ?? string.Empty} | " +
                        $"CANTIDAD_PENDIENTE={context.PendingQuantity:F6} | " +
                        $"MOTIVO={failure.Message}");
                }
            }

            _logger.LogCheckpoint(
                $"#MI3_SUMMARY - " +
                $"Solicitado: {context.Request.Cantidad:F6}, " +
                $"Reservado: {totalReserved:F6}, " +
                $"Pendiente: {context.PendingQuantity:F6}, " +
                $"Reservas creadas: {context.CreatedReservations.Count}");

#if DEBUG
            context.ValidateInvariants("POST_PROCESSING_END");
#endif

            _logger.LogCheckpoint("#MI3_POST_PROCESSING_END");
        }

        private static string MapearTipoNoReserva(ReservationFailureCode code)
        {
            return code switch
            {
                ReservationFailureCode.LOT_NOT_FOUND => "LOTE_NO_ENCONTRADO",
                ReservationFailureCode.LOCATION_RESTRICTED_NO_STOCK => "UBICACION_CLIENTE_OBLIGATORIA",
                ReservationFailureCode.PRODUCT_STATE_REQUIRED_NO_STOCK => "ESTADO_PRODUCTO_NO_APLICA",
                ReservationFailureCode.PICKING_ZONE_REQUIRED_NO_STOCK => "SOLO_NO_PICKING_SIN_EXPLOSION",
                ReservationFailureCode.NON_PICKING_ZONE_REQUIRED_NO_STOCK => "FEFO_BLOQUEA_PICKING",
                ReservationFailureCode.RECEPTION_LOCATION_NOT_ALLOWED => "UBICACION_NO_APLICA",
                ReservationFailureCode.ALL_STOCK_EXPIRED => "SIN_VENCIMIENTO_VALIDO",
                ReservationFailureCode.ZONE_PRIORITY_CONFLICT => "FEFO_BLOQUEA_PICKING",
                ReservationFailureCode.PRODUCT_NOT_FOUND => "PRODUCTO_NO_ENCONTRADO",
                ReservationFailureCode.INVALID_QUANTITY => "CANTIDAD_INVALIDA",
                ReservationFailureCode.STORAGE_CONDITION_MISMATCH => "CONDICION_ALMACENAJE_NO_APLICA",
                ReservationFailureCode.MANUFACTURING_DATE_INVALID => "SIN_FECHA_MANUFACTURA_VALIDA",
                ReservationFailureCode.NO_STOCK => "SIN_STOCK_APLICABLE",
                _ => "RESERVA_NO_COMPLETADA"
            };
        }
    }
}
