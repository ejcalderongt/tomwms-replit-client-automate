---
id: db-brain-table-transacciones-log
type: db-table
title: dbo.transacciones_log
schema: dbo
name: transacciones_log
kind: table
rows: 0
modify_date: 2019-09-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.transacciones_log`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2019-09-02 |
| Columnas | 15 |
| Índices | 1 |
| FKs | out:7 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTransaccionLog` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdObservacion` | `int` |  |  |
| 6 | `IdProductoBodega` | `int` |  |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `IdProductoEstado` | `int` | ✓ |  |
| 9 | `IdUnidadMedida` | `int` | ✓ |  |
| 10 | `IdUbicacion` | `int` | ✓ |  |
| 11 | `cantidad` | `float` | ✓ |  |
| 12 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 13 | `fec_agr` | `datetime` | ✓ |  |
| 14 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 15 | `fec_mod` | `datetime` | ✓ |  |
| 16 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_transacciones_log` | CLUSTERED · **PK** | IdTransaccionLog, IdEmpresa, IdPropietarioBodega, IdObservacion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_transacciones_log_empresa` → `empresa`
- `FK_transacciones_log_producto_bodega` → `producto_bodega`
- `FK_transacciones_log_producto_estado` → `producto_estado`
- `FK_transacciones_log_producto_presentacion` → `producto_presentacion`
- `FK_transacciones_log_propietario_bodega` → `propietario_bodega`
- `FK_transacciones_log_sis_obs_log` → `sis_obs_log`
- `FK_transacciones_log_unidad_medida` → `unidad_medida`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `VW_RevisionProducto` (view)

