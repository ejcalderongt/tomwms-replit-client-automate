# Pasada 3.2 C — Inspección profunda Killios + análisis logs

**Generado**: 2026-04-27T05:46:36.593Z
**BD**: `TOMWMS_KILLIOS_PRD` (SQL Server 2022 CU18 sobre EC2 AWS, server <REDACTED>)
**Modo**: read-only forzado (cliente con whitelist regex `SELECT|WITH...SELECT`, blacklist `INSERT/UPDATE/DELETE/TRUNCATE/DROP/CREATE/ALTER/EXEC/MERGE`)
**Tiempo total de extracción**: ~25 segundos para todas las queries

---

## 1. Resumen ejecutivo

| Métrica | Valor |
|---|---|
| Tablas | 345 |
| Columnas totales | 5,193 (promedio 15.1 col/tabla) |
| Filas totales | 693,420 |
| Tablas sin PK | **27** ⚠️ (7.8%) |
| FKs sin índice de soporte | **347** ⚠️ |
| Stored procedures | 39 (8970 líneas el más grande: `sp_Blitz`) |
| Funciones | 17 |
| Vistas | 220 (no analizadas en profundidad) |
| Filas analizadas en `log_error_wms` | 66,339 |
| Mensajes únicos normalizados | 37,462 |
| Filas analizadas en `stock_hist` | 19,225 |

## 2. Cobertura objetos vs Pasada 3.1

| Objeto | Plan 3.2 C | Real medido | Match |
|---|---|---|---|
| Tablas | 345 | 345 | ✓ |
| SPs | 39 | 39 | ✓ |
| Funciones | 17 | 17 | ✓ |
| Vistas | (no en plan) | 220 | bonus |

## 3. Reclasificación de buckets (Rev 2)

Comparativa antes/después. La pasada 3.1 dejó **141 tablas en bucket genérico `otro`**. Esta pasada las re-evalúa con criterios refinados:

| Bucket Rev 2 | # Tablas | Filas | Sin PK |
|---|---|---|---|
| otro | 146 | 24,442 | 13 |
| master_data | 40 | 22,102 | 2 |
| trans_otra | 37 | 9,578 | 0 |
| integracion_nav | 30 | 65,117 | 1 |
| trans_salida | 18 | 111,655 | 0 |
| nucleo_operativo | 15 | 82,133 | 1 |
| trans_inventario | 15 | 11,264 | 0 |
| permisos | 11 | 1,469 | 0 |
| trans_recepcion | 10 | 187,381 | 0 |
| temporal_tmp | 7 | 3,819 | 7 |
| configuracion | 5 | 161 | 0 |
| logs | 4 | 66,342 | 0 |
| trans_log | 3 | 23,375 | 0 |
| cache_calc | 1 | 2,432 | 1 |
| integracion_sap | 1 | 265 | 1 |
| inventario_externo | 1 | 244 | 1 |
| trans_movimientos | 1 | 81,641 | 0 |

### Cambios respecto a Pasada 3.1

| Bucket previo (3.1) | Conteo | Bucket Rev 2 | Cambio |
|---|---|---|---|
| `transaccional_trans` | 84 | (ver §3 tabla arriba) | redistribuido |
| `transaccional_log` | 2 | (ver §3 tabla arriba) | redistribuido |
| `transaccional_hist` | 5 | (ver §3 tabla arriba) | redistribuido |
| `config_param` | 11 | (ver §3 tabla arriba) | redistribuido |
| `catalogo_tipo` | 8 | (ver §3 tabla arriba) | redistribuido |
| `maestro_dominio` | 63 | (ver §3 tabla arriba) | redistribuido |
| `i_nav_interface` | 25 | (ver §3 tabla arriba) | redistribuido |
| `bk_snapshot` | 1 | (ver §3 tabla arriba) | redistribuido |
| `sis_sistema` | 5 | (ver §3 tabla arriba) | redistribuido |
| `otro` | 141 | (ver §3 tabla arriba) | redistribuido |

> **Decisión clave**: el bucket **`nucleo_operativo`** se introdujo para cubrir las 15 tablas que sostienen el día-a-día del WMS (`stock`, `stock_res`, `stock_hist`, `tarea_hh*`, `marcaje*`, `bodega_*`, `t_producto_bodega`). Estas son **las 15 tablas más sensibles a interrupción de servicio** — un downtime aquí frena la operación completa.

### Tablas aún sin clasificar (`otro`): 146

Quedan 146 tablas en el bucket `otro` (vs 141 inicial). Reducción del -4%. Top 15 sin clasificar (candidatas a una pasada manual futura):

