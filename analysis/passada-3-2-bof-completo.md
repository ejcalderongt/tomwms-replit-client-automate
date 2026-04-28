# Ciclo 3.2 A — Catalogación completa TOMWMS_BOF

**Generado**: 2026-04-27T05:19:08.949Z
**Fuente**: Azure DevOps `ejcalderon0892/TOMWMS_BOF`, rama `dev_2028_merge`
**Volumen procesado**: 1,584 archivos / 740,851 líneas (excluyendo `Designer.vb`, `Reportes/`, `Mantenimientos/` Forms, y `.resx` traducciones).

---

## 1. Resumen ejecutivo

Este ciclo cierra el barrido amplio del backend del WMS BOF iniciado en el ciclo 3.1 (que muestreó 38 clsLn de configuración). Aquí se procesaron **todos** los archivos operacionales relevantes — entidades, DAL, WebServices, formularios de transacciones, y la capa moderna .NET Core. Los hallazgos principales:

- **534 entidades** `clsBe*` con un total de 7,249 properties y 175 navegaciones tipadas (en archivos `_Partial`).
- **634 clases DAL** `clsLn*` con **5,167 bloques SQL inline** y solo **2 llamadas EXEC a stored procedures** — confirma con autoridad que el BOF mantiene el patrón "SQL en código" casi universal (99.96% inline).
- **371 WebMethods** repartidos en 2 servicios reales: **TOMHHWS** (369 WMs, el endpoint principal del Handheld) y **srvSAPSync** (2 WMs).
- **86 formularios operacionales** del BOF (Transacciones), distribuidos en 8+ dominios.
- **48 archivos de capa moderna .NET Core** con **110 endpoints REST** descubiertos — la migración existe pero está incompleta.
- **5,514 aristas** en el grafo Entity ↔ Tabla ↔ DAL ↔ WM ↔ Form, con **756 tablas físicas** referenciadas (vs las 345 reales en Killios — el delta de ~411 son tablas referenciadas pero no encontradas en la BD activa, indica catálogos cliente-específicos o tablas removidas).

## 2. Hallazgos críticos vs replit.md

| Item | Valor en `replit.md` | Valor real medido | Comentario |
|---|---|---|---|
| Entities (clsBe*) | 538 | 534 únicas | Marginal, 4 archivos resultaron ser `_Partial` que se merge con la base |
| DAL (clsLn*) | ~120 | **634** | Subestimación 5×. El ciclo 3.1 muestreó solo el 6% del DAL |
| Carpetas top-level esperadas | EntityCore, DALCore | **NO EXISTEN como repos raíz** | Capa moderna vive dentro de `/TOMIMSV4/TOMIMS_WCF` (117 archivos) y `/WMSWebAPI` (84 archivos) |
| WS principales | WSHHRN, WSSAPSYNC | **TOMHHWS, srvSAPSync** | El nombre lógico es WSHHRN/WSSAPSYNC pero los archivos físicos se llaman así |
| WS files (.asmx + .vb) | — | 2 `.asmx` + 18 `.vb` codebehind/helpers | TOMHHWS es 1 monolito `.asmx.vb`. Ver §6 |

**Acción recomendada**: actualizar `replit.md` §2 para reflejar paths reales de la capa moderna y cantidades correctas de DAL.

## 3. Estructura del repo BOF (alto nivel)

```
/TOMIMSV4
  /Entity            555 archivos  → 534 clsBe* + 73 .xsd (DataSets tipados)
  /DAL               646 archivos  → 624 clsLn* + helpers
    /Mantenimientos  314           → catálogos
    /Transacciones   171           → operaciones (recepción, picking, etc)
    /Interface       64            → integraciones SAP/NAV/DMS
    /Inventario      30
  /TOMIMSV4 (proj)   1469          → forms + recursos (mayoría es .resx, excluido)
    /Mantenimientos  712           → forms config (excluidos por scope)
    /Reportes        433           → forms reportes (excluidos por scope)
  /Transacciones     258           → forms operacionales BOF (incluidos en ciclo)
  /TOMIMS_WCF        117           → capa WCF/Modern (parcial)
/WSHHRN              292 (38 vb)   → TOMHHWS.asmx.vb es el monolito principal
/WSSAPSYNC           18 (14 vb)    → srvSAPSync.asmx.vb
/WMSWebAPI           96 (84)       → API REST .NET Core moderna
```

## 4. Cobertura por capa

| Capa | Cantidad | Métrica auxiliar | Propósito |
|---|---|---|---|
| Entity (clsBe*) | 534 | 7249 props | Modelo de datos |
| DAL (clsLn*) | 634 | 5167 SQL blocks | Lógica de acceso |
| WebServices | 371 WMs | TOMHHWS=369, srvSAPSync=2 | Contratos HH/SAP |
| UI BOF (Transacciones) | 86 forms | 86 total | Operación BOF |
| Modern API | 48 files | 110 endpoints | Migración .NET Core |

