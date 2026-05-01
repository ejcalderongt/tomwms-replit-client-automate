---
id: state-machine-pedido
tipo: process-flow
estado: vigente
titulo: Maquina de estados de pedido — confirmada por Erik + SQL
tags: [process-flow]
---

# Maquina de estados de pedido — confirmada por Erik + SQL

> **Version 3** (ciclo 9b). Combina respuestas de Erik (tarea 1) +
> hallazgos por SQL READ-ONLY (tarea 2). Pendientes derivados PEND-01 y
> PEND-03 cerrados.
>
> Snapshot: Killios PRD, 4,202 pedidos en `trans_pe_enc`.

## Estados (count desc)

| Estado | Conteo | % | Definicion canonica |
|---|---:|---:|---|
| `Despachado` | 3,989 | 95.0% | Cerrado, push al ERP completado |
| `Pickeado` | 86 | 2.0% | Picking concluido en HH, falta verif/desp |
| `Pendiente` | 73 | 1.7% | Picking asignado y en proceso |
| `Anulado` | 33 | 0.8% | Cancelado antes de despachar (con rollback) |
| `NUEVO` | 14 | 0.3% | Pedido creado, sin picking |
| `Verificado` | 7 | 0.2% | Verificacion HH concluida (opcional) |

## Maquina de estados

```
        +-----------+
        |  (insert) |   creacion manual (BackOffice) o
        +-----+-----+   push desde ERP (SAP/NAV via user_agr=6)
              |
              | [SI]                   [WMS] casos sinteticos
              v                          (15/4040 = 0.4% de PDV_NAV)
        +-----------+              +-----------+
        |   NUEVO   |---[WMS]----->| Pickeado  |
        |  (14)     |              |  (86)     |
        +-----+-----+              +-----+-----+
              |                          ^   |
              |  asignacion picker       |   | confirmacion
              v                          |   v
        +-----------+   confirmacion     |  +-----------+
        | Pendiente |--- HH marcaje ----+  | Verificado|
        |  (73)     |                      |  (7) OPC  |
        +-----+-----+                      +-----+-----+
              |                                   |
              |  cancela usuario                  | despacho confirmado
              v                                   v   + i_nav_transacciones_out (outbox) NAV/SAP OK
        +-----------+                       +-----------+
        |  Anulado  |<----- desde * --------|Despachado |
        |   (33)    |  con rollback         |  (3989)   |
        +-----------+  de stock_res         +-----------+
                       (excepto desde NUEVO    *terminal*
                        sin reserva: raro)

   Transiciones especiales:
   - NUEVO → Pickeado [WMS]: solo casos WMS-inyectados (traslados entre
     bodegas virtuales). Confirmado en SQL: ~15 PDV_NAV con fec_agr=fec_mod
     y estado=Despachado en una sola transaccion (con IdPickingEnc poblado
     pero proceso instantaneo).
   - Verificado: estado opcional, controlado por trans_pe_tipo.Verificar
   - Anulado desde NUEVO: posible pero raro (requiere stock_res previo)
```

## Matriz de transiciones permitidas

|  desde \ hacia  | NUEVO | Pendiente | Pickeado | Verificado | Despachado | Anulado |
|---|:---:|:---:|:---:|:---:|:---:|:---:|
| **(insert)** | SI | — | — | — | — | — |
| **NUEVO** | — | SI | WMS-only | — | — | raro |
| **Pendiente** | — | — | SI | — | — | SI |
| **Pickeado** | — | — | — | OPC | SI | SI |
| **Verificado** | — | — | — | — | SI | SI |
| **Despachado** | — | — | — | — | — | NO |
| **Anulado** | — | — | — | — | — | — |

**Leyenda**:
- `SI` = transicion canonica permitida y observada
- `OPC` = opcional (controlada por `trans_pe_tipo.Verificar`)
- `WMS-only` = solo casos sinteticos (~0.4% historico)
- `raro` = posible pero requiere stock reservado + orden explicita
- `NO` = transicion prohibida

## Banderas de comportamiento por tipo (`trans_pe_tipo`)

**RESUELTO en tarea 2 (PEND-01)**: las 3 banderas maestras son `Preparar`,
`Verificar`, `ReservaStock`.

### Killios PRD

| Tipo | Descripcion | Preparar | Verificar | ReservaStock | Pedidos historicos |
|---|---|:---:|:---:|:---:|---:|
| **PDV_NAV** | Pedido de Venta SAP | false | false | true | 4,040 (96.1%) |
| **PE0004** | Solicitud de traslado | true | false | false | 156 (3.7%) |
| **PE0001** | Pedido de Cliente | false | false | true | 4 |
| **PE0003** | Pedido de cliente | true | **true** | true | 0 (sin uso) |
| **TRAS_WMS** | Traslado Directo WMS | false | false | **false** | 0 (capacidad latente) |
| **DEVPROV** | Devolucion proveedor | false | — | — | (no contado) |

### BYB PRD (referencia)

- `PE0001` (Pedido_De_Bodega): `Verificar=true` → BYB usa el flujo Verificado.

### CEALSA QAS (referencia)

- `PE0001` (Transferencia Fiscal a General): `Verificar=true` + `control_poliza=true` (3PL fiscal).
- `PE0002` (PEDIDO SIN PREPARACION Y SIN VERIFICACION): los 3 flags relevantes en false.

## Reglas confirmadas (R-01..R-06 + R-07..R-09 nuevas de tarea 2)

### R-01: Anulado NO es automatico (Erik P-08)

Anulacion requiere accion explicita del usuario. NO implementar anulacion automatica por timeout o fallo de reserva en reserva-webapi.

### R-02: Pedidos sin lineas se ELIMINAN, no se anulan (Erik P-08)

El form previene pedidos sin lineas eliminandolos. Por eso solo 33 anulados historicos.

### R-03: Rollback de stock_res obligatorio en Anulado (Erik P-08)

Cuando un pedido pasa a Anulado, las filas en `stock_res` deben liberarse. Si el bridge encuentra `Anulado` con `stock_res` activo es bug.

### R-04: WMS puede inyectar pedidos en cualquier estado (Erik P-08, refinado SQL)

**Refinamiento de tarea 2**: la inyeccion WMS NO salta el modelo, lo SIMULA en una sola transaccion. Todos los PDV_NAV en estados intermedios tienen `IdPickingEnc` ≠ 0. Los 15 casos historicos de "inyeccion directa" tambien tienen IdPickingEnc poblado, solo que el proceso ocurre en milisegundos sin paso por HH.

### R-05: Verificado controlado por `trans_pe_tipo.Verificar` (Erik P-08, RESUELTO SQL)

**Resuelto en tarea 2**: la columna existe y se llama exactamente `Verificar` (bit). NO es controlable por el usuario en runtime — es configuracion del catalogo de tipos. Lo que Erik llamo "habilitar por usuario" probablemente refiere al **administrador** que configura el tipo.

### R-06: TRAS_WMS asume reserva previa, NO se valida (DEUDA-001, Erik P-18)

La bandera `ReservaStock=NO` no se valida explicitamente. Riesgo de doble reserva si alguien crea TRAS_WMS sin reserva upstream.

### R-07: Origen ERP marcado por `user_agr=6` (NUEVA, SQL)

99.7% de los pedidos historicos (4190/4202) los creo el usuario sintetico ID=6, que es el job que polea la interface NAV/SAP. Para distinguir "pedido humano" vs "pedido push ERP", reserva-webapi debe usar:

```sql
CASE WHEN user_agr = '6' THEN 'erp_push' ELSE 'humano' END
```

### R-08: Pedidos atascados son histórico no purgado (NUEVA, SQL)

Los 180 pedidos en estados intermedios tienen TODOS mas de 90 dias. No reflejan flujo activo — son deuda operativa de limpieza. **El bridge debe excluir estos pedidos del comparativo** (ruido historico).

### R-09: La maquina se respeta siempre, aunque sea instantaneamente (NUEVA, SQL)

Aunque algunos pedidos atraviesan toda la maquina en milisegundos (caso WMS-inyectado), TODOS pasan por todos los estados con `IdPickingEnc` real. **reserva-webapi debe modelar los 6 estados completos** aunque algunos casos los recorran sin paso humano.

## Columnas de auditoria en `trans_pe_enc`

- `Fecha_Pedido` (datetime) — **CONTAMINADA**: hereda fecha del documento NAV/SAP, no es la fecha real WMS
- `fec_agr` (datetime) — **CANONICA**: cuando WMS creo la fila
- `fec_mod` (datetime) — ultima modificacion en WMS
- `hora_ini` / `hora_fin` (datetime) — **CONTAMINADAS** para PDV_NAV: heredan timestamps NAV
- `user_agr` / `user_mod` — quien creo / quien modifico (user_agr=6 = sintetico ERP)
- `IdPickingEnc` — vinculacion al picking. Siempre ≠ 0 para pedidos en estados intermedios y Despachado.

**Para timeline real WMS, usar `fec_agr` y `fec_mod`**, NO `Fecha_Pedido`/`hora_ini`/`hora_fin`.

## Trazabilidad de la informacion

| Aspecto | Fuente | Confianza |
|---|---|---|
| Lista de estados | Query `trans_pe_enc` (snapshot 2026-04-27) | Alta |
| Significado de cada estado | Erik (tarea 1) | Canonica |
| Transiciones permitidas | Erik (tarea 1) | Canonica |
| WMS-inyectado salta Pendiente | Erik (tarea 1) + refinado SQL | Canonica |
| Verificado opcional | Erik (tarea 1) + columna confirmada SQL | Canonica |
| Rollback en Anulado | Erik (tarea 1) | Canonica |
| Columna que activa Verificado | `trans_pe_tipo.Verificar` (SQL tarea 2) | **RESUELTO** |
| Flag pedidos WMS-inyectados | `tipo + user_agr=6` (SQL tarea 2) | **RESUELTO PARCIAL** |
| `NUEVO → Despachado` directo | 15 casos historicos PDV_NAV (SQL tarea 2) | **RESUELTO** |
| Atascos > 90 dias = histórico | SQL tarea 2 | Alta |
| Timeline real usa fec_agr/fec_mod | SQL tarea 2 | Alta |

## Pendientes que sobreviven

- **PEND-01**: ~~RESUELTO~~ → columna es `Verificar`.
- **PEND-02**: ~~PARCIAL~~ → patron por tipo + user_agr, no flag binario.
- **PEND-03**: ~~RESUELTO~~ → 15 casos historicos PDV_NAV (0.4%).
- **PEND-04 (NUEVA)**: ¿hay tambien una "verificacion liviana" que NO pase por `trans_packing_enc` (que solo tiene 13 filas)? Tal vez se registra en `trans_picking_ubic_stock` mediante `IdOperadorBodega_Verifico` y `cantidad_verificada`. Pendiente confirmar con Erik.
- **PEND-05 (NUEVA)**: ¿el caso `NUEVO → Despachado` instantaneo (15 historicos) se dispara por algun campo especifico del payload o por timing del job?
