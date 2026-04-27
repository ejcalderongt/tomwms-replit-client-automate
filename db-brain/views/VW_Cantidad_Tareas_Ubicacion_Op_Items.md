---
id: db-brain-view-vw-cantidad-tareas-ubicacion-op-items
type: db-view
title: dbo.VW_Cantidad_Tareas_Ubicacion_Op_Items
schema: dbo
name: VW_Cantidad_Tareas_Ubicacion_Op_Items
kind: view
modify_date: 2021-12-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Cantidad_Tareas_Ubicacion_Op_Items`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-12-29 |
| Columnas | 5 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareaUbicacionEnc` | `int` |  |  |
| 2 | `cambio_estado` | `bit` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `IdOperadorBodega` | `int` | ✓ |  |
| 5 | `IdOperadorBodegaOp` | `int` | ✓ |  |

## Consume

- `trans_ubic_hh_det`
- `trans_ubic_hh_enc`
- `trans_ubic_hh_op`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view [dbo].[VW_Cantidad_Tareas_Ubicacion_Op_Items] as
select hh_enc.IdTareaUbicacionEnc,hh_enc.cambio_estado,hh_det.IdBodega,hh_det.IdOperadorBodega,hh_op.IdOperadorBodega as IdOperadorBodegaOp
from trans_ubic_hh_enc hh_enc
INNER JOIN trans_ubic_hh_det hh_det on hh_enc.IdTareaUbicacionEnc=hh_det.IdTareaUbicacionEnc
LEFT JOIN trans_ubic_hh_op hh_op on hh_enc.IdTareaUbicacionEnc = hh_op.IdTareaUbicacionEnc
where (hh_enc.estado='NUEVO' or hh_enc.estado='PENDIENTE') AND hh_enc.activo=1 and hh_det.Realizado=0
```
