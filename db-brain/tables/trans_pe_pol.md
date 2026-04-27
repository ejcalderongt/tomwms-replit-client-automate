---
id: db-brain-table-trans-pe-pol
type: db-table
title: dbo.trans_pe_pol
schema: dbo
name: trans_pe_pol
kind: table
rows: 0
modify_date: 2024-10-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_pe_pol`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-10-01 |
| Columnas | 41 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenPedidoPol` | `int` |  |  |
| 2 | `IdOrdenPedidoEnc` | `int` |  |  |
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
| 33 | `codigo_poliza` | `nvarchar(50)` | ✓ |  |
| 34 | `ticket` | `nvarchar(50)` | ✓ |  |
| 35 | `numero_orden` | `nvarchar(50)` | ✓ |  |
| 36 | `fecha_aceptacion` | `datetime` | ✓ |  |
| 37 | `fecha_llegada` | `datetime` | ✓ |  |
| 38 | `total_otros` | `float` | ✓ |  |
| 39 | `IdRegimen` | `int` | ✓ |  |
| 40 | `activo` | `bit` | ✓ |  |
| 41 | `total_bultos_peso_neto` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_pe_pol` | CLUSTERED · **PK** | IdOrdenPedidoPol, IdOrdenPedidoEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**3** objetos:

- `VW_Despacho_Rep_Det_I` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_Propietario` (view)

