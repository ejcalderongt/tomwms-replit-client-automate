---
id: db-brain-table-proveedor-bodega
type: db-table
title: dbo.proveedor_bodega
schema: dbo
name: proveedor_bodega
kind: table
rows: 4594
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.proveedor_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4.594 |
| Schema modify_date | 2024-02-01 |
| Columnas | 9 |
| Índices | 2 |
| FKs | out:3 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdAsignacion` | `int` |  |  |
| 2 | `IdProveedor` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `IdAreaOrigen` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_proveedor_bodega` | CLUSTERED · **PK** | IdAsignacion |
| `NCL_Proveedor_Bodega_20200115_EJC` | NONCLUSTERED | IdProveedor, IdBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_proveedor_bodega_area` → `bodega_area`
- `FK_proveedor_bodega_bodega` → `bodega`
- `FK_proveedor_bodega_proveedor` → `proveedor`

### Entrantes (otra tabla → esta)

- `trans_oc_enc` (`FK_trans_oc_enc_proveedor`)

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

