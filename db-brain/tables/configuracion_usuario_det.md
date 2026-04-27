---
id: db-brain-table-configuracion-usuario-det
type: db-table
title: dbo.configuracion_usuario_det
schema: dbo
name: configuracion_usuario_det
kind: table
rows: 141
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.configuracion_usuario_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 141 |
| Schema modify_date | 2023-08-21 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConfiguracionUsuarioDet` | `int` |  |  |
| 2 | `IdConfiguracionUsuarioEnc` | `int` | ✓ |  |
| 3 | `Maquina_Host` | `nvarchar(50)` | ✓ |  |
| 4 | `Maquina_IP` | `nvarchar(50)` | ✓ |  |
| 5 | `Nombre_Template` | `nvarchar(50)` | ✓ |  |
| 6 | `String_Template` | `xml` | ✓ |  |
| 7 | `Fecha_Agregado` | `datetime` | ✓ |  |
| 8 | `Fecha_Modificado` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_configuracion_det` | CLUSTERED · **PK** | IdConfiguracionUsuarioDet |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Configuracion_Usuario_Template` (view)

