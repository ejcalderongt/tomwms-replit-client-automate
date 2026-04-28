---
titulo: Consolidacion Ciclo 7 - cross-reference de las 3 fuentes
fuentes_consolidadas:
  - tarea-1: respuestas-tarea-1.md (Erik Calderon, ciclo 9, 3 preguntas)
  - tarea-2: respuestas-tarea-2.md (agente brain via SQL READ-ONLY, ciclo 9b, 9 preguntas)
  - ciclo-7: respuestas-ciclo-7.md (Carol Karina Flores Klee, 25 preguntas + addendum EJC)
generado: 28 abril 2026
generado_por: agente brain en sesion replit
proposito: detectar contradicciones, complementariedades y vacios entre las 3 fuentes para que el lector encuentre la verdad consolidada en un solo lugar
documento_origen_consolidacion: brain/wms-specific-process-flow/respuestas-ciclo-7.md (commit eeefb02f)
---

# Consolidacion Ciclo 7

Las 25 preguntas de la Ciclo 7 fueron respondidas en **3 momentos distintos por 3 fuentes distintas**, lo que genera potencial de contradiccion o redundancia. Este documento es el **cruce maestro**: para cada pregunta, que dijo cada fuente, donde coinciden, donde se contradicen, y cual es la verdad operativa que el bridge/webapi debe asumir.

**No reemplaza** a los documentos fuente — los sintetiza para acelerar lectura. Para evidencia detallada (citas literales, queries SQL, addendums), seguir los links.

## Tabla maestra: estado consolidado por pregunta

| # | Pregunta | Erik (tarea-1) | SQL agente (tarea-2) | Carol (ciclo-7) | Estado consolidado |
|---|---|:-:|:-:|:-:|---|
| P-01 | Disparo de recepcion | — | — | RESP | ACLARACION (no son tablas matcheables) |
| P-02 | trans_re_det_lote_num cardinalidad | — | — | RESP | RESUELTO (1 fila por recepcion HH) |
| P-03 | Estados OC | — | — | RESP | RESUELTO (6 estados, transiciones documentadas) |
| P-04 | Decimales SAP | — | — | RESP | RESUELTO (decimal en doc, descompone al reservar) |
| P-05 | License Plate | — | — | RESP | RESUELTO (LP unico por bodega) |
| P-06 | Estados de producto | — | — | RESP | RESUELTO (etiquetas planas, no FSM, flag bueno/malo) |
| P-07 | Politica de putaway | — | — | RESP | RESUELTO (algoritmo en cascada) |
| **P-08** | **Transiciones pedido** | **RESP** | **RESP (PEND-01)** | **RESP** | **CONTRADICCION C-01: Verificado obligatorio vs opcional. Resuelta por SQL.** |
| P-09 | Pedidos atascados | — | RESP | RESP | CONFIRMADO (ambas: ruido historico) |
| **P-10** | **LLR significado** | **RESP** | — | RESP (parcial) | **COMPLEMENTO: Erik canonico (Llamado Luego de Reserva), Carol confirma mecanica** |
| P-11 | Polizas CEALSA | — | — | RESP | RESUELTO (100% pedidos fiscales, validar campos) |
| P-12 | Sugerencia vs realidad picking | — | RESP | RESP | CONFIRMADO + REFINADO (causas enumeradas) |
| P-13 | Bypass picker | — | — | RESP | RESUELTO (reemplazo de inventario, marcaje NO sirve) |
| P-14 | Operaciones picking | — | RESP | RESP | **CONTRADICCION C-02: distintos referentes (tabla vs concepto)** |
| P-15 | Packing 13 filas | — | — | RESP | RESUELTO (packing != verificacion) |
| **P-16** | **Despacho vs estado** | — | RESP (refinada) | RESP | **DOS HALLAZGOS COMPLEMENTARIOS: tablas distintas + bypass de estado permitido** |
| P-17 | Outbox | — | — | RESP | **CONTRADICCION C-03 con P-21 SQL (alcance de eventos)** |
| **P-18** | **TRAS_WMS sin reserva** | **RESP (DEUDA-001)** | (PEND-01 confirma flag) | **RESP + addendum EJC** | **CONFIRMADO: 3 fuentes coinciden. Erik es canonico, Carol confirma, SQL prueba** |
| P-19 | Ajustes SAP rollback | — | — | RESP | RESUELTO (sin rollback, flag binario enviado) |
| P-20 | Clasificacion errores | — | — | RESP | RESUELTO (logs estructurados ocultos) |
| P-21 | Cadencia outbox | — | RESP | RESP | **CONTRADICCION C-03 (alcance de eventos)** |
| P-22 | Cierre jornada CEALSA | — | — | RESP | RESUELTO (no hay cierre formal, corte por dia) |
| P-23 | Prefactura | — | — | NO SABE | **VACIO: sin informante** |
| P-24 | Reabasto Killios | — | RESP | RESP | **CONTRADICCION C-04: bug de instalacion vs modulo activo a medias** |
| P-25 | Tareas HH TOP | — | RESP (parcial) | RESP | COMPLEMENTO (Carol da TOP intuitivo, falta SQL para validar) |

