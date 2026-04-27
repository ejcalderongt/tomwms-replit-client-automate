---
id: db-brain-table-trans-oc-enc
type: db-table
title: dbo.trans_oc_enc
schema: dbo
name: trans_oc_enc
kind: table
rows: 558
modify_date: 2025-05-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 558 |
| Schema modify_date | 2025-05-05 |
| Columnas | 42 |
| Índices | 3 |
| FKs | out:5 in:5 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` |  |  |
| 3 | `IdProveedorBodega` | `int` |  |  |
| 4 | `IdTipoIngresoOC` | `int` | ✓ |  |
| 5 | `IdEstadoOC` | `int` | ✓ |  |
| 6 | `IdMotivoDevolucion` | `int` | ✓ |  |
| 7 | `Fecha_Creacion` | `datetime` | ✓ |  |
| 8 | `Hora_Creacion` | `datetime` | ✓ |  |
| 9 | `No_Documento` | `nvarchar(30)` | ✓ |  |
| 10 | `User_Agr` | `nvarchar(50)` | ✓ |  |
| 11 | `Fec_Agr` | `datetime` | ✓ |  |
| 12 | `User_Mod` | `nvarchar(50)` | ✓ |  |
| 13 | `Fec_Mod` | `datetime` | ✓ |  |
| 14 | `Procedencia` | `nvarchar(150)` | ✓ |  |
| 15 | `No_Marchamo` | `nvarchar(50)` | ✓ |  |
| 16 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 17 | `Observacion` | `text` | ✓ |  |
| 18 | `Control_Poliza` | `bit` | ✓ |  |
| 19 | `Activo` | `bit` | ✓ |  |
| 20 | `Fecha_Recepcion` | `datetime` | ✓ |  |
| 21 | `Hora_Inicio_Recepcion` | `datetime` | ✓ |  |
| 22 | `Hora_Fin_Recepcion` | `datetime` | ✓ |  |
| 23 | `IdMuelleRecepcion` | `int` | ✓ |  |
| 24 | `Programar_Recepcion` | `bit` | ✓ |  |
| 25 | `IdMotivoAnulacionBodega` | `int` | ✓ |  |
| 26 | `Enviado_A_ERP` | `bit` | ✓ |  |
| 27 | `serie` | `nvarchar(25)` | ✓ |  |
| 28 | `correlativo` | `int` | ✓ |  |
| 29 | `IdDespachoEnc` | `int` | ✓ |  |
| 30 | `no_ticket_tms` | `int` | ✓ |  |
| 31 | `IdNoDocumentoRef` | `int` | ✓ |  |
| 32 | `idacuerdocomercial` | `int` | ✓ |  |
| 33 | `IdOperadorBodegaDefecto` | `int` | ✓ |  |
| 34 | `IdBodega` | `int` | ✓ |  |
| 35 | `no_documento_recepcion_erp` | `nvarchar(50)` | ✓ |  |
| 36 | `No_Documento_Devolucion` | `nvarchar(50)` | ✓ |  |
| 37 | `IdPedidoEncDevolucion` | `int` | ✓ |  |
| 38 | `push_to_nav` | `bit` | ✓ |  |
| 39 | `no_documento_ubicacion_erp` | `nvarchar(50)` | ✓ |  |
| 40 | `PutAway_Registrado` | `bit` | ✓ |  |
| 41 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |
| 42 | `IdCampaña` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_orden_compra_enc` | CLUSTERED · **PK** | IdOrdenCompraEnc |
| `NCLI_20210920_Trans_Oc_Enc_EJC` | NONCLUSTERED | IdOrdenCompraEnc, IdPropietarioBodega, IdProveedorBodega, IdEstadoOC, Fecha_Creacion, No_Documento, IdTipoIngresoOC |
| `NCL_trans_oc_enc_20240306` | NONCLUSTERED | IdTipoIngresoOC, IdEstadoOC, Fecha_Creacion, IdBodega, IdPropietarioBodega, Activo |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_oc_enc_motivo_devolucion` → `motivo_devolucion`
- `FK_trans_oc_enc_propietario_bodega` → `propietario_bodega`
- `FK_trans_oc_enc_proveedor` → `proveedor_bodega`
- `FK_trans_oc_enc_trans_oc_estado` → `trans_oc_estado`
- `FK_trans_oc_enc_trans_oc_ti` → `trans_oc_ti`

### Entrantes (otra tabla → esta)

- `trans_oc_det` (`FK_trans_orden_compra_det_trans_orden_compra_enc`)
- `trans_oc_imagen` (`FK_trans_orden_compra_imagen_trans_orden_compra_enc`)
- `trans_re_det_infraccion` (`FK_trans_re_det_infraccion_trans_oc_enc`)
- `trans_re_oc` (`FK_trans_recepcion_oc_trans_orden_compra_enc`)
- `trans_servicio_enc` (`FK_servicio_enc_trans_oc_enc`)

## Quién la referencia

**51** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Cantidad_Ingresos_Proveedor` (view)
- `VW_CodigoBarra_OC` (view)
- `VW_Doc_Con_Diferencias` (view)
- `VW_Estado_Envios_Nav` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_historico` (view)
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `VW_Indicador_Ingresos` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Ingresos_Sin_Ticket` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Movimientos_Retroactivos` (view)
- `VW_OrdenCompra` (view)
- `VW_OrdenCompraPreIngreso` (view)
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
- `VW_RecepcionSinOC` (view)
- `VW_RecOC_MIX` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Reporte_Recepcion_20190727` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Stock_Transito` (view)
- `VW_Tareas_Activas_HH` (view)
- `VW_Tiempos_ingreso` (view)
- `VW_Tiempos_Ingreso_Operador` (view)
- `VW_TMSTickets_Sin_Retroactivo` (view)
- `VW_TRANS_OC_DET` (view)
- `VW_Trans_Servicios` (view)

