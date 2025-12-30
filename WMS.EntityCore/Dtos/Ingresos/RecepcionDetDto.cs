using WMS.EntityCore.Dtos.Ingresos;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Dtos.Ingresos
{
    public class RecepcionDetDto
    {
        public int IdRecepcionDet { get; set; }
        public int IdRecepcionEnc { get; set; }
        public int IdProductoBodega { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdProductoEstado { get; set; }
        public int? IdOperadorBodega { get; set; }
        public int? IdMotivoDevolucion { get; set; }
        public int? No_Linea { get; set; }
        public double? Cantidad_Recibida { get; set; }
        public string? Nombre_Producto { get; set; }
        public string? Nombre_Presentacion { get; set; }
        public string? Nombre_Unidad_Medida { get; set; }
        public string? Nombre_Producto_Estado { get; set; }
        public string? Lote { get; set; }
        public DateTime? Fecha_Vence { get; set; }
        public DateTime? Fecha_Ingreso { get; set; }
        public double? Peso { get; set; }
        public double? Peso_Estadistico { get; set; }
        public double? Peso_Minimo { get; set; }
        public double? Peso_Maximo { get; set; }
        public double? Peso_Unitario { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? Observacion { get; set; }
        public int? Añada { get; set; }
        public double? Costo { get; set; }
        public double? Costo_OC { get; set; }
        public double? Costo_Estadistico { get; set; }
        public string? Atributo_Variante_1 { get; set; }
        public string? Codigo_Producto { get; set; }
        public string? Lic_Plate { get; set; }
        public bool? Pallet_No_Estandar { get; set; }
        public int? IdOrdenCompraEnc { get; set; }
        public int? IdOrdenCompraDet { get; set; }
        public int? IdJornadaSistema { get; set; }
        public ProductoDto Producto { get; set; } = new ProductoDto();
        public ProductoPresentacionDto Presentacion { get; set; } = new ProductoPresentacionDto();
        public ProductoEstadoDto ProductoEstado { get; set; } = new ProductoEstadoDto();
        public UnidadMedidaDto UnidadMedida { get; set; } = new UnidadMedidaDto();

        //#GT: actualizar a tipo objeto
       // public string MotivoDevolucion { get; set; } = string.Empty;
       public List<MotivoDevolucionDto> MotivoDevolucion { get; set; } = new List<MotivoDevolucionDto>();
        public bool IsNew { get; set; } = true;
        public bool Control_Peso { get; set; }
        public int IdPropietarioBodega { get; set; }
        public int IdUbicacion { get; set; }
        public int IdUbicacionAnterior { get; set; }
        public DateTime Fecha_Rec { get; set; }
        public DateTime Fecha_tarea { get; set; }
        public DateTime Hora_ini { get; set; }
        public DateTime Hora_Fin { get; set; }
        public string Estado_Rec { get; set; } = string.Empty;
        public string UbicacionCompleta { get; set; } = string.Empty;
        public decimal Uds_lic_plate { get; set; }
        public string Host { get; set; } = string.Empty;
        public string Talla { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int IdProductoTallaColor { get; set; }
    }
}