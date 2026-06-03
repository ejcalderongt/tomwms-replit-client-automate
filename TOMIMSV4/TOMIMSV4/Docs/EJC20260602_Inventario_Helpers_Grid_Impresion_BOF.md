# EJC20260602 - Inventario de aplicacion helper Grid/Impresion (solo BOF)

## Objetivo
Definir rollout seguro para:

1. Helper de menu contextual de copiado para grids de solo lectura.
2. Helper de cabecera estandar para `Imprimir_Vista`.

Sin cambiar logica funcional de negocio.

## Cambios base implementados
- `Clases_AP/clsUiGridCopyHelper.vb`
  - Agrega submenu `Copiar` en click derecho de `GridView`.
  - Permite copiar por columna visible y fila completa.
- `Clases_AP/clsUiPrintHelper.vb`
  - Dibuja cabecera base estandar para `PrintableComponentLink`.

## Pilotos aplicados
- `Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb`
  - Copiado contextual habilitado en `GridView1`.
  - Cabecera de impresion movida al helper estandar.
- `Transacciones/Inventario/frmRegularizarInventario.vb`
  - Copiado contextual habilitado en `GridView1`, `GridView2`, `GridViewRegularizado`.
  - Cabecera de impresion movida al helper estandar.

## Inventario de cobertura para fase siguiente
Busqueda de `Imprimir_Vista(` en BOF: **380 coincidencias**.

### Prioridad A (operacion/reportes de alto uso)
- `Transacciones/Picking/frmPicking.vb`
- `Transacciones/Picking/frmPicking_List.vb`
- `Transacciones/Recepcion/frmIngreso_List.vb`
- `Transacciones/Recepcion/frmRecepcion_List.vb`
- `Transacciones/Despacho/frmDespacho_List.vb`
- `Transacciones/Inventario/frmInventario.vb`
- `Transacciones/Inventario/frmInventarioList.vb`
- `Reportes/Resumen_Stock/frmResumenExistencias.vb`
- `Reportes/Resumen_Stock/frmExistenciasUbicacion.vb`
- `Reportes/Movimiento/frmMovimientosKardex.vb`
- `Reportes/Movimiento/frmMovimientosDoc.vb`
- `Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb` (ya aplicado)

### Prioridad B (catalogos/listados administrativos)
- `Mantenimientos/Bodega/frmBodega_List.vb`
- `Mantenimientos/Cliente/frmCliente_List.vb`
- `Mantenimientos/Producto/frmProducto_List.vb`
- `Mantenimientos/Consolidador/frmConsolidador_List.vb`
- `Mantenimientos/Arancel/frmArancel_List.vb`
- `Mantenimientos/Turno/frmTurno_List.vb`

## Estrategia de despliegue recomendada
1. Aplicar helpers por modulo (Picking, Recepcion, Inventario, Reportes).
2. Validar por modulo:
   - click derecho no rompe menus existentes.
   - copiar valor/fila funciona con columnas ocultas/agrupadas.
   - impresion mantiene datos y paginacion.
3. Publicar por lotes pequenos para acotar riesgo.

