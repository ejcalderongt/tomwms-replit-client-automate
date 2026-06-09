# Inventario inicial - performance de importacion y carga

Fecha: 2026-06-06
Dominio: BOF/DAL + SQL Server
Tags: `#EJC20260606_INV_IMPORT_PRODUCTO_TVP`, `#EJC20260606_INV_LISTAR_TRACE`

## Contexto

Se optimizo el proceso de carga de inventario inicial para MAMPA QA (`TOMWMS_MAMPA_QA`, servidor `10.238.26.70`) luego de observar que 9,833 registros tardaban demasiado en las etapas finales. El problema visible parecia UI/grid, pero las trazas separaron dos fuentes:

- UI/grid legacy: asignacion masiva a `DataGridView` disparaba autosize/repaint. Se migro la ruta principal a `dgridInventario` DevExpress + `DataTable` como backing store.
- DAL detalle: la etapa `DAL_DETALLE_LOOP` gastaba la mayor parte en lookup de producto por codigo.

## Evidencia clave

Log: `%TEMP%/TOMWMS/inventario-import-trace.log`

Run observado con 9,833 filas:

- `DAL_IMPORTAR_PRODUCTOS_FIN TotalMs=841025`
- `MsProductoDetalle=766649`
- `MsInsertDetalle=20415`
- `MsInsertResumen=14569`
- `MsInsertStock=17022`
- `MsUbicStock=14628`

Interpretacion: el oro estaba en `MsProductoDetalle`. Aunque ya existia diccionario por codigo, se llenaba lazy dentro del loop, produciendo N consultas cuando hay muchos codigos unicos.

## Cambio BOF/DAL

Archivos:

- `TOMIMSV4/DAL/Mantenimientos/Producto/Producto/clsLnProducto_Partial.vb`
- `TOMIMSV4/DAL/Inventario/Inicial/Stock_Prod/clsLnTrans_inv_stock_prod_Partial.vb`

Cambio:

- Nuevo metodo `clsLnProducto.Get_All_By_Codigos_For_InventarioImport`.
- Construye un `DataTable` con codigos unicos.
- Envia TVP `dbo.tvp_wms_codigo_producto_v1` al SP `dbo.usp_wms_producto_lite_por_codigos_tvp_v1`.
- Devuelve `Dictionary(Of String, clsBeProducto)` con el mismo shape lite que usaba `Get_Single_By_Codigo_For_InventarioImport`.
- Fallback seguro: si el SP/TVP no existe en una BD, usa el lookup legacy por codigo.

Trazas nuevas esperadas:

```text
DAL_PRODUCTO_CACHE_START
DAL_PRODUCTO_CACHE_END ProductoCache=N;MsProductoCacheBulk=X
DAL_DETALLE_LOOP_END MsProductoCacheBulk=X;MsProducto=Y
DAL_IMPORTAR_PRODUCTOS_FIN MsProductoCacheBulk=X;MsProductoDetalle=Y
```

## Cambio DBA

Repo: `C:/Users/yejc2/source/repos/DBA`
Branch: `codex/stock-por-lote-dataset-20260602`

Objetos:

- `tvp_wms_codigo_producto_v1`
- `usp_wms_producto_lite_por_codigos_tvp_v1`

Contrato del SP:

- Solo lectura.
- Recibe codigos por TVP.
- Resuelve producto por `producto.codigo` o `producto.codigo_barra`.
- Prioriza match por `codigo` sobre `codigo_barra` y luego menor `IdProducto`.
- Devuelve: `IdProducto`, `IdPropietario`, `PropietarioNombre`, `IdUnidadMedidaBasica`, `UnidadMedidaNombre`, parametros A/B, `codigo`, `nombre`, flags lote/vencimiento, costo/precio.

## Validacion realizada

- Build BOF con salida temporal OK:

```powershell
MSBuild TOMIMSV4/TOMIMSV4/WMS.vbproj /t:Build /p:Configuration=Debug /p:Platform=AnyCPU /p:OutDir=%TEMP%/TOMWMS/build-check/
```

- Objetos aplicados en MAMPA QA y verificados:

```text
TYPE_OK SP_OK
```

- Prueba del SP con 5 codigos reales devolvio filas con propietario, UM y datos producto lite.

## Siguiente medicion sugerida

Reejecutar importacion de 9,833 filas y comparar:

- Antes: `MsProductoDetalle=766649` (~12.8 min).
- Esperado: `MsProductoDetalle` baja drasticamente; el costo se concentra en `MsProductoCacheBulk`, idealmente segundos.

Si `MsProductoDetalle` no baja, revisar si:

- WMS_DEV esta corriendo binario viejo.
- El SP/TVP no existe en la BD apuntada por `Conn.ini`.
- Hay fallback por error SQL no visible; buscar `DAL_PRODUCTO_CACHE_END` y valores de cache.

## Regla reusable

Para importaciones masivas BOF:

1. No hacer lookups lazy dentro del loop cuando el universo de claves ya esta en memoria.
2. Precalcular claves unicas.
3. Enviar TVP a SP read-model/lite.
4. Consumir `Dictionary` en memoria.
5. Mantener fallback legacy si el SP no existe para no romper clientes no migrados.
