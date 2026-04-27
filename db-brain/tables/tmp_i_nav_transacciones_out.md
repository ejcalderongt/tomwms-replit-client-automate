---
id: db-brain-table-tmp-i-nav-transacciones-out
type: db-table
title: dbo.tmp_i_nav_transacciones_out
schema: dbo
name: tmp_i_nav_transacciones_out
kind: table
rows: 0
modify_date: 2019-12-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tmp_i_nav_transacciones_out`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2019-12-11 |
| Columnas | 35 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idtransaccion` | `int` |  |  |
| 2 | `idempresa` | `int` | ✓ |  |
| 3 | `idbodega` | `int` | ✓ |  |
| 4 | `idpropietario` | `int` | ✓ |  |
| 5 | `idpropietariobodega` | `int` | ✓ |  |
| 6 | `idordencompra` | `int` | ✓ |  |
| 7 | `idrecepcionenc` | `int` | ✓ |  |
| 8 | `idpedidoenc` | `int` | ✓ |  |
| 9 | `iddespachoenc` | `int` | ✓ |  |
| 10 | `idproductobodega` | `int` | ✓ |  |
| 11 | `idproducto` | `int` | ✓ |  |
| 12 | `idunidadmedida` | `int` | ✓ |  |
| 13 | `idpresentacion` | `int` | ✓ |  |
| 14 | `idproductoestado` | `int` | ✓ |  |
| 15 | `cantidad` | `float` | ✓ |  |
| 16 | `peso` | `float` | ✓ |  |
| 17 | `lote` | `nvarchar(50)` | ✓ |  |
| 18 | `fecha_vence` | `date` | ✓ |  |
| 19 | `fecha_recepcion` | `date` | ✓ |  |
| 20 | `no_pedido` | `nvarchar(50)` | ✓ |  |
| 21 | `no_linea` | `nvarchar(50)` | ✓ |  |
| 22 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
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

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