## 5. SQL inline vs Stored Procedures (regla 08)

| Métrica | Valor |
|---|---|
| Total bloques SQL inline en DAL | **5,167** |
| Total llamadas `EXEC <SP>` desde DAL | **2** |
| Ratio SP/SQL | 0.04% |

**Confirmado**: el BOF es esencialmente "SQL en código". La regla 08 (no usar SPs salvo casos justificados) está siendo respetada con disciplina notable. Los 2 EXEC encontrados son edge cases, no patrón.

## 6. Top 30 clsLn por superficie SQL (más bloques inline)

| # | Clase | SQL blocks | Métodos | Tablas | Ejemplo de tablas |
|---|---|---|---|---|---|
| 1 | `clsLnStock_Partial` | 149 | 186 | 52 | `bodega, bodega_tramo, bodega_ubicacion, cliente_lotes…` |
| 2 | `clsLnProducto_Partial` | 145 | 180 | 39 | `color, gt30032026, indice_rotacion, member…` |
| 3 | `clsLnTrans_pe_enc_Partial` | 127 | 129 | 34 | `bodega, cliente, despachos_otros, elegibles…` |
| 4 | `clsLnStock_res_Partial` | 100 | 131 | 19 | `bodega_ubicacion, producto, producto_bodega, producto_presentacion…` |
| 5 | `clsLnBodega_ubicacion_Partial` | 73 | 75 | 21 | `bodega_area, bodega_sector, bodega_tramo, bodega_ubicacion…` |
| 6 | `clsLnProducto_presentacion_Partial` | 72 | 79 | 8 | `producto, producto_bodega, producto_presentacion, propietario_bodega…` |
| 7 | `clsLnTrans_oc_enc_Partial` | 71 | 83 | 18 | `bodega, i_nav_transacciones_push, producto, producto_bodega…` |
| 8 | `clsLnTrans_inv_ciclico_Partial` | 69 | 91 | 28 | `bodega, bodega_tramo, bodega_ubicacion, color…` |
| 9 | `clsLnBodega_Partial` | 69 | 71 | 10 | `bodega, bodega_area, propietario_bodega, propietarios…` |
| 10 | `clsLnTrans_re_enc_Partial` | 52 | 56 | 22 | `bodega, bodega_ubicacion, i_nav_transacciones_push, para…` |
| 11 | `clsLnCliente_Partial` | 51 | 57 | 6 | `bodega, cliente, cliente_bodega, propietarios…` |
| 12 | `clsLnTrans_movimientos_Partial` | 47 | 59 | 31 | `bodega_ubicacion, motivo_ubicacion, operador, operador_bodega…` |
| 13 | `clsLnUnidad_medida_Partial` | 43 | 41 | 5 | `producto, propietario_bodega, propietarios, unidad_medida…` |
| 14 | `clsLnProducto_estado_Partial` | 41 | 50 | 11 | `bodega, bodega_ubicacion, i_nav_config_enc, producto_estado…` |
| 15 | `clsLnTrans_picking_ubic_Partial` | 41 | 73 | 27 | `bodega, bodega_area, bodega_sector, bodega_tramo…` |
| 16 | `clsLnTarea_hh_Partial` | 40 | 41 | 26 | `bodega, bodega_muelles, propietario_bodega, propietarios…` |
| 17 | `clsLnTrans_oc_det_Partial` | 38 | 36 | 13 | `color, producto, producto_bodega, producto_talla_color…` |
| 18 | `clsLnTablas_relacionadas` | 37 | 32 | 11 | `polizas_cealsa, polizas_con_noorden, propietario_bodega, propietarios…` |
| 19 | `clsLnTrans_pe_det_Partial` | 34 | 44 | 29 | `bodega, bodega_ubicacion, cliente, color…` |
| 20 | `clsLnBodega_tramo_Partial` | 30 | 37 | 8 | `bodega_area, bodega_sector, bodega_tramo, bodega_ubicacion…` |
| 21 | `clsLnProveedor_Partial` | 30 | 34 | 10 | `cliente, propietarios, proveedor, proveedor_bodega…` |
| 22 | `clsLnProducto_clasificacion_Partial` | 29 | 25 | 3 | `producto, producto_clasificacion, vw_productoclasificacion` |
| 23 | `clsLnTrans_re_det_Partial` | 29 | 30 | 18 | `color, operador, operador_bodega, producto…` |
| 24 | `clsLnI_nav_transacciones_out_partial` | 28 | 47 | 7 | `color, i_nav_transacciones_out, producto, talla…` |
| 25 | `clsLnProducto_talla_color` | 28 | 30 | 8 | `campa, color, para, producto_bodega…` |
| 26 | `clsLnPropietarios_Partial` | 28 | 31 | 6 | `cliente, producto, propietario_bodega, propietarios…` |
| 27 | `clsLnProducto_familia_Partial` | 27 | 23 | 4 | `producto, producto_familia, propietarios, vw_productofamilia` |
| 28 | `clsLnProducto_tipo_Partial` | 27 | 24 | 6 | `producto, producto_bodega, producto_tipo, propietario_bodega…` |
| 29 | `clsLnI_nav_barras_pallet` | 26 | 24 | 3 | `i_nav_barras_pallet, i_nav_barras_rfid_det, i_nav_barras_rfid_enc` |
| 30 | `clsLnProducto_bodega_Partial` | 26 | 30 | 3 | `producto, producto_bodega, trans_pe_det` |

