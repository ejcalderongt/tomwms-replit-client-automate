---
id: sintesis
tipo: heat-map-params
estado: vigente
titulo: Sintesis — Matriz visual del heat-map-params
tags: [heat-map-params]
---

# Sintesis — Matriz visual del heat-map-params

> Vista condensada de cada capa para responder rapido: "donde puedo
> tocar para cambiar X comportamiento?"

## Mapa de calor: parametros mas referenciados a lo largo del codigo

(PENDIENTE: cruzar con codigo `wms-agent` y `wms-brain-client` para
contar referencias por col)

## Decisiones del sendero por parametro

```
RECEPCION
  ¿que tipo? → trans_oc_ti.IdTipoIngresoOC + Nombre + es_devolucion
  ¿requiere poliza? → trans_oc_ti.control_poliza
  ¿genera tarea (empuja stock)? → trans_oc_ti.genera_tarea_ingreso
  ¿estado por defecto? → trans_oc_ti.IdProductoEstado o bodega.IdProductoEstado
  ¿requiere ubic? → trans_oc_ti.requerir_ubic_rec_ingreso
  ¿permite vencido? → trans_oc_ti.permitir_vencido_ingreso
  ¿bloquea lote? → trans_oc_ti.bloquea_lotes
  ¿es_importacion? → trans_oc_ti.es_importacion
      └─ si SI → flujo Polizas/DUCA

PUT-AWAY
  ¿auto-cambio ubic? → bodega.cambio_ubicacion_auto
  ¿priorizar ubic_rec? → bodega.priorizar_ubicrec_sobre_ubicest
  ¿valida disponibilidad destino? → bodega.validar_disponibilidad_ubicaicon_destino

PICKING
  ¿tipo de pedido? → trans_pe_tipo.IdTipoPedido + Descripcion
  ¿requiere preparar? → trans_pe_tipo.Preparar
  ¿reserva stock al crear? → trans_pe_tipo.ReservaStock
  ¿picking por voz? → bodega.notificacion_voz
  ¿confirmar codigo? → bodega.confirmar_codigo_en_picking
  ¿escanear licencia? → bodega.escanear_licencia_picking
  ¿permite reemplazo? → bodega.permitir_reemplazo_picking
  ¿operador hace tambien VERI? → bodega.operador_picking_realiza_verificacion
  ¿pantalla? → bodega.TIPO_PANTALLA_PICKING

VERIFICACION
  ¿requiere? → trans_pe_tipo.Verificar
  ¿consolidada? → bodega.Permitir_Verificacion_Consolidada + verificacion_consolidada
  ¿foto? → trans_pe_tipo.fotografia_verificacion
  ¿permite reemplazo? → bodega.permitir_reemplazo_verificacion

PACKING
  ¿hace packing? → trans_pe_tipo.empaque_tarima
  ¿imprime barras? → bodega.ImprimeBarrasPacking

DESPACHO
  ¿permite parcial? → trans_pe_tipo.permitir_despacho_parcial
  ¿permite multiple? → trans_pe_tipo.permitir_despacho_multiple
  ¿automatico desde HH? → bodega.despacho_automatico_hh
  ¿genera guia? → trans_pe_tipo.genera_guia_remision
  ¿despacha vencido? → bodega.despachar_producto_vencido

TRANSFERENCIA WMS ↔ WMS
  ¿cliente es bodega WMS? → trans_pe_tipo.requerir_cliente_es_bodega_wms (salida)
                            o trans_oc_ti.requerir_proveedor_es_bodega_wms (entrada)
  ¿genera pedido en destino? → trans_pe_tipo.Generar_pedido_ingreso_bodega_destino
                              + i_nav_config_enc.generar_pedido_ingreso_bodega_destino
  ¿recepcion automatica destino? → trans_pe_tipo.generar_recepcion_auto_bodega_destino
                                  + bodega.generar_recepcion_auto_bodega_destino
  ¿recibe producto auto? → trans_pe_tipo.recibir_producto_auto_bodega_destino

DEVOLUCION
  ¿es devolucion? → trans_oc_ti.es_devolucion (entrada) o trans_pe_tipo.es_devolucion (salida)
  ¿bodega de devolucion? → bodega.es_devolucion

REABASTO
  ¿excluye ubicaciones? → i_nav_config_enc.excluir_ubicaciones_reabasto
  ¿considera paletizado? → i_nav_config_enc.considerar_paletizado_en_reabasto
  ¿considera disponibilidad? → i_nav_config_enc.considerar_disponibilidad_ubicacion_reabasto
  ¿top manual? → bodega.top_reabastecimiento_manual
  ¿tarimas completas? → bodega.ubicar_tarimas_completas_reabasto

EXPLOSION/IMPLOSION
  ¿automatica? → i_nav_config_enc.explosion_automatica + implosion_automatica
  ¿desde ubic picking? → i_nav_config_enc.explosion_automatica_desde_ubicacion_picking
  ¿nivel max? → i_nav_config_enc.explosion_automatica_nivel_max

INTERFACE ERP
  ¿que ejecutable? → i_nav_config_enc.nombre_ejecutable
  ¿interface SAP? → i_nav_config_enc.interface_sap + bodega.interface_SAP
  ¿NAV crea recepcion compra? → i_nav_config_enc.crear_recepcion_de_compra_nav
  ¿NAV crea recepcion transferencia? → i_nav_config_enc.crear_recepcion_de_transferencia_nav
  ¿push NAV desde HH? → i_nav_config_enc.push_ingreso_nav_desde_hh
  ¿drafts SAP? → i_nav_config_enc.sap_control_draft_ajustes/traslados
  ¿bonificacion SAP? → i_nav_config_enc.inferir_bonificacion_pedido_sap

PRODUCCION
  ¿recibe produccion? → i_nav_config_enc.codigo_proveedor_produccion no nulo

PRODUCTOS NO COMERCIALES (NC)
  ¿bodega ERP NC? → i_nav_config_enc.codigo_bodega_erp_nc
  ¿lote default NC? → i_nav_config_enc.lote_defecto_entrada_nc
  ¿vence default NC? → i_nav_config_enc.vence_defecto_nc
  ¿estado NC? → i_nav_config_enc.IdProductoEstado_NC + bodega.IdProductoEstadoNE

DECIMALES
  ¿permite decimales? → bodega.permitir_decimales
  ¿convierte a UMBas? → i_nav_config_enc.convertir_decimales_a_umbas

TALLA Y COLOR
  ¿maneja talla/color? → bodega.control_talla_color (caso MAMPA)

ML / SUGERENCIAS
  ¿sugerencia ubic con ML? → bodega.calcular_ubicacion_sugerida_ml
```

