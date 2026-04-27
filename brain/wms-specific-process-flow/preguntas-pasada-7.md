# Preguntas para afinar el mapeo del flujo WMS — pasada 7

> Ancladas en datos productivos de TOMWMS_KILLIOS_PRD, IMS4MB_BYB_PRD e IMS4MB_CEALSA_QAS (snapshot 2026-04-27).
> Cada pregunta cita la evidencia que la motiva. Las respuestas se incorporaran a `process-map.md`, `state-machine-pedido.md` o documentos por proceso (a crear).

---

## Inbound / Recepcion

### P-01 — Disparo de recepcion: ERP push o WMS pull?

**Evidencia**: `i_nav_ped_traslado_enc` tiene 4,237 registros vs `trans_re_enc` solo 576. Hay 7x mas pedidos NAV recibidos que recepciones WMS creadas.

**Pregunta**: ¿Quien crea fisicamente la fila en `trans_re_enc`? ¿El job que polea `i_nav_ped_traslado_*`, o el operador HH al iniciar la descarga? Y si es lo primero, ¿que filtro reduce 4237 a 576 (solo OC, no transferencias)?

### P-02 — Tabla `trans_re_det_lote_num` (180k filas)

**Evidencia**: 180,181 filas — la tabla mas grande del WMS. El nombre sugiere "lote y numeracion" del detalle de recepcion.

**Pregunta**: ¿Es 1 fila por escaneo de codigo de barras en HH (pallet, caja, unidad)? ¿O 1 fila por lote unico? ¿Hay politica de purga? ¿Se replica esta tabla a algun datawarehouse?

### P-03 — Estados de orden de compra (`trans_oc_estado` = 6)

**Evidencia**: 6 estados definidos en el catalogo, 576 OC fisicas.

**Pregunta**: ¿Cuales son los 6 estados y la maquina de transiciones? ¿En que estado se considera "OC cerrada para WMS"? ¿Que pasa con OC parciales (recibidas en multiples viajes)?

### P-04 — Conversion decimales SAP en recepcion

**Evidencia**: Killios tiene `convertir_decimales_a_umbas=1`. SAP B1 acepta cantidades decimales (ej. `3.5 cajas`).

**Pregunta**: Cuando llega una OC SAP con `Quantity=3.5` y la presentacion tiene `equivalencia=12 UDS/caja`, ¿WMS recibe internamente `42 UDS` o `3.5 cajas`? Si es UDS, ¿al despachar como vuelve a representarlo en el documento de salida SAP?

---

## Putaway / Ubicacion

### P-05 — License Plate (`genera_lp=true`)

**Evidencia**: Killios y BYB tienen `genera_lp=true`, CEALSA falso. `bodega_ubicacion` tiene 9,510 ubicaciones en Killios.

**Pregunta**: ¿Cuando exactamente WMS genera el LP — al confirmar el primer escaneo del pallet en HH, al cerrar el detalle de recepcion, o al ubicar? ¿El LP es unico global o por bodega? ¿Cuando se "consume" un LP (al despachar el pallet completo)?

### P-06 — Estados de producto (`producto_estado` = 18)

**Evidencia**: 18 estados — mucho mas de lo esperado (solia ser disponible/cuarentena/bloqueado).

**Pregunta**: ¿Cuales son los 18 estados? ¿Cuales son **elegibles para reserva** y cuales no? ¿Hay maquina de estados (transiciones permitidas) o son etiquetas planas? ¿Quien cambia el estado de un producto (HH, SP automatico, ERP)?

### P-07 — Politica de putaway

**Evidencia**: 9,510 ubicaciones, 4,703 stocks activos. La asignacion no es trivial.

**Pregunta**: ¿La sugerencia de ubicacion al recibir es por SP o por reglas declarativas? ¿Hay peso de criterios (zona del producto, rotacion FIFO/FEFO, capacidad disponible, vencimiento mas viejo arriba)? ¿El operador puede sobrescribir la sugerencia siempre o solo con permiso?

---

## Pedido / Maquina de estados

### P-08 — Transiciones permitidas

**Evidencia**: Estados observados: NUEVO (14), Pendiente (73), Pickeado (86), Verificado (7), Despachado (3989), Anulado (33).

**Pregunta**: ¿Cual es la matriz de transiciones permitidas? Por ejemplo:
- ¿Puede un pedido pasar de NUEVO directo a Anulado?
- ¿Puede saltarse Pendiente (NUEVO → Pickeado directo si hay stock)?
- ¿Verificado es opcional o solo aplica a tipos de pedido especificos?