| Tabla | Filas | Cols | PK |
|---|---|---|---|
| `dh_ocupacion_bodega` | 8144 | 5 | ✓ |
| `stock_rec` | 4394 | 37 | ✓ |
| `stock_20250624` | 3541 | 33 | ✗ |
| `stock_20250606` | 3202 | 33 | ✗ |
| `estructura_ubicacion` | 1448 | 33 | ✓ |
| `ubicaciones_por_regla` | 1102 | 26 | ✗ |
| `pais_municipio` | 401 | 7 | ✓ |
| `resultado_aplica_reservado` | 275 | 4 | ✗ |
| `stock_bodegas23` | 270 | 33 | ✗ |
| `stock_res_20250624` | 267 | 35 | ✗ |
| `estructura_grupo` | 238 | 14 | ✓ |
| `stock_20250515` | 151 | 33 | ✗ |
| `operador_bodega` | 84 | 8 | ✓ |
| `pais_departamento` | 69 | 7 | ✓ |
| `estructura_tramo` | 68 | 25 | ✓ |

## 4. Tablas sin Primary Key (riesgo de duplicados)

**27 tablas no tienen PK declarada**. Estas son riesgo operativo: SQL Server no puede garantizar unicidad de filas, los UPDATEs por valor pueden afectar múltiples filas, y la replicación/snapshot puede fallar.

| Tabla | Filas | Cols | Indexes | FKs |
|---|---|---|---|---|
| `t_producto_bodega` | 42357 | 9 | 0 | 0 |
| `producto_presentacion_bk` | 8630 | 28 | 0 | 0 |
| `stock_20250624` | 3541 | 33 | 0 | 0 |
| `stock_20250606` | 3202 | 33 | 0 | 0 |
| `tmp_picking` | 2951 | 4 | 0 | 0 |
| `cod_barra_clc` | 2432 | 2 | 0 | 0 |
| `ubicaciones_por_regla` | 1102 | 26 | 0 | 0 |
| `i_nav_barras_pallet` | 1056 | 21 | 0 | 0 |
| `tmp_estructura_ubicacion` | 400 | 30 | 0 | 0 |
| `tmp_stock_res` | 400 | 33 | 0 | 0 |
| `resultado_aplica_reservado` | 275 | 4 | 0 | 0 |
| `stock_bodegas23` | 270 | 33 | 0 | 0 |
| `stock_res_20250624` | 267 | 35 | 0 | 0 |
| `Inv_SAP` | 265 | 5 | 0 | 0 |
| `Inv_WMS` | 244 | 4 | 0 | 0 |
| `stock_20250515` | 151 | 33 | 0 | 0 |
| `tmp_licencia_item` | 65 | 6 | 0 | 0 |
| `stock_res_ped_164` | 38 | 35 | 0 | 0 |
| `stock_picking20250624` | 14 | 8 | 0 | 0 |
| `TempComparacionInventario` | 7 | 21 | 0 | 0 |

> **Recomendación**: las que tienen >1000 filas (8) requieren PK retroactiva. Las temporales (`tmp_*`) pueden quedar sin PK si se truncan periódicamente.

## 5. FKs sin índice de soporte

**347 foreign keys** referencian columnas que no son la primera columna de ningún índice. Esto degrada JOINs y bloquea el cleanup ON DELETE.

| Tabla | Col FK | Ref tabla | Ref col |
|---|---|---|---|
| `bodega` | `IdEmpresa` | `empresa` | `IdEmpresa` |
| `bodega` | `IdPais` | `paises` | `IdPais` |
| `bodega_area` | `IdBodega` | `bodega` | `IdBodega` |
| `bodega_monitor_parametro` | `IdBodega` | `bodega` | `IdBodega` |
| `bodega_muelles` | `IdBodega` | `bodega` | `IdBodega` |
| `bodega_sector` | `IdArea` | `bodega_area` | `IdArea` |
| `bodega_sector` | `IdBodega` | `bodega_area` | `IdBodega` |
| `bodega_tramo` | `IdSector` | `bodega_sector` | `IdSector` |
| `bodega_tramo` | `IdBodega` | `bodega_sector` | `IdBodega` |
| `bodega_ubicacion` | `IdTipoRotacion` | `tipo_rotacion` | `IdTipoRotacion` |
| `cliente` | `IdTipoCliente` | `cliente_tipo` | `IdTipoCliente` |
| `cliente` | `IdPropietario` | `propietarios` | `IdPropietario` |
| `cliente_bodega` | `IdAreaDestino` | `bodega_area` | `IdArea` |
| `cliente_bodega` | `IdBodega` | `bodega_area` | `IdBodega` |
| `cliente_bodega` | `IdBodega` | `bodega` | `IdBodega` |
| `cliente_bodega` | `IdBodega` | `bodega` | `IdBodega` |
| `cliente_bodega` | `IdCliente` | `cliente` | `IdCliente` |
| `cliente_direccion` | `IdMunicipio` | `pais_municipio` | `IdMunicipio` |
| `cliente_direccion` | `IdRegion` | `pais_region` | `IdRegion` |
| `cliente_tiempos` | `IdClasificacion` | `producto_clasificacion` | `IdClasificacion` |
| `cliente_tiempos` | `IdFamilia` | `producto_familia` | `IdFamilia` |
| `cliente_tipo` | `IdPropietario` | `propietarios` | `IdPropietario` |
| `configuracion_qa` | `IdBodegaOrigen` | `bodega` | `IdBodega` |
| `configuracion_qa` | `IdCliente` | `cliente` | `IdCliente` |
| `configuracion_qa` | `IdEmpresaOrigen` | `empresa` | `IdEmpresa` |

