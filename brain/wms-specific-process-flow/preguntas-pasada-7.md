# Preguntas para afinar el mapeo del flujo WMS — pasada 7

> **Status global**: 12/25 respondidas + 1 parcial + 0 reabiertas. Ver
> respuestas consolidadas en `respuestas-tanda-1.md` (Erik) y
> `respuestas-tanda-2.md` (SQL autonomo).
>
> Las preguntas originales se mantienen literales (regla 6 del README:
> las pasadas no se editan, se suceden). Lo unico que se agrega es el
> badge de status arriba de cada pregunta y el link a la respuesta cuando
> ya existe.

---

## Inbound / Recepcion

### P-01 — Disparo de recepcion: ERP push o WMS pull?

`status: PENDIENTE`

**Evidencia**: `i_nav_ped_traslado_enc` tiene 4,237 registros vs `trans_re_enc` solo 576. Hay 7x mas pedidos NAV recibidos que recepciones WMS creadas.

**Pregunta**: ¿Quien crea fisicamente la fila en `trans_re_enc`? ¿El job que polea `i_nav_ped_traslado_*`, o el operador HH al iniciar la descarga? Y si es lo primero, ¿que filtro reduce 4237 a 576 (solo OC, no transferencias)?

### P-02 — Tabla `trans_re_det_lote_num` (180k filas)

`status: PENDIENTE`

**Evidencia**: 180,181 filas — la tabla mas grande del WMS. El nombre sugiere "lote y numeracion" del detalle de recepcion.

**Pregunta**: ¿Es 1 fila por escaneo de codigo de barras en HH (pallet, caja, unidad)? ¿O 1 fila por lote unico? ¿Hay politica de purga? ¿Se replica esta tabla a algun datawarehouse?

### P-03 — Estados de orden de compra (`trans_oc_estado` = 6)

`status: PENDIENTE`

**Evidencia**: 6 estados definidos en el catalogo, 576 OC fisicas.

**Pregunta**: ¿Cuales son los 6 estados y la maquina de transiciones? ¿En que estado se considera "OC cerrada para WMS"? ¿Que pasa con OC parciales (recibidas en multiples viajes)?

### P-04 — Conversion decimales SAP en recepcion

`status: PENDIENTE [PRIORIDAD ALTA — la unica critica abierta para Erik]`

**Evidencia**: Killios tiene `convertir_decimales_a_umbas=1`. SAP B1 acepta cantidades decimales (ej. `3.5 cajas`).

**Pregunta**: Cuando llega una OC SAP con `Quantity=3.5` y la presentacion tiene `equivalencia=12 UDS/caja`, ¿WMS recibe internamente `42 UDS` o `3.5 cajas`? Si es UDS, ¿al despachar como vuelve a representarlo en el documento de salida SAP?

---

## Putaway / Ubicacion

### P-05 — License Plate (`genera_lp=true`)

`status: PENDIENTE`

**Evidencia**: Killios y BYB tienen `genera_lp=true`, CEALSA falso. `bodega_ubicacion` tiene 9,510 ubicaciones en Killios.

**Pregunta**: ¿Cuando exactamente WMS genera el LP — al confirmar el primer escaneo del pallet en HH, al cerrar el detalle de recepcion, o al ubicar? ¿El LP es unico global o por bodega? ¿Cuando se "consume" un LP (al despachar el pallet completo)?

### P-06 — Estados de producto (`producto_estado` = 18)

`status: PENDIENTE`

**Evidencia**: 18 estados — mucho mas de lo esperado (solia ser disponible/cuarentena/bloqueado).

**Pregunta**: ¿Cuales son los 18 estados? ¿Cuales son **elegibles para reserva** y cuales no? ¿Hay maquina de estados (transiciones permitidas) o son etiquetas planas? ¿Quien cambia el estado de un producto (HH, SP automatico, ERP)?

### P-07 — Politica de putaway

