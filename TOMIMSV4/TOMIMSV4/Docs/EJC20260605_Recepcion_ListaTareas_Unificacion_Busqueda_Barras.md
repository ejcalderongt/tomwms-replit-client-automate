# EJC20260605 - Recepción HH (Lista de tareas): unificación de búsqueda SKU + licencia pallet

## Objetivo

Reducir roundtrip y variabilidad en el escaneo desde `frm_lista_tareas_recepcion` cuando el valor escaneado puede ser:

- SKU/código de producto
- licencia de pallet (barra de `i_nav_barras_pallet`)

## Flujo anterior

1. HH llamaba `Get_ListOrdenCompraEnc_By_Codigo_Producto`.
2. Si no encontraba OC, HH hacía fallback a `Get_All_Pallet_Ingreso_By_Barra`.
3. Luego resolvía `IdOrdenCompraEnc -> IdRecepcionEnc`.

Consecuencia:
- Doble roundtrip en escenarios MHS.
- Mayor latencia percibida y más ramas de error.

## Flujo actual

1. HH llama `Get_ListOrdenCompraEnc_By_Codigo_Producto`.
2. DAL resuelve en una sola consulta:
   - por `trans_oc_det.codigo_producto`
   - por `VW_ProductoSI.codigo_barra_pcb`
   - por `i_nav_barras_pallet.Codigo_Barra` (con `EXISTS`)
3. HH continúa con `IdRecepcionEnc` y abre recepción.

## Cambio aplicado

Archivo:
- `TOMIMSV4/DAL/Transacciones/OrdenCompra/OC_Detalle/clsLnTrans_oc_det_Partial.vb`

Método:
- `Get_ListOrdenCompraEnc_By_Codigo_Producto`

Tag:
- `#EJC20260605_FIX_REC_OC_UNIFICA_SKU_LP`

Detalle técnico:
- Se evitó `LEFT JOIN` directo a `i_nav_barras_pallet` para no multiplicar filas.
- Se usó `EXISTS` correlacionado por:
  - `p.Codigo_Barra = @Codigo_Producto`
  - `p.IdOrdenCompraEnc = d.IdOrdenCompraEnc`
  - `ISNULL(p.Recibido,0) = 0`

## Compatibilidad y riesgo

- No cambia firma del WebMethod.
- No cambia contrato de respuesta para HH (`DataTable ListaOC`).
- Se mantiene fallback HH temporal a `Get_All_Pallet_Ingreso_By_Barra` como red de seguridad.

Riesgo residual:
- Si la barra existe pero `IdOrdenCompraEnc` en `i_nav_barras_pallet` está inconsistente, puede seguir entrando al fallback.

## Trazabilidad operativa

Logcat HH usado para diagnóstico:
- `WMS.REC_MHS_SCAN`

Eventos relevantes:
- `WS_OC_LISTA`, `WS_OC_VACIA`, `WS_OC_NULL`
- `WS_PALLET_BARRA`, `WS_PALLET_BARRA_OC`, `WS_PALLET_BARRA_OC_SIN_TAREA`

