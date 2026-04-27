# Maquina de estados de pedido — observada en trans_pe_enc.estado

> Snapshot 2026-04-27. Killios PRD, 4,202 pedidos totales en `trans_pe_enc`.

## Estados observados (count desc)

| Estado | Conteo | % |
|---|---:|---:|
| `Despachado` | 3,989 | 95.0% |
| `Pickeado` | 86 | 2.0% |
| `Pendiente` | 73 | 1.7% |
| `Anulado` | 33 | 0.8% |
| `NUEVO` | 14 | 0.3% |
| `Verificado` | 7 | 0.2% |

## Maquina inferida

```
                  +------------+
                  |   NUEVO    |  (14)  ← entra desde ERP / creacion manual
                  +------+-----+
                         |
                         v
                  +------------+
                  | Pendiente  |  (73)  ← reservado pero sin picker asignado
                  +------+-----+
                         |
                         v
                  +------------+
                  |  Pickeado  |  (86)  ← terminado en HH, pendiente verificacion
                  +------+-----+
                         |
                         v   (¿opcional?)
                  +------------+
                  | Verificado |  (7)   ← muy raro — solo algunos clientes/tipos?
                  +------+-----+
                         |
                         v
                  +------------+
                  | Despachado |  (3989) ← cerrado, push al ERP
                  +------------+

                  Anulado (33)         ← puede salir desde cualquier estado anterior
```

## Hipotesis y brechas

1. **Verificacion opcional**: solo 7 de 4202 pedidos pasaron por `Verificado` explicito. La gran mayoria salta de `Pickeado` directo a `Despachado`. La tabla `trans_packing_enc` tiene solo 13 filas → la verificacion en packing es minoritaria. Posibles razones:
   - Solo algunos `trans_pe_tipo` requieren verificacion.
   - El estado `Verificado` puede ser temporal (se sobreescribe rapido a `Despachado`).
   - Verificacion ad-hoc por excepcion, no sistematica.

2. **`Pickeado` > `Pendiente`**: 86 vs 73 sugiere que pedidos antiguos se quedaron en estado intermedio (no finalizaron a `Despachado`). ¿Son outliers a investigar o es normal?

3. **`Despachado` 3989 vs `trans_despacho_enc` 4032**: diferencia de 43. Posibles explicaciones:
   - Despachos sin pedido (manuales, ej. ajuste de salida).
   - Pedidos despachados pero sin update de `estado` (bug del legacy).
   - Diferencia de scope (algunos `trans_despacho_enc` no son outbound).

4. **Anulados (33)**: ¿desde qué estados se permite anular? ¿hay rollback de reservas (`stock_res`) cuando se anula?

## Eventos posibles (transiciones a confirmar con Erik)

| Evento | De → A | Tabla afectada |
|---|---|---|
| Crear pedido | (none) → NUEVO | `trans_pe_enc` insert |
| Reservar | NUEVO → Pendiente | `stock_res` insert + `trans_pe_det_log_reserva` |
| Iniciar picking | Pendiente → ? | `trans_picking_enc/op` insert |
| Terminar picking | ? → Pickeado | update `trans_pe_enc.estado` + `hora_fin` |
| Verificar | Pickeado → Verificado | `trans_packing_enc` insert |
| Despachar | Pickeado/Verificado → Despachado | `trans_despacho_enc/det` + `i_nav_transacciones_out` |
| Anular | * → Anulado | rollback `stock_res`? |

## Columnas de auditoria en trans_pe_enc

- `Fecha_Pedido` (datetime) — cuando se creo
- `hora_ini` (datetime) — inicio de operacion (¿picking?)
- `hora_fin` (datetime) — fin
- `fecha_preparacion` (date) — fecha preparacion
- `RoadFechaEntr` (datetime) — fecha entrega
- `HoraEntregaDesde` / `HoraEntregaHasta` — ventana de entrega
- `user_agr` / `user_mod` — quien creo / quien modifico

Estos campos permiten reconstruir el ciclo de vida temporal sin necesidad de estado intermedio en logs separados.

## Pendiente

Una vez Erik responda preguntas P-08, P-09, P-15 del documento de preguntas, se actualiza este state-machine con transiciones confirmadas y SPs/eventos disparadores reales.