`status: PENDIENTE`

**Evidencia**: 9,510 ubicaciones, 4,703 stocks activos. La asignacion no es trivial.

**Pregunta**: ¿La sugerencia de ubicacion al recibir es por SP o por reglas declarativas? ¿Hay peso de criterios (zona del producto, rotacion FIFO/FEFO, capacidad disponible, vencimiento mas viejo arriba)? ¿El operador puede sobrescribir la sugerencia siempre o solo con permiso?

---

## Pedido / Maquina de estados

### P-08 — Transiciones permitidas

`status: RESPONDIDA → respuestas-tanda-1.md`

**Evidencia**: Estados observados: NUEVO (14), Pendiente (73), Pickeado (86), Verificado (7), Despachado (3989), Anulado (33).

> **TL;DR**: matriz canonica completa. Anulado requiere accion explicita.
> WMS-inyectados pueden saltarse Pendiente. Verificado opcional.

### P-09 — Pedidos atascados

`status: RESPONDIDA → respuestas-tanda-2.md (SQL)`

**Evidencia**: 86 `Pickeado` + 73 `Pendiente` = 159 pedidos no terminados de un total de 4202 (3.8%).

> **TL;DR**: 100% de los 180 pedidos atascados tienen mas de 90 dias.
> No hay atascos recientes — son histórico no purgado. Deuda operativa
> de limpieza.

### P-10 — Caso `_LLR_CASO_#X_`

`status: RESPONDIDA → respuestas-tanda-1.md`

> **TL;DR**: LLR = "Llamado Luego de Reserva" (recursion del motor).
> Mapeo #20→#28, #23→#29, #24→#31. Para stock modificado durante reserva.

### P-11 — Polizas en CEALSA (`control_poliza=SI`)

`status: PENDIENTE`

**Evidencia**: `trans_pe_pol` (41 cols) modela polizas; CEALSA tiene `control_poliza=SI` en PE0001 y PE0005.

**Pregunta**: ¿Que porcentaje de los pedidos de CEALSA tiene poliza asociada? ¿La poliza la trae el cliente externo o WMS la genera? ¿Que campos de `trans_pe_pol` son obligatorios para que el pedido pueda avanzar?

---

## Picking

### P-12 — `trans_picking_ubic` (26k) vs `trans_picking_ubic_stock` (20k)

`status: RESPONDIDA → respuestas-tanda-2.md (SQL)`

> **TL;DR**: hipotesis confirmada. `trans_picking_ubic` = planificacion
> (sugerencias). `trans_picking_ubic_stock` = ejecucion (stock real).
> Diferencia 6k son sugerencias no ejecutadas (cancelados, dañados,
> no encontrados, sustituciones).

### P-13 — Bypass del picker

`status: PENDIENTE`

**Evidencia**: el motor reserva FEFO, pero el operador esta fisicamente en la bodega.

**Pregunta**: ¿El picker puede saltarse una reserva FEFO si la ubicacion sugerida esta bloqueada (desorden, pallet danado, lote mezclado)? ¿Hay alarmas/tareas para investigar la divergencia? ¿`marcaje` (3,701 filas) lo registra?

### P-14 — `trans_picking_op` (5895) vs `trans_picking_enc` (1293)

`status: RESPONDIDA → respuestas-tanda-2.md (SQL)`

> **TL;DR**: hipotesis incorrecta. `trans_picking_op` no es "operaciones",
> es relacion muchos-a-muchos operador↔picking. Ratio 4.56 operadores
> por sesion de picking. No es relevante para el bridge.

---

## Packing / Verificacion

### P-15 — `trans_packing_enc` solo 13 filas

`status: PENDIENTE [resuelto parcial via PEND-01]`

**Evidencia**: Solo 13 sesiones de packing en toda la historia productiva. Vs 4032 despachos.

**Pregunta original**: ¿La verificacion en packing es **opcional por configuracion**? ¿O esta tabla solo se llena para excepciones?

