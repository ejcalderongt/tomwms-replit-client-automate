---
id: db-brain-view-vw-resoluciones-usuario
type: db-view
title: dbo.VW_Resoluciones_Usuario
schema: dbo
name: VW_Resoluciones_Usuario
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Resoluciones_Usuario`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
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

- `resolucion_lp_usuario`
- `usuario`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Resoluciones_Usuario]
AS
SELECT   r.idbodega AS Bodega, u.IdUsuario AS Operador, u.nombres + ' ' + u.apellidos AS Nombre, 
r.serie, r.correlativo_inicial AS Inicial, r.correlativo_final AS Final, r.correlativo_actual AS Actual, r.activo
FROM            dbo.resolucion_lp_usuario AS r INNER JOIN
                         dbo.usuario AS u ON u.IdUsuario = r.idusuario
```