### P-09 — Pedidos atascados

**Evidencia**: 86 `Pickeado` + 73 `Pendiente` = 159 pedidos no terminados de un total de 4202 (3.8%).

**Pregunta**: ¿Es normal este nivel de pedidos no terminados? ¿Hay un job de reconciliacion que los cierre o requiere intervencion manual? ¿Cual es el SLA para pasar de NUEVO a Despachado?

### P-10 — Caso `_LLR_CASO_#X_`

**Evidencia**: Casos secundarios `CASO_#X_LLR_CASO_#28_/29/31` aparecen en `trans_pe_det_log_reserva` como derivados de #20, #23, #24.

**Pregunta**: ¿Que significa "LLR" exactamente — "Llamado Luego de Reserva", "Logica Lateral de Reserva", "Loop Logico de Reserva"? ¿Los casos #28, #29, #31 son ramas alternativas del motor que solo se ejecutan dentro del caso principal? ¿Tienen documentacion en el VB?

### P-11 — Polizas en CEALSA (`control_poliza=SI`)

**Evidencia**: `trans_pe_pol` (41 cols) modela polizas; CEALSA tiene `control_poliza=SI` en PE0001 y PE0005.

**Pregunta**: ¿Que porcentaje de los pedidos de CEALSA tiene poliza asociada? ¿La poliza la trae el cliente externo o WMS la genera? ¿Que campos de `trans_pe_pol` son obligatorios para que el pedido pueda avanzar?

---

## Picking

### P-12 — `trans_picking_ubic` (26k) vs `trans_picking_ubic_stock` (20k)

**Evidencia**: Diferencia de 6k filas. Hipotesis: una es sugerencia (motor calcula que ubicacion deberia tomarse), la otra es realidad (de donde se tomo realmente).

**Pregunta**: ¿Se confirma la hipotesis? Si es asi, los 6k de diferencia ¿son sugerencias rechazadas (operador tomo de otra ubicacion)? ¿Hay log que explique cada divergencia? Esto es **critico** para el bridge porque mide la "inteligencia" del motor de reserva.

### P-13 — Bypass del picker

**Evidencia**: el motor reserva FEFO, pero el operador esta fisicamente en la bodega.

**Pregunta**: ¿El picker puede saltarse una reserva FEFO si la ubicacion sugerida esta bloqueada (desorden, pallet danado, lote mezclado)? ¿Hay alarmas/tareas para investigar la divergencia? ¿`marcaje` (3,701 filas) lo registra?

### P-14 — `trans_picking_op` (5895) vs `trans_picking_enc` (1293)

**Evidencia**: Ratio ~4.5 operaciones por encabezado.

**Pregunta**: ¿Que define una "operacion de picking" (1 fila en `op`)? ¿Es 1 por linea pickeada, 1 por movimiento entre ubicaciones, 1 por escaneo? ¿Hay tipo de operacion (tomar, dejar, mover)?

---

## Packing / Verificacion

### P-15 — `trans_packing_enc` solo 13 filas

**Evidencia**: Solo 13 sesiones de packing en toda la historia productiva. Vs 4032 despachos.

**Pregunta**: ¿La verificacion en packing es **opcional por configuracion** (algun flag de bodega o tipo de pedido)? ¿O esta tabla solo se llena para excepciones (ej. pedidos premium, reclamos)? ¿Hay un nivel de verificacion mas liviano que se hace en otra tabla?

---

## Despacho

### P-16 — Discrepancia despacho vs estado

**Evidencia**: `trans_despacho_enc` = 4,032 vs `trans_pe_enc.estado='Despachado'` = 3,989. Diferencia de 43.

**Pregunta**: ¿Los 43 son:
- (a) Despachos sin pedido (ajustes manuales de salida).
- (b) Pedidos despachados pero estado no actualizado (bug del legacy).
- (c) Despachos cancelados que no rebajaron el estado.
- (d) Otra cosa.

### P-17 — Push automatico al ERP

**Evidencia**: `i_nav_transacciones_out` tiene 24,193 filas. Es el outbox hacia NAV/SAP.

**Pregunta**: ¿Es un patron outbox (WMS escribe la transaccion, un job la pushea al ERP, marca exito/error)? ¿Hay reintentos automaticos en error? ¿Que tabla guarda la respuesta del ERP (ej. numero de documento generado)?

---

## Traslado

### P-18 — Traslado interno sin reserva (`TRAS_WMS`)

**Evidencia**: `trans_pe_tipo` `TRAS_WMS` tiene `ReservaStock=NO` en Killios.

