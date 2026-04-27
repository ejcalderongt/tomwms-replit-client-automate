---
id: db-brain-table-stock-jornada
type: db-table
title: dbo.stock_jornada
schema: dbo
name: stock_jornada
kind: table
rows: 0
modify_date: 2022-10-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_jornada`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-10-18 |
| Columnas | 83 |
| Índices | 9 |
| FKs | out:13 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockJornada` | `int` |  |  |
| 2 | `IdJornadaSistema` | `int` | ✓ |  |
| 3 | `Fecha` | `date` | ✓ |  |
| 4 | `IdBodega` | `int` | ✓ |  |
| 5 | `IdStock` | `int` | ✓ |  |
| 6 | `IdPropietarioBodega` | `int` | ✓ |  |
| 7 | `IdProductoBodega` | `int` | ✓ |  |
| 8 | `IdProductoEstado` | `int` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `IdUnidadMedida` | `int` | ✓ |  |
| 11 | `IdUbicacion` | `int` | ✓ |  |
| 12 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 13 | `IdRecepcionEnc` | `int` | ✓ |  |
| 14 | `IdRecepcionDet` | `int` | ✓ |  |
| 15 | `IdPedidoEnc` | `int` | ✓ |  |
| 16 | `IdPickingEnc` | `int` | ✓ |  |
| 17 | `IdDespachoEnc` | `int` | ✓ |  |
| 18 | `lote` | `nvarchar(50)` | ✓ |  |
| 19 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 20 | `serial` | `nvarchar(50)` | ✓ |  |
| 21 | `cantidad` | `float` | ✓ |  |
| 22 | `fecha_ingreso` | `datetime` | ✓ |  |
| 23 | `fecha_vence` | `datetime` | ✓ |  |
| 24 | `uds_lic_plate` | `float` | ✓ |  |
| 25 | `no_bulto` | `int` | ✓ |  |
| 26 | `fecha_manufactura` | `datetime` | ✓ |  |
| 27 | `añada` | `int` | ✓ |  |
| 28 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 29 | `fec_agr` | `datetime` | ✓ |  |
| 30 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 31 | `fec_mod` | `datetime` | ✓ |  |
| 32 | `activo` | `bit` | ✓ |  |
| 33 | `peso` | `float` | ✓ |  |
| 34 | `temperatura` | `float` | ✓ |  |
| 35 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 36 | `pallet_no_estandar` | `bit` | ✓ |  |
| 37 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 38 | `Proveedor` | `nvarchar(100)` | ✓ |  |
| 39 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 40 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 41 | `No_DocumentoOC` | `nvarchar(30)` | ✓ |  |
| 42 | `No_DocumentoRec` | `nvarchar(50)` | ✓ |  |
| 43 | `ReferenciaOC` | `nvarchar(100)` | ✓ |  |
| 44 | `Fecha_Recepcion` | `datetime` | ✓ |  |
| 45 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 46 | `Fecha_Agrego` | `datetime` | ✓ |  |
| 47 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 48 | `codigo_barra_producto` | `nvarchar(35)` | ✓ |  |
| 49 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 50 | `existencia` | `float` | ✓ |  |
| 51 | `nom_umBas` | `nvarchar(50)` | ✓ |  |
| 52 | `nom_estado_producto` | `nvarchar(50)` | ✓ |  |
| 53 | `nom_presentacion_producto` | `nvarchar(50)` | ✓ |  |
| 54 | `ubicacion_origen` | `nvarchar(200)` | ✓ |  |
| 55 | `no_poliza` | `nvarchar(50)` | ✓ |  |
| 56 | `valor_aduana` | `float` | ✓ |  |
| 57 | `valor_fob` | `float` | ✓ |  |
| 58 | `valor_iva` | `float` | ✓ |  |
| 59 | `valor_dai` | `float` | ✓ |  |
| 60 | `valor_seguro` | `float` | ✓ |  |
| 61 | `valor_flete` | `float` | ✓ |  |
| 62 | `peso_neto` | `float` | ✓ |  |
| 63 | `numero_orden` | `nvarchar(50)` | ✓ |  |
| 64 | `codigo_regimen` | `nvarchar(20)` | ✓ |  |
| 65 | `nombre_regimen` | `nvarchar(500)` | ✓ |  |
| 66 | `dias_vencimiento_regimen` | `int` | ✓ |  |
| 67 | `fecha_ingreso_ticket_tms` | `datetime` | ✓ |  |
| 68 | `es_retroactivo` | `bit` | ✓ |  |
| 69 | `factor` | `float` | ✓ |  |
| 70 | `CamasPorTarima` | `float` | ✓ |  |
| 71 | `CajasPorCama` | `float` | ✓ |  |
| 72 | `IdTicketTMS` | `int` | ✓ |  |
| 73 | `posiciones` | `int` |  |  |
| 74 | `IdPropietario` | `int` | ✓ |  |
| 75 | `IdClasificacion` | `int` | ✓ |  |
| 76 | `Clasificacion` | `nvarchar(100)` | ✓ |  |
| 77 | `Regimen` | `nvarchar(20)` | ✓ |  |
| 78 | `Stock_Jornada_Hash` | `nvarchar(150)` | ✓ |  |
| 79 | `Cantidad_Ingreso_Afecta_A_Salida` | `float` | ✓ |  |
| 80 | `costo_unitario` | `float` | ✓ |  |
| 81 | `no_documento_procesado_erp` | `nvarchar(50)` | ✓ |  |
| 82 | `procesado_erp` | `bit` | ✓ |  |
| 83 | `fecha_procesado_stock_jornada` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_jornada` | CLUSTERED · **PK** | IdStockJornada |
| `<Name of Missing Index, sysname,>` | NONCLUSTERED | existencia, Clasificacion, IdPropietario, IdClasificacion, Regimen, Fecha |
| `IndexSJ_20210924_1` | NONCLUSTERED | No_DocumentoOC, nombre_producto, existencia, nom_umBas, numero_orden, codigo_producto, factor, IdPropietario, Regimen, Fecha |
| `IndexSJ_20210924` | NONCLUSTERED | nombre_producto, existencia, nom_umBas, No_DocumentoOC, codigo_producto, numero_orden, IdPropietario, Regimen, Fecha |
| `NCLI_stock_jornada_20211007_EJC` | NONCLUSTERED | IdBodega, Proveedor, nombre_producto, valor_aduana, valor_fob, valor_iva, valor_dai, valor_seguro, valor_flete, fecha_ingreso_ticket_tms, Regimen, activo |
| `NCLI_stock_jornada_20211007_EJCA` | NONCLUSTERED | Proveedor, nombre_producto, valor_aduana, valor_fob, valor_iva, valor_dai, valor_seguro, valor_flete, fecha_ingreso_ticket_tms, Regimen, IdBodega, activo |
| `IX_Stock_Jornada_20221001` | NONCLUSTERED | Fecha, lic_plate |
| `NCI_stock_jornada_20221006` | NONCLUSTERED | IdTicketTMS |
| `NCI_Stock_Jornada_20221017` | NONCLUSTERED | IdJornadaSistema, IdBodega, IdStock, lic_plate, IdTicketTMS, Fecha |

## Check constraints

- `stock_jornada_NonNegative_20200115_EJC`: `([Cantidad]>(0))`

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_stock_jornada_producto_bodega` → `producto_bodega`
- `FK_stock_jornada_producto_bodega_rec` → `producto_bodega`
- `FK_stock_jornada_producto_estado` → `producto_estado`
- `FK_stock_jornada_producto_estado_rec` → `producto_estado`
- `FK_stock_jornada_producto_presentacion` → `producto_presentacion`
- `FK_stock_jornada_producto_presentacion_rec` → `producto_presentacion`
- `FK_stock_jornada_propietario_bodega` → `propietario_bodega`
- `FK_stock_jornada_propietario_bodega_rec` → `propietario_bodega`
- `FK_stock_jornada_trans_pe_enc` → `trans_pe_enc`
- `FK_stock_jornada_trans_pe_enc_rec` → `trans_pe_enc`
- `FK_stock_jornada_trans_re_det` → `trans_re_det`
- `FK_stock_jornada_unidad_medida` → `unidad_medida`
- `FK_stock_jornada_unidad_medida_rec` → `unidad_medida`

## Quién la referencia

**10** objetos:

- `SP_STOCK_JORNADA_DESFASE` (stored_procedure)
- `SP_STOCK_JORNADA_DESFASE_RETROACTIVO` (stored_procedure)
- `stock_jornada_NonNegative_20200115_EJC` (check_constraint)
- `VW_Fiscal_historico` (view)
- `VW_Fiscal_Valorización` (view)
- `VW_Ingresos_Sin_Ticket` (view)
- `VW_Movimientos_Retroactivos` (view)
- `VW_Stock_res_jornada` (view)
- `VW_Stock_res_jornada_merca` (view)
- `VW_TMSTickets_Sin_Retroactivo` (view)

