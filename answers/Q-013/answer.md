---
protocolVersion: 2
answerForId: Q-013
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered
dimension: afinidad-de-procesos
---

# Q-013 - CEALSA QAS poliza

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server contra EC2 52.41.114.122,1437.
>
> **Dimension**: esta respuesta es de **afinidad de procesos**. Los numeros observados en nuestro snapshot NO se comparan con los reportados por las personas del cliente (que trabajan con backups recientes propios). La comparacion cuantitativa queda diferida a un segmento futuro de afinidad-de-datos.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Hallazgo de PROCESO

**Estructura real de `trans_pe_pol`**: 47 columnas. PK: `IdOrdenPedidoPol`. FK: `IdOrdenPedidoEnc`. Campos: `NoPoliza`, `bl_no`, `viaje_no`, `buque_no`, `dua`, `IdRegimen`, `nit_imp_exp`, `clave_aduana`, totales multi-moneda (USD/flete/seguro/general/liquidar/otros), dual-fechas (poliza/aceptacion/llegada/abordaje), `activo` bit. Es un trade compliance record completo.

**Naming inconsistente**: la PK de `trans_pe_enc` es `IdPedidoEnc`, pero la FK en `trans_pe_pol` se llama `IdOrdenPedidoEnc`. Verificacion empirica: valores identicos (183=183, 184=184). Es alias - misma columna logica.

**El modelo permite gap fiscal**: NO hay constraint server-side que prevenga cerrar un pedido fiscal sin trans_pe_pol. Coherente con H-04 (dual-state frontend-only).

## Datos observados en nuestro snapshot (no comparables)

**Estructura**: 47 columnas, mayoria nullable. `activo` bit permite soft-delete (sugiere flujos de correccion/anulacion).

**Pedidos fiscales (control_poliza=1) en C9-QAS snapshot**:

| Metrica | Valor |
|---------|------:|
| Pedidos fiscales | 1.441 |
| Con registro en trans_pe_pol | 1.416 |
| Sin registro de poliza | 25 |

## Datos crudos

- `q1-estructura-poliza.csv` (47 columnas con tipo y nullability)
- `q2-fiscales-con-sin.csv`

## Decision recomendada

(a) documentar el alias `IdOrdenPedidoEnc==IdPedidoEnc` en schema-canon, (b) refactor controlado para unificar naming en versiones futuras, (c) implementar P3-FISCAL-LOCK con validacion bloqueante server-side.

## Eventos generados en _inbox

- H09 (naming inconsistente)
- H10 (modelo permite gap fiscal)
