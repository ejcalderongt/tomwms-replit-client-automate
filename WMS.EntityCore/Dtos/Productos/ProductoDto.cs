using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Stock;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoDto
    {
        // === EXISTENTES ===
        public int IdProducto { get; set; }
        public int IdPropietario { get; set; }
        public int? IdClasificacion { get; set; }
        public int? IdFamilia { get; set; }
        public int? IdMarca { get; set; }
        public int? IdTipoProducto { get; set; }
        public int IdUnidadMedidaBasica { get; set; }
        public int? IdCamara { get; set; }
        public int? IdTipoRotacion { get; set; }
        public int? IdPerfilSerializado { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public int? IdSimbologia { get; set; }
        public int? IdArancel { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo_Barra { get; set; }
        public float? Precio { get; set; }
        public float? Existencia_Min { get; set; }
        public float? Existencia_Max { get; set; }
        public float? Costo { get; set; }
        public float? Peso_Referencia { get; set; }
        public float? Peso_Tolerancia { get; set; }
        public float? Temperatura_Referencia { get; set; }
        public float? Temperatura_Tolerancia { get; set; }
        public bool? Activo { get; set; }
        public bool? Serializado { get; set; }
        public bool? Genera_Lote { get; set; }
        public bool? Genera_Lp_Old { get; set; }
        public bool? Control_Vencimiento { get; set; }
        public bool? Control_Lote { get; set; }
        public bool? Peso_Recepcion { get; set; }
        public bool? Peso_Despacho { get; set; }
        public bool? Temperatura_Recepcion { get; set; }
        public bool? Temperatura_Despacho { get; set; }
        public bool? Materia_Prima { get; set; }
        public bool? Kit { get; set; }
        public int? Tolerancia { get; set; }
        public int? Ciclo_Vida { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public string? NoSerie { get; set; }
        public string? NoParte { get; set; }
        public bool? FechaManufactura { get; set; }
        public bool? Capturar_Aniada { get; set; }
        public bool? Control_Peso { get; set; }
        public bool? Captura_Arancel { get; set; }
        public bool? Es_Hardware { get; set; }
        public float? Largo { get; set; }
        public float? Alto { get; set; }
        public float? Ancho { get; set; }
        public int? IdUnidadMedidaCobro { get; set; }
        public int? IdTipoEtiqueta { get; set; }
        public int? Dias_Inventario_Promedio { get; set; }
        public int? IDPRODUCTOPARAMETROA { get; set; }
        public int? IDPRODUCTOPARAMETROB { get; set; }
        public int? IdTipoManufactura { get; set; }
        public byte[]? imagen { get; set; } = Array.Empty<byte>();
        
        public int IdProductoBodega { get; set; }
        
        // Estado/meta
        public bool IsNew { get; set; } = true;
        public object? Tag { get; set; }

        // Presentaciones origen/destino
        public int IdPresentacionOrigen { get; set; } = 0;
        public int IdPresentacionDestino { get; set; } = 0;
        public double Factor { get; set; } = 0;
        public double ExistenciaUMBas { get; set; } = 0;

        // Indice de rotación complejo + parámetros A/B
        public clsBeIndice_rotacion? Indice_Rotacion { get; set; }        

        // Campos para inventario cíclico
        public string? Lote { get; set; } = "";
        public DateTime? FechaVence { get; set; }
        public double? Cantidad { get; set; } = 0;
        public clsBeVW_stock_res? Stock { get; set; }
        public bool genera_lote { get; set; } = false;
        public bool genera_lp_old { get; set; } = false;
        public bool control_vencimiento { get; set; } = false;
        public bool control_lote { get; set; } = false;
        public bool control_peso { get; set; } = false;
        public bool activo { get; set; } = true;
        public PropietarioDto Propietario { get; set; } = new PropietarioDto();
        public ProductoPresentacionDto Presentacion { get; set; } = new ProductoPresentacionDto();
        public ProductoClasificacionDto Clasificacion { get; set; } = new ProductoClasificacionDto();
        public ProductoFamiliaDto Familia { get; set; } = new ProductoFamiliaDto();
        public ProductoMarcaDto Marca { get; set; } = new ProductoMarcaDto();
        public ProductoTipoDto TipoProducto { get; set; } = new ProductoTipoDto();
        public UnidadMedidaDto UnidadMedida { get; set; } = new UnidadMedidaDto();
        public List<ProductoPresentacionDto> Presentaciones { get; set; } = new List<ProductoPresentacionDto>();
        public List<clsBeProducto_codigos_barra> Codigos_Barra { get; set; } = new List<clsBeProducto_codigos_barra>();
        public List<clsBeProducto_parametros> Parametros { get; set; } = new List<clsBeProducto_parametros>();
        public clsBeProducto_parametro_a ParametroA { get; set; }= new clsBeProducto_parametro_a();
        public clsBeProducto_parametro_b ParametroB { get; set; } = new clsBeProducto_parametro_b();
        public List<PropietarioBodegaDto>? PropietarioBodega { get; set; }
        public List<ProductoBodegaDto>? ProductoBodega { get; set; }
        public List<ProductoEstadoDto>? ProductoEstado { get; set; }
    }    

    public class IndiceRotacionDto
    {
        public int IdIndiceRotacion { get; set; }
        public string? Nombre { get; set; }
    }    
}