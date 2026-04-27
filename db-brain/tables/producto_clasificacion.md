---
id: db-brain-table-producto-clasificacion
type: db-table
title: dbo.producto_clasificacion
schema: dbo
name: producto_clasificacion
kind: table
rows: 28
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_clasificacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 28 |
| Schema modify_date | 2024-02-01 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:1 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 3 | `IdClasificacion` | `int` |  |  |
| 4 | `IdPropietario` | `int` |  |  |
| 5 | `nombre` | `nvarchar(50)` | ✓ |  |
| 6 | `activo` | `bit` | ✓ |  |
| 7 | `sistema` | `bit` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_mod` | `datetime` |  |  |
| 12 | `codigo` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_clasificacion` | CLUSTERED · **PK** | IdClasificacion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_clasificacion_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `cliente_tiempos` (`FK_cliente_tiempos_producto_clasificacion`)
- `producto` (`FK_producto_producto_clasificacion`)
- `proveedor_tiempos` (`FK_proveedor_tiempos_producto_clasificacion`)
- `regla_vencimiento` (`FK__regla_ven__IdPro__7DBBC965`)

## Quién la referencia

**38** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `vw_existencias_producto_categoria` (view)
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Indicador_Picking_Detalle` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Pedido` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_Productividad_Picking` (view)
- `VW_Producto` (view)
- `VW_ProductoClasificacion` (view)
- `VW_ProductoOC` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Tipo_Producto` (view)
- `VW_Stock_Res_US` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_TiempoCliente` (view)
- `VW_TiempoProveedor` (view)
- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)

