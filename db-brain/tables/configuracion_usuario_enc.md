---
id: db-brain-table-configuracion-usuario-enc
type: db-table
title: dbo.configuracion_usuario_enc
schema: dbo
name: configuracion_usuario_enc
kind: table
rows: 19
modify_date: 2022-09-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.configuracion_usuario_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 19 |
| Schema modify_date | 2022-09-11 |
| Columnas | 3 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConfiguracionUsuarioEnc` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdUsuario` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_configuracion_usuario_enc` | CLUSTERED · **PK** | IdConfiguracionUsuarioEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Configuracion_Usuario_Template` (view)

