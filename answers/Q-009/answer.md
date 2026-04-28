---
protocolVersion: 2
answerForId: Q-009
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered
dimension: afinidad-de-procesos
---

# Q-009 - Outbox alcance real (3 BDs)

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server contra EC2 52.41.114.122,1437.
>
> **Dimension**: esta respuesta es de **afinidad de procesos**. Los numeros observados en nuestro snapshot NO se comparan con los reportados por las personas del cliente (que trabajan con backups recientes propios). La comparacion cuantitativa queda diferida a un segmento futuro de afinidad-de-datos.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Hallazgo de PROCESO

El outbox `i_nav_transacciones_out` tiene 4 FKs (`idordencompra`, `idrecepcionenc`, `idpedidoenc`, `iddespachoenc`). En nuestro snapshot, en las 2 BDs con datos (K7 y BB), el conteo de `con_pedido` es **identico** al de `con_despacho` (no aparece ningun caso de pedido sin despacho). Esto indica que el outbox **emite cuando hay despacho** y arrastra el IdPedidoEnc como FK, no que emita eventos de pedido sueltos.

Es un patron de **confirmacion-de-despacho**, no event-source de cambios de pedido.

## Datos observados en nuestro snapshot (no comparables)

| BD | con_oc | con_recepcion | con_pedido | con_despacho | total |
|----|-------:|--------------:|-----------:|-------------:|------:|
| K7-PRD | 4.394 | 16.553 | 19.799 | 19.799 | 24.193 |
| BB-PRD | 110.902 | 514.788 | 422.427 | 422.427 | 533.329 |
| C9-QAS | 0 | 0 | 0 | 0 | 0 |

## Datos crudos

- `q1-outbox-counts-K7-PRD.csv`
- `q1-outbox-counts-BB-PRD.csv`
- `q1-outbox-counts-C9-QAS.csv` (vacio - QAS sin trafico)

## Decision recomendada

Simplificar el bridge Navigator de 4 tipos a 2 (recepcion + despacho con FKs adicionales OC y pedido).

## Eventos generados en _inbox

- H08 (`20260428-1907-H08-outbox-registra-solo-despachos-no-pedidos-sueltos.json`)