**Conteo final:** 23/25 con respuesta consensuada o resuelta · 1 vacio (P-23) · 4 contradicciones (C-01..C-04) que se documentan abajo.

---

## Contradicciones detectadas

### C-01 — Verificado: obligatorio (Carol) vs opcional (Erik)

**Cita Erik (tarea-1):**
> Verificado es opcional. Puede ser habilitado por el usuario. Puede estar previamente definido en base al tipo de pedido.

**Cita Carol (ciclo-7):**
> Verificado NO es opcional. Aplica a todos los pedidos que se pickean. Si un pedido no se verifica, no se puede despachar.

**Resolucion via SQL (tarea-2 PEND-01):** la columna existe (`trans_pe_tipo.Verificar` bit). Configuracion observada:

| Cliente | Tipo | Verificar |
|---|---|:-:|
| Killios | PDV_NAV (96% del trafico) | **false** |
| Killios | PE0003 | true (no se usa: 0 pedidos historicos) |
| BYB | PE0001 (Pedido_De_Bodega) | **true** |
| CEALSA | PE0001 (Transferencia Fiscal a General) | **true** |

**Verdad consolidada:**

- **Ambos tienen razon parcial.** La obligatoriedad esta gobernada por **tipo de pedido en BD**, no por configuracion runtime ni por flag global.
- **Carol describe la realidad de su universo dominante:** en clientes donde el tipo principal tiene `Verificar=true` (BYB, CEALSA), todos los pedidos pickeados se verifican. Por eso ella lo experimenta como "obligatorio".
- **Erik describe la flexibilidad tecnica del sistema:** la BD permite ambas opciones por tipo, por eso es "opcional configurable".
- **Killios es un caso especial:** PDV_NAV (96% del trafico) tiene `Verificar=false` → la mayoria de pedidos en Killios saltan verificacion. Eso explica los **solo 7 pedidos** en estado "Verificado" en Killios.

**Implicancia para reserva-webapi:** el bridge debe leer `JOIN trans_pe_tipo ON IdTipoPedido` y consultar `Verificar`. NO asumir obligatoriedad global ni opcionalidad runtime.

---

### C-02 — `trans_picking_op`: 1 op = 1 pickeo (Carol) vs muchos-a-muchos operador-picking (SQL)

**Cita Carol (ciclo-7):**
> Es una por pickeo realizado, por ejemplo puedo tener un registro que indica que debo tomar 100 cajas, pero solo tomo 50 — se va a registrar **una operacion por 50**.

