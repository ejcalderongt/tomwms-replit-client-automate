---
id: db-brain-table-tipo-tarima
type: db-table
title: dbo.tipo_tarima
schema: dbo
name: tipo_tarima
kind: table
rows: 10
modify_date: 2017-05-30
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tipo_tarima`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 10 |
| Schema modify_date | 2017-05-30 |
| Columnas | 16 |
| Índices | 1 |
| FKs | out:0 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoTarima` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `Alto` | `float` | ✓ |  |
| 4 | `Largo` | `float` | ✓ |  |
| 5 | `Ancho` | `float` | ✓ |  |
| 6 | `CargaDinamica` | `float` | ✓ |  |
| 7 | `CargaEstatica` | `float` | ✓ |  |
| 8 | `CargaEstanterias` | `float` | ✓ |  |
| 9 | `EntradasTransPaleta` | `float` | ✓ |  |
| 10 | `PesoPromedio` | `float` | ✓ |  |
| 11 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_agr` | `date` | ✓ |  |
| 13 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 14 | `fec_mod` | `date` | ✓ |  |
| 15 | `activo` | `bit` | ✓ |  |
| 16 | `Tara` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_tarima` | CLUSTERED · **PK** | IdTipoTarima |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto_presentacion_tarima` (`FK_producto_presentacion_tarima_tipo_tarima`)
- `tarimas` (`FK_tarimas_tipo_tarima`)

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `VW_Presentacion_Tarima` (view)
- `VW_Tarimas` (view)
- `VW_TarimasUsadasEnTransaccion` (view)