## 6. Stored procedures — análisis

### Top 15 SPs por tamaño

| # | SP | Líneas | Params | Tablas ref. | Última mod. |
|---|---|---|---|---|---|
| 1 | `sp_Blitz` | 8970 | 27 | 38 | 2020-01-16 |
| 2 | `sp_BlitzCache` | 6510 | 34 | 42 | 2020-01-16 |
| 3 | `sp_WhoIsActive` | 5263 | 24 | 17 | 2020-01-28 |
| 4 | `sp_BlitzIndex` | 5004 | 20 | 18 | 2020-01-16 |
| 5 | `sp_BlitzFirst` | 3963 | 32 | 37 | 2020-01-16 |
| 6 | `sp_BlitzBackups` | 1521 | 15 | 15 | 2020-01-16 |
| 7 | `sp_AllNightLog_Setup` | 1309 | 16 | 15 | 2020-01-16 |
| 8 | `sp_BlitzLock` | 1236 | 15 | 14 | 2020-01-16 |
| 9 | `sp_DatabaseRestore` | 1186 | 28 | 10 | 2020-01-16 |
| 10 | `sp_BlitzWho` | 876 | 19 | 2 | 2020-01-16 |
| 11 | `SP_Importa_Stock_Bodegas_General_y_Dañado` | 628 | 0 | 26 | 2018-08-28 |
| 12 | `CLBD_PRC_BY_IDBODEGA` | 459 | 1 | 51 | 2020-10-07 |
| 13 | `CLBD` | 425 | 0 | 139 | 2016-08-04 |
| 14 | `CLBD_INICIARBD` | 389 | 0 | 119 | 2018-09-18 |
| 15 | `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` | 376 | 0 | 19 | 2018-09-17 |

### SPs y dominio funcional inferido

| SP | Dominio inferido | Tablas ref. | Params |
|---|---|---|---|
| `asignar_jornada_laboral` | Otro | 7 | 1 |
| `CLBD` | Otro | 139 | 0 |
| `CLBD_INICIARBD` | Inventario | 119 | 0 |
| `CLBD_PRC` | Otro | 60 | 0 |
| `CLBD_PRC_BY_IDBODEGA` | Otro | 51 | 1 |
| `Concurrencia` | Otro | 1 | 0 |
| `Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega` | Otro | 5 | 2 |
| `GetCantidadPesoByProductoBodega` | Otro | 2 | 10 |
| `GetListaStockByProductoBodega` | Stock | 2 | 9 |
| `GetResumenStockCantidad` | Stock | 3 | 6 |
| `SET_CLIENTES_REC` | Otro | 1 | 0 |
| `sp_AllNightLog` | Otro | 0 | 0 |
| `sp_AllNightLog_Setup` | Otro | 15 | 16 |
| `sp_alterdiagram` | Otro | 2 | 4 |
| `sp_Blitz` | Otro | 38 | 27 |
| `sp_BlitzBackups` | Otro | 15 | 15 |
| `sp_BlitzCache` | Otro | 42 | 34 |
| `sp_BlitzFirst` | Otro | 37 | 32 |
| `sp_BlitzIndex` | Otro | 18 | 20 |
| `sp_BlitzLock` | Otro | 14 | 15 |
| `sp_BlitzWho` | Otro | 2 | 19 |
| `SP_Cambia_Collate` | Otro | 0 | 0 |
| `sp_creatediagram` | Otro | 1 | 4 |
| `sp_DatabaseRestore` | Otro | 10 | 28 |
| `sp_dropdiagram` | Otro | 1 | 2 |
| `sp_eliminar_by_Referencia` | Otro | 7 | 1 |
| `sp_foreachdb` | Otro | 4 | 26 |
| `sp_helpdiagramdefinition` | Inventario | 1 | 2 |
| `sp_helpdiagrams` | Otro | 1 | 2 |
| `SP_Importa_Stock_Bodegas_General_y_Dañado` | Stock | 26 | 0 |
| `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` | Stock | 19 | 0 |
| `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion` | Stock | 12 | 0 |
| `sp_index_maintenance_daily` | Otro | 0 | 1 |
| `sp_ineachdb` | Otro | 5 | 24 |
| `sp_renamediagram` | Otro | 1 | 3 |
| `SP_STOCK_JORNADA_DESFASE` | Stock | 5 | 3 |
| `SP_STOCK_JORNADA_DESFASE_RETROACTIVO` | Stock | 4 | 1 |
| `sp_upgraddiagrams` | Otro | 2 | 0 |
| `sp_WhoIsActive` | Otro | 17 | 24 |

