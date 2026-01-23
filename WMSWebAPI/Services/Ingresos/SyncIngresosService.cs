using AutoMapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WMS.EntityCore.Operador;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Proveedor;
using WMS.EntityCore.Stock;
using WMS.EntityCore.Trans_oc;
using WMS.EntityCore.Trans_re;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.WebResponseDto;


namespace WMSWebAPI.Services.Ingresos
{
    public class SyncIngresosService : ISyncIngresosService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SyncIngresosService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public void ProcesarDocumentosIngreso(List<OrdenCompraDto> listaDto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (listaDto == null || listaDto.Count == 0)
                    throw new ArgumentNullException(nameof(listaDto), "La lista de órdenes de compra no puede ser nula o vacía.");

                foreach (var dto in listaDto)
                {
                    if (dto.Encabezado == null)
                        throw new ArgumentNullException(nameof(dto.Encabezado), "OrdenCompra o su Encabezado no pueden ser nulos.");

                    if (dto.Detalle == null)
                        throw new ArgumentNullException(nameof(dto.Detalle), "Detalle de la orden de compra no puede ser nulo.");

                    if(dto.stockRec == null)
                        throw new ArgumentNullException(nameof(dto.Detalle), "El stock_rec recibido no puede ser nulo.");

                    if(dto.movimientos == null)
                        throw new ArgumentNullException(nameof(dto.Detalle), "El movimiento no puede ser nulo.");

                    if (dto.Proveedores == null)
                        throw new ArgumentException(nameof(dto.Proveedores), "Proveedores no puede ser nulo");

                    if (dto.ProveedoresBodega == null)
                        throw new ArgumentException(nameof(dto.ProveedoresBodega), "Proveedor bodega no puede ser nulo");


                    var oc_enc = _mapper.Map<clsBeTrans_oc_enc>(dto.Encabezado);
                    var oc_det_list = _mapper.Map<List<clsBeTrans_oc_det>>(dto.Detalle);
                    var re_enc_list = dto.Recepciones == null
                                      ? new List<clsBeTrans_re_enc>()
                                      : dto.Recepciones
                                      .Select(r => _mapper.Map<clsBeTrans_re_enc>(r.Encabezado))
                                      .ToList();

                    var re_oc_list = dto.Recepciones == null ? new List<clsBeTrans_re_oc>()
                                                        : dto.Recepciones
                                                            .Where(r => r.OcsRelacionadas != null)
                                                            .SelectMany(r => r.OcsRelacionadas!)
                                                            .Select(_mapper.Map<clsBeTrans_re_oc>)
                                                            .ToList();

                    var ops_list = dto.Recepciones == null ? new List<clsBeOperador>()
                                                      : dto.Recepciones
                                                          .Where(r => r.Operadores != null)
                                                          .SelectMany(r => r.Operadores!)
                                                          .Select(_mapper.Map<clsBeOperador>)
                                                          .ToList();

                    var ops_bodega_list = dto.Recepciones == null ? new List<clsBeOperador_bodega>()
                                                      : dto.Recepciones
                                                          .Where(r => r.OperadorBodega != null)
                                                          .SelectMany(r => r.OperadorBodega!)
                                                          .Select(_mapper.Map<clsBeOperador_bodega>)
                                                          .ToList();

                    var ops_rec_list = dto.Recepciones == null ? new List<clsBeTrans_re_op>()
                                                      : dto.Recepciones
                                                          .Where(r => r.OperadoresRec != null)
                                                          .SelectMany(r => r.OperadoresRec!)
                                                          .Select(_mapper.Map<clsBeTrans_re_op>)
                                                          .ToList();

                    var re_tr_list = dto.Recepciones == null
                                   ? new List<clsBeTrans_re_tr>()
                                   : dto.Recepciones
                                       .Where(r => r.TipoRec != null)
                                       .Select(r => _mapper.Map<clsBeTrans_re_tr>(r.TipoRec))
                                       .ToList();
 
                    var re_stock_rec_list = _mapper.Map<List<clsBeStock_rec>>(dto.stockRec);

                    List<clsBeTrans_re_det> re_det_list = new();
                    if (dto.Recepciones != null)
                    {
                        foreach (var recepcion in dto.Recepciones)
                        {
                            var mapeados = _mapper.Map<List<clsBeTrans_re_det>>(recepcion.Detalle);
                            re_det_list.AddRange(mapeados);
                        }
                    }

                    var producto_bodega_list = listaDto?
                                             .Where(r => r.Detalle != null)
                                             .SelectMany(r => r.Detalle!)
                                             .Where(d => d.ProductoBodega != null)
                                             .Select(d => _mapper.Map<clsBeProducto_bodega>(d.ProductoBodega))
                                             .ToList() ?? new List<clsBeProducto_bodega>();


                    var stock_list = _mapper.Map<List<clsBeStock>>(dto.stock);

                    var re_movimientos_list = _mapper.Map<List<clsBeTrans_movimientos>>(dto.movimientos);

                    var proveedores_list = _mapper.Map<List<clsBeProveedor>>(dto.Proveedores);

                    var proveedores_bodega_list = _mapper.Map<List<clsBeProveedor_bodega>>(dto.ProveedoresBodega);

                    if (proveedores_list != null && proveedores_list.Count > 0)
                        clsLnProveedor.InsertarOActualizar(proveedores_list, conn, tx);

                    if (proveedores_bodega_list != null && proveedores_bodega_list.Count > 0)
                        clsLnProveedor_bodega.InsertarOActualizar(proveedores_bodega_list, conn, tx);


                    if (ops_list != null && ops_list.Count > 0)
                        clsLnOperador.InsertarOActualizar(ops_list, conn, tx);

                    if (ops_bodega_list != null && ops_bodega_list.Count > 0)
                        clsLnOperador_bodega.InsertarOActualizar(ops_bodega_list, conn, tx);

                    if (producto_bodega_list != null && producto_bodega_list.Count > 0)
                        clsLnProducto_bodega.InsertarOActualizar(producto_bodega_list, conn, tx);

                    if (oc_enc != null)
                        clsLnTrans_oc_enc.InsertarOActualizar(oc_enc, conn, tx);

                    if (oc_det_list != null)
                        clsLnTrans_oc_det.InsertarOActualizar(oc_det_list, conn, tx);

                    if (re_enc_list != null)
                        clsLnTrans_re_enc.InsertarOActualizar(re_enc_list, conn, tx);

                    if (re_det_list != null)
                        clsLnTrans_re_det.InsertarOActualizar(re_det_list, conn, tx);

                    if (re_oc_list != null && re_oc_list.Count > 0)
                        clsLnTrans_re_oc.InsertarOActualizar(re_oc_list, conn, tx);

                    if (ops_rec_list != null && ops_rec_list.Count > 0)
                        clsLnTrans_re_op.InsertarOActualizar(ops_rec_list, conn, tx);

                    if (re_tr_list != null && re_tr_list.Count > 0)
                        clsLnTrans_re_tr.InsertarOActualizar(re_tr_list, conn, tx);

                    if (re_stock_rec_list != null && re_stock_rec_list.Count > 0)
                        clsLnStock_rec.InsertarOActualizar(re_stock_rec_list, conn, tx);

                    if(stock_list!=null && stock_list.Count > 0)
                        clsLnStock.InsertarOActualizar(stock_list, conn, tx);

                    if (re_movimientos_list != null && re_movimientos_list.Count > 0)
                        clsLnTrans_movimientos.InsertarOActualizar(re_movimientos_list, conn, tx);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar las órdenes de compra → " + ex.Message, ex);
            }
        }
        public List<clsBeVWOrdenCompra> ObtenerDocumentosDeIngreso(bool activo, DateTime fechaInicio, DateTime fechaFin, int idBodega, int idPropietario)
        {
            try
            {                
                return clsLnTrans_oc_enc.GetAll(_configuration, activo, fechaInicio, fechaFin, idBodega, idPropietario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los documentos de ingreso → " + ex.Message, ex);
            }
        }
        public List<clsBeTrans_oc_det> ObtenerDetalleOrdenCompra(int IdOrdenCompraEnc)
        {
            var detalles = new List<clsBeTrans_oc_det>();

            try
            {

                detalles = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(_configuration,IdOrdenCompraEnc);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el detalle de la orden de compra: {ex.Message}", ex);
            }
           
            return detalles;
        }
        public List<ReEncWebResponseDto> ObtenerDetalleRecepcion(int IdOrdenCompraEnc)
        {
            try
            {
                List<ReEncWebResponseDto> detalles = clsLnTrans_re_det.Get_Detalle_Rec_By_IdOrdenCompraEnc(_configuration, IdOrdenCompraEnc);
                return detalles;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el detalle de la orden de compra: {ex.Message}", ex);
            }
        }
        public int Insert(clsBeI_nav_ped_compra_enc beINavPedCompraEnc)
        {
            try
            {
                // 1) Validación
                if (!clsLnI_nav_ped_compra_enc.Datos_Validos(beINavPedCompraEnc))
                    throw new Exception("Error de validación de datos.");

                // 2) Insert a tabla intermedia
                if (clsLnI_nav_ped_compra_enc.Insert_Single_Pedido_From_ERP(_configuration, beINavPedCompraEnc) <= 0)
                    throw new Exception("No se pudo insertar el pedido en la tabla intermedia.");

                // 2.5) obtener Pedido de NavCompra
                //clsBeTrans_pe_enc? pPedido = new clsBeTrans_pe_enc();
                //pPedido.IdPedidoEnc = beINavPedCompraEnc.No_Document_Wms;
                //if (!clsLnTrans_pe_enc.GetSingle(_configuration, ref pPedido)){
                //    pPedido = null;
                //}

                // 3) Procesar MI3
                var bePedidoCompraEnc = new clsBeTrans_oc_enc();
                string vResult = string.Empty;

                bool ok = clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(_configuration, 
                                                                               ref beINavPedCompraEnc,
                                                                               ref bePedidoCompraEnc,
                                                                               ref vResult,
                                                                               null);

                if (!ok)
                    throw new Exception(string.IsNullOrWhiteSpace(vResult)
                        ? "Error al procesar el pedido en MI3."
                        : vResult);

                return 1;
            }            
            catch (Exception)
            {
                throw; // propaga para que el controller lo maneje y responda 500 + mensaje
            }
        }
    }
}