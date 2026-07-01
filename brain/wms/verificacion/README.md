# Verificacion WMS HH

Este brain concentra el flujo de Verificación en la HH Android.

## Qué sabemos ahora

- La pantalla principal de detalle vive en `frm_verificacion_datos.java`.
- Para productos con talla/color, la HH usa `CodigoSKU` como código operativo principal.
- Para productos sin talla/color, usa `Codigo` base.
- La confirmación de cantidad se hace antes de guardar.
- El proceso soporta reemplazo, fotos, verificación consolidada y verificación sin lote/fecha de vencimiento.

## Puntos de entrada

- `txtCantVeri`:
  - dispara la confirmación cuando el operador termina de capturar cantidad.
  - valida que no se exceda la cantidad solicitada.
- `txtPesoVeri`:
  - valida peso cuando el producto controla peso.
- `lblTituloForma`:
  - refresca el producto desde WS.

## Reglas de negocio

- Si `Control_Talla_Color` está activo, el código operativo mostrado y usado en la HH es `CodigoSKU`.
- Si no hay talla/color, la HH usa `Codigo`.
- La cantidad ingresada no puede ser mayor a la pendiente de verificar.
- Cuando hay verificación consolidada, la lista de líneas se filtra por código/SKU; si no, también se filtra por lote y fecha de vencimiento.

## WS relevantes

- `Get_Verificacion_By_Producto_And_Pedido`
- `Actualiza_Cant_Peso_Verificacion`
- `Guardar_Fotos_Verificacion`
- `Get_All_Imagen_Verificacion`

## Archivos clave

- `app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_verificacion_datos.java`
- `app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_list_prod_reemplazo_verif.java`
- `app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_danado_verificacion.java`
- `app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_verificacion_consolidada_detalle.java`

## Tags de trazabilidad

- `#EJC20260530`
- `#EJC20260603`
- `#EJC20260625`

## Nota operativa

La verificación no debe mezclarse con inventario cíclico ni con packing. Su propia lógica de scan y guardado debe vivir aislada en este brain.
