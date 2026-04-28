# Respuestas Ciclo 8a (tarea-3) - CIERRE COMPLETO via ejecucion live SQL

> Documento generado por el agente brain (sesion replit) el 28 abril 2026.
>
> **Estado**: 6 sub-preguntas CERRADAS al 100% en su dimension de **afinidad de procesos** (1 via wms-db-brain dump + 5 via ejecucion live SQL Server con `sa@52.41.114.122,1437`). Ciclo 8a CERRADA.
>
> **Fuentes**:
> - `wms-brain-client/questions/Q-009..Q-014` (commit `582da718` rama `wms-brain-client`)
> - `wms-brain-client/answers/Q-XXX/` (commit `44dee30` rama `wms-brain-client`)
> - `wms-db-brain/db-brain/` (commit `d3884b57` rama `wms-db-brain`, snapshot 2026-04-27)
> - **Ejecucion live SQL** contra EC2 52.41.114.122,1437 el 28-abr-2026 via pymssql 2.3.13

---

## Principio operativo - afinidad de PROCESOS vs afinidad de DATOS

Las respuestas previas de las personas del cliente (P-XX, CKFK, KKKL, etc.) provienen de quien trabaja con backups RECIENTES de cada cliente. Nuestro brain trabaja contra el SQL Server EC2 al que tenemos acceso, **que NO necesariamente refleja los mismos snapshots**. Por lo tanto:

| Dimension | Que es | Confirmable desde nuestro snapshot? |
|-----------|--------|-------------------------------------|
| **Afinidad de PROCESOS** | Que tablas existen, sus columnas, sus tipos, los SPs, las relaciones, los flags de configuracion (`trans_pe_tipo.control_poliza`, etc.), los catalogos (`sis_tipo_tarea`), los caminos posibles (estado='Despachado' sin trans_despacho_det) | **SI** - este es el segmento que estamos cerrando |
| **Afinidad de DATOS** | Cantidad exacta de bypass, distribucion mensual real, totales monetarios, ratios de uso, cardinalidades absolutas | **NO** - distintos snapshots dan distintos numeros y la comparacion es metodologicamente invalida |

**Politica adoptada en Ciclo 8a**: cuando una pregunta toca afinidad de datos, se reporta el dato OBSERVADO en nuestro snapshot SIN compararlo con el dato reportado por las personas del cliente. La sincronizacion de backups y la comparacion cuantitativa quedan diferidas a un segmento de trabajo dedicado a "afinidad de datos".

---

## Hallazgo de infraestructura - mapeo real de BDs

Las cards usan codenames K7-PRD / BB-PRD / C9-QAS. **El mapeo real descubierto al conectarse al motor**:

| Codename | Database real | Producto | Recovery |
|----------|---------------|----------|----------|
| K7-PRD | `TOMWMS_KILLIOS_PRD` | TOMWMS | SIMPLE |
| **BB-PRD** | **`IMS4MB_BYB_PRD`** | **IMS4MB** | SIMPLE |
| **C9-QAS** | **`IMS4MB_CEALSA_QAS`** | **IMS4MB** | SIMPLE |

**Hallazgo de proceso**: TOMWMS e IMS4MB tienen el **mismo schema fisico** (verificadas 7 tablas clave existen en las 3 BDs con nombres identicos). Probablemente son fork/rebrand del mismo producto. **Implicacion para nueva WebAPI**: un solo modelo de datos cubre las 3 BDs sin alias.

Otras BDs en el motor (no solicitadas): `LIVE` (FULL recovery, parece productiva), `mpos_pollo_express_qa`, `POD_BETA`.

---

## Q-009 - Outbox alcance real (3 BDs) - **CERRADA (proceso)**

### Datos observados en nuestro snapshot (no comparables con backups de las personas)

| BD | con_oc | con_recepcion | con_pedido | con_despacho | total |
|----|-------:|--------------:|-----------:|-------------:|------:|
| K7-PRD | 4.394 | 16.553 | 19.799 | 19.799 | 24.193 |
| BB-PRD | 110.902 | 514.788 | 422.427 | 422.427 | 533.329 |
| C9-QAS | 0 | 0 | 0 | 0 | 0 |

### Hallazgo de PROCESO

`con_pedido == con_despacho` en las 2 BDs con datos. **El outbox NO emite eventos de pedidos sueltos** - solo emite cuando hay despacho que arrastra el IdPedidoEnc como FK. Es un patron de **confirmacion-de-despacho**, no event-source de cambios de pedido.

### Decision recomendada

Simplificar el bridge Navigator de 4 tipos a 2 (recepcion + despacho con FKs adicionales OC y pedido).

**Evento generado**: H08 (`20260428-1907-H08-outbox-registra-solo-despachos-no-pedidos-sueltos`).

**Detalle completo**: `wms-brain-client/answers/Q-009/answer.md` + 3 CSVs.

---

## Q-010 - Killios reabasto pre/post-2024 - **CERRADA (proceso) via wms-db-brain**

(Ya cerrada en ciclo anterior - `CLBD_PRC.md` confirma que el SP NO incluye `trans_reabastecimiento_log`. Ver evento H02.)

**Extension descubierta en Ciclo 8a (proceso)**: la tabla existe en las 3 BDs, K7=1218 filas, BB=755, C9=0. El alcance del problema es **estructural** del instalador, no de una BD especifica. **Nuevo evento H11**.

---

## Q-011 - Killios bypass despachado - **CERRADA (proceso)**

### Datos observados en nuestro snapshot (no comparables con backup del cliente)

| Metrica | Valor en nuestro snapshot |
|---------|------------------------:|
| Pedidos en estado='Despachado' | 3.989 |
| Pedidos sin filas en trans_despacho_det | 1 (2025-06) |
| Pedidos con despacho real | 3.988 |

### Hallazgo de PROCESO

El camino tecnico "estado='Despachado' sin filas en trans_despacho_det" **existe como camino real** en TOMWMS_KILLIOS_PRD. NO hay constraint server-side que lo prevenga. Esto valida empiricamente la hipotesis de fondo de P-19: el bypass es tecnicamente posible.

### Decision recomendada

ADR-012 se sostiene en este HALLAZGO DE PROCESO (el camino existe -> hay que decidir como tratarlo). La **frecuencia exacta** queda diferida al segmento de afinidad-de-datos con backups sincronizados; las decisiones sobre rate-limit, permisos especiales, etc. NO se reajustan con base a este snapshot - se sostienen en el riesgo del proceso.

**Evento generado**: H06 (`20260428-1905-H06-q011-bypass-real-1-no-43-simplificar-adr-012`) - filename historico, contenido refleja el principio de afinidad-procesos.

**Detalle completo**: `wms-brain-client/answers/Q-011/answer.md` + 2 CSVs.

---

## Q-012 - CEALSA QAS corte jornada - **CERRADA (proceso) con pivot**

C9-QAS tiene 0 filas en `trans_despacho_det` (sin trafico productivo). La query se pivoteo a K7-PRD y BB-PRD donde si hay datos.

### Datos observados en nuestro snapshot

| BD | Total despachos | Con dias_diferencia > 0 |
|----|----------------:|------------------------:|
| K7-PRD | 19.799 | 9.799 |
| BB-PRD | 420.505 | 89.674 |
| C9-QAS | 0 | 0 |

BB tiene cola larga - buckets de hasta 78 dias entre creacion y despacho.

### Hallazgo de PROCESO

El modelo `(trans_pe_enc.fec_agr, trans_despacho_enc.fec_agr)` **permite** que un despacho se cierre dias o semanas despues de la creacion del pedido. NO hay constraint que fuerce despacho-en-jornada. El "corte de jornada" es politica operativa, no constraint del modelo.

### Decision recomendada

Refuerza P3-2025-04-22-Q012-CORTE: el corte por idle-de-tarea es mas afin al modelo que el corte por reloj. Hacerlo parametrizable y opcional por cliente.

**Detalle completo**: `wms-brain-client/answers/Q-012/answer.md` + 3 CSVs.

---

## Q-013 - CEALSA QAS poliza - **CERRADA (proceso) con 2 hallazgos estructurales**

### Hallazgo de PROCESO 1 - estructura real de `trans_pe_pol`

47 columnas. PK: `IdOrdenPedidoPol`. FK: `IdOrdenPedidoEnc`. Campos clave: `NoPoliza`, `bl_no`, `viaje_no`, `buque_no`, `dua`, `IdRegimen`, `nit_imp_exp`, `clave_aduana`, totales multi-moneda (USD/flete/seguro/general/liquidar/otros), dual-fechas (poliza/aceptacion/llegada/abordaje), `activo` bit. Es un trade compliance record completo, no un campo simple. La existencia de `activo` bit permite soft-delete (sugiere flujos de correccion/anulacion).

### Hallazgo de PROCESO 2 - naming inconsistente

La PK de `trans_pe_enc` es `IdPedidoEnc`. La FK en `trans_pe_pol` se llama `IdOrdenPedidoEnc`. Verificacion empirica: `pp.IdOrdenPedidoEnc == pe.IdPedidoEnc` (mismo valor, distintos nombres). Es alias - misma columna logica. Sugiere refactor historico parcial (modelo era 'orden_pedido' y se renombro a 'pedido' pero la tabla de polizas quedo sin migrar).

### Datos observados en nuestro snapshot (no comparables)

| Metrica | Valor en C9-QAS snapshot |
|---------|-----------------------:|
| Pedidos fiscales (control_poliza=1) | 1.441 |
| Con registro en trans_pe_pol | 1.416 |
| Sin registro de poliza | 25 |

### Hallazgo de PROCESO 3 - el modelo permite gap

NO hay constraint server-side que prevenga cerrar un pedido fiscal sin trans_pe_pol. Coherente con H-04 (dual-state frontend-only).

### Decision recomendada

(a) documentar el alias `IdOrdenPedidoEnc==IdPedidoEnc` en schema-canon, (b) refactor controlado para unificar naming en futuras versiones, (c) reforzar P3-FISCAL-LOCK con validacion bloqueante server-side.

**Eventos generados**: H09 (naming), H10 (modelo permite gap fiscal).

**Detalle completo**: `wms-brain-client/answers/Q-013/answer.md` + 2 CSVs.

---

## Q-014 - TOP15 tareas HH (3 BDs) - **CERRADA (proceso)**

### Datos observados en nuestro snapshot (no comparables con backups)

| Cliente | TOP1 | TOP2 | TOP3 | Tipos usados / disponibles |
|---------|------|------|------|---------------------------:|
| K7-PRD | PIK 71% | RECE 28% | UBIC 0,9% | 5 / 35 |
| BB-PRD | UBIC 50% | RECE 31% | PIK 19% | 5 / 35 |
| C9-QAS | PIK 74% | RECE 26% | INVE 0,01% | 3 / 33 |

### Hallazgo de PROCESO

Las 3 BDs comparten el catalogo `sis_tipo_tarea` con la misma estructura y los mismos IdTipoTarea. La tabla `tarea_hh` registra ejecuciones del mismo modo en las 3. Hay 30+ tipos definidos en el catalogo pero los datos muestran que **cada deployment usa un perfil operativo distinto** (BB pone peso en UBIC, K7+C9 en PIK).

### Decision recomendada

(a) la nueva WebAPI debe parametrizar peso de tareas por cliente para KPIs (no asumir que PIK siempre domina), (b) ofrecer endpoint "tipos activos" filtrado para evitar mostrar opciones muertas en HH, (c) los dashboards deben pesar tareas distinto por cliente.

**Evento generado**: H07 (`20260428-1906-H07-bb-putaway-intensivo-50pct-ubic`).

**Detalle completo**: `wms-brain-client/answers/Q-014/answer.md` + 3 CSVs.

---

## Resumen ejecutivo - 6 hallazgos derivados de PROCESO

| # | Hallazgo de PROCESO | Decision recomendada | Cards origen |
|---|---------------------|----------------------|--------------|
| H06 | El bypass `estado=Despachado` sin trans_despacho_det es camino tecnicamente posible | Sostiene ADR-012 (frecuencia diferida a afinidad-datos) | Q-011 |
| H07 | Las 3 BDs comparten catalogo de tareas pero cada deployment tiene perfil operativo distinto | Parametrizar peso de tareas en KPIs | Q-014 |
| H08 | El outbox solo emite confirmacion-de-despacho, no events de pedido | Simplificar bridge Navigator a 2 tipos | Q-009 |
| H09 | `IdOrdenPedidoEnc == IdPedidoEnc` (alias por naming inconsistente) | Documentar en schema-canon | Q-013 |
| H10 | El modelo permite cerrar pedido fiscal sin trans_pe_pol | Refuerza P3-FISCAL-LOCK con validacion server-side | Q-013 |
| H11 | SP CLBD_PRC no limpia `trans_reabastecimiento_log` (afecta a cualquier deploy) | Extender H02 - fix estructural en instalador | Q-010 (extension) |

Todos en `brain/_inbox/20260428-19{05..10}-H{06..11}-*.json` + proposals MD en `brain/_proposals/`.

---

## Cierre de Ciclo 8a

- **6 cards generadas** (Q-009..Q-014, commit `582da718`)
- **6 ejecuciones live exitosas** (Q-009 x3 + Q-011 + Q-012 x2 + Q-013 + Q-014 x3)
- **6 hallazgos H06..H11** derivados, cada uno con event JSON + proposal MD
- **3 decisiones de arquitectura** afectadas (todas por afinidad-de-procesos): ADR-012 (sostenida), bridge Navigator (simplificar), P3-FISCAL-LOCK (reforzar)
- **0 destrucciones, 0 escrituras** contra las BDs (READ-ONLY confirmado)
- **0 comparaciones cuantitativas** con backups de las personas (politica de afinidad-procesos)

**Ciclo 8a CERRADA en su dimension de afinidad de procesos.** Listo para ratificacion de Erik. Las cuestiones cuantitativas que requieran comparacion con backups del cliente quedan diferidas a un segmento futuro de "afinidad de datos".