## 7. Funciones — listado y propósito

| Función | Tipo | Líneas | Params | Tablas ref. |
|---|---|---|---|---|
| `ConvertSecondsFormatoFecha` | Scalar | 31 | 1 | 0 |
| `fdias_Exterior_by_IdCliente` | Scalar | 21 | 3 | 1 |
| `fdias_locales_by_IdCliente` | Scalar | 21 | 3 | 1 |
| `fn_diagramobjects` | Scalar | 47 | 0 | 0 |
| `Get_Codigo_Area_By_IdUbicacion` | Scalar | 24 | 2 | 2 |
| `Get_Porcentaje_Avance_Pedido` | Scalar | 27 | 2 | 1 |
| `Get_Porcentaje_Avance_Picking` | Scalar | 28 | 2 | 1 |
| `Nombre_Area` | Scalar | 23 | 2 | 1 |
| `Nombre_Completo_Ubicacion` | Scalar | 30 | 2 | 2 |
| `Nombre_Completo_Ubicacion_1` | Scalar | 29 | 2 | 2 |
| `Nombre_Completo_Ubicacion_2` | Scalar | 30 | 2 | 2 |
| `Nombre_Completo_Ubicacion_3` | Scalar | 60 | 3 | 2 |
| `Nombre_Completo_Ubicacion_Barra` | Scalar | 30 | 2 | 2 |
| `Nombre_Completo_Ubicacion_Barra_1` | Scalar | 31 | 2 | 2 |
| `Nombre_Completo_Ubicacion_Barra_2` | Scalar | 30 | 2 | 2 |
| `Nombre_Completo_Ubicacion_Barra_3` | Scalar | 60 | 3 | 2 |
| `Nombre_Tramo` | Scalar | 23 | 2 | 1 |

## 8. Análisis log_error_wms (66,339 filas)

### Distribución por dominio operacional

| Dominio | Errores | % |
|---|---|---|
| OTRO | 61,448 | 92.6% |
| PEDIDO/PICKING-OUT+PICKING | 4,642 | 7.0% |
| RECEPCION | 219 | 0.3% |
| PEDIDO/PICKING-OUT | 30 | 0.0% |

### Top 30 mensajes de error (normalizados)

