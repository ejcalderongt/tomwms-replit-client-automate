---
id: db-brain-table-trans-pe-docu-ref
type: db-table
title: dbo.trans_pe_docu_ref
schema: dbo
name: trans_pe_docu_ref
kind: table
rows: 20
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_pe_docu_ref`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 20 |
| Schema modify_date | 2021-08-25 |
| Columnas | 13 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDocumentoRef` | `int` |  |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `Descripcion` | `nvarchar(150)` | ✓ |  |
| 5 | `FechaDocumento` | `datetime` | ✓ |  |
| 6 | `FechaAsignacion` | `datetime` | ✓ |  |
| 7 | `FechaAgregado` | `datetime` | ✓ |  |
| 8 | `Asignado` | `bit` | ✓ |  |
| 9 | `Activo` | `bit` | ✓ |  |
| 10 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 11 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 12 | `referencia` | `nvarchar(50)` | ✓ |  |
| 13 | `codigo_cliente` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_pe_docu_ref` | CLUSTERED · **PK** | IdDocumentoRef |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

