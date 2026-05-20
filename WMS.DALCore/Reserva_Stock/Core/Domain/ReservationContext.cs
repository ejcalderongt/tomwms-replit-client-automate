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
        public SqlTransaction? Transaction { get; set; } = null;
        public clsBeTrans_pe_det? PedidoDet { get; set; } = new clsBeTrans_pe_det();
        public clsBeI_nav_ped_traslado_det? TrasladoDet { get; set; } = new clsBeI_nav_ped_traslado_det();

        /// <summary>
        /// IdPedidoEnc del Trans_pe_enc creado. Se usa para asignar IdTransaccion en stock_res.
        /// </summary>
        public int IdPedidoEnc { get; set; } = 0;
        public bool TareaReabasto { get; set; } = false;
        public bool EsDevolucion { get; set; } = false;
        public int LineNumber { get; set; } = 0;
        public string MachineName { get; set; } = "";
        public double DiasVencimiento { get; set; } = 0;  // Días antes del vencimiento para filtrar stock

        // ===== ENTITIES (cargadas en EntityLoadingStep) =====
        public clsBeBodega? Bodega { get; set; } = new clsBeBodega();
        public clsBeProducto? Product { get; set; } = new clsBeProducto();
        public clsBeProducto_presentacion? DefaultPresentation { get; set; } = new clsBeProducto_presentacion();

        // ===== STOCK LISTS (modificadas por handlers y steps) =====
        public List<clsBeStock>? StockListPickingZone { get; set; } = new List<clsBeStock>();
        public List<clsBeStock>? StockListNonPickingZones { get; set; } = new List<clsBeStock>();
        public List<clsBeStock> WorkingStockList { get; set; } = new List<clsBeStock>();

        // ===== DATES (calculadas en DateCalculationStep) =====
        public DateTime GlobalMinimumExpirationDate { get; set; } = new DateTime(1900,1,1);
        public DateTime MinExpirationDatePickingZone { get; set; } = new DateTime(1900, 1, 1);
        public DateTime MinExpirationDateNonPickingZones { get; set; } = new DateTime(1900, 1, 1);
        public DateTime MinExpirationCompletePalletsClavaud { get; set; } = new DateTime(1900, 1, 1);
        public DateTime MinExpirationIncompletePalletsClavaud { get; set; } = new DateTime(1900, 1, 1);

        // ===== STATE (estado de la operación) =====
        public double PendingQuantity { get; set; } = 0;
        public List<clsBeStock_res> CreatedReservations { get; set; } = new List<clsBeStock_res>();
        public List<int> ProcessStateFlags { get; set; } = new List<int>();
        public int StartingPoint { get; set; } = new int();
        public bool IsExplosionModeEnabled { get; set; } = new bool();
        public bool IsUMBasModeEnabled { get; set; } = new bool();
        
        /// <summary>
        /// Cantidad original solicitada (antes de conversión a unidades).
        /// Ejemplo: 0.5 cajas, 1.25 presentaciones
        /// </summary>
        public double OriginalRequestedQuantity { get; set; } = 0;
        
        /// <summary>
        /// Indica si la cantidad original estaba en presentación (necesitó conversión a unidades).
        /// true = Request.Cantidad era en presentación y fue convertida
        /// false = Request.Cantidad ya estaba en unidades
        /// </summary>
        public bool WasQuantityInPresentation { get; set; } = false;
        
        /// <summary>
        /// Factor de presentación usado para la conversión.
        /// </summary>
        public double ConversionFactor { get; set; } = 1;
        
        // ===== LOTE ESPECÍFICO (para transferencias/reservas específicas) =====
        /// <summary>
        /// Número de lote específico desde lineas_Detalle.lote_No.
        /// Si tiene valor, el sistema reservará SOLO ese lote por Batch_No.
        /// </summary>
        public string? SpecificLotNo { get; set; }
        
        /// <summary>
        /// Indica si hay un lote específico definido.
        /// </summary>
        public bool HasSpecificLot => !string.IsNullOrWhiteSpace(SpecificLotNo);

        // ===== MANUFACTURA (validación por fecha de manufactura) =====
        /// <summary>
        /// Indica si la validación de días debe usar fecha_manufactura en lugar de fecha_vence.
        /// Se determina por cliente_tiempos.es_manufactura.
        /// </summary>
        public bool EsManufactura { get; set; } = false;

        // ===== VENCIMIENTO (diagnóstico de causa de fallo) =====
        /// <summary>
        /// Verdadero si existía stock disponible pero fue eliminado por estar vencido
        /// (fecha_vence menor o igual a hoy). Permite a DetectStockFailures distinguir
        /// entre "no hay stock" y "hay stock pero está vencido".
        /// </summary>
        public bool HadExpiredStock { get; set; } = false;

        // ===== CACHES (evitar consultas repetidas) =====
        public List<clsBeBodega_ubicacion> CachedLocations { get; set; } = new List<clsBeBodega_ubicacion>();
        public List<clsBeProducto_presentacion> CachedPresentations { get; set; } = new List<clsBeProducto_presentacion>();

        // ===== ERROR HANDLING =====
        public bool HasError { get; set; }=false;
        public string ErrorMessage { get; set; } = "";
        
        // ===== FAILURE REASONS (para respuestas detalladas) =====
        public List<ReservationFailureReason> FailureReasons { get; set; } = new();
        public bool UsedPickingZone { get; set; } = false;
        public bool UsedNonPickingZone { get; set; } = false;
        public bool ExplosionModeEnabled { get; set; } = false;

        // ===== CONSTRUCTOR =====
        public ReservationContext()
        {
            CreatedReservations = new List<clsBeStock_res>();
            ProcessStateFlags = new List<int>();
            CachedLocations = new List<clsBeBodega_ubicacion>();
            CachedPresentations = new List<clsBeProducto_presentacion>();
            FailureReasons = new List<ReservationFailureReason>();
            GlobalMinimumExpirationDate = new DateTime(1900, 1, 1);
            MinExpirationDatePickingZone = new DateTime(1900, 1, 1);
            MinExpirationDateNonPickingZones = new DateTime(1900, 1, 1);
            MinExpirationCompletePalletsClavaud = new DateTime(1900, 1, 1);
            MinExpirationIncompletePalletsClavaud = new DateTime(1900, 1, 1);
        }

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
        /// Agrega una razón de fallo al contexto.
        /// </summary>
        public void AddFailure(ReservationFailureCode code, string message, double? qtyAffected = null)
        {
            FailureReasons ??= new List<ReservationFailureReason>();
            FailureReasons.Add(new ReservationFailureReason
            {
                Code = code,
                Message = message,
                QuantityAffected = qtyAffected
            });
        }

        /// <summary>
        /// Agrega una razón de fallo con detalle de lote.
        /// </summary>
        public void AddLotFailure(string loteNo, double qty)
        {
            AddFailure(
                ReservationFailureCode.LOT_NOT_FOUND,
                $"Lote '{loteNo}' no encontrado o sin stock disponible.",
                qty);
            FailureReasons.Last().LoteNo = loteNo;
        }

        /// <summary>
        /// Agrega una razón de fallo por zona.
        /// </summary>
        public void AddZoneFailure(bool pickingRequired, double qty)
        {
            var code = pickingRequired
                ? ReservationFailureCode.PICKING_ZONE_REQUIRED_NO_STOCK
                : ReservationFailureCode.NON_PICKING_ZONE_REQUIRED_NO_STOCK;
            var zone = pickingRequired ? "picking" : "almacenaje";
            AddFailure(code, $"No hay stock disponible en zona de {zone}.", qty);
        }

        /// <summary>
        /// Verifica si ya existe una razón de fallo específica.
        /// </summary>
        public bool HasFailure(ReservationFailureCode code)
        {
            return FailureReasons?.Any(r => r.Code == code) ?? false;
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
