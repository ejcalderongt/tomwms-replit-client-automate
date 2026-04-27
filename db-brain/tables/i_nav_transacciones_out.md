---
id: db-brain-table-i-nav-transacciones-out
type: db-table
title: dbo.i_nav_transacciones_out
schema: dbo
name: i_nav_transacciones_out
kind: table
rows: 24193
modify_date: 2024-09-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_transacciones_out`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 24.193 |
| Schema modify_date | 2024-09-18 |
| Columnas | 61 |
| Índices | 2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idtransaccion` | `int` |  |  |
| 2 | `idempresa` | `int` | ✓ |  |
| 3 | `idbodega` | `int` |  |  |
| 4 | `idpropietario` | `int` | ✓ |  |
| 5 | `idpropietariobodega` | `int` | ✓ |  |
| 6 | `idordencompra` | `int` | ✓ |  |
| 7 | `idrecepcionenc` | `int` | ✓ |  |
| 8 | `idpedidoenc` | `int` | ✓ |  |
| 9 | `iddespachoenc` | `int` | ✓ |  |
| 10 | `idproductobodega` | `int` |  |  |
| 11 | `idproducto` | `int` | ✓ |  |
| 12 | `idunidadmedida` | `int` | ✓ |  |
| 13 | `idpresentacion` | `int` | ✓ |  |
| 14 | `idproductoestado` | `int` | ✓ |  |
| 15 | `cantidad` | `float` |  |  |
| 16 | `peso` | `float` | ✓ |  |
| 17 | `lote` | `nvarchar(50)` | ✓ |  |
| 18 | `fecha_vence` | `date` | ✓ |  |
| 19 | `fecha_recepcion` | `date` | ✓ |  |
| 20 | `no_pedido` | `nvarchar(50)` | ✓ |  |
| 21 | `no_linea` | `nvarchar(50)` | ✓ |  |
| 22 | `codigo_producto` | `nvarchar(50)` |  |  |
| 23 | `nombre_producto` | `nvarchar(150)` | ✓ |  |
| 24 | `codigo_variante` | `nvarchar(50)` | ✓ |  |
| 25 | `unidad_medida` | `nvarchar(50)` | ✓ |  |
| 26 | `tipo_transaccion` | `nvarchar(50)` | ✓ |  |
| 27 | `enviado` | `int` | ✓ |  |
| 28 | `fec_agr` | `datetime` | ✓ |  |
| 29 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 30 | `fec_mod` | `datetime` | ✓ |  |
| 31 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 32 | `Cantidad_Esperada` | `float` | ✓ |  |
| 33 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 34 | `uds_lic_plate` | `float` | ✓ |  |
| 35 | `cantidad_presentacion` | `float` | ✓ |  |
| 36 | `tipo_documento` | `nvarchar(25)` | ✓ |  |
| 37 | `observacion` | `nvarchar(50)` | ✓ |  |
| 38 | `empresa_transporte` | `nvarchar(50)` | ✓ |  |
| 39 | `piloto_transporte` | `nvarchar(50)` | ✓ |  |
| 40 | `contacto_recibe` | `nvarchar(50)` | ✓ |  |
| 41 | `contacto_entrega` | `nvarchar(50)` | ✓ |  |
| 42 | `marchamo_no` | `nvarchar(50)` | ✓ |  |
| 43 | `IdTipoDocumento` | `int` | ✓ |  |
| 44 | `es_servicio` | `bit` | ✓ |  |
| 45 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 46 | `valor_aduana` | `float` | ✓ |  |
| 47 | `valor_fob` | `float` | ✓ |  |
| 48 | `valor_iva` | `float` | ✓ |  |
| 49 | `valor_dai` | `float` | ✓ |  |
| 50 | `valor_seguro` | `float` | ✓ |  |
| 51 | `valor_flete` | `float` | ✓ |  |
| 52 | `peso_neto` | `float` | ✓ |  |
| 53 | `peso_bruto` | `float` | ✓ |  |
| 54 | `fecha_despacho` | `datetime` | ✓ |  |
| 55 | `no_documento_salida_ref_devol` | `nvarchar(50)` | ✓ |  |
| 56 | `IdPedidoEncDevol` | `int` | ✓ |  |
| 57 | `IdDespachoDet` | `int` | ✓ |  |
| 58 | `IdRecepcionDet` | `int` | ✓ |  |
| 59 | `cantidad_enviada` | `float` | ✓ |  |
| 60 | `cantidad_pendiente` | `float` | ✓ |  |
| 61 | `auditar` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_transacciones_out` | CLUSTERED · **PK** | idtransaccion |
| `NCLI_I_NAV_TRANSACCIONES_OUT_20230221_EJC` | NONCLUSTERED | idpedidoenc, IdDespachoDet, iddespachoenc, lic_plate |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

