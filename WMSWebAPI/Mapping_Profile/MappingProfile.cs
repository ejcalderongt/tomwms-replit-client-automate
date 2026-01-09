using AutoMapper;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Dtos.Clientes;
using WMS.EntityCore.Dtos.Ingresos;
using WMS.EntityCore.Dtos.Pedido;
using WMS.EntityCore.Dtos.Productos;
using WMS.EntityCore.Log;
using WMS.EntityCore.Operador;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Picking;
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
    //public MappingProfile()
    //{
    //    CreateMap<ProductoDto, clsBeProducto>().ReverseMap();
    //    CreateMap<ProductoParametroADto, clsBeProducto_parametro_a>().ReverseMap();
    //    CreateMap<ProductoParametroBDto, clsBeProducto_parametro_b>().ReverseMap();
    //    CreateMap<ProductoPresentacionDto, clsBeProducto_presentacion>().ReverseMap();
    //    CreateMap<PropietarioDto, clsBePropietarios>().ReverseMap();
    //    CreateMap<PropietarioBodegaDto, clsBePropietario_bodega>().ReverseMap();

    //    CreateMap<ProductoClasificacionDto, clsBeProducto_clasificacion>()
    //        .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));        

    //    CreateMap<ProductoMarcaDto, clsBeProducto_marca>()
    //        .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));

    //    CreateMap<ProductoTipoDto, clsBeProducto_tipo>()
    //        .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));

    //    CreateMap<ProductoFamiliaDto, clsBeProducto_familia>()
    //        .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));

    //    CreateMap<OrdenCompraEncDto, clsBeTrans_oc_enc>().ReverseMap();        
    //    CreateMap<OrdenCompraDetDto, clsBeTrans_oc_det>().ReverseMap();
    //    CreateMap<OrdenCompraPolizaDto, clsBeTrans_oc_pol>().ReverseMap();
    //    CreateMap<TipoIngresoDto, clsBeTrans_oc_ti>().ReverseMap();
    //    CreateMap<RecepcionEncDto, clsBeTrans_re_enc>().ReverseMap();
    //    CreateMap<RecepcionDetDto, clsBeTrans_re_det>().ReverseMap();
    //    CreateMap<RecepcionOperadorDto, clsBeTrans_re_op>().ReverseMap();
    //    CreateMap<TipoRecDto, clsBeTrans_re_tr>().ReverseMap();
    //    CreateMap<StockRecDto, clsBeStock_rec>().ReverseMap();
    //    CreateMap<ProductoBodegaDto, clsBeProducto_bodega>().ReverseMap();
    //    CreateMap<clsBeTrans_movimientos, MovimientosDto>().ReverseMap();
    //    CreateMap<clsBeStock, StockDto>().ReverseMap();
    //    CreateMap<RecepcionOcDto, clsBeTrans_re_oc>().ReverseMap();
    //    CreateMap<ProductoEstadoDto, clsBeProducto_estado>().ReverseMap();
    //    CreateMap<UnidadMedidaDto, clsBeUnidad_medida>();
    //    CreateMap<OperadorDto, clsBeOperador>();
    //    CreateMap<OperadorBodegaDto, clsBeOperador_bodega>();
    //    CreateMap<TransPeTipoDto, clsBeTrans_pe_tipo>().ReverseMap();
    //    CreateMap<TransPePolDto, clsBeTrans_pe_pol>().ReverseMap();
    //    CreateMap<TransPeEncDto, clsBeTrans_pe_enc>().ReverseMap();
    //    CreateMap<TransPeDetDto, clsBeTrans_pe_det>().ReverseMap();
    //    CreateMap<PickingEncDto, clsBeTrans_picking_enc>().ReverseMap();
    //    CreateMap<PickingDetDto, clsBeTrans_picking_det>().ReverseMap();
    //    CreateMap<PickingImgDto, clsBeTrans_picking_img>().ReverseMap();
    //    CreateMap<PickingOpDto, clsBeTrans_picking_op>().ReverseMap();
    //    CreateMap<PickingPrioridadDto, clsBeTrans_picking_prioridad>().ReverseMap();
    //    CreateMap<PickingUbicDto, clsBeTrans_picking_ubic>().ReverseMap();
    //    CreateMap<PickingUbicStockDto, clsBeTrans_picking_ubic_stock>().ReverseMap();
    //    CreateMap<BodegaMuelleDto, clsBeBodega_muelles>().ReverseMap();
    //    CreateMap<ProveedorDto, clsBeProveedor>().ReverseMap();
    //    CreateMap<ProveedorBodegaDto, clsBeProveedor_bodega>().ReverseMap();
    //    CreateMap<ClienteDto, clsBeCliente>().ReverseMap();
    //    CreateMap<LogPortalUxDto,clsBeLog_portal_ux>().ReverseMap();
    //    CreateMap<ProductoMi3Dto, clsBeProductoMi3>().ReverseMap();
    //    CreateMap<ProductoClasificacionMi3Dto,clsBeProducto_clasificacionSimple>().ReverseMap();
    //    CreateMap<ProductoMarcaMi3Dto,clsBeProducto_marcaSimple>().ReverseMap();
    //    CreateMap<ProductoFamiliaMi3Dto, clsBeProducto_familiaSimple>().ReverseMap();
    //    CreateMap<ClienteMi3Dto, clsBeClientesMi3>().ReverseMap();
    //    CreateMap<Producto_tipoMi3Dto,clsBeProducto_tipoMi3>().ReverseMap();
    //    CreateMap<UnidadMedidaMi3Dto, clsBeUnidad_medidaMi3>().ReverseMap();
    //    CreateMap<ProductoPresentacionMi3Dto,clsBeProducto_presentacionMi3>().ReverseMap();

    //}
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
        CreateMap<OrdenCompraDetDto, clsBeTrans_oc_det>();
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
        CreateMap<RecepcionEnc_3plDto,clsBeTrans_re_enc_3pl >().ReverseMap();
        CreateMap<RecepcionDet_3plDto,clsBeTrans_re_det_3pl>().ReverseMap();
        CreateMap<MotivoDevolucion_3plDto, clsBeMotivo_devolucion>().ReverseMap();
        CreateMap<RecepcionOperador_3plDto, clsBeTrans_re_op_3pl >().ReverseMap();
        CreateMap<TransPeDet_3plDto, clsBeTrans_pe_det_3pl>().ReverseMap();
        //CreateMap<StockResDto, clsBeStock_res>().ReverseMap();
        CreateMap<StockRes_3plDto, clsBeStock_res_3pl>().ReverseMap();
    }
}