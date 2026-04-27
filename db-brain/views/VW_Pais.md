---
id: db-brain-view-vw-pais
type: db-view
title: dbo.VW_Pais
schema: dbo
name: VW_Pais
kind: view
modify_date: 2016-05-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Pais`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-05-27 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPais` | `int` |  |  |
| 2 | `NombrePais` | `nvarchar(50)` | ✓ |  |
| 3 | `IdRegion` | `int` | ✓ |  |
| 4 | `NombreRegion` | `varchar(25)` | ✓ |  |
| 5 | `IdDepartamento` | `int` | ✓ |  |
| 6 | `NombreDepartamento` | `varchar(25)` | ✓ |  |
| 7 | `IdMunicipio` | `int` | ✓ |  |
| 8 | `NombreMunicipio` | `varchar(255)` | ✓ |  |

## Consume

- `pais_departamento`
- `pais_municipio`
- `pais_region`
- `paises`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Pais
AS
SELECT        dbo.paises.IdPais, dbo.paises.NOMBRE AS NombrePais, dbo.pais_region.IdRegion, dbo.pais_region.Nombre AS NombreRegion, 
                         dbo.pais_municipio.IdDepartamento, dbo.pais_departamento.Nombre AS NombreDepartamento, dbo.pais_municipio.IdMunicipio, 
                         dbo.pais_municipio.Nombre AS NombreMunicipio
FROM            dbo.pais_departamento LEFT OUTER JOIN
                         dbo.pais_municipio ON dbo.pais_departamento.IdDepartamento = dbo.pais_municipio.IdDepartamento RIGHT OUTER JOIN
                         dbo.paises ON dbo.pais_departamento.IdPais = dbo.paises.IdPais LEFT OUTER JOIN
                         dbo.pais_region ON dbo.paises.IdPais = dbo.pais_region.IdPais
```
