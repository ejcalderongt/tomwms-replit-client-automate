---
id: db-brain-view-vw-configuracion-usuario-template
type: db-view
title: dbo.VW_Configuracion_Usuario_Template
schema: dbo
name: VW_Configuracion_Usuario_Template
kind: view
modify_date: 2022-09-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Configuracion_Usuario_Template`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-09-11 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConfiguracionUsuarioEnc` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdUsuario` | `int` | ✓ |  |
| 4 | `IdConfiguracionUsuarioDet` | `int` |  |  |
| 5 | `Maquina_Host` | `nvarchar(50)` | ✓ |  |
| 6 | `Maquina_IP` | `nvarchar(50)` | ✓ |  |
| 7 | `Nombre_Template` | `nvarchar(50)` | ✓ |  |
| 8 | `String_Template` | `nvarchar(50)` | ✓ |  |
| 9 | `Fecha_Agregado` | `datetime` | ✓ |  |
| 10 | `Fecha_Modificado` | `datetime` | ✓ |  |

## Consume

- `configuracion_usuario_det`
- `configuracion_usuario_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Configuracion_Usuario_Template]
AS
SELECT        dbo.configuracion_usuario_enc.IdConfiguracionUsuarioEnc, dbo.configuracion_usuario_enc.IdEmpresa, dbo.configuracion_usuario_enc.IdUsuario, dbo.configuracion_usuario_det.IdConfiguracionUsuarioDet, 
              dbo.configuracion_usuario_det.Maquina_Host, dbo.configuracion_usuario_det.Maquina_IP, dbo.configuracion_usuario_det.Nombre_Template, 
              dbo.configuracion_usuario_det.String_Template, dbo.configuracion_usuario_det.Fecha_Agregado, dbo.configuracion_usuario_det.Fecha_Modificado
FROM          dbo.configuracion_usuario_enc INNER JOIN
              dbo.configuracion_usuario_det ON dbo.configuracion_usuario_enc.IdConfiguracionUsuarioEnc = dbo.configuracion_usuario_det.IdConfiguracionUsuarioEnc
```
