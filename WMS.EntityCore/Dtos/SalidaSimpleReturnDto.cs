namespace WMS.EntityCore.Dtos
{
    public class SalidaSimpleReturnDto
    {
        public int Idtransaccion { get; set; }
        public string? No_pedido { get; set; }
        public string? Codigo_producto { get; set; }
        public string? Nombre_producto { get; set; }
        public string? UM { get; set; }
        public string? Presentacion { get; set; }
        public double Cantidad { get; set; }
        public string? Lote { get; set; }
        public DateTime? Vence { get; set; }
        public int Linea { get; set; }
        public string? Licencia { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Codigo_Bodega_Origen { get; set; }
        public string? Codigo_Bodega_Destino { get; set; }
        public string? Codigo_Cliente { get; set; }
        public int? IdDocIngresoBodDestino { get; set; }
        public int? IdDocSalidaBodOrigen { get; set; }
        public string? UsuarioDocumento { get; set; }
        public string? UsuarioDespacho { get; set; }

    }

}