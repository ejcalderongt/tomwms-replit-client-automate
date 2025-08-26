namespace WMSWebAPI.Dtos.Ingresos
{
    public class DetalleOrdenCompraDto
    {
        public int IdPropietarioBodega { get; set; }
        public string Nombre_Propietario { get; set; } = "";
        public int No_Linea { get; set; }
        public int IdProductoBodega { get; set; }
        public string Codigo_Producto { get; set; } = "";
        public string Nombre_Producto { get; set; } = "";
        public string Nombre_Unidad_Medida_Basica { get; set; } = "";
        public int IdUnidadMedidaBasica { get; set; }
        public int IdPresentacion { get; set; }
        public int IdArancel { get; set; }
        public int IdMotivoDevolucion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Cantidad_Recibida { get; set; }
        public decimal Cantidad_Pendiente { get; set; }
        public decimal Peso_Bruto { get; set; }
        public decimal Peso_Neto { get; set; }
        public decimal Costo { get; set; }
        public decimal Valor_Aduana { get; set; }
        public decimal Valor_FOB { get; set; }
        public decimal Valor_IVA { get; set; }
        public decimal Valor_DAI { get; set; }
        public decimal Valor_Seguro { get; set; }
        public decimal Valor_Flete { get; set; }
        public decimal Total_Linea { get; set; }
        public int IdProducto { get; set; }
        public bool IsNew { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public int IdOrdenCompraDet { get; set; }        
        public string Atributo_Variante_1 { get; set; } = "";
        public bool Kit { get; set; }
        public int IdPedidoCompraDet { get; set; }
        public int IdOrdenCompraDetPadre { get; set; }
        public bool Control_Peso { get; set; }
        public decimal Peso_Referencia { get; set; }
        public string Nombre_Embarcador { get; set; } = "";
        public string Clasificacion_Producto { get; set; } = "";
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public string? SKU { get; set; }
    }
}