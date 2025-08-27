using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Pedido
{
    public class clsBeTrans_pe_tipo : ICloneable
    {
        [Column("IdTipoPedido")]
        [DisplayName("IdTipoPedido")]
        public int IdTipoPedido { get; set; } = 0;

        [Column("Nombre")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; } = "";

        [Column("Descripcion")]
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } = "";

        [Column("Preparar")]
        [DisplayName("Preparar")]
        public bool Preparar { get; set; } = false;

        [Column("Verificar")]
        [DisplayName("Verificar")]
        public bool Verificar { get; set; } = false;

        [Column("ReservaStock")]
        [DisplayName("ReservaStock")]
        public bool ReservaStock { get; set; } = false;

        [Column("ImprimeBarrasPicking")]
        [DisplayName("ImprimeBarrasPicking")]
        public bool ImprimeBarrasPicking { get; set; } = false;

        [Column("ImprimeBarrasPacking")]
        [DisplayName("ImprimeBarrasPacking")]
        public bool ImprimeBarrasPacking { get; set; } = false;

        [Column("control_poliza")]
        [DisplayName("control_poliza")]
        public bool Control_poliza { get; set; } = false;

        [Column("Generar_pedido_ingreso_bodega_destino")]
        [DisplayName("Generar_pedido_ingreso_bodega_destino")]
        public bool Generar_pedido_ingreso_bodega_destino { get; set; } = false;

        [Column("IdTipoIngresoOC")]
        [DisplayName("IdTipoIngresoOC")]
        public int IdTipoIngresoOC { get; set; } = 0;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("requerir_documento_ref")]
        [DisplayName("requerir_documento_ref")]
        public bool Requerir_documento_ref { get; set; } = false;

        [Column("trasladar_lotes_doc_ingreso")]
        [DisplayName("trasladar_lotes_doc_ingreso")]
        public bool Trasladar_lotes_doc_ingreso { get; set; } = false;

        [Column("requerir_cliente_es_bodega_wms")]
        [DisplayName("requerir_cliente_es_bodega_wms")]
        public bool Requerir_cliente_es_bodega_wms { get; set; } = false;

        [Column("marcar_registros_enviados_mi3")]
        [DisplayName("marcar_registros_enviados_mi3")]
        public bool Marcar_registros_enviados_mi3 { get; set; } = false;

        [Column("generar_recepcion_auto_bodega_destino")]
        [DisplayName("generar_recepcion_auto_bodega_destino")]
        public bool Generar_recepcion_auto_bodega_destino { get; set; } = false;

        [Column("recibir_producto_auto_bodega_destino")]
        [DisplayName("recibir_producto_auto_bodega_destino")]
        public bool Recibir_producto_auto_bodega_destino { get; set; } = false;

        [Column("Control_Cliente_En_Detalle")]
        [DisplayName("Control_Cliente_En_Detalle")]
        public bool Control_Cliente_En_Detalle { get; set; } = false;

        [Column("permitir_despacho_parcial")]
        [DisplayName("permitir_despacho_parcial")]
        public bool Permitir_despacho_parcial { get; set; } = false;

        [Column("permitir_despacho_multiple")]
        [DisplayName("permitir_despacho_multiple")]
        public bool Permitir_despacho_multiple { get; set; } = false;

        [Column("fotografia_verificacion")]
        [DisplayName("fotografia_verificacion")]
        public bool Fotografia_verificacion { get; set; } = false;

        [Column("es_devolucion")]
        [DisplayName("es_devolucion")]
        public bool Es_devolucion { get; set; } = false;

        public clsBeTrans_pe_tipo() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}