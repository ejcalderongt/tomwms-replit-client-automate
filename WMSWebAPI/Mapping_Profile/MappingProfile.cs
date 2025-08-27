using AutoMapper;
using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Ingresos;
using WMSWebAPI.Dtos.Stock;
using WMSWebAPI.Entity.Producto;
using WMSWebAPI.Entity.Propietario;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductoDto, clsBeProducto>();
        CreateMap<ProductoParametroADto, clsBeProducto_parametro_a>();
        CreateMap<ProductoParametroBDto, clsBeProducto_parametro_b>();
        CreateMap<ProductoPresentacionDto, clsBeProducto_presentacion>();
        CreateMap<PropietarioDto, clsBePropietarios>();
        CreateMap<PropietarioBodegaDto, clsBePropietario_bodega>();
        CreateMap<ProductoClasificacionDto, clsBeProducto_clasificacion>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));
        CreateMap<ProductoMarcaDto, clsBeProducto_marca>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));
        CreateMap<ProductoTipoDto, clsBeProducto_tipo>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));
        CreateMap<ProductoFamiliaDto, clsBeProducto_familia>()
            .ForMember(dest => dest.IdPropietario, opt => opt.MapFrom(src => src.Propietario.IdPropietario));
        CreateMap<OrdenCompraEncDto, clsBeTrans_oc_enc>();
        CreateMap<OrdenCompraDetDto, clsBeTrans_oc_det>();
        CreateMap<OrdenCompraPolizaDto, clsBeTrans_oc_pol>();
        CreateMap<TipoIngresoDto, clsBeTrans_oc_ti>();
        CreateMap<RecepcionEncDto, clsBeTrans_re_enc>().ReverseMap();
        CreateMap<RecepcionDetDto, clsBeTrans_re_det>();
        CreateMap<RecepcionOperadorDto, clsBeTrans_re_op>();
        CreateMap<TipoRecDto, clsBeTrans_re_tr>();
        CreateMap<StockRecDto, clsBeStock_rec>();
        CreateMap<ProductoBodegaDto, clsBeProducto_bodega>();
    }
}