> **Resuelto parcial (tanda 2)**: la columna que controla verificacion es
> `trans_pe_tipo.Verificar` (bit). En Killios solo PE0003 la tiene en
> true (y no se usa: 0 historicos). Por eso solo 13 packing — son los
> casos manuales que se forzaron. **Falta confirmar con Erik**: ¿hay
> tambien una "verificacion liviana" sin pasar por trans_packing_enc?

---

## Despacho

### P-16 — Discrepancia despacho vs estado

`status: REABIERTA como P-16b → respuestas-tanda-2.md (SQL)`

**Evidencia original**: `trans_despacho_enc` = 4,032 vs `trans_pe_enc.estado='Despachado'` = 3,989.

> **TL;DR**: la pregunta estaba mal planteada. `trans_despacho_enc` agrupa
> MULTIPLES pedidos (es la salida del camion, no del pedido). La
> diferencia 4032 vs 3989 NO son huerfanos 1:1. Reabierta como **P-16b**:
> ¿hay pedidos en `trans_despacho_det.IdPedidoEnc` cuyo `trans_pe_enc.estado`
> NO sea Despachado? Es decir, ¿hay despachos completados que olvidaron
> actualizar el estado del pedido?

### P-17 — Push automatico al ERP

`status: RESPONDIDA → interfaces-erp-por-cliente.md (Erik tanda 3)`

> **TL;DR**: outbox polimorfico. Cada cliente tiene su interface dedicada
> (MI3 WCF / NavSync push / SAPSYNC* / WebAPI MHS). El "patron" del
> outbox es flexible y depende de la modalidad de integracion:
> - **Modalidad 1 (WCF)**: ERP cliente hace pull al WMS via MI3.vbproj
> - **Modalidad 2 (push programado)**: WMS dispara NavSync.vbproj con args
> - **Modalidad 3 (SAP dedicado)**: SAPSYNC*.vbproj por cliente
> - **Modalidad 4 (WebAPI moderno)**: WMS expone REST, cliente consume
>
> reserva-webapi es la siguiente generacion del modelo MHS (modalidad 4).
> 5 PEND nuevas (06-10) sobre detalles finos de cada modalidad.

---

## Traslado

### P-18 — Traslado interno sin reserva (`TRAS_WMS`)

`status: RESPONDIDA → respuestas-tanda-1.md (parcial + DEUDA-001)`

> **TL;DR**: La reserva ya se garantizo por proceso previo. **DEUDA-001**:
> la bandera `ReservaStock=NO` no se valida explicitamente. Vision
> futura: bolson/bucket con politicas tipo 50/30/20.

---

## Ajuste de inventario

### P-19 — Ajustes y SAP (`sap_control_draft_ajustes=false`)

`status: PENDIENTE`

**Evidencia**: Killios tiene `interface_sap=true` y `sap_control_draft_ajustes=false` (postea directo, no draft). `ajuste_tipo` tiene 6 tipos. `trans_inv_stock` 4,540 filas.

**Pregunta**: Si SAP rechaza el ajuste (ej. periodo cerrado, falta autorizacion), ¿WMS hace rollback automatico del stock o queda fuera de sincronizacion hasta intervencion manual? ¿Hay alguna cola de "ajustes pendientes de retry"?

---

## Interfaz ERP / Errores

### P-20 — Clasificacion de errores

`status: PENDIENTE`

**Evidencia**: `i_nav_ejecucion_det_error` (4,021) y `log_error_wms` (66,339). Hay 16x mas errores generales que errores de interfaz.

**Pregunta**: ¿Hay clasificacion de errores: recuperables (red, timeout) vs fatales (datos invalidos, ERP rechaza)? ¿Que dispara la replica del 16x — son errores capturados en cualquier lugar del codigo? ¿Quien los revisa y con que cadencia?

### P-21 — Cadencia del outbox `i_nav_transacciones_out`

