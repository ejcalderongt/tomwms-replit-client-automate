---
id: db-brain-view-vw-bodega
type: db-view
title: dbo.VW_Bodega
schema: dbo
name: VW_Bodega
kind: view
modify_date: 2021-10-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Bodega`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-10-28 |
| Columnas | 9 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Correlativo` | `int` |  |  |
| 2 | `Pais` | `nvarchar(50)` | ✓ |  |
| 3 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 4 | `Código` | `nvarchar(50)` | ✓ |  |
| 5 | `nombre` | `nvarchar(50)` | ✓ |  |
| 6 | `NombreComercial` | `nvarchar(50)` | ✓ |  |
| 7 | `Responsable` | `nvarchar(50)` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |
| 9 | `IdEmpresa` | `int` |  |  |

## Consume

- `bodega`
- `empresa`
- `paises`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Bodega]
AS
SELECT        b.IdBodega AS Correlativo, p.NOMBRE AS Pais, e.nombre AS Empresa, b.codigo AS Código, b.nombre, b.nombre_comercial AS NombreComercial, b.encargado AS Responsable, b.activo, b.IdEmpresa
FROM            dbo.bodega AS b INNER JOIN
                         dbo.empresa AS e ON b.IdEmpresa = e.IdEmpresa LEFT OUTER JOIN
                         dbo.paises AS p ON b.IdPais = p.IdPais
```
