# Maquina de estados de pedido — confirmada por Erik

> **Version 2** (pasada 9). Transiciones confirmadas por Erik en
> `respuestas-tanda-1.md` (P-08). La version 1 (pasada 8) era hipotesis;
> esta es canonica.
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

## Maquina de estados — confirmada

```
        +-----------+
        |  (insert) |   creacion manual (BackOffice) o
        +-----+-----+   push desde ERP (SAP/NAV)
              |
              | [SI]                   [WMS] solo cuando WMS
              v                          inyecta el pedido
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
              v                                   v   + outbox NAV/SAP OK
        +-----------+                       +-----------+
        |  Anulado  |<----- desde * --------|Despachado |
        |   (33)    |  con rollback         |  (3989)   |
        +-----------+  de stock_res         +-----------+
                       (excepto desde NUEVO    *terminal*
                        sin reserva: raro)

   Transiciones especiales:
   - NUEVO → Pickeado [WMS]: solo WMS-inyectado (traslados entre bodegas
     virtuales en mismo espacio fisico, ej. BOD7 ↔ bodega real Killios)
   - Verificado: estado opcional, configurable por usuario o por tipo de pedido
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
- `SI` = transicion canonica permitida y observada en produccion
- `OPC` = opcional (depende de configuracion)
- `WMS-only` = solo cuando WMS inyecta el pedido (no humano via BackOffice/HH)
- `raro` = posible pero requiere condiciones especificas (stock reservado + orden explicita)
- `NO` = transicion prohibida
- `—` = no aplica

## Reglas confirmadas (P-08 + P-18)

### R-01: Anulado NO es automatico

Erik: *"solo se anula bajo demanda y no solo pasa a estado anulado cuando
tiene stock reservado previamente"*.

Implicancia para reserva-webapi: NO implementar anulacion automatica por
timeout o fallo de reserva. Si reserva falla, dejar el pedido en `NUEVO`
con flag de error y que el humano decida.

### R-02: Pedidos sin lineas se ELIMINAN, no se anulan

Erik: *"hay un mecanismo en la forma de pedido para prevenir que hayan
pedidos sin lineas, entonces generalmente si el pedido no tiene lineas
se elimina"*.

Implicancia: el conteo bajo de `Anulado` (33) refleja solo cancelaciones
deliberadas, no abortos tempranos. Los abortos tempranos no dejan rastro
en `trans_pe_enc`.

### R-03: Rollback de stock_res obligatorio en Anulado

Erik: *"rollback de inventario importante asociado [a Anulado]"*.

Implicancia: el bridge debe verificar que despues de anular, las filas
correspondientes en `stock_res` quedan liberadas. Si encuentra `Anulado`
con `stock_res` activo, es bug.

### R-04: WMS puede inyectar pedidos en cualquier estado

Erik: *"para transacciones que el mismo WMS inyecta puede ser que por
proceso se simule un picking y un despacho [...] WMS podria simular un
pedido, insertarlo en cualquier otro estado"*.

Implicancia: la maquina permite atajo `NUEVO → Pickeado` (e incluso
`NUEVO → Despachado`?) cuando el origen es WMS-inyectado. **Pendiente**
identificar el flag o columna que distingue pedidos WMS-inyectados de
pedidos reales — el bridge necesita saber esto para no marcar como bug
una transicion que es legitima.

### R-05: Verificado es opcional configurable

Erik: *"Verificado es opcional. Puede ser habilitado por el usuario.
Puede estar previamente definido en base al tipo de pedido"*.

Hipotesis: hay una columna en `trans_pe_tipo` que controla si el tipo
exige verificacion (algo como `requiere_verificacion`). Pendiente
identificarla en proxima pasada. Esto explicaria por que hay 7
`Verificado` historicos (los tipos que lo tenian activado).

### R-06: TRAS_WMS asume reserva previa, NO se valida (DEUDA-001)

Ver `respuestas-tanda-1.md` P-18. La bandera `ReservaStock=NO` no se
valida explicitamente; el flujo asume reserva upstream. Riesgo de doble
reserva si alguien crea TRAS_WMS sin reserva previa.

## Columnas de auditoria en trans_pe_enc

- `Fecha_Pedido` (datetime) — cuando se creo
- `hora_ini` (datetime) — inicio de operacion (¿picking?)
- `hora_fin` (datetime) — fin
- `fecha_preparacion` (date) — fecha preparacion
- `RoadFechaEntr` (datetime) — fecha entrega
- `HoraEntregaDesde` / `HoraEntregaHasta` — ventana de entrega
- `user_agr` / `user_mod` — quien creo / quien modifico

Estos campos permiten reconstruir el ciclo de vida temporal sin necesidad
de log separado de transiciones.

## Trazabilidad de la informacion

| Aspecto | Fuente | Confianza |
|---|---|---|
| Lista de estados | Query directa a `trans_pe_enc` (snapshot 2026-04-27) | Alta |
| Significado de cada estado | Erik (P-08) | Canonica |
| Transiciones permitidas | Erik (P-08) | Canonica |
| WMS-inyectado salta Pendiente | Erik (P-08) | Canonica |
| Verificado opcional | Erik (P-08) | Canonica |
| Rollback en Anulado | Erik (P-08) | Canonica |
| Columna que activa Verificado por tipo | Pendiente | Hipotesis |
| Flag que marca pedido WMS-inyectado | Pendiente | Hipotesis |

## Pendientes

- **PEND-01**: identificar la columna en `trans_pe_tipo` (o equivalente) que
  controla si el tipo exige `Verificado`.
- **PEND-02**: identificar el flag o columna que marca pedidos
  WMS-inyectados (los que tienen permitido saltarse `Pendiente`).
- **PEND-03**: confirmar si existe transicion `NUEVO → Despachado` directa
  (mas alla de `NUEVO → Pickeado → Despachado`) cuando WMS inyecta el pedido.