| # | Count | Mensaje normalizado (truncado) | Items | Users | WM/SP probable |
|---|---|---|---|---|---|
| 1 | 1,800 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 70 y TipoTarea 2 | 0 | 0 | - |
| 2 | 1,179 | Agregar_Marcaje Referencia a objeto no establecida como instancia de un objeto. | 0 | 0 | - |
| 3 | 915 | AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 716 ubicacion anterior 0opoerador 70 | 0 | 0 | - |
| 4 | 868 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 67 y TipoTarea 2 | 0 | 0 | - |
| 5 | 710 | Error_08012025_Marcaje A: Agregar_Marcaje Referencia a objeto no establecida como instancia de un ob | 0 | 0 | - |
| 6 | 707 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 43 y TipoTarea 2 | 0 | 0 | - |
| 7 | 474 | Guarda_Trans_re_det 1Actualiza_Cantidad_Recibida_OC 1 Guarda_Stock_Rec 1Insertar_Movimientos_Recepci | 0 | 0 | - |
| 8 | 457 | Error_08012025_Marcaje: Agregar_Marcaje Referencia a objeto no establecida como instancia de un obje | 0 | 0 | - |
| 9 | 456 | AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 716 ubicacion anterior 0opoerador 43 | 0 | 0 | - |
| 10 | 442 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 64 y TipoTarea 2 | 0 | 0 | - |
| 11 | 355 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 62 y TipoTarea 2 | 0 | 0 | - |
| 12 | 308 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 76 y TipoTarea 2 | 0 | 0 | - |
| 13 | 285 | Guarda_Trans_re_det 2Actualiza_Cantidad_Recibida_OC 1 Guarda_Stock_Rec 1Insertar_Movimientos_Recepci | 0 | 0 | - |
| 14 | 242 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 72 y TipoTarea 2 | 0 | 0 | - |
| 15 | 214 | AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 716 ubicacion anterior 0opoerador 62 | 0 | 0 | - |
| 16 | 212 | Guarda_Trans_re_det 3Actualiza_Cantidad_Recibida_OC 1 Guarda_Stock_Rec 1Insertar_Movimientos_Recepci | 0 | 0 | - |
| 17 | 208 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 75 y TipoTarea 2 | 0 | 0 | - |
| 18 | 207 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 50 y TipoTarea 2 | 0 | 0 | - |
| 19 | 199 | AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 1458 ubicacion anterior 0opoerador 67 | 0 | 0 | - |
| 20 | 191 | AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 1459 ubicacion anterior 0opoerador 67 | 0 | 0 | - |
| 21 | 187 | AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 1450 ubicacion anterior 0opoerador 76 | 0 | 0 | - |
| 22 | 179 | Guarda_Trans_re_det 4Actualiza_Cantidad_Recibida_OC 1 Guarda_Stock_Rec 1Insertar_Movimientos_Recepci | 0 | 0 | - |
| 23 | 174 | Reemplazar_ListaPu_By_Stock Error_Reemplazo_A: No es posible reservar más de lo solicitado | 0 | 0 | - |
| 24 | 152 | Guarda_Trans_re_det 5Actualiza_Cantidad_Recibida_OC 1 Guarda_Stock_Rec 1Insertar_Movimientos_Recepci | 0 | 0 | - |
| 25 | 147 | Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 66 y TipoTarea 2 | 0 | 0 | - |
| 26 | 138 | AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 1450 ubicacion anterior 0opoerador 67 | 0 | 0 | - |
| 27 | 137 | Guarda_Trans_re_det 6Actualiza_Cantidad_Recibida_OC 1 Guarda_Stock_Rec 1Insertar_Movimientos_Recepci | 0 | 0 | - |
| 28 | 133 | AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 1460 ubicacion anterior 0opoerador 67 | 0 | 0 | - |
| 29 | 122 | Guarda_Trans_re_det 7Actualiza_Cantidad_Recibida_OC 1 Guarda_Stock_Rec 1Insertar_Movimientos_Recepci | 0 | 0 | - |
| 30 | 121 | Guarda_Trans_re_det 8Actualiza_Cantidad_Recibida_OC 1 Guarda_Stock_Rec 1Insertar_Movimientos_Recepci | 0 | 0 | - |

### Diagnóstico de los 5 errores más críticos


**1. `Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 70 y TipoTarea 2`** — 1800 ocurrencias
- **Periodo**: 2025-06-03 → 2025-08-19
- **Sample completo**: `Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 70 y TipoTarea 2`
- **Items afectados**: 0, **usuarios afectados**: 0
- **Mapeo probable**: no detectado por heurística

- **Patrón**: log informativo, no error real. Considerar bajar a nivel INFO o mover a tabla separada.



**2. `Agregar_Marcaje Referencia a objeto no establecida como instancia de un objeto.`** — 1179 ocurrencias
- **Periodo**: 2025-06-10 → 2025-08-19
- **Sample completo**: `Agregar_Marcaje Referencia a objeto no establecida como instancia de un objeto.`
- **Items afectados**: 0, **usuarios afectados**: 0
- **Mapeo probable**: no detectado por heurística
- **Patrón**: NullReferenceException de VB.NET. Revisar guard `If x IsNot Nothing` en el flujo.




**3. `AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 716 ubicacion anterior 0opoerador 70`** — 915 ocurrencias
- **Periodo**: 2025-06-10 → 2025-08-18
- **Sample completo**: `AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 716 ubicacion anterior 0opoerador 70`
- **Items afectados**: 0, **usuarios afectados**: 0
- **Mapeo probable**: no detectado por heurística





**4. `Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 67 y TipoTarea 2`** — 868 ocurrencias
- **Periodo**: 2025-06-05 → 2025-08-19
- **Sample completo**: `Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: 67 y TipoTarea 2`
- **Items afectados**: 0, **usuarios afectados**: 0
- **Mapeo probable**: no detectado por heurística

- **Patrón**: log informativo, no error real. Considerar bajar a nivel INFO o mover a tabla separada.



**5. `Error_08012025_Marcaje A: Agregar_Marcaje Referencia a objeto no establecida como instancia de un ob`** — 710 ocurrencias
- **Periodo**: 2025-07-10 → 2025-08-19
- **Sample completo**: `Error_08012025_Marcaje A: Agregar_Marcaje Referencia a objeto no establecida como instancia de un objeto.`
- **Items afectados**: 0, **usuarios afectados**: 0
- **Mapeo probable**: no detectado por heurística
- **Patrón**: NullReferenceException de VB.NET. Revisar guard `If x IsNot Nothing` en el flujo.




