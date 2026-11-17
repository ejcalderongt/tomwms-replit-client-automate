# Decimales en unidades — WMS272

Carolina, #CKFK20261027_UNIDADES_DECIMALES. Investigación y pruebas; no se modificó código BOF/HH adicional ni inventario productivo.

## Producto encontrado y utilizado

Consulta productiva del 27/10: el único grupo con cantidad fraccionaria en unidad base UN, usando tolerancia de 0.000001 para excluir ruido binario, fue WMS272 — ELOTITOS TIERNOS MIGUELS ENTERO 12/908GR 32OZ, IdProductoBodega 941, bodega 1, licencia FU07927, lote 70.06126. Tiene 78 stocks con fracciones, desde 0.02 hasta 14.33 UN. Son 78 filas, no 78 productos ni 78 cajas. La suma de esas filas es aproximadamente 166 UN; esa suma no cuantifica un faltante.

WMS215 ya no contiene las fracciones de la captura en la consulta actual. Carolina confirmó que lo ajustó. No se atribuye su desaparición al código local ni a estas consultas.

## Comparación histórica más concreta

En QAS del respaldo, FU07927 tiene tres stocks: 6 UN + 6 UN + 264 UN = 276 UN. El stock 172113 contiene 264 UN, presentación 43 Caja12, factor 12, estado REABASTO (8), ubicación 774.

En producción, la misma licencia suma aproximadamente 276 UN en 86 stocks. El stock 172113 conserva 0.2 UN. El 03/10/2026 hay 83 movimientos CEST de REABASTO (8) a BUEN ESTADO (1), que suman aproximadamente 263.8 UN: 264 - 263.8 = 0.2. Existe además un movimiento CEST de 6 UN de estado 1 a 1. Se distinguió de los 83 cambios para no duplicar la salida del origen 172113.

Ejemplos de los movimientos CEST:

| Movimiento | Cantidad registrada UN | Saldo reconstruido antes | Ese saldo / 12 |
|---|---:|---:|---:|
| 577849 | 22 | 264 | 22 |
| 577979 | 20 | 242 | 20.166667 |
| 577984 | 18 | 222 | 18.5 |
| 577985 | 17 | 204 | 17 |
| 577991 | 15 | 187 | 15.583333 |
| 577999 | 14.33 | 172 | 14.333333 |
| 578051 | 13.14 | 157.67 | 13.139167 |
| 578053 | 12.04 | 144.53 | 12.044167 |

Los primeros movimientos no todos coinciden con una división redondeada a dos decimales; no se afirma que la operación automática exacta haya sido la misma en todos. Desde 14.33 la secuencia es compatible con cantidades en cajas aplicadas como UN. La captura de WMS215 presenta un patrón análogo. Esto es evidencia del mecanismo candidato, no prueba del dispositivo, versión o pantalla que lo originó.

El hallazgo de esta licencia es fragmentación en cantidades fraccionarias, manteniendo el total. Es distinto del defecto de reserva que podía convertir 9 UN en 12 UN.

## Prueba usando WMS272

Se creó un stock de prueba de 264 UN, presentación Caja12 43, REABASTO, en QAS. Se ejecutó la rutina real local `clsLnTrans_movimientos.Aplicar` con ocho cantidades de los movimientos históricos: 22, 20, 18, 17, 15, 14.33, 13.14 y 12.04 UN.

El stock origen recorrió 264 -> 242 -> 222 -> 204 -> 187 -> 172 -> 157.67 -> 144.53 -> 132.49. Los nuevos stocks conservaron las cantidades decimales aplicadas; la suma de origen y destinos se mantuvo en 264 UN, salvo ruido de representación numérica de ~0.00000000000006. Esto NO explica los decimales de centésimas: esos son valores efectivos, muy superiores al ruido binario.

El ensayo utilizó DAL corregida en dev_2026_estable. Por tanto, la corrección de reserva no impide que el cambio de estado guarde fracciones recibidas en UN. Se repitieron las cantidades observadas, no la UI ni el endpoint completo; todavía hay que demostrar dónde se pierde la conversión de presentación a unidad base antes de llegar a esa rutina. No se modificó producción.

## Pedidos válidos de media y cuarto de caja

Se probaron solicitudes expresadas en presentación con WMS272 y stock de prueba de 24 UN en BUEN ESTADO, contra el motor MI3 completo original y corregido:

- 0.5 cajas debe equivaler a 6 UN; la prueba directa rechazó ambos ensamblados con ERROR_202212140132B, mostrando Sol: 6 y Disp: 2.
- 0.25 cajas debe equivaler a 3 UN; la prueba directa rechazó ambos con Sol: 3 y Disp: 2.
- 1.5 cajas se reservó correctamente como 18 UN (12 + 6), conservando el total de 24 en ambos.

No se presenta media/cuarto como regresión de la corrección: el resultado fue igual en el código original. La comparación parece mezclar unidades solicitadas y cajas disponibles en la ruta invocada, pero falta validar el contrato del llamador y la configuración del flujo real de pedidos antes de atribuirlo al uso productivo. Esta prueba NO certifica que medias y cuartos estén cubiertos de extremo a extremo.

Todos los casos se revirtieron. Los ejecutores compararon los stocks originales y no detectaron cambios en cantidad, estado, presentación y licencia. Un intento de comparación simultánea sufrió un deadlock de QAS y se revirtió; la comparación válida se repitió secuencialmente. Las rutinas pueden conservar logs de ensayo mediante conexiones independientes; no se borraron bitácoras. Se consumieron identidades de prueba.

## Qué corresponde atender

1. Rastrear el cambio de estado completo (pantalla -> movimiento/solicitud -> DAL) con WMS272 y un stock de 264 UN = 22 cajas, comprobando que cambiar las 22 cajas mueve 264 UN una sola vez.
2. Normalizar explícitamente la unidad de entrada y aplicar el factor solo una vez. Rechazar fracciones en UN para productos indivisibles después de convertir, conservando pedidos válidos de 0.5/0.25 cajas cuando el resultado sea entero.
3. No redondear ni truncar registros productivos automáticamente. Conciliar el total y la distribución por estado, presentación y ubicación antes de recomponerlos.
4. Resolver y validar el flujo de pedidos de media/cuarto de caja en la configuración real antes de publicar la corrección amplia de reserva.

La corrección de conservación de stock ya aplicada permanece sin cambios; este diagnóstico no la amplió ni la desplegó.

## Evidencias

31-vinagre-TOMWMS_KILLIOS_PRD.json y QA; 32-unidades-fraccionarias.json; 33-elotitos-qa.json; 34-elotitos-estado-total.json; stable-35-pedidos-fracciones-caja-wms272.json y baseline-35-pedidos-fracciones-caja-wms272.json; 36-replay-cest-wms272.json; 37-control-qa-elotitos.json. Ejecutores: probar-fracciones-caja-wms272.ps1 y probar-replay-cest-wms272.ps1.
