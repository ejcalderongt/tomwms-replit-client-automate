---
id: DIFF-BOF-2023-VS-2028
tipo: code-deep-flow
estado: vigente
ramas: [dev_2023_estable, dev_2028_merge]
tags: [code-deep-flow/diff, repo/bof]
---

# Diff BOF: `dev_2023_estable` vs `dev_2028_merge`

> Generado por F2 del Atlas BOF/HH 2023↔2028 cliente-aware (2026-04-30).
> Fuente: parsing exhaustivo de los 4 checkouts (ver `tools/wms-deep-dive/`).

## Resumen ejecutivo

| Categoría | Conteo |
|---|---:|
| Archivos solo en `dev_2023_estable` (eliminados) | **105** |
| Archivos solo en `dev_2028_merge` (nuevos) | **761** |
| Archivos modificados (mismo path, distinto hash) | **541** |
| Archivos idénticos (sin cambios entre ramas) | **3085** |

## Archivos NUEVOS en `dev_2028_merge` (761)

Agrupados por carpeta de primer nivel:

| Carpeta | # archivos | Ejemplos |
|---|---:|---|
| `WMS.EntityCore` | 263 | `clsBeTrans_acuerdoscomerciales_det.cs`, `clsBeTrans_acuerdoscomerciales_enc.cs`, `clsBeTrans_ajuste_det.cs` |
| `WMS.DALCore` | 142 | `clsLnTrans_acuerdoscomerciales_det.cs`, `clsLnTrans_acuerdoscomerciales_enc.cs`, `clsLnTrans_ajuste_det.cs` |
| `TOMIMSV4` | 96 | `clsLnLog_sincronizacion_fallos.vb`, `clsLnLog_sincronizacion_nube.vb`, `clsLnTrans_ajuste_det_borrador.vb` |
| `WMSWebAPI` | 66 | `clsHelper.cs`, `AcuerdoComercialController.cs`, `AcuerdoComercialWebhookController.cs` |
| `GoCloud` | 51 | `BundleConfig.vb`, `FilterConfig.vb`, `IdentityConfig.vb` |
| `reservastockfrommi3` | 28 | `StockReservationFacade.cs`, `GlobalUsings.cs`, `ReservationContext.cs` |
| `WMS.StockReservation2` | 27 | `StockReservationFacade.cs`, `GlobalUsings.cs`, `ReservationContext.cs` |
| `WMS.StockReservation3` | 20 | `StockReservationFacade.cs`, `GlobalUsings.cs`, `ReservationContext.cs` |
| `SAPSYNCMAMPA` | 19 | `clsSyncSapAjustes.vb`, `clsSyncSapDevolProveedor.vb`, `clsSyncSapFacturaDeudor.vb` |
| `RFIDPrint` | 10 | `ApplicationEvents.vb`, `Form1.Designer.vb`, `Form1.vb` |
| `TOMWMSUX` | 9 | `AuthController.cs`, `LoginRequestDto.cs`, `LoginResponseDto.cs` |
| `GoCloudy` | 7 | `ChangePassword.aspx`, `ChangePasswordSuccess.aspx`, `Login.aspx` |
| `WindowsApp1` | 7 | `Form1.Designer.vb`, `Form1.vb`, `Application.Designer.vb` |
| `WMS.AppGlobalCore` | 6 | `DataRecordExtensions.cs`, `FormatoFechas.cs`, `WMS.AppGlobalCore.csproj` |
| `tools` | 4 | `01_stock_snapshot.sql`, `02_movements_window.sql`, `03_vw_stock_res.sql` |
| `AppGlobal` | 2 | `CryptoHelper.vb`, `EpcSscc96.vb` |
| `Archivos de copia de seguridad de Crystal Reports` | 1 | `WMS.vbproj` |
| `IAService` | 1 | `IAService.csproj` |
| `TestMI3Console` | 1 | `Program.vb` |
| `WSHHRN` | 1 | `rptOrden_Compra.vb` |

## Archivos ELIMINADOS en `dev_2028_merge` (105)

