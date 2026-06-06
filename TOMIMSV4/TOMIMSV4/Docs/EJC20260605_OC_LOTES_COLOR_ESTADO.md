# EJC20260605 - Semáforo visual en grid de lotes OC

## Objetivo
Mejorar lectura operativa en pestaña **Lotes** de la OC con colores por estado de trazabilidad/recepción.

## Trazabilidad usada
En `frmOrdenCompra -> Cargar_Detalle_Lotes_OC()` el grid ya carga:
- `Licencia` (`datos.Lic_Plate`)
- `Cantidad_UMBAS` (`datos.Cantidad`)
- `Cantidad_Recibida_UMBAS` (`datos.Cantidad_recibida`)

Con eso se puede pintar estado sin roundtrip adicional.

## Regla aplicada
Archivo:
- `Transacciones/Orden_Compra/frmOrdenCompra.vb`
- Método nuevo: `gridviewLotes_RowStyle(...)`

Semáforo:
- **Amarillo**: `Licencia` vacía -> lote sin barra/licencia impresa.
- **Naranja**: `Licencia` con valor y `Cantidad_Recibida_UMBAS < Cantidad_UMBAS` -> pendiente con barra impresa.
- **Verde**: `Licencia` con valor y `Cantidad_Recibida_UMBAS >= Cantidad_UMBAS` -> completo.

## Confirmación funcional sobre cantidad recibida de lote
Sí, el sistema ya actualiza `cantidad_recibida` en lote durante recepción.  
Ruta de traza principal:
- `DAL/Transacciones/Recepcion/Recepcion_Detalle/clsLnTrans_re_det_Partial.vb`
- flujo `Actualiza_Detalle_Lotes_OC` y llamada a `clsLnTrans_oc_det_lote.Actualizar_Cantidad_Recibida(...)`.

Esto valida que el color **naranja/verde** está basado en dato vivo del proceso de recepción.

