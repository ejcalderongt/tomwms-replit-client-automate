---
id: db-brain-table-version-wms-hh
type: db-table
title: dbo.version_wms_hh
schema: dbo
name: version_wms_hh
kind: table
rows: 48
modify_date: 2021-08-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.version_wms_hh`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 48 |
| Schema modify_date | 2021-08-26 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresaVersion` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `version` | `nvarchar(50)` | ✓ |  |
| 4 | `notas` | `nvarchar(150)` | ✓ |  |
| 5 | `fecha` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_P_EMPRESA_VERSION` | CLUSTERED · **PK** | IdEmpresaVersion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

