---
id: db-brain-view-v-motivo-anulacion
type: db-view
title: dbo.v_motivo_anulacion
schema: dbo
name: v_motivo_anulacion
kind: view
modify_date: 2015-11-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.v_motivo_anulacion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2015-11-24 |
| Columnas | 6 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `IdMotivoAnulacion` | `int` | ✓ |  |
| 4 | `MotivoAnulacion` | `nvarchar(50)` | ✓ |  |
| 5 | `IdMotivoAnulacionBodega` | `int` |  |  |
| 6 | `Activo` | `bit` | ✓ |  |

## Consume

- `bodega`
- `motivo_anulacion`
- `motivo_anulacion_bodega`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.v_motivo_anulacion
AS
SELECT        dbo.motivo_anulacion_bodega.IdBodega, dbo.bodega.Nombre AS Bodega, dbo.motivo_anulacion_bodega.IdMotivoAnulacion, 
                         dbo.motivo_anulacion.Nombre AS MotivoAnulacion, dbo.motivo_anulacion_bodega.IdMotivoAnulacionBodega, dbo.motivo_anulacion_bodega.activo AS Activo
FROM            dbo.motivo_anulacion INNER JOIN
                         dbo.motivo_anulacion_bodega ON dbo.motivo_anulacion.IdMotivoAnulacion = dbo.motivo_anulacion_bodega.IdMotivoAnulacion INNER JOIN
                         dbo.bodega ON dbo.motivo_anulacion_bodega.IdBodega = dbo.bodega.IdBodega
```
