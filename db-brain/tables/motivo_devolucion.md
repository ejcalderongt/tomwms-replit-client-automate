---
id: db-brain-table-motivo-devolucion
type: db-table
title: dbo.motivo_devolucion
schema: dbo
name: motivo_devolucion
kind: table
rows: 12
modify_date: 2018-04-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.motivo_devolucion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 12 |
| Schema modify_date | 2018-04-12 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:2 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMotivoDevolucion` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `IdPropietario` | `int` | ✓ |  |
| 4 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `activo` | `bit` |  |  |
| 10 | `es_detalle` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_motivo_devolucion` | CLUSTERED · **PK** | IdMotivoDevolucion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_motivo_devolucion_empresa` → `empresa`
- `FK_motivo_devolucion_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `motivo_devolucion_bodega` (`FK_motivo_devolucion_bodega_motivo_devolucion`)
- `trans_oc_det` (`FK_trans_oc_det_motivo_devolucion`)
- `trans_oc_enc` (`FK_trans_oc_enc_motivo_devolucion`)
- `trans_re_det` (`FK_trans_re_det_motivo_devolucion`)

## Quién la referencia

**9** objetos:

- `CLBD` (stored_procedure)
- `VW_MotivoDevolucion` (view)
- `VW_OrdenCompraPreIngreso` (view)
- `VW_Recepcion_For_HH_By_IdBodega` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)

