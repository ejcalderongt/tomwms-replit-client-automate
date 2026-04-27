---
id: db-brain-table-proveedor
type: db-table
title: dbo.proveedor
schema: dbo
name: proveedor
kind: table
rows: 1386
modify_date: 2025-06-09
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.proveedor`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.386 |
| Schema modify_date | 2025-06-09 |
| Columnas | 27 |
| Índices | 2 |
| FKs | out:2 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresa` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdProveedor` | `int` |  |  |
| 4 | `codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `nombre` | `nvarchar(100)` | ✓ |  |
| 6 | `telefono` | `nvarchar(50)` | ✓ |  |
| 7 | `nit` | `nvarchar(25)` | ✓ |  |
| 8 | `direccion` | `nvarchar(250)` | ✓ |  |
| 9 | `email` | `nvarchar(50)` | ✓ |  |
| 10 | `contacto` | `nvarchar(100)` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |
| 12 | `muestra_precio` | `bit` | ✓ |  |
| 13 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 14 | `fec_agr` | `datetime` | ✓ |  |
| 15 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 16 | `fec_mod` | `datetime` | ✓ |  |
| 17 | `actualiza_costo_oc` | `bit` | ✓ |  |
| 18 | `idubicacionvirtual` | `int` | ✓ |  |
| 19 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 20 | `es_bodega_traslado` | `bit` | ✓ |  |
| 21 | `referencia` | `nvarchar(25)` | ✓ |  |
| 22 | `sistema` | `bit` | ✓ |  |
| 24 | `IdConfiguracionBarraPallet` | `int` | ✓ |  |
| 25 | `es_proveedor_servicio` | `bit` | ✓ |  |
| 26 | `IdBodegaAreaSAP` | `int` | ✓ |  |
| 27 | `IDPAIS` | `int` | ✓ |  |
| 28 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_proveedor_1` | CLUSTERED · **PK** | IdProveedor |
| `NCLI_Proveedor_20200120_EJC` | NONCLUSTERED | nombre, IdPropietario |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_proveedor_empresa` → `empresa`
- `FK_proveedor_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `proveedor_bodega` (`FK_proveedor_bodega_proveedor`)
- `proveedor_tiempos` (`FK_proveedor_tiempos_proveedor`)
- `regla_vencimiento` (`FK__regla_ven__IdPro__7FA411D7`)

## Quién la referencia

**30** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Cantidad_Ingresos_Proveedor` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_OrdenCompra` (view)
- `VW_OrdenCompraPreIngreso` (view)
- `VW_Proveedor` (view)
- `VW_ProveedorBodega` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_CONOC_FIN` (view)
- `VW_REC_SIN_OC` (view)
- `VW_Recepcion` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Recepcion_For_HH_By_IdBodega` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_RecepcionConOC` (view)
- `VW_RecepcionCostoArancel` (view)
- `VW_RecepcionSinOC` (view)
- `VW_RecOC_MIX` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Reporte_Recepcion_20190727` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Transito` (view)
- `VW_Tiempos_ingreso` (view)
- `VW_Tiempos_Ingreso_Operador` (view)