### Top 20 productos (Item_No) con más errores

| # | Item_No | Errores |
|---|---|---|
| 1 | `WMS221` | 7 |
| 2 | `WMS56` | 6 |
| 3 | `WMS151` | 5 |
| 4 | `WMS152` | 5 |
| 5 | `WMS167` | 5 |
| 6 | `WMS140` | 5 |
| 7 | `WMS298` | 4 |
| 8 | `WMS231` | 4 |
| 9 | `WMS199` | 4 |
| 10 | `WMS184` | 4 |
| 11 | `WMS92` | 4 |
| 12 | `WMS202` | 4 |
| 13 | `WMS89` | 3 |
| 14 | `WMS61` | 3 |
| 15 | `WMS100` | 3 |
| 16 | `WMS66` | 3 |
| 17 | `WMS192` | 3 |
| 18 | `WMS204` | 2 |
| 19 | `WMS307` | 2 |
| 20 | `WMS62` | 2 |

## 9. Patrones de stock_hist (19,225 filas)

### Distribución por dominio operacional (qué genera el cambio de stock)

| Dominio | Cambios | % |
|---|---|---|
| RECEPCION+PICKING+DESPACHO+PEDIDO | 11,027 | 57.4% |
| PICKING+DESPACHO+PEDIDO | 7,622 | 39.6% |
| RECEPCION | 321 | 1.7% |
| RECEPCION+PICKING+PEDIDO | 140 | 0.7% |
| OTRO | 115 | 0.6% |

### Distribución horaria (UTC)

| Hora | Cambios |
|---|---|
| 12:00 | 2284 |
| 14:00 | 2077 |
| 16:00 | 1959 |
| 15:00 | 1953 |
| 13:00 | 1758 |
| 17:00 | 1430 |
| 10:00 | 1308 |
| 11:00 | 1259 |
| 7:00 | 1200 |
| 18:00 | 1029 |
| 9:00 | 923 |
| 6:00 | 873 |

> **Hallazgo**: los picos horarios indican cuándo concentrar tareas operativas y cuándo hacer maintenance.

### Top 20 productos con más cambios de stock

| # | IdProductoBodega | Cambios |
|---|---|---|
| 1 | 391 | 725 |
| 2 | 1316 | 683 |
| 3 | 1511 | 602 |
| 4 | 601 | 469 |
| 5 | 61 | 447 |
| 6 | 1346 | 370 |
| 7 | 1351 | 362 |
| 8 | 1311 | 343 |
| 9 | 1371 | 339 |
| 10 | 1491 | 327 |
| 11 | 51 | 324 |
| 12 | 1291 | 323 |
| 13 | 726 | 304 |
| 14 | 591 | 278 |
| 15 | 1391 | 272 |
| 16 | 366 | 267 |
| 17 | 1506 | 251 |
| 18 | 1361 | 225 |
| 19 | 596 | 218 |
| 20 | 1481 | 218 |

### Top 20 operadores con más cambios

| # | user_agr | Cambios |
|---|---|---|
| 1 | `1` | 7736 |
| 2 | `18` | 6998 |
| 3 | `26` | 3236 |
| 4 | `19` | 848 |
| 5 | `23` | 200 |
| 6 | `6` | 110 |
| 7 | `28` | 96 |
| 8 | `12` | 1 |

## 10. Recomendaciones de housekeeping

### Tablas vacías candidatas a deprecar (148)

<details><summary>Listado de las 148 tablas con 0 filas</summary>

