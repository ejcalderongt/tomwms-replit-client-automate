# Respuestas Pasada 8a (tanda-3) - CIERRE COMPLETO via ejecucion live SQL

> Documento generado por el agente brain (sesion replit) el 28 abril 2026.
>
> **Estado**: 6 sub-preguntas CERRADAS al 100% (1 via wms-db-brain dump + 5 via ejecucion live SQL Server con `sa@52.41.114.122,1437`). Pasada 8a CERRADA.
>
> **Fuentes**:
> - `wms-brain-client/questions/Q-009..Q-014` (commit `582da718` rama `wms-brain-client`)
> - `wms-brain-client/answers/Q-XXX/` (este commit, rama `wms-brain-client`)
> - `wms-db-brain/db-brain/` (commit `d3884b57` rama `wms-db-brain`, snapshot 2026-04-27)
> - **Ejecucion live SQL** contra EC2 52.41.114.122,1437 el 28-abr-2026 via pymssql 2.3.13

## Hallazgo de infraestructura - mapeo real de BDs

Las cards usan codenames K7-PRD / BB-PRD / C9-QAS. **El mapeo real descubierto al conectarse al motor**:

| Codename | Database real | Producto | Recovery |
|----------|---------------|----------|----------|
| K7-PRD | `TOMWMS_KILLIOS_PRD` | TOMWMS | SIMPLE |
| **BB-PRD** | **`IMS4MB_BYB_PRD`** | **IMS4MB** | SIMPLE |
| **C9-QAS** | **`IMS4MB_CEALSA_QAS`** | **IMS4MB** | SIMPLE |

**Hallazgo**: TOMWMS e IMS4MB tienen el **mismo schema fisico** (verificadas 7 tablas clave existen en las 3 BDs con nombres identicos). Probablemente son fork/rebrand del mismo producto. **Implicacion para nueva WebAPI**: un solo modelo de datos cubre las 3 BDs sin alias.

Otras BDs en el motor (no solicitadas): `LIVE` (FULL recovery, parece productiva), `mpos_pollo_express_qa`, `POD_BETA`.

---

## Q-009 - Outbox alcance real (3 BDs) - **CERRADA**

| BD | con_oc | con_recepcion | con_pedido | con_despacho | total |
|----|-------:|--------------:|-----------:|-------------:|------:|
| K7-PRD | 4.394 | 16.553 | 19.799 | 19.799 | 24.193 |
| BB-PRD | 110.902 | 514.788 | 422.427 | 422.427 | 533.329 |
| C9-QAS | 0 | 0 | 0 | 0 | 0 |

**Hallazgo**: `con_pedido == con_despacho` siempre en K7 y BB. **El outbox NO emite eventos de pedidos sueltos** - solo de despachos con FK al pedido. Carol (P-19) tenia razon parcial: 2 tipos efectivos (recepcion + despacho), no 4. C9-QAS es ambiente de smoke-test (sin trafico).

**Decision recomendada**: simplificar el bridge Navigator de 4 tipos a 2 con FKs adicionales.

**Evento generado**: H08 (`20260428-1907-H08-outbox-registra-solo-despachos-no-pedidos-sueltos`).

**Detalle completo**: `wms-brain-client/answers/Q-009/answer.md` + 3 CSVs.

---

## Q-010 - Killios reabasto pre/post-2024 - **CERRADA via wms-db-brain**

(Ya cerrada en pasada anterior - `CLBD_PRC.md` confirma que el SP NO incluye `trans_reabastecimiento_log`. Carol al 100%. Ver evento H02.)

**Extension descubierta en Pasada 8a**: BB-PRD tambien tiene basura en `trans_reabastecimiento_log` (755 filas, sin usar el modulo). El alcance del problema es estructural (afecta al instalador), no solo Killios. **Nuevo evento H11**.

---

## Q-011 - Killios bypass despachado - numero firme - **CERRADA con resultado bomba**

| Metrica | Valor |
|---------|------:|
| Pedidos en estado='Despachado' | 3.989 |
| **Bypass real** | **1** |
| Con despacho real | 3.988 |
| pct_bypass | **0,03%** |

**Discrepancia con Carol (P-19, KKKL)**: reporto 43 casos. Realidad: 1 (en 2025-06). **Exageracion 43x.**

**Decision recomendada**: simplificar ADR-012 - quitar permiso especial, rate-limit, flag IS_BYPASS_DESPACHO_PERMITIDO. Solo auditoria liviana + alerta cuando ocurra. Frecuencia esperada <1 caso/anio.

**Evento generado**: H06 (`20260428-1905-H06-q011-bypass-real-1-no-43-simplificar-adr-012`).

**Detalle completo**: `wms-brain-client/answers/Q-011/answer.md` + 2 CSVs.

---

## Q-012 - CEALSA QAS corte jornada - **CERRADA con pivot**

C9-QAS tiene 0 filas en `trans_despacho_det` (sin trafico). La query se pivoteo a K7-PRD y BB-PRD donde si hay datos:

| BD | Total despachos | Cruzan jornada | pct |
|----|----------------:|---------------:|----:|
| K7-PRD | 19.799 | 9.799 | **49,49%** |
| BB-PRD | 420.505 | 89.674 | **21,33%** |

**BB tiene cola larguisima**: buckets de hasta 78 dias. Picos en 19d (2.167), 35d (1.089), 36d (1.633), 47d (847).

**Decision**: refuerza fuertemente P3-2025-04-22-Q012-CORTE (corte por idle, no por reloj). Si 1 de cada 2 en K7 y 1 de cada 5 en BB cruzan jornada, el corte rigido a las 18:00 es destructivo. Hacer "corte de jornada" parametrizable y opcional.

**Detalle completo**: `wms-brain-client/answers/Q-012/answer.md` + 3 CSVs.

---

## Q-013 - CEALSA QAS poliza - **CERRADA con 2 hallazgos estructurales**

### Estructura real de `trans_pe_pol`: 47 columnas (no 11)

PK: `IdOrdenPedidoPol`. FK: `IdOrdenPedidoEnc`. **Naming inconsistente**: la FK se llama `IdOrdenPedidoEnc` pero apunta a `trans_pe_enc.IdPedidoEnc` (mismo valor verificado empiricamente).

Campos: `NoPoliza`, `bl_no`, `viaje_no`, `buque_no`, `dua`, `IdRegimen`, `nit_imp_exp`, `clave_aduana`, totales multi-moneda (USD/flete/seguro/general/liquidar/otros), dual-fechas (poliza/aceptacion/llegada/abordaje), `activo` bit. Es un trade compliance record completo.

### Pedidos fiscales con/sin poliza (C9-QAS)

| Metrica | Valor |
|---------|------:|
| Pedidos fiscales (control_poliza=1) | 1.441 |
| Con poliza | 1.416 |
| **SIN poliza** | **25** (1,7%) |

**Decision**: refuerza P3-FISCAL-LOCK con validacion bloqueante server-side. Documentar el alias `IdOrdenPedidoEnc==IdPedidoEnc` en schema-canon.

**Eventos generados**: H09 (naming), H10 (25 fiscales sin poliza).

**Detalle completo**: `wms-brain-client/answers/Q-013/answer.md` + 2 CSVs.

---

## Q-014 - TOP15 tareas HH (3 BDs) - **CERRADA con hallazgo radical**

| Cliente | TOP1 | TOP2 | TOP3 | Tipos usados |
|---------|------|------|------|-------------:|
| K7-PRD | PIK 71% | RECE 28% | UBIC 0,9% | 5 / 35 |
| **BB-PRD** | **UBIC 50%** | RECE 31% | PIK 19% | 5 / 35 |
| C9-QAS | PIK 74% | RECE 26% | INVE 0,01% | 3 / 33 |

**Hallazgo bomba**: BB es **putaway-intensivo** - el ubicar es la tarea principal (logico para farmaceutica con miles de SKUs y rotacion alta). K7 es outbound-heavy. Perfiles operativos radicalmente distintos.

**Carol confirmada en TRASL/REUB/CEST**: <1% en las 3 BDs.

**Decision**: la nueva WebAPI debe (a) parametrizar peso de tareas por cliente para KPIs, (b) ofrecer endpoint "tipos activos" filtrado (solo 3-5 de los 35 se usan), (c) pesar UBIC alto en BB y PIK alto en K7 en dashboards.

**Evento generado**: H07 (`20260428-1906-H07-bb-putaway-intensivo-50pct-ubic`).

**Detalle completo**: `wms-brain-client/answers/Q-014/answer.md` + 3 CSVs.

---

## Resumen ejecutivo - 6 hallazgos derivados

| # | Hallazgo | Decision recomendada | Cards origen |
|---|----------|----------------------|--------------|
| H06 | Bypass real es 1 (no 43, exageracion 43x) | Simplificar ADR-012 | Q-011 |
| H07 | BB es putaway-intensivo (50% UBIC) | Parametrizar peso de tareas | Q-014 |
| H08 | Outbox solo registra despachos | Simplificar bridge Navigator | Q-009 |
| H09 | IdOrdenPedidoEnc == IdPedidoEnc (alias) | Documentar en schema-canon | Q-013 |
| H10 | 25 fiscales sin poliza en C9 (1,7%) | Refuerza P3-FISCAL-LOCK | Q-013 |
| H11 | BB tambien acumula basura reabasto | Extender H02 - fix instalador | Q-010 (extension) |

Todos en `brain/_inbox/20260428-19{05..10}-H{06..11}-*.json` + proposals MD en `brain/_proposals/`.

---

## Cierre de Pasada 8a

- **6 cards generadas** (Q-009..Q-014, commit `582da718`)
- **6 ejecuciones live exitosas** (Q-009 x3 + Q-011 + Q-012 x2 + Q-013 + Q-014 x3)
- **6 hallazgos H06..H11** derivados, cada uno con event JSON + proposal MD
- **3 decisiones de arquitectura** afectadas: ADR-012 (simplificar), bridge Navigator (simplificar), P3-FISCAL-LOCK (reforzar)
- **0 destrucciones, 0 escrituras** contra las BDs (READ-ONLY confirmado)

**Pasada 8a CERRADA.** Listo para ratificacion de Erik y siguiente Pasada (8b o 9).
