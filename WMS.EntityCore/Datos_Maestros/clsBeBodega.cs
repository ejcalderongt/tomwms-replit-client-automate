using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Datos_Maestros
{
    public class clsBeBodega : ICloneable
    {
        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("IdPais")]
        [DisplayName("IdPais")]
        public int IdPais { get; set; } = 0;

        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("codigo")]
        [DisplayName("codigo")]
        public string Codigo { get; set; } = "";

        [Column("codigo_barra")]
        [DisplayName("codigo_barra")]
        public string Codigo_barra { get; set; } = "";

        [Column("nombre")]
        [DisplayName("nombre")]
        public string Nombre { get; set; } = "";

        [Column("nombre_comercial")]
        [DisplayName("nombre_comercial")]
        public string Nombre_comercial { get; set; } = "";

        [Column("direccion")]
        [DisplayName("direccion")]
        public string Direccion { get; set; } = "";

        [Column("telefono")]
        [DisplayName("telefono")]
        public string Telefono { get; set; } = "";

        [Column("email")]
        [DisplayName("email")]
        public string Email { get; set; } = "";

        [Column("encargado")]
        [DisplayName("encargado")]
        public string Encargado { get; set; } = "";

        [Column("ubic_recepcion")]
        [DisplayName("ubic_recepcion")]
        public string Ubic_recepcion { get; set; } = "";

        [Column("ubic_picking")]
        [DisplayName("ubic_picking")]
        public string Ubic_picking { get; set; } = "";

        [Column("ubic_despacho")]
        [DisplayName("ubic_despacho")]
        public string Ubic_despacho { get; set; } = "";

        [Column("ubic_merma")]
        [DisplayName("ubic_merma")]
        public string Ubic_merma { get; set; } = "";

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("coordenada_x")]
        [DisplayName("coordenada_x")]
        public string Coordenada_x { get; set; } = "";

        [Column("coordenada_y")]
        [DisplayName("coordenada_y")]
        public string Coordenada_y { get; set; } = "";

        [Column("largo")]
        [DisplayName("largo")]
        public double Largo { get; set; } = 0;

        [Column("ancho")]
        [DisplayName("ancho")]
        public double Ancho { get; set; } = 0;

        [Column("alto")]
        [DisplayName("alto")]
        public double Alto { get; set; } = 0;

        [Column("reservar_stocks_por_linea")]
        [DisplayName("reservar_stocks_por_linea")]
        public bool Reservar_stocks_por_linea { get; set; } = false;

        [Column("rechazar_pedido_por_stock")]
        [DisplayName("rechazar_pedido_por_stock")]
        public bool Rechazar_pedido_por_stock { get; set; } = false;

        [Column("IdTipoTransaccion")]
        [DisplayName("IdTipoTransaccion")]
        public string IdTipoTransaccion { get; set; } = "";

        [Column("zoom")]
        [DisplayName("zoom")]
        public double Zoom { get; set; } = 0;

        [Column("IdMotivoUbicacionDañadoPicking")]
        [DisplayName("IdMotivoUbicacionDañadoPicking")]
        public int IdMotivoUbicacionDañadoPicking { get; set; } = 0;

        [Column("cambio_ubicacion_auto")]
        [DisplayName("cambio_ubicacion_auto")]
        public bool Cambio_ubicacion_auto { get; set; } = false;

        [Column("codigo_bodega_erp")]
        [DisplayName("codigo_bodega_erp")]
        public string Codigo_bodega_erp { get; set; } = "";

        [Column("ubic_producto_ne")]
        [DisplayName("ubic_producto_ne")]
        public int Ubic_producto_ne { get; set; } = 0;

        [Column("IdProductoEstadoNE")]
        [DisplayName("IdProductoEstadoNE")]
        public int IdProductoEstadoNE { get; set; } = 0;

        [Column("Cuenta_Ingreso_Mercancias")]
        [DisplayName("Cuenta_Ingreso_Mercancias")]
        public string Cuenta_Ingreso_Mercancias { get; set; } = "";

        [Column("Cuenta_Egreso_Mercancias")]
        [DisplayName("Cuenta_Egreso_Mercancias")]
        public string Cuenta_Egreso_Mercancias { get; set; } = "";

        [Column("notificacion_voz")]
        [DisplayName("notificacion_voz")]
        public bool Notificacion_voz { get; set; } = false;

        [Column("control_tarifa_servicios")]
        [DisplayName("control_tarifa_servicios")]
        public bool Control_tarifa_servicios { get; set; } = false;

        [Column("Id_Motivo_Ubic_Reabasto")]
        [DisplayName("Id_Motivo_Ubic_Reabasto")]
        public int Id_Motivo_Ubic_Reabasto { get; set; } = 0;

        [Column("operador_defecto_en_documento_ingreso")]
        [DisplayName("operador_defecto_en_documento_ingreso")]
        public bool Operador_defecto_en_documento_ingreso { get; set; } = false;

        [Column("es_bodega_fiscal")]
        [DisplayName("es_bodega_fiscal")]
        public bool Es_bodega_fiscal { get; set; } = false;

        [Column("habilitar_ingreso_consolidado")]
        [DisplayName("habilitar_ingreso_consolidado")]
        public bool Habilitar_ingreso_consolidado { get; set; } = false;

        [Column("bloquear_lp_hh")]
        [DisplayName("bloquear_lp_hh")]
        public bool Bloquear_lp_hh { get; set; } = false;

        [Column("captura_estiba_ingreso")]
        [DisplayName("captura_estiba_ingreso")]
        public bool Captura_estiba_ingreso { get; set; } = false;

        [Column("captura_pallet_no_estandar")]
        [DisplayName("captura_pallet_no_estandar")]
        public bool Captura_pallet_no_estandar { get; set; } = false;

        [Column("valor_porcentaje_iva")]
        [DisplayName("valor_porcentaje_iva")]
        public double Valor_porcentaje_iva { get; set; } = 0;

        [Column("Permitir_Verificacion_Consolidada")]
        [DisplayName("Permitir_Verificacion_Consolidada")]
        public bool Permitir_Verificacion_Consolidada { get; set; } = false;

        [Column("control_banderas_cliente")]
        [DisplayName("control_banderas_cliente")]
        public bool Control_banderas_cliente { get; set; } = false;

        [Column("IdTamañoEtiquetaUbicacionDefecto")]
        [DisplayName("IdTamañoEtiquetaUbicacionDefecto")]
        public int IdTamañoEtiquetaUbicacionDefecto { get; set; } = 0;

        [Column("priorizar_ubicrec_sobre_ubicest")]
        [DisplayName("priorizar_ubicrec_sobre_ubicest")]
        public bool Priorizar_ubicrec_sobre_ubicest { get; set; } = false;

        [Column("ubicar_tarimas_completas_reabasto")]
        [DisplayName("ubicar_tarimas_completas_reabasto")]
        public bool Ubicar_tarimas_completas_reabasto { get; set; } = false;

        [Column("validar_disponibilidad_ubicaicon_destino")]
        [DisplayName("validar_disponibilidad_ubicaicon_destino")]
        public bool Validar_disponibilidad_ubicaicon_destino { get; set; } = false;

        [Column("IdTipoTransaccionSalida")]
        [DisplayName("IdTipoTransaccionSalida")]
        public int IdTipoTransaccionSalida { get; set; } = 0;

        [Column("permitir_eliminar_documento_salida")]
        [DisplayName("permitir_eliminar_documento_salida")]
        public bool Permitir_eliminar_documento_salida { get; set; } = false;

        [Column("mostrar_area_en_hh")]
        [DisplayName("mostrar_area_en_hh")]
        public bool Mostrar_area_en_hh { get; set; } = false;

        [Column("confirmar_codigo_en_picking")]
        [DisplayName("confirmar_codigo_en_picking")]
        public bool Confirmar_codigo_en_picking { get; set; } = false;

        [Column("control_operador_ubicacion")]
        [DisplayName("control_operador_ubicacion")]
        public bool Control_operador_ubicacion { get; set; } = false;

        [Column("inferir_origen_en_cambio_ubic")]
        [DisplayName("inferir_origen_en_cambio_ubic")]
        public bool Inferir_origen_en_cambio_ubic { get; set; } = false;

        [Column("eliminar_documento_salida")]
        [DisplayName("eliminar_documento_salida")]
        public bool Eliminar_documento_salida { get; set; } = false;

        [Column("operador_picking_realiza_verificacion")]
        [DisplayName("operador_picking_realiza_verificacion")]
        public bool Operador_picking_realiza_verificacion { get; set; } = false;

        [Column("permitir_cambio_ubic_producto_picking")]
        [DisplayName("permitir_cambio_ubic_producto_picking")]
        public bool Permitir_cambio_ubic_producto_picking { get; set; } = false;

        [Column("despachar_producto_vencido")]
        [DisplayName("despachar_producto_vencido")]
        public bool Despachar_producto_vencido { get; set; } = false;

        [Column("TIPO_PANTALLA_PICKING")]
        [DisplayName("TIPO_PANTALLA_PICKING")]
        public int TIPO_PANTALLA_PICKING { get; set; } = 0;

        [Column("verificacion_consolidada")]
        [DisplayName("verificacion_consolidada")]
        public bool Verificacion_consolidada { get; set; } = false;

        [Column("tipo_pantalla_recepcion")]
        [DisplayName("tipo_pantalla_recepcion")]
        public int Tipo_pantalla_recepcion { get; set; } = 0;

        [Column("tipo_pantalla_verificacion")]
        [DisplayName("tipo_pantalla_verificacion")]
        public int Tipo_pantalla_verificacion { get; set; } = 0;

        [Column("PERMITIR_BUEN_ESTADO_EN_REEMPLAZO")]
        [DisplayName("PERMITIR_BUEN_ESTADO_EN_REEMPLAZO")]
        public bool PERMITIR_BUEN_ESTADO_EN_REEMPLAZO { get; set; } = false;

        [Column("restringir_vencimiento_en_reemplazo")]
        [DisplayName("restringir_vencimiento_en_reemplazo")]
        public bool Restringir_vencimiento_en_reemplazo { get; set; } = false;

        [Column("restringir_lote_en_reemplazo")]
        [DisplayName("restringir_lote_en_reemplazo")]
        public bool Restringir_lote_en_reemplazo { get; set; } = false;

        [Column("industria_motriz")]
        [DisplayName("industria_motriz")]
        public bool Industria_motriz { get; set; } = false;

        [Column("top_reabastecimiento_manual")]
        [DisplayName("top_reabastecimiento_manual")]
        public int Top_reabastecimiento_manual { get; set; } = 0;

        [Column("permitir_decimales")]
        [DisplayName("permitir_decimales")]
        public bool Permitir_decimales { get; set; } = false;

        [Column("permitir_repeticiones_en_ingreso")]
        [DisplayName("permitir_repeticiones_en_ingreso")]
        public bool Permitir_repeticiones_en_ingreso { get; set; } = false;

        [Column("dias_maximo_vencimiento_reemplazo")]
        [DisplayName("dias_maximo_vencimiento_reemplazo")]
        public int Dias_maximo_vencimiento_reemplazo { get; set; } = 0;

        [Column("validar_existencias_inv_ini")]
        [DisplayName("validar_existencias_inv_ini")]
        public bool Validar_existencias_inv_ini { get; set; } = false;

        [Column("calcular_ubicacion_sugerida_ml")]
        [DisplayName("calcular_ubicacion_sugerida_ml")]
        public bool Calcular_ubicacion_sugerida_ml { get; set; } = false;

        [Column("permitir_reemplazo_picking")]
        [DisplayName("permitir_reemplazo_picking")]
        public bool Permitir_reemplazo_picking { get; set; } = false;

        [Column("permitir_no_encontrado_picking")]
        [DisplayName("permitir_no_encontrado_picking")]
        public bool Permitir_no_encontrado_picking { get; set; } = false;

        [Column("permitir_reemplazo_verificacion")]
        [DisplayName("permitir_reemplazo_verificacion")]
        public bool Permitir_reemplazo_verificacion { get; set; } = false;

        [Column("ordenar_por_nombre_completo")]
        [DisplayName("ordenar_por_nombre_completo")]
        public bool Ordenar_por_nombre_completo { get; set; } = false;

        [Column("ordenar_picking_descendente")]
        [DisplayName("ordenar_picking_descendente")]
        public bool Ordenar_picking_descendente { get; set; } = false;

        [Column("Permitir_Reemplazo_Picking_Misma_Licencia")]
        [DisplayName("Permitir_Reemplazo_Picking_Misma_Licencia")]
        public bool Permitir_Reemplazo_Picking_Misma_Licencia { get; set; } = false;

        [Column("dias_limite_retroactivo")]
        [DisplayName("dias_limite_retroactivo")]
        public int Dias_limite_retroactivo { get; set; } = 0;

        [Column("horario_ejecucion_historico")]
        [DisplayName("horario_ejecucion_historico")]
        public DateTime Horario_ejecucion_historico { get; set; } = DateTime.Now;

        [Column("filtrar_pedidos_usuario")]
        [DisplayName("filtrar_pedidos_usuario")]
        public bool Filtrar_pedidos_usuario { get; set; } = false;

        [Column("liberar_stock_depachos_parciales")]
        [DisplayName("liberar_stock_depachos_parciales")]
        public bool Liberar_stock_depachos_parciales { get; set; } = false;

        [Column("liberar_stock_despachos_parciales")]
        [DisplayName("liberar_stock_despachos_parciales")]
        public bool Liberar_stock_despachos_parciales { get; set; } = false;

        [Column("homologar_lote_vencimiento")]
        [DisplayName("homologar_lote_vencimiento")]
        public bool Homologar_lote_vencimiento { get; set; } = false;

        [Column("escanear_licencia_picking")]
        [DisplayName("escanear_licencia_picking")]
        public bool Escanear_licencia_picking { get; set; } = false;

        [Column("interface_SAP")]
        [DisplayName("interface_SAP")]
        public bool Interface_SAP { get; set; } = false;

        [Column("IdTipoEtiquetaLicencia")]
        [DisplayName("IdTipoEtiquetaLicencia")]
        public int IdTipoEtiquetaLicencia { get; set; } = 0;

        [Column("IdSimbologiaLicencia")]
        [DisplayName("IdSimbologiaLicencia")]
        public int IdSimbologiaLicencia { get; set; } = 0;

        [Column("restringir_areas_sap")]
        [DisplayName("restringir_areas_sap")]
        public bool Restringir_areas_sap { get; set; } = false;

        [Column("control_pallet_mixto")]
        [DisplayName("control_pallet_mixto")]
        public bool Control_pallet_mixto { get; set; } = false;

        [Column("despacho_automatico_hh")]
        [DisplayName("despacho_automatico_hh")]
        public bool Despacho_automatico_hh { get; set; } = false;
        public bool Control_Talla_Color { get; set; } = false;
        public bool Reservar_primero_almacenaje { get; set; } = false;
        public clsBeBodega() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}