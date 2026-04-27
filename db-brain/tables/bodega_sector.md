---
id: db-brain-table-bodega-sector
type: db-table
title: dbo.bodega_sector
schema: dbo
name: bodega_sector
kind: table
rows: 153
modify_date: 2019-12-10
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega_sector`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 153 |
| Schema modify_date | 2019-12-10 |
| Columnas | 23 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdSector` | `int` |  |  |
| 3 | `IdArea` | `int` |  |  |
| 4 | `sistema` | `bit` | ✓ |  |
| 5 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 6 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |
| 11 | `alto` | `float` | ✓ |  |
| 12 | `largo` | `float` | ✓ |  |
| 13 | `ancho` | `float` | ✓ |  |
| 14 | `margen_izquierdo` | `float` | ✓ |  |
| 15 | `margen_derecho` | `float` | ✓ |  |
| 16 | `margen_superior` | `float` | ✓ |  |
| 17 | `margen_inferior` | `float` | ✓ |  |
| 18 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 19 | `IdSectorIzquierda` | `int` | ✓ |  |
| 20 | `IdSectorDerecha` | `int` | ✓ |  |
| 21 | `horizontal` | `bit` | ✓ |  |
| 22 | `pos_x` | `float` | ✓ |  |
| 23 | `pos_y` | `float` | ✓ |  |
| 24 | `IdBodega` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_BodegaSectorId` | CLUSTERED · **PK** | IdSector, IdBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_bodega_sector_bodega_area` → `bodega_area`

### Entrantes (otra tabla → esta)

- `bodega_tramo` (`FK_bodega_tramo_bodega_sector`)

## Quién la referencia

**23** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `VW_BodegaSector` (view)
- `VW_BodegaTramo` (view)
- `VW_BodegaUbicacion` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_OcupacionBodega` (view)
- `VW_OcupacionBodegaTramo` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_Producto_Estado_Ubic_Bodega` (view)
- `VW_ProductoEstadoUbicacion` (view)
- `VW_ProductoEstadoUbicDefecto` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_Ubicaciones_Inventario_Ciclico` (view)
- `VW_Ubicaciones_Picking` (view)
- `vw_ubicaciones_por_regla` (view)
- `VW_Ubicaciones_Tramo_Disponibles` (view)
- `VW_Zona_Picking_Tramo` (view)

