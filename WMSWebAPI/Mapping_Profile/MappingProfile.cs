using AutoMapper;
using WMS.EntityCore.AcuerdosComerciales;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Dtos.Acuerdos;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Dtos.Clientes;
using WMS.EntityCore.Dtos.Ingresos;
using WMS.EntityCore.Dtos.Pedido;
using WMS.EntityCore.Dtos.Prefactura;
using WMS.EntityCore.Dtos.Productos;
using WMS.EntityCore.Log;
using WMS.EntityCore.Operador;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Picking;
using WMS.EntityCore.Prefactura;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Producto.ProductoSimple;
using WMS.EntityCore.Propietario;
using WMS.EntityCore.Proveedor;
using WMS.EntityCore.Stock;
using WMS.EntityCore.Trans_oc;
using WMS.EntityCore.Trans_re;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Datos_Maestros;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.Log_portal_ux;
using WMSWebAPI.Dtos.Movimientos.WMSWebAPI.Dto;
using WMSWebAPI.Dtos.Operador;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Picking;
using WMSWebAPI.Dtos.Productos;
using WMSWebAPI.Dtos.Stock;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeos básicos con ReverseMap
        CreateMap<ProductoDto, clsBeProducto>().ReverseMap();
        CreateMap<ProductoParametroADto, clsBeProducto_parametro_a>().ReverseMap();
        CreateMap<ProductoParametroBDto, clsBeProducto_parametro_b>().ReverseMap();
        CreateMap<ProductoPresentacionDto, clsBeProducto_presentacion>().ReverseMap();
        CreateMap<PropietarioDto, clsBePropietarios>().ReverseMap();
        CreateMap<PropietarioBodegaDto, clsBePropietario_bodega>().ReverseMap();
        CreateMap<ProductoClasificacionDto, clsBeProducto_clasificacion>();
        CreateMap<ProductoMarcaDto, clsBeProducto_marca>();
        CreateMap<ProductoTipoDto, clsBeProducto_tipo>();
        CreateMap<ProductoFamiliaDto, clsBeProducto_familia>();
        CreateMap<OrdenCompraEncDto, clsBeTrans_oc_enc>();
        CreateMap<OrdenCompraDetDto, clsBeTrans_oc_det>()
        .ForMember(d => d.Camas_Tarima, o => o.MapFrom(s => s.LayersPallet))
        .ForMember(d => d.Cajas_Cama, o => o.MapFrom(s => s.BoxesLayer))
        .ReverseMap()
        .ForMember(d => d.LayersPallet, o => o.MapFrom(s => s.Camas_Tarima))
        .ForMember(d => d.BoxesLayer, o => o.MapFrom(s => s.Cajas_Cama));
        CreateMap<OrdenCompraPolizaDto, clsBeTrans_oc_pol>();
        CreateMap<TipoIngresoDto, clsBeTrans_oc_ti>();

        // Mapeos para Recepción con propiedades ignoradas
        CreateMap<RecepcionEncDto, clsBeTrans_re_enc>()
            .ForMember(dest => dest.DetalleImagenes, opt => opt.Ignore())
            .ForMember(dest => dest.DetalleFacturas, opt => opt.Ignore())
            .ForMember(dest => dest.Muelle, opt => opt.Ignore())
            .ForMember(dest => dest.TareaHH, opt => opt.Ignore())
            .ForMember(dest => dest.OrdenCompraRec, opt => opt.Ignore())
            .ForMember(dest => dest.Detalle, opt => opt.Ignore())
            .ForMember(dest => dest.DetalleParametros, opt => opt.Ignore())
            .ForMember(dest => dest.DetalleOperadores, opt => opt.Ignore())
            .ForMember(dest => dest.UbicacionRecepcion, opt => opt.Ignore())
            .ForMember(dest => dest.Bodega, opt => opt.Ignore())
            .ForMember(dest => dest.Usuario, opt => opt.Ignore())
            .ForMember(dest => dest.PropietarioBodega, opt => opt.Ignore())
            .ForMember(dest => dest.PropietarioOC, opt => opt.Ignore())
            .ForMember(dest => dest.Proveedor, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.DetalleImagenes, opt => opt.Ignore())
            .ForMember(dest => dest.DetalleFacturas, opt => opt.Ignore())
            .ForMember(dest => dest.Muelle, opt => opt.Ignore())
            .ForMember(dest => dest.TareaHH, opt => opt.Ignore())
            .ForMember(dest => dest.OrdenCompraRec, opt => opt.Ignore())
            .ForMember(dest => dest.Detalle, opt => opt.Ignore())
            .ForMember(dest => dest.DetalleParametros, opt => opt.Ignore())
            .ForMember(dest => dest.DetalleOperadores, opt => opt.Ignore())
            .ForMember(dest => dest.Bodega, opt => opt.Ignore())
            .ForMember(dest => dest.Usuario, opt => opt.Ignore())
            .ForMember(dest => dest.PropietarioBodega, opt => opt.Ignore())
            .ForMember(dest => dest.PropietarioOC, opt => opt.Ignore())
            .ForMember(dest => dest.Proveedor, opt => opt.Ignore());

        CreateMap<RecepcionDetDto, clsBeTrans_re_det>()
            .ForMember(dest => dest.MotivoDevolucion, opt => opt.Ignore())
            .ForMember(dest => dest.Talla, opt => opt.Ignore())
            .ForMember(dest => dest.Color, opt => opt.Ignore())
            .ForMember(dest => dest.Producto, opt => opt.Ignore())
            .ForMember(dest => dest.Presentacion, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoEstado, opt => opt.Ignore())
            .ForMember(dest => dest.UnidadMedida, opt => opt.Ignore())
            .ForMember(dest => dest.Estado_Rec, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.MotivoDevolucion, opt => opt.Ignore())
            .ForMember(dest => dest.Talla, opt => opt.Ignore())
            .ForMember(dest => dest.Color, opt => opt.Ignore())
            .ForMember(dest => dest.Producto, opt => opt.Ignore())
            .ForMember(dest => dest.Presentacion, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoEstado, opt => opt.Ignore())
            .ForMember(dest => dest.UnidadMedida, opt => opt.Ignore())
            .ForMember(dest => dest.Estado_Rec, opt => opt.Ignore());


        // Mapeos para Pedidos con propiedades ignoradas
        CreateMap<TransPeEncDto, clsBeTrans_pe_enc>()
            .ForMember(dest => dest.Picking, opt => opt.Ignore())
            .ForMember(dest => dest.IsNew, opt => opt.Ignore())
            .ForMember(dest => dest.Detalle, opt => opt.Ignore())
            .ForMember(dest => dest.PropietarioBodega, opt => opt.Ignore())
            .ForMember(dest => dest.Cliente, opt => opt.Ignore())
            .ForMember(dest => dest.TipoPedido, opt => opt.Ignore())
            .ForMember(dest => dest.ObjPoliza, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Picking, opt => opt.Ignore())
            .ForMember(dest => dest.IsNew, opt => opt.Ignore())
            .ForMember(dest => dest.Detalle, opt => opt.Ignore())
            .ForMember(dest => dest.PropietarioBodega, opt => opt.Ignore())
            .ForMember(dest => dest.Cliente, opt => opt.Ignore())
            .ForMember(dest => dest.TipoPedido, opt => opt.Ignore())
            .ForMember(dest => dest.ObjPoliza, opt => opt.Ignore());

        CreateMap<TransPeDetDto, clsBeTrans_pe_det>()
            .ForMember(dest => dest.ListaStockRes, opt => opt.Ignore())
            .ForMember(dest => dest.IsNew, opt => opt.Ignore())
            .ForMember(dest => dest.Producto, opt => opt.Ignore())
            .ForMember(dest => dest.Presentacion, opt => opt.Ignore())
            .ForMember(dest => dest.UnidadMedida, opt => opt.Ignore())
            .ForMember(dest => dest.ListaPickingUbic, opt => opt.Ignore())
            .ForMember(dest => dest.NombreProducto, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoPresentacion, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoUnidadMedida, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoEstado, opt => opt.Ignore())
            .ForMember(dest => dest.BodegaUbicacion, opt => opt.Ignore())
            .ForMember(dest => dest.CantidadFisica, opt => opt.Ignore())
            .ForMember(dest => dest.Factor, opt => opt.Ignore())
            .ForMember(dest => dest.CantidadReservada, opt => opt.Ignore())
            .ForMember(dest => dest.PesoReservado, opt => opt.Ignore())
            .ForMember(dest => dest.FechaIngreso, opt => opt.Ignore())
            .ForMember(dest => dest.FechaVence, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ListaStockRes, opt => opt.Ignore())
            .ForMember(dest => dest.IsNew, opt => opt.Ignore())
            .ForMember(dest => dest.Producto, opt => opt.Ignore())
            .ForMember(dest => dest.Presentacion, opt => opt.Ignore())
            .ForMember(dest => dest.UnidadMedida, opt => opt.Ignore())
            .ForMember(dest => dest.ListaPickingUbic, opt => opt.Ignore())
            .ForMember(dest => dest.NombreProducto, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoPresentacion, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoUnidadMedida, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoEstado, opt => opt.Ignore())
            .ForMember(dest => dest.BodegaUbicacion, opt => opt.Ignore())
            .ForMember(dest => dest.CantidadFisica, opt => opt.Ignore())
            .ForMember(dest => dest.Factor, opt => opt.Ignore())
            .ForMember(dest => dest.CantidadReservada, opt => opt.Ignore())
            .ForMember(dest => dest.PesoReservado, opt => opt.Ignore())
            .ForMember(dest => dest.FechaIngreso, opt => opt.Ignore())
            .ForMember(dest => dest.FechaVence, opt => opt.Ignore());

        // Resto de mapeos básicos
        CreateMap<TipoRecDto, clsBeTrans_re_tr>().ReverseMap();
        CreateMap<StockRecDto, clsBeStock_rec>()
            .ForMember(dest => dest.IsNew, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoValidado, opt => opt.Ignore())
            .ForMember(dest => dest.Presentacion, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoEstado, opt => opt.Ignore())
            .ForMember(dest => dest.CantidadEnStock, opt => opt.Ignore())
            .ForMember(dest => dest.PesoEnStock, opt => opt.Ignore())
            .ForMember(dest => dest.Cantidad_Nav, opt => opt.Ignore())
            .ForMember(dest => dest.IdProductoTallaColor, opt => opt.Ignore())
            .ForMember(dest => dest.Talla, opt => opt.Ignore())
            .ForMember(dest => dest.Color, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.IsNew, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoValidado, opt => opt.Ignore())
            .ForMember(dest => dest.Presentacion, opt => opt.Ignore())
            .ForMember(dest => dest.ProductoEstado, opt => opt.Ignore())
            .ForMember(dest => dest.CantidadEnStock, opt => opt.Ignore())
            .ForMember(dest => dest.PesoEnStock, opt => opt.Ignore())
            .ForMember(dest => dest.Cantidad_Nav, opt => opt.Ignore())
            .ForMember(dest => dest.IdProductoTallaColor, opt => opt.Ignore())
            .ForMember(dest => dest.Talla, opt => opt.Ignore())
            .ForMember(dest => dest.Color, opt => opt.Ignore());

        CreateMap<ProductoBodegaDto, clsBeProducto_bodega>()
            .ForMember(dest => dest.Producto, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Producto, opt => opt.Ignore());

        CreateMap<clsBeTrans_movimientos, MovimientosDto>().ReverseMap();
        CreateMap<clsBeStock, StockDto>().ReverseMap();

        CreateMap<RecepcionOcDto, clsBeTrans_re_oc>()
            .ForMember(dest => dest.IsNew, opt => opt.Ignore())
            .ForMember(dest => dest.OC, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.IsNew, opt => opt.Ignore())
            .ForMember(dest => dest.OC, opt => opt.Ignore());

        // Resto de mapeos sin cambios
        CreateMap<ProductoEstadoDto, clsBeProducto_estado>().ReverseMap();
        CreateMap<UnidadMedidaDto, clsBeUnidad_medida>();
        CreateMap<OperadorDto, clsBeOperador>();
        CreateMap<OperadorBodegaDto, clsBeOperador_bodega>();
        CreateMap<TransPeTipoDto, clsBeTrans_pe_tipo>().ReverseMap();
        CreateMap<TransPePolDto, clsBeTrans_pe_pol>().ReverseMap();
        CreateMap<PickingEncDto, clsBeTrans_picking_enc>().ReverseMap();
        CreateMap<PickingDetDto, clsBeTrans_picking_det>().ReverseMap();
        CreateMap<PickingImgDto, clsBeTrans_picking_img>().ReverseMap();
        CreateMap<PickingOpDto, clsBeTrans_picking_op>().ReverseMap();
        CreateMap<PickingPrioridadDto, clsBeTrans_picking_prioridad>().ReverseMap();
        CreateMap<PickingUbicDto, clsBeTrans_picking_ubic>().ReverseMap();
        CreateMap<PickingUbicStockDto, clsBeTrans_picking_ubic_stock>().ReverseMap();
        CreateMap<BodegaMuelleDto, clsBeBodega_muelles>().ReverseMap();
        CreateMap<ProveedorDto, clsBeProveedor>();
        CreateMap<ProveedorBodegaDto, clsBeProveedor_bodega>().ReverseMap();
        CreateMap<ClienteDto, clsBeCliente>().ReverseMap();
        CreateMap<LogPortalUxDto, clsBeLog_portal_ux>().ReverseMap();
        CreateMap<ProductoMi3Dto, clsBeProductoMi3>().ReverseMap();
        CreateMap<ProductoClasificacionMi3Dto, clsBeProducto_clasificacionSimple>().ReverseMap();
        CreateMap<ProductoMarcaMi3Dto, clsBeProducto_marcaSimple>().ReverseMap();
        CreateMap<ProductoFamiliaMi3Dto, clsBeProducto_familiaSimple>().ReverseMap();
        CreateMap<ClienteMi3Dto, clsBeClientesMi3>().ReverseMap();
        CreateMap<Producto_tipoMi3Dto, clsBeProducto_tipoMi3>().ReverseMap();
        CreateMap<UnidadMedidaMi3Dto, clsBeUnidad_medidaMi3>().ReverseMap();
        CreateMap<ProductoPresentacionMi3Dto, clsBeProducto_presentacionMi3>().ReverseMap();
        CreateMap<NavPedTrasladoRequestDto, clsBeI_nav_ped_traslado_enc>().ConvertUsing(src => src.beINavPedCompraEnc);
        CreateMap<Producto3PL_Dto, clsBeProducto_3PL>().ReverseMap();
        CreateMap<RecepcionEnc_3plDto, clsBeTrans_re_enc_3pl>().ReverseMap();
        CreateMap<RecepcionDet_3plDto, clsBeTrans_re_det_3pl>().ReverseMap();
        CreateMap<MotivoDevolucion_3plDto, clsBeMotivo_devolucion>().ReverseMap();
        CreateMap<RecepcionOperador_3plDto, clsBeTrans_re_op_3pl>().ReverseMap();
        CreateMap<TransPeDet_3plDto, clsBeTrans_pe_det_3pl>().ReverseMap();
        CreateMap<StockRes_3plDto, clsBeStock_res_3pl>().ReverseMap();
        CreateMap<PickingDet_3plDto, clsBeTrans_picking_det_3pl>().ReverseMap();
        CreateMap<PickingUbic_3plDto, clsBeTrans_picking_ubic_3pl>().ReverseMap();
        CreateMap<Stock_3plDto, clsBeStock_3pl>().ReverseMap();
        CreateMap<Cliente_3plDto, clsBeCliente_3pl>().ReverseMap();

        // Mapeo de Encabezado: DTO -> Entidad
        CreateMap<AcuerdoComercialEncDto, clsBeTrans_acuerdoscomerciales_enc>()
            .ForMember(dest => dest.IdAcuerdoEnc, opt => opt.MapFrom(src => src.IdAcuerdoEnc))
            .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
            .ForMember(dest => dest.Codigo_acuerdo, opt => opt.MapFrom(src => src.codigo_acuerdo))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.descripcion ?? string.Empty))
            .ForMember(dest => dest.Tipo_cobro, opt => opt.MapFrom(src => src.tipo_cobro ?? string.Empty))
            .ForMember(dest => dest.Cod_moneda, opt => opt.MapFrom(src => src.cod_moneda))
            .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.moneda ?? "USD"))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.estado ?? true))
            .ForMember(dest => dest.User_agr, opt => opt.MapFrom(src => src.user_agr ?? string.Empty))
            .ForMember(dest => dest.Fec_agr, opt => opt.MapFrom(src => src.fec_agr ?? DateTime.Now))
            .ForMember(dest => dest.User_mod, opt => opt.MapFrom(src => src.user_mod ?? string.Empty))
            .ForMember(dest => dest.Fec_mod, opt => opt.MapFrom(src => src.fec_mod))
            .ForMember(dest => dest.Fec_erp, opt => opt.MapFrom(src => src.fec_erp))
            .ForMember(dest => dest.Enviado_A_ERP, opt => opt.Ignore())
            .ForMember(dest => dest.Auditado, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Detalles, opt => opt.Ignore());

        // Mapeo de Encabezado: Entidad -> DTO (con detalles)
        CreateMap<clsBeTrans_acuerdoscomerciales_enc, AcuerdoComercialEncDto>()
            .ForMember(dest => dest.IdAcuerdoEnc, opt => opt.MapFrom(src => src.IdAcuerdoEnc))
            .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
            .ForMember(dest => dest.codigo_acuerdo, opt => opt.MapFrom(src => src.Codigo_acuerdo))
            .ForMember(dest => dest.descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.tipo_cobro, opt => opt.MapFrom(src => src.Tipo_cobro))
            .ForMember(dest => dest.cod_moneda, opt => opt.MapFrom(src => src.Cod_moneda))
            .ForMember(dest => dest.moneda, opt => opt.MapFrom(src => src.Moneda))
            .ForMember(dest => dest.estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.user_agr, opt => opt.MapFrom(src => src.User_agr))
            .ForMember(dest => dest.fec_agr, opt => opt.MapFrom(src => src.Fec_agr))
            .ForMember(dest => dest.user_mod, opt => opt.MapFrom(src => src.User_mod))
            .ForMember(dest => dest.fec_mod, opt => opt.MapFrom(src => src.Fec_mod))
            .ForMember(dest => dest.fec_erp, opt => opt.MapFrom(src => src.Fec_erp))
            .ForMember(dest => dest.Detalles, opt => opt.Ignore()); // Los detalles se mapean por separado

        // Mapeo de Detalle: DTO -> Entidad
        CreateMap<AcuerdoComercialDetDto, clsBeTrans_acuerdoscomerciales_det>()
            .ForMember(dest => dest.IdAcuerdoDet, opt => opt.MapFrom(src => src.IdAcuerdoDet))
            .ForMember(dest => dest.IdAcuerdoEnc, opt => opt.MapFrom(src => src.IdAcuerdoEnc))
            .ForMember(dest => dest.Codigo_producto, opt => opt.MapFrom(src => src.codigo_producto ?? string.Empty))
            .ForMember(dest => dest.Servicio, opt => opt.MapFrom(src => src.servicio ?? string.Empty))
            .ForMember(dest => dest.Nemonico, opt => opt.MapFrom(src => src.nemonico ?? string.Empty))
            .ForMember(dest => dest.Codigo_acuerdo, opt => opt.MapFrom(src => src.codigo_acuerdo))
            .ForMember(dest => dest.Correlativo_detalleacuerdo, opt => opt.MapFrom(src => src.correlativo_detalleacuerdo))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.descripcion ?? string.Empty))
            .ForMember(dest => dest.Numero_unidades, opt => opt.MapFrom(src => src.numero_unidades ?? 0))
            .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.monto ?? 0))
            .ForMember(dest => dest.Porcentaje, opt => opt.MapFrom(src => src.porcentaje ?? 0))
            .ForMember(dest => dest.Dias_eventos, opt => opt.MapFrom(src => src.dias_eventos))
            .ForMember(dest => dest.Corre_cbcatalogoproductos, opt => opt.MapFrom(src => src.corre_cbcatalogoproductos))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.estado ?? true))
            .ForMember(dest => dest.Prioridad, opt => opt.MapFrom(src => src.prioridad ?? 0))
            .ForMember(dest => dest.IdBodega, opt => opt.MapFrom(src => src.IdBodega))
            .ForMember(dest => dest.IdTipoCobro, opt => opt.MapFrom(src => src.IdTipoCobro))
            .ForMember(dest => dest.User_agr, opt => opt.MapFrom(src => src.user_agr))
            .ForMember(dest => dest.Fec_agr, opt => opt.MapFrom(src => src.fec_agr ?? DateTime.Now))
            .ForMember(dest => dest.User_mod, opt => opt.MapFrom(src => src.user_mod))
            .ForMember(dest => dest.Fec_mod, opt => opt.MapFrom(src => src.fec_mod))
            .ForMember(dest => dest.Enviado, opt => opt.Ignore())
            .ReverseMap();

        // Mapeo de Detalle: Entidad -> DTO
        CreateMap<clsBeTrans_acuerdoscomerciales_det, AcuerdoComercialDetDto>()
            .ForMember(dest => dest.IdAcuerdoDet, opt => opt.MapFrom(src => src.IdAcuerdoDet))
            .ForMember(dest => dest.IdAcuerdoEnc, opt => opt.MapFrom(src => src.IdAcuerdoEnc))
            .ForMember(dest => dest.codigo_producto, opt => opt.MapFrom(src => src.Codigo_producto))
            .ForMember(dest => dest.servicio, opt => opt.MapFrom(src => src.Servicio))
            .ForMember(dest => dest.nemonico, opt => opt.MapFrom(src => src.Nemonico))
            .ForMember(dest => dest.codigo_acuerdo, opt => opt.MapFrom(src => src.Codigo_acuerdo))
            .ForMember(dest => dest.correlativo_detalleacuerdo, opt => opt.MapFrom(src => src.Correlativo_detalleacuerdo))
            .ForMember(dest => dest.descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.numero_unidades, opt => opt.MapFrom(src => src.Numero_unidades))
            .ForMember(dest => dest.monto, opt => opt.MapFrom(src => src.Monto))
            .ForMember(dest => dest.porcentaje, opt => opt.MapFrom(src => src.Porcentaje))
            .ForMember(dest => dest.dias_eventos, opt => opt.MapFrom(src => src.Dias_eventos))
            .ForMember(dest => dest.corre_cbcatalogoproductos, opt => opt.MapFrom(src => src.Corre_cbcatalogoproductos))
            .ForMember(dest => dest.estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.prioridad, opt => opt.MapFrom(src => src.Prioridad))
            .ForMember(dest => dest.IdBodega, opt => opt.MapFrom(src => src.IdBodega))
            .ForMember(dest => dest.IdTipoCobro, opt => opt.MapFrom(src => src.IdTipoCobro))
            .ForMember(dest => dest.user_agr, opt => opt.MapFrom(src => src.User_agr))
            .ForMember(dest => dest.fec_agr, opt => opt.MapFrom(src => src.Fec_agr))
            .ForMember(dest => dest.user_mod, opt => opt.MapFrom(src => src.User_mod))
            .ForMember(dest => dest.fec_mod, opt => opt.MapFrom(src => src.Fec_mod));        

        // Mapeo para Response DTO
        CreateMap<clsBePropietarios, PropietarioResponseDto>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.IdPropietario))
            .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src.IdEmpresa))
            .ForMember(dest => dest.Contacto, opt => opt.MapFrom(src => src.Contacto))
            .ForMember(dest => dest.Nombre_Comercial, opt => opt.MapFrom(src => src.Nombre_comercial))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Activo))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
            .ForMember(dest => dest.NIT, opt => opt.MapFrom(src => src.NIT))
            .ForMember(dest => dest.Fec_Agr, opt => opt.MapFrom(src => src.Fec_agr))
            .ForMember(dest => dest.Fec_Mod, opt => opt.MapFrom(src => src.Fec_mod))
            .ForMember(dest => dest.Success, opt => opt.Ignore())
            .ForMember(dest => dest.Message, opt => opt.Ignore());

        // ============================================================
        // PREFACTURA
        // ============================================================
        // Encabezado WMS -> DTO
        CreateMap<clsBeTrans_prefactura_enc, PrefacturaPendienteDto>()
            .ForMember(dest => dest.IdPrefacturaEnc, opt => opt.MapFrom(src => src.IdTransPrefacturaEnc))
            .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.Fec_agr))
            .ForMember(dest => dest.FechaDesde, opt => opt.MapFrom(src => src.Fecha_desde))
            .ForMember(dest => dest.FechaHasta, opt => opt.MapFrom(src => src.Fecha_hasta))
            .ForMember(dest => dest.TipoCambio, opt => opt.MapFrom(src => src.Tipo_Cambio))
            .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.Observacion))

            // Estas propiedades hoy no están resueltas desde clsBeTrans_prefactura_enc.
            // Se ignoran para que AssertConfigurationIsValid() no falle.
            .ForMember(dest => dest.Nit, opt => opt.Ignore())
            .ForMember(dest => dest.IdClienteFacturar, opt => opt.Ignore())
            .ForMember(dest => dest.CodigoAcuerdo, opt => opt.Ignore())
            .ForMember(dest => dest.IdCliente, opt => opt.Ignore())
            .ForMember(dest => dest.Moneda, opt => opt.Ignore())
            .ForMember(dest => dest.Periodo, opt => opt.Ignore())
            .ForMember(dest => dest.Mercaderia, opt => opt.Ignore())

            // El detalle lo llenas aparte, o luego lo mapeas si existe colección en la entidad
            .ForMember(dest => dest.Detalle, opt => opt.Ignore());

        // Detalle WMS -> DTO
        CreateMap<clsBeTrans_prefactura_det, PrefacturaPendienteDetDto>()
            .ForMember(dest => dest.CorrelativoDetalleAcuerdo, opt => opt.MapFrom(src => src.Correlativo_detalle_acuerdo))
            .ForMember(dest => dest.CodigoProducto, opt => opt.MapFrom(src => src.Codigo_producto_acuerdo_det))
            .ForMember(dest => dest.Dias, opt => opt.MapFrom(src => src.Dias_eventos))
            .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto_Erp))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.NumeroUnidades, opt => opt.MapFrom(src => src.Numero_unidades_acuerdo_det));

        // DTO -> Entidad ERP (envío a Odoo)
        CreateMap<PrefacturaPendienteDto, clsBeTrans_prefactura_erp>()
            .ForMember(dest => dest.IdPrefacturaEnc, opt => opt.MapFrom(src => src.IdPrefacturaEnc))
            .ForMember(dest => dest.Nit, opt => opt.MapFrom(src => src.Nit))
            .ForMember(dest => dest.IdCliente_facturar, opt => opt.MapFrom(src => src.IdClienteFacturar))
            .ForMember(dest => dest.Codigo_acuerdo, opt => opt.MapFrom(src => src.CodigoAcuerdo))
            .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
            .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.Moneda))
            .ForMember(dest => dest.Periodo, opt => opt.MapFrom(src => src.Periodo))
            .ForMember(dest => dest.Mercaderia, opt => opt.MapFrom(src => src.Mercaderia))
            .ForMember(dest => dest.TipoCambio, opt => opt.MapFrom(src => src.TipoCambio))
            .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.Observaciones))
            .ForMember(dest => dest.Detalle, opt => opt.MapFrom(src => src.Detalle));

        // DTO detalle -> ERP detalle
        CreateMap<PrefacturaPendienteDetDto, clsBeTrans_prefactura_erp_det>()
            .ForMember(dest => dest.corre_cbdetacuerdosservicios, opt => opt.MapFrom(src => src.CorrelativoDetalleAcuerdo))
            .ForMember(dest => dest.codigoproducto, opt => opt.MapFrom(src => src.CodigoProducto))
            .ForMember(dest => dest.dias, opt => opt.MapFrom(src => src.Dias))
            .ForMember(dest => dest.monto, opt => opt.MapFrom(src => src.Monto));

    }  // ← Cierre del constructor MappingProfile

}  // ← Cierre de la clase MappingProfile