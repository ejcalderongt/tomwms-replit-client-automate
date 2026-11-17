# Correccion HH de verificacion consolidada

<!-- #EJC20261117_VERI_CONSOLIDADA -->

Alcance: HH `dev_2026_estable`, incidente pedido 8981, IdBodega 9.
Cliente no identificado de forma independiente. No se consulto ni modifico SQL.

En `frm_verificacion_datos.Guardar_Verificacion`, la rama consolidada sin
control talla/color ahora selecciona siempre por producto, presentacion y
cantidad pendiente, aunque el fallback haya completado lote y licencia.
Se elimina de esa rama el filtro por lote/fecha que comparaba la fecha
agrupada 01/01/1900 contra la fecha de picking 01/01/2000.
Se conserva el contrato del servicio, incluido pTipo=1, y las ramas de
verificacion normal, agrupacion sin licencia no consolidada y talla/color.

Validacion:

- `compile.log`: compilacion Java Android satisfactoria.
- `replay.log`: el metodo original reproduce ERR_20220406 sin llamar al servicio.
  El metodo modificado supera nueve escenarios: incidente, distintos lotes,
  fechas y licencias, aislamiento por producto y presentacion, filas completas,
  flujo normal, agrupacion sin licencia, talla/color y pTipo del servicio.
- La prueba extrae el metodo real de HEAD y del archivo modificado y utiliza
  dobles de interfaz, filtro en memoria y servicio; no opera pedidos reales.
- UTF-8 sin BOM y CRLF conservados; diff sin errores de espacios.

Limitaciones: no se instalo un APK ni se guardo el pedido real. Queda pendiente
la comprobacion operativa en el dispositivo con una compilacion instalada.
No se hizo commit ni push. Los cambios previos de cambio de ubicacion se conservaron.
