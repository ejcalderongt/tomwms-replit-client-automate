---
id: db-brain-table-trans-re-tr
type: db-table
title: dbo.trans_re_tr
schema: dbo
name: trans_re_tr
kind: table
rows: 10
modify_date: 2018-10-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_tr`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 10 |
| Schema modify_date | 2018-10-11 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoTransaccion` | `nvarchar(50)` |  |  |
| 2 | `Descripcion` | `nvarchar(50)` |  |  |
| 3 | `Funcionalidad` | `text` | ✓ |  |
| 4 | `UsaHH` | `bit` | ✓ |  |
| 5 | `DescDev` | `text` | ✓ |  |
| 6 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 7 | `ConRef` | `bit` | ✓ |  |
| 8 | `Activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_re_tr` | CLUSTERED · **PK** | IdTipoTransaccion |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `trans_re_enc` (`FK_trans_re_enc_trans_re_tr`)

## Quién la referencia

**17** objetos:

- `CLBD` (stored_procedure)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_CONOC_FIN` (view)
- `VW_REC_SIN_OC` (view)
- `VW_Recepcion` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Recepcion_For_HH_By_IdBodega` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_RecepcionConOC` (view)
- `VW_RecepcionSinOC` (view)
- `VW_RecOC_MIX` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Reporte_Recepcion_20190727` (view)
- `VW_Stock_Jornada` (view)

