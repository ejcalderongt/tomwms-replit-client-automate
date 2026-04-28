# Capa 2 — Parametros a nivel BODEGA

> La bodega es la entidad MAS RICA en parametros (101 cols en BYB).
> Cada cliente tiene N bodegas y cada bodega tiene su propia
> configuracion. Aqui se decide casi todo el comportamiento operativo.

## Tabla origen

`bodega` (101 cols en BYB)

## Filas por cliente

| Cliente | Bodegas (catalogo) | Bodegas activas | Notas |
|---|---|---|---|
| BECOFARMA | a contar | a contar | 1 unica bodega operativa |
| K7 | a contar | a contar | 6 bodegas + 5 prorrateo + Z17 |
| MAMPA | a contar | a contar | 31 bodegas (multi-tienda) |
| BYB | a contar | a contar | 2 bodegas |
| CEALSA | a contar | a contar | 2 bodegas (B01 GENERAL, B02 FISCAL) |

## Preguntas operativas Erik (29-abr-2026)

Cada bodega responde a un set de preguntas que determinan su sendero:

1. **¿Recibe produccion?** → ver `i_nav_config_enc.codigo_proveedor_produccion` no nulo
2. **¿Hay proceso de calidad?** → ¿hay bodega de calidad? ¿cual? → ver bodegas con nombre "CALIDAD"/"QC"/"QA"
3. **¿En que estado se recibe el producto por defecto?** → `bodega.IdProductoEstado` o `IdProductoEstadoNE`
4. **¿Genera etiqueta al recibir o viene pre-impresa (escanea LP)?** → `bodega.IdTipoEtiquetaLicencia` y flags relacionados
5. **¿Tiene transferencias a otras bodegas WMS?** → `bodega.requerir_cliente_es_bodega_wms=True`
6. **¿Las transferencias salen del WMS (a bodega no-WMS)?** → flag inverso
7. **¿Es bodega fiscal?** → `bodega.es_bodega_fiscal`
8. **¿Es bodega de devolucion?** → `bodega.es_devolucion`
9. **¿Hace packing?** → `bodega.empaque_tarima`
10. **¿Picking por voz?** → `bodega.notificacion_voz`
11. **¿Picking consolidado?** → `bodega.verificacion_consolidada` + `Permitir_Verificacion_Consolidada`
12. **¿Maneja talla y color?** → `bodega.control_talla_color` (caso MAMPA)
13. **¿Sugerencia de ubicacion con ML?** → `bodega.calcular_ubicacion_sugerida_ml`

## Catalogo de parametros (101 cols agrupadas)

### Identidad y catalogo
- `IdBodega`, `IdPais`, `IdEmpresa`, `codigo`, `codigo_barra`, `nombre`,
  `nombre_comercial`, `direccion`, `telefono`, `email`, `encargado`
- `coordenada_x`, `coordenada_y`, `zoom`, `largo`, `ancho`, `alto`

### Ubicaciones default
- `ubic_recepcion`, `ubic_picking`, `ubic_despacho`, `ubic_merma`
- `ubic_producto_ne` (no comercial)

### Estados producto
- `IdProductoEstado` (default), `IdProductoEstadoNE` (no comercial)
- `IdMotivoUbicacionDañadoPicking`
- `Id_Motivo_Ubic_Reabasto`

### Politicas operativas (flags principales)

| Flag | Funcion |
|---|---|
| `activo` | Bodega activa |
| `es_bodega_fiscal` | Bodega fiscal |
| `es_devolucion` | Bodega para devoluciones |
| `empaque_tarima` | Hace packing |
| `industria_motriz` | DEPRECATED. Diseñado para una venta unica de repuestos automotriz que nunca llego a operar productivamente. Va junto con `IDPRODUCTOPARAMETROA/B` (catalogo producto) que tampoco se usa. Mantener por compatibilidad pero NO priorizar en WebAPI. |
| `materia_prima` | Bodega de materia prima |
| `kit` | Bodega para kits |
| `control_poliza` | Maneja polizas |
| `requerir_cliente_es_bodega_wms` | Las transferencias requieren que cliente sea otra bodega WMS |
| `control_cliente_en_detalle` | Control cliente a nivel detalle |

### Reservas y stock
- `reservar_stocks_por_linea`
- `rechazar_pedido_por_stock`
- `liberar_stock_despachos_parciales`
- `permitir_despacho_parcial`, `permitir_despacho_multiple`

### Recepcion
- `IdTipoIngresoOC` (tipo doc ingreso default)
- `requerir_documento_ref`
- `captura_estiba_ingreso`
- `captura_pallet_no_estandar`
- `bloquear_lp_hh`
- `permitir_repeticiones_en_ingreso`
- `tipo_pantalla_recepcion`
- `priorizar_ubicrec_sobre_ubicest`
- `permitir_cambio_ubic_recepcion`
- `cambio_ubicacion_auto`
- `inferir_origen_en_cambio_ubic`
- `homologar_lote_vencimiento`
- `validar_existencias_inv_ini`

