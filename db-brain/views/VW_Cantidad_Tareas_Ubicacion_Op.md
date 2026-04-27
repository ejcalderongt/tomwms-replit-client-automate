---
id: db-brain-view-vw-cantidad-tareas-ubicacion-op
type: db-view
title: dbo.VW_Cantidad_Tareas_Ubicacion_Op
schema: dbo
name: VW_Cantidad_Tareas_Ubicacion_Op
kind: view
modify_date: 2021-12-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Cantidad_Tareas_Ubicacion_Op`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-12-29 |
| Columnas | 5 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareaUbicacionEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdOperadorBodega` | `int` | ✓ |  |
| 4 | `IdOperadorBodegaOp` | `int` | ✓ |  |
| 5 | `cambio_estado` | `bit` | ✓ |  |

## Consume

- `trans_ubic_hh_det`
- `trans_ubic_hh_enc`
- `trans_ubic_hh_op`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view VW_Cantidad_Tareas_Ubicacion_Op as

SELECT hh_enc.IdTareaUbicacionEnc, hh_det.IdBodega, hh_det.IdOperadorBodega, hh_op.IdOperadorBodega AS IdOperadorBodegaOp, hh_enc.cambio_estado
FROM     dbo.trans_ubic_hh_enc AS hh_enc INNER JOIN
                  dbo.trans_ubic_hh_det AS hh_det ON hh_enc.IdTareaUbicacionEnc = hh_det.IdTareaUbicacionEnc LEFT OUTER JOIN
                  dbo.trans_ubic_hh_op AS hh_op ON hh_enc.IdTareaUbicacionEnc = hh_op.IdTareaUbicacionEnc
WHERE  (hh_enc.estado = 'NUEVO' OR
                  hh_enc.estado = 'PENDIENTE') AND (hh_enc.activo = 1) AND (hh_det.Realizado = 0)
```
