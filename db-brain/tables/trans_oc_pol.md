---
id: db-brain-table-trans-oc-pol
type: db-table
title: dbo.trans_oc_pol
schema: dbo
name: trans_oc_pol
kind: table
rows: 0
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_pol`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-02-01 |
| Columnas | 55 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraPol` | `int` |  |  |
| 2 | `IdOrdenCompraEnc` | `int` |  |  |
| 3 | `bl_no` | `varchar(50)` | ✓ |  |
| 4 | `NoPoliza` | `nvarchar(50)` | ✓ |  |
| 5 | `pto_descarga` | `nvarchar(50)` | ✓ |  |
| 6 | `viaje_no` | `nvarchar(50)` | ✓ |  |
| 7 | `buque_no` | `nvarchar(50)` | ✓ |  |
| 8 | `remitente` | `nvarchar(50)` | ✓ |  |
| 9 | `fecha_abordaje` | `datetime` | ✓ |  |
| 10 | `destino` | `nvarchar(50)` | ✓ |  |
| 11 | `dir_destino` | `nvarchar(50)` | ✓ |  |
| 12 | `descripcion` | `nvarchar(250)` | ✓ |  |
| 13 | `po_number` | `nvarchar(50)` | ✓ |  |
| 14 | `cantidad` | `int` | ✓ |  |
| 15 | `piezas` | `int` | ✓ |  |
| 16 | `total_kgs` | `float` | ✓ |  |
| 17 | `cbm` | `float` | ✓ |  |
| 18 | `dua` | `nvarchar(50)` | ✓ |  |
| 19 | `fecha_poliza` | `datetime` | ✓ |  |
| 20 | `pais_procede` | `nvarchar(50)` | ✓ |  |
| 21 | `tipo_cambio` | `float` | ✓ |  |
| 22 | `total_valoraduana` | `float` | ✓ |  |
| 23 | `total_lineas` | `int` | ✓ |  |
| 24 | `total_bultos` | `int` | ✓ |  |
| 25 | `total_bultos_peso` | `float` | ✓ |  |
| 26 | `total_usd` | `float` | ✓ |  |
| 27 | `total_flete` | `float` | ✓ |  |
| 28 | `total_seguro` | `float` | ✓ |  |
| 29 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 30 | `fec_agr` | `datetime` | ✓ |  |
| 31 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 32 | `fec_mod` | `datetime` | ✓ |  |
| 33 | `clave_aduana` | `varchar(50)` | ✓ |  |
| 34 | `nit_imp_exp` | `varchar(50)` | ✓ |  |
| 35 | `clase` | `varchar(50)` | ✓ |  |
| 36 | `mod_transporte` | `varchar(50)` | ✓ |  |
| 37 | `total_liquidar` | `float` | ✓ |  |
| 38 | `total_general` | `float` | ✓ |  |
| 39 | `valor_aduana` | `float` | ✓ |  |
| 40 | `valor_fob` | `float` | ✓ |  |
| 41 | `valor_iva` | `float` | ✓ |  |
| 42 | `valor_dai` | `float` | ✓ |  |
| 43 | `valor_seguro` | `float` | ✓ |  |
| 44 | `valor_flete` | `float` | ✓ |  |
| 45 | `peso_neto` | `float` | ✓ |  |
| 46 | `IdRegimen` | `int` | ✓ |  |
| 47 | `numero_orden` | `nvarchar(50)` | ✓ |  |
| 48 | `Codigo_Barra` | `nvarchar(1000)` | ✓ |  |
| 49 | `codigo_poliza` | `nvarchar(150)` | ✓ |  |
| 50 | `ticket` | `nvarchar(50)` | ✓ |  |
| 51 | `activo` | `bit` |  |  |
| 52 | `fecha_aceptacion` | `date` | ✓ |  |
| 53 | `fecha_llegada` | `datetime` | ✓ |  |
| 54 | `total_otros` | `float` | ✓ |  |
| 55 | `total_bultos_peso_neto` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_oc_pol` | CLUSTERED · **PK** | IdOrdenCompraPol, IdOrdenCompraEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**32** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Doc_Con_Diferencias` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_historico` (view)
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_MovimientosDetalle` (view)
- `VW_OrdenCompra` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_RecepcionCostoArancel` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)
- `VW_Valorizacion_OC` (view)

