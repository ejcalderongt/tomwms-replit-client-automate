# L-053 PROC Recepcion OC Talla/Color Llenado Antes de StockRec

Fecha: 2026-06-14

## Contexto

En el flujo de recepcion automatica por Orden de Compra, el detalle de OC entra a `Generar_Tarea_Recepcion_By_OrdenCompraEnc_Doc_Devolucion(...)` y de alli se arma `trans_re_det` y `stock_rec`.

## Hallazgo

La persistencia de `stock_rec` si guarda `IdProductoTallaColor`, `Talla` y `Color`, pero el flujo dependia de que `clsBeTrans_oc_det` ya viniera llenado.

Si el detalle llegaba con `IdProductoTallaColor > 0` pero sin `Talla` o `Color` completos, el lote y la recepcion quedaban vacios aunque el registro tuviera referencia de talla/color.

## Regla nueva

Solo llenar talla/color cuando `IdProductoTallaColor > 0`.

Si el detalle no trae `IdProductoTallaColor`, no forzar campos de talla/color en recepcion ni en `stock_rec`.

## Fix aplicado

1. Se centralizo el llenado de talla/color en OC detalle con una ayuda reutilizable.
2. Se llamo esa ayuda antes de mapear recepcion y stock.
3. En el armado de `trans_re_det` y `stock_rec` se dejaron vacios los campos cuando `IdProductoTallaColor = 0`.
4. Se agregaron trazas de debug en OC detalle, recepcion y `stock_rec` para confirmar el llenado y verificar el dato persistido en BD.

## Rutas tocadas

- `C:\Users\carol\source\repos\TOMWMS_BOF\TOMIMSV4\DAL\Transacciones\OrdenCompra\OC_Detalle\clsLnTrans_oc_det_Partial.vb`
- `C:\Users\carol\source\repos\TOMWMS_BOF\TOMIMSV4\DAL\Transacciones\Recepcion\Recepcion_Encabezado\clsLnTrans_re_enc_Partial.vb`
- `C:\Users\carol\source\repos\TOMWMS_BOF\TOMIMSV4\DAL\Transacciones\Stock_Recepcion\clsLnStock_rec.vb`
- `C:\Users\carol\source\repos\TOMWMS_BOF\TOMIMSV4\DAL\Transacciones\Stock_Recepcion\clsLnStock_rec_Partial.vb`

## Observacion

El build completo del mono-repo sigue mostrando errores historicos ajenos a este cambio en otros proyectos. El proyecto `DAL` compilo correctamente con este fix.