**Hallazgo SQL (tarea-2 P-14):**
> `trans_picking_op` solo tiene 7 columnas: `IdOperadorPicking`, `IdPickingEnc`, `IdOperadorBodega`, `user_agr`, `fec_agr`, `user_mod`, `fec_mod`. **No es una tabla de operaciones de picking**. Es una tabla muchos-a-muchos entre `trans_picking_enc` y `OperadorBodega`. Registra **que operadores trabajaron en cada picking_enc**.

**Verdad consolidada:**

- **Carol describe un concepto correcto pero atribuido a la tabla equivocada.** El concepto "1 fila por pickeo realizado con cantidad parcial" SI existe en el WMS — pero vive en `trans_picking_ubic_stock` (la tabla de ejecucion confirmada en P-12), no en `trans_picking_op`.
- **`trans_picking_op` es operacionalmente irrelevante para el bridge** — solo sirve para reportes de productividad por operador.

**Implicancia para reserva-webapi:** descartar `trans_picking_op` para validacion de logica del motor. Para validar pickeos parciales, usar `trans_picking_ubic_stock` con sus campos `cantidad_recibida`, `cantidad_verificada`, `cantidad_despachada`.

---

### C-03 — Outbox: solo recepciones+despachos (Carol) vs unificado 4 tipos (SQL)

**Cita Carol (ciclo-7 P-21):**
> Recepciones y despachos. (eventos que generan filas en `i_nav_transacciones_out`)

**Hallazgo SQL (tarea-2 P-21):**
> `i_nav_transacciones_out` es un outbox **unificado** que sirve para cualquier transaccion WMS→ERP. Tiene columnas para los 4 tipos: `idordencompra` (recepciones de OC), `idrecepcionenc` (recepciones generales), `idpedidoenc` (eventos de pedido), `iddespachoenc` (despachos cerrados). Cada fila viene poblada solo en el ID que corresponde.

**Verdad consolidada:**

- **El SCHEMA soporta 4 tipos.** La OPERACION ACTUAL (segun Carol) usa solo 2.
- **Sub-pregunta abierta:** cuantas filas en `i_nav_transacciones_out` tienen `idpedidoenc IS NOT NULL` o `idordencompra IS NOT NULL` poblados? Si son cero, Carol tiene razon operativa. Si son distintos de cero, hay uso silencioso.
- **L-013 ya documenta** la granularidad por linea del outbox (commit reciente). Hay que cruzar con esa.

**Implicancia para reserva-webapi:** el bridge debe escribir SOLO en `idrecepcionenc` e `iddespachoenc` para mantener paridad con legacy. Las otras dos columnas se dejan en NULL hasta confirmacion. Documentar el alcance reducido en el contrato del bridge.

**Accion concreta recomendada:** ejecutar query SQL contra Killios PRD para confirmar cuales de las 4 columnas estan pobladas en las 24,193 filas existentes. Si hay distinta de NULL para `idpedidoenc` o `idordencompra`, abrir Q-NNN para refinar.

---

### C-04 — Reabasto Killios: bug de instalacion (Carol) vs modulo activo a medias (SQL)

**Cita Carol (ciclo-7 P-24):**
> La tabla no se limpio al instalar ese cliente, debemos agregarla al SP `CLBD_PRC`.

**Hallazgo SQL (tarea-2 P-24):**
> 1,218 filas en `trans_reabastecimiento_log`. Mas vieja: 2021-11-22. Mas reciente: **2025-06-16** → el modulo SIGUE generando filas activamente. Esta encendido a medias: deteccion automatica ON, procesamiento OFF.

**Verdad consolidada:**

- **Las dos versiones NO son mutuamente excluyentes.** Pueden coexistir:
  1. **Hay basura inicial NO LIMPIADA** (Carol) — la instalacion no purgo `trans_reabastecimiento_log`.
  2. **El modulo de DETECCION sigue activo** (tarea-2) — el SP que detecta inexistencias en ubicaciones de picking sigue corriendo y agregando filas nuevas.
