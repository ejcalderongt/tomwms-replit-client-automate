using WMSWebAPI.Be;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Stock;
using Microsoft.Data.SqlClient;

namespace WMS.StockReservation.Core.Domain
{
    /// <summary>
    /// Contexto mutable compartido entre todos los pasos del pipeline y handlers.
    /// Contiene TODO el estado de la operación de reserva.
    /// </summary>
    public class ReservationContext
    {
        // ===== INPUT (parámetros de entrada) =====
        public clsBeStock_res Request { get; set; } = new clsBeStock_res();
        public int ProductId { get; set; }
        public clsBeI_nav_config_enc Configuration { get; set; } = new clsBeI_nav_config_enc();
        public SqlConnection Connection { get; set; } = new SqlConnection();
        public SqlTransaction? Transaction { get; set; }
        public clsBeTrans_pe_det PedidoDet { get; set; } = new clsBeTrans_pe_det();
        public clsBeI_nav_ped_traslado_det TrasladoDet { get; set; } = new clsBeI_nav_ped_traslado_det();
        public bool TareaReabasto { get; set; }=false;
        public bool EsDevolucion { get; set; } = false;
        public int LineNumber { get; set; } = 0;
        public string MachineName { get; set; } = string.Empty;
        public double DiasVencimiento { get; set; } = 0;

        // ===== ENTITIES (cargadas en EntityLoadingStep) =====
        public clsBeBodega Bodega { get; set; } = new clsBeBodega();
        public clsBeProducto Product { get; set; } = new clsBeProducto();
        public clsBeProducto_presentacion DefaultPresentation { get; set; } = new clsBeProducto_presentacion();

        // ===== STOCK LISTS (modificadas por handlers y steps) =====
        public List<clsBeStock> StockListPickingZone { get; set; } = new List<clsBeStock>();
        public List<clsBeStock> StockListNonPickingZones { get; set; } = new List<clsBeStock>();
        public List<clsBeStock> WorkingStockList { get; set; } = new List<clsBeStock>();

        // ===== DATES (calculadas en DateCalculationStep) =====
        public DateTime GlobalMinimumExpirationDate { get; set; } = new DateTime(1900, 1, 1);
        public DateTime MinExpirationDatePickingZone { get; set; } = new DateTime(1900, 1, 1);
        public DateTime MinExpirationDateNonPickingZones { get; set; } = new DateTime(1900, 1, 1);
        public DateTime MinExpirationCompletePalletsClavaud { get; set; } = new DateTime(1900, 1, 1);
        public DateTime MinExpirationIncompletePalletsClavaud { get; set; } = new DateTime(1900, 1, 1);

        // ===== STATE (estado de la operación) =====
        public double PendingQuantity { get; set; }
        public List<clsBeStock_res> CreatedReservations { get; set; } = new List<clsBeStock_res>();
        public List<int> ProcessStateFlags { get; set; } = new List<int>();
        public int StartingPoint { get; set; }
        public bool IsExplosionModeEnabled { get; set; } = false;
        public bool IsUMBasModeEnabled { get; set; } = false;

        // ===== CACHES (evitar consultas repetidas) =====
        public List<clsBeBodega_ubicacion> CachedLocations { get; set; } = new List<clsBeBodega_ubicacion>();
        public List<clsBeProducto_presentacion> CachedPresentations { get; set; } = new List<clsBeProducto_presentacion>();

        // ===== ERROR HANDLING =====
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        // ===== HELPER METHODS =====

        /// <summary>
        /// Verifica si la cantidad pendiente ya fue completamente reservada.
        /// </summary>
        public bool IsQuantityFullyReserved()
        {
            return PendingQuantity <= 0.000001;
        }

        /// <summary>
        /// Establece un error en el contexto, deteniendo el pipeline.
        /// </summary>
        public void SetError(string message)
        {
            HasError = true;
            ErrorMessage = message;
        }

        /// <summary>
        /// Verifica si se puede intentar el fallback de explosión.
        /// </summary>
        public bool CanRetryWithExplosion()
        {
            return !IsExplosionModeEnabled &&
                   Configuration.Explosion_Automatica &&
                   Request.IdPresentacion != 0 &&
                   DefaultPresentation != null;
        }

        /// <summary>
        /// Habilita el modo de explosión para la próxima iteración.
        /// </summary>
        public void EnableExplosionMode()
        {
            IsExplosionModeEnabled = true;
        }

        /// <summary>
        /// Verifica si se puede intentar el fallback de UMBas.
        /// </summary>
        public bool CanRetryWithUMBas()
        {
            return !IsUMBasModeEnabled &&
                   Request.IdPresentacion == 0 &&
                   DefaultPresentation != null;
        }

        /// <summary>
        /// Habilita el modo UMBas para la próxima iteración.
        /// </summary>
        public void EnableUMBasMode()
        {
            IsUMBasModeEnabled = true;
        }

        /// <summary>
        /// Valida los invariantes del contexto (solo en DEBUG).
        /// Ayuda a detectar bugs durante desarrollo.
        /// </summary>
        [Conditional("DEBUG")]
        public void ValidateInvariants(string checkpoint)
        {
            Debug.Assert(CreatedReservations != null, "CreatedReservations must be initialized");

            // Stock nunca negativo
            if (WorkingStockList != null)
            {
                foreach (var stock in WorkingStockList)
                {
                    Debug.Assert(stock.Cantidad >= -0.01,
                        $"[{checkpoint}] Stock {stock.IdStock} tiene cantidad negativa: {stock.Cantidad}");
                }
            }

            // Pending nunca negativo
            Debug.Assert(PendingQuantity >= -0.01,
                $"[{checkpoint}] PendingQuantity es negativo: {PendingQuantity}");

            // Total reservado <= solicitado (con tolerancia por decimales)
            double totalReserved = CreatedReservations.Sum(r => r.Cantidad);
            Debug.Assert(totalReserved <= Request.Cantidad + 0.01,
                $"[{checkpoint}] Total reservado ({totalReserved}) > solicitado ({Request.Cantidad})");
        }
    }
}