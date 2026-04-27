---
id: db-brain-table-trans-re-enc
type: db-table
title: dbo.trans_re_enc
schema: dbo
name: trans_re_enc
kind: table
rows: 576
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 576 |
| Schema modify_date | 2024-02-01 |
| Columnas | 37 |
| Índices | 1 |
| FKs | out:3 in:7 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionEnc` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` | ✓ |  |
| 3 | `IdMuelle` | `int` | ✓ |  |
| 4 | `IdUbicacionRecepcion` | `int` | ✓ |  |
| 5 | `IdTipoTransaccion` | `nvarchar(50)` |  |  |
| 6 | `fecha_recepcion` | `datetime` | ✓ |  |
| 7 | `hora_ini_pc` | `datetime` | ✓ |  |
| 8 | `hora_fin_pc` | `datetime` | ✓ |  |
| 9 | `muestra_precio` | `bit` | ✓ |  |
| 10 | `estado` | `nvarchar(20)` | ✓ |  |
| 11 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 12 | `fec_agr` | `datetime` | ✓ |  |
| 13 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 14 | `fec_mod` | `datetime` | ✓ |  |
| 15 | `fecha_tarea` | `datetime` | ✓ |  |
| 16 | `tomar_fotos` | `bit` | ✓ |  |
| 17 | `escanear_rec_ubic` | `bit` | ✓ |  |
| 18 | `para_por_codigo` | `bit` | ✓ |  |
| 19 | `observacion` | `nvarchar(100)` | ✓ |  |
| 20 | `firma_piloto` | `image` | ✓ |  |
| 21 | `activo` | `bit` | ✓ |  |
| 22 | `NoGuia` | `nvarchar(50)` | ✓ |  |
| 23 | `CorreoEnviado` | `bit` | ✓ |  |
| 24 | `Revision_Inconsistencia` | `bit` | ✓ |  |
| 25 | `bloqueada` | `bit` | ✓ |  |
| 26 | `bloqueada_por` | `nvarchar(2)` | ✓ |  |
| 27 | `idusuariobloqueo` | `int` | ✓ |  |
| 28 | `idmotivoanulacionbodega` | `int` | ✓ |  |
| 29 | `Habilitar_Stock` | `bit` |  |  |
| 30 | `idvehiculo` | `int` | ✓ |  |
| 31 | `idpiloto` | `int` | ✓ |  |
| 32 | `No_Marchamo` | `nvarchar(50)` | ✓ |  |
| 33 | `mostrar_cantidad_esperada` | `bit` | ✓ |  |
| 34 | `IdBodega` | `int` | ✓ |  |
| 35 | `carta_cupo` | `nvarchar(50)` | ✓ |  |
| 36 | `no_contenedor` | `nvarchar(50)` | ✓ |  |
| 37 | `IdEstado_defecto_recepcion` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_recepcion_enc` | CLUSTERED · **PK** | IdRecepcionEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_re_enc_trans_re_tr` → `trans_re_tr`
- `FK_trans_recepcion_enc_bodega_muelles` → `bodega_muelles`
- `FK_trans_recepcion_enc_propietario_bodega` → `propietario_bodega`

### Entrantes (otra tabla → esta)

- `producto_pallet` (`FK_producto_pallet_trans_re_enc`)
- `trans_re_det_infraccion` (`FK_trans_re_det_infraccion_trans_re_enc`)
- `trans_re_det` (`FK_trans_recepcion_det_trans_recepcion_enc`)
- `trans_re_fact` (`FK_trans_re_fact_trans_re_enc`)
- `trans_re_img` (`FK_trans_recepcion_img_trans_recepcion_enc`)
- `trans_re_oc` (`FK_trans_recepcion_oc_trans_recepcion_enc`)
- `trans_re_op` (`FK_trans_recepcion_operadores_trans_recepcion_enc`)

## Quién la referencia

**35** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Get_Detalle_By_IdRecepcionEnc` (view)
- `VW_Indicador_Ingresos` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_N1` (view)
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
- `VW_Stock_Jornada` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)
- `VW_Tareas_Activas_HH` (view)
- `VW_Tiempos_ingreso` (view)
- `VW_Tiempos_Ingreso_Operador` (view)
- `VW_TMSTickets_Sin_Retroactivo` (view)
- `VW_Valorizacion_OC` (view)

