---
id: db-brain-view-vw-propietario-regla-recepcion
type: db-view
title: dbo.VW_Propietario_Regla_Recepcion
schema: dbo
name: VW_Propietario_Regla_Recepcion
kind: view
modify_date: 2017-07-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Propietario_Regla_Recepcion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-07-12 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `Regla` | `nvarchar(50)` | ✓ |  |
| 3 | `Propietario` | `nvarchar(100)` |  |  |
| 4 | `Mensaje` | `nvarchar(50)` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `IdPropietario` | `int` | ✓ |  |
| 7 | `IdReglaRecepcion` | `int` | ✓ |  |
| 8 | `Rechazar` | `bit` | ✓ |  |
| 9 | `StockNoDisponible` | `bit` | ✓ |  |
| 10 | `descripcion` | `nvarchar(100)` | ✓ |  |

## Consume

- `mensaje_regla`
- `propietario_reglas_enc`
- `propietarios`
- `reglas_recepcion`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_Propietario_Regla_Recepcion
AS
SELECT DISTINCT 
                      enc.IdReglaPropietarioEnc AS Código, rc.nombre AS Regla, pr.nombre_comercial AS Propietario, mr.Nombre AS Mensaje, enc.activo, enc.IdPropietario, 
                      enc.IdReglaRecepcion, rc.Rechazar, rc.StockNoDisponible, rc.descripcion
FROM         dbo.propietario_reglas_enc AS enc INNER JOIN
                      dbo.reglas_recepcion AS rc ON enc.IdReglaRecepcion = rc.IdReglaRecepcion INNER JOIN
                      dbo.mensaje_regla AS mr ON enc.IdMensajeRegla = mr.IdMensajeRegla INNER JOIN
                      dbo.propietarios AS pr ON enc.IdPropietario = pr.IdPropietario
```