| Carpeta | # archivos | Ejemplos |
|---|---:|---|
| `WMSWebAPI` | 94 | `WeatherForecastController.cs`, `clsLnProducto.cs`, `clsLnProducto_bodega.cs` |
| `SAPSYNCMAMPA` | 9 | `clsSAP_Config.vb`, `clsSyncSAPDevolucionMeracnciaCliente.vb`, `clsSyncSAPDevolucionMercancia.vb` |
| `TOMIMSV4` | 2 | `clsBeRecepcionIntegridad.vb`, `DsListaUbicacion1.Designer.vb` |

## TOP 30 archivos con más cambios estructurales

Ordenados por |funcs agregadas| + |funcs eliminadas|.

| Archivo | ext | Δ líneas | + funcs | - funcs | + tablas | - tablas |
|---|---|---:|---:|---:|---:|---:|
| `SAPSYNCMAMPA/Clases Interface Sync/Ajustes/clsSyncSAPAjustes.vb` | .vb | -490 | 32 | 7 | 0 | 7 |
| `TOMIMSV4/TOMIMSV4/Mantenimientos/Producto/frmProducto_List.vb` | .vb | +374 | 31 | 0 | 0 | 0 |
| `TOMIMSV4/DAL/Mantenimientos/Producto/Producto_Talla_Color/clsLnProducto_talla_color.vb` | .vb | +816 | 15 | 1 | 4 | 0 |
| `TOMIMSV4/TOMIMSV4/Reportes/Ajustes/dsRepAjustes.Designer.vb` | .vb | +167 | 8 | 6 | 1 | 0 |
| `SAPSYNCMAMPA/Formas/frmEjecucion.vb` | .vb | -168 | 0 | 13 | 0 | 0 |
| `SAPSYNC_Killios/Clases Interface Sync/Solicitud_Traslado/clsSyncSAPSSolicitudTraslado.vb` | .vb | +22 | 12 | 0 | 0 | 0 |
| `TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb` | .vb | +1442 | 11 | 0 | 0 | 0 |
| `TOMIMSV4/DAL/Interface/Pedido_Compra/Pedido_Compra_Enc/clsLnI_nav_ped_compra_enc_Partial.vb` | .vb | -455 | 10 | 0 | 0 | 0 |
| `SAPSYNCMAMPA/Clases Interface Sync/Proveedor/clsSyncSAPProveedor.vb` | .vb | +176 | 0 | 9 | 0 | 0 |
| `TOMIMSV4/Entity/Mantenimientos/Ajustes/clsBeTrans_ajuste_det.vb` | .vb | +16 | 9 | 0 | 0 | 0 |
| `TOMIMSV4/TOMIMSV4/Transacciones/Despacho/frmDespacho.vb` | .vb | +212 | 0 | 9 | 0 | 0 |
| `TOMIMSV4/DAL/Interface/Barras_Pallet/clsLnI_nav_barras_pallet.vb` | .vb | +404 | 8 | 0 | 2 | 0 |
| `TOMIMSV4/Entity/Transacciones/Picking/Trans_Picking_Ubic/clsBeTrans_picking_ubic_Partial.vb` | .vb | +8 | 8 | 0 | 0 | 0 |
| `SAPSYNCMAMPA/Clases Interface Sync/Pedido_Cliente/clsSyncSAPSPedidoCliente.vb` | .vb | -989 | 0 | 7 | 0 | 4 |
| `SAPSYNC_Killios/Clases Interface Sync/Pedido_Cliente/clsSyncSAPSPedidoCliente.vb` | .vb | +10 | 6 | 0 | 0 | 0 |
| `TOMIMSV4/DAL/Interface/Pedido_Traslado/Pedido_Traslado_Enc/clsLnI_nav_ped_traslado_enc_Partial.vb` | .vb | +846 | 6 | 0 | 0 | 0 |
| `TOMIMSV4/DAL/Inventario/Inicial/Detalle/clsLnTrans_inv_detalle_Partial.vb` | .vb | -241 | 0 | 6 | 0 | 5 |
| `TOMIMSV4/Entity/Inventario/Ciclico/clsBeTempComparacionInventario.vb` | .vb | +6 | 6 | 0 | 0 | 0 |
| `TOMIMSV4/Entity/Mantenimientos/Ajustes/clsBeTrans_ajuste_enc.vb` | .vb | +9 | 5 | 1 | 0 | 0 |
| `TOMIMSV4/Entity/Transacciones/Ajuste/clsBeAjustesMI3.vb` | .vb | +10 | 6 | 0 | 0 | 0 |
| `TOMIMSV4/Entity/Transacciones/Ajuste/clsBe_vw_ajustes.vb` | .vb | +11 | 6 | 0 | 0 | 0 |
| `TOMIMSV4/Entity/Transacciones/Pedido/clsBeDetallePedidoAVerificar.vb` | .vb | +6 | 6 | 0 | 0 | 0 |
| `WMSWebAPI/Controllers/AuthController.cs` | .cs | +312 | 5 | 1 | 0 | 0 |
| `WMSWebAPI/Controllers/SyncIngresosController.cs` | .cs | +145 | 6 | 0 | 0 | 0 |
| `DMS/Clases/clsHelper.vb` | .vb | -12 | 0 | 5 | 0 | 0 |
| `SAPSYNCMAMPA/Clases Interface Sync/Factura_Reserva/clsSyncSapFacturaReserva.vb` | .vb | +547 | 0 | 5 | 0 | 1 |
| `SAPSYNCMAMPA/Clases Interface Sync/Producto/clsSyncSAPProducto.vb` | .vb | +369 | 0 | 5 | 0 | 2 |
| `SAPSYNCMAMPA/Clases/m_Global.vb` | .vb | -195 | 0 | 5 | 0 | 0 |
| `TOMIMSV4/DAL/Interface/Pedido_Compra/Pedido_Compra_Enc/clsLnI_nav_ped_compra_enc.vb` | .vb | +363 | 5 | 0 | 0 | 0 |
| `TOMIMSV4/DAL/Mantenimientos/Producto/Producto_Color/clsLnColor.vb` | .vb | +265 | 5 | 0 | 1 | 0 |

