---
id: db-brain-view-vw-pais-departamento
type: db-view
title: dbo.VW_Pais_Departamento
schema: dbo
name: VW_Pais_Departamento
kind: view
modify_date: 2016-05-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Pais_Departamento`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-05-27 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPais` | `int` |  |  |
| 2 | `NombrePais` | `nvarchar(50)` | ✓ |  |
| 3 | `IdDepartamento` | `int` |  |  |
| 4 | `NombreDepartamento` | `varchar(25)` | ✓ |  |

## Consume

- `pais_departamento`
- `paises`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Pais_Departamento
AS
SELECT        dbo.paises.IdPais, dbo.paises.NOMBRE AS NombrePais, dbo.pais_departamento.IdDepartamento, dbo.pais_departamento.Nombre AS NombreDepartamento
FROM            dbo.paises INNER JOIN
                         dbo.pais_departamento ON dbo.paises.IdPais = dbo.pais_departamento.IdPais
```
