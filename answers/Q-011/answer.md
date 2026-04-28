---
protocolVersion: 2
answerForId: Q-011
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered
dimension: afinidad-de-procesos
---

# Q-011 - Killios bypass despachado

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server contra EC2 52.41.114.122,1437.
>
> **Dimension**: esta respuesta es de **afinidad de procesos**. Los numeros observados en nuestro snapshot NO se comparan con los reportados por las personas del cliente (que trabajan con backups recientes propios). La comparacion cuantitativa queda diferida a un segmento futuro de afinidad-de-datos.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Hallazgo de PROCESO

El camino tecnico "estado='Despachado' sin filas en `trans_despacho_det`" **existe como camino real** en el modelo de TOMWMS_KILLIOS_PRD. NO hay constraint server-side que lo prevenga. Esto valida empiricamente la hipotesis de fondo de P-19: el bypass es tecnicamente posible.

El proceso de validacion server-side bloqueante NO esta implementado en el modelo actual.

## Datos observados en nuestro snapshot (no comparables)

| Metrica | Valor en nuestro snapshot |
|---------|------------------------:|
| Pedidos en estado='Despachado' | 3.989 |
| Pedidos sin filas en trans_despacho_det | 1 (en 2025-06) |
| Pedidos con despacho real | 3.988 |

## Datos crudos

- `q1-bypass-count-killios.csv`
- `q2-bypass-temporal-killios.csv`

## Decision recomendada

ADR-012 se sostiene en este HALLAZGO DE PROCESO (el camino existe, hay que decidir como tratarlo). La frecuencia exacta queda diferida al segmento de afinidad-de-datos con backups sincronizados; las decisiones sobre rate-limit, permisos especiales, etc. NO se reajustan con base a este snapshot.

## Eventos generados en _inbox

- H06 (`20260428-1905-H06-q011-bypass-real-1-no-43-simplificar-adr-012.json`) - filename historico, contenido refleja el principio de afinidad-procesos
