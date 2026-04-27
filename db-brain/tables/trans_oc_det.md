---
id: db-brain-table-trans-oc-det
type: db-table
title: dbo.trans_oc_det
schema: dbo
name: trans_oc_det
kind: table
rows: 1907
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.907 |
| Schema modify_date | 2024-07-02 |
| Columnas | 38 |
| Índices | 3 |
| FKs | out:6 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` |  |  |
| 2 | `IdOrdenCompraDet` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdArancel` | `int` | ✓ |  |
| 5 | `IdPresentacion` | `int` | ✓ |  |
| 6 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 7 | `IdMotivoDevolucion` | `int` | ✓ |  |
| 8 | `No_Linea` | `int` | ✓ |  |
| 9 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 10 | `nombre_presentacion` | `nvarchar(50)` | ✓ |  |
| 11 | `nombre_arancel` | `nvarchar(50)` | ✓ |  |
| 12 | `porcentaje_arancel` | `float` | ✓ |  |
| 13 | `nombre_unidad_medida_basica` | `nvarchar(50)` | ✓ |  |
| 14 | `cantidad` | `float` | ✓ |  |
| 15 | `cantidad_recibida` | `float` | ✓ |  |
| 16 | `costo` | `float` | ✓ |  |
| 17 | `total_linea` | `float` | ✓ |  |
| 18 | `user_agr` | `nvarchar(50)` |  |  |
| 19 | `fec_agr` | `datetime` |  |  |
| 20 | `user_mod` | `nvarchar(50)` |  |  |
| 21 | `fec_mod` | `datetime` |  |  |
| 23 | `activo` | `bit` |  |  |
| 24 | `peso` | `float` | ✓ |  |
| 25 | `peso_recibido` | `float` | ✓ |  |
| 26 | `atributo_variante_1` | `nvarchar(50)` | ✓ |  |
| 27 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 28 | `valor_aduana` | `float` | ✓ |  |
| 29 | `valor_fob` | `float` | ✓ |  |
| 30 | `valor_iva` | `float` | ✓ |  |
| 31 | `valor_dai` | `float` | ✓ |  |
| 32 | `valor_seguro` | `float` | ✓ |  |
| 33 | `valor_flete` | `float` | ✓ |  |
| 34 | `peso_neto` | `float` | ✓ |  |
| 35 | `peso_bruto` | `float` | ✓ |  |
| 36 | `IdPropietarioBodega` | `int` | ✓ |  |
| 37 | `nombre_propietario` | `nvarchar(150)` | ✓ |  |
| 38 | `IdOrdenCompraDetPadre` | `int` | ✓ |  |
| 39 | `IdEmbarcador` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_orden_compra_det` | CLUSTERED · **PK** | IdOrdenCompraDet, IdOrdenCompraEnc |
| `NCLI_Trans_Oc_Det_IdProductoBodega_20210601EJC` | NONCLUSTERED | IdProductoBodega |
| `NCLI_TRANS_OC_DET_20210920_ENC` | NONCLUSTERED | cantidad, cantidad_recibida, activo, codigo_producto, IdProductoBodega, IdPresentacion, IdUnidadMedidaBasica, nombre_producto, IdOrdenCompraEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_oc_det_motivo_devolucion` → `motivo_devolucion`
- `FK_trans_oc_det_producto_bodega` → `producto_bodega`
- `FK_trans_orden_compra_det_Arancel` → `arancel`
- `FK_trans_orden_compra_det_producto_presentacion` → `producto_presentacion`
- `FK_trans_orden_compra_det_trans_orden_compra_enc` → `trans_oc_enc`
- `FK_trans_orden_compra_det_unidad_medida` → `unidad_medida`

## Quién la referencia

**26** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_CodigoBarra_OC` (view)
- `VW_Doc_Con_Diferencias` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_Fiscal_historico` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_OrdenCompraPreIngreso` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_CONOC_FIN` (view)
- `VW_RecepcionConOC` (view)
- `VW_RecepcionCostoArancel` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Stock_Transito` (view)
- `VW_TRANS_OC_DET` (view)
- `VW_Valorizacion_OC` (view)

