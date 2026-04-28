---
protocolVersion: 2
answerForId: Q-011
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered
---

# Q-011 - Killios bypass despachado - numero firme

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server (sa) contra EC2 52.41.114.122,1437.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Resultado

**CERRADA con resultado bomba**. Ejecutada contra TOMWMS_KILLIOS_PRD:

| Metrica | Valor |
|---------|------:|
| Pedidos en estado='Despachado' | 3.989 |
| **Bypass real (sin filas en trans_despacho_det)** | **1** |
| Con despacho real | 3.988 |
| pct_bypass | **0,03%** |

**Distribucion temporal**: 1 unico caso, en 2025-06.

**Discrepancia con Carol (P-19, KKKL)**: ella reporto 43 casos. Realidad: 1. **Exageracion de 43x**.

## Datos crudos

- `q1-bypass-count-killios.csv`
- `q2-bypass-temporal-killios.csv`

## Hallazgos derivados

1. **El bypass es un evento RARISIMO**, no un patron sistemico. <1 caso por anio.
2. **ADR-012 esta sobre-dimensionado**: requiere permiso especial, rate-limit, flag IS_BYPASS_DESPACHO_PERMITIDO, etc. Para 1 caso/anio es overkill.
3. **Decision recomendada**: simplificar ADR-012 a "auditoria liviana + alerta cuando ocurre". Quitar todo el aparataje de control preventivo.
4. **Por investigar**: que paso con ese 1 caso de 2025-06? Auditarlo manualmente para entender si fue bug, accion humana valida (correccion de error) o data quality.

## Eventos generados en _inbox

- H06 (`20260428-1905-H06-q011-bypass-real-1-no-43-simplificar-adr-012.json`)
