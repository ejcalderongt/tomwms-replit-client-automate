---
protocolVersion: 2
answerForId: Q-012
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered-pivot
dimension: afinidad-de-procesos
---

# Q-012 - CEALSA QAS corte jornada (pivot a K7+BB)

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server contra EC2 52.41.114.122,1437.
>
> **Dimension**: esta respuesta es de **afinidad de procesos**. Los numeros observados en nuestro snapshot NO se comparan con los reportados por las personas del cliente (que trabajan con backups recientes propios). La comparacion cuantitativa queda diferida a un segmento futuro de afinidad-de-datos.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Hallazgo de PROCESO

El modelo `(trans_pe_enc.fec_agr, trans_despacho_enc.fec_agr)` **permite** que un despacho se cierre dias o semanas despues de la creacion del pedido. NO hay constraint que fuerce despacho-en-jornada. El "corte de jornada" es politica operativa, no constraint del modelo.

C9-QAS tiene 0 filas en `trans_despacho_det` (entorno smoke-test sin trafico productivo), por eso la query se pivoteo a K7-PRD y BB-PRD donde el camino se ejerce.

## Datos observados en nuestro snapshot (no comparables)

| BD | Total despachos | Con dias_diferencia > 0 |
|----|----------------:|------------------------:|
| K7-PRD | 19.799 | 9.799 |
| BB-PRD | 420.505 | 89.674 |
| C9-QAS | 0 | 0 |

BB tiene cola larga - buckets observados de hasta 78 dias entre creacion y despacho.

## Datos crudos

- `q2-distribucion-dias-cruce-K7-PRD.csv` (10 buckets)
- `q2-distribucion-dias-cruce-BB-PRD.csv` (40 buckets)
- `q1-cruzan-jornada-detalle.csv` (vacio - C9-QAS sin datos)

## Decision recomendada

Refuerza P3-2025-04-22-Q012-CORTE: el corte por idle-de-tarea es mas afin al modelo que el corte por reloj. La nueva WebAPI debe ofrecer el corte como parametro por cliente y opcional.

## Eventos generados en _inbox

- (sin event nuevo - refuerza P3-Q012 existente)
