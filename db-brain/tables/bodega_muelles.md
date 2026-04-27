---
id: db-brain-table-bodega-muelles
type: db-table
title: dbo.bodega_muelles
schema: dbo
name: bodega_muelles
kind: table
rows: 8
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega_muelles`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 8 |
| Schema modify_date | 2024-07-02 |
| Columnas | 14 |
| Índices | 1 |
| FKs | out:1 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMuelle` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 4 | `nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `color` | `int` | ✓ |  |
| 10 | `imagen` | `image` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |
| 12 | `Entrada` | `bit` | ✓ |  |
| 13 | `Salida` | `bit` | ✓ |  |
| 14 | `IdUbicacionDefecto` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_bodega_muelles_1` | CLUSTERED · **PK** | IdMuelle |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_bodega_muelles_bodega` → `bodega`

### Entrantes (otra tabla → esta)

- `tarea_hh` (`FK_tarea_hh_bodega_muelles`)
- `trans_pe_enc` (`FK_trans_pedido_enc_bodega_muelles`)
- `trans_re_enc` (`FK_trans_recepcion_enc_bodega_muelles`)
- `trans_tras_enc` (`FK_trans_tras_enc_bodega_muelles`)

## Quién la referencia

**14** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `v_trans_pedido` (view)
- `VW_BodegaMuelle` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Pedidos_List` (view)
- `VW_Picking` (view)
- `VW_Recepcion` (view)
- `VW_Recepcion_For_HH_By_IdBodega` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_tareas_hh` (view)
- `VW_Tareas_Picking_HH` (view)

