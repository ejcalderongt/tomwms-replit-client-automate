---
id: db-brain-table-trans-ubic-hh-det
type: db-table
title: dbo.trans_ubic_hh_det
schema: dbo
name: trans_ubic_hh_det
kind: table
rows: 695
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ubic_hh_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 695 |
| Schema modify_date | 2024-07-02 |
| Columnas | 18 |
| Índices | 1 |
| FKs | out:6 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareaUbicacionEnc` | `int` |  |  |
| 2 | `IdTareaUbicacionDet` | `int` |  |  |
| 3 | `IdStock` | `int` | ✓ |  |
| 4 | `IdUbicacionOrigen` | `int` | ✓ |  |
| 5 | `IdUbicacionDestino` | `int` | ✓ |  |
| 6 | `IdEstadoOrigen` | `int` | ✓ |  |
| 7 | `IdEstadoDestino` | `int` | ✓ |  |
| 8 | `IdOperadorBodega` | `int` | ✓ |  |
| 9 | `HoraInicio` | `datetime` | ✓ |  |
| 10 | `HoraFin` | `datetime` | ✓ |  |
| 11 | `Realizado` | `bit` | ✓ |  |
| 12 | `cantidad` | `float` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `recibido` | `float` | ✓ |  |
| 15 | `estado` | `nvarchar(25)` | ✓ |  |
| 16 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 17 | `IdBodega` | `int` | ✓ |  |
| 18 | `no_linea` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ubic_hh_det` | CLUSTERED · **PK** | IdTareaUbicacionEnc, IdTareaUbicacionDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_ubic_hh_det_bodega_ubic_dest` → `bodega_ubicacion`
- `FK_trans_ubic_hh_det_bodega_ubic_orig` → `bodega_ubicacion`
- `FK_trans_ubic_hh_det_operador_bodega` → `operador_bodega`
- `FK_trans_ubic_hh_det_producto_estado_destino` → `producto_estado`
- `FK_trans_ubic_hh_det_producto_estado_orig` → `producto_estado`
- `FK_trans_ubic_hh_det_trans_ubic_hh_enc` → `trans_ubic_hh_enc`

## Quién la referencia

**8** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Cantidad_Tareas_Ubicacion_Op` (view)
- `VW_Cantidad_Tareas_Ubicacion_Op_Items` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_TransUbicHhDet` (view)

