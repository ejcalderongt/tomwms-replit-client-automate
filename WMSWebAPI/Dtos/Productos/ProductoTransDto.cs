using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Dtos.Productos
{
    public class ProductoTransDto : ProductoDto
    {
        public ProductoMarcaDto? Marca { get; set; }
        public ProductoTipoDto? TipoProducto { get; set; }
        public ProductoFamiliaDto? Familia { get; set; }
        public ProductoClasificacionDto? Clasificacion { get; set; }
        public ProductoParametroADto? ParametroA { get; set; }
        public ProductoParametroBDto? ParametroB { get; set; }
        public PropietarioDto? Propietario { get; set; }
        public List<PropietarioBodegaDto>? PropietarioBodega { get; set; }
        //public List<ProductoPresentacionDto> Presentaciones { get; set; } = new();        
        //public List<ProductoBodegaDto>? ProductoBodega { get; set; }
        //public List<ProductoEstadoDto>? ProductoEstado { get; set; }
    }
}