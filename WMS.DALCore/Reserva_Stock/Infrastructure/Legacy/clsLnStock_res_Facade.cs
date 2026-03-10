using Microsoft.Data.SqlClient;
using WMSWebAPI.Be;
using WMS.StockReservation.Compatibility;
using WMS.EntityCore.Pedido;

namespace WMS.StockReservation.Infrastructure.Legacy
{
    /// <summary>
    /// Fachada de compatibilidad EXACTA con código legacy VB.NET.
    /// FIRMA ORIGINAL VB.NET (con ByRef):
    /// Function Reserva_Stock_From_MI3(ByRef pStockResSolicitud, ByVal DiasVencimiento,
    ///                                 ByVal MaquinaQueSolicita, ByVal pBeConfigEnc,
    ///                                 ByRef pCantidadDisponibleStock, ByVal pIdPropietarioBodega,
    ///                                 ByRef pListStockResOUT, ByRef lConnection, ByRef ltransaction,
    ///                                 Optional No_Linea = 0, Optional pTarea_Reabasto = False,
    ///                                 Optional pBeTrasladoDet = Nothing) As Boolean
    /// </summary>
    public static class clsLnStock_res
    {
        /// <summary>
        /// Firma EXACTA del método original VB.NET con todos los ByRef convertidos a ref.
        /// Nota: pBeTrasladoDet puede modificarse dentro de la función (sus propiedades).
        /// </summary>
        public static bool Reserva_Stock_From_MI3(ref clsBeStock_res pStockResSolicitud,
                                                  double DiasVencimiento,
                                                  string MaquinaQueSolicita,
                                                  clsBeI_nav_config_enc pBeConfigEnc,
                                                  ref double pCantidadDisponibleStock,
                                                  int pIdPropietarioBodega,
                                                  ref List<clsBeStock_res> pListStockResOUT,
                                                  ref SqlConnection lConnection,
                                                  ref SqlTransaction ltransaction,
                                                  int No_Linea = 0,
                                                  bool pTarea_Reabasto = false,
                                                  clsBeI_nav_ped_traslado_det? pBeTrasladoDet = null)
        {
            try
            {
                // Validaciones básicas
                if (pStockResSolicitud == null || lConnection == null)
                {
                    pListStockResOUT = new List<clsBeStock_res>();
                    return false;
                }

                // Determinar el IdProducto desde el request (clsBeStock_res solo tiene IdProductoBodega)
                int idProducto = pStockResSolicitud.IdProductoBodega;

                // Llamar al facade refactorizado
                var resultList = StockReservationFacade.Reserva_Stock_From_MI3(oBeStockResRequest: pStockResSolicitud,
                                                                                IdProducto: idProducto,
                                                                                oBeConfigEnc: pBeConfigEnc,
                                                                                cnnSql: lConnection,
                                                                                trSql: ltransaction,
                                                                                oBePedidoDet: null,
                                                                                oBeI_nav_ped_traslado_det: pBeTrasladoDet,
                                                                                Tarea_Reabasto: pTarea_Reabasto,
                                                                                EsDevolucion: false,
                                                                                LineNumber: No_Linea,
                                                                                MachineName: MaquinaQueSolicita);

                // Asignar resultado
                pListStockResOUT = resultList ?? new List<clsBeStock_res>();
                
                // Calcular cantidad disponible del stock reservado (suma de todas las reservas)
                pCantidadDisponibleStock = 0;
                foreach (var reserva in pListStockResOUT)
                {
                    pCantidadDisponibleStock += reserva.Cantidad;
                }

                // Retornar éxito si hay reservas creadas
                return pListStockResOUT.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Reserva_Stock_From_MI3: {ex.Message}");
                pListStockResOUT = new List<clsBeStock_res>();
                pCantidadDisponibleStock = 0;
                return false;
            }
        }
    }
}