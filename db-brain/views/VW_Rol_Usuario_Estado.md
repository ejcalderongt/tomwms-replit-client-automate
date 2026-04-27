---
id: db-brain-view-vw-rol-usuario-estado
type: db-view
title: dbo.VW_Rol_Usuario_Estado
schema: dbo
name: VW_Rol_Usuario_Estado
kind: view
modify_date: 2024-10-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Rol_Usuario_Estado`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-10-28 |
| Columnas | 14 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRolUsuarioEstado` | `int` |  |  |
| 2 | `IdRolUsuario` | `int` | ✓ |  |
| 3 | `IdPropietario` | `int` | ✓ |  |
| 4 | `IdEstadoOrigen` | `int` | ✓ |  |
| 5 | `IdEstadoDestino` | `int` | ✓ |  |
| 6 | `Permitir` | `bit` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `activo` | `bit` |  |  |
| 12 | `Propietario` | `nvarchar(100)` |  |  |
| 13 | `EstadoOrigen` | `nvarchar(50)` | ✓ |  |
| 14 | `EstadoDestino` | `nvarchar(50)` | ✓ |  |

## Consume

- `producto_estado`
- `propietarios`
- `rol_usuario_estado`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Rol_Usuario_Estado]
AS
SELECT dbo.rol_usuario_estado.IdRolUsuarioEstado, dbo.rol_usuario_estado.IdRolUsuario, dbo.rol_usuario_estado.IdPropietario, dbo.rol_usuario_estado.IdEstadoOrigen, dbo.rol_usuario_estado.IdEstadoDestino, 
                  dbo.rol_usuario_estado.Permitir, dbo.rol_usuario_estado.user_agr, dbo.rol_usuario_estado.fec_agr, dbo.rol_usuario_estado.user_mod, dbo.rol_usuario_estado.fec_mod, dbo.rol_usuario_estado.activo, 
                  dbo.propietarios.nombre_comercial AS Propietario, dbo.producto_estado.nombre AS EstadoOrigen, producto_estado_1.nombre AS EstadoDestino
FROM     dbo.propietarios INNER JOIN
                  dbo.rol_usuario_estado ON dbo.propietarios.IdPropietario = dbo.rol_usuario_estado.IdPropietario INNER JOIN
                  dbo.producto_estado ON dbo.rol_usuario_estado.IdEstadoOrigen = dbo.producto_estado.IdEstado INNER JOIN
                  dbo.producto_estado AS producto_estado_1 ON dbo.rol_usuario_estado.IdEstadoDestino = producto_estado_1.IdEstado
```