## Detalle TOP 10 (con funciones puntuales)

### `SAPSYNCMAMPA/Clases Interface Sync/Ajustes/clsSyncSAPAjustes.vb`
- Extensión: `.vb`
- Líneas: 798 → 308 (-490)
- **Funciones agregadas en 2028** (32):
  - `Class:BatchNumber`
  - `Class:InventoryDocumentLine`
  - `Class:InventoryPayload`
  - `Class:clsSyncSapAjustes`
  - `Property:BatchNumber`
  - `Property:BatchNumbers`
  - `Property:Comments`
  - `Property:CostingCode`
  - `Property:CostingCode2`
  - `Property:CostingCode3`
  - `Property:DocDate`
  - `Property:DocumentLines`
  - `Property:ItemCode`
  - `Property:JournalMemo`
  - `Property:Quantity`
  - _(+17 más)_
- **Funciones removidas en 2028** (7):
  - `Class:clsSyncSAPAjustes`
  - `Function:Enviar_Entrega_Mercancia_Traslado_SAP`
  - `Function:Importar_Ajustes_SAP`
  - `Function:Marcar_PI_Sincronizado_SAP`
  - `Function:Procesar_Ajustes_SAP`
  - `Sub:Dispose`
  - `Sub:Enviar_Transacciones_De_Salida`
- Tablas dejaron de usarse: `IBT1`, `IGN1`, `OBTN`, `OCRD`, `OIGN`, `OITM`, `OWHS`