- **El gap real:** el modulo de PROCESAMIENTO (que consume las filas para mover stock) esta apagado. Las filas se acumulan como cola sin consumidor.

**Sub-pregunta abierta:** de las 1218 filas, cuantas son pre-2024 (basura instalacion) vs post-2024 (deteccion activa)? El analisis SQL podria responder esto sin nueva intervencion humana.

**Implicancia para reserva-webapi:** el modulo de reabasto NO esta en el alcance del nuevo motor (no se procesa hoy, no se procesa manana). Pero la **deteccion** esta activa — si el bridge va a leer estado de stock en ubicaciones de picking, debe ignorar `trans_reabastecimiento_log` (es ruido, no input).

**Accion atomica derivada:** independientemente de la causa raiz, **Carol propone una accion concreta y dueña** (agregar la tabla al SP `CLBD_PRC`). Esa accion sirve para limpiar la basura inicial y dejar baseline limpio en clientes nuevos. Esto ya quedo capturado como evento `learning_proposed` H-02 en `_inbox/`.

---

## Refinamientos aceptados (consenso entre fuentes)

Estos son cruces donde las fuentes **se complementan** sin contradecirse — el resultado es una verdad mas rica que la de cada fuente sola.

### R-01 — P-08: matriz de transiciones de pedido

**Erik (tarea-1)** documento la matriz canonica con casos especiales (`NUEVO → Pickeado` solo para WMS-inyectados). **Carol (ciclo-7)** confirmo el flujo lineal canonico para humanos. **Tarea-2 PEND-03 (SQL)** mostro que solo 0.4% de los pedidos atraviesan la maquina en una transaccion (los WMS-inyectados de Erik), pero **siempre con `IdPickingEnc` ≠ 0** — es decir, no se SALTA el modelo, se SIMULA completo en milisegundos.

**Conclusion compuesta:** la matriz lineal de Carol es correcta como **especificacion**. El caso WMS-inyectado de Erik es correcto como **excepcion implementada via picking sintetico instantaneo** (no como bypass real del flujo). El bridge debe modelar los 6 estados aunque algunos pedidos los atraviesen en milisegundos.

### R-02 — P-10: LLR (Llamado Luego de Reserva)

**Erik (tarea-1)** dio el nombre canonico ("Llamado Luego de Reserva") y la justificacion arquitectonica ("la forma mas facil para no generar mas codigo, una llamada recursiva"). **Carol (ciclo-7)** confirmo la mecanica observada ("ocurre cuando no logro reservar completo y requiero llamado recursivo") sin saber el nombre.

**Conclusion compuesta:** ambos describen el mismo fenomeno. El mapeo `#20→#28, #23→#29, #24→#31` viene de Erik. El bridge debe portar el patron, pero el nuevo motor puede reemplazar la recursion por una cola de tareas pendientes — el bridge valida resultado, no implementacion. **Renombrar a `PostReservationProcessing` en webapi.**

### R-03 — P-12: planificacion vs ejecucion de picking

**Carol (ciclo-7)** confirmo la hipotesis basica (`trans_picking_ubic_stock` es lo realmente pickeado). **Tarea-2 P-12 (SQL)** enumero las 4 causas concretas de la diferencia 6,130: (1) pedidos cancelados, (2) sugerencias rechazadas con reemplazo, (3) stock no encontrado, (4) pallet dañado. Tambien aporto la query exacta para medir "inteligencia" del motor.

**Conclusion compuesta:** el bridge tiene una metrica clara (% sugerencias aceptadas sin cambio) y conoce las 4 causas de no-pick para clasificarlas en su reporte de regresion.

### R-04 — P-09: pedidos atascados (ruido historico, no flujo activo)

**Carol (ciclo-7)** dio el contexto operativo (manuales no migrados + facturados antes de pickearse). **Tarea-2 P-09 (SQL)** cuantifico: 100% de los 180 atascados tienen >90 dias, no hay atascos recientes.

