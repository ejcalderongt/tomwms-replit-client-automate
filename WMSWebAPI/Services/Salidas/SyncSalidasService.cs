using AutoMapper;
using Microsoft.Data.SqlClient;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Salidas;
using WMSWebAPI.Entity.Despacho;


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
                    clsLnCliente.InsertarOActualizar(_configuration, clientes, conn, tx);
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
                    clsLnOperador.InsertarOActualizar(_configuration, operador_list, conn, tx);
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
                    clsLnOperador_bodega.InsertarOActualizar(_configuration, operador_bodega_list, conn, tx);
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
                    
                    clsLnTrans_pe_enc.InsertOrUpdate(_configuration, enc, conn, tx);
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
                    clsLnTrans_pe_det.InsertOrUpdate(_configuration, detalle, conn, tx);
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
                        clsLnTrans_picking_det.InsertOrUpdate(_configuration, pickingDet, conn, tx);
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
                        clsLnTrans_picking_ubic.InsertOrUpdate(_configuration, pickingUbic, conn, tx);
                    }
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
                        clsLnTrans_picking_ubic_stock.InsertOrUpdate(_configuration, pickingUbicStock, conn, tx);
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
    }
}