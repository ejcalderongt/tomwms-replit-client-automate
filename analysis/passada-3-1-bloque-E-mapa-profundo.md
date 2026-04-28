# Ciclo #3.1 Bloque E — Mapa profundo HH↔BOF↔Killios (WebMethod → clsLn → Tabla)

**Generado**: 2026-04-27T03:47:34.360Z
**Alcance**: las 89 clases Logic (.vb) invocadas por el WSHHRN (TOMHHWS.asmx).
**Fuente**: Azure DevOps `dev_2028_merge` /TOMIMSV4/DAL/ (DAL.vbproj .NET 4.8).
**Cruce**: 343 WebMethods × 89 clsLn × 345 tablas Killios.

## 1. Resumen ejecutivo

| Métrica | Valor |
|--|--|
| Archivos .vb parseados | 91 (89 únicos + 2 duplicados ambiguos) |
| Bytes parseados | 2.5 MB |
| Métodos públicos extraídos | 909 (avg 10.0 por clase) |
| Clases con SQL detectado | 86/91 |
| Refs cruzados clsLn↔clsLn | 50 |
| **Tablas únicas tocadas** | **85/345 = 24.64%** |
| Vistas únicas tocadas | 9/220 = 4.09% |
| SPs detectados | 0 — el código WMS usa SQL inline, no StoredProcedures |

**Cobertura tabla (24.6%) vs los 5% del reporte #2 (solo XSDs) = mejora de 5x.** Para llegar a 75-80% del catálogo Killios habría que procesar las otras 534 clsLn (las que NO usa el WSHHRN HH, sino BOF/MI3/WMSWebAPI directos), planificado para un bloque F futuro.

## 2. Hallazgo crítico: el código WMS NO usa StoredProcedures

Confirmado por inspección: las 89 clsLn instancian `New SqlCommand(<sql_inline>, conn)` con `CommandType.Text`. NO se vio un solo `CommandType.StoredProcedure`.

**Killios sí tiene 39 SPs**, pero al revisar el catálogo el detalle es:
- **28 con prefijo `sp_*`** son herramientas de DBA externas (`sp_Blitz`, `sp_BlitzCache`, `sp_BlitzIndex`, `sp_AllNightLog`, `sp_alterdiagram`, etc.) — **no son SPs del negocio**.
- **5 SPs de negocio**: `Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega`, `GetCantidadPesoByProductoBodega`, `GetListaStockByProductoBodega`, `GetResumenStockCantidad`, `Concurrencia`.
- **4 con prefijo `clbd_*`** (lockdown/concurrencia legacy).
- 2 jobs de jornada (`asignar_jornada_laboral`, `SET_CLIENTES_REC`).

**Implicancia para el Brain**: el mapa código→DB se hace 100% sobre tablas+vistas, no sobre SPs. Los 5 SPs de negocio se documentan aparte como utilidades.

## 3. Mapa WebMethod → clsLn → tablas

| Métrica | Valor |
|--|--|
| WMs analizados | 344 |
| WMs que invocan ≥1 clase Logic | 340 (98.8%) |
| WMs con cobertura de tablas | 328 (95.3%) |
| Promedio clases invocadas por WM | 2.29 |
| Promedio tablas tocadas por WM | 1.67 |
| Tablas únicas accedidas vía algún WM | 83/345 = 24.1% |

### 3.1 Top 20 WebMethods por impacto (más dependencias)

| WebMethod | Línea asmx | clsLn invocadas | Tablas | Vistas |
|--|--|--|--|--|
| `Get_Disponibilidad_Ubicacion_Destino` | 14779 | 3 | 12 | 2 |
| `Get_Stock_Res_By_Codigo_And_IdUbicacion` | 15931 | 2 | 12 | 2 |
| `Get_Lista_Conteo_Inventario_Inicial_By_IdInventarioEnc` | 7171 | 2 | 6 | 0 |
| `Get_Producto_Talla_Color_JSON` | 18131 | 2 | 6 | 0 |
| `Get_All_PickingUbic_By_IdPickingEnc` | 3103 | 3 | 5 | 0 |
| `Get_All_PickingUbic_By_IdPickingEnc_Tipo` | 3215 | 3 | 5 | 0 |
| `Reemplazo_Producto_En_Picking` | 3980 | 3 | 5 | 0 |
| `Sustituir_Producto_NE_Picking` | 4057 | 3 | 5 | 0 |
| `SustituirProductoNEPicking_Test` | 4129 | 3 | 5 | 0 |
| `Marcar_No_Encontrado` | 4341 | 3 | 5 | 0 |
| `Get_Single_Trans_Picking_Ubic` | 9040 | 3 | 5 | 0 |
| `Guardar_Picking_Cm` | 9153 | 3 | 5 | 0 |
| `Actualizar_Picking_Con_Reemplazo_De_Pallet` | 9242 | 3 | 5 | 0 |
| `Actualizar_PickingUbic_Por_Verificacion` | 9311 | 3 | 5 | 0 |
| `Reemplaza_Producto_Dannado_Menor` | 11943 | 3 | 5 | 0 |
| `Reemplaza_Producto_Dannado_Mayor_Igual` | 12016 | 3 | 5 | 0 |
| `Marcar_Danado` | 12086 | 3 | 5 | 0 |
| `Get_All_PickingUbic_By_PickingEnc` | 14448 | 3 | 5 | 0 |
| `Get_All_PickingUbic_By_PickingEnc_Group` | 14493 | 3 | 5 | 0 |
| `Get_All_PickingUbic_By_IdPickingEnc_For_Verificacion` | 3160 | 2 | 5 | 0 |

### 3.2 Top 20 tablas más leídas/escritas (medido en # WMs que las tocan)

| Tabla | # WMs que la usan | Categoría |
|--|--|--|
| `producto` | 78 | Maestro Producto |
| `producto_bodega` | 54 | Maestro Producto |
| `stock` | 41 | Stock |
| `stock_res` | 34 | Stock |
| `trans_packing_enc` | 24 | Transaccional |
| `stock_rec` | 22 | Stock |
| `trans_picking_ubic` | 20 | Transaccional |
| `trans_picking_enc` | 18 | Transaccional |
| `trans_re_enc` | 17 | Transaccional |
| `tarea_hh` | 14 | Sistema HH |
| `producto_presentacion` | 14 | Maestro Producto |
| `bodega` | 11 | Maestro Bodega |
| `bodega_ubicacion` | 11 | Maestro Bodega |
| `trans_ubic_hh_det` | 11 | Transaccional |
| `producto_estado` | 10 | Maestro Producto |
| `trans_inv_detalle` | 10 | Transaccional |
| `bodega_area` | 8 | Maestro Bodega |
| `trans_re_det` | 8 | Transaccional |
| `trans_inv_resumen` | 7 | Transaccional |
| `empresa` | 6 | Otro |

## 4. Mapas detallados de los 5 WMs más complejos


### `Get_Disponibilidad_Ubicacion_Destino` (asmx línea 14779, body 44 líneas)
- **Clases Logic invocadas**: `clsLnVW_stock_res`, `clsLnLog_error_wms`, `clsLnLog_error_wms_ubic`
- **Tablas tocadas (12)**: `stock`, `producto_bodega`, `producto`, `propietario_bodega`, `propietarios`, `producto_estado`, `bodega_ubicacion`, `unidad_medida`, `producto_presentacion`, `producto_talla_color`, `talla`, `color`
- **Vistas tocadas (2)**: `vw_stock_res`, `vw_stock_res_pedido`

### `Get_Stock_Res_By_Codigo_And_IdUbicacion` (asmx línea 15931, body 46 líneas)
- **Clases Logic invocadas**: `clsLnVW_stock_res`, `clsLnLog_error_wms`
- **Tablas tocadas (12)**: `stock`, `producto_bodega`, `producto`, `propietario_bodega`, `propietarios`, `producto_estado`, `bodega_ubicacion`, `unidad_medida`, `producto_presentacion`, `producto_talla_color`, `talla`, `color`
- **Vistas tocadas (2)**: `vw_stock_res`, `vw_stock_res_pedido`

### `Get_Lista_Conteo_Inventario_Inicial_By_IdInventarioEnc` (asmx línea 7171, body 46 líneas)
- **Clases Logic invocadas**: `clsLnTrans_inv_detalle_grid`, `clsLnLog_error_wms`
- **Tablas tocadas (6)**: `trans_inv_detalle`, `producto_estado`, `unidad_medida`, `bodega_ubicacion`, `producto_presentacion`, `trans_inv_enc`

### `Get_Producto_Talla_Color_JSON` (asmx línea 18131, body 41 líneas)
- **Clases Logic invocadas**: `clsLnProducto_talla_color`, `clsLnLog_error_wms`
- **Tablas tocadas (6)**: `producto_talla_color`, `talla`, `color`, `trans_oc_det`, `producto_bodega`, `stock`

### `Get_All_PickingUbic_By_IdPickingEnc` (asmx línea 3103, body 46 líneas)
- **Clases Logic invocadas**: `clsLnTrans_picking_ubic`, `clsLnLog_error_wms`, `clsLnLog_error_wms_pick`
- **Tablas tocadas (5)**: `trans_picking_ubic`, `stock_res`, `producto_bodega`, `producto`, `trans_packing_enc`

## 5. Top 15 clsLn por densidad (M+SP+T+V)

