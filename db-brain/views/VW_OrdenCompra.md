---
id: db-brain-view-vw-ordencompra
type: db-view
title: dbo.VW_OrdenCompra
schema: dbo
name: VW_OrdenCompra
kind: view
modify_date: 2025-06-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_OrdenCompra`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-11 |
| Columnas | 25 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 4 | `Proveedor` | `nvarchar(100)` | ✓ |  |
| 5 | `Tipo Ingreso` | `nvarchar(50)` | ✓ |  |
| 6 | `Estado` | `nvarchar(50)` | ✓ |  |
| 7 | `No. Documento` | `nvarchar(30)` | ✓ |  |
| 8 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 9 | `Procedencia` | `nvarchar(150)` | ✓ |  |
| 10 | `IdPropietario` | `int` | ✓ |  |
| 11 | `Activo` | `bit` | ✓ |  |
| 12 | `IdPropietarioBodega` | `int` |  |  |
| 13 | `Fecha` | `datetime` | ✓ |  |
| 14 | `es_devolucion` | `bit` | ✓ |  |
| 15 | `Enviado_A_ERP` | `bit` | ✓ |  |
| 16 | `IdBodega` | `int` | ✓ |  |
| 17 | `NoPoliza` | `nvarchar(150)` | ✓ |  |
| 18 | `NoOrden` | `nvarchar(50)` | ✓ |  |
| 19 | `No_Documento_Recepcion_ERP` | `nvarchar(50)` | ✓ |  |
| 20 | `No_Documento_Devolucion` | `nvarchar(50)` | ✓ |  |
| 21 | `No_Documento_Ubicacion_ERP` | `nvarchar(50)` | ✓ |  |
| 22 | `No_Ticket_TMS` | `int` | ✓ |  |
| 23 | `No_Marchamo` | `nvarchar(50)` | ✓ |  |
| 24 | `Control_Poliza` | `bit` | ✓ |  |
| 25 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_enc`
- `trans_oc_estado`
- `trans_oc_pol`
- `trans_oc_ti`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_OrdenCompra]
AS
SELECT DISTINCT 
                  enc.IdOrdenCompraEnc AS Código, b.nombre AS Bodega, pr.nombre_comercial AS Propietario, pv.nombre AS Proveedor, tpi.Nombre AS [Tipo Ingreso], e.Nombre AS Estado, enc.No_Documento AS [No. Documento], enc.Referencia, enc.Procedencia, prb.IdPropietario, enc.Activo, 
                  enc.IdPropietarioBodega, enc.Fec_Agr AS Fecha, tpi.es_devolucion, enc.Enviado_A_ERP, enc.IdBodega, dbo.trans_oc_pol.codigo_poliza AS NoPoliza, dbo.trans_oc_pol.numero_orden AS NoOrden, enc.no_documento_recepcion_erp AS No_Documento_Recepcion_ERP, 
                  enc.No_Documento_Devolucion, enc.No_Documento_Ubicacion_ERP, enc.no_ticket_tms AS No_Ticket_TMS, enc.No_Marchamo, enc.Control_Poliza, enc.Codigo_Empresa_ERP
FROM        dbo.proveedor AS pv INNER JOIN
                  dbo.proveedor_bodega AS pb ON pv.IdProveedor = pb.IdProveedor RIGHT OUTER JOIN
                  dbo.trans_oc_pol RIGHT OUTER JOIN
                  dbo.bodega AS b INNER JOIN
                  dbo.trans_oc_ti AS tpi INNER JOIN
                  dbo.trans_oc_enc AS enc ON tpi.IdTipoIngresoOC = enc.IdTipoIngresoOC INNER JOIN
                  dbo.trans_oc_estado AS e ON enc.IdEstadoOC = e.IdEstadoOC ON b.IdBodega = enc.IdBodega ON dbo.trans_oc_pol.IdOrdenCompraEnc = enc.IdOrdenCompraEnc ON pb.IdAsignacion = enc.IdProveedorBodega LEFT OUTER JOIN
                  dbo.propietarios AS pr INNER JOIN
                  dbo.propietario_bodega AS prb ON pr.IdPropietario = prb.IdPropietario ON enc.IdPropietarioBodega = prb.IdPropietarioBodega
```
