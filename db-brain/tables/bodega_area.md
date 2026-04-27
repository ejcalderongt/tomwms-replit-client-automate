---
id: db-brain-table-bodega-area
type: db-table
title: dbo.bodega_area
schema: dbo
name: bodega_area
kind: table
rows: 18
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega_area`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 18 |
| Schema modify_date | 2024-02-01 |
| Columnas | 19 |
| Índices | 1 |
| FKs | out:1 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdArea` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `Descripcion` | `nvarchar(50)` | ✓ |  |
| 4 | `sistema` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |
| 11 | `alto` | `float` | ✓ |  |
| 12 | `largo` | `float` | ✓ |  |
| 13 | `ancho` | `float` | ✓ |  |
| 14 | `margen_izquierdo` | `float` | ✓ |  |
| 15 | `margen_derecho` | `float` | ✓ |  |
| 16 | `margen_superior` | `float` | ✓ |  |
| 17 | `margen_inferior` | `float` | ✓ |  |
| 18 | `grupo` | `nvarchar(50)` | ✓ |  |
| 19 | `IdUbicacionRef` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_BodegaAreaId` | CLUSTERED · **PK** | IdArea, IdBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_bodega_area_bodega` → `bodega`

### Entrantes (otra tabla → esta)

- `bodega_sector` (`FK_bodega_sector_bodega_area`)
- `cliente_bodega` (`FK_cliente_bodega_area`)
- `proveedor_bodega` (`FK_proveedor_bodega_area`)

## Quién la referencia

**27** objetos:

- `CLBD` (stored_procedure)
- `Get_Codigo_Area_By_IdUbicacion` (scalar_function)
- `Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega` (stored_procedure)
- `Nombre_Area` (scalar_function)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `VW_BodegaArea` (view)
- `VW_BodegaSector` (view)
- `VW_BodegaTramo` (view)
- `VW_BodegaUbicacion` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_Fiscal_Valorización` (view)
- `VW_OcupacionBodega` (view)
- `VW_OcupacionBodegaTramo` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_Producto_Estado_Ubic_Bodega` (view)
- `VW_ProductoEstadoUbicacion` (view)
- `VW_ProductoEstadoUbicDefecto` (view)
- `VW_Stock_res_jornada` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_Ubicaciones_Inventario_Ciclico` (view)
- `VW_Ubicaciones_Picking` (view)
- `vw_ubicaciones_por_regla` (view)
- `VW_Ubicaciones_Tramo_Disponibles` (view)
- `VW_Zona_Picking_Tramo` (view)