`status: RESPONDIDA → interfaces-erp-por-cliente.md (Erik tanda 3)`

> **TL;DR**: la cadencia NO es responsabilidad del WMS — depende de la
> modalidad de integracion del cliente. Idealsa (WCF): el ERP cliente
> decide cuando polear. BYB (NavSync): scheduler externo dispara el .exe.
> Killios/Mampa/Becofarma/Cumbre (SAPSYNC*): cada uno con su propio
> scheduler. MHS (WebAPI): cliente decide cuando consultar.
>
> Ver `interfaces-erp-por-cliente.md` para el mapeo completo.

---

## 3PL CEALSA

### P-22 — Cierre de jornada (`VW_Stock_Jornada`)

`status: PENDIENTE`

**Evidencia**: vista canonica para auditoria 3PL.

**Pregunta**: ¿El "cierre de jornada" es un evento manual (operador lo dispara), automatico por hora, o por SP que corre nightly? ¿Que pasa con movimientos timestamp-eados ANTES del cierre pero registrados DESPUES (ej. operador olvido marcar)? ¿Se reabre la jornada o quedan en la siguiente?

### P-23 — Prefactura: agregacion

`status: PENDIENTE`

**Evidencia**: `trans_prefactura_enc` (22 cols), `trans_prefactura_det` (19 cols), `trans_prefactura_mov` (13 cols), `trans_pe_servicios` (9 cols).

**Pregunta**: ¿La prefactura agrupa por **cliente x periodo** (mes), o por **pedido**? ¿Que servicios facturables existen (almacenaje x pallet/dia, picking x linea, recepcion x bulto)? ¿Hay tarifas variables por cliente o tarifa unica?

---

## Reabastecimiento

### P-24 — `trans_reabastecimiento_log` con datos en Killios

`status: RESPONDIDA → respuestas-tanda-2.md (SQL)`

> **TL;DR**: el modulo esta encendido a medias en Killios. Detecta
> inexistencias automaticamente (mas reciente: 2025-06-16) pero no se
> ejecuta el procesamiento (`Procesado_HH=false`, `Enviado=false`).
> Las 1218 filas son cola que se acumula sin consumir.

---

## Tareas handheld

### P-25 — `sis_tipo_tarea` (35 tipos)

`status: PARCIAL → respuestas-tanda-2.md (SQL)`

**Evidencia**: 35 tipos de tarea posibles en HH.

> **TL;DR**: encontrada la tabla operativa real (`tarea_hh`, 18 cols).
> Catalogo completo de los 35 tipos enumerado en respuestas-tanda-2.
> Falta el TOP10 de uso real (query en proceso).

---

## Bonus — Priorizacion para Erik (actualizado)

Originalmente las criticas eran 6: P-04, P-08, P-10, P-12, P-16, P-17.

**Resueltas**: P-08 (tanda 1), P-10 (tanda 1), P-12 (SQL), P-16 (SQL → reabierta como P-16b).

**Aun necesitan a Erik**:

1. **P-04** (decimales SAP) — la unica critica original abierta para Erik.
2. **P-16b** ya CONFIRMADO en datos (ver `bug-report-p16b.md`). Falta
   identificar el SP responsable (necesito pistas del nombre).
3. **PEND-10 RESUELTA en tanda 4**: outbox usa `enviado=0/1` +
   `tipo_transaccion=INGRESO/SALIDA`. Ver `interfaces-erp-por-cliente.md`
   apendice "Marca de envio".
4. **PEND-12 NUEVA**: BYB tiene 110,795 INGRESOS pendientes en outbox
   (99.90%) vs solo 107 enviados. ¿Esta caido NavSync para INGRESOS?
   ¿O los INGRESOS van por otro canal?
5. **PEND-06, 07, 08, 09, 11** (detalles finos de cada modalidad).

Si tenes 5 minutos para P-04 dejamos cerrado el frente Killios.
