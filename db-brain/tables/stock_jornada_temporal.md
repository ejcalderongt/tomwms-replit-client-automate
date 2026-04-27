---
id: db-brain-table-stock-jornada-temporal
type: db-table
title: dbo.stock_jornada_temporal
schema: dbo
name: stock_jornada_temporal
kind: table
rows: 0
modify_date: 2023-04-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_jornada_temporal`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-04-17 |
| Columnas | 83 |

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
| 72 | `Cantidad_Ingreso_Afecta_A_Salida` | `float` | ✓ |  |
| 73 | `Stock_Jornada_Hash` | `nvarchar(150)` | ✓ |  |
| 74 | `IdTicketTMS` | `int` | ✓ |  |
| 75 | `fecha_procesado_stock_jornada` | `datetime` | ✓ |  |
| 76 | `IdPropietario` | `int` | ✓ |  |
| 77 | `IdClasificacion` | `int` | ✓ |  |
| 78 | `Clasificacion` | `nvarchar(100)` | ✓ |  |
| 79 | `Regimen` | `nvarchar(20)` | ✓ |  |
| 80 | `posiciones` | `int` |  |  |
| 81 | `costo_unitario` | `float` | ✓ |  |
| 82 | `procesado_erp` | `bit` | ✓ |  |
| 83 | `no_documento_procesado_erp` | `nvarchar(1)` | ✓ |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `SP_STOCK_JORNADA_DESFASE_RETROACTIVO` (stored_procedure)

