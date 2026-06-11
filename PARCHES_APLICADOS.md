# Registro de parches aplicados (TOMWMS)

> Bitácora manual de aplicación de bundles/parches en `dev_2028_merge`.
> 
> Regla: cada vez que se aplique un bundle, agregar una línea con fecha, bundle, alcance, método y estado.

| Fecha (CST) | Bundle | Alcance | Método | Estado | Notas |
|---|---|---|---|---|---|
| 2026-04-24 23:09 | v10_consolidado_2026-04-25 | v8 + v10.C1 + v10.C3 | mixto (apply + manual) | ✅ aplicado | Reemplaza v9 |
| 2026-04-24 23:19 | v11_consolidado_2026-04-25 | v11.E1 | manual | ✅ aplicado | Eliminación handler duplicado vacío |
| 2026-04-24 23:54 | v12_consolidado_2026-04-25 | v12.F1 | manual | ✅ aplicado | Hash anti-duplicado + ColProveedor nominal |
| 2026-04-25 00:01 | v13_consolidado_2026-04-25 | v13.G1 + v13.G2/G3 | manual | ✅ aplicado | Sync lista/grid + delete por stocklink |
| 2026-04-25 00:07 | v14_consolidado_2026-04-25 | v14.H1 + v14.H2 | manual | ✅ aplicado | Eliminar ajuste sin detalle + existencia live |
| 2026-04-25 00:15 | v15_consolidado_2026-04-25 | v15.I1 + v15.I2 | manual | ✅ aplicado | Importer borrador mapeo explícito + recuperación código/nombre producto |
| 2026-04-25 00:35 | v16_consolidado_2026-04-25 | v16.J1 + v16.J2 | manual | ✅ aplicado | Rewrite sync con fallback posicional + espejo borrador con MapearDetalleABorrador |
| 2026-04-25 00:43 | v17_bundle_2026-04-25 | I1 + I2 + J1/J2 | verificación/no-op | ✅ ya incorporado | Bundle en formato `.patch`; contenido ya presente por v15+v16, no cambios adicionales |
| 2026-04-25 01:29 | v18_bundle_2026-04-25 | v18.D2 | manual | ✅ aplicado | Cargar_Detalle usa existenciaLive local para CantidadP sin pisar Cantidad_original |
| 2026-04-25 01:38 | v19_bundle_2026-04-25 | v19.E1 + v19.F1 | apply (.patch) | ✅ aplicado | Llenar_Tipo sincroniza IdTipoAjuste inicial + libera stock_res al eliminar en borrador |
| 2026-04-25 01:55 | v20_bundle | v20 proveedor persistencia (entities + DAL + form) | apply (.patch) | ✅ aplicado | Agrega/persiste datos de proveedor en detalle y borrador |
| 2026-04-25 01:59 | v21_bundle.zip | v21.G1 + v21.G2 | manual (sobre v18) | ✅ aplicado | Existencia live solo en borrador + ColCantidad = Abs(Cantidad_nueva - Cantidad_original) para tipo 3/5 |
| 2026-04-25 02:14 | v22_bundle_2026-04-25.zip | v22 (existencia from BD) | manual | ✅ aplicado | Se elimina refresh live desde Stock; CantidadP usa Cantidad_original persistida |
| 2026-04-25 02:46 | v23_bundle_2026-04-25.zip | v23 reglas eliminar ajuste | apply (.patch) | ✅ aplicado | Ajusta validaciones de eliminación considerando modo borrador y listas en memoria |
| 2026-05-23 18:05 | hh_json_wrapper_global_2026-05-23 | HH base parser JSON colecciones | manual | ✅ aplicado | WebService normaliza recursivamente arreglos a wrapper compatible (items + nombre original) para evitar fix campo a campo |
| 2026-05-23 18:22 | recepcion_lite_hh_ws_2026-05-23 | HH + WSHHRN + DAL recepción/OC | manual | ✅ aplicado | Nuevo GetSingleRec_JSON_Lite + DAL lite + fallback HH + carga diferida de detalle OC en frm_list_rec_prod |
| 2026-06-04 18:35 | fix_caja_master_idempotencia_y_aislamiento_2026-06-04 | HH + DAL recepción + DAL packing/picking | manual | ✅ aplicado | Caja Master: bloqueo HH si LP ya completa + revalidación server-side por línea en GuardarHH_CM y Guardar_Recepcion(pRecepcionCajaMaster=true). Packing: aislamiento por IdPedidoEnc+lic_plate para evitar cruce entre pedidos al empacar |
| 2026-06-04 21:05 | fix_impresion_oc_licencia_madre_fardos_2026-06-04 | BOF Impresión OC (MHS preimpresión) | manual | ✅ aplicado | Se ancla licencia madre en modo Licencia-Bulto para que fardos no cambien de licencia por estados de UI/reimpresión; se bloquea impresión de fardo en modo reimpresión y se restaura licencia madre si detecta desvío |
| 2026-06-10 14:35 | fix_pedido_mi3_cliente_bodega_guardrail_2026-06-10 | BOF DAL Interface Pedido + SQL guardrail opcional | manual | ✅ aplicado | Antes de insertar `trans_pe_enc` se asegura `cliente_bodega` activa por `(IdCliente,IdBodega)` en ambos caminos MI3/NAV. Se documenta y entrega script opcional para bloquear `DELETE` de `cliente_bodega` cuando exista referencia en `trans_pe_enc`. |

## Cómo actualizar esta bitácora

Agregar una fila nueva al final con:
- **Fecha (CST)**
- **Bundle**
- **Alcance** (ids de fix)
- **Método** (`apply`, `manual`, `mixto`)
- **Estado** (✅/⚠️)
- **Notas**