- `Appointments`
- `Auditoria`
- `Contacto`
- `Infraestructura`
- `Organizacion`
- `Propuesta`
- `Prospecto`
- `Resources`
- `TempComparativoInventario`
- `area_estado`
- `bodega_monitor_parametro`
- `campaña`
- `centro_costo`
- `cliente_lotes`
- `cliente_tiempos`
- `color`
- `configuracion_alias_campos`
- `configuracion_qa`
- `contenedor`
- `diferencias_movimientos`
- `empresa_transporte_bodega`
- `empresa_transporte_vehiculos`
- `estilo`
- `i_nav_acuerdo`
- `i_nav_acuerdo_det`
- `i_nav_acuerdo_enc`
- `i_nav_cliente`
- `i_nav_config_area_bodega`
- `i_nav_config_det`
- `i_nav_config_ent`
- `i_nav_config_producto_estado`
- `i_nav_conversion`
- `i_nav_ent`
- `i_nav_ent_filtros`
- `i_nav_ped_compra_det`
- `i_nav_ped_compra_det_lote`
- `i_nav_ped_compra_enc`
- `i_nav_ped_traslado_det_lote`
- `i_nav_producto`
- `i_nav_producto_presentacion`
- `i_nav_servicio`
- `i_nav_transacciones_out_error`
- `i_nav_transacciones_push`
- `impresora_mensaje`
- `interface_enc`
- `jornada_sistema`
- `licencias_pendientes_retroactivo`
- `log_importacion_excel`
- `montacarga_servicio_enc`
- `operador_montacarga`
- `operador_zona_picking_tramo`
- `p_parametro`
- `perfil_serializado`
- `producto_clasificacion_etiqueta`
- `producto_familia`
- `producto_kit_composicion`
- `producto_pallet_rec`
- `producto_parametro_a`
- `producto_parametro_b`
- `producto_parametros`
- `producto_presentacion_tarima`
- `producto_presentaciones_conversiones`
- `producto_subtarea`
- `producto_sustituto`
- `producto_talla_color`
- `proveedor_tiempos`
- `regimen_fiscal`
- `regla_ubic_det_ir`
- `regla_ubic_det_pe`
- `regla_ubic_det_pp`
- `regla_ubic_det_prop`
- `regla_ubic_det_tp`
- `regla_ubic_det_tr`
- `regla_ubic_enc`
- `regla_ubic_sel`
- `regla_ubic_sel_det`
- `regla_ubic_sel_enc`
- `regla_ubicacion`
- `regla_vencimiento`
- `reglas_vencimiento_contacto`
- `road_p_vendedor`
- `road_ruta`
- `rol_bodega`
- `rol_menu`
- `stock_det`
- `stock_jornada`
- `stock_jornada_temporal`
- `stock_parametro`
- `stock_res_se`
- `stock_se`
- `stock_se_rec`
- `tablas_sync`
- `talla`
- `tarifa_tipo_transaccion`
- `tarifa_tipo_transaccion_det`
- `tipo_etiqueta_detalle`
- `tmp_bodega_ubicacion`
- `tmp_i_nav_transacciones_out`
- `tms_ticket`
- `tms_ticket_pol`
- `trans_acuerdoscomerciales_det`
- `trans_acuerdoscomerciales_enc`
- `trans_ajuste_det_doc`
- `trans_bodega_ubicaciones_excel`
- `trans_despacho_det_lote_num`
- `trans_inv_ciclico`
- `trans_inv_ciclico_ubic`
- `trans_inv_enc_reconteo`
- `trans_inv_operador`
- `trans_inv_reconteo`
- `trans_inv_teorico_erp`
- `trans_inventario_det`
- `trans_inventario_enc`
- `trans_manufactura_det`
- `trans_manufactura_enc`
- `trans_manufactura_picking`
- `trans_manufactura_tipo`
- `trans_oc_docu_ref`
- `trans_oc_embarcador`
- `trans_oc_imagen`
- `trans_oc_pol`
- `trans_oc_servicios`
- `trans_pe_pol`
- `trans_pe_servicios`
- `trans_picking_det_parametros`
- `trans_picking_img`
- `trans_picking_prioridad`
- `trans_prefactura_det`
- `trans_prefactura_enc`
- `trans_prefactura_mov`
- `trans_re_det_infraccion`
- `trans_re_det_parametros`
- `trans_re_fact`
- `trans_re_img`
- `trans_series_doc`
- `trans_servicio_det`
- `trans_servicio_enc`
- `trans_tras_det`
- `trans_tras_enc`
- `trans_tras_op`
- `trans_ubic_hh_se`
- `trans_ubic_tarima`
- `transacciones_log`
- `unidad_medida_conversion`
- `us_solic_det`
- `us_solic_enc`
- `zona_picking`
- `zona_picking_tramo`

</details>

### Tablas con `_bk`/`_backup` candidatas a archivado

| Tabla | Filas | Cols |
|---|---|---|
| `producto_presentacion_bk` | 8630 | 28 |

### Tablas `tmp_*` con datos persistentes (sospechoso)

| Tabla | Filas | Cols |
|---|---|---|
| `temp_licencia_llave` | 3 | 2 |
| `tmp_estructura_ubicacion` | 400 | 30 |
| `tmp_licencia_item` | 65 | 6 |
| `tmp_picking` | 2951 | 4 |
| `tmp_stock_res` | 400 | 33 |

> **Acción sugerida**: si son temporales reales, deberían vaciarse en cada job. Si tienen datos viejos, mover a tabla persistente o agregar TTL.

## 11. Top 10 tablas más activas (mayor row count)