**Conclusion compuesta:** 3.8% atascado es **falso positivo** — son ruido historico, no problema activo. El bridge debe excluir pedidos `WHERE fec_agr < DATEADD(day, -90, GETDATE())` del comparativo.

### R-05 — P-18: TRAS_WMS, 3 fuentes coinciden

**Erik (tarea-1)** documento la DEUDA-001 (bandera `ReservaStock` no se valida). **Tarea-2 PEND-01 (SQL)** confirmo que `trans_pe_tipo.ReservaStock=false` para TRAS_WMS en BD. **Carol (ciclo-7)** repitio (con menos detalle) la misma observacion ("no se utiliza esa bandera"). El **addendum de Erik en ciclo-7** repitio sustancialmente lo de tarea-1.

**Conclusion compuesta:** triple confirmacion. La vision futura del bolson/bucket es exclusiva de Erik (Carol no la conoce — probablemente nunca se discutio formalmente). Esto ya quedo capturado como evento `learning_proposed` H-01 en `_inbox/`.

---

## Hallazgos derivados de la consolidacion

Cosas nuevas que solo se ven al cruzar las 3 fuentes:

### D-01 — Verificado es habito de cliente, no flag global

Detectado al resolver C-01. La obligatoriedad de Verificado se decide por configuracion del catalogo `trans_pe_tipo` por cliente. El bridge necesita un fixture diferente por cliente:
- Killios: la mayoria de pedidos saltan Verificado.
- BYB: la mayoria de pedidos pasan por Verificado.
- CEALSA: 100% de pedidos fiscales pasan por Verificado + poliza.

Esto afecta los expected del bridge — no se puede usar el mismo escenario "Verificado obligatorio" para los 3 clientes.

### D-02 — Outbox: usar SQL para confirmar alcance real antes del scaffold

Detectado al resolver C-03. Antes de implementar el writer del outbox en `reserva-webapi`, ejecutar la query:

```sql
SELECT
  COUNT(CASE WHEN idordencompra   IS NOT NULL THEN 1 END) AS con_oc,
  COUNT(CASE WHEN idrecepcionenc  IS NOT NULL THEN 1 END) AS con_recepcion,
  COUNT(CASE WHEN idpedidoenc     IS NOT NULL THEN 1 END) AS con_pedido,
  COUNT(CASE WHEN iddespachoenc   IS NOT NULL THEN 1 END) AS con_despacho,
  COUNT(*) AS total
FROM i_nav_transacciones_out;
```

Si `con_pedido` o `con_oc` son distintos de cero, Carol tiene una vision parcial y el bridge debe soportar todos los casos. Si son cero, su afirmacion se confirma y el bridge se simplifica.

**Costo:** 1 query SQL READ-ONLY. Beneficio: cierra C-03 sin nuevo ciclo con humanos.

### D-03 — P-16 ahora tiene DOS hallazgos en vez de uno

Antes de la consolidacion, P-16 era "discrepancia 4032 vs 3989 = 43". Despues:

1. **La discrepancia 4032 vs 3989 NO es huerfanos 1:1** (tarea-2): son tablas con cardinalidad distinta (despacho_enc agrupa multiples pedidos en un camion).
2. **PERO ADEMAS, el WMS permite forzar estado=Despachado sin disparar despacho** (Carol): bypass del flujo confirmado.

Estos son hallazgos diferentes que merecen tratamiento separado:
- El hallazgo (1) cierra la pregunta original (matematica). No requiere accion.
- El hallazgo (2) abre una decision de producto (replicar bypass, prohibirlo, o permitirlo con permiso). Esto ya quedo capturado como evento `learning_proposed` H-04 en `_inbox/`.

### D-04 — Killios es caso de borde para todo

Detectado al cruzar P-04, P-08, P-25, C-01, C-04:
- Killios es el unico cliente con `convertir_decimales_a_umbas=true`.
- Killios tiene `Verificar=false` en su tipo dominante (PDV_NAV 96%).
- Killios tiene reabasto encendido a medias (deteccion ON, procesamiento OFF).
- Killios tiene 43 pedidos con bypass de estado.

