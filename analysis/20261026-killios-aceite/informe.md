# Diferencia de aceite WMS282 — Killios

Revisión: 26/10/2026. Base consultada: TOMWMS_KILLIOS_PRD, servidor proporcionado por la usuaria. Solo SELECT y lectura de metadatos; sin cambios en datos ni código del sistema. Las credenciales no se guardaron en estos archivos.

## Hallazgo confirmado

Producto: ACEITE OLIVA VIRGEN EXTRA LA ESPANOLA 12/250ML. IdProducto 315, IdProductoBodega 2199, bodega 1.

| Alcance | Existencia, UN | Kardex, UN | Existencia menos kardex |
|---|---:|---:|---:|
| Total adjuntos | 1,888 | 1,885 | +3 |
| Lote 05225 | 2 | 2 | 0 |
| Lote 21225 | 1,886 | 1,883 | +3 |
| Licencia JM001927 | 264 | 261 | +3 |

Los 14 registros de stock de la bodega 1 suman 1,888 unidades también en producción. Los 302 movimientos de los tipos que aparecen en el adjunto (INVE, RECE, DESP, AJCANTN) reproducen el saldo de 1,885 en producción. No sumar reservas/preparación/verificación como ingresos: son etapas internas.

La diferencia documental tiene esta dirección: la existencia muestra tres unidades MÁS que el saldo del kardex. Los archivos por sí solos no prueban un faltante físico.

## Despacho rastreado

Pedido 27794, referencia K21932, detalle 126392, picking 11263, despacho 23120, detalle 141574. Estado del pedido: Despachado; no anulado. Despacho: Finalizado.

| Fecha y hora registradas en SQL | Evento | Evidencia |
|---|---|---|
| 22/10/2026 11:37:03.350 | Reserva de 3 UN | LogReserva 179882, CASO_#21_EJC202310090957; stock 209033; reserva 230215 |
| 23/10/2026 09:20:06.930 | Picking de 3 UN | Movimiento 602426; trans_picking_ubic_stock 143885 |
| 23/10/2026 10:51:05.087 | Verificación de 3 UN | Movimiento 602741 |
| 23/10/2026 11:24:03.793 | Despacho de 3 UN | Movimiento 602909; fila 301 del Excel de kardex |
| 23/10/2026 11:24:04.057 | Registro histórico del stock despachado | stock_hist 138910, stock 209395, cantidad 3 UN |

La cadena documenta una salida de 3 UN de JM001927, lote 21225, estado NUEVA ERA, ubicación origen 756. No se encontró una reserva pendiente de este producto en stock_res. El stock original 209033 conserva 9 UN; el stock pickeado 209395 ya está en histórico.

La licencia conserva 246 UN en BUEN ESTADO y 18 UN en NUEVA ERA (stocks 208668 y 209033, 9 UN cada uno), total 264. La ubicación actual es R15 - C2 - N4 - A - #756.

## Hipótesis anterior: no demostrada

La conversión de una caja de 12 desde un origen menor fue una posibilidad identificada en código local, no un hecho demostrado para este producto. Se retira su clasificación anterior como causa probable fuerte. La rama CASO_#21 está documentada, pero se desconoce el saldo y el identificador del origen inmediatamente antes de convertir. Una coincidencia de 3 unidades no prueba causalidad.

## Revisión de cambios de estado del 22/10/2026 — #CKFK20261026

Consultas adicionales 08 a 11, exclusivamente de lectura.

| Movimiento | Fecha registrada | Cantidad cruda del movimiento | Estado origen | Estado destino | Presentación |
|---|---|---:|---|---|---|
| 600486 | 22/10/2026 08:28:19 | 9 | BUEN ESTADO (1) | NUEVA ERA (21) | 316, Caja12 |
| 600603 | 22/10/2026 08:58:17 | 9.25 | BUEN ESTADO (1) | NUEVA ERA (21) | 316, Caja12 |

Ambos movimientos identifican explícitamente producto 2199, licencia JM001927, lote 21225, ubicación 756. La presentación asociada no basta para afirmar que la cantidad esté en cajas: hay rutas con conversión previa a unidades. Tampoco cantidad_hist constituye por sí sola un saldo auditado de antes/después: en estos movimientos repite 9 y 9.25 y no guarda IdStock.

Existencia actual: BUEN ESTADO 246 UN (stocks 205748: 6, 205753: 228, 206703: 12) y NUEVA ERA 18 UN (stocks 208668: 9 y 209033: 9), total 264 UN. No hay otros cambios de estado de la licencia entre el 22/10 y la consulta. Después de esos cambios sí existe el despacho de 3 UN ya comprobado.

La reconstrucción contable del kardex da 264 UN antes de los cambios del 22/10 y 261 UN después del despacho del 23/10. Son saldos teóricos de movimientos, NO fotografías de stock en esas fechas. Un cambio de estado debería conservar ese total; las tablas disponibles no permiten medir el total real inmediatamente antes y después de cada uno de los dos cambios.

