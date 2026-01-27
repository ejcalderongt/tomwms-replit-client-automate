using Microsoft.Extensions.Configuration;
using WMS.DALCore.I_nav_ped_traslado_det;
using WMSWebAPI.Be;

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
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (_logger is null) throw new InvalidOperationException("Logger no inicializado.");

            // Normaliza lista para evitar null refs
            context.CreatedReservations ??= new List<clsBeStock_res>(); // ajusta tipo real

            _logger.LogCheckpoint("#MI3_POST_PROCESSING_START");

            // Request es obligatorio para este step (se usa en pasos 1 y 3)
            if (context.Request is null)
                throw new InvalidOperationException("ReservationContext.Request es null en PostProcessingStep.");

            // Connection/Transaction se requieren si vas a insertar/actualizar
            if (context.Connection is null)
                throw new InvalidOperationException("ReservationContext.Connection es null en PostProcessingStep.");

            if (context.Transaction is null)
                throw new InvalidOperationException("ReservationContext.Transaction es null en PostProcessingStep.");

            // =========================
            // PASO 1: INSERTAR RESERVAS
            // =========================
            if (context.CreatedReservations.Count > 0)
            {
                _logger.LogCheckpoint($"#MI3_INSERTING_STOCK_RES - Count: {context.CreatedReservations.Count}");

                var config = new ConfigurationBuilder().Build();

                // Obtener IdPedidoEnc para IdTransaccion
                int idPedidoEnc = context.Request.IdPedido > 0 ? context.Request.IdPedido : 0;

                foreach (var reservation in context.CreatedReservations)
                {
                    if (reservation is null) continue; // por si la lista trae nulos

                    try
                    {
                        int maxId = clsLnStock_res.MaxID(context.Connection, context.Transaction);
                        reservation.IdStockRes = maxId + 1;

                        reservation.IdTransaccion = idPedidoEnc;

                        _logger.LogCheckpoint(
                            $"#MI3_STOCK_RES_MAXID - MaxID: {maxId}, NewIdStockRes: {reservation.IdStockRes}, IdTransaccion: {idPedidoEnc}");

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

            // ============================

        }
    }
}