### `TOMIMSV4/TOMIMSV4/Mantenimientos/Producto/frmProducto_List.vb`
- Extensión: `.vb`
- Líneas: 1,234 → 1,608 (+374)
- **Funciones agregadas en 2028** (31):
  - `Class:AWSResponse`
  - `Class:CatalogUploadRequest`
  - `Class:CatalogUploadResponse`
  - `Class:EmbeddingResult`
  - `Class:ProcessedObject`
  - `Property:bucket`
  - `Property:clasificacion`
  - `Property:embedding_vector`
  - `Property:embeddings_count`
  - `Property:environment`
  - `Property:familia`
  - `Property:filename`
  - `Property:hash`
  - `Property:image_base64`
  - `Property:key`
  - _(+16 más)_

### `TOMIMSV4/DAL/Mantenimientos/Producto/Producto_Talla_Color/clsLnProducto_talla_color.vb`
- Extensión: `.vb`
- Líneas: 540 → 1,356 (+816)
- **Funciones agregadas en 2028** (15):
  - `Function:Existe_Producto_By_Talla_and_Color`
  - `Function:GetSingle`
  - `Function:Get_All_By_IdProducto`
  - `Function:Get_All_By_IdProducto_FromStock`
  - `Function:Get_All_Dt_By_IdCampaña_And_IdOrdenCompraEnc`
  - `Function:Get_All_Dt_By_IdProductoTallaColor`
  - `Function:Get_IdProductoTallaColor_By_CodTalla_and_CodColor`
  - `Function:Get_IdProductoTallaColor_By_IdTalla_and_IdColor`
  - `Function:Get_ProductoTallaColor_By_Talla_and_Color`
  - `Function:Get_Single_By_IdColor_IdTalla`
  - `Function:Get_Single_By_IdParameters`
  - `Function:Get_Single_By_IdProducto`
  - `Function:Get_Single_By_IdProductoBodega`
  - `Function:Get_Single_By_Params`
  - `Function:Get_Single_Dt_By_IdProductoTallaColor`
- **Funciones removidas en 2028** (1):
  - `Sub:GetSingle`
- Tablas nuevas tocadas: `para`, `producto_bodega`, `stock`, `trans_oc_det`

### `TOMIMSV4/TOMIMSV4/Reportes/Ajustes/dsRepAjustes.Designer.vb`
- Extensión: `.vb`
- Líneas: 1,542 → 1,709 (+167)
- **Funciones agregadas en 2028** (8):
  - `Function:Istalla_destinoNull`
  - `Function:Istalla_origenNull`
  - `Property:color_destino`
  - `Property:color_origen`
  - `Property:talla_destino`
  - `Property:talla_origen`
  - `Sub:Settalla_destinoNull`
  - `Sub:Settalla_origenNull`
- **Funciones removidas en 2028** (6):
  - `Class:trans_ajuste_detRowChangeEvent`
  - `Property:BackupDataSetBeforeUpdate`
  - `Property:ClearBeforeFill`
  - `Property:Connection`
  - `Property:Transaction`
  - `Property:UpdateOrder`
- Tablas nuevas tocadas: `trans_ajuste_det`

### `SAPSYNCMAMPA/Formas/frmEjecucion.vb`
- Extensión: `.vb`
- Líneas: 1,763 → 1,595 (-168)
- **Funciones removidas en 2028** (13):
  - `Sub:Ejecuta_Interface_Bodegas`
  - `Sub:Ejecuta_Interface_Clientes`
  - `Sub:Ejecuta_Interface_Colores`
  - `Sub:Ejecuta_Interface_Productos`
  - `Sub:Ejecuta_Interface_Proveedores`
  - `Sub:Ejecuta_Interface_Tallas`
  - `Sub:Ejecuta_interface_Ajustes`
  - `Sub:Ejecuta_interface_Devolucion`
  - `Sub:Ejecuta_interface_Devolucion_Mercancia`
  - `Sub:Ejecuta_interface_Pedido_Cliente`
  - `Sub:Ejecuta_interface_Traslado_Mercancias_SAP`
  - `Sub:Ejecuta_interface_Traslados_Entrada_SAP`
  - `Sub:Ejecuta_interface_Traslados_SAP`

