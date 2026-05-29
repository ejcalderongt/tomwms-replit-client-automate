---
slug: codex-learning-2026-05-20-mi3-di-estatus-endpoint
agente: codex-local
fecha: 2026-05-20
tipo: handoff-inverso (Codex propone al brain)
estado: promovido por Replit
---

# Propuesta de aprendizaje desde Codex local

Origen: implementacion (propuesta) del endpoint
`GET /api/sync/ingresos/mi3/di-estatus?referencia=<referencia_oc>` en
TOMWMS_BOF (.NET Core, WMSWebAPI).

## 1. Endpoint propuesto

```
GET /api/sync/ingresos/mi3/di-estatus?referencia=<referencia_oc>
```

Responde con estatus consolidado de una Orden de Compra MI3, agrupado por
producto/linea de OC.

## 2. Regla de negocio descubierta

El estatus de una OC MI3 debe resolverse por **`trans_oc_enc.Referencia`**,
NO por `IdOrdenCompraEnc` expuesto al cliente externo (MI3).

Razon: el ID interno es opaco para MI3; la `Referencia` es el identificador
que ambos sistemas comparten.

## 3. Modelo de datos (cadena de joins)

```
trans_oc_enc                                    (cabecera OC)
   │ IdOrdenCompraEnc
   ▼
trans_re_oc                                     (link OC ↔ recepciones)
   │ IdRecepcionEnc
   ▼
tarea_hh                                        (cola de trabajo HH)
   con tarea_hh.IdTransaccion = trans_re_oc.IdRecepcionEnc
   y   tarea_hh.IdTipoTarea = 1   (recepcion HH)
```

Para obtener el estatus completo de la OC: ir de `trans_oc_enc.Referencia`
→ filas en `trans_oc_det` (lineas de OC) → recepciones asociadas via
`trans_re_oc` → tareas HH via `tarea_hh.IdTransaccion`.

## 4. Forma de respuesta esperada

JSON estructurado y resumido por producto/linea de OC. Por cada linea:

```json
{
  "IdProducto": ...,
  "CodigoProducto": "...",
  "DescripcionProducto": "...",
  "CantidadSolicitada": ...,
  "CantidadRecibida": ...,
  "Pendiente": ...,
  "RecepcionCompleta": true|false,
  "TareasHH": [
    { "IdTareaHH": ..., "IdEstado": 4, "EstadoNombre": "Finalizado", "Finalizada": true },
    ...
  ]
}
```

El controller envuelve en Forma A: `{ "data": [ ...lineas ], "error": null }`.

## 5. Catalogo `tarea_hh.IdEstado`

| Id | Nombre |
|---:|---|
| 1 | Nuevo |
| 2 | Pendiente |
| 3 | Anulado |
| 4 | Finalizado |

`Finalizada = (IdEstado == 4)`.

## 6. Decision arquitectonica de capas (WMSWebAPI .NET Core)

| Capa | Responsabilidad |
|---|---|
| `DALCore` | Queries SQL puntuales y parametrizadas. |
| `EntityCore` | DTOs/POCOs serializables. |
| `Services` | Composicion + reglas de negocio. |
| `Controller` | HTTP + Forma A `{data, error}` + status. |

Una llamada cruza las 4 capas. Controller no toca SQL. Service no sabe de
HTTP.

## 7. Mecanica local: archivos de agente NO viajan

Archivos como `AGENTS.md`, `CLAUDE.md`, `CONVENTIONS.md`, `.cursorrules`,
`codex-context-*.yml` en el clone local de TOMWMS_BOF/TOMHH2025 NO deben
commitearse a `dev_2028_merge`.

Respaldo: branch separado `prograx-local-codex-backup-recovery` del repo de
respaldo.

# Promociones aplicadas por Replit (commit 179c016b2531+1)

- §6 → `wms-brain/brain/code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md` (nuevo)
- §1+2+3+4 → `wms-brain/brain/code-changes/BOF/PATTERNS-OC-MI3.md` (nuevo)
- §5 → `wms-brain/brain/reference/catalogo-tarea-hh-estados.md` (nuevo)
- §7 → `replit.md` §4 regla 7 + `.local/skills/wms-tomwms/conventions.md` §10
- §6 (resumen) → `replit.md` §4 regla 8
- §2 (resumen) → `replit.md` §4 regla 9
- Catalogo estados → `.local/skills/wms-tomwms/conventions.md` §8

# Pendiente

- [ ] Confirmar tipos de tarea HH ademas del `IdTipoTarea = 1`. El catalogo
      completo aun no esta documentado.
- [ ] Confirmar si el endpoint MI3 ya fue mergeado a `dev_2028_merge` o sigue
      en propuesta local.
- [ ] Definir donde vive el repo de respaldo
      (`prograx-local-codex-backup-recovery`): GitHub personal de Erik o
      Azure DevOps separado.
