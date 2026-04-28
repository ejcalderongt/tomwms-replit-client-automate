---
titulo: Respuestas Ciclo 7 - Comportamiento WMS
respondedora: Carol Karina Flores Klee (CKFK)
contribuidor: Erik Calderon (PrograX24) addendum P-18
recibido: 27 abril 2026
incorporado: 28 abril 2026
fuentes_originales:
  - TOMWMS_KILLIOS_PRD
  - IMS4MB_BYB_PRD
  - IMS4MB_CEALSA_QAS
documento_origen: attached_assets/preguntas-wms-ciclo-7_CKFK_1777340806630.docx
relacionado:
  - brain/protocol/BRAIN-PROTOCOL.md
  - ADR-010 (motor reserva legacy vs webapi)
---

# Respuestas Ciclo 7: Tuning del comportamiento WMS

Las 25 preguntas de la Ciclo 7 fueron generadas a partir del mapeo SQL de las tres bases productivas en el EC2 compartido. Carol Karina (CKFK), responsable funcional del WMS, contesto las 25. Erik agrego una addendum de contexto historico en P-18.

Este documento es la fuente de verdad para el folder `brain/wms-specific-process-flow/`. Las respuestas se transcriben **literales** (sin parafraseo) precedidas por `R_CKFK/` o `R_EJC/`. La interpretacion y los hallazgos accionables van al final, claramente separados del input crudo.

## Indice