### `SAPSYNC_Killios/Clases Interface Sync/Solicitud_Traslado/clsSyncSAPSSolicitudTraslado.vb`
- Extensión: `.vb`
- Líneas: 1,751 → 1,773 (+22)
- **Funciones agregadas en 2028** (12):
  - `Class:ProductoTransferSAPProrrateo`
  - `Class:clsBeInfoBodegaByIdPedidoEnc`
  - `Property:CantidadBase`
  - `Property:CodigoPresentacion`
  - `Property:CodigoProductoSAP`
  - `Property:CodigoProductoWMS`
  - `Property:Codigo_Bodega_Destino`
  - `Property:Codigo_Bodega_Origen`
  - `Property:Factor`
  - `Property:IdPedidoEnc`
  - `Property:No_Linea`
  - `Property:No_Pedido`

### `TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb`
- Extensión: `.vb`
- Líneas: 1,277 → 2,719 (+1,442)
- **Funciones agregadas en 2028** (11):
  - `Function:Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack`
  - `Function:Aplica_Cambio_Estado_Ubic_HH_LicenciaCompleta_ConValidacionRack`
  - `Function:Aplica_Implosion`
  - `Function:ConstruirMensajePosicionPosteriorHH`
  - `Function:EsRackDobleProfundidadHH`
  - `Function:ExisteProductoBodegaDistintoEnUbicacionHH`
  - `Function:ObtenerCodigoProductoBodegaEnUbicacionHH`
  - `Function:ObtenerOrientacionParejaHH`
  - `Function:ObtenerUbicacionParejaDobleProfundidadHH`
  - `Function:Validar_Mismo_Producto_Posicion_JSON`
  - `Function:Validar_Regla_Ubicacion_JSON`

### `TOMIMSV4/DAL/Interface/Pedido_Compra/Pedido_Compra_Enc/clsLnI_nav_ped_compra_enc_Partial.vb`
- Extensión: `.vb`
- Líneas: 4,146 → 3,691 (-455)
- **Funciones agregadas en 2028** (10):
  - `Function:Asigna_Unidad_De_Medida`
  - `Function:AsignarTipoDocumentoIngreso`
  - `Function:CrearYGuardarDetalleOC`
  - `Function:InicializarEncabezadoNuevaOC`
  - `Function:InsertaProveedor`
  - `Function:ObtenerConfiguracionDeBodega`
  - `Function:ObtenerIdBodegaDestino`
  - `Function:ProcesarLotes`
  - `Sub:ConfigurarEncabezadoOrdenCompra`
  - `Sub:Generar_Tarea_Recepcion`

### `SAPSYNCMAMPA/Clases Interface Sync/Proveedor/clsSyncSAPProveedor.vb`
- Extensión: `.vb`
- Líneas: 496 → 672 (+176)
- **Funciones removidas en 2028** (9):
  - `Function:Get_Clientes_SAP_Hana`
  - `Function:Get_Proveedor_Devolucion_SAP`
  - `Function:Get_Proveedor_SAP_Hana`
  - `Function:Get_Proveedores_SAP_Hana`
  - `Function:Importar_Proveedores_Desde_SAP_A_TablaIntermedia`
  - `Function:Insertar_Proveedor_Single`
  - `Function:Insertar_Proveedores_Desde_TablaIntermedia_A_Tabla_TOMWMS`
  - `Function:Marcar_Proveedor_Sincronizado_SAP`
  - `Function:ObtenerEntidadesDesdeSAP`

### `TOMIMSV4/Entity/Mantenimientos/Ajustes/clsBeTrans_ajuste_det.vb`
- Extensión: `.vb`
- Líneas: 218 → 234 (+16)
- **Funciones agregadas en 2028** (9):
  - `Property:Codigo_Proveedor`
  - `Property:Color_destino`
  - `Property:Color_origen`
  - `Property:IdProductoTallaColor_destino`
  - `Property:IdProductoTallaColor_origen`
  - `Property:IdProveedor`
  - `Property:Nombre_Proveedor`
  - `Property:Talla_destino`
  - `Property:Talla_origen`

