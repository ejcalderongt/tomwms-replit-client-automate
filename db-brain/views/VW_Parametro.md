---
id: db-brain-view-vw-parametro
type: db-view
title: dbo.VW_Parametro
schema: dbo
name: VW_Parametro
kind: view
modify_date: 2016-03-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Parametro`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-03-01 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdParametro` | `int` |  |  |
| 2 | `Tipo` | `nvarchar(50)` | ✓ |  |
| 3 | `Descripción` | `nvarchar(50)` | ✓ |  |
| 4 | `Valor Texto` | `nvarchar(50)` | ✓ |  |
| 5 | `Valor Númerico` | `float` | ✓ |  |
| 6 | `Valor Fecha` | `datetime` | ✓ |  |
| 7 | `Valor Lógico` | `bit` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Consume

- `P_parametro`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW VW_Parametro
AS
SELECT IdParametro,Tipo,descripcion AS Descripción,
valor_texto AS 'Valor Texto',valor_numerico AS 'Valor Númerico',
valor_fecha AS 'Valor Fecha',valor_logico AS 'Valor Lógico',activo 
FROM P_parametro
```
