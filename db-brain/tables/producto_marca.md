---
id: db-brain-table-producto-marca
type: db-table
title: dbo.producto_marca
schema: dbo
name: producto_marca
kind: table
rows: 16
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_marca`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 16 |
| Schema modify_date | 2024-07-02 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMarca` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `codigo` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_marca` | CLUSTERED · **PK** | IdMarca |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_marca_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `producto` (`FK_producto_producto_marca`)

## Quién la referencia

**14** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Indicador_Picking_Detalle` (view)
- `VW_Producto` (view)
- `VW_ProductoMarca` (view)
- `VW_ProductoOC` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Resumen` (view)

