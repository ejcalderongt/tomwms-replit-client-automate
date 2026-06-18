# MAMPA productos SAP -> WMS performance

Fecha: 2026-06-18
Servidor objetivo informado: `10.238.26.70`
Proceso padre: `clsSyncSAPProducto.Importar_Productos_Desde_SAP_A_TablaIntermediaAsync`
Proceso hijo principal: `Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS`

## Traza fina

Ruta UI: `frmEjecucion.mnuProductosI` -> `clsSyncSAPProducto.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS` -> `Confirmar_Y_Llenar_Intermedia` -> `Importar_Productos_Desde_SAP_A_TablaIntermediaAsync` -> `Get_Productos_SAP_SL` -> staging `i_nav_producto` -> `ProcesarProductosDesdeSAP`.

## Cuellos encontrados

- `Get_Productos_SAP_SL` paginaba de 100 en 100 y no usaba `$select`, aumentando payload y roundtrips al Service Layer.
- El import hacía `clsLnProducto.Existe_Codigo(prod.No)` por cada producto durante la lectura SAP; luego el proceso TOMWMS volvía a consultar existencia por producto.
- `Marcar_Producto_Sincronizado_SL` hacía login a SAP por cada producto procesado.
- Los catálogos `producto_clasificacion`, `producto_tipo`, `producto_marca`, `producto_familia` y `unidad_medida` se consultaban repetidamente por código/nombre dentro del loop.
- `InsertarProductoNuevo` hacía `clsLnProducto_bodega.MaxID()` por cada bodega del producto nuevo.
- `GestionarProductoBodegaAsync` validaba producto-bodega con consulta por fila, sin cache por lote.

## Cambios de código

- Se agregaron caches de lote para producto existente, catálogos y producto-bodega.
- `Get_Productos_SAP_SL` ahora usa `$select` y página de 500.
- La lectura SAP ya no filtra productos existentes con consulta SQL por cada `ItemCode`; la decisión se centraliza en el procesamiento TOMWMS.
- El marcado de `U_ENVIADO_WMS` usa `Marcar_Productos_Sincronizados_SL`, con una sola sesión SAP por lote.
- El progreso UI se reduce a checkpoints para bajar costo de `RichTextBox`/`Application.DoEvents`.
- `InsertarProductoNuevo` calcula una vez el siguiente `IdProductoBodega` y lo incrementa en memoria.

## SQL generado

Archivo: `brain/handoffs/2026-06-18-mampa-productos-performance/sql/20260618_mampa_productos_performance.sql`

Incluye índices idempotentes para lookups por código/nombre y el SP `dbo.usp_mampa_producto_interface_preflight_v1`.

## Pendiente de validación

- Compilar `SAPSYNCMAMPA` en Visual Studio/MSBuild del entorno WMS.
- Ejecutar el SQL en QAS contra `10.238.26.70` con `SET STATISTICS IO, TIME ON` alrededor de una corrida controlada.
- Comparar número de productos leídos, insertados/actualizados y marcados en SAP antes/después.
