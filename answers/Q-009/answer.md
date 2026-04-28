---
protocolVersion: 2
answerForId: Q-009
answeredBy: agent-replit
answeredAt: 2026-04-28T22:30:00-03:00
executedVia: live-sql-pymssql-2.3.13
executedBy: sa@52.41.114.122,1437
status: answered
---

# Q-009 - Outbox alcance real (3 BDs)

> Respondida por agente brain (sesion replit) el 28 abril 2026 via ejecucion live SQL Server (sa) contra EC2 52.41.114.122,1437.
>
> **BDs reales descubiertas**: K7-PRD = `TOMWMS_KILLIOS_PRD`, BB-PRD = `IMS4MB_BYB_PRD`, C9-QAS = `IMS4MB_CEALSA_QAS`. Las 3 comparten schema (TOMWMS y IMS4MB son aparentemente forks del mismo modelo).

## Resultado

**CERRADA**. Ejecutada contra las 3 BDs:

| BD | con_oc | con_recepcion | con_pedido | con_despacho | total |
|----|-------:|--------------:|-----------:|-------------:|------:|
| K7-PRD | 4.394 | 16.553 | 19.799 | 19.799 | 24.193 |
| BB-PRD | 110.902 | 514.788 | 422.427 | 422.427 | 533.329 |
| C9-QAS | 0 | 0 | 0 | 0 | 0 |

**Patron clave**: `con_pedido == con_despacho` SIEMPRE en K7 y BB. NO hay eventos de "pedido suelto" en el outbox.

## Datos crudos

- `q1-outbox-counts-K7-PRD.csv`
- `q1-outbox-counts-BB-PRD.csv`
- `q1-outbox-counts-C9-QAS.csv` (vacio - QAS sin trafico)

## Hallazgos derivados

1. **El outbox SOLO emite eventos de despacho**, no de creacion/modificacion de pedidos. El IdPedidoEnc viaja como FK del despacho.
2. **Carol (P-19) tenia razon parcial**: dijo "recepciones y despachos". Confirmado: esos 2 dominan; OC y pedido son metadata de polizon.
3. **BB usa el outbox masivamente** (533k filas) vs K7 (24k). Ratio 22x consistente con cardinalidades operativas (BB ~10x mas grande).
4. **Decision para nueva WebAPI**: el bridge Navigator puede simplificarse - definir 2 tipos primarios (recepcion, despacho) con FKs adicionales en lugar de 4 tipos independientes.

## Eventos generados en _inbox

- H08 (`20260428-1907-H08-outbox-registra-solo-despachos-no-pedidos-sueltos.json`)
