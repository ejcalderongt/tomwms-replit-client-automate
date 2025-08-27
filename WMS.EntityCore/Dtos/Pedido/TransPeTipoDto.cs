namespace WMSWebAPI.Dtos.Pedido
{
    public class TransPeTipoDto
    {
        public int IdTipoPedido { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Preparar { get; set; } = false;
        public bool Verificar { get; set; } = false;
        public bool ReservaStock { get; set; } = false;
        public bool ImprimeBarrasPicking { get; set; } = false;
        public bool ImprimeBarrasPacking { get; set; } = false;
        public bool Control_poliza { get; set; } = false;
        public bool Generar_pedido_ingreso_bodega_destino { get; set; } = false;
        public int IdTipoIngresoOC { get; set; } = 0;
        public bool Activo { get; set; } = false;
        public bool Requerir_documento_ref { get; set; } = false;
        public bool Trasladar_lotes_doc_ingreso { get; set; } = false;
        public bool Requerir_cliente_es_bodega_wms { get; set; } = false;
        public bool Marcar_registros_enviados_mi3 { get; set; } = false;
        public bool Generar_recepcion_auto_bodega_destino { get; set; } = false;
        public bool Recibir_producto_auto_bodega_destino { get; set; } = false;
        public bool Control_Cliente_En_Detalle { get; set; } = false;
        public bool Permitir_despacho_parcial { get; set; } = false;
        public bool Permitir_despacho_multiple { get; set; } = false;
        public bool Fotografia_verificacion { get; set; } = false;
        public bool Es_devolucion { get; set; } = false;
    }
}
