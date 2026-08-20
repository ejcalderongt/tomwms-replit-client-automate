# MAMPA: filtrar talla/color al abrir detalle de recepcion

Fecha: 2026-08-18  
Tag: #EJC20260818

## Incidente

Al seleccionar una linea para recibir, la HH llama `Get_Producto_Talla_Color_JSON`. En MAMPA la respuesta observada en Logcat es de 9,160,217 bytes y tarda entre 29.9 y 37.5 segundos por llamada. Los clics repetidos quedan encolados y elevan la espera a varios minutos.

## Cambio de contrato

- Se conserva `Get_Producto_Talla_Color_JSON` sin cambios.
- Se agrega `Get_Producto_Talla_Color_By_IdProducto_JSON(pIdProducto)` en `TOMHHWS.asmx`.
- La nueva operacion devuelve el mismo DTO `clsBeProducto_talla_color`, pero solo las combinaciones del producto solicitado.
- `frm_list_rec_prod` usa la operacion filtrada y evita solicitudes duplicadas mientras una consulta esta en curso.

## Compatibilidad

Cambio aditivo. Las HH instaladas continúan usando la operación anterior. La HH nueva requiere que el BOF nuevo esté desplegado antes o al mismo tiempo.

## Verificacion prevista

- Compilar BOF/WSHHRN.
- Compilar `:app:compileDebugJavaWithJavac`.
- Confirmar en Logcat que el payload deja de ser global y que un clic genera una sola solicitud.
