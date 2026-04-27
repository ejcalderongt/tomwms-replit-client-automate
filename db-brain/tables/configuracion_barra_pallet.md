---
id: db-brain-table-configuracion-barra-pallet
type: db-table
title: dbo.configuracion_barra_pallet
schema: dbo
name: configuracion_barra_pallet
kind: table
rows: 1
modify_date: 2019-03-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.configuracion_barra_pallet`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2019-03-28 |
| Columnas | 6 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConfiguracionPallet` | `int` |  |  |
| 2 | `LongCodBodegaOrigen` | `int` | ✓ |  |
| 3 | `LongCodProducto` | `int` | ✓ |  |
| 4 | `LongLP` | `int` | ✓ |  |
| 5 | `CodigoNumerico` | `bit` | ✓ |  |
| 6 | `IdentificadorInicio` | `char(4)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_configuracion_barra_pallet` | CLUSTERED · **PK** | IdConfiguracionPallet |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

