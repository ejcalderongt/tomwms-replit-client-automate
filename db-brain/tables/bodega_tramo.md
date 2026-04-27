---
id: db-brain-table-bodega-tramo
type: db-table
title: dbo.bodega_tramo
schema: dbo
name: bodega_tramo
kind: table
rows: 171
modify_date: 2021-10-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega_tramo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 171 |
| Schema modify_date | 2021-10-26 |
| Columnas | 27 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTramo` | `int` |  |  |
| 2 | `IdSector` | `int` |  |  |
| 3 | `sistema` | `bit` | ✓ |  |
| 4 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `alto` | `float` | ✓ |  |
| 11 | `largo` | `float` | ✓ |  |
| 12 | `ancho` | `float` | ✓ |  |
| 13 | `margen_izquierdo` | `float` | ✓ |  |
| 14 | `margen_derecho` | `float` | ✓ |  |
| 15 | `margen_superior` | `float` | ✓ |  |
| 16 | `margen_inferior` | `float` | ✓ |  |
| 17 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 18 | `Indice_x` | `int` | ✓ |  |
| 19 | `Orientacion` | `int` | ✓ |  |
| 20 | `IdTipoProductoDefault` | `int` | ✓ |  |
| 21 | `IdFontEnc` | `int` | ✓ |  |
| 22 | `IdTipoRack` | `int` | ✓ |  |
| 23 | `es_rack` | `bit` | ✓ |  |
| 24 | `Horizontal` | `bit` | ✓ |  |
| 25 | `IdArea` | `int` |  |  |
| 26 | `IdBodega` | `int` |  |  |
| 27 | `orden_descendente` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_BodegaTramoId` | CLUSTERED · **PK** | IdTramo, IdBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_bodega_tramo_bodega_sector` → `bodega_sector`

### Entrantes (otra tabla → esta)

- `bodega_ubicacion` (`FK_bodega_ubicacion_bodega_tramo`)

## Quién la referencia

**60** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega` (stored_procedure)
- `Nombre_Completo_Ubicacion` (scalar_function)
- `Nombre_Completo_Ubicacion_1` (scalar_function)
- `Nombre_Completo_Ubicacion_2` (scalar_function)
- `Nombre_Completo_Ubicacion_3` (scalar_function)
- `Nombre_Completo_Ubicacion_Barra` (scalar_function)
- `Nombre_Completo_Ubicacion_Barra_1` (scalar_function)
- `Nombre_Completo_Ubicacion_Barra_2` (scalar_function)
- `Nombre_Completo_Ubicacion_Barra_3` (scalar_function)
- `Nombre_Tramo` (scalar_function)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `VW_BodegaTramo` (view)
- `VW_BodegaUbicacion` (view)
- `VW_CalculoVencimientos` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Detalle_Licencias_Inconsistentes` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Licencias_Por_Ubicacion` (view)
- `VW_OcupacionBodega` (view)
- `VW_OcupacionBodegaTramo` (view)
- `VW_Operador_Zona_Picking` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_Producto_Estado_Ubic_Bodega` (view)
- `VW_ProductoEstadoUbic` (view)
- `VW_ProductoEstadoUbicacion` (view)
- `VW_ProductoEstadoUbicDefecto` (view)
- `VW_ProximosVencimiento` (view)
- `VW_Reporte_Detalle_Stock_DataSet` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Rep_20200112` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_Res_Pedido` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Reservado_By_IdPedidoEnc` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_Trans_Inv_Stock` (view)
- `VW_TransUbicHhDet` (view)
- `VW_Ubicaciones_Inventario_Ciclico` (view)
- `VW_Ubicaciones_Picking` (view)
- `vw_ubicaciones_por_regla` (view)
- `VW_Ubicaciones_Tramo_Disponibles` (view)
- `VW_Valorizacion_OC` (view)
- `VW_Zona_Picking_Tramo` (view)
- `WMS_Existencia` (view)