## Tabla cross-cliente del sendero (preview, datos parciales)

| Pregunta | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| Bodegas | 1 | 6 | 31 | 2 | 2 |
| Recibe produccion? | ? | ? | ? | SI (1060315) | ? |
| Bodega calidad? | SI (cuarentena ubic) | ? | ? | ? | NO |
| Estado default ingreso | Cuarentena | Buen Estado | Buen Estado | Buen Estado (1) | NULL |
| Etiqueta generada o pre-impresa | generada | ? | ? | ? | ? |
| Transferencias WMS<->WMS | NO | SI (TRAS_WMS) | SI (Prorrateo) | SI (PE0004) | SI (5+ tipos) |
| Transferencias WMS<->no-WMS | ? | ? | ? | ? | ? |
| Hace packing? | NO | NO | NO | SI | NO |
| Hace explosion? | NO | NO | NO | SI | NO |
| Hace implosion? | NO | NO | NO | SI | NO |
| Reabasto automatico? | NO | SI (intensivo) | NO | SI | NO |
| Picking consolidado? | NO | NO | NO | SI (PE0002) | NO |
| Picking por voz? | ? | ? | ? | ? | NO |
| Talla y color? | NO | NO | SI | NO | NO |
| ML sugerencia ubic? | ? | ? | ? | ? | ? |
| Interface | ? (a confirmar) | NAV (PDV_NAV+TRAS_WMS) | SAP (TRAS_SAP) | NAV (NavSync.exe) | PREFACTURA |
| Salida sintetica | NO | NO | NO | NO | SI |
| Schema actualizado | ? | ? | ? | mas reciente | mas viejo (-7 cols en trans_pe_tipo) |

## Pendientes

- Cross-cliente exhaustivo con valores reales (correr matriz completa).
- Cargar capa por capa en proxima iteracion.
- Empezar archivos dedicados por proceso en `06-procesos-homologados/`.
