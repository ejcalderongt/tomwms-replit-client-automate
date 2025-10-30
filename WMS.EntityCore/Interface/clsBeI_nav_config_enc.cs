using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Interface
{
    public class clsBeI_nav_config_enc : ICloneable
    {
        [Column("idnavconfigenc")]
        [DisplayName("idnavconfigenc")]
        public int Idnavconfigenc { get; set; } = 0;

        [Column("idempresa")]
        [DisplayName("idempresa")]
        public int Idempresa { get; set; } = 0;

        [Column("idbodega")]
        [DisplayName("idbodega")]
        public int Idbodega { get; set; } = 0;

        [Column("idPropietario")]
        [DisplayName("idPropietario")]
        public int IdPropietario { get; set; } = 0;

        [Column("idUsuario")]
        [DisplayName("idUsuario")]
        public int IdUsuario { get; set; } = 0;

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("IdProductoEstado")]
        [DisplayName("IdProductoEstado")]
        public int IdProductoEstado { get; set; } = 0;

        [Column("rechazar_pedido_incompleto")]
        [DisplayName("rechazar_pedido_incompleto")]
        public int Rechazar_pedido_incompleto { get; set; } = 0;

        [Column("despachar_existencia_parcial")]
        [DisplayName("despachar_existencia_parcial")]
        public int Despachar_existencia_parcial { get; set; } = 0;

        [Column("convertir_decimales_a_umbas")]
        [DisplayName("convertir_decimales_a_umbas")]
        public int Convertir_decimales_a_umbas { get; set; } = 0;

        [Column("generar_pedido_ingreso_bodega_destino")]
        [DisplayName("generar_pedido_ingreso_bodega_destino")]
        public bool Generar_pedido_ingreso_bodega_destino { get; set; } = false;

        [Column("generar_recepcion_auto_bodega_destino")]
        [DisplayName("generar_recepcion_auto_bodega_destino")]
        public bool Generar_recepcion_auto_bodega_destino { get; set; } = false;

        [Column("codigo_proveedor_produccion")]
        [DisplayName("codigo_proveedor_produccion")]
        public string Codigo_proveedor_produccion { get; set; } = "";

        [Column("idFamilia")]
        [DisplayName("idFamilia")]
        public int IdFamilia { get; set; } = 0;

        [Column("idclasificacion")]
        [DisplayName("idclasificacion")]
        public int Idclasificacion { get; set; } = 0;

        [Column("idMarca")]
        [DisplayName("idMarca")]
        public int IdMarca { get; set; } = 0;

        [Column("idTipoProducto")]
        [DisplayName("idTipoProducto")]
        public int IdTipoProducto { get; set; } = 0;

        [Column("control_lote")]
        [DisplayName("control_lote")]
        public bool Control_lote { get; set; } = false;

        [Column("control_vencimiento")]
        [DisplayName("control_vencimiento")]
        public bool Control_vencimiento { get; set; } = false;

        [Column("genera_lp")]
        [DisplayName("genera_lp")]
        public bool Genera_lp { get; set; } = false;

        [Column("nombre_ejecutable")]
        [DisplayName("nombre_ejecutable")]
        public string Nombre_ejecutable { get; set; } = "";

        [Column("IdTipoDocumentoTransferenciasIngreso")]
        [DisplayName("IdTipoDocumentoTransferenciasIngreso")]
        public int IdTipoDocumentoTransferenciasIngreso { get; set; } = 0;

        [Column("crear_recepcion_de_transferencia_nav")]
        [DisplayName("crear_recepcion_de_transferencia_nav")]
        public bool Crear_recepcion_de_transferencia_nav { get; set; } = false;

        [Column("control_peso")]
        [DisplayName("control_peso")]
        public bool Control_peso { get; set; } = false;

        [Column("crear_recepcion_de_compra_nav")]
        [DisplayName("crear_recepcion_de_compra_nav")]
        public bool Crear_recepcion_de_compra_nav { get; set; } = false;

        [Column("IdAcuerdoEnc")]
        [DisplayName("IdAcuerdoEnc")]
        public int IdAcuerdoEnc { get; set; } = 0;

        [Column("IdTipoEtiqueta")]
        [DisplayName("IdTipoEtiqueta")]
        public int IdTipoEtiqueta { get; set; } = 0;

        [Column("equiparar_cliente_con_propietario_en_doc_salida")]
        [DisplayName("equiparar_cliente_con_propietario_en_doc_salida")]
        public bool Equiparar_cliente_con_propietario_en_doc_salida { get; set; } = false;

        [Column("push_ingreso_nav_desde_hh")]
        [DisplayName("push_ingreso_nav_desde_hh")]
        public bool Push_ingreso_nav_desde_hh { get; set; } = false;

        [Column("reservar_umbas_primero")]
        [DisplayName("reservar_umbas_primero")]
        public bool Reservar_umbas_primero { get; set; } = false;

        [Column("implosion_automatica")]
        [DisplayName("implosion_automatica")]
        public bool Implosion_automatica { get; set; } = false;

        [Column("explosion_automatica")]
        [DisplayName("explosion_automatica")]
        public bool Explosion_automatica { get; set; } = false;

        [Column("Ejecutar_En_Despacho_Automaticamente")]
        [DisplayName("Ejecutar_En_Despacho_Automaticamente")]
        public bool Ejecutar_En_Despacho_Automaticamente { get; set; } = false;

        [Column("IdTipoRotacion")]
        [DisplayName("IdTipoRotacion")]
        public int IdTipoRotacion { get; set; } = 0;

        [Column("explosion_automatica_desde_ubicacion_picking")]
        [DisplayName("explosion_automatica_desde_ubicacion_picking")]
        public bool Explosion_automatica_desde_ubicacion_picking { get; set; } = false;

        [Column("explosion_automatica_nivel_max")]
        [DisplayName("explosion_automatica_nivel_max")]
        public int Explosion_automatica_nivel_max { get; set; } = 0;

        [Column("conservar_zona_picking_clavaud")]
        [DisplayName("conservar_zona_picking_clavaud")]
        public bool Conservar_zona_picking_clavaud { get; set; } = false;

        [Column("recepcion_genera_historico")]
        [DisplayName("recepcion_genera_historico")]
        public bool Recepcion_genera_historico { get; set; } = false;

        [Column("excluir_ubicaciones_reabasto")]
        [DisplayName("excluir_ubicaciones_reabasto")]
        public bool Excluir_ubicaciones_reabasto { get; set; } = false;

        [Column("considerar_disponibilidad_ubicacion_reabasto")]
        [DisplayName("considerar_disponibilidad_ubicacion_reabasto")]
        public bool Considerar_disponibilidad_ubicacion_reabasto { get; set; } = false;

        [Column("considerar_paletizado_en_reabasto")]
        [DisplayName("considerar_paletizado_en_reabasto")]
        public bool Considerar_paletizado_en_reabasto { get; set; } = false;

        [Column("dias_vida_defecto_perecederos")]
        [DisplayName("dias_vida_defecto_perecederos")]
        public int Dias_vida_defecto_perecederos { get; set; } = 0;

        [Column("codigo_bodega_erp_nc")]
        [DisplayName("codigo_bodega_erp_nc")]
        public string Codigo_bodega_erp_nc { get; set; } = "";

        [Column("lote_defecto_entrada_nc")]
        [DisplayName("lote_defecto_entrada_nc")]
        public string Lote_defecto_entrada_nc { get; set; } = "";

        [Column("vence_defecto_nc")]
        [DisplayName("vence_defecto_nc")]
        public DateTime Vence_defecto_nc { get; set; } = DateTime.Now;

        [Column("IdProductoEstado_NC")]
        [DisplayName("IdProductoEstado_NC")]
        public int IdProductoEstado_NC { get; set; } = 0;

        [Column("lote_defecto_entrada_mercancia_sap")]
        [DisplayName("lote_defecto_entrada_mercancia_sap")]
        public string Lote_defecto_entrada_mercancia_sap { get; set; } = "";

        [Column("fecha_vence_defecto")]
        [DisplayName("fecha_vence_defecto")]
        public DateTime Fecha_vence_defecto { get; set; } = DateTime.Now;

        [Column("interface_sap")]
        [DisplayName("interface_sap")]
        public bool Interface_sap { get; set; } = false;

        [Column("sap_control_draft_ajustes")]
        [DisplayName("sap_control_draft_ajustes")]
        public bool Sap_control_draft_ajustes { get; set; } = false;

        [Column("sap_control_draft_traslados")]
        [DisplayName("sap_control_draft_traslados")]
        public bool Sap_control_draft_traslados { get; set; } = false;

        [Column("inferir_bonificacion_pedido_sap")]
        [DisplayName("inferir_bonificacion_pedido_sap")]
        public bool Inferir_bonificacion_pedido_sap { get; set; } = false;

        [Column("rechazar_bonificacion_incompleta")]
        [DisplayName("rechazar_bonificacion_incompleta")]
        public bool Rechazar_bonificacion_incompleta { get; set; } = false;

        [Column("IdIndiceRotacion")]
        [DisplayName("IdIndiceRotacion")]
        public int IdIndiceRotacion { get; set; } = 0;

        [Column("rango_dias_importacion")]
        [DisplayName("rango_dias_importacion")]
        public int Rango_dias_importacion { get; set; } = 0;

        [Column("equiparar_productos")]
        [DisplayName("equiparar_productos")]
        public bool Equiparar_productos { get; set; } = false;

        [Column("bodega_facturacion")]
        [DisplayName("bodega_facturacion")]
        public string Bodega_facturacion { get; set; } = "";

        [Column("valida_solo_codigo")]
        [DisplayName("valida_solo_codigo")]
        public bool Valida_solo_codigo { get; set; } = false;

        [Column("excluir_recepcion_picking")]
        [DisplayName("excluir_recepcion_picking")]
        public bool Excluir_recepcion_picking { get; set; } = false;

        [Column("bodega_prorrateo")]
        [DisplayName("bodega_prorrateo")]
        public string Bodega_prorrateo { get; set; } = "";

        [Column("bodega_prorrateo1")]
        [DisplayName("bodega_prorrateo1")]
        public string Bodega_prorrateo1 { get; set; } = "";
        public bool Interface_SAP { get; set; } = false;
        public bool Equiparar_Productos { get; set; } = false;
        //public bool Crear_Recepcion_De_Compra_NAV { get; set; } = false;

        public clsBeI_nav_config_enc() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
