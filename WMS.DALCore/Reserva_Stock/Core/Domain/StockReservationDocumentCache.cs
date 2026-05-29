using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Stock;
using WMSWebAPI.Be;

namespace WMS.StockReservation.Core.Domain
{
    /// <summary>
    /// Cache por documento MI3: conserva consultas de stock ya ajustadas por reservas
    /// existentes en BD y descuenta en memoria las reservas insertadas despues.
    /// </summary>
    public class StockReservationDocumentCache
    {
        private sealed class StockCacheEntry
        {
            public int BaseSequence { get; init; }
            public List<clsBeStock> BaseStock { get; init; } = new();
        }

        private readonly Dictionary<string, StockCacheEntry> _stockByKey = new();
        private readonly List<(int Sequence, int IdStock, double Cantidad)> _documentReservations = new();
        private int _sequence;

        public List<clsBeStock>? GetStock(
            clsBeStock_res request,
            clsBeProducto? product,
            int diasVencimiento,
            clsBeI_nav_config_enc configuration,
            SqlConnection connection,
            SqlTransaction? transaction,
            bool excluirUbicacionPicking,
            bool conmutarUmbasAPresentacion,
            bool tareaReabasto,
            bool esDevolucion,
            bool esManufactura)
        {
            var key = BuildStockKey(
                request,
                diasVencimiento,
                configuration,
                excluirUbicacionPicking,
                conmutarUmbasAPresentacion,
                tareaReabasto,
                esDevolucion,
                esManufactura);

            if (!_stockByKey.TryGetValue(key, out var entry))
            {
                var tempRequest = request;
                var tempProduct = product;

                var stock = clsLnStock.lStock(
                    ref tempRequest,
                    ref tempProduct,
                    diasVencimiento,
                    configuration,
                    connection,
                    transaction,
                    pExcluirUbicacionPicking: excluirUbicacionPicking,
                    Conmutar_Umbas_A_Presentacion: conmutarUmbasAPresentacion,
                    pTarea_Reabasto: tareaReabasto,
                    pEs_Devolucion: esDevolucion,
                    pEsManufactura: esManufactura);

                if (stock != null && stock.Count > 0)
                {
                    clsLnStock_res.Restar_Stock_Reservado(stock, configuration, connection, transaction);
                }

                entry = new StockCacheEntry
                {
                    BaseSequence = _sequence,
                    BaseStock = CloneStockList(stock)
                };
                _stockByKey[key] = entry;
            }

            var result = CloneStockList(entry.BaseStock);
            ApplyDocumentReservations(result, entry.BaseSequence);
            return result.Where(s => s.Cantidad > 0.000001).ToList();
        }

        public void RegisterReservations(IEnumerable<clsBeStock_res>? reservations)
        {
            if (reservations == null) return;

            foreach (var reservation in reservations)
            {
                if (reservation == null || reservation.IdStock <= 0 || reservation.Cantidad <= 0)
                    continue;

                _sequence++;
                _documentReservations.Add((_sequence, reservation.IdStock, reservation.Cantidad));
            }
        }

        private void ApplyDocumentReservations(List<clsBeStock> stock, int baseSequence)
        {
            if (stock.Count == 0 || _documentReservations.Count == 0)
                return;

            var pendingByStock = _documentReservations
                .Where(r => r.Sequence > baseSequence)
                .GroupBy(r => r.IdStock)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Cantidad));

            if (pendingByStock.Count == 0)
                return;

            foreach (var item in stock)
            {
                if (item == null) continue;
                if (!pendingByStock.TryGetValue(item.IdStock, out var cantidadReservada))
                    continue;

                item.Cantidad = Math.Max(0, item.Cantidad - cantidadReservada);
            }
        }

        private static List<clsBeStock> CloneStockList(IEnumerable<clsBeStock>? source)
        {
            if (source == null) return new List<clsBeStock>();

            return source
                .Where(s => s != null)
                .Select(s => (clsBeStock)s.Clone())
                .ToList();
        }

        private static string BuildStockKey(
            clsBeStock_res request,
            int diasVencimiento,
            clsBeI_nav_config_enc configuration,
            bool excluirUbicacionPicking,
            bool conmutarUmbasAPresentacion,
            bool tareaReabasto,
            bool esDevolucion,
            bool esManufactura)
        {
            return string.Join("|",
                configuration.Idbodega,
                request.IdBodega,
                request.IdProductoBodega,
                request.IdProductoEstado,
                request.IdUnidadMedida,
                request.IdPresentacion,
                request.Atributo_Variante_1 ?? string.Empty,
                request.IdUbicacionAbastecerCon,
                request.IdProductoTallaColor,
                request.Control_Ultimo_Lote,
                request.Ultimo_Lote ?? string.Empty,
                diasVencimiento,
                excluirUbicacionPicking,
                conmutarUmbasAPresentacion,
                tareaReabasto,
                esDevolucion,
                esManufactura,
                configuration.Excluir_Recepcion_Picking,
                configuration.Excluir_Ubicaciones_Reabasto,
                configuration.Explosion_Automatica,
                configuration.Explosion_Automatica_Desde_Ubicacion_Picking,
                configuration.Explosion_Automatica_Nivel_Max);
        }
    }
}
