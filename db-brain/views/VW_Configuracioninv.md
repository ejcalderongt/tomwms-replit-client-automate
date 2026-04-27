---
id: db-brain-view-vw-configuracioninv
type: db-view
title: dbo.VW_Configuracioninv
schema: dbo
name: VW_Configuracioninv
kind: view
modify_date: 2017-09-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Configuracioninv`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-09-21 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Correlativo` | `int` |  |  |
| 2 | `nombre` | `varchar(256)` | ✓ |  |
| 3 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 4 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 5 | `idempresa` | `int` |  |  |
| 6 | `idbodega` | `int` |  |  |
| 7 | `Propietario` | `nvarchar(100)` |  |  |
| 8 | `idPropietario` | `int` | ✓ |  |

## Consume

- `bodega`
- `empresa`
- `i_nav_config_enc`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Configuracioninv
AS
SELECT        dbo.i_nav_config_enc.idnavconfigenc AS Correlativo, dbo.i_nav_config_enc.nombre, dbo.empresa.nombre AS Empresa, dbo.bodega.nombre AS Bodega, dbo.i_nav_config_enc.idempresa, 
                         dbo.i_nav_config_enc.idbodega, dbo.propietarios.nombre_comercial AS Propietario, dbo.i_nav_config_enc.idPropietario
FROM            dbo.bodega INNER JOIN
                         dbo.empresa ON dbo.bodega.IdEmpresa = dbo.empresa.IdEmpresa INNER JOIN
                         dbo.i_nav_config_enc ON dbo.bodega.IdBodega = dbo.i_nav_config_enc.idbodega AND dbo.empresa.IdEmpresa = dbo.i_nav_config_enc.idempresa INNER JOIN
                         dbo.propietarios ON dbo.empresa.IdEmpresa = dbo.propietarios.IdEmpresa AND dbo.i_nav_config_enc.idPropietario = dbo.propietarios.IdPropietario
```
