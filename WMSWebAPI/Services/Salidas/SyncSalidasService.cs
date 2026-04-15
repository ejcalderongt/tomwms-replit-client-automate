using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.DALCore;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Despacho;
using WMS.EntityCore.Operador;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Picking;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Salidas;


namespace WMSWebAPI.Services.Salidas
{
    public class SyncSalidasService : ISyncSalidasService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;        
        public SyncSalidasService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public void ProcesarSalidaDesdeDto(SalidaTransDto dto, SqlConnection conn, SqlTransaction tx)
        {


            try
            {
                if (dto.Encabezado != null && dto.Encabezado.IdBodega != 0)
                {
                    var pedido = _mapper.Map<clsBeTrans_pe_enc>(dto.Encabezado);
                    if (pedido != null)
                    {
                        if (!clsLnBodega.Existe(pedido.IdBodega, conn, tx))
                        {
                            throw new Exception($"La bodega {pedido.IdBodega} no existe.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                if (dto.Cliente != null  && dto.Cliente.Any())
                {
                    var clientes = _mapper.Map<List<clsBeCliente>>(dto.Cliente);
                    clsLnCliente.InsertarOActualizar(clientes, conn, tx);
                }

            }
            catch (Exception ex) 
            {
                throw new Exception("Error al procesar Cliente → " + ex.Message, ex);
            }


            try
            {
                if (dto.TipoPedido != null && dto.TipoPedido.IdTipoPedido != 0)
                {
                    var tipo = _mapper.Map<clsBeTrans_pe_tipo>(dto.TipoPedido);
                    clsLnTrans_pe_tipo.InsertOrUpdate(_configuration, tipo, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Tipo de Pedido → " + ex.Message, ex);
            }

            try
            {
                if (dto.BodegaMuelle != null && dto.BodegaMuelle.IdMuelle != 0)
                {
                    var bodega_muelle = _mapper.Map<clsBeBodega_muelles>(dto.BodegaMuelle);
                    clsLnBodega_muelles.InsertOrUpdate(_configuration, bodega_muelle, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Muelle→ " + ex.Message, ex);
            }
            
            try
            {
                if (dto.Operadores != null && dto.Operadores.Any())
                {
                    var operador_list = _mapper.Map<List<clsBeOperador>>(dto.Operadores);
                    clsLnOperador.InsertarOActualizar(operador_list, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Operador→ " + ex.Message, ex);
            }

            try
            {
                if (dto.OperadorBodega != null && dto.OperadorBodega.Any())
                {
                    var operador_bodega_list = _mapper.Map<List<clsBeOperador_bodega>>(dto.OperadorBodega);
                    clsLnOperador_bodega.InsertarOActualizar(operador_bodega_list, conn, tx);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al procesar Operador_Bodega→ " + ex.Message, ex);
            }

            try
            {
                if (dto.Encabezado != null && dto.Encabezado.IdPedidoEnc != 0)
                {
                    var enc = _mapper.Map<clsBeTrans_pe_enc>(dto.Encabezado);
                    
                    clsLnTrans_pe_enc.InsertOrUpdate(enc, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Pedido Encabezado → " + ex.Message, ex);
            }

            try
            {
                if (dto.Detalle != null && dto.Detalle.Any())
                {
                    var detalle = _mapper.Map<List<clsBeTrans_pe_det>>(dto.Detalle);
                    clsLnTrans_pe_det.InsertOrUpdate(detalle, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Pedido Detalle → " + ex.Message, ex);
            }

            try
            {
                if (dto.Poliza != null && dto.Poliza.Any())
                {
                    var polizas = _mapper.Map<List<clsBeTrans_pe_pol>>(dto.Poliza);
                    clsLnTrans_pe_pol.InsertOrUpdate(_configuration, polizas, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Polizas de Pedido → " + ex.Message, ex);
            }
            
            if (dto.Picking != null)
            {
                try
                {
                    if (dto.Picking.Encabezado != null && dto.Picking.Encabezado.IdPickingEnc != 0)
                    {
                        var pickingEnc = _mapper.Map<clsBeTrans_picking_enc>(dto.Picking.Encabezado);
                        clsLnTrans_picking_enc.InsertOrUpdate(_configuration, pickingEnc, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Picking Encabezado → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.Detalle != null && dto.Picking.Detalle.Any())
                    {
                        var pickingDet = _mapper.Map<List<clsBeTrans_picking_det>>(dto.Picking.Detalle);
                        clsLnTrans_picking_det.InsertOrUpdate(pickingDet, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Picking Detalles → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.PickingUbic != null && dto.Picking.PickingUbic.Any())
                    {
                        var pickingUbic = _mapper.Map<List<clsBeTrans_picking_ubic>>(dto.Picking.PickingUbic);
                        clsLnTrans_picking_ubic.InsertOrUpdate(pickingUbic, conn, tx);
                    }
                    { }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Ubicaciones de Picking → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.PickingUbicStock != null && dto.Picking.PickingUbicStock.Any())
                    {
                        var pickingUbicStock = _mapper.Map<List<clsBeTrans_picking_ubic_stock>>(dto.Picking.PickingUbicStock);
                        clsLnTrans_picking_ubic_stock.InsertOrUpdate(pickingUbicStock, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Ubicaciones de Stock de Picking → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.PickingImg != null && dto.Picking.PickingImg.Any())
                    {                        
                        var pickingImg = _mapper.Map<List<clsBeTrans_picking_img>>(dto.Picking.PickingImg);
                        clsLnTrans_picking_img.InsertOrUpdate(_configuration, pickingImg, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Imágenes de Picking → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.PickingOperadores != null && dto.Picking.PickingOperadores.Any())
                    {
                        var pickingOp = _mapper.Map<List<clsBeTrans_picking_op>>(dto.Picking.PickingOperadores);
                        clsLnTrans_picking_op.InsertOrUpdate(_configuration, pickingOp, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Operadores de Picking → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.Prioridad != null && dto.Picking.Prioridad.IdPrioridadPicking != 0)
                    {
                        var pickingPri = _mapper.Map<clsBeTrans_picking_prioridad>(dto.Picking.Prioridad);
                        clsLnTrans_picking_prioridad.InsertOrUpdate(_configuration, pickingPri, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Prioridad de Picking → " + ex.Message, ex);
                }
            }
        }
        public List<PedidoSalidaDto> ObtenerDocumentosDeSalida(bool activo, DateTime fechaInicio, DateTime fechaFin, int idBodega, int idPropietario)
        {
            try
            {
                return clsLnTrans_pe_enc.GetAllPedidosSalida(_configuration, activo, fechaInicio, fechaFin, idBodega, idPropietario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los documentos de salida → " + ex.Message, ex);
            }
        }
        public List<clsBeTrans_pe_det> ObtenerDetallePedido(int IdOrdenCompraEnc)
        {
            var detalles = new List<clsBeTrans_pe_det>();

            try
            {

                detalles = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(_configuration, IdOrdenCompraEnc);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el detalle de la orden de compra: {ex.Message}", ex);
            }

            return detalles;
        }
        public List<clsBeTrans_despacho_enc> ObtenerDespachos(int idPedidoEnc, SqlConnection? connection, SqlTransaction? transaction)
        {
            var detalles = new List<clsBeTrans_despacho_enc>();

            try
            {
                detalles = clsLnTrans_despacho_enc.Get_All_By_IdPedidoEnc(_configuration, idPedidoEnc);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el detalle de la orden de compra: {ex.Message}", ex);
            }

            return detalles;
        }
        public MI3ProcessingResultDto Insert_salida_mi3(clsBeI_nav_ped_traslado_enc BeInavPedSalida)
        {
            var result = new MI3ProcessingResultDto();
            string resultado = string.Empty;

            try
            {
                if (!Datos_Validos(_configuration, BeInavPedSalida))
                {
                    result.Exito = false;
                    result.Mensaje = "Datos no válidos";
                    return result;
                }

                clsBeTrans_pe_enc? BePedidoEnc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(
                    BeInavPedSalida, ref resultado, _configuration);

                if (BePedidoEnc == null)
                {
                    result.Exito = false;
                    result.Mensaje = !string.IsNullOrEmpty(resultado) ? resultado : "Error al procesar el documento";
                    result.TotalSolicitado = BeInavPedSalida?.lineas_Detalle?.Sum(l => l.Quantity) ?? 0;
                    result.TotalReservado = 0;
                    return result;
                }

                int cantLineas = clsLnTrans_pe_det.Get_Count_Lines_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc, _configuration);
                
                if (cantLineas == 0)
                {
                    result.Exito = false;
                    result.Mensaje = "No se procesaron líneas. Verifique stock disponible.";
                    result.TotalSolicitado = BeInavPedSalida?.lineas_Detalle?.Sum(l => l.Quantity) ?? 0;
                    result.TotalReservado = 0;
                    return result;
                }

                result.Exito = true;
                result.Mensaje = "Documento MI3 procesado correctamente.";
                result.LineasProcesadas = cantLineas;
                result.Resultado = resultado;

                var connectionString = _configuration.GetConnectionString("WMSConnection") 
                    ?? _configuration.GetConnectionString("DefaultConnection");
                    
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                result.LineasDetalle = ObtenerDetallesReserva(BePedidoEnc.IdPedidoEnc, conn, null);
                
                result.TotalSolicitado = result.LineasDetalle.Sum(l => l.QuantityRequested);
                result.TotalReservado = result.LineasDetalle.Sum(l => l.QuantityReserved);
            }
            catch (Exception ex)
            {
                result.Exito = false;
                result.Mensaje = ex.Message;
                result.LineasFallo = BuildLineasFallo(BeInavPedSalida, ex.Message);
            }

            return result;
        }

        private static List<LineaFalloDto> BuildLineasFallo(
            clsBeI_nav_ped_traslado_enc documento, string exceptionMessage)
        {
            var lista = new List<LineaFalloDto>();
            if (documento?.lineas_Detalle == null) return lista;

            foreach (var linea in documento.lineas_Detalle)
            {
                var fallo = new LineaFalloDto
                {
                    NoLinea         = linea.Line_No,
                    CodigoProducto  = linea.Item_No ?? string.Empty,
                    Variante        = linea.Variant_Code ?? string.Empty,
                    UnidadMedida    = linea.Unit_of_Measure_Code ?? string.Empty,
                    Solicitado      = linea.Quantity,
                    Reservado       = 0,
                    Estado          = "Error",
                    RazonFallo      = ExtractLineFailureReason(linea.Line_No, exceptionMessage)
                };
                lista.Add(fallo);
            }

            return lista;
        }

        private static string ExtractLineFailureReason(int lineNo, string exceptionMessage)
        {
            if (string.IsNullOrWhiteSpace(exceptionMessage))
                return "No se pudo reservar stock";

            var lineMarker = $"línea {lineNo}:";
            var idx = exceptionMessage.IndexOf(lineMarker, StringComparison.OrdinalIgnoreCase);
            if (idx < 0)
                return "No se pudo reservar stock";

            var segment = exceptionMessage.Substring(idx);

            var razonIdx = segment.IndexOf("Razón:", StringComparison.OrdinalIgnoreCase);
            if (razonIdx < 0)
                return "No se pudo reservar stock";

            var razonText = segment.Substring(razonIdx + 6).Trim();
            var endIdx = razonText.IndexOf(';');
            return endIdx >= 0 ? razonText.Substring(0, endIdx).Trim() : razonText.Trim();
        }

        private List<ReservationLineDetailDto> ObtenerDetallesReserva(int idPedidoEnc, SqlConnection conn, SqlTransaction? tx)
        {
            try
            {
                var rows = clsLnTrans_pe_det.Get_Detalles_Reserva_By_IdPedidoEnc(idPedidoEnc, conn, tx);

                var lineDict = new Dictionary<int, ReservationLineDetailDto>();

                foreach (var row in rows)
                {
                    if (!lineDict.TryGetValue(row.NoLinea, out var lineDetail))
                    {
                        lineDetail = new ReservationLineDetailDto
                        {
                            LineNo            = row.NoLinea,
                            ProductCode       = row.ProductCode,
                            ProductName       = row.ProductName,
                            QuantityRequested = row.QuantityRequested,
                            Reservations      = new List<ReservationDetailDto>()
                        };
                        lineDict[row.NoLinea] = lineDetail;
                    }

                    if (row.IdStockRes > 0)
                    {
                        double factor = row.Factor > 0 ? row.Factor : 1;
                        double quantityInRequestedUnit = Math.Round(row.ReservationQty / factor, 6);

                        lineDetail.Reservations.Add(new ReservationDetailDto
                        {
                            IdStockRes     = row.IdStockRes,
                            IdStock        = row.IdStock,
                            LotNo          = row.LotNo,
                            ExpirationDate = row.ExpirationDate == DateTime.MinValue ? "" : row.ExpirationDate.ToString("yyyy-MM-dd"),
                            LocationCode   = row.LocationCode,
                            Zone           = row.Zone,
                            Quantity       = quantityInRequestedUnit
                        });
                    }
                }

                foreach (var line in lineDict.Values)
                {
                    line.QuantityReserved = line.Reservations.Sum(r => r.Quantity);
                    line.Status = line.QuantityReserved >= line.QuantityRequested - 0.001
                        ? "Completa"
                        : line.QuantityReserved > 0
                            ? "Parcial"
                            : "Sin reserva";
                }

                return lineDict.Values.OrderBy(l => l.LineNo).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error obteniendo detalles de reserva: {ex.Message}");
                return new List<ReservationLineDetailDto>();
            }
        }

                private bool Datos_Validos(IConfiguration config, clsBeI_nav_ped_traslado_enc BeINavPedClienteEnc)
        {
            bool Datos_Validos = false;

            try
            {
                if (BeINavPedClienteEnc.lineas_Detalle == null)
                {
                    throw new Exception("No se proporcionó el detalle del documento");
                }
                else if (BeINavPedClienteEnc.lineas_Detalle.Count == 0)
                {
                    throw new Exception("No se proporcionó el detalle del documento");
                }
                else if (string.IsNullOrEmpty(BeINavPedClienteEnc.No))
                {
                    throw new Exception("El número de documento no puede ser vacío ");
                }
                else if (clsLnI_nav_ped_traslado_enc.Exist(config,BeINavPedClienteEnc.No))
                {
                    throw new Exception($"El número de documento: {BeINavPedClienteEnc.No} ya existe.");
                }
                else if (string.IsNullOrEmpty(BeINavPedClienteEnc.Product_Owner_Code))
                {
                    throw new Exception("El campo Producto_Owner_Code no puede ser vacío, este valor corresponde al codigo de propietario tabla -> propietarios ");
                }
                else
                {
                    Datos_Validos = true;
                }
            }         
            catch (Exception)
            {
                throw;
            }

            return Datos_Validos;
        }

        public void ProcesarSalidaDesde_3plDto(SalidaTrans_3plDto dto, SqlConnection conn, SqlTransaction tx)
        {

            try
            {
                if (dto.Encabezado != null && dto.Encabezado.IdBodega != 0)
                {
                    var pedido = _mapper.Map<clsBeTrans_pe_enc>(dto.Encabezado);
                    if (pedido != null)
                    {
                        if (!clsLnBodega.Existe(pedido.IdBodega, conn, tx))
                        {
                            throw new Exception($"La bodega {pedido.IdBodega} no existe.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                if (dto.Clientes != null && dto.Clientes.Any())
                {
                    var clientes = _mapper.Map<List<clsBeCliente>>(dto.Clientes);
                    clsLnCliente.InsertarOActualizar(clientes, conn, tx);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Cliente → " + ex.Message, ex);
            }


            try
            {
                if (dto.TipoPedido != null && dto.TipoPedido.IdTipoPedido != 0)
                {
                    var tipo = _mapper.Map<clsBeTrans_pe_tipo>(dto.TipoPedido);
                    clsLnTrans_pe_tipo.InsertOrUpdate(_configuration, tipo, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Tipo de Pedido → " + ex.Message, ex);
            }

            try
            {
                if (dto.BodegaMuelle != null && dto.BodegaMuelle.IdMuelle != 0)
                {
                    var bodega_muelle = _mapper.Map<clsBeBodega_muelles>(dto.BodegaMuelle);
                    clsLnBodega_muelles.InsertOrUpdate(_configuration, bodega_muelle, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Muelle→ " + ex.Message, ex);
            }

            try
            {
                if (dto.Operadores != null && dto.Operadores.Any())
                {
                    var operador_list = _mapper.Map<List<clsBeOperador>>(dto.Operadores);
                    clsLnOperador.InsertarOActualizar(operador_list, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Operador→ " + ex.Message, ex);
            }

            try
            {
                if (dto.OperadorBodega != null && dto.OperadorBodega.Any())
                {
                    var operador_bodega_list = _mapper.Map<List<clsBeOperador_bodega>>(dto.OperadorBodega);
                    clsLnOperador_bodega.InsertarOActualizar(operador_bodega_list, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Operador_Bodega→ " + ex.Message, ex);
            }

            try
            {
                if (dto.Encabezado != null && dto.Encabezado.IdPedidoEnc != 0)
                {
                    var enc = _mapper.Map<clsBeTrans_pe_enc>(dto.Encabezado);

                    clsLnTrans_pe_enc.InsertOrUpdate(enc, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Pedido Encabezado → " + ex.Message, ex);
            }

            try
            {
                if (dto.Detalle != null && dto.Detalle.Any())
                {
                    //var detalle = _mapper.Map<List<clsBeTrans_pe_det>>(dto.Detalle);
                    var detalle = _mapper.Map<List<clsBeTrans_pe_det_3pl>>(dto.Detalle);
                    clsLnTrans_pe_det.InsertOrUpdate_3pl(detalle, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Pedido Detalle → " + ex.Message, ex);
            }

            try
            {
                if (dto.Poliza != null && dto.Poliza.Any())
                {
                    var polizas = _mapper.Map<List<clsBeTrans_pe_pol>>(dto.Poliza);
                    clsLnTrans_pe_pol.InsertOrUpdate(_configuration, polizas, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Polizas de Pedido → " + ex.Message, ex);
            }

            if (dto.Picking != null)
            {
                try
                {
                    if (dto.Picking.Encabezado != null && dto.Picking.Encabezado.IdPickingEnc != 0)
                    {
                        var pickingEnc = _mapper.Map<clsBeTrans_picking_enc>(dto.Picking.Encabezado);
                        clsLnTrans_picking_enc.InsertOrUpdate(_configuration, pickingEnc, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Picking Encabezado → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.Detalle != null && dto.Picking.Detalle.Any())
                    {
                        var pickingDet = _mapper.Map<List<clsBeTrans_picking_det_3pl>>(dto.Picking.Detalle);
                        //clsLnTrans_picking_det.InsertOrUpdate(pickingDet, conn, tx);
                        clsLnTrans_picking_det.InsertOrUpdate_3pl(pickingDet, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Picking Detalles → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.PickingUbic != null && dto.Picking.PickingUbic.Any())
                    {
                        var pickingUbic = _mapper.Map<List<clsBeTrans_picking_ubic_3pl>>(dto.Picking.PickingUbic);
                        //clsLnTrans_picking_ubic.InsertOrUpdate(pickingUbic, conn, tx);
                        clsLnTrans_picking_ubic.InsertOrUpdate_3pl(pickingUbic, conn, tx);
                    }
                    { }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Ubicaciones de Picking → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.PickingUbicStock != null && dto.Picking.PickingUbicStock.Any())
                    {
                        var pickingUbicStock = _mapper.Map<List<clsBeTrans_picking_ubic_stock>>(dto.Picking.PickingUbicStock);
                        clsLnTrans_picking_ubic_stock.InsertOrUpdate(pickingUbicStock, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Ubicaciones de Stock de Picking → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.PickingImg != null && dto.Picking.PickingImg.Any())
                    {
                        var pickingImg = _mapper.Map<List<clsBeTrans_picking_img>>(dto.Picking.PickingImg);
                        clsLnTrans_picking_img.InsertOrUpdate(_configuration, pickingImg, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Imágenes de Picking → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.PickingOperadores != null && dto.Picking.PickingOperadores.Any())
                    {
                        var pickingOp = _mapper.Map<List<clsBeTrans_picking_op>>(dto.Picking.PickingOperadores);
                        clsLnTrans_picking_op.InsertOrUpdate(_configuration, pickingOp, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Operadores de Picking → " + ex.Message, ex);
                }

                try
                {
                    if (dto.Picking.Prioridad != null && dto.Picking.Prioridad.IdPrioridadPicking != 0)
                    {
                        var pickingPri = _mapper.Map<clsBeTrans_picking_prioridad>(dto.Picking.Prioridad);
                        clsLnTrans_picking_prioridad.InsertOrUpdate(_configuration, pickingPri, conn, tx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al procesar Prioridad de Picking → " + ex.Message, ex);
                }
            }

        }
    }
}