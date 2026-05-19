# Catalogo: estados y tipos de `tarea_hh`

> Origen: handoff inverso `codex-learning-2026-05-20-mi3-di-estatus-endpoint`.
> Confirmado por Codex local 2026-05-20.

## `tarea_hh.IdEstado`

| Id | Nombre | Significado operativo |
|---:|---|---|
| 1 | Nuevo | Recien creada, aun no asignada/tomada. |
| 2 | Pendiente | En curso o asignada, pendiente de finalizar. |
| 3 | Anulado | Cancelada. NO cuenta como completada. |
| 4 | Finalizado | Completada exitosamente. |

Helper recomendado:

```csharp
public static bool EsFinalizada(int idEstado) => idEstado == 4;
public static bool EsCancelada(int idEstado) => idEstado == 3;
public static bool EsActiva(int idEstado) => idEstado == 1 || idEstado == 2;
```

En SQL:

```sql
-- Tareas activas
WHERE IdEstado IN (1, 2)

-- Tareas cerradas (finalizadas o anuladas)
WHERE IdEstado IN (3, 4)

-- Solo finalizadas exitosamente
WHERE IdEstado = 4
```

## `tarea_hh.IdTipoTarea`

Catalogo parcial confirmado:

| Id | Descripcion | Link a transaccion |
|---:|---|---|
| 1 | Recepcion HH | `tarea_hh.IdTransaccion = trans_re_oc.IdRecepcionEnc` |
| _resto_ | Por documentar | Aparecera segun nuevos endpoints |

> **Pendiente**: confirmar con Erik el catalogo completo de `IdTipoTarea`
> (probablemente incluya picking, ubicacion, ajuste, transferencia,
> inventario ciclico, etc.). Cuando se confirme, actualizar este archivo y
> agregar al ATLAS.

## Joins canonicos por tipo de tarea

```
IdTipoTarea = 1 (Recepcion)
   tarea_hh.IdTransaccion → trans_re_oc.IdRecepcionEnc
   → trans_re_enc / trans_re_det

IdTipoTarea = ? (Picking)        — por confirmar
IdTipoTarea = ? (Ajuste)         — por confirmar
IdTipoTarea = ? (Transferencia)  — por confirmar
```

## Cross-cliente

Este catalogo es **del core**, no especifico a cliente. Los IDs 1-4 son
estables en todas las BDs auditadas (snapshot 2026-05-05). Si aparece otro
ID en una BD cliente, registrarlo aca + abrir handoff de revision.

## Referencias

- Pattern OC MI3 (uso): `code-changes/BOF/PATTERNS-OC-MI3.md`
- Origen confirmado: handoff inverso
  `handoffs/codex-learning-2026-05-20-mi3-di-estatus-endpoint/PROPOSAL.md`