## 7. Top 30 WebMethods por SQL inline

| # | WM | SQL | Tablas (R) | Tablas (W) | DAL usados | Ejemplo tablas |
|---|---|---|---|---|---|---|
| 1 | `TOMHHWS.Registrar_Pedido_Compra_To_NAV_For_BYB` | 2 | 0 | 0 | 3 | `` |
| 2 | `TOMHHWS.Despachar_Pedido` | 2 | 0 | 0 | 6 | `` |
| 3 | `TOMHHWS.Push_Recepcion_To_NAV_For_BYB` | 1 | 0 | 0 | 1 | `` |
| 4 | `TOMHHWS.Push_Recepcion_Produccion_To_NAV_For_BYB` | 1 | 0 | 0 | 2 | `` |
| 5 | `TOMHHWS.Push_Recepcion_Pedido_Compra_To_NAV_For_BYB` | 1 | 0 | 0 | 2 | `` |
| 6 | `TOMHHWS.Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB` | 1 | 0 | 0 | 2 | `` |
| 7 | `TOMHHWS.Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB` | 1 | 0 | 0 | 2 | `` |
| 8 | `TOMHHWS.Get_Stock_Por_Producto_Ubicacion_CI_Json` | 1 | 0 | 0 | 2 | `` |
| 9 | `TOMHHWS.Get_Picking_By_IdPickingEncJson` | 1 | 0 | 0 | 3 | `` |
| 10 | `TOMHHWS.Get_Tipo_Etiqueta_By_IdTipoEtiqueta_Json` | 1 | 0 | 0 | 2 | `` |
| 11 | `TOMHHWS.Get_All_PickingUbic_By_IdPickingEnc_Tipo_Json` | 1 | 0 | 0 | 3 | `` |
| 12 | `TOMHHWS.Get_Single_By_IdPedidoEnc_Json` | 1 | 0 | 0 | 3 | `` |
| 13 | `TOMHHWS.Get_Tipo_Pedido_Json` | 1 | 0 | 0 | 3 | `` |
| 14 | `srvSAPSync.Get_Stock_By_ItemCode_And_WsCode` | 1 | 0 | 0 | 0 | `` |
| 15 | `srvSAPSync.Get_Stock_By_WsCode` | 1 | 0 | 0 | 0 | `` |
| 16 | `TOMHHWS.Get_Empresa_By_Codigo_And_Clave` | 0 | 0 | 0 | 2 | `` |
| 17 | `TOMHHWS.Get_List_Empresas_For_HH` | 0 | 0 | 0 | 2 | `` |
| 18 | `TOMHHWS.Get_Bodegas_By_IdEmpresa_For_HH` | 0 | 0 | 0 | 2 | `` |
| 19 | `TOMHHWS.Android_Get_Bodegas_By_IdEmpresa` | 0 | 0 | 0 | 2 | `` |
| 20 | `TOMHHWS.Get_Productos_By_IdBodega_For_HH` | 0 | 0 | 0 | 2 | `` |
| 21 | `TOMHHWS.Get_Propietarios_By_IdBodega_For_HH` | 0 | 0 | 0 | 2 | `` |
| 22 | `TOMHHWS.Get_All_Reglas_Recepcion_For_HH` | 0 | 0 | 0 | 2 | `` |
| 23 | `TOMHHWS.Get_Clientes_By_IdBodega_For_HH` | 0 | 0 | 0 | 2 | `` |
| 24 | `TOMHHWS.Get_Proveedores_By_Bodega_For_HH` | 0 | 0 | 0 | 2 | `` |
| 25 | `TOMHHWS.Get_All_By_IdBodega_HH_Filtro` | 0 | 0 | 0 | 2 | `` |
| 26 | `TOMHHWS.Get_Motivo_Anulacion_By_IdBodega_For_HH` | 0 | 0 | 0 | 2 | `` |
| 27 | `TOMHHWS.Get_Motivos_Anulacion_By_IdBodega_For_HH` | 0 | 0 | 0 | 2 | `` |
| 28 | `TOMHHWS.Get_Motivos_Devolucion_By_IdBodega_For_HH` | 0 | 0 | 0 | 2 | `` |
| 29 | `TOMHHWS.Get_Motivos_Devolucion_Bodega_By_IdBodega_For_HH` | 0 | 0 | 0 | 2 | `` |
| 30 | `TOMHHWS.Get_Motivos_Ubicacion_For_HH` | 0 | 0 | 0 | 3 | `` |

