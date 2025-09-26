using AutoMapper;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Log;
using WMS.EntityCore.Operador;
using WMS.EntityCore.Pedido;
using WMS.EntityCore.Picking;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Propietario;
using WMS.EntityCore.Proveedor;
using WMS.EntityCore.Stock;
using WMS.EntityCore.Trans_oc;
using WMS.EntityCore.Trans_re;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Datos_Maestros;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.Movimientos.WMSWebAPI.Dto;
using WMSWebAPI.Dtos.Operador;
using WMSWebAPI.Dtos.Pedido;
using WMSWebAPI.Dtos.Picking;
using WMSWebAPI.Dtos.Productos;
using WMSWebAPI.Dtos.Stock;
using WMSWebAPI.Dtos.Log_portal_ux;
using WMS.EntityCore.Producto.ProductoSimple;
using WMS.EntityCore.Dtos.Catalogos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductoDto, clsBeProducto>().ReverseMap();
        CreateMap<ProductoParametroADto, clsBeProducto_parametro_a>().ReverseMap();
        CreateMap<ProductoParametroBDto, clsBeProducto_parametro_b>().ReverseMap();
        CreateMap<ProductoPresentacionDto, clsBeProducto_presentacion>().ReverseMap();
        CreateMap<PropietarioDto, clsBePropietarios>().ReverseMap();
        CreateMap<PropietarioBodegaDto, clsBePropietario_bodega>().ReverseMap();

        CreateMap<ProductoClasificacionDto, clsBeProducto_clasificacion>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));        

        CreateMap<ProductoMarcaDto, clsBeProducto_marca>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));

        CreateMap<ProductoTipoDto, clsBeProducto_tipo>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));

        CreateMap<ProductoFamiliaDto, clsBeProducto_familia>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));

        CreateMap<OrdenCompraEncDto, clsBeTrans_oc_enc>().ReverseMap();
        CreateMap<OrdenCompraDetDto, clsBeTrans_oc_det>().ReverseMap();
        CreateMap<OrdenCompraPolizaDto, clsBeTrans_oc_pol>().ReverseMap();
        CreateMap<TipoIngresoDto, clsBeTrans_oc_ti>().ReverseMap();
        CreateMap<RecepcionEncDto, clsBeTrans_re_enc>().ReverseMap();
        CreateMap<RecepcionDetDto, clsBeTrans_re_det>().ReverseMap();
        CreateMap<RecepcionOperadorDto, clsBeTrans_re_op>().ReverseMap();
        CreateMap<TipoRecDto, clsBeTrans_re_tr>().ReverseMap();
        CreateMap<StockRecDto, clsBeStock_rec>().ReverseMap();
        CreateMap<ProductoBodegaDto, clsBeProducto_bodega>().ReverseMap();
        CreateMap<clsBeTrans_movimientos, MovimientosDto>().ReverseMap();
        CreateMap<clsBeStock, StockDto>().ReverseMap();
        CreateMap<RecepcionOcDto, clsBeTrans_re_oc>().ReverseMap();
        CreateMap<ProductoEstadoDto, clsBeProducto_estado>().ReverseMap();
        CreateMap<UnidadMedidaDto, clsBeUnidad_medida>();
        CreateMap<OperadorDto, clsBeOperador>();
        CreateMap<OperadorBodegaDto, clsBeOperador_bodega>();
        CreateMap<TransPeTipoDto, clsBeTrans_pe_tipo>().ReverseMap();
        CreateMap<TransPePolDto, clsBeTrans_pe_pol>().ReverseMap();
        CreateMap<TransPeEncDto, clsBeTrans_pe_enc>().ReverseMap();
        CreateMap<TransPeDetDto, clsBeTrans_pe_det>().ReverseMap();
        CreateMap<PickingEncDto, clsBeTrans_picking_enc>().ReverseMap();
        CreateMap<PickingDetDto, clsBeTrans_picking_det>().ReverseMap();
        CreateMap<PickingImgDto, clsBeTrans_picking_img>().ReverseMap();
        CreateMap<PickingOpDto, clsBeTrans_picking_op>().ReverseMap();
        CreateMap<PickingPrioridadDto, clsBeTrans_picking_prioridad>().ReverseMap();
        CreateMap<PickingUbicDto, clsBeTrans_picking_ubic>().ReverseMap();
        CreateMap<PickingUbicStockDto, clsBeTrans_picking_ubic_stock>().ReverseMap();
        CreateMap<BodegaMuelleDto, clsBeBodega_muelles>().ReverseMap();
        CreateMap<ProveedorDto, clsBeProveedor>().ReverseMap();
        CreateMap<ProveedorBodegaDto, clsBeProveedor_bodega>().ReverseMap();
        CreateMap<ClienteDto, clsBeCliente>().ReverseMap();
        CreateMap<LogPortalUxDto,clsBeLog_portal_ux>().ReverseMap();
        CreateMap<ProductoSimpleDto, clsBeProductoSimple>().ReverseMap();
        CreateMap<ProductoClasificacionSimpleDto,clsBeProducto_clasificacionSimple>().ReverseMap();
        CreateMap<ProductoMarcaSimpleDto,clsBeProducto_marcaSimple>().ReverseMap();
        CreateMap<ProductoFamiliaSimpleDto, clsBeProducto_familiaSimple>().ReverseMap();
        
    }
}