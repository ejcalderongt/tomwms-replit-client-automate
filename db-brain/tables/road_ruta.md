---
id: db-brain-table-road-ruta
type: db-table
title: dbo.road_ruta
schema: dbo
name: road_ruta
kind: table
rows: 0
modify_date: 2016-10-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.road_ruta`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-10-26 |
| Columnas | 54 |
| Índices | 1 |
| FKs | out:0 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRuta` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` | ✓ |  |
| 3 | `IdUbicacionTransito` | `int` | ✓ |  |
| 4 | `CODIGO` | `nvarchar(15)` |  |  |
| 5 | `NOMBRE` | `nvarchar(50)` |  |  |
| 6 | `ACTIVO` | `nvarchar(1)` |  |  |
| 7 | `VENDEDOR` | `nvarchar(8)` |  |  |
| 8 | `VENTA` | `nvarchar(1)` |  |  |
| 9 | `FORANIA` | `nvarchar(1)` |  |  |
| 10 | `SUCURSAL` | `nvarchar(10)` |  |  |
| 11 | `TIPO` | `nvarchar(10)` |  |  |
| 12 | `SUBTIPO` | `nvarchar(10)` |  |  |
| 13 | `BODEGA` | `nvarchar(15)` |  |  |
| 14 | `SUBBODEGA` | `nvarchar(15)` |  |  |
| 15 | `DESCUENTO` | `nvarchar(1)` |  |  |
| 16 | `BONIF` | `nvarchar(1)` |  |  |
| 17 | `KILOMETRAJE` | `nvarchar(1)` |  |  |
| 18 | `IMPRESION` | `nvarchar(1)` |  |  |
| 19 | `RECIBOPROPIO` | `nvarchar(1)` |  |  |
| 20 | `CELULAR` | `nvarchar(1)` |  |  |
| 21 | `RENTABIL` | `nvarchar(1)` |  |  |
| 22 | `OFERTA` | `nvarchar(1)` |  |  |
| 23 | `PERCRENT` | `float` |  |  |
| 24 | `PASARCREDITO` | `nvarchar(1)` |  |  |
| 25 | `TECLADO` | `nvarchar(1)` |  |  |
| 26 | `EDITDEVPREC` | `nvarchar(1)` |  |  |
| 27 | `EDITDESC` | `nvarchar(1)` |  |  |
| 28 | `PARAMS` | `nvarchar(25)` |  |  |
| 29 | `SEMANA` | `int` |  |  |
| 30 | `OBJANO` | `int` |  |  |
| 31 | `OBJMES` | `int` |  |  |
| 32 | `SYNCFOLD` | `nvarchar(200)` |  |  |
| 33 | `WLFOLD` | `nvarchar(100)` |  |  |
| 34 | `FTPFOLD` | `nvarchar(100)` |  |  |
| 35 | `EMAIL` | `nvarchar(35)` |  |  |
| 36 | `LASTIMP` | `int` |  |  |
| 37 | `LASTCOM` | `int` |  |  |
| 38 | `LASTEXP` | `int` |  |  |
| 39 | `IMPSTAT` | `nvarchar(1)` |  |  |
| 40 | `EXPSTAT` | `nvarchar(1)` |  |  |
| 41 | `COMSTAT` | `nvarchar(1)` |  |  |
| 42 | `PARAM1` | `nvarchar(15)` |  |  |
| 43 | `PARAM2` | `nvarchar(15)` |  |  |
| 44 | `PESOLIM` | `float` |  |  |
| 45 | `INTERVALO_MAX` | `int` |  |  |
| 46 | `LECTURAS_VALID` | `int` |  |  |
| 47 | `INTENTOS_LECT` | `int` |  |  |
| 48 | `HORA_INI` | `int` |  |  |
| 49 | `HORA_FIN` | `int` |  |  |
| 50 | `APLICACION_USA` | `int` |  |  |
| 51 | `PUERTO_GPS` | `int` |  |  |
| 52 | `ES_RUTA_OFICINA` | `bit` |  |  |
| 53 | `DILUIR_BON` | `bit` |  |  |
| 54 | `PREIMPRESION_FACTURA` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_ruta` | CLUSTERED · **PK** | IdRuta |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `trans_despacho_enc` (`FK_trans_despacho_enc_road_ruta`)
- `trans_tras_enc` (`FK_trans_tras_enc_road_ruta`)

## Quién la referencia

**11** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `v_trans_pedido` (view)
- `VW_Despacho` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Pedidos_List` (view)

