---
id: db-brain-table-producto-presentacion-tarima
type: db-table
title: dbo.producto_presentacion_tarima
schema: dbo
name: producto_presentacion_tarima
kind: table
rows: 0
modify_date: 2017-05-30
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_presentacion_tarima`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-05-30 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPresentacionTarima` | `int` |  |  |
| 2 | `IdPresentacion` | `int` | ✓ |  |
| 3 | `IdTipoTarima` | `int` | ✓ |  |
| 4 | `Cantidad` | `float` | ✓ |  |
| 5 | `CantidadPorCama` | `float` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_presentacion_tarima` | CLUSTERED · **PK** | IdPresentacionTarima |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_presentacion_tarima_producto_presentacion` → `producto_presentacion`
- `FK_producto_presentacion_tarima_tipo_tarima` → `tipo_tarima`

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Presentacion_Tarima` (view)

