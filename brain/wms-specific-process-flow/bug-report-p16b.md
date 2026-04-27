# BUG-001: Pedidos despachados que vuelven a estado Pendiente/Pickeado/Verificado

> **Investigacion**: pasada 9b (autonoma SQL READ-ONLY).
> **Reportado por**: usuarios de produccion via Erik (no reproducible en dev).
> **Confirmado en datos**: SI — 116 pedidos afectados en Killios PRD.
> **Severidad**: ALTA — afecta integridad operativa (pedidos cobrados como
> abiertos cuando ya entregaron mercancia).
> **Periodo observado**: junio-agosto 2025.

## TL;DR

El bug **existe en datos productivos** y no es un caso aislado: 116 pedidos
distintos en Killios tienen lineas activas en `trans_despacho_det` (es
decir, su mercancia salio fisicamente de la bodega) pero su
`trans_pe_enc.estado` NO es `Despachado`. La distribucion:

| Estado actual del pedido | Cant pedidos | Lineas despachadas totales |
|---|---:|---:|
| Pickeado | 71 | ~600 |
| Pendiente | 39 | ~280 |
| Verificado | 6 | ~50 |
| **TOTAL** | **116** | **~929 lineas** |

## Hipotesis del mecanismo (basada en cronologia real)

### Patron 1: race condition en cierre de despacho (~12 min, mayoria de los Pickeado)

**Caso ejemplo: pedido 4152 (Pendiente con 4 lineas despachadas)**

```
2025-08-18 09:06:00 — pedido 4152 creado (PDV_NAV)
2025-08-18 09:59:52 — user 13 (humano, BackOffice) ejecuta "Se eliminó el picking"
2025-08-18 16:45:16 — despacho_det creado (4 lineas)
2025-08-18 16:45:18 — despacho_enc creado, estado=Finalizado
2025-08-18 16:45:31 — log: "Transferencia generada correctamente DocNum 5653-Garesa"
2025-08-18 16:45:32 — log: "Se creó la entrega de mercancía DocNum 3255 en SAP" (user_agr=6)
2025-08-18 16:45:32 — fec_mod del pedido (user_mod=0)

ESTADO ACTUAL: "Pendiente"
PERO: 4 lineas en despacho_det, despacho_enc=Finalizado, transferencia y entrega SAP exitosas
```

El pedido **fue despachado completamente** (despacho fisico + transferencia
WMS interna + entrega de mercancia SAP), pero **el estado quedó en Pendiente**.

### Patron 2: doble despacho parcial (caso 2328)

```
Pedido 2328 (estado actual: Pendiente)
├─ DespachoEnc 2319 (2025-07-17 14:55) — 2 lineas, cant 80 c/u, estado=Finalizado
└─ DespachoEnc 2485 (2025-07-21 12:54, 4 dias despues) — 3 lineas, cant 55+35+...

El pedido se despacho en 2 viajes diferentes con 4 dias de separacion.
Despues del primer viaje el sistema marco el pedido como "Pendiente"
(esperando completar). Tras el segundo viaje, NADIE actualizo el estado a
"Despachado".
```

### Patron 3: override del job ERP (~23 horas, Pendientes)

Los Pendientes tienen `avg_min_despues_despacho = 1376 min` (~23 horas) y
`max = 23,155 min` (16 dias). El patron sugiere que un job de poleo
NAV/SAP recorre periodicamente los pedidos y, cuando encuentra un cambio
upstream en SAP, **sobrescribe `trans_pe_enc.estado`** sin verificar
si WMS ya tiene el pedido en estado terminal.

## Disparador identificado

El log `log_error_wms` (mal nombrado — guarda eventos exitosos tambien)
captura la secuencia exacta:

```
1. "Transferencia generada correctamente. DocNum: <X>-Cliente"  ← user_agr=6 (job)
2. "Se creó la entrega de mercancía para el pedido de cliente: <Y> con DocNum: <Z> en SAP. Para el IdPedidoEnc WMS: <ped> - Cliente"  ← user_agr=6 (job)
```

**Ambos pasos los ejecuta el `user_agr=6` (job sintetico SAP)**. Entre el
paso 1 y el paso 2 hay una ventana de ~1-13 segundos. Si el WMS marco
el pedido como Despachado entre paso 1 y paso 2, el paso 2 **lo regresa**.

## Distribucion por usuario que modifico ultimo

```
user_mod=0  → 692 lineas afectadas (60%)  — proceso de sistema (default)
user_mod=12 → 111 lineas (12%)            — humano BackOffice
user_mod=6  → 85 lineas (9%)              — job sintetico SAP
user_mod=1  → 35 lineas (4%)              — humano BackOffice
user_mod=11 → 6 lineas (0.6%)             — humano BackOffice
```

El **`user_mod=0`** es el principal sospechoso. Probablemente un SP/trigger
que no setea `user_mod` antes de hacer UPDATE.

## Distribucion temporal

| Anio-Mes | Pedidos buggeados (lineas) |
|---|---:|
| 2025-06 | 267 |
| 2025-07 | 486 |
| 2025-08 | 176 |

Es un bug **continuo y constante**, no un evento aislado. **Julio fue el
mes peor** (486 lineas). Agosto bajo, posiblemente porque la BD se
detuvo de actualizar (snapshot del 19/ago).

## Casos concretos para Erik (reproduccion)

Los siguientes 5 pedidos son los mas representativos para reproducir el bug
en un entorno controlado (todos tienen el patron clasico):

| IdPedidoEnc | Estado actual | Lineas despach. | fec_agr pedido | fec_mod pedido | Min mod despues despacho |
|---:|---|---:|---|---|---:|
| **4156** | Pendiente | 34 | 2025-08-18 10:14 | 2025-08-18 13:37 | +1 |
| **4155** | Pendiente | 32 | 2025-08-18 10:06 | 2025-08-18 13:38 | 0 |
| **4152** | Pendiente | 4 | 2025-08-18 09:06 | 2025-08-18 16:45 | 0 |
| **3890** | Verificado | 3 | 2025-08-12 10:38 | 2025-08-13 11:21 | +1397 (23h) |
| **2328** | Pendiente | 5 | 2025-07-17 14:55 | 2025-07-21 12:54 | doble viaje |

## Recomendacion para Erik

1. **Auditar el SP que hace UPDATE en `trans_pe_enc.estado`**. No hay triggers
   en la tabla (verificado), asi que el bug esta en logica de aplicacion
   (BackOffice o job ERP).

2. **Buscar SP que haga `UPDATE trans_pe_enc SET estado = ...` sin chequear
   estado actual ≠ 'Despachado'**. Probablemente hay un SP que se llama:
   - Cuando llega push de SAP (ej. `sp_actualizar_pedido_desde_sap`).
   - Cuando se procesa la transferencia post-despacho.

3. **Bloquear con `WHERE estado <> 'Despachado'` en cualquier UPDATE de estado**
   que no sea el mismo despacho confirmando.

4. **Implementar maquina de estados estricta** que solo permita las
   transiciones del state-machine v3 (R-01 a R-09).

## Impacto potencial

- **Operativo**: usuarios reportan que pedidos "cerrados" reaparecen como
  pendientes. Si despacho ya facturo, hay desincronizacion contable.
- **Reabastecimiento**: si el bug afecta visibilidad de stock comprometido
  (`stock_res`), podria estar reservando dos veces.
- **Reporting**: dashboards que cuentan pedidos por estado dan numeros
  inflados de Pendiente/Pickeado.

## Evidencia raw

- `/tmp/dbq/out/12-bug-hunt.json` — 9 hipotesis (H1..H7), 116 pedidos detallados
- `/tmp/dbq/out/13-cronologia-bug.json` — cronologia de casos puntuales
- Tabla `log_error_wms` — registra ambos pasos (Transferencia + Entrega SAP)
  con timestamps que revelan la ventana de race condition.

## Tablas de auditoria disponibles para investigar mas a fondo

| Tabla | Cols | Para que sirve |
|---|---:|---|
| `Auditoria` | 9 | Auditoria generica (revisar contenido) |
| `trans_log_pedido_liberacion` | 23 | Log de liberacion de pedidos (NO captura cambios de estado del 4156) |
| `trans_log_reubic_stock_res` | 25 | Log de reubicacion de reservas de stock |
| `trans_pe_det_log_reserva` | 17 | Log de reservas a nivel detalle |
| `stock_hist` | 33 | Historial de movimientos de stock |
| `log_error_wms` | 15 | Eventos de integracion ERP (mal nombrado, no son solo errores) |

## Pendiente para confirmar con Erik

1. ¿Que SP es el responsable de actualizar `trans_pe_enc.estado` cuando llega
   confirmacion de SAP? (Probable: alguno con nombre `*_actualizar_*` o
   `*_pedido_*` que toque la columna estado).
2. ¿Hay alguna proteccion (a nivel aplicacion BackOffice .NET) contra
   actualizaciones de estado retroactivas? Si la hay, el job sintetico
   probablemente la bypasea.
3. ¿Es posible que el bug venga de la **eliminacion del picking** (caso 4152
   tuvo "Se eliminó el picking" pre-despacho)? ¿Que SP es responsable de
   eliminar picking sin validar estado del pedido?
