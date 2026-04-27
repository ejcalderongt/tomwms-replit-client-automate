---
id: db-brain-table-producto-familia
type: db-table
title: dbo.producto_familia
schema: dbo
name: producto_familia
kind: table
rows: 0
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_familia`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-02-01 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:1 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdFamilia` | `int` |  |  |
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
| `PK_producto_familia_1` | CLUSTERED · **PK** | IdFamilia |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_familia_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `cliente_tiempos` (`FK_cliente_tiempos_producto_familia`)
- `producto` (`FK_producto_producto_familia`)
- `proveedor_tiempos` (`FK_proveedor_tiempos_producto_familia`)
- `regla_vencimiento` (`FK__regla_ven__IdPro__7CC7A52C`)

## Quién la referencia

**24** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `vw_existencias_producto_categoria` (view)
- `VW_Indicador_Picking_Detalle` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Productividad_Picking` (view)
- `VW_Producto` (view)
- `VW_ProductoFamilia` (view)
- `VW_ProductoOC` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_TiempoCliente` (view)
- `VW_TiempoProveedor` (view)