Se buscaron fotografías de stock_jornada para este producto y no se encontraron; tampoco registros de trans_ubic_hh_stock para esta licencia, auditoría de stock de los identificadores actuales/pickeado ni auditoría de stock/movimientos del 22/10. Stock no es temporal ni tiene CDC; no se encontraron triggers en stock/stock_hist/trans_movimientos. El historial de respaldos de msdb consultado no contiene copias de esta base para el 19–23/10; esto NO demuestra que no existan respaldos externos o en otro servidor.

La bitácora log_error_wms_ubic contiene solicitudes coincidentes por ubicación/estados/presentación/operador: 9 a las 08:21:20.580, 9.25 a las 08:51:18.333 y 9 a las 08:52:37.427. Carecen de licencia e IdStock, y sus horas difieren de las de los movimientos. No se asignan de forma concluyente a este producto ni se interpretan como tres cambios completados, reintentos o duplicaciones.

**Resultado:** se identificaron los dos cambios y el saldo actual por estado; no se pudo demostrar ni descartar que conservaran el inventario físico del sistema en el momento de ejecutarse. No se identificó una causa nueva comprobada. Para completar la comparación real se necesita una copia de la base anterior a los cambios y una posterior, idealmente antes de la reserva de las 11:37 del 22/10, o respaldos/logs que permitan recuperar esos cortes en un entorno separado. No se restauró nada ni se alteró producción.

## Comparación QAS del respaldo de septiembre — #CKFK20261026

Base consultada: TOMWMS_KILLIOS_QA, mismo servidor. Restauración registrada el 21/09/2026 00:12:16.113; último movimiento de toda la base fechado 19/09/2026 14:32:56.020, último registro de movimiento 14:32:56.160. No confundir fecha de restauración con fecha efectiva de los datos. No se hicieron escrituras en QAS.

Para WMS282, bodega 1, QAS contiene 11 stocks, total 484 UN. El saldo contable de los movimientos INVE/RECE menos DESP/AJCANTN da 484 UN: diferencia cero. JM001927 todavía no existe en ese stock.

Los 790 movimientos del producto de QAS tienen correspondencia por clave compuesta en producción, sin diferencias en cantidad, fecha, licencia, lote, tipo de tarea ni producto. No se encontraron movimientos adicionales de producción con fecha anterior o igual al último movimiento global de QAS que faltaran en QAS. La comparación no implica que todos los demás campos o tablas sean idénticos.

| Puente desde QAS a producción | UN |
|---|---:|
| Existencia inicial QAS | 484 |
| Entradas posteriores | 3,842 |
| Salidas posteriores | -2,441 |
| Existencia esperada | 1,885 |
| Existencia actual en producción | 1,888 |
| Diferencia | +3 |

Las entradas posteriores se componen de 1,920 UN de JM001927, 2 de JM001960 y 1,920 de JM002594. Las licencias presentes en QAS concilian individualmente: FU10685 18 - 16 = 2; FU09253 4 - 4 = 0; FU09714 6 - 6 = 0; FU09952 6 - 6 = 0; FU11887 450 - 426 = 24. Las otras nuevas licencias también concilian: JM001960 2 y JM002594 1,596.

Única licencia con diferencia en este puente: JM001927, ingreso de 1,920 UN del 24/09/2026, salidas documentadas de 1,659 UN, saldo esperado 261 UN y stock actual 264 UN. Este contraste confirma que no se arrastra una diferencia neta del corte QAS y concentra el desajuste posterior en JM001927. No establece cuándo apareció ni demuestra que el cambio de estado del 22/10 o la conversión de reserva lo causara.

Evidencia: consultas y resultados 12-qa-baseline y 13-puente-qa-prd. Las consultas de conciliación fueron SELECT; no se simularon operaciones en QAS.

## Conciliación completa de despachos JM001927 — #CKFK20261026

Se revisaron los 25 movimientos DESP de JM001927 (1,659 UN) individualmente contra sus detalles de despacho, picking final y stock_hist. Los cuatro controles suman 1,659 UN y coinciden para cada salida. Los 25 stocks históricos ya no existen en stock, incluso buscándolos por IdStock sin filtrar licencia. No hay claves duplicadas de movimiento DESP por IdDespachoEnc/IdDespachoDet, históricos huérfanos en esta conciliación, reservas actuales de la licencia ni movimientos de devolución/anulación registrados bajo ella.

En stock_hist, 22 registros están en Caja12 (factor 12), y 3 son unidades sueltas. La conversión se verificó contra los detalles y picking; no se sumó la cantidad cruda como si toda estuviera en UN. La traza de movimientos PIK también suma 1,659 UN.

