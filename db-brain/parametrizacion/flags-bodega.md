# Flags del maestro `bodega`

> ~57 columnas bit en `dbo.bodega` (validado 2026-04-27). **El maestro más configurable del WMS.**

## Listado completo

- `activo`
- `reservar_stocks_por_linea`
- `rechazar_pedido_por_stock`
- `cambio_ubicacion_auto`
- `notificacion_voz`
- `control_tarifa_servicios`
- `operador_defecto_en_documento_ingreso`
- `es_bodega_fiscal`
- `habilitar_ingreso_consolidado`
- `bloquear_lp_hh`
- `captura_estiba_ingreso`
- `captura_pallet_no_estandar`
- `Permitir_Verificacion_Consolidada`
- `control_banderas_cliente`
- `priorizar_ubicrec_sobre_ubicest`
- `validar_disponibilidad_ubicaicon_destino`
- `ubicar_tarimas_completas_reabasto`
- `permitir_eliminar_documento_salida`
- `mostrar_area_en_hh`
- `confirmar_codigo_en_picking`
- `control_operador_ubicacion`
- `inferir_origen_en_cambio_ubic`
- `eliminar_documento_salida`
- `operador_picking_realiza_verificacion`
- `permitir_cambio_ubic_producto_picking`
- `despachar_producto_vencido`
- `verificacion_consolidada`
- `permitir_buen_estado_en_reemplazo`
- `industria_motriz`
- `restringir_vencimiento_en_reemplazo`
- `restringir_lote_en_reemplazo`
- `permitir_decimales`
- `permitir_repeticiones_en_ingreso`
- `validar_existencias_inv_ini`
- `calcular_ubicacion_sugerida_ml`
- `ordenar_por_nombre_completo`
- `ordenar_picking_descendente`
- `permitir_reemplazo_picking`
- `permitir_no_encontrado_picking`
- `permitir_reemplazo_verificacion`
- `Permitir_Reemplazo_Picking_Misma_Licencia`
- `filtrar_pedidos_usuario`
- `liberar_stock_despachos_parciales`
- `homologar_lote_vencimiento`
- `escanear_licencia_picking`
- `restringir_areas_sap`
- `interface_SAP`
- `control_pallet_mixto`
- `despacho_automatico_hh`
- `limpiar_campos`
- `permitir_cambio_ubic_recepcion`
- `agrupar_sin_lic_veri_no_cons`
- `advertir_mpq_umbas`
- `Control_Talla_Color`
- `Reservar_primero_almacenaje`

## Flags vinculados a lotes (4)

| Flag | Significado preliminar |
|---|---|
| `homologar_lote_vencimiento` | Forzar consistencia: mismo lote = misma fecha de vencimiento. |
| `restringir_lote_en_reemplazo` | Limita el reemplazo de lote en operaciones (picking/despacho). |
| `restringir_vencimiento_en_reemplazo` | Misma idea para fechas de vencimiento. |
| `despachar_producto_vencido` | Permite/prohíbe despachar productos vencidos. |

## Familias temáticas (heurística)

- **Picking**: `escanear_licencia_picking`, `confirmar_codigo_en_picking`, `permitir_reemplazo_picking`, `permitir_no_encontrado_picking`, `Permitir_Reemplazo_Picking_Misma_Licencia`, `ordenar_picking_descendente`, `operador_picking_realiza_verificacion`, `despacho_automatico_hh`, `permitir_cambio_ubic_producto_picking`.
- **Verificación**: `verificacion_consolidada`, `Permitir_Verificacion_Consolidada`, `agrupar_sin_lic_veri_no_cons`, `permitir_reemplazo_verificacion`.
- **Reemplazo**: `permitir_buen_estado_en_reemplazo`, `restringir_lote_en_reemplazo`, `restringir_vencimiento_en_reemplazo`.
- **Reservas/stocks**: `reservar_stocks_por_linea`, `rechazar_pedido_por_stock`, `liberar_stock_despachos_parciales`, `Reservar_primero_almacenaje`.
- **Pallets/LP**: `bloquear_lp_hh`, `captura_pallet_no_estandar`, `control_pallet_mixto`, `captura_estiba_ingreso`, `Permitir_Reemplazo_Picking_Misma_Licencia`.
- **Documentos**: `permitir_eliminar_documento_salida`, `eliminar_documento_salida`, `habilitar_ingreso_consolidado`.
- **Ubicaciones**: `cambio_ubicacion_auto`, `calcular_ubicacion_sugerida_ml`, `priorizar_ubicrec_sobre_ubicest`, `validar_disponibilidad_ubicaicon_destino`, `ubicar_tarimas_completas_reabasto`, `permitir_cambio_ubic_recepcion`, `inferir_origen_en_cambio_ubic`.
- **Operador**: `operador_defecto_en_documento_ingreso`, `control_operador_ubicacion`.
- **Interfaces**: `interface_SAP`, `restringir_areas_sap`.
- **Voz**: `notificacion_voz`.
- **UI HH**: `mostrar_area_en_hh`, `limpiar_campos`, `despacho_automatico_hh`.

## Naming inconsistente (hallazgo)

Convención mezclada:
- snake_case (mayoría): `escanear_licencia_picking`, `liberar_stock_despachos_parciales`.
- PascalCase: `Permitir_Verificacion_Consolidada`, `Permitir_Reemplazo_Picking_Misma_Licencia`, `Reservar_primero_almacenaje`, `Control_Talla_Color`.
- Typo: `validar_disponibilidad_ubicaicon_destino` (debería ser "ubicacion").

NO renombrar sin coordinar — código VB lee por nombre exacto.

## Cross-refs
- `db-brain://tables/bodega` (extracción posterior)
- `parametrizacion/matriz-killios#bodega`