### Picking
- `confirmar_codigo_en_picking`
- `escanear_licencia_picking`
- `permitir_reemplazo_picking`
- `permitir_no_encontrado_picking`
- `Permitir_Reemplazo_Picking_Misma_Licencia`
- `permitir_cambio_ubic_producto_picking`
- `operador_picking_realiza_verificacion`
- `notificacion_voz` (pick by voice)
- `TIPO_PANTALLA_PICKING`
- `escanear_muelle_picking`
- `mover_producto_zona_muelle`
- `agrupar_sin_lic_veri_no_cons`
- `ordenar_picking_descendente`
- `ordenar_por_nombre_completo`
- `filtrar_pedidos_usuario`

### Verificacion
- `Permitir_Verificacion_Consolidada`
- `verificacion_consolidada`
- `tipo_pantalla_verificacion`
- `permitir_reemplazo_verificacion`
- `restringir_vencimiento_en_reemplazo`, `restringir_lote_en_reemplazo`
- `dias_maximo_vencimiento_reemplazo`
- `PERMITIR_BUEN_ESTADO_EN_REEMPLAZO`
- `fotografia_verificacion`

### Reabasto
- `top_reabastecimiento_manual`
- `ubicar_tarimas_completas_reabasto`

### Etiquetas e impresion
- `IdTamañoEtiquetaUbicacionDefecto`
- `IdTipoEtiqueta` (en bodega)
- `IdTipoEtiquetaLicencia`
- `IdSimbologiaLicencia`
- `ImprimeBarrasPicking`, `ImprimeBarrasPacking`
- `margen_impresion`
- `mostrar_area_en_hh`

### Transferencias / Salida
- `IdTipoTransaccion`, `IdTipoTransaccionSalida`
- `Generar_pedido_ingreso_bodega_destino`
- `trasladar_lotes_doc_ingreso`
- `marcar_registros_enviados_mi3`
- `generar_recepcion_auto_bodega_destino`
- `recibir_producto_auto_bodega_destino`
- `transferir_ubicacion`
- `genera_guia_remision`

### ERP / Interfaces
- `interface_SAP` (en bodega! distinto del de empresa)
- `restringir_areas_sap`
- `codigo_bodega_erp`

### Decimales
- `permitir_decimales`

### Talla / Color (caso MAMPA)
- `control_talla_color`

### ML / Inteligencia
- `calcular_ubicacion_sugerida_ml`

### Especifico vencimientos
- `despachar_producto_vencido`
- `dias_limite_retroactivo`
- `rango_dias_documentos`

### Eliminacion docs
- `permitir_eliminar_documento_salida`
- `eliminar_documento_salida`

### Voice / HH
- `despacho_automatico_hh`
- `limpiar_campos`

### Pallet mixto
- `control_pallet_mixto`

### Auditoria
- `user_agr`, `fec_agr`, `user_mod`, `fec_mod`
- `Cuenta_Ingreso_Mercancias`, `Cuenta_Egreso_Mercancias`

### Otros (no claros)
- `valor_porcentaje_iva`
- `control_tarifa_servicios`
- `operador_defecto_en_documento_ingreso`
- `habilitar_ingreso_consolidado`
- `IdMotivoUbicacionDañadoPicking`
- `control_banderas_cliente`
- `validar_disponibilidad_ubicaicon_destino` (sic, typo en BD)
- `control_operador_ubicacion`
- `horario_ejecucion_historico`
- `ruta_cdn`
- `advertir_mpq_umbas`

## Cross-cliente (PENDIENTE — proxima iteracion)

Tabla matriz `flag x cliente x bodega`:

| Flag | BECOFARMA bodega 1 | K7 bod 1..6 | MAMPA bod 1..31 | BYB bod 1..2 | CEALSA bod 1..2 |
|---|---|---|---|---|---|
| es_bodega_fiscal | ? | ? | ? | ? | bod 2 = True |
| empaque_tarima | ? | ? | ? | ? | n/a (col no existe) |
| notificacion_voz | ? | ? | ? | ? | ? |
| control_talla_color | ? | ? | True (probable) | ? | ? |
| ... | | | | | |

## Schema drift entre clientes

CEALSA tiene schema mas viejo. Faltan en CEALSA cols que SI estan en BYB
(detectado para `trans_pe_tipo`):

```
Cols en BYB pero NO en CEALSA (trans_pe_tipo):
  IdProductoEstado, IdPropietario, control_cliente_en_detalle,
  empaque_tarima, escanear_muelle_picking, genera_guia_remision,
  mover_producto_zona_muelle
```

PENDIENTE: medir mismo drift en `bodega` cross-cliente.

## Implicaciones para WebAPI

Cada flag de bodega es una decision de comportamiento. La WebAPI debe:
1. Cargar el set de flags de la bodega activa al iniciar sesion HH/BOF.
2. Activar/desactivar capabilities en runtime segun los flags.
3. Para schema drift: mantener compatibilidad con clientes con schema
   mas viejo (cols ausentes = valor default).