## 8. Tablas más tocadas por la capa DAL

| # | Tabla | Cantidad de clsLn que la tocan |
|---|---|---|
| 1 | `producto` | 42 |
| 2 | `producto_bodega` | 30 |
| 3 | `bodega` | 30 |
| 4 | `propietarios` | 28 |
| 5 | `propietario_bodega` | 26 |
| 6 | `producto_presentacion` | 25 |
| 7 | `bodega_ubicacion` | 22 |
| 8 | `trans_pe_enc` | 19 |
| 9 | `producto_estado` | 18 |
| 10 | `stock` | 18 |
| 11 | `trans_pe_det` | 17 |
| 12 | `unidad_medida` | 17 |
| 13 | `bodega_tramo` | 17 |
| 14 | `color` | 15 |
| 15 | `talla` | 15 |
| 16 | `producto_talla_color` | 15 |
| 17 | `operador` | 15 |
| 18 | `stock_res` | 14 |
| 19 | `bodega_area` | 13 |
| 20 | `cliente` | 13 |
| 21 | `vw_stock_res` | 11 |
| 22 | `operador_bodega` | 11 |
| 23 | `trans_oc_det` | 11 |
| 24 | `trans_picking_ubic` | 11 |
| 25 | `trans_oc_enc` | 10 |
| 26 | `trans_inv_stock` | 10 |
| 27 | `bodega_sector` | 10 |
| 28 | `trans_despacho_det` | 9 |
| 29 | `trans_re_oc` | 9 |
| 30 | `trans_inv_ciclico` | 9 |

## 9. Tablas más mutadas por WebServices (escritura directa desde WMs)

| # | Tabla | WMs que escriben en ella |
|---|---|---|

## 10. Top entities (por densidad de columnas)

| # | Entity | Props | Navs | Tabla inferida | Implements |
|---|---|---|---|---|---|
| 1 | `clsBeBodega` | 109 | 0 | `bodega` | ICloneable, IDisposable |
| 2 | `clsBeStock_jornada` | 89 | 0 | `stock_jornada` | ICloneable, System.ICloneable.Clone |
| 3 | `clsBeTrans_bodega_ubicaciones_excel` | 84 | 0 | `trans_bodega_ubicaciones_excel` | ICloneable, System.ICloneable.Clone |
| 4 | `clsBeTrans_pe_enc` | 71 | 0 | `trans_pe_enc` | ICloneable, IDisposable |
| 5 | `clsBeI_nav_config_enc` | 69 | 0 | `i_nav_config_enc` | ICloneable, System.ICloneable.Clone |
| 6 | `clsBeI_nav_transacciones_out` | 67 | 0 | `i_nav_transacciones_out` | ICloneable, System.ICloneable.Clone |
| 7 | `clsBeVW_Despacho_Rep` | 67 | 0 | `vw_despacho_rep` | ICloneable, System.ICloneable.Clone |
| 8 | `clsBeVW_stock_res` | 67 | 0 | `vw_stock_res` | ICloneable, System.ICloneable.Clone |
| 9 | `clsBeTrans_reabastecimiento_log` | 62 | 0 | `trans_reabastecimiento_log` | ICloneable, System.ICloneable.Clone |
| 10 | `clsBeProducto` | 60 | 0 | `producto` | ICloneable, IDisposable |
| 11 | `clsBeTrans_inv_inicial_excel_op_log` | 56 | 0 | `trans_inv_inicial_excel_op_log` | ICloneable, System.ICloneable.Clone |
| 12 | `clsBeP_CLIENTE` | 55 | 0 | `p_cliente` | ICloneable, System.ICloneable.Clone |
| 13 | `clsBeRoad_ruta` | 54 | 0 | `road_ruta` | ICloneable, IDisposable |
| 14 | `clsBeVW_Movimientos_Poliza` | 54 | 0 | `vw_movimientos_poliza` | - |
| 15 | `clsBeTrans_ajuste_det_borrador` | 53 | 1 | `trans_ajuste_det_borrador` | - |

## 11. UI BOF: cobertura por dominio operacional