**Pregunta**: Si TRAS_WMS no pasa por el motor de reserva, ¿como garantiza que el stock que se movera esta disponible (no reservado por otro pedido)? ¿Es un check ad-hoc en el SP o se asume que el operador HH valida visualmente?

---

## Ajuste de inventario

### P-19 — Ajustes y SAP (`sap_control_draft_ajustes=false`)

**Evidencia**: Killios tiene `interface_sap=true` y `sap_control_draft_ajustes=false` (postea directo, no draft). `ajuste_tipo` tiene 6 tipos. `trans_inv_stock` 4,540 filas.

**Pregunta**: Si SAP rechaza el ajuste (ej. periodo cerrado, falta autorizacion), ¿WMS hace rollback automatico del stock o queda fuera de sincronizacion hasta intervencion manual? ¿Hay alguna cola de "ajustes pendientes de retry"?

---

## Interfaz ERP / Errores

### P-20 — Clasificacion de errores

**Evidencia**: `i_nav_ejecucion_det_error` (4,021) y `log_error_wms` (66,339). Hay 16x mas errores generales que errores de interfaz.

**Pregunta**: ¿Hay clasificacion de errores: recuperables (red, timeout) vs fatales (datos invalidos, ERP rechaza)? ¿Que dispara la replica del 16x — son errores capturados en cualquier lugar del codigo? ¿Quien los revisa y con que cadencia?

### P-21 — Cadencia del outbox `i_nav_transacciones_out`

**Evidencia**: 24,193 filas vs 4,032 despachos. Es ~6x → no es 1:1 con despachos.

**Pregunta**: ¿Que eventos generan filas en `i_nav_transacciones_out`? ¿Recepciones, ajustes, traslados, despachos, todos? ¿Cada cuanto corre el job que las pushea?

---

## 3PL CEALSA

### P-22 — Cierre de jornada (`VW_Stock_Jornada`)

**Evidencia**: vista canonica para auditoria 3PL.

**Pregunta**: ¿El "cierre de jornada" es un evento manual (operador lo dispara), automatico por hora, o por SP que corre nightly? ¿Que pasa con movimientos timestamp-eados ANTES del cierre pero registrados DESPUES (ej. operador olvido marcar)? ¿Se reabre la jornada o quedan en la siguiente?

### P-23 — Prefactura: agregacion

**Evidencia**: `trans_prefactura_enc` (22 cols), `trans_prefactura_det` (19 cols), `trans_prefactura_mov` (13 cols), `trans_pe_servicios` (9 cols).

**Pregunta**: ¿La prefactura agrupa por **cliente x periodo** (mes), o por **pedido**? ¿Que servicios facturables existen (almacenaje x pallet/dia, picking x linea, recepcion x bulto)? ¿Hay tarifas variables por cliente o tarifa unica?

---

## Reabastecimiento

### P-24 — `trans_reabastecimiento_log` con datos en Killios

**Evidencia**: 1,218 registros aunque Killios no usa el modulo (BYB sí). `considerar_paletizado_en_reabasto=false` y `excluir_ubicaciones_reabasto=false` en Killios.

**Pregunta**: ¿Que genera estos 1218 registros — un proceso latente, un SP que se quedo corriendo, una migracion historica de cuando se evaluo activar el modulo? ¿Es seguro asumir que el modulo de reabasto NO esta operativo en Killios o hay un uso parcial?

---

## Tareas handheld

### P-25 — `sis_tipo_tarea` (35 tipos)

**Evidencia**: 35 tipos de tarea posibles en HH. Mucho mas de lo esperado.

**Pregunta**: ¿Cuales son los **5-10 tipos de tarea mas usados en produccion** (los que cubren el 90% de los marcajes)? Esto define el alcance minimo del bridge para validar el handheld. Los otros 25 pueden ser obsoletos, raros o solo para casos de excepcion.

---

## Bonus — Priorizacion para Erik

Si el tiempo no alcanza para responder las 25, las **criticas para reserva-webapi** (target del bridge) son:

1. **P-08** (transiciones permitidas) → indispensable para los `expected` de cada escenario.
2. **P-10** (`_LLR_CASO_#X_`) → cierra el catalogo de casos.
3. **P-12** (sugerencia vs realidad de picking) → mide la "inteligencia" del motor.
4. **P-04** (conversion decimales SAP) → reto principal de Killios.
5. **P-16** (discrepancia despacho vs estado) → puede ser bug a corregir en webapi.
6. **P-17** (outbox patron) → arquitectura de integracion.

Las demas pueden esperar a una pasada futura.
