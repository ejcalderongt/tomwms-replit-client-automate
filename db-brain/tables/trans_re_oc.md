---
id: db-brain-table-trans-re-oc
type: db-table
title: dbo.trans_re_oc
schema: dbo
name: trans_re_oc
kind: table
rows: 576
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_oc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 576 |
| Schema modify_date | 2023-08-21 |
| Columnas | 11 |
| Índices | 2 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionOc` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `IdOrdenCompraEnc` | `int` |  |  |
| 4 | `recepcion_ciega` | `bit` | ✓ |  |
| 5 | `recepcion_manual` | `bit` | ✓ |  |
| 6 | `no_docto` | `nvarchar(100)` | ✓ |  |
| 7 | `hora_ini_hh` | `datetime` | ✓ |  |
| 8 | `hora_fin_hh` | `datetime` | ✓ |  |
| 9 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `firma_operador` | `image` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_recepcion_oc` | CLUSTERED · **PK** | IdRecepcionOc, IdRecepcionEnc |
| `NCLI_TRANS_RE_OC_20210210_EJC` | NONCLUSTERED | IdRecepcionEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_recepcion_oc_trans_orden_compra_enc` → `trans_oc_enc`
- `FK_trans_recepcion_oc_trans_recepcion_enc` → `trans_re_enc`

## Quién la referencia

**48** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Estado_Envios_Nav` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_historico` (view)
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `VW_Indicador_Ingresos` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_MovimientosDetalle` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_CONOC_FIN` (view)
- `VW_REC_SIN_OC` (view)
- `VW_Recepcion` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Recepcion_For_HH_By_IdBodega` (view)
- `VW_Recepcion_For_HH_By_IdBodega_By_Operador` (view)
- `VW_RecepcionConOC` (view)
- `VW_RecepcionCostoArancel` (view)
- `VW_RecepcionesEncOC` (view)
- `VW_RecepcionSinOC` (view)
- `VW_RecOC_MIX` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Reporte_Recepcion_20190727` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)
- `VW_Tareas_Activas_HH` (view)
- `VW_Tiempos_ingreso` (view)
- `VW_Tiempos_Ingreso_Operador` (view)
- `VW_TMSTickets_Sin_Retroactivo` (view)
- `VW_Valorizacion_OC` (view)

