using AutoMapper;
using Microsoft.Data.SqlClient;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Entity.Producto;

namespace WMSWebAPI.Services
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

                    var re_op_list = dto.Recepciones == null ? new List<clsBeTrans_re_op>()
                                                      : dto.Recepciones
                                                          .Where(r => r.Operadores != null)
                                                          .SelectMany(r => r.Operadores!)
                                                          .Select(_mapper.Map<clsBeTrans_re_op>)
                                                          .ToList();

                    var re_tr_list = dto.Recepciones == null
                                   ? new List<clsBeTrans_re_tr>()
                                   : dto.Recepciones
                                       .Where(r => r.TipoRec != null)
                                       .Select(r => _mapper.Map<clsBeTrans_re_tr>(r.TipoRec))
                                       .ToList();

                    var re_stock_rec_list = dto.Recepciones == null
                                            ? new List<clsBeStock_rec>()
                                            : dto.Recepciones
                                                .Where(r => r.StockRec != null)
                                                .SelectMany(r => _mapper.Map<List<clsBeStock_rec>>(r.StockRec))
                                                .ToList();

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

                    if (oc_enc != null)
                        clsLnTrans_oc_enc.InsertarOActualizar(_configuration, oc_enc, conn, tx);

                    if (producto_bodega_list != null && producto_bodega_list.Count > 0)
                        clsLnProducto_bodega.InsertarOActualizar(_configuration, producto_bodega_list, conn, tx);

                    if (oc_det_list != null)
                        clsLnTrans_oc_det.InsertarOActualizar(_configuration, oc_det_list, conn, tx);

                    if (re_enc_list != null)
                        clsLnTrans_re_enc.InsertarOActualizar(_configuration, re_enc_list, conn, tx);

                    if (re_det_list != null)
                        clsLnTrans_re_det.InsertarOActualizar(_configuration, re_det_list, conn, tx);

                    if (re_oc_list != null && re_oc_list.Count > 0)
                        clsLnTrans_re_oc.InsertarOActualizar(_configuration, re_oc_list, conn, tx);

                    if (re_op_list != null && re_op_list.Count > 0)
                        clsLnTrans_re_op.InsertarOActualizar(_configuration, re_op_list, conn, tx);

                    if (re_tr_list != null && re_tr_list.Count > 0)
                        clsLnTrans_re_tr.InsertarOActualizar(_configuration, re_tr_list, conn, tx);

                    if (re_stock_rec_list != null && re_stock_rec_list.Count > 0)
                        clsLnStock_rec.InsertarOActualizar(_configuration, re_stock_rec_list, conn, tx);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar las órdenes de compra → " + ex.Message, ex);
            }
        }
    }
}