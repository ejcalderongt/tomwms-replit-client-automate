---
id: db-brain-view-vw-resoluciones-operador
type: db-view
title: dbo.VW_Resoluciones_Operador
schema: dbo
name: VW_Resoluciones_Operador
kind: view
modify_date: 2022-11-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Resoluciones_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-11-28 |
| Columnas | 8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Bodega` | `int` | ✓ |  |
| 2 | `Operador` | `int` |  |  |
| 3 | `Nombre` | `nvarchar(201)` | ✓ |  |
| 4 | `serie` | `nvarchar(50)` | ✓ |  |
| 5 | `Inicial` | `int` | ✓ |  |
| 6 | `Final` | `int` | ✓ |  |
| 7 | `Actual` | `int` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Consume

- `operador`
- `resolucion_lp_operador`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Resoluciones_Operador]
AS
SELECT   r.idbodega AS Bodega, o.IdOperador AS Operador, o.nombres + ' ' + o.apellidos AS Nombre, r.serie, r.correlativo_inicial AS Inicial, r.correlativo_final AS Final, r.correlativo_actual AS Actual, r.activo
FROM            dbo.resolucion_lp_operador AS r INNER JOIN
                         dbo.operador AS o ON o.IdOperador = r.idoperador
```
