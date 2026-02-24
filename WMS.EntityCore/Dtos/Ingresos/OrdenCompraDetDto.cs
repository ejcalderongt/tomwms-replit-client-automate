using WMSWebAPI.Be;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class OrdenCompraDetDto
    {
        public int IdOrdenCompraEnc { get; set; }
        public int IdOrdenCompraDet { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdArancel { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedidaBasica { get; set; }
        public int? IdMotivoDevolucion { get; set; }
        public int? No_Linea { get; set; }
        public string? Nombre_Producto { get; set; }
        public string? Nombre_Presentacion { get; set; }
        public string? Nombre_Arancel { get; set; }
        public float? Porcentaje_Arancel { get; set; }
        public string? Nombre_Unidad_Medida_Basica { get; set; }
        public float? Cantidad { get; set; }
        public float? Cantidad_Recibida { get; set; }
        public float? Costo { get; set; }
        public float? Total_Linea { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public bool Activo { get; set; }
        public float? Peso { get; set; }
        public float? Peso_Recibido { get; set; }
        public string? Atributo_Variante_1 { get; set; }
        public string? Codigo_Producto { get; set; }
        public float? Valor_Aduana { get; set; }
        public float? Valor_FOB { get; set; }
        public float? Valor_IVA { get; set; }
        public float? Valor_DAI { get; set; }
        public float? Valor_Seguro { get; set; }
        public float? Valor_Flete { get; set; }
        public float? Peso_Neto { get; set; }
        public float? Peso_Bruto { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public string? Nombre_Propietario { get; set; }
        public int? IdOrdenCompraDetPadre { get; set; }
        public int? IdEmbarcador { get; set; }
        public ProductoBodegaDto ProductoBodega { get; set; } = new ProductoBodegaDto();
        public ProductoPresentacionDto Presentacion { get; set; } = new ProductoPresentacionDto();
        public bool IsNew { get; set; } = true;
        public bool ExisteEnRecepcion { get; set; }
        public decimal FactorPresentacion { get; set; }        
        public ProductoDto Producto { get; set; } = new ProductoDto();
        public UnidadMedidaDto UnidadMedida { get; set; } = new UnidadMedidaDto();
        public int RowIndex { get; set; }        
        public int IdProductoTallaColor { get; set; }
        public string Nombre_Embarcador { get; set; } = string.Empty;
        public string Nombre_Clasificacion { get; set; } = string.Empty;
        public clsBeArancel Arancel { get; set; } = new clsBeArancel();
        public clsBeTalla Talla { get; set; } = new clsBeTalla();
        public clsBeColor Color { get; set; } = new clsBeColor();
        public int LayersPallet { get; set; } = 0;
        public int BoxesLayer { get; set; } = 0;
        public List<OrdenCompraDetDto> lProductosHijosKit { get; set; } = new List<OrdenCompraDetDto>();
    }
}