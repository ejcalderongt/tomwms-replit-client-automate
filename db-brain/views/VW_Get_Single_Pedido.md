---
id: db-brain-view-vw-get-single-pedido
type: db-view
title: dbo.VW_Get_Single_Pedido
schema: dbo
name: VW_Get_Single_Pedido
kind: view
modify_date: 2025-06-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Get_Single_Pedido`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-11 |
| Columnas | 83 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdCliente` | `int` | ✓ |  |
| 4 | `IdMuelle` | `int` | ✓ |  |
| 5 | `IdPropietarioBodega` | `int` | ✓ |  |
| 6 | `IdTipoPedido` | `int` | ✓ |  |
| 7 | `IdPickingEnc` | `int` | ✓ |  |
| 8 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 9 | `hora_ini` | `datetime` | ✓ |  |
| 10 | `hora_fin` | `datetime` | ✓ |  |
| 11 | `ubicacion` | `nvarchar(35)` | ✓ |  |
| 12 | `estado` | `nvarchar(20)` | ✓ |  |
| 13 | `no_despacho` | `bigint` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |
| 19 | `no_documento` | `bigint` | ✓ |  |
| 20 | `local` | `bit` | ✓ |  |
| 21 | `pallet_primero` | `bit` | ✓ |  |
| 22 | `dias_cliente` | `float` | ✓ |  |
| 23 | `anulado` | `bit` | ✓ |  |
| 24 | `RoadKilometraje` | `float` | ✓ |  |
| 25 | `RoadFechaEntr` | `datetime` | ✓ |  |
| 26 | `RoadDirEntrega` | `nvarchar(255)` | ✓ |  |
| 27 | `RoadTotal` | `float` | ✓ |  |
| 28 | `RoadDesMonto` | `float` | ✓ |  |
| 29 | `RoadImpMonto` | `float` | ✓ |  |
| 30 | `RoadPeso` | `float` | ✓ |  |
| 31 | `RoadBandera` | `nvarchar(5)` | ✓ |  |
| 32 | `RoadStatCom` | `nvarchar(1)` | ✓ |  |
| 33 | `RoadCalcoBJ` | `nvarchar(1)` | ✓ |  |
| 34 | `RoadImpres` | `int` | ✓ |  |
| 35 | `RoadADD1` | `nvarchar(5)` | ✓ |  |
| 36 | `RoadADD2` | `nvarchar(5)` | ✓ |  |
| 37 | `RoadADD3` | `nvarchar(35)` | ✓ |  |
| 38 | `RoadStatProc` | `nvarchar(3)` | ✓ |  |
| 39 | `RoadRechazado` | `bit` | ✓ |  |
| 40 | `RoadRazon_Rechazado` | `nvarchar(50)` | ✓ |  |
| 41 | `RoadInformado` | `bit` | ✓ |  |
| 42 | `RoadSucursal` | `nvarchar(10)` | ✓ |  |
| 43 | `RoadIdDespacho` | `int` | ✓ |  |
| 44 | `RoadIdFacturacion` | `int` | ✓ |  |
| 45 | `RoadIdRuta` | `int` | ✓ |  |
| 46 | `RoadIdVendedor` | `int` | ✓ |  |
| 47 | `RoadIdRutaDespacho` | `int` | ✓ |  |
| 48 | `RoadIdVendedorDespacho` | `int` | ✓ |  |
| 49 | `Observacion` | `nvarchar(255)` | ✓ |  |
| 50 | `PedidoRoad` | `bit` | ✓ |  |
| 51 | `HoraEntregaDesde` | `datetime` | ✓ |  |
| 52 | `HoraEntregaHasta` | `datetime` | ✓ |  |
| 53 | `referencia` | `nvarchar(25)` | ✓ |  |
| 54 | `IdMotivoAnulacionBodega` | `int` | ✓ |  |
| 55 | `Enviado_A_ERP` | `bit` | ✓ |  |
| 56 | `control_ultimo_lote` | `bit` | ✓ |  |
| 57 | `serie` | `nvarchar(25)` | ✓ |  |
| 58 | `correlativo` | `int` | ✓ |  |
| 59 | `Referencia_Documento_Ingreso_Bodega_Destino` | `nvarchar(50)` | ✓ |  |
| 60 | `sync_mi3` | `bit` | ✓ |  |
| 61 | `Nombre_Tipo_Pedido` | `nvarchar(50)` | ✓ |  |
| 62 | `Nombre_Cliente` | `nvarchar(150)` | ✓ |  |
| 63 | `Nombre_Propietario` | `nvarchar(100)` | ✓ |  |
| 64 | `IdPropietario` | `int` | ✓ |  |
| 65 | `Codigo_Cliente` | `nvarchar(150)` | ✓ |  |
| 66 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 67 | `es_bodega_traslado` | `bit` | ✓ |  |
| 68 | `idubicacionvirtual` | `int` | ✓ |  |
| 69 | `control_ultimo_lote_cliente` | `bit` | ✓ |  |
| 70 | `No_Picking_ERP` | `nvarchar(50)` | ✓ |  |
| 71 | `fecha_preparacion` | `date` | ✓ |  |
| 72 | `IdTipoManufactura` | `int` | ✓ |  |
| 73 | `bodega_origen` | `nvarchar(50)` | ✓ |  |
| 74 | `IdMotivoDevolucion` | `int` | ✓ |  |
| 75 | `bodega_destino` | `nvarchar(303)` |  |  |
| 76 | `permitir_despacho_parcial` | `bit` | ✓ |  |
| 77 | `permitir_despacho_multiple` | `bit` | ✓ |  |
| 78 | `empaque_tarima` | `bit` |  |  |
| 79 | `Expr1` | `nvarchar(50)` | ✓ |  |
| 80 | `no_documento_externo` | `nvarchar(50)` | ✓ |  |
| 81 | `requiere_tarimas` | `bit` | ✓ |  |
| 82 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |
| 83 | `EsExportacion` | `bit` |  |  |

## Consume

- `cliente`
- `propietario_bodega`
- `propietarios`
- `trans_pe_enc`
- `trans_pe_tipo`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Get_Single_Pedido]
AS
SELECT     dbo.trans_pe_enc.IdPedidoEnc, dbo.trans_pe_enc.IdBodega, dbo.trans_pe_enc.IdCliente, dbo.trans_pe_enc.IdMuelle, dbo.trans_pe_enc.IdPropietarioBodega, dbo.trans_pe_enc.IdTipoPedido, dbo.trans_pe_enc.IdPickingEnc, dbo.trans_pe_enc.Fecha_Pedido, dbo.trans_pe_enc.hora_ini, 
                  dbo.trans_pe_enc.hora_fin, dbo.trans_pe_enc.ubicacion, dbo.trans_pe_enc.estado, dbo.trans_pe_enc.no_despacho, dbo.trans_pe_enc.activo, dbo.trans_pe_enc.user_agr, dbo.trans_pe_enc.fec_agr, dbo.trans_pe_enc.user_mod, dbo.trans_pe_enc.fec_mod, 
                  dbo.trans_pe_enc.no_documento, dbo.trans_pe_enc.local, dbo.trans_pe_enc.pallet_primero, dbo.trans_pe_enc.dias_cliente, dbo.trans_pe_enc.anulado, dbo.trans_pe_enc.RoadKilometraje, dbo.trans_pe_enc.RoadFechaEntr, dbo.trans_pe_enc.RoadDirEntrega, 
                  dbo.trans_pe_enc.RoadTotal, dbo.trans_pe_enc.RoadDesMonto, dbo.trans_pe_enc.RoadImpMonto, dbo.trans_pe_enc.RoadPeso, dbo.trans_pe_enc.RoadBandera, dbo.trans_pe_enc.RoadStatCom, dbo.trans_pe_enc.RoadCalcoBJ, dbo.trans_pe_enc.RoadImpres, 
                  dbo.trans_pe_enc.RoadADD1, dbo.trans_pe_enc.RoadADD2, dbo.trans_pe_enc.RoadADD3, dbo.trans_pe_enc.RoadStatProc, dbo.trans_pe_enc.RoadRechazado, dbo.trans_pe_enc.RoadRazon_Rechazado, dbo.trans_pe_enc.RoadInformado, dbo.trans_pe_enc.RoadSucursal, 
                  dbo.trans_pe_enc.RoadIdDespacho, dbo.trans_pe_enc.RoadIdFacturacion, dbo.trans_pe_enc.RoadIdRuta, dbo.trans_pe_enc.RoadIdVendedor, dbo.trans_pe_enc.RoadIdRutaDespacho, dbo.trans_pe_enc.RoadIdVendedorDespacho, dbo.trans_pe_enc.Observacion, 
                  dbo.trans_pe_enc.PedidoRoad, dbo.trans_pe_enc.HoraEntregaDesde, dbo.trans_pe_enc.HoraEntregaHasta, dbo.trans_pe_enc.referencia, dbo.trans_pe_enc.IdMotivoAnulacionBodega, dbo.trans_pe_enc.Enviado_A_ERP, dbo.trans_pe_enc.control_ultimo_lote, dbo.trans_pe_enc.serie, 
                  dbo.trans_pe_enc.correlativo, dbo.trans_pe_enc.Referencia_Documento_Ingreso_Bodega_Destino, dbo.trans_pe_enc.sync_mi3, dbo.trans_pe_tipo.Nombre AS Nombre_Tipo_Pedido, dbo.cliente.nombre_comercial AS Nombre_Cliente, 
                  dbo.propietarios.nombre_comercial AS Nombre_Propietario, dbo.propietario_bodega.IdPropietario, dbo.cliente.codigo AS Codigo_Cliente, dbo.cliente.es_bodega_recepcion, dbo.cliente.es_bodega_traslado, dbo.cliente.idubicacionvirtual, 
                  dbo.cliente.control_ultimo_lote AS control_ultimo_lote_cliente, dbo.trans_pe_enc.No_Picking_ERP, dbo.trans_pe_enc.fecha_preparacion, dbo.trans_pe_enc.IdTipoManufactura, dbo.trans_pe_enc.bodega_origen, dbo.trans_pe_enc.IdMotivoDevolucion, CASE WHEN c1.codigo IS NULL 
                  THEN '' ELSE ISNULL(concat(c1.codigo, ' - ', c1.nombre_comercial), '') END AS bodega_destino, dbo.trans_pe_tipo.permitir_despacho_parcial, dbo.trans_pe_tipo.permitir_despacho_multiple, ISNULL(dbo.trans_pe_tipo.empaque_tarima, 0) AS empaque_tarima, 
                  dbo.trans_pe_enc.bodega_destino AS Expr1, dbo.trans_pe_enc.no_documento_externo, dbo.trans_pe_enc.requiere_tarimas, dbo.trans_pe_enc.Codigo_Empresa_ERP,trans_pe_enc.EsExportacion
FROM        dbo.propietarios INNER JOIN
                  dbo.propietario_bodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario RIGHT OUTER JOIN
                  dbo.trans_pe_enc ON dbo.propietario_bodega.IdPropietarioBodega = dbo.trans_pe_enc.IdPropietarioBodega LEFT OUTER JOIN
                  dbo.trans_pe_tipo ON dbo.trans_pe_enc.IdTipoPedido = dbo.trans_pe_tipo.IdTipoPedido LEFT OUTER JOIN
                  dbo.cliente ON dbo.trans_pe_enc.IdCliente = dbo.cliente.IdCliente LEFT OUTER JOIN
                  dbo.cliente AS c1 ON c1.codigo COLLATE Modern_Spanish_CI_AS = dbo.trans_pe_enc.bodega_destino
```
