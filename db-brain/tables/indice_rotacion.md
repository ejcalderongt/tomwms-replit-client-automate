---
id: db-brain-table-indice-rotacion
type: db-table
title: dbo.indice_rotacion
schema: dbo
name: indice_rotacion
kind: table
rows: 5
modify_date: 2018-01-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.indice_rotacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 5 |
| Schema modify_date | 2018-01-11 |
| Columnas | 5 |
| Índices | 1 |
| FKs | out:0 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdIndiceRotacion` | `int` |  |  |
| 2 | `Descripcion` | `nvarchar(50)` |  |  |
| 3 | `Activo` | `bit` |  |  |
| 4 | `IndicePrioridad` | `int` | ✓ |  |
| 5 | `Grupo` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_indice_rotacion` | CLUSTERED · **PK** | IdIndiceRotacion |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto` (`FK_producto_indice_rotacion`)
- `regla_ubic_det_ir` (`FK_regla_ubic_det_ir_indice_rotacion`)

## Quién la referencia

**15** objetos:

- `CLBD` (stored_procedure)
- `VW_Producto` (view)
- `VW_rptMinimosMaximos` (view)
- `VW_rptMinimosMaximos_v2` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Enc` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Trans_Inv_Stock` (view)
- `VW_Valorizacion_OC` (view)

