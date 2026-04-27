---
id: db-brain-table-impresora-tipo-conexion
type: db-table
title: dbo.Impresora_Tipo_Conexion
schema: dbo
name: Impresora_Tipo_Conexion
kind: table
rows: 1
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Impresora_Tipo_Conexion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2024-07-02 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdImpresoraTipoConexion` | `int` |  |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `User_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `Fec_agr` | `datetime` | ✓ |  |
| 6 | `User_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `Fec_mod` | `datetime` | ✓ |  |
| 8 | `Activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_Impresora_Tipo_Conexion` | CLUSTERED · **PK** | IdImpresoraTipoConexion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