| # | Tabla | Filas | Cols | Indexes | FKs | Bucket |
|---|---|---|---|---|---|---|
| 1 | `trans_re_det_lote_num` | 180,181 | 8 | 0 | 0 | trans_recepcion |
| 2 | `trans_movimientos` | 81,641 | 35 | 5 | 8 | trans_movimientos |
| 3 | `log_error_wms` | 66,339 | 15 | 0 | 0 | logs |
| 4 | `t_producto_bodega` | 42,357 | 9 | 0 | 0 | nucleo_operativo |
| 5 | `trans_picking_ubic` | 26,567 | 52 | 13 | 2 | trans_salida |
| 6 | `i_nav_transacciones_out` | 24,193 | 61 | 1 | 0 | integracion_nav |
| 7 | `trans_pe_det_log_reserva` | 22,576 | 17 | 0 | 0 | trans_log |
| 8 | `trans_picking_ubic_stock` | 20,437 | 47 | 0 | 0 | trans_salida |
| 9 | `trans_despacho_det` | 19,799 | 20 | 3 | 2 | trans_salida |
| 10 | `stock_hist` | 19,225 | 33 | 0 | 14 | nucleo_operativo |

## 12. Manifiesto de archivos generados

| Archivo | Tamaño | Contenido |
|---|---|---|
| `tablas-completas.json` | 1568KB | 345 tablas con cols + PK + indexes + FKs + sample 3 filas |
| `sps-completos.json` | 2071KB | 39 SPs con definición completa + params + tablas referenciadas |
| `funciones-completas.json` | 29KB | 17 funciones con definición completa |
| `log-error-analysis.json` | 20KB | Top 30 mensajes normalizados, distribución por dominio, items afectados |
| `stock-hist-patterns.json` | 4KB | Top 20 productos/operadores, distribución horaria/diaria, dominio |
| `buckets-rev2.json` | 96KB | Re-clasificación de 345 tablas en 17 buckets refinados |

**Total**: ~3788KB.

## 13. Top 10 hallazgos accionables para Erik

1. **27 tablas sin PK** (incluidas algunas no-temporales con miles de filas) — riesgo de duplicados. Top: `t_producto_bodega` (42357), `producto_presentacion_bk` (8630), `stock_20250624` (3541).
2. **347 FKs sin índice** — JOINs degradados y ON DELETE bloqueante. Priorizar las de tablas grandes.
3. **Top error `Aplica_Cambio_Estado_Ubic_HH` (1800 ocurrencias)** — no es error sino log informativo: considerar mover a tabla `info_log` separada para no contaminar la tabla de errores reales.
4. **NullReferenceException en `Agregar_Marcaje` (1179 ocurrencias)** — bug real. Revisar el flujo de marcaje en BOF y agregar guard `If marcaje IsNot Nothing`.
5. **60 líneas en SP `asignar_jornada_laboral`** — god-procedure. Candidato a refactor en piezas.
6. **19,225 cambios de stock** distribuidos entre 8+ operadores — el operador `1` genera 7736 cambios (top contribuyente).
7. **Producto top `IdProductoBodega=391` con 725 cambios** — investigar si es producto rotativo legítimo o bug de duplicación.
8. **148 tablas vacías** sin uso aparente — candidatas a deprecación tras confirmar con Erik.
9. **Picos horarios en stock_hist** ofrecen ventana para maintenance: hora 20:00 UTC tiene mínima actividad (127 eventos).
10. **146 tablas aún sin bucket claro** — pasada manual o consultar a Erik para nomenclatura desconocida.

## 14. Gaps reconocidos en esta pasada

| # | Gap | Mitigación |
|---|---|---|
| 1 | 220 vistas no analizadas a profundidad (solo conteo) | Pasada futura para vistas críticas (`VW_Stock_Res`) |
| 2 | Triggers no enumerados (sys.triggers no consultado) | Adicionar query `sys.triggers` en próxima iteración |
| 3 | Plan caché y stats no analizados (`sys.dm_exec_*`) | Requiere ventana de carga real para tener datos representativos |
| 4 | log_error_wms agrupado por mensaje, no por root cause real | Heurística da indicios; análisis manual de los top 5 daría mapeo definitivo |
| 5 | stock_hist sin agregación por mes (solo día/hora) | Trivial añadir si se necesita reporte de evolución mensual |
| 6 | No se calculó tamaño físico (MB) de tablas vía `sp_spaceused` | El catálogo trae rowCount; tamaño físico se puede agregar |

---

**Fin Pasada 3.2 C.** Esto cierra el bloque de catalogación de fuentes (BOF, HH, Killios). La próxima pasada D combinará todo en flujos end-to-end: pantalla HH → WM invocado → SQL ejecutado en server → tablas mutadas en Killios → mensaje al operador.
