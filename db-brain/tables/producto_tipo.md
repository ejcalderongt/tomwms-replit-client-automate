---
id: db-brain-table-producto-tipo
type: db-table
title: dbo.producto_tipo
schema: dbo
name: producto_tipo
kind: table
rows: 2
modify_date: 2022-12-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_tipo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2 |
| Schema modify_date | 2022-12-17 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:1 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 3 | `IdTipoProducto` | `int` |  |  |
| 4 | `IdPropietario` | `int` |  |  |
| 5 | `NombreTipoProducto` | `nvarchar(50)` | ✓ |  |
| 6 | `Activo` | `bit` | ✓ |  |
| 7 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `codigo` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_tipo` | CLUSTERED · **PK** | IdTipoProducto |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_tipo_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `producto` (`FK_producto_producto_tipo`)
- `regla_ubic_det_tp` (`FK_regla_ubic_det_tp_producto_tipo`)

## Quién la referencia

**16** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Comparativo_NAV_WMS_ConCostos` (view)
- `VW_Despacho_Rep_DyD` (view)
- `vw_existencias_producto_categoria` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Inventario_prg_por_tipo` (view)
- `VW_Productividad_Picking` (view)
- `VW_Producto` (view)
- `VW_ProductoOC` (view)
- `VW_ProductoTipo` (view)
- `VW_Stock_Rep_20200112` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Resumen` (view)

