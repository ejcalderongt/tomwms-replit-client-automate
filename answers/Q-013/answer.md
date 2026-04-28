---
protocolVersion: 2
answerForId: Q-013
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered
---

# Q-013 - CEALSA QAS poliza - 11 campos

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server (sa) contra EC2 52.41.114.122,1437.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Resultado

**CERRADA con 2 hallazgos estructurales**.

### Estructura real de `trans_pe_pol`

47 columnas (no 11). Snapshot completo en `q1-estructura-poliza.csv`. Campos clave: `IdOrdenPedidoPol` (PK), `IdOrdenPedidoEnc` (FK alias), `NoPoliza`, `bl_no`, `viaje_no`, `buque_no`, `dua`, `IdRegimen`, `nit_imp_exp`, `clave_aduana`, totales (USD/flete/seguro/general/liquidar/otros), `fecha_aduana`, `fecha_aceptacion`, `activo` (bit).

### Pedidos fiscales con/sin poliza

| Metrica | Valor |
|---------|------:|
| Pedidos fiscales (control_poliza=1) | 1.441 |
| Con registro en trans_pe_pol | 1.416 |
| **SIN registro de poliza** | **25** |
| Gap | **1,7%** |

## Datos crudos

- `q1-estructura-poliza.csv` (47 columnas con tipo y nullability)
- `q2-fiscales-con-sin.csv`

## Hallazgos derivados

1. **Naming inconsistente**: la PK de `trans_pe_enc` es `IdPedidoEnc`, pero la FK en `trans_pe_pol` se llama `IdOrdenPedidoEnc`. Verificacion empirica: valores identicos (183=183, 184=184). Es alias, NO columna distinta.
2. **25 pedidos fiscales sin poliza** confirma empiricamente H-04 (poliza fiscal con dual-state frontend-only). Patron 1.7% de "captura perdida" es esperable cuando no hay validacion server-side bloqueante.
3. **Tabla mucho mas rica que lo que decia Carol** (11 campos): tiene 47 columnas con info aduanera completa (DUA, buque, viaje, regimen, NIT, totales multi-moneda, dual-fechas). Es un "trade compliance record" completo.
4. **Tiene `activo` bit**: permite soft-delete de polizas, lo que sugiere flujos de correccion/anulacion.
5. **Decision para nueva WebAPI**: (a) documentar el alias en el schema-canon, (b) refactor controlado para unificar naming, (c) implementar P3-FISCAL-LOCK con validacion bloqueante server-side.

## Eventos generados en _inbox

- H09 (naming inconsistente)
- H10 (25 fiscales sin poliza)