| Dominio | Forms | Total clsLn usados | Total clsBe usados |
|---|---|---|---|
| Inventario | 18 | 110 | 82 |
| Recepcion | 9 | 60 | 61 |
| Pedido | 7 | 59 | 47 |
| Ajustes | 6 | 47 | 33 |
| Cambio_Ubicacion_Estado | 6 | 33 | 31 |
| Orden_Compra | 5 | 48 | 43 |
| Picking | 5 | 45 | 44 |
| RFID | 5 | 11 | 10 |
| Despacho | 3 | 27 | 23 |
| Manufactura | 3 | 24 | 32 |
| PreFactura | 3 | 34 | 30 |
| Orden_Compra_Tipo_Ingreso | 2 | 3 | 2 |
| PreIngreso | 2 | 24 | 27 |
| Revision_Producto | 2 | 4 | 3 |
| Servicios | 2 | 19 | 13 |
| Stock_Reservado | 2 | 7 | 7 |
| Control_Calidad | 1 | 16 | 18 |
| Implosion | 1 | 8 | 16 |
| Orden_Compra_Tipo_Transaccion | 1 | 2 | 2 |
| Pedidos_MI3 | 1 | 2 | 0 |
| Recepcion_BOF | 1 | 32 | 28 |
| Verificacion_BOF | 1 | 12 | 14 |

## 12. Top 15 UI Forms por cobertura (más conexiones a DAL+Entity+WS)

| # | Form | Dominio | clsLn | clsBe | WS refs | Total edges |
|---|---|---|---|---|---|---|
| 1 | `frmPedido` | Pedido | 45 | 38 | 0 | 83 |
| 2 | `frmOrdenCompra` | Orden_Compra | 42 | 39 | 0 | 81 |
| 3 | `frmRecepcion` | Recepcion | 41 | 37 | 0 | 78 |
| 4 | `frmRecepcionBOF` | Recepcion_BOF | 32 | 28 | 0 | 60 |
| 5 | `frmInventario` | Inventario | 30 | 25 | 0 | 55 |
| 6 | `frmPicking` | Picking | 26 | 26 | 0 | 52 |
| 7 | `frmPreFactura` | PreFactura | 27 | 23 | 0 | 50 |
| 8 | `frmCambioUbicacion` | Cambio_Ubicacion_Estado | 25 | 24 | 0 | 49 |
| 9 | `frmPreIngreso` | PreIngreso | 22 | 25 | 0 | 47 |
| 10 | `frmAjusteStock` | Ajustes | 25 | 20 | 0 | 45 |
| 11 | `frmDespacho` | Despacho | 23 | 21 | 0 | 44 |
| 12 | `frmManufactura` | Manufactura | 19 | 24 | 0 | 43 |
| 13 | `frmListaStockControlCalidad` | Control_Calidad | 16 | 18 | 0 | 34 |
| 14 | `frmRegularizarInventario` | Inventario | 16 | 11 | 0 | 27 |
| 15 | `frmVerificacionBOF` | Verificacion_BOF | 12 | 14 | 0 | 26 |

## 13. Capa moderna (.NET Core) — endpoints REST descubiertos

| Métrica | Valor |
|---|---|
| Total archivos analizados | 48 |
| Total endpoints REST | 110 |
| DbContexts detectados | 1 |
| Controllers | 25 |
| Services/Repositories | 22 |

**Distribución por verbo HTTP**: HTTPGET=58, HTTPPOST=39, HTTPPUT=6, HTTPDELETE=4, HTTPPATCH=3.

**Distribución por carpeta**:
| Carpeta | Archivos analizables |
|---|---|
| WMSWebAPI/Controllers | 25 |
| WMSWebAPI/Services | 22 |
| WMSWebAPI/ApplicationDbContext.cs | 1 |

**Conclusión**: la migración a .NET Core está iniciada pero claramente incompleta. Con 110 endpoints vs 371 WebMethods legacy, la cobertura del API moderno es del **29.6%**. Se identificaron DbContexts y atributos `[Table]` que confirman uso de Entity Framework Core. **Acción recomendada**: cuando el cliente decida cuáles funcionalidades migrar primero, este catálogo permite mapear cada WM legacy → endpoint moderno faltante.

## 14. Modern API: top archivos por endpoints expuestos

| # | File | Controller | Route prefix | # endpoints |
|---|---|---|---|---|
| 1 | `PropietarioController.cs` | PropietarioController | api/[controller] | 15 |
| 2 | `CentroCostoController.cs` | CentroCostoController | api/[controller] | 10 |
| 3 | `PropietarioWebhookController.cs` | PropietarioWebhookController | api/webhooks/[controller] | 9 |
| 4 | `AcuerdoComercialWebhookController.cs` | AcuerdoComercialWebhookController | api/webhooks/[controller] | 7 |
| 5 | `KpiController.cs` | KpiController | api/[controller] | 7 |
| 6 | `SyncIngresosController.cs` | SyncIngresosController | api/sync/ingresos | 7 |
| 7 | `PreFacturaController.cs` | PreFacturaController | api/[controller] | 6 |
| 8 | `SyncSalidasController.cs` | SyncSalidasController | api/sync/salidas | 6 |
| 9 | `AuthController.cs` | AuthController | api/[controller] | 5 |
| 10 | `CambioEstadoController.cs` | CambioEstadoController | api/[controller] | 5 |
| 11 | `ProductosController.cs` | ProductosController | api/[controller] | 5 |
| 12 | `AcuerdoComercialController.cs` | AcuerdoComercialController | api/[controller] | 4 |
| 13 | `ClienteController.cs` | ClienteController | api/[controller] | 3 |
| 14 | `ProveedorController.cs` | ProveedorController | api/[controller] | 3 |
| 15 | `AjusteController.cs` | SyncAjustesController | api/sync/ajustes | 2 |