Se detectó una excepción auxiliar en el pedido 27171 (G116189), despacho 22691 del 15/10: trans_picking_ubic_stock 141474 conserva 6 cajas (72 UN), pero el pedido/detalle actual, picking final, movimiento PIK, verificación, despacho e histórico corresponden a 5 cajas (60 UN). Su encabezado está Pendiente, aunque el detalle del aceite tiene 5 solicitadas y 5 despachadas. No hay registro de liberación de este producto/pedido en la tabla consultada. Esta diferencia auxiliar de 12 UN requiere explicación, pero NO constituye una diferencia de 12 entre kardex y baja histórica ni demuestra el origen de las 3 UN. Los otros 24 pedidos figuran Despachados; ninguno anulado. Los 25 despachos están Finalizados.

Conclusión acotada: las 25 salidas tienen histórico equivalente y sus registros retirados no siguen sumando en inventario. Esto no certifica la conservación del saldo al dividir el stock de origen durante picking/conversión ni durante los cambios de estado. La causa de las 3 UN continúa sin demostrar. No se hicieron reproducciones con escritura en QAS ni ajustes en producción.

Detalle auditable: conciliacion-despachos.md y 16-conciliacion-calculada.json; fuentes 14, 15 y 17. El siguiente paso técnicamente útil es una reproducción aislada de cambio de estado/conversión con controles antes/después, o recuperación de cortes históricos cercanos al 22/10. Una prueba reproducible del código no demuestra por sí misma el evento histórico.

## Defecto reproducido en prueba aislada QAS — #CKFK20261026

Se ejecutó la rutina local de aplicación de cambio de estado con cantidades 9 y 9.25 y con stocks sueltos: todos conservaron el total. Luego se ejecutó una subrama extraída literalmente de CASO_#21 con persistencia DAL real y transacción QAS: origen 9 UN con Caja12 pasó a 12 UN (+3); 9.25 pasó a 12 (+2.75); controles de 12 y 24 conservaron sus totales.

La combinación Aplicar real + subrama de conversión produjo 228 -> (219 + 9) -> (219 + 12) = 231 UN. Se demuestra un defecto de esa subrama bajo las condiciones del ensayo. NO se ejecutó el motor MI3 completo con selección/configuración previa ni se probó el binario desplegado en producción; la causa histórica de JM001927 continúa pendiente de confirmación. El caso 9.25 no genera exactamente tres, sino 2.75; no se oculta esa diferencia.

Todos los ensayos se revirtieron. QAS mantiene 11 stocks originales y 484 UN; no quedan licencias de prueba. No hubo escrituras productivas ni cambios de código WMS. Los contadores de identidad pueden avanzar por las inserciones revertidas.

Informe detallado y límites: pruebas-qa.md; evidencias 18 a 22. La siguiente validación debe ejercitar el motor completo y contrastar la versión desplegada, antes de corregir o atribuir causalidad histórica.

## Acción recomendada

1. Contar físicamente JM001927, lote 21225, en ubicación 756, separando BUEN ESTADO y NUEVA ERA, y comprobar la entrega del pedido K21932.
2. Obtener cortes históricos del 22/10 para comparar cantidades antes/después del cambio de estado y de la reserva. Una reproducción en QA puede evaluar defectos del código, pero no demuestra que se hayan ejecutado en este incidente.
3. Confirmar versión desplegada y contrato de unidades de los movimientos CEST antes de atribuir una causa o proponer una corrección.
4. Conciliar el saldo y, solo con conteo y autorización, preparar un ajuste trazable. No borrar el despacho ni restar otras tres unidades automáticamente: el despacho sí tiene respaldo completo.

## Evidencia conservada

- Adjuntos originales: C:/Users/carol/Downloads/WMS_ExistenciasConUbicacion Aceite.xlsx y C:/Users/carol/Downloads/WMS_MovimientosKardex Aceite.xlsx.
- Consultas, resultados y pruebas JSON 01 a 22 (16 contiene cálculos derivados; 18 a 22 documentan ensayos QAS) de esta carpeta. Son fotografías de la consulta, no un respaldo integral ni una bitácora histórica completa.
- Código local: C:/Users/carol/source/repos/TOMWMS_BOF/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb, ramas CASO_#21 alrededor de líneas 26043–26140 y 33838–33913.

## Corrección aplicada en dev_2026_estable — #CKFK20261026_RESERVA_EXPLOSION

Se completó la reproducción con el motor MI3 entero, comparando ensamblados original y corregido: 9->12 frente a 9->9. Se preservan las reservas existentes y el inventario QAS tras rollback. Cambio aplicado a 15 bloques homólogos; compilación del servicio BOF con cero errores y advertencias. Autoría CKFK. Sin despliegue ni ajuste productivo. Véase [correccion-estable.md](correccion-estable.md) para alcance, pruebas, candidatos de auditoría y límites.
