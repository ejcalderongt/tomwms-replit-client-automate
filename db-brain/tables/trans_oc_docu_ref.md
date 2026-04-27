---
id: db-brain-table-trans-oc-docu-ref
type: db-table
title: dbo.trans_oc_docu_ref
schema: dbo
name: trans_oc_docu_ref
kind: table
rows: 0
modify_date: 2021-09-08
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_docu_ref`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-09-08 |
| Columnas | 9 |
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

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_oc_docu_ref` | CLUSTERED · **PK** | IdDocumentoRef |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

