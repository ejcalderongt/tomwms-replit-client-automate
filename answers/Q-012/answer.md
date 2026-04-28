---
protocolVersion: 2
answerForId: Q-012
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered-pivot
---

# Q-012 - CEALSA QAS corte jornada - excepciones

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server (sa) contra EC2 52.41.114.122,1437.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Resultado

**CERRADA con pivot**. C9-QAS tiene 0 filas en `trans_despacho_det` (entorno QAS sin trafico productivo), asi que la query se re-ejecuto contra K7-PRD y BB-PRD para obtener evidencia significativa:

| BD | Total despachos | Cruzan jornada (>0 dias) | pct |
|----|----------------:|-------------------------:|----:|
| K7-PRD | 19.799 | 9.799 | **49,49%** |
| BB-PRD | 420.505 | 89.674 | **21,33%** |
| C9-QAS | 0 | 0 | N/A |

**Distribucion BB-PRD**: cola larguisima - hay buckets de hasta 78 dias entre creacion y despacho. La mayor concentracion: 0d (330k), 1d (51k), 2d (10k), pero hay picos en 19d (2167), 35d (1089), 36d (1633), 47d (847).

## Datos crudos

- `q2-distribucion-dias-cruce-K7-PRD.csv` (10 buckets)
- `q2-distribucion-dias-cruce-BB-PRD.csv` (40 buckets)
- `q1-cruzan-jornada-detalle.csv` (vacio - C9-QAS sin datos)

## Hallazgos derivados

1. **Refuerza fuertemente P3-2025-04-22-Q012-CORTE** (corte por idle, no por reloj): si **1 de cada 2** pedidos en K7 y **1 de cada 5** en BB cruzan jornada, el "corte rigido a las 18:00" lastima brutalmente esos casos.
2. **BB tiene patron operativo distinto**: la cola larga de despachos sugiere pedidos que esperan stock, transporte programado o ventanas comerciales. No son anomalias - son operacion normal.
3. **C9-QAS confirmado como entorno-test estable**: 0 despachos = es ambiente de smoke-test, no UAT continuo.
4. **Decision para nueva WebAPI**: el "corte de jornada" debe ser parametro por cliente y ademas opcional - puede ser "no cortar nunca" para clientes con cola larga como BB.

## Eventos generados en _inbox

- (sin event nuevo - refuerza P3-Q012 existente)
