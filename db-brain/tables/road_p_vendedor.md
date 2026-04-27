---
id: db-brain-table-road-p-vendedor
type: db-table
title: dbo.road_p_vendedor
schema: dbo
name: road_p_vendedor
kind: table
rows: 0
modify_date: 2016-05-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.road_p_vendedor`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-05-02 |
| Columnas | 15 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRuta` | `int` |  |  |
| 2 | `IdVendedor` | `int` |  |  |
| 3 | `codigo` | `nvarchar(8)` |  |  |
| 4 | `nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `clave` | `nvarchar(15)` | ✓ |  |
| 6 | `ruta` | `nvarchar(15)` | ✓ |  |
| 7 | `nivel` | `int` | ✓ |  |
| 8 | `nivelprecio` | `int` | ✓ |  |
| 9 | `bodega` | `nvarchar(15)` | ✓ |  |
| 10 | `subbodega` | `nvarchar(15)` | ✓ |  |
| 11 | `cod_vehiculo` | `nvarchar(15)` |  |  |
| 12 | `liquidando` | `nvarchar(50)` |  |  |
| 13 | `ultima_fecha_liq` | `datetime` |  |  |
| 14 | `bloqueado` | `bit` |  |  |
| 15 | `devolucion_sap` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_P_VENDEDOR` | CLUSTERED · **PK** | IdRuta, IdVendedor, codigo |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `v_trans_pedido` (view)
- `VW_Pedidos_List` (view)