**Implicancia:** el bridge debe usar Killios como "cliente complejo" y BYB/CEALSA como casos mas estandar. Si la suite de tests pasa contra Killios, probablemente pasa contra los otros dos (excepto poliza CEALSA, que es exclusiva).

---

## Preguntas todavia abiertas o reabiertas

| Origen | Pregunta | Tipo | Quien puede responder |
|---|---|---|---|
| P-23 | Prefactura CEALSA: agregacion, servicios facturables, tarifas | original sin respuesta | NO Carol. Buscar respondedor en finanzas/billing CEALSA o consultor previo. |
| C-03 sub-Q | Cuales columnas del outbox unificado estan realmente pobladas en produccion? | sub-Q derivada | SQL agente (READ-ONLY) — accion concreta D-02 |
| C-04 sub-Q | De las 1218 filas en `trans_reabastecimiento_log` Killios, cuantas son pre-2024 vs post-2024? | sub-Q derivada | SQL agente (READ-ONLY) |
| P-16 sub-Q | Hay pedidos en `trans_despacho_det` cuyo `trans_pe_enc.estado` NO sea Despachado? (la P-16b refinada que tarea-2 propuso, Carol no la respondio explicitamente) | sub-Q derivada | SQL agente o Erik |
| P-21b | Cuando se ejecuta el push manual del outbox? Cadencia humana? | reabierta | Carol o operadores reales |
| sub-Q4 (de respuestas-ciclo-7) | Cuales son los "procesos con excepciones" en el corte de jornada CEALSA? | sub-Q | Carol (re-pregunta) o SQL |
| sub-Q5 (de respuestas-ciclo-7) | Validar los 11 campos obligatorios de poliza contra `trans_pe_pol` en CEALSA QAS | sub-Q | SQL agente (READ-ONLY) |
| P-25 TOP10 real | TOP10 real de tareas HH cruzando `tarea_hh.IdTipoTarea` con `sis_tipo_tarea` | parcial | SQL agente |

**Resumen:** 8 sub-preguntas abiertas, de las cuales **5 son respondibles por el agente SQL READ-ONLY** sin nueva intervencion humana. Esas 5 son candidatas a una **Ciclo 8a (autonoma SQL)** antes de la Ciclo 8 con humanos.

---

## Como se relaciona este documento con los demas

```
preguntas-ciclo-7.md         (input: 25 preguntas con evidencia SQL)
        |
        +---> respuestas-tarea-1.md       (Erik responde 3: P-08, P-10, P-18)
        |
        +---> respuestas-tarea-2.md       (SQL agente responde/refina 9)
        |
        +---> respuestas-ciclo-7.md      (Carol responde 25 + addendum EJC)
                       |
                       v
              consolidacion-ciclo-7.md   (este doc: cross-reference de las 3)
                       |
                       +---> _inbox/H-01..H-05.json   (5 hallazgos accionables)
                       |
                       +---> state-machine-pedido.md  (a actualizar con C-01 + R-01)
                       |
                       +---> bug-report-p16b.md       (a actualizar con D-03)
```

**Para el lector apurado:** leer SOLO la **Tabla maestra** y la seccion **Contradicciones**. El resto es referencia.

**Para el implementador del WebAPI (P-16b):** prestar atencion especial a D-01 (Verificado por cliente), D-02 (outbox alcance), D-03 (P-16 dos hallazgos), D-04 (Killios como caso complejo).

**Para la Ciclo 8:** las 5 sub-preguntas SQL-respondibles son el siguiente trabajo autonomo del agente brain. La sub-Q de prefactura (P-23) requiere identificar nuevo respondedor humano.

---

— Brain TomWMS · Consolidacion Ciclo 7 · Generado 28 abril 2026 —