1. [Inbound / Recepcion (P-01..P-04)](#1-inbound--recepcion)
2. [Putaway / Ubicacion (P-05..P-07)](#2-putaway--ubicacion)
3. [Pedido / Maquina de estados (P-08..P-11)](#3-pedido--maquina-de-estados)
4. [Picking (P-12..P-14)](#4-picking)
5. [Packing / Verificacion (P-15)](#5-packing--verificacion)
6. [Despacho (P-16..P-17)](#6-despacho)
7. [Transferencias internas (P-18)](#7-transferencias-internas)
8. [Ajuste de inventario (P-19)](#8-ajuste-de-inventario)
9. [Interfaz ERP / Errores (P-20..P-21)](#9-interfaz-erp--errores)
10. [3PL CEALSA (P-22..P-23)](#10-3pl-cealsa)
11. [Reabastecimiento (P-24)](#11-reabastecimiento)
12. [Tareas handheld (P-25)](#12-tareas-handheld)
13. [Hallazgos accionables](#hallazgos-accionables)
14. [Preguntas que quedaron abiertas](#preguntas-que-quedaron-abiertas)

---

## 1. Inbound / Recepcion

### P-01 Disparo de recepcion: ERP push o WMS pull

**Evidencia:** `i_nav_ped_traslado_enc` tiene 4,237 registros vs `trans_re_enc` solo 576. Hay 7x mas pedidos NAV recibidos que recepciones WMS creadas.

**Pregunta original:** Quien crea fisicamente la fila en `trans_re_enc`? Es el job que polea `i_nav_ped_traslado_*`, o el operador HH al iniciar la descarga? Si es lo primero, que filtro reduce 4237 a 576 (solo OC, no transferencias)?

**R_CKFK/** La fila la crea TOMWMS ya sea desde la HH o desde el BOF.

**R_CKFK/** No entiendo la pregunta. La descarga de datos de la interface se hace desde la computadora no desde la HH.

**R_CKFK/ (clarificacion no solicitada):** Es importante entender que hay una diferencia entre `trans_re_enc` y `i_nav_ped_traslado_enc`: la primera es para las recepciones, la otra para los pedidos de cliente o de venta. Una es entrada, la otra es salida. **No pueden hacer match.**

> **Lectura para el brain:** la hipotesis del mapeo (que ambas tablas se relacionan 1:N por ratio 7x) era falsa. `trans_re_enc` es exclusivamente recepciones (compras/transferencias entrantes); `i_nav_ped_traslado_enc` es pedidos salientes. El bridge debe modelarlas como dominios separados.

---

### P-02 Tabla `trans_re_det_lote_num` (180k filas)

**Evidencia:** 180,181 filas. La tabla mas grande del WMS. El nombre sugiere "lote y numeracion" del detalle de recepcion.

**Pregunta original:** Es 1 fila por escaneo de codigo de barras en HH (pallet, caja, unidad)? O 1 fila por lote unico? Hay politica de purga? Se replica a algun datawarehouse?

**R_CKFK/** Esta tabla se utiliza para llevar control de los lotes de forma numerica. Se inserta un registro **para cada recepcion de la HH**.

**R_CKFK/** No hay politica de purga.

**R_CKFK/** No se replica a un datawarehouse.

> **Lectura para el brain:** la cardinalidad es por recepcion HH (ni por escaneo individual ni por lote unico). Sin purga ni replica, esta tabla crecera indefinidamente. Marcar como riesgo de performance.

---

### P-03 Estados de orden de compra

**Evidencia:** `trans_oc_estado` tiene 6 estados definidos y 576 OC fisicas en Killios.

**Pregunta original:** Cuales son los 6 estados y la maquina de transiciones? En que estado se considera "OC cerrada para WMS"? Que pasa con OC parciales?

**R_CKFK/** Los 6 estados son: **Nuevo, Asignada, En proceso, Cerrada, Anulada, BackOrder**.

| Estado | Disparo |
|---|---|
| Nuevo | Se crea el documento |
| Asignada | Se crea la tarea de recepcion y se asigna a los operadores |
| En proceso | Se comienza a recepcionar |
| Cerrada | Se finaliza la recepcion del producto |
| BackOrder | Queda producto pendiente de recibir |
| Anulada | (no detallado por CKFK) |

**R_CKFK/** Un documento se cierra cuando el producto recibido fisicamente en la bodega se recibe en el documento del TOMWMS y si queda producto pendiente ya no se va a recibir.

**R_CKFK/** Las OC parciales quedan en estado **Backorder** para continuar la recepcion en otros documentos.

> **Lectura para el brain:** matriz de transiciones lineal con bifurcacion al final: `Nuevo -> Asignada -> En proceso -> {Cerrada | BackOrder | Anulada}`. Backorder es estado terminal para la OC original; el remanente genera otro documento.

---

### P-04 Conversion decimales SAP en recepcion

**Evidencia:** Killios tiene `convertir_decimales_a_umbas=1`. SAP B1 acepta cantidades decimales (ej. 3.5 cajas).

**Pregunta original:** Cuando llega una OC SAP con Quantity=3.5 y la presentacion tiene equivalencia=12 UDS/caja, WMS recibe internamente 42 UDS o 3.5 cajas? Si es UDS, al despachar como vuelve a representarlo en el documento de salida SAP?

**R_CKFK/** WMS recibe **3.5 en el documento de salida**. Al reservar, reserva las cajas y las unidades.

**R_CKFK/** A SAP se envian las cajas y las unidades.

> **Lectura para el brain:** el WMS NO convierte a UDS al recibir, mantiene el decimal de cajas tal cual. Pero al RESERVAR descompone en cajas enteras + unidades sueltas (3.5 cajas con equiv=12 = 3 cajas + 6 UDS). El bridge debe validar que el motor `reserva-webapi` reproduzca esa descomposicion exacta. **Critica para Killios.**

---

## 2. Putaway / Ubicacion

### P-05 License Plate (`genera_lp=true`)

**Evidencia:** Killios y BYB tienen `genera_lp=true`, CEALSA falso. `bodega_ubicacion` tiene 9,510 ubicaciones en Killios.

**Pregunta original:** Cuando exactamente WMS genera el LP? El LP es unico global o por bodega? Cuando se "consume" un LP?

**R_CKFK/** El LP se genera **al escanear o seleccionar el producto que voy a recibir**.

**R_CKFK/** El LP que se genera es **unico por bodega** (no global).

**R_CKFK/** Se consume cuando el pallet sale completo.

> **Lectura para el brain:** LP es identidad por bodega, no global. Bridge debe simular escaneo en HH para generar LP. El "consumo" es atomico al despachar pallet completo (no parcial).

---

### P-06 Estados de producto (`producto_estado` = 18)

**Evidencia:** 18 estados, mucho mas que los esperados (disponible/cuarentena/bloqueado).

**Pregunta original:** Cuales son los 18 estados? Cuales son elegibles para reserva? Hay maquina de estados? Quien cambia el estado?

**R_CKFK/** Los estados de los productos **no son fijos, no estan predefinidos**. Son tantos como el cliente necesite tener.

**R_CKFK/** Para una reserva por interface el estado se define en la tabla de configuracion por bodega. Si el cliente tiene definida ubicacion especifica, el producto se toma de esa ubicacion **sin importar el estado**. Si la reserva es automatica y el documento tiene definido un estado, se toma este como referencia; en caso contrario se toma el mismo de la interface. Si la reserva se hace por stock especifico se deja el seleccionado por el usuario.

**R_CKFK/** Son **etiquetas planas**, pero el sistema sabe si un estado es **utilizable** o no, y si es **bueno o malo**.

**R_CKFK/** El estado lo puede cambiar un operador en la HH o un usuario en el BOF.

> **Lectura para el brain:** **NO hay maquina de estados de producto**. Son etiquetas con dos flags: `utilizable` y `bueno/malo`. La logica de elegibilidad para reserva es polimorfica segun: (a) tipo de reserva (interface / automatica / por stock), (b) configuracion por bodega, (c) override por documento. Bridge debe soportar las 3 estrategias.

---

### P-07 Politica de putaway

**Evidencia:** 9,510 ubicaciones, 4,703 stocks activos. La asignacion no es trivial.

**Pregunta original:** La sugerencia de ubicacion al recibir es por SP o por reglas declarativas? Hay peso de criterios? El operador puede sobrescribir siempre?

**R_CKFK/** La sugerencia se hace a traves de un algoritmo que toma en cuenta: si esta o no habilitado el proceso de sugerencia, si hay reglas de ubicacion creadas, en caso contrario se toma en cuenta el producto y sus caracteristicas.

**R_CKFK/** Si se toman en cuenta esos criterios (zona, FIFO/FEFO, capacidad, vencimiento) y **tienen peso**.

**R_CKFK/** Se puede sobrescribir siempre, **pero existen parametros a nivel de bodega** que permiten habilitar o no el cambio de ubicacion.

> **Lectura para el brain:** algoritmo en cascada: (1) flag global ON/OFF, (2) reglas declarativas por bodega, (3) fallback a caracteristicas del producto. Override por operador esta gobernado por flags de bodega. Bridge necesita tres fixtures de bodega: con reglas / sin reglas / con override bloqueado.

---

## 3. Pedido / Maquina de estados

### P-08 Transiciones permitidas

**Evidencia:** Estados observados: NUEVO (14), Pendiente (73), Pickeado (86), Verificado (7), Despachado (3989), Anulado (33).

**Pregunta original:** Matriz de transiciones? Puede saltar de NUEVO directo a Anulado? Puede saltarse Pendiente? Verificado es opcional?

**R_CKFK/** La matriz lineal es: **Nuevo -> Pendiente -> Pickeado -> Verificado -> Despachado**.

**R_CKFK/** Si, un pedido puede pasar de NUEVO directo a Anulado.

**R_CKFK/** No puede saltar Pendiente. **En cuanto se le crea el Picking pasa a estado Pendiente.**

**R_CKFK/** Verificado **NO es opcional**. Aplica a todos los pedidos que se pickean. **Si un pedido no se verifica, no se puede despachar.**

> **Lectura para el brain (CRITICA para reserva-webapi):** la matriz es estricta y lineal. Anulado es alcanzable desde cualquier estado anterior a Despachado. El bridge debe rechazar transiciones invalidas (ej. NUEVO -> Pickeado, Pendiente -> Despachado). Verificado es **gate obligatorio** antes de Despachado, sin excepciones por tipo de pedido.

---

### P-09 Pedidos atascados

**Evidencia:** 86 Pickeado + 73 Pendiente = 159 pedidos no terminados de un total de 4202 (3.8%).

**Pregunta original:** Es normal este nivel de atasco? Hay job de reconciliacion? SLA NUEVO -> Despachado?

**R_CKFK/** Si es **normal** que esto pase. Hay pedidos manuales que no se migran al ERP. Tambien hay casos donde los pedidos se facturan ANTES de pickearse en WMS, lo que provoca que luego no los despachen porque el pedido sale de la empresa con la factura solamente.

**R_CKFK/** **No existe un job que los cierre**, requiere intervencion manual.

**R_CKFK/** El SLA varia: de **1 hora a 3 dias**, totalmente variable, depende de muchos factores.

> **Lectura para el brain:** 3.8% de atasco es baseline aceptado. Los atascos son sintoma de des-sincronizacion ERP/WMS, no de bug del motor. SLA tan amplio (1h-3d) hace inviable detectar atasco solo por tiempo.

---

### P-10 Caso `_LLR_CASO_#X_`

**Evidencia:** Casos secundarios `CASO_#X_LLR_CASO_#28_/29/31` aparecen en `trans_pe_det_log_reserva` como derivados de #20, #23, #24.

**Pregunta original:** Que significa "LLR" exactamente? Los casos #28/#29/#31 son ramas alternativas del motor que solo se ejecutan dentro del caso principal? Tienen documentacion en el VB?

**R_CKFK/** No se que significa exactamente, pero ocurre **cuando no logro reservar completo y requiero de un llamado recursivo**.

**R_CKFK/** Si es correcto, los casos #28/#29/#31 se ejecutan **cuando no es posible reservar en el llamado principal**.

**R_CKFK/** Tiene una documentacion **muy basica** en el VB; quien no conozca el motor no la entiende.

> **Lectura para el brain (CRITICA):** "LLR" probablemente sea "**Llamado Luego de Reserva**" o "Loop Logico de Reserva" — semantica recursiva confirmada. Los casos secundarios son fallback recursivos del motor cuando la reserva principal falla parcialmente. El bridge necesita un caso de test que fuerce reserva incompleta para activar el LLR. La documentacion VB es insuficiente; **necesitamos excavar el codigo legacy para mapear los 28+/29/31**.

---

### P-11 Polizas en CEALSA (`control_poliza=SI`)

**Evidencia:** `trans_pe_pol` (41 cols) modela polizas; CEALSA tiene `control_poliza=SI` en PE0001 y PE0005.

**Pregunta original:** Que porcentaje de pedidos CEALSA tiene poliza? La trae el cliente o WMS la genera? Que campos son obligatorios?

**R_CKFK/** **El 100% de los pedidos de la bodega fiscal deben de tener poliza asociada.**

**R_CKFK/** La trae el cliente externo.

**R_CKFK/** No estoy 100% segura, pero creo que estos son obligatorios: Numero Orden, Numero DUCA, Fecha, Clave Aduana Despacho Destino, NIT Importador, Regimen, Clase, Pais procedencia, Modo transporte, Tipo cambio, Codigo Poliza.

> **Lectura para el brain:** CEALSA es 100% bodega fiscal, regla dura. WMS no genera poliza (consume input externo). Los 11 campos enumerados son requisitos minimos para que el pedido avance, pero CKFK no esta 100% segura — **validar cruzando contra `trans_pe_pol` en CEALSA QAS** antes de cerrar la lista.

---

## 4. Picking

### P-12 `trans_picking_ubic` vs `trans_picking_ubic_stock`

**Evidencia:** Diferencia de 6k filas: `trans_picking_ubic` (26k) vs `trans_picking_ubic_stock` (20k). Hipotesis: una es sugerencia, la otra realidad.

**Pregunta original:** Se confirma la hipotesis? Los 6k de diferencia son sugerencias rechazadas? Hay log que explique cada divergencia? (critico para el bridge porque mide la "inteligencia" del motor de reserva)

**R_CKFK/** Es correcto, **`trans_picking_ubic_stock` tiene lo que realmente se pickeo**.

**R_CKFK/** Los 6k de diferencia son **productos que no se pickearon** (no son sugerencias rechazadas en el sentido de "tomar de otra ubicacion" — son ubicaciones sugeridas que quedaron sin tomar).

**R_CKFK/** Hay un log, **pero no tengo la certeza que guarde lo que se requiere**.

> **Lectura para el brain (CRITICA para reserva-webapi):** clarificacion importante respecto a la hipotesis original: la diferencia NO es "operador tomo de otra ubicacion", es **productos que el motor sugirio pickear pero el operador NO pickeo**. Causa probable: la reserva esperaba mas stock del que habia fisicamente, o el operador abandono el pedido. El bridge debe medir esta tasa de "no-pick" como metrica de calidad del motor. Logging adicional puede ser insuficiente — **tarea: auditar el log existente** para confirmar si captura la causa de cada no-pick.

---

### P-13 Bypass del picker

**Evidencia:** El motor reserva FEFO, pero el operador esta fisicamente en la bodega.

**Pregunta original:** El picker puede saltarse una reserva FEFO si la ubicacion esta bloqueada? Hay alarmas/tareas para investigar la divergencia? La tabla `marcaje` (3,701 filas) lo registra?

**R_CKFK/** Si lo puede hacer, con un **reemplazo de inventario**.

**R_CKFK/** **No existen alarmas**, pero si hay informacion que se guarda y permite saber que ocurrio. El producto con reemplazo se muestra en una **pestana del picking**.

**R_CKFK/** La tabla `marcaje` **NO** registra reemplazos. Es para control del ingreso de los operadores (asistencia).

> **Lectura para el brain:** "reemplazo de inventario" es la primitiva legitima de bypass FEFO. No hay alarmas — solo trazabilidad pasiva. La tabla `marcaje` es asistencia, no operaciones — **descartar de cualquier feature de picking**. Para auditar reemplazos hay que mirar la pestana del picking (UI) o la tabla que la alimenta — **identificar tabla pendiente**.

---

### P-14 `trans_picking_op` vs `trans_picking_enc`

**Evidencia:** Ratio ~4.5 operaciones por encabezado: 5895 op vs 1293 enc.

**Pregunta original:** Que define una "operacion de picking" (1 fila en op)? Es 1 por linea, 1 por movimiento, 1 por escaneo? Hay tipo de operacion (tomar, dejar, mover)?

**R_CKFK/** Es **una por pickeo realizado**. Por ejemplo: puedo tener un registro que indica que debo tomar 100 cajas de una licencia, pero solo tomo 50 — se va a registrar **una operacion por 50**.

> **Lectura para el brain:** la cardinalidad es 1 op por accion fisica del operador (no por intencion del sistema). El ratio 4.5 op/enc significa que en promedio cada pedido genera 4-5 acciones fisicas. Bridge: cada `op` debe poder ser parcial (ej. tomar 50 de 100). No se confirmo si hay tipos (tomar/dejar/mover) — pregunta abierta.

---

## 5. Packing / Verificacion

### P-15 `trans_packing_enc` solo 13 filas

**Evidencia:** Solo 13 sesiones de packing en toda la historia productiva, vs 4032 despachos.

**Pregunta original:** La verificacion en packing es opcional por configuracion? O esta tabla solo se llena para excepciones (premium, reclamos)? Hay verificacion en otra tabla?

**R_CKFK/** El packing es por pedido y depende de un **flag que se define por tipo de documento**. Tambien se le puede marcar este flag de forma manual a un pedido.

**R_CKFK/** La **verificacion de un pedido se hace sobre la misma tabla del picking**. El packing es el proceso de empacar el producto.

> **Lectura para el brain:** distincion clave: **verificacion != packing**. La verificacion (gate obligatorio P-08) vive en las tablas del picking. El packing es opcional, gobernado por flag por tipo de documento + override manual. Las 13 filas son los casos donde el cliente exigio empacado fisico explicito. **El bridge NO debe modelar packing como gate; solo verificacion.**

---

## 6. Despacho

### P-16 Discrepancia despacho vs estado

**Evidencia:** `trans_despacho_enc` = 4,032 vs `trans_pe_enc.estado="Despachado"` = 3,989. Diferencia de 43.

**Pregunta original:** Los 43 son: (a) Despachos sin pedido; (b) Pedidos despachados con estado no actualizado (bug); (c) Despachos cancelados que no rebajaron estado; (d) Otra cosa?

**R_CKFK/** Inciso (d). Esto generalmente pasa porque **se le cambia el estado al pedido a "Despachado" sin realmente despachar el producto. El WMS lo permite.**

> **Lectura para el brain (CRITICA, hallazgo de bug):** el WMS legacy permite forzar el estado terminal sin requerir el evento de despacho fisico. Es un bypass del flujo. La pregunta abierta es si esto es **feature** (operador admin para corregir errores) o **bug** (cualquier usuario puede saltarse despacho). El nuevo `reserva-webapi` debe decidir: prohibir el cambio manual a Despachado, o requerir un permiso especifico + razon + log. **Decision de producto pendiente.**

---

### P-17 Push automatico al ERP

**Evidencia:** `i_nav_transacciones_out` tiene 24,193 filas. Es el outbox hacia NAV/SAP.

**Pregunta original:** Es un patron outbox (WMS escribe, job pushea, marca exito/error)? Hay reintentos automaticos?

**R_CKFK/** WMS escribe la transaccion.

**R_CKFK/** **No hay reintentos automaticos.**

> **Lectura para el brain (CRITICA arquitectura):** es outbox **manual**, no es outbox transaccional con job de retry. La parte "WMS escribe + job pushea" se rompe — el push lo dispara manualmente algun operador/admin. Sin retry, los errores son terminales hasta intervencion humana. El nuevo `reserva-webapi` debe decidir si: (a) replica el comportamiento manual; (b) implementa outbox transaccional con retry exponencial; (c) hibrido (auto-retry para errores transientes, manual para fallos de negocio).

---

## 7. Transferencias internas

### P-18 `TRAS_WMS` sin reserva

**Evidencia:** `trans_pe_tipo` `TRAS_WMS` tiene `ReservaStock=NO` en Killios.

**Pregunta original:** Si `TRAS_WMS` no pasa por el motor de reserva, como garantiza que el stock que se movera esta disponible (no reservado por otro pedido)? Es un check ad-hoc en el SP o se asume que el operador HH valida visualmente?

**R_CKFK/** **Actualmente no se utiliza esa bandera para reservar o no producto de un documento de salida.**

**R_EJC/ (addendum, Erik Calderon):** `TRAS_WMS` es una transaccion de transferencia interna para WMS. Alguien decide que stock se mueve por diferentes criterios (puede ser stock especifico, reserva por wms), luego esto se "transfiere" a una bodega que tambien existe o esta configurada en WMS. En consecuencia, **la reserva se garantizo mediante proceso empirico/discrecional o por flujo de WMS**, sin embargo **tienes razon, me parece que no se esta validando especificamente esa bandera en el flujo**.

Para mejorar contexto: esa bandera estaba **intencionada para poder recibir documentos en WMS que fueran como un "bolson/bucket" de pedidos**, y eventualmente se ejecutara un proceso en batch de abastecimiento de los mismos en base a criterios de abastecimiento.

Por ejemplo, en el futuro se podria querer que de X producto se priorice 50% para los CD's (bodegas internas de WMS), 30% nuevos clientes y 20% calidad y merma. Entonces en base a esa politica se podria mejorar/optimizar la demanda de producto en la bodega. Habria que tener la configuracion en el cliente, o en las bodegas origenes y destinos para afinar bien la transicion y consumo entre ellas. Esto tambien puede servir para establecer, en base a peticiones durante un periodo de tiempo, una **necesidad de reserva o proyeccion de demanda**.

> **Lectura para el brain (HALLAZGO, bug latente):** el campo `ReservaStock` es codigo muerto en el flujo actual. El diseno original (bucket de demanda + abastecimiento batch policy-driven) nunca se implemento. Riesgo: si un dia alguien activa la bandera esperando que funcione, el motor no la respeta. Decision: (a) eliminar el campo del schema y los DTO del webapi; (b) implementar el flujo bucket que Erik describe; (c) dejarlo y documentarlo como reservado para futuro. **Recomendacion: opcion (c) con marca explicita en la documentacion.**

---

## 8. Ajuste de inventario

### P-19 Ajustes y SAP (`sap_control_draft_ajustes=false`)

**Evidencia:** Killios tiene `interface_sap=true` y `sap_control_draft_ajustes=false` (postea directo, no draft). `ajuste_tipo` tiene 6 tipos. `trans_inv_stock` 4,540 filas.

**Pregunta original:** Si SAP rechaza el ajuste (periodo cerrado, falta autorizacion), WMS hace rollback automatico del stock o queda fuera de sincronizacion hasta intervencion manual? Hay cola de "ajustes pendientes de retry"?

**R_CKFK/** Si requiere intervencion manual. **El WMS no hace rollback.**

**R_CKFK/** Los ajustes marcan como **enviados o no**, entonces es facil saber cuales han sido enviados y cuales no.

> **Lectura para el brain (HALLAZGO, riesgo conocido):** desincronizacion silenciosa entre WMS y SAP en caso de rechazo. No hay rollback ni cola de retry — solo flag binario `enviado`. El nuevo `reserva-webapi` debe decidir: (a) implementar rollback transaccional (complejo, requiere compensacion); (b) cola de retry con backoff; (c) UI de reconciliacion con permisos.

---

## 9. Interfaz ERP / Errores

### P-20 Clasificacion de errores

**Evidencia:** `i_nav_ejecucion_det_error` (4,021) y `log_error_wms` (66,339). Hay 16x mas errores generales que errores de interfaz.

**Pregunta original:** Hay clasificacion de errores: recuperables (red, timeout) vs fatales (datos invalidos, ERP rechaza)? Que dispara la replica del 16x? Quien los revisa y con que cadencia?

**R_CKFK/** Actualmente tenemos creados logs por cada transaccion, pero **esta informacion no se le muestra a los usuarios**, entonces no es muy util. Cuando solo teniamos `log_error_wms` esta si se visualizaba y era util, aunque demasiado grande.

**R_CKFK/** Si, eran errores capturados en diferentes lugares del codigo. **Actualmente lo tenemos mas estructurado por transaccion, pero no lo estamos mostrando.**

**R_CKFK/** Los revisan los usuarios que administran el sistema.

> **Lectura para el brain (HALLAZGO, mejora UX pendiente):** el nuevo schema estructurado por transaccion existe pero esta mudo. La tabla vieja sigue siendo la unica visible al admin. Accion concreta: **exponer los logs estructurados en una vista del BOF**. Sin clasificacion explicita recuperable/fatal, el bridge no puede automatizar retry. Tarea pendiente: definir taxonomia de errores antes de migrar a `reserva-webapi`.

---

### P-21 Cadencia del outbox

**Evidencia:** 24,193 filas en `i_nav_transacciones_out` vs 4,032 despachos = ~6x. No es 1:1 con despachos.

**Pregunta original:** Que eventos generan filas en `i_nav_transacciones_out`? Cada cuanto corre el job que las pushea?

**R_CKFK/** **Recepciones y despachos** (no ajustes ni traslados).

**R_CKFK/** **No existe un job que los pushea, es manual.**

> **Lectura para el brain:** confirma P-17. El outbox es manual y solo cubre 2 dominios (recepciones + despachos), pese a tener filas que sugieren mas. La diferencia 24k vs (576 OC + 4032 despachos = 4608) significa que el ratio real es ~5x — probablemente multi-fila por documento. Verificar: una recepcion/despacho genera N filas en outbox? **Pregunta abierta (P-26 sub-Q1).**

---

## 10. 3PL CEALSA

### P-22 Cierre de jornada (`VW_Stock_Jornada`)

**Evidencia:** Vista canonica para auditoria 3PL.

**Pregunta original:** El cierre de jornada es manual, automatico por hora, o nightly por SP? Que pasa con movimientos timestamp-eados antes del cierre pero registrados despues?

**R_CKFK/** **No hay como tal un cierre de jornada**, se establece por el **cambio de dia**. Generalmente se guardan los cambios por la hora que tengan, aunque hay algunos procesos que tienen excepciones.

> **Lectura para el brain:** "cierre de jornada" es un concepto contable, no un evento del sistema. La vista corta por fecha (probablemente `CONVERT(date, fecha)`). Los movimientos con timestamp del dia anterior pero insertados hoy quedan en la jornada anterior (correcto). Los "procesos con excepciones" son pregunta abierta — **identificar cuales son**.

---

### P-23 Prefactura: agregacion

**Evidencia:** `trans_prefactura_enc` (22 cols), `trans_prefactura_det` (19 cols), `trans_prefactura_mov` (13 cols), `trans_pe_servicios` (9 cols).

**Pregunta original:** La prefactura agrupa por cliente x periodo (mes), o por pedido? Que servicios facturables existen? Tarifas variables por cliente o unica?

**R_CKFK/** **Este proceso no lo conozco.**

> **Lectura para el brain:** Carol no es la informante para prefactura. Necesitamos identificar al dueno funcional (probablemente alguien de finanzas/billing en CEALSA o un consultor previo). **Tarea de ciclo futuro: encontrar respondedor para P-23.**

---

## 11. Reabastecimiento

### P-24 `trans_reabastecimiento_log` con datos en Killios

**Evidencia:** 1,218 registros aunque Killios no usa el modulo (BYB si). `considerar_paletizado_en_reabasto=false` y `excluir_ubicaciones_reabasto=false` en Killios.

**Pregunta original:** Que genera estos 1218 registros — proceso latente, SP corriendo, migracion historica? El modulo de reabasto NO esta operativo en Killios o hay uso parcial?

**R_CKFK/** **La tabla no se limpio al instalar ese cliente, debemos agregarla al SP `CLBD_PRC`.**

> **Lectura para el brain (HALLAZGO, bug operativo concreto):** los 1218 registros son basura historica. Accion atomica: agregar `trans_reabastecimiento_log` a la lista de tablas que limpia el SP `CLBD_PRC` durante la instalacion de un cliente nuevo. **Dueno: el equipo de instalacion.** El modulo NO esta operativo en Killios — la presencia de filas era falso positivo.

---

## 12. Tareas handheld

### P-25 `sis_tipo_tarea` (35 tipos)

**Evidencia:** 35 tipos de tarea posibles en HH. Mucho mas de lo esperado.

**Pregunta original:** Cuales son los 5-10 tipos mas usados (90% del uso)? Esto define el alcance minimo del bridge para validar el handheld.

**R_CKFK/** Los 7 tipos que cubren el grueso son:

1. Recepcion
2. Cambios de ubicacion
3. Cambios de estado
4. Implosiones
5. Picking
6. Verificacion
7. Despacho

> **Lectura para el brain:** el bridge HH solo necesita validar estos 7. Las otras 28 tareas son obsoletas, raras o de excepcion. **Critica para scoping del proyecto handheld:** el contract test del WebAPI debe cubrir como minimo estas 7 operaciones desde el endpoint HH.

---

## Hallazgos accionables

Estos no son input al brain — son **tareas concretas** detectadas a partir de las respuestas de Carol. Cada uno es candidato a generar un evento `learning_proposed` en el inbox del brain o un ticket de trabajo.

| ID | Hallazgo | Origen | Accion sugerida | Dueno |
|---|---|---|---|---|
| **H-01** | Bandera `TRAS_WMS.ReservaStock` es codigo muerto, no se valida en el flujo. Riesgo: si alguien la activa esperando comportamiento, el motor no la respeta. | P-18 (CKFK + addendum EJC) | Decidir entre: (a) eliminar campo del schema webapi, (b) implementar bucket+abastecimiento batch que EJC describe, (c) documentar como reservado-para-futuro. **Recomendacion: (c).** | Erik |
| **H-02** | `trans_reabastecimiento_log` no se limpia al instalar cliente nuevo. Genera 1218 filas basura en Killios. | P-24 (CKFK) | Agregar la tabla al SP `CLBD_PRC` que se ejecuta en instalacion. | Equipo instalacion |
| **H-03** | Logs estructurados por transaccion existen en el WMS pero no se muestran al admin. Usuario sigue dependiendo de la tabla vieja `log_error_wms` (66k filas). | P-20 (CKFK) | Exponer logs estructurados en una vista del BOF + definir taxonomia recuperable/fatal. | Equipo BOF |
| **H-04** | WMS permite forzar estado "Despachado" sin disparar despacho fisico. 43 pedidos en discrepancia en Killios. | P-16 (CKFK) | Decision de producto: el `reserva-webapi` prohibe el bypass, lo permite con permiso explicito, o lo replica tal cual. | Erik + producto |
| **H-05** | Ciclo 7 dejo P-23 (Prefactura CEALSA) sin respuesta. CKFK no conoce el proceso. | P-23 | Identificar respondedor alternativo (finanzas CEALSA o consultor previo) y reservar para Ciclo 8. | Erik |

---

## Preguntas que quedaron abiertas

Estas no son hallazgos accionables sino sub-preguntas que surgieron durante la lectura de las respuestas y que merecen rondar al proximo respondedor.

| Sub-Q | Origen | Pregunta |
|---|---|---|
| sub-Q1 | P-21 | Una recepcion/despacho genera cuantas filas en `i_nav_transacciones_out`? El ratio 5x sugiere N>1 por documento. |
| sub-Q2 | P-13 | Si la tabla `marcaje` no registra reemplazos de inventario, en que tabla quedan registrados? La pestana del picking lee de donde? |
| sub-Q3 | P-14 | Hay tipos de operacion (tomar/dejar/mover) en `trans_picking_op`? La respuesta solo confirmo que es 1 op = 1 accion fisica del operador, no si se distinguen tipos. |
| sub-Q4 | P-22 | Cuales son los "procesos con excepciones" en el corte de jornada por cambio de dia? |
| sub-Q5 | P-11 | La lista de 11 campos obligatorios de poliza fue dada con incertidumbre ("no estoy 100% segura"). Validar contra `trans_pe_pol` en CEALSA QAS. |
| sub-Q6 | P-10 | Que significa exactamente "LLR"? CKFK no lo sabe. Hay que excavar el VB legacy para mapear los casos #28/#29/#31 que son llamados recursivos del motor de reserva. |

---

## Como usar este documento

- **Para el bridge de tests:** las "Lecturas para el brain" en cada pregunta son los puntos a codificar como expected en los contract tests del `reserva-webapi`.
- **Para el WebAPI nuevo:** los hallazgos H-01, H-04 y la preguntas sub-Q3, sub-Q6 son decisiones de diseno previas a P-16b (scaffold).
- **Para la documentacion del flujo:** distribuir en archivos tematicos del folder `brain/wms-specific-process-flow/` cuando se confirme la estructura.
- **Para la planificacion de ciclos:** los hallazgos H-02, H-03, H-05 y las sub-Q1..6 son inputs para la Ciclo 8.

---

— Brain TomWMS · Ciclo 7 · Recibido 27 abril 2026 · Incorporado 28 abril 2026 —
