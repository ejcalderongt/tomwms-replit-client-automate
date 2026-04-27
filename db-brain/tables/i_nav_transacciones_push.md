---
id: db-brain-table-i-nav-transacciones-push
type: db-table
title: dbo.i_nav_transacciones_push
schema: dbo
name: i_nav_transacciones_push
kind: table
rows: 0
modify_date: 2022-01-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_transacciones_push`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-01-25 |
| Columnas | 38 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTransaccionPush` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` |  |  |
| 4 | `IdPropietariobodega` | `int` | ✓ |  |
| 5 | `IdOrdenCompra` | `int` | ✓ |  |
| 6 | `IdRecepcionEnc` | `int` | ✓ |  |
| 7 | `IdRecepcionDet` | `int` | ✓ |  |
| 8 | `Idproductobodega` | `int` |  |  |
| 9 | `Idproducto` | `int` | ✓ |  |
| 10 | `Idunidadmedida` | `int` | ✓ |  |
| 11 | `Idpresentacion` | `int` | ✓ |  |
| 12 | `Idproductoestado` | `int` | ✓ |  |
| 13 | `cantidad` | `float` |  |  |
| 14 | `peso` | `float` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` | ✓ |  |
| 16 | `fecha_vence` | `date` | ✓ |  |
| 17 | `no_linea` | `nvarchar(50)` | ✓ |  |
| 18 | `codigo_variante` | `nvarchar(50)` | ✓ |  |
| 19 | `nom_unidad_medida` | `nvarchar(50)` | ✓ |  |
| 20 | `tipo_transaccion` | `nvarchar(50)` | ✓ |  |
| 21 | `IdTipoDocumento` | `int` | ✓ |  |
| 22 | `tipo_push` | `nvarchar(50)` | ✓ |  |
| 23 | `no_recepcion_almacen` | `nvarchar(50)` | ✓ |  |
| 24 | `documento_ubicacion` | `nvarchar(50)` | ✓ |  |
| 25 | `documento_ingreso` | `nvarchar(50)` | ✓ |  |
| 26 | `documento_recepcion` | `nvarchar(50)` | ✓ |  |
| 27 | `location_code` | `nvarchar(50)` | ✓ |  |
| 28 | `zone_code` | `nvarchar(50)` | ✓ |  |
| 29 | `bin_code` | `nvarchar(50)` | ✓ |  |
| 30 | `assigne_user_id` | `nvarchar(50)` | ✓ |  |
| 31 | `item_no` | `nvarchar(50)` | ✓ |  |
| 32 | `no_orden_prod` | `nvarchar(50)` | ✓ |  |
| 33 | `respuesta_push` | `nvarchar(500)` | ✓ |  |
| 34 | `Enviado_A_ERP` | `bit` | ✓ |  |
| 35 | `fec_agr` | `datetime` | ✓ |  |
| 36 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 37 | `fec_mod` | `datetime` | ✓ |  |
| 38 | `user_mod` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_transacciones_push` | CLUSTERED · **PK** | IdTransaccionPush |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

