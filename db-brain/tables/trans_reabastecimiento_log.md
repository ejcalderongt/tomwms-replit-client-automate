---
id: db-brain-table-trans-reabastecimiento-log
type: db-table
title: dbo.trans_reabastecimiento_log
schema: dbo
name: trans_reabastecimiento_log
kind: table
rows: 1218
modify_date: 2023-02-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_reabastecimiento_log`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.218 |
| Schema modify_date | 2023-02-27 |
| Columnas | 50 |
| Índices | 2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReabastecimientoLog` | `int` |  |  |
| 2 | `IdRellenado` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `IdProductoBodega` | `int` | ✓ |  |
| 5 | `IdProducto` | `int` | ✓ |  |
| 6 | `Codigo_Producto` | `nvarchar(50)` | ✓ |  |
| 7 | `Nombre_Producto` | `nvarchar(100)` | ✓ |  |
| 8 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 9 | `NombreUmBas` | `nvarchar(50)` | ✓ |  |
| 10 | `IdPresentacion` | `int` | ✓ |  |
| 11 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 12 | `Minimo` | `float` | ✓ |  |
| 13 | `Maximo` | `float` | ✓ |  |
| 14 | `IdProductoEstado` | `int` | ✓ |  |
| 15 | `Estado` | `nvarchar(50)` | ✓ |  |
| 16 | `StockUMBas` | `float` | ✓ |  |
| 17 | `ReservadoUmBas` | `float` | ✓ |  |
| 18 | `DisponibleUMBas` | `float` | ✓ |  |
| 19 | `factor` | `float` | ✓ |  |
| 20 | `StockPres` | `float` | ✓ |  |
| 21 | `ReservadoPres` | `float` | ✓ |  |
| 22 | `DisponiblePres` | `float` | ✓ |  |
| 23 | `Ubicacion` | `nvarchar(200)` | ✓ |  |
| 24 | `IdPropietarioBodega` | `int` | ✓ |  |
| 25 | `IdUbicacion` | `int` | ✓ |  |
| 26 | `IdTipoAccion` | `int` | ✓ |  |
| 27 | `Activo` | `bit` | ✓ |  |
| 28 | `IdPropietario` | `int` | ✓ |  |
| 29 | `Nombre_Propietario` | `nvarchar(100)` | ✓ |  |
| 30 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 31 | `fec_agr` | `datetime` | ✓ |  |
| 32 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 33 | `fec_mod` | `datetime` | ✓ |  |
| 34 | `IdUmBasAbastercerCon` | `int` | ✓ |  |
| 35 | `IdPresentacionAbastercerCon` | `int` | ✓ |  |
| 36 | `NombrePresentacionAbastecerCon` | `nvarchar(50)` | ✓ |  |
| 37 | `Enviado` | `bit` | ✓ |  |
| 38 | `Cancelado` | `bit` | ✓ |  |
| 39 | `Fecha_Generacion_Inexistencia` | `date` | ✓ |  |
| 40 | `Hora_Generacion_Inexistencia` | `datetime` | ✓ |  |
| 41 | `Fecha_Procesamiento_BOF` | `date` | ✓ |  |
| 42 | `Hora_Procesamiento_BOF` | `date` | ✓ |  |
| 43 | `IdOperadorBodega` | `int` | ✓ |  |
| 44 | `Procesado_HH` | `bit` | ✓ |  |
| 45 | `Fecha_Procesamiento_HH` | `datetime` | ✓ |  |
| 46 | `Stock_Ubicacion` | `float` | ✓ |  |
| 47 | `Cantidad_A_Ubicar` | `float` | ✓ |  |
| 48 | `Stock_Inferior` | `float` | ✓ |  |
| 49 | `Stock_Disponible` | `float` | ✓ |  |
| 50 | `IdTareaUbicacionEnc` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_reabastecimiento_log` | CLUSTERED · **PK** | IdReabastecimientoLog |
| `NCI_20230223_trans_reabastecimiento_log` | NONCLUSTERED | Maximo, IdProductoEstado, Estado, StockUMBas, ReservadoUmBas, DisponibleUMBas, factor, StockPres, ReservadoPres, DisponiblePres, Ubicacion, IdPropietarioBodega, IdUbicacion, IdTipoAccion, Activo, IdPropietario, Nombre_Propietario, user_agr, fec_agr, user_mod, fec_mod, IdUmBasAbastercerCon, IdPresentacionAbastercerCon, NombrePresentacionAbastecerCon, Enviado, Cancelado, Fecha_Generacion_Inexistencia, Hora_Generacion_Inexistencia, Fecha_Procesamiento_BOF, Hora_Procesamiento_BOF, IdOperadorBodega, Procesado_HH, Fecha_Procesamiento_HH, Minimo, Presentacion, IdPresentacion, NombreUmBas, IdUnidadMedidaBasica, Nombre_Producto, Codigo_Producto, IdProducto, IdProductoBodega, IdBodega, Stock_Ubicacion, Cantidad_A_Ubicar, Stock_Inferior, Stock_Disponible, IdTareaUbicacionEnc, IdRellenado |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

