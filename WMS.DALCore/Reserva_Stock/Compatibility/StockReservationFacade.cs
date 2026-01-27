using Microsoft.Data.SqlClient;
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
        /// SOBRECARGA LEGACY: Firma exacta del VB.NET original (12 parámetros, retorna bool).
        /// Internamente usa el pipeline refactorizado moderno.
        /// Use esta sobrecarga para migrar código VB.NET sin cambios.
        /// </summary>
        /// <param name="pStockResSolicitud">Request de reserva (modificado por el pipeline)</param>
        /// <param name="DiasVencimiento">Días antes del vencimiento para considerar stock</param>
        /// <param name="MaquinaQueSolicita">Nombre de la máquina que solicita</param>
        /// <param name="pBeConfigEnc">Configuración de navegación</param>
        /// <param name="pCantidadDisponibleStock">Salida: cantidad total reservada</param>
        /// <param name="pIdPropietarioBodega">ID del propietario de la bodega</param>
        /// <param name="pListStockResOUT">Salida: lista de reservas creadas</param>
        /// <param name="lConnection">Conexión SQL activa</param>
        /// <param name="ltransaction">Transacción SQL activa</param>
        /// <param name="No_Linea">Número de línea (opcional)</param>
        /// <param name="pTarea_Reabasto">Indica si es tarea de reabasto (opcional)</param>
        /// <param name="pBeTrasladoDet">Detalle de traslado (opcional)</param>
        /// <returns>True si se crearon reservas exitosamente</returns>
        public static bool Reserva_Stock_From_MI3(ref clsBeStock_res pStockResSolicitud,
                                                  double DiasVencimiento,
                                                  string MaquinaQueSolicita,
                                                  clsBeI_nav_config_enc pBeConfigEnc,
                                                  ref double pCantidadDisponibleStock,
                                                  int pIdPropietarioBodega,
                                                  ref List<clsBeStock_res> pListStockResOUT,
                                                  SqlConnection lConnection,
                                                  SqlTransaction ltransaction,
                                                  int No_Linea = 0,
                                                  bool pTarea_Reabasto = false,
                                                  clsBeI_nav_ped_traslado_det? pBeTrasladoDet = null,
                                                  clsBeTrans_pe_det? pBePedidoDet = null)
        {
            try
            {
                // Validaciones básicas
                if (pStockResSolicitud == null)
                    throw new ArgumentNullException(nameof(pStockResSolicitud));
                if (lConnection == null)
                    throw new ArgumentNullException(nameof(lConnection));

                // MAPEO DE PARÁMETROS LEGACY A REQUEST:
                // Asignar IdPropietarioBodega al request (usado en consulta de stock)
                if (pIdPropietarioBodega > 0)
                {
                    pStockResSolicitud.IdPropietarioBodega = pIdPropietarioBodega;
                }

                // Usar IdProductoBodega directamente (los pedidos/reservas van en función de bodega)
                int idProductoBodega = pStockResSolicitud.IdProductoBodega;

                // Llamar al pipeline interno refactorizado con DiasVencimiento
                var reservations = Reserva_Stock_Internal(oBeStockResRequest: pStockResSolicitud,
                                                          IdProductoBodega: idProductoBodega,
                                                          oBeConfigEnc: pBeConfigEnc,
                                                          cnnSql: lConnection,
                                                          trSql: ltransaction,
                                                          oBePedidoDet: pBePedidoDet,
                                                          oBeI_nav_ped_traslado_det: pBeTrasladoDet,
                                                          Tarea_Reabasto: pTarea_Reabasto,
                                                          EsDevolucion: false,
                                                          LineNumber: No_Linea,
                                                          MachineName: MaquinaQueSolicita,
                                                          DiasVencimiento: DiasVencimiento);

                // Asignar resultados a parámetros de salida (ref)
                pListStockResOUT = reservations ?? new List<clsBeStock_res>();

                // Calcular cantidad total reservada (suma de todas las reservas)
                pCantidadDisponibleStock = 0;
                foreach (var reserva in pListStockResOUT)
                {
                    pCantidadDisponibleStock += reserva.Cantidad;
                }

                // Retornar éxito si hay reservas
                return pListStockResOUT.Count > 0;
            }
            catch
            {
                // En caso de error, inicializar salidas y retornar false
                pListStockResOUT = new List<clsBeStock_res>();
                pCantidadDisponibleStock = 0;
                return false;
            }
        }

        /// <summary>
        /// SOBRECARGA MODERNA: API simplificada sin parámetros ref (para código C# moderno).
        /// Internamente usa el mismo pipeline refactorizado que la sobrecarga legacy.
        /// </summary>
        /// <param name="oBeStockResRequest">Request de reserva con producto, bodega, cantidad, etc.</param>
        /// <param name="IdProducto">ID del producto a reservar</param>
        /// <param name="oBeConfigEnc">Configuración de navegación (bodega, explosión automática, etc.)</param>
        /// <param name="cnnSql">Conexión SQL activa</param>
        /// <param name="trSql">Transacción SQL activa (opcional)</param>
        /// <param name="oBePedidoDet">Detalle de pedido (opcional)</param>
        /// <param name="oBeI_nav_ped_traslado_det">Detalle de traslado (opcional)</param>
        /// <param name="Tarea_Reabasto">Indica si es tarea de reabasto (1=true, 0=false)</param>
        /// <param name="EsDevolucion">Indica si es una devolución</param>
        /// <param name="LineNumber">Número de línea del pedido/traslado</param>
        /// <param name="MachineName">Nombre de la máquina que solicita la reserva</param>
        /// <returns>Lista de reservas creadas</returns>
        public static List<clsBeStock_res> Reserva_Stock_From_MI3(clsBeStock_res oBeStockResRequest,
                                                                  int IdProducto,
                                                                  clsBeI_nav_config_enc oBeConfigEnc,
                                                                  SqlConnection cnnSql,
                                                                  SqlTransaction? trSql = null,
                                                                  clsBeTrans_pe_det? oBePedidoDet = null,
                                                                  clsBeI_nav_ped_traslado_det? oBeI_nav_ped_traslado_det = null,
                                                                  bool Tarea_Reabasto = false,
                                                                  bool EsDevolucion = false,
                                                                  int LineNumber = 0,
                                                                  string MachineName = "")
        {
            return Reserva_Stock_Internal(oBeStockResRequest,
                                          IdProducto,
                                          oBeConfigEnc,
                                          cnnSql,
                                          trSql,
                                          oBePedidoDet,
                                          oBeI_nav_ped_traslado_det,
                                          Tarea_Reabasto,
                                          EsDevolucion,
                                          LineNumber,
                                          MachineName);
        }

        /// <summary>
        /// Implementación interna compartida que ejecuta el pipeline de reserva.
        /// </summary>
        private static List<clsBeStock_res> Reserva_Stock_Internal(clsBeStock_res oBeStockResRequest,
                                                                   int IdProductoBodega,
                                                                   clsBeI_nav_config_enc oBeConfigEnc,
                                                                   SqlConnection cnnSql,
                                                                   SqlTransaction? trSql,
                                                                   clsBeTrans_pe_det? oBePedidoDet,
                                                                   clsBeI_nav_ped_traslado_det? oBeI_nav_ped_traslado_det,
                                                                   bool Tarea_Reabasto,
                                                                   bool EsDevolucion,
                                                                   int LineNumber,
                                                                   string MachineName,
                                                                   double DiasVencimiento = 0)
        {
            try
            {
                // Validaciones básicas
                if (oBeStockResRequest == null)
                    throw new ArgumentNullException(nameof(oBeStockResRequest), "El request de reserva no puede ser null");

                if (cnnSql == null)
                    throw new ArgumentNullException(nameof(cnnSql), "La conexión SQL no puede ser null");

                // Crear factory y pipeline
                var factory = new ServiceFactory();
                var pipeline = factory.CreateReservationPipeline();

                oBeStockResRequest.IdBodega = oBeConfigEnc.Idbodega;

                if (oBePedidoDet is null)
                    throw new Exception("PedidoDet es null: no se puede construir el contexto de reserva.");

                if (oBeI_nav_ped_traslado_det is null)
                    throw new Exception("TrasladoDet es null: no se puede construir el contexto de reserva.");

                // Construir contexto de reserva
                var context = new ReservationContext
                {
                    Request = oBeStockResRequest,
                    ProductId = IdProductoBodega,
                    Configuration = oBeConfigEnc,
                    Connection = cnnSql,
                    Transaction = trSql,
                    PedidoDet = oBePedidoDet,
                    TrasladoDet = oBeI_nav_ped_traslado_det,
                    TareaReabasto = Tarea_Reabasto ? true : false,
                    EsDevolucion = EsDevolucion,
                    LineNumber = LineNumber,
                    MachineName = MachineName ?? Environment.MachineName,
                    DiasVencimiento = DiasVencimiento,
                };

                // Ejecutar pipeline de reserva
                var result = pipeline.Execute(context);

                // Verificar resultado
                if (!result.Success)
                {
                    var errorMsg = context.HasError
                        ? context.ErrorMessage
                        : "Error desconocido en la reserva de stock";
                    throw new Exception($"Error en reserva de stock: {errorMsg}");
                }

                // Retornar reservas creadas (nunca null)
                return result.Reservations ?? new List<clsBeStock_res>();
            }
            catch (Exception ex)
            {
                // Re-lanzar con contexto adicional
                throw new Exception(
                    $"Error ejecutando Reserva_Stock_From_MI3 para producto {IdProductoBodega}: {ex.Message}",
                    ex);
            }
        }

        /// <summary>
        /// Versión simplificada para casos básicos (sin pedidos/traslados).
        /// Ideal para reservas directas desde código C#.
        /// </summary>
        /// <param name="bodegaId">ID de la bodega</param>
        /// <param name="productoId">ID del producto</param>
        /// <param name="cantidad">Cantidad a reservar</param>
        /// <param name="presentacionId">ID de la presentación (UMBas)</param>
        /// <param name="configuracion">Configuración de navegación</param>
        /// <param name="connection">Conexión SQL</param>
        /// <param name="transaction">Transacción SQL (opcional)</param>
        /// <returns>Lista de reservas creadas</returns>
        public static List<clsBeStock_res> ReservarStock(int bodegaId,
                                                        int productoId,
                                                        double cantidad,
                                                        int presentacionId,
                                                        clsBeI_nav_config_enc configuracion,
                                                        SqlConnection connection,
                                                        SqlTransaction? transaction = null)
        {
            // Construir request básico
            var request = new clsBeStock_res
            {
                IdBodega = bodegaId,
                IdProductoBodega = productoId,
                IdPresentacion = presentacionId,
                Cantidad = cantidad
            };

            // Llamar método principal
            return Reserva_Stock_From_MI3(oBeStockResRequest: request,
                                          IdProducto: productoId,
                                          oBeConfigEnc: configuracion,
                                          cnnSql: connection,
                                          trSql: transaction);
        }
    }
}