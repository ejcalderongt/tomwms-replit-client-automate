---
id: db-brain-table-trans-manufactura-enc
type: db-table
title: dbo.trans_manufactura_enc
schema: dbo
name: trans_manufactura_enc
kind: table
rows: 0
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_manufactura_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-07-02 |
| Columnas | 15 |
| Índices | 1 |
| FKs | out:4 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdManufacturaEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` | ✓ |  |
| 4 | `IdTipoManufactura` | `int` | ✓ |  |
| 5 | `IdPedidoEnc` | `int` | ✓ |  |
| 6 | `fecha_manufactura` | `datetime` | ✓ |  |
| 7 | `hora_ini` | `datetime` | ✓ |  |
| 8 | `hora_fin` | `datetime` | ✓ |  |
| 9 | `estado` | `nvarchar(20)` | ✓ |  |
| 10 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 13 | `fec_mod` | `datetime` | ✓ |  |
| 14 | `escaneo` | `bit` |  |  |
| 15 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_manufactura_enc` | CLUSTERED · **PK** | IdManufacturaEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_manufactura_enc_bodega` → `bodega`
- `FK_trans_manufactura_enc_propietario_bodega` → `propietario_bodega`
- `FK_trans_manufactura_enc_trans_manufactura_tipo` → `trans_manufactura_tipo`
- `FK_trans_manufactura_enc_trans_pe_enc` → `trans_pe_enc`

### Entrantes (otra tabla → esta)

- `trans_manufactura_det` (`FK_trans_manufactura_det_trans_manufactura_enc`)
- `trans_manufactura_picking` (`FK_trans_manufactura_picking_trans_manufactura_enc`)

## Quién la referencia

**4** objetos:

- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Consolidada_LFV` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)