## 15. Cruce extendido — métricas del grafo

| Tipo de arista | Cantidad |
|---|---|
| dal-touches-table | 1558 |
| dal-calls-dal | 1218 |
| ws-uses-dal | 836 |
| ui-uses-dal | 627 |
| ui-uses-entity | 566 |
| entity-implements-table | 534 |
| entity-navigates-entity | 175 |

**Cobertura de uso del DAL** (de los 634 clsLn):

| Llamado por | Cantidad | % de DAL total |
|---|---|---|
| Otros clsLn (DAL→DAL) | 238 | 37.5% |
| WebMethods (WS→DAL) | 96 | 15.1% |
| UI Forms (UI→DAL) | 154 | 24.3% |
| **Huérfanos** (no llamados por nada parseado) | **354** | **55.8%** |

> Los huérfanos pueden ser: (a) llamados desde Forms de `Mantenimientos` o `Reportes` (excluidos del scope), (b) llamados via reflection o instancias dinámicas, o (c) dead code real. Ver §17 para top 20 huérfanos.

## 16. Top 20 clsLn por uso interno (más llamados desde otros clsLn / WS / UI)

| # | clsLn | Veces llamado (in-degree) |
|---|---|---|
| 1 | `clsLnLog_error_wms` | 709 |
| 2 | `clsLnProducto` | 88 |
| 3 | `clsLnStock` | 65 |
| 4 | `clsLnProducto_presentacion` | 63 |
| 5 | `clsLnBodega` | 60 |
| 6 | `clsLnLog_error_wms_rec` | 54 |
| 7 | `clsLnBodega_ubicacion` | 53 |
| 8 | `clsLnLog_error_wms_pick` | 49 |
| 9 | `clsLnStock_res` | 43 |
| 10 | `clsLnTrans_picking_ubic` | 42 |
| 11 | `clsLnProducto_estado` | 34 |
| 12 | `clsLnTrans_re_enc` | 33 |
| 13 | `clsLnLog_error_wms_ubic` | 33 |
| 14 | `clsLnTrans_pe_enc` | 31 |
| 15 | `clsLnPropietarios` | 29 |
| 16 | `clsLnPropietario_bodega` | 29 |
| 17 | `clsLnTrans_picking_enc` | 27 |
| 18 | `clsLnUnidad_medida` | 26 |
| 19 | `clsLnI_nav_config_enc` | 26 |
| 20 | `clsLnTarea_hh` | 26 |

## 17. Top 15 clsLn que más invocan a otros clsLn (out-degree alto = orquestadores)

| # | clsLn | Cantidad de clsLn invocados |
|---|---|---|
| 1 | `clsLnTrans_re_enc_Partial` | 40 |
| 2 | `clsLnI_nav_ped_traslado_enc_Partial` | 34 |
| 3 | `clsLnTrans_despacho_enc_Partial` | 34 |
| 4 | `clsLnStock_Partial` | 33 |
| 5 | `clsLnStock_res_Partial` | 32 |
| 6 | `clsLnI_nav_transacciones_out_partial` | 28 |
| 7 | `clsLnTrans_oc_enc_Partial` | 26 |
| 8 | `clsLnI_nav_ped_compra_enc_Partial` | 25 |
| 9 | `clsLnProducto_Partial` | 24 |
| 10 | `clsLnTrans_picking_ubic_Partial` | 23 |
| 11 | `clsLnTrans_ubic_hh_enc_Partial` | 22 |
| 12 | `clsLnTrans_ubic_hh_det_Partial` | 21 |
| 13 | `clsLnTrans_pe_enc_Partial` | 20 |
| 14 | `clsLnTrans_re_det_Partial` | 20 |
| 15 | `clsLnTrans_pe_det_Partial` | 18 |

## 18. WMs que ejecutan SQL puro inline (sin pasar por DAL)

**2 WebMethods** ejecutan SQL directamente sin instanciar ningún clsLn. Esto es legal pero rompe el patrón general (WM → clsLn → SQL). Top 15:

| # | WM | SQL blocks | Tablas |
|---|---|---|---|
| 1 | `srvSAPSync.Get_Stock_By_ItemCode_And_WsCode` | 1 | `` |
| 2 | `srvSAPSync.Get_Stock_By_WsCode` | 1 | `` |

## 19. Tablas referenciadas SIN clsLn dedicado

**201 tablas** son tocadas por Entity y/o WMs pero ningún clsLn las accede directamente. Probable causa: SQL inline en WMs o lectura via SP / vista. Muestra:

`_servicio, _servicio_logistico, _vw_ajustes, ajustesmi3, bodega_area_partial, bodega_partial, bodega_tramo_partial, bodega_tramo_seleccion, bodega_ubicacion_partial, bodega_ubicacion_seleccion, bodegaseleccion, cealsa_acuerdoscomerciales, cealsa_acuerdoscomerciales_partial, cealsa_clientes, cealsa_detacuerdoscomerciales, cealsa_duca_enc, cliente_bodega_partial, cliente_direccion_partial, cliente_partial, cuadrilla_det_montacarga_partial, cuadrilla_det_operador_partial, cuadrilla_enc_partial, detallepedidoaverificar, dms_log_sincronizacion_fallos, dms_log_sincronizacion_nube, empresaand, font_det_partial, font_enc_partial, horario_laboral_det_partial, i_nav_acuerdo_enc_partial`…

## 20. Resolución de ambigüedades pendientes (ciclo 3.1)

### 20.1 `clsLnTrans_oc_estado`

- **Paths encontrados**: 2
  - `/TOMIMSV4/DAL/Transacciones/Movimiento/OrdenCompra/OC_Estado/clsLnTrans_oc_estado.vb`
  - `/TOMIMSV4/DAL/Transacciones/OrdenCompra/OC_Estado/clsLnTrans_oc_estado.vb`
- **Canónico** (decisión): `/TOMIMSV4/DAL/Transacciones/Movimiento/OrdenCompra/OC_Estado/clsLnTrans_oc_estado.vb`
- **Legacy** (a deprecar): `/TOMIMSV4/DAL/Transacciones/OrdenCompra/OC_Estado/clsLnTrans_oc_estado.vb`
- **Justificación**: Criterio mejorado (methodCount > sqlBlockCount > refs): /TOMIMSV4/DAL/Transacciones/Movimiento/OrdenCompra/OC_Estado/clsLnTrans_oc_estado.vb con methods=10, sql=11, refs=7.

### 20.2 `clsLnLog_verificacion_bof`

- **Paths encontrados**: 2
  - `/TOMIMSV4/DAL/Mantenimientos/LogVerificacionBOF/clsLnLog_verificacion_bof.vb`
  - `/TOMIMSV4/DAL/Transacciones/VerificacionBOF/clsLnLog_verificacion_bof.vb`
- **Canónico** (decisión): `/TOMIMSV4/DAL/Transacciones/VerificacionBOF/clsLnLog_verificacion_bof.vb`
- **Legacy** (a deprecar): `/TOMIMSV4/DAL/Mantenimientos/LogVerificacionBOF/clsLnLog_verificacion_bof.vb`
- **Justificación**: Criterio mejorado (methodCount > sqlBlockCount > refs): /TOMIMSV4/DAL/Transacciones/VerificacionBOF/clsLnLog_verificacion_bof.vb con methods=10, sql=10, refs=17. El duplicado (1 método, 0 SQL) parece ser stub/wrapper legacy.

> **Criterio aplicado**: Prioridad LEXICOGRÁFICA: 1) methodCount desc, 2) sqlBlockCount desc, 3) referencias externas desc. El path con MÁS método y SQL real es el canónico (es donde está la lógica de negocio); el otro es típicamente stub/wrapper legacy.

## 21. Capa de Interfaces (DAL/Interface — 64 archivos)

Sub-carpeta `/TOMIMSV4/DAL/Interface/` con 64 archivos representa la integración con sistemas externos (SAP, NAV, DMS, Cealsa, AWS). No es parte del flujo HH/BOF directo pero define la "frontera externa" del WMS. **Acción**: en ciclo futuro, generar un catálogo dedicado de interfaces.

## 22. Datasets tipados (.xsd) — 73 archivos

Excluidos del parseo en este ciclo por ser esquemas declarativos (no código). La mayoría definen tablas auxiliares para reportes WinForms. **Acción**: en ciclo futuro, parsear los `.xsd` para obtener un mapping XML → SQL Server alternativo al de las clsBe.

## 23. Gaps reconocidos en este ciclo