| Clase | Bytes | Métodos | SPs | Tablas | Vistas | Subcarpeta |
|--|--|--|--|--|--|--|
| `clsLnVW_stock_res` | 73.4K | 15 | 0 | 12 | 2 | Transacciones |
| `clsLnProducto_talla_color` | 49.2K | 25 | 0 | 6 | 0 | Mantenimientos |
| `clsLnBodega` | 98.4K | 25 | 0 | 3 | 0 | Mantenimientos |
| `clsLnI_nav_barras_rfid_enc` | 39.0K | 19 | 0 | 5 | 0 | Transacciones |
| `clsLnOperador` | 42.0K | 19 | 0 | 4 | 0 | Mantenimientos |
| `clsLnTrans_picking_ubic` | 77.6K | 16 | 0 | 5 | 0 | Transacciones |
| `clsLnI_nav_barras_pallet` | 54.7K | 26 | 0 | 1 | 0 | Interface |
| `clsLnStock` | 54.4K | 18 | 0 | 3 | 1 | Transacciones |
| `clsLnStock_CI` | 72.2K | 16 | 0 | 3 | 2 | Transacciones |
| `clsLnTrans_inv_ciclico` | 67.2K | 20 | 0 | 1 | 0 | Inventario |
| `clsLnTalla` | 32.6K | 17 | 0 | 2 | 0 | Mantenimientos |
| `clsLnTrans_inv_detalle_grid` | 12.3K | 4 | 0 | 6 | 0 | Inventario |
| `clsLnBodega_ubicacion` | 64.8K | 18 | 0 | 1 | 0 | Mantenimientos |
| `clsLnOperador_bodega` | 18.4K | 12 | 0 | 3 | 0 | Mantenimientos |
| `clsLnColor` | 28.9K | 16 | 0 | 1 | 0 | Mantenimientos |

## 6. Distribución de tablas por dominio funcional (las 85 detectadas)

| Dominio | # tablas detectadas | Lista (top 8) |
|--|--|--|
| Otro | 30 | `empresa`, `i_nav_barras_pallet`, `i_nav_transacciones_push`, `menu_rol_op`, `impresora`, `unidad_medida`, `licencia_item`, `i_nav_config_enc`... |
| Transaccional | 27 | `trans_packing_enc`, `trans_picking_ubic`, `trans_picking_enc`, `trans_re_enc`, `trans_ubic_hh_det`, `trans_inv_detalle`, `trans_re_det`, `trans_inv_resumen`... |
| Maestro Producto | 13 | `producto`, `producto_bodega`, `producto_presentacion`, `producto_estado`, `producto_codigos_barra`, `producto_talla_color`, `producto_imagen`, `producto_clasificacion_etiqueta`... |
| Stock | 4 | `stock`, `stock_res`, `stock_rec`, `stock_se_rec` |
| Maestro Bodega | 4 | `bodega`, `bodega_ubicacion`, `bodega_area`, `bodega_muelles` |
| Maestro Operador | 2 | `operador`, `operador_bodega` |
| Maestro Propietario | 2 | `propietario_bodega`, `propietarios` |
| Sistema HH | 1 | `tarea_hh` |

## 7. Limitaciones y pendientes

1. **Cobertura SPs = 0** y eso es correcto — el código WMS no usa SPs de negocio. Documentar esto en el SKILL como hecho estructural.
2. **Cobertura tablas = 25%** desde solo las 89 clsLn del WSHHRN. Para llegar a 75-80% hay que procesar las otras 534 clsLn (BOF + WMSWebAPI/.NET Core 141 .cs + MI3). Bloque F futuro.
3. **2 ambigüedades de nombre** detectadas (mismo `clsLn*.vb` en 2 paths legacy):
   - `clsLnTrans_oc_estado` en `/Transacciones/Movimiento/OrdenCompra/OC_Estado/` y `/Transacciones/OrdenCompra/OC_Estado/`.
   - `clsLnLog_verificacion_bof` en `/Mantenimientos/LogVerificacionBOF/` y `/Transacciones/VerificacionBOF/`.
   Para este bloque se procesaron AMBAS — confirmar con Erik cuál es la canónica.
4. **Ruido de loggers**: los 8 `clsLnLog_error_wms*` aparecen como invocados en casi todos los WMs (logging transversal). El reporte los excluye del análisis de dominio.
5. **Heurística "clase invocada"** se basa en regex `\bclsLn\w+\b` sobre el cuerpo del WM — puede sobreestimar (matchea menciones en strings de error, comentarios, etc.). Refinable con AST si se necesita más precisión.

## 8. Habilita ahora

Con este mapa + el catálogo Killios + los XSDs de #2 ya queda armado el grafo de impacto:
- **"Si modifico la tabla X, qué WMs se rompen?"** → pregunta directa al cruce `tableCallers`.
- **"Qué hace exactamente el WM Y?"** → cruce `cruce[Y]` te da clases Logic + tablas.
- **"Qué tablas son centrales?"** → top de la sección 3.2 (producto, producto_bodega, stock, stock_res, trans_*).

**Próximo paso (#3.1 bloques D+A)**:
- D: tablas `valores_fijos*`, `config_*`, `i_nav_config_*`, `cliente_config*`, `bodega_config*` + lectura de valores reales (read-only Killios) + cruce con `clsLnConfiguracion_usuario_*`.
- A: parsear .config + .resx + AndroidManifest + 16 XSDs ya descargados.
- Estimado: 1.5-2 h.
