---
id: db-brain-view-vw-clientetipo
type: db-view
title: dbo.VW_ClienteTipo
schema: dbo
name: VW_ClienteTipo
kind: view
modify_date: 2017-08-30
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ClienteTipo`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-08-30 |
| Columnas | 9 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Propietario` | `nvarchar(100)` |  |  |
| 2 | `IdTipoCliente` | `int` |  |  |
| 3 | `IdPropietario` | `int` |  |  |
| 4 | `NombreTipoCliente` | `nvarchar(50)` | ✓ |  |
| 5 | `Activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |

## Consume

- `cliente_tipo`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_ClienteTipo
AS
SELECT        p.nombre_comercial AS Propietario, ct.IdTipoCliente, ct.IdPropietario, ct.NombreTipoCliente, ct.Activo, ct.user_agr, ct.fec_agr, ct.user_mod, ct.fec_mod
FROM            dbo.cliente_tipo AS ct INNER JOIN
                         dbo.propietarios AS p ON ct.IdPropietario = p.IdPropietario
```
