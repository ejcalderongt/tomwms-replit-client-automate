---
id: db-brain-view-vw-tarimasusadasentransaccion
type: db-view
title: dbo.VW_TarimasUsadasEnTransaccion
schema: dbo
name: VW_TarimasUsadasEnTransaccion
kind: view
modify_date: 2016-07-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_TarimasUsadasEnTransaccion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-07-19 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTarimaTareaUbic` | `int` |  |  |
| 2 | `IdTareaUbicacionEnc` | `int` | ✓ |  |
| 3 | `IdTarima` | `int` | ✓ |  |
| 4 | `codigoTarima` | `nvarchar(50)` | ✓ |  |
| 5 | `nombreTipoTarima` | `nvarchar(50)` | ✓ |  |
| 6 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 7 | `Utilizada` | `bit` | ✓ |  |
| 8 | `FechaUtilizacion` | `datetime` | ✓ |  |
| 9 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |

## Consume

- `tarimas`
- `tipo_tarima`
- `trans_ubic_tarima`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_TarimasUsadasEnTransaccion
AS
SELECT        dbo.trans_ubic_tarima.IdTarimaTareaUbic, dbo.trans_ubic_tarima.IdTareaUbicacionEnc, dbo.trans_ubic_tarima.IdTarima, dbo.tarimas.codigo AS codigoTarima, dbo.tipo_tarima.Nombre AS nombreTipoTarima, 
                         dbo.trans_ubic_tarima.Codigo, dbo.trans_ubic_tarima.Utilizada, dbo.trans_ubic_tarima.FechaUtilizacion, dbo.trans_ubic_tarima.user_agr, dbo.trans_ubic_tarima.fec_agr, dbo.trans_ubic_tarima.user_mod, 
                         dbo.trans_ubic_tarima.fec_mod
FROM            dbo.tipo_tarima INNER JOIN
                         dbo.tarimas ON dbo.tipo_tarima.IdTipoTarima = dbo.tarimas.IdTipoTarima RIGHT OUTER JOIN
                         dbo.trans_ubic_tarima ON dbo.tarimas.IdTarima = dbo.trans_ubic_tarima.IdTarima
```
