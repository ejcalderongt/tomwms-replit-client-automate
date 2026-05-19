# Catalogo: estados y tipos de `tarea_hh` (y `sis_tipo_tarea`)

> Origenes:
> - Estados (`IdEstado` 1..4): handoff inverso
>   `codex-learning-2026-05-20-mi3-di-estatus-endpoint` (Codex 2026-05-20).
> - Tipos `IdTipoTarea = 1, 8, 11`: handoff inverso
>   `2026-05-19-codex-learning-bof-veri-movimientos-duplicados` (Codex 2026-05-19).

## `tarea_hh.IdEstado`

| Id | Nombre | Significado operativo |
|---:|---|---|
| 1 | Nuevo | Recien creada, aun no asignada/tomada. |
| 2 | Pendiente | En curso o asignada, pendiente de finalizar. |
| 3 | Anulado | Cancelada. NO cuenta como completada. |
| 4 | Finalizado | Completada exitosamente. |

Helpers recomendados:

```csharp
public static bool EsFinalizada(int idEstado) => idEstado == 4;
public static bool EsCancelada(int idEstado)  => idEstado == 3;
public static bool EsActiva(int idEstado)     => idEstado == 1 || idEstado == 2;
```

```sql
-- Tareas activas
WHERE IdEstado IN (1, 2)

-- Tareas cerradas (finalizadas o anuladas)
WHERE IdEstado IN (3, 4)

-- Solo finalizadas exitosamente
WHERE IdEstado = 4
```

## `sis_tipo_tarea.IdTipoTarea` (catalogo en construccion)

| Id | Codigo | Descripcion | Link a transaccion | Donde aparece |
|---:|---|---|---|---|
| 1 | RECEP | Recepcion HH | `tarea_hh.IdTransaccion = trans_re_oc.IdRecepcionEnc` | `tarea_hh` (cola HH) |
| 8 | PIK | Picking HH | `tarea_hh.IdTransaccion = trans_picking_enc.IdPickingEnc` | `tarea_hh` (cola HH) |
| 11 | VERI | Verificacion (movimiento de stock) | `trans_movimientos.IdTransaccion = trans_picking_enc.IdPickingEnc` | `trans_movimientos` (NO tarea_hh para VERI segun caso 1628) |
| _resto_ | _por confirmar_ | Picking ubicacion, ajuste, transferencia, inventario ciclico, etc. | _por confirmar_ | _por confirmar_ |

Notas:

- **VERI no genera tarea_hh** (al menos en el caso `IdPickingEnc = 1628`). Es
  un tipo que aparece en `trans_movimientos` pero NO en la cola HH.
- **PIK (8)** es la tarea HH del picking; la verificacion VERI (11) es la
  contraparte de movimiento de stock, no una tarea HH separada.

## Joins canonicos por tipo de tarea (confirmados)

```
IdTipoTarea = 1  (RECEP en tarea_hh)
   tarea_hh.IdTransaccion → trans_re_oc.IdRecepcionEnc
   → trans_re_enc / trans_re_det

IdTipoTarea = 8  (PIK en tarea_hh)
   tarea_hh.IdTransaccion → trans_picking_enc.IdPickingEnc
   → trans_picking_ubic

IdTipoTarea = 11 (VERI en trans_movimientos, NO tarea_hh)
   trans_movimientos.IdTransaccion → trans_picking_enc.IdPickingEnc
   trans_movimientos.IdPedidoEnc / IdPedidoDet → trans_pe_enc / trans_pe_det
   trans_movimientos.IdRecepcion / IdRecepcionDet → trans_re_enc / trans_re_det
```

## Pendientes de confirmar

- [ ] Catalogo completo de `sis_tipo_tarea.IdTipoTarea` (faltan: picking
      ubicacion, ajuste, transferencia, inventario ciclico, devolucion,
      mermas, etc.).
- [ ] Quien define el catalogo: tabla `sis_tipo_tarea` directa? `SELECT
      DISTINCT IdTipoTarea, Codigo, Descripcion FROM sis_tipo_tarea ORDER BY
      IdTipoTarea` deberia darlo. Pendiente correr.
- [ ] Confirmar si existen tipos de tarea HH adicionales (mas alla de 1 y 8).

## Cross-cliente

Este catalogo es **del core**, no especifico a cliente. Los IDs 1-4 (estados)
son estables en todas las BDs auditadas (snapshot 2026-05-05). Para los tipos
1, 8, 11, confirmados en KILLIOS_PRD; cross-cliente pendiente.

## Referencias

- Pattern OC MI3 (usa `IdTipoTarea = 1`): `code-changes/BOF/PATTERNS-OC-MI3.md`
- Pattern VERI picking (usa `IdTipoTarea = 11`):
  `code-changes/BOF/PATTERNS-PICKING-VERI.md`
- Handoffs origen:
  - `handoffs/codex-learning-2026-05-20-mi3-di-estatus-endpoint/PROPOSAL.md`
  - `handoffs/2026-05-19-codex-learning-bof-veri-movimientos-duplicados/PROPOSAL.md`
