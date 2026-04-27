---
id: db-brain-table-trans-oc-ti
type: db-table
title: dbo.trans_oc_ti
schema: dbo
name: trans_oc_ti
kind: table
rows: 15
modify_date: 2025-04-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_ti`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 15 |
| Schema modify_date | 2025-04-01 |
| Columnas | 24 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoIngresoOC` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `es_devolucion` | `bit` | ✓ |  |
| 4 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |
| 9 | `control_poliza` | `bit` | ✓ |  |
| 10 | `requerir_documento_ref` | `bit` | ✓ |  |
| 11 | `es_poliza_consolidada` | `bit` | ✓ |  |
| 12 | `genera_tarea_ingreso` | `bit` | ✓ |  |
| 13 | `requerir_proveedor_es_bodega_wms` | `bit` | ✓ |  |
| 14 | `requerir_documento_ref_wms` | `bit` | ✓ |  |
| 15 | `requerir_ubic_rec_ingreso` | `bit` | ✓ |  |
| 16 | `exigir_campo_referencia` | `bit` |  |  |
| 17 | `marcar_registros_enviados_mi3` | `bit` |  |  |
| 18 | `preguntar_en_backorder` | `bit` |  |  |
| 19 | `bloquear_lotes` | `bit` |  |  |
| 20 | `permitir_excedente_lotes` | `bit` |  |  |
| 21 | `es_importacion` | `bit` |  |  |
| 22 | `permitir_vencido_ingreso` | `bit` |  |  |
| 23 | `IdProductoEstado` | `int` | ✓ |  |
| 24 | `IdPropietario` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_orden_compra_tipo_ingreso` | CLUSTERED · **PK** | IdTipoIngresoOC |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `trans_oc_enc` (`FK_trans_oc_enc_trans_oc_ti`)

## Quién la referencia

**12** objetos:

- `CLBD` (stored_procedure)
- `VW_Cantidad_Ingresos_Proveedor` (view)
- `VW_Doc_Con_Diferencias` (view)
- `VW_Indicador_Ingresos` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_OrdenCompra` (view)
- `VW_Recepcion_For_HH_By_IdBodega` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Transito` (view)
- `VW_Tiempos_ingreso` (view)
- `VW_Tiempos_Ingreso_Operador` (view)