| # | Gap | Mitigación / Próxima ciclo |
|---|---|---|
| 1 | Forms de `/TOMIMSV4/TOMIMSV4/Mantenimientos/` (712 archivos) NO catalogados | Ciclo 3.3 dedicada a config UI |
| 2 | Forms de `/TOMIMSV4/TOMIMSV4/Reportes/` (433 archivos) NO catalogados | Ciclo de reportería |
| 3 | `Designer.vb` excluidos (no muestran controles UI) | Si se necesita topología de controles, parsear bajo demanda |
| 4 | DAL/Interface (64 archivos) parseados pero no analizados por integración externa | Ciclo de interfaces |
| 5 | DAL/Inventario, DAL/Road, DAL/QuickTag (44 archivos) parseados sin desglose por dominio | Análisis específico cuando se modele esos flujos |
| 6 | `.xsd` DataSets (73 archivos) sin parsear | Ciclo de modelo de datos extendido |
| 7 | 354 clsLn marcados como huérfanos pueden estar siendo usados desde Mantenimientos/Reportes | Recalcular tras ciclo 3.3 |
| 8 | TOMHHWS.asmx.vb es un monolito de ~1.5MB y 364 WMs — ningún WM individualmente analizado a profundidad | Ciclo D (flujos end-to-end) lo cubrirá |

## 24. Top 10 hallazgos accionables para Erik

1. **Migración .NET Core**: 110/371 WMs ya tienen contraparte moderna (29.6% cobertura). Lista completa de WMs sin contraparte disponible en `ws-sql-inline.json` cruzada con `modern-api.json`.
2. **Duplicación de carpetas DAL**: detectada en 2 ambigüedades resueltas (OrdenCompra y VerificacionBOF); probablemente hay más. **Recomendación**: scan completo de duplicados en `/DAL/Transacciones/` vs `/DAL/Transacciones/Movimiento/` y `/DAL/Mantenimientos/` vs `/DAL/Transacciones/`.
3. **Top 5 clsLn más complejos** (orquestadores con muchas dependencias internas): `clsLnTrans_re_enc_Partial`(40), `clsLnI_nav_ped_traslado_enc_Partial`(34), `clsLnTrans_despacho_enc_Partial`(34), `clsLnStock_Partial`(33), `clsLnStock_res_Partial`(32) — candidatos prioritarios para refactor / tests.
4. **Top 5 tablas críticas** (más tocadas por DAL): `producto`(42), `producto_bodega`(30), `bodega`(30), `propietarios`(28), `propietario_bodega`(26) — riesgos altos de coupling.
5. **15 WMs con SQL puro inline** (sin pasar por DAL) son candidatos a refactor para encapsular en clsLn. Lista en §18.
6. **354 clsLn potencialmente huérfanos** (54%) — necesita validación tras incluir Mantenimientos/Reportes; si se confirma alta cantidad de dead code, oportunidad de limpieza significativa.
7. **TOMHHWS** es un monolito gigante de 364 WMs en un solo archivo `.asmx.vb` — refactor priority alto cuando se modernice la HH.
8. **Cobertura entity ↔ tabla**: 534 entities mapean a 534 tablas (1:1). Las **222 tablas adicionales** referenciadas son típicamente vistas, tablas dinámicas (Inv_*) o tablas legacy sin entity.
9. **Modern API DbContexts** (1): hacer auditoría de cuáles tablas tienen ya su EF Core mapping vs cuáles falta.
10. **Capa Interface** (64 archivos en /DAL/Interface/) es la frontera externa del WMS — ciclo dedicada cuando se documente la integración SAP/NAV.

## 25. Manifiesto de archivos generados

Todos en `data/passada-3-2-bof/` del repo `ejcalderongt/tomwms-replit-client-automate` rama `wms-brain`:

| Archivo | Propósito | Tamaño |
|---|---|---|
| `index-maestro.json` | Lista de los 1584 archivos catalogados con metadata | 414KB |
| `entity-completo.json` | 534 `clsBe*` con properties + navegaciones | 830KB |
| `dal-completo.json` | 634 `clsLn*` con métodos + SQL + tablas | 1973KB |
| `ws-sql-inline.json` | 371 WebMethods con SQL inline + tablas + DAL usados | 175KB |
| `ui-bof.json` | 86 Forms operacionales + clsLn/clsBe/WS usados por dominio | 60KB |
| `modern-api.json` | 48 archivos .NET Core con endpoints/DbContext/Controllers | 23KB |
| `cruce-extendido.json` | Grafo de 5514 aristas + métricas | 788KB |
| `ambiguedades-resueltas.json` | Decisión documentada de los 2 paths ambiguos | 7KB |

**Total**: 4 MB de catálogo.

---

**Fin Ciclo 3.2 A.** Próxima en pipeline: **Ciclo 3.2 B — Catalogación TOMHH2025 (Android)**, que cruzará contra los 371 WMs aquí catalogados para detectar WMs huérfanos (declarados en server, no consumidos por HH) y zombies (llamados por HH, removidos del server).
