---
id: db-brain-view-vw-motivoanulacionbodega
type: db-view
title: dbo.VW_MotivoAnulacionBodega
schema: dbo
name: VW_MotivoAnulacionBodega
kind: view
modify_date: 2017-06-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_MotivoAnulacionBodega`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-06-02 |
| Columnas | 11 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdMotivoAnulacionBodega` | `int` |  |  |
| 3 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 4 | `IdMotivoAnulacion` | `int` |  |  |
| 5 | `IdEmpresa` | `int` |  |  |
| 6 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 7 | `activo` | `bit` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |

## Consume

- `empresa`
- `motivo_anulacion`
- `motivo_anulacion_bodega`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_MotivoAnulacionBodega
AS
SELECT     dbo.motivo_anulacion_bodega.IdBodega, dbo.motivo_anulacion_bodega.IdMotivoAnulacionBodega, e.nombre AS Empresa, d.IdMotivoAnulacion, d.IdEmpresa, 
                      d.Nombre, d.activo, d.user_agr, d.fec_agr, d.user_mod, d.fec_mod
FROM         dbo.motivo_anulacion AS d INNER JOIN
                      dbo.motivo_anulacion_bodega ON d.IdMotivoAnulacion = dbo.motivo_anulacion_bodega.IdMotivoAnulacion LEFT OUTER JOIN
                      dbo.empresa AS e ON d.IdEmpresa = e.IdEmpresa
```
