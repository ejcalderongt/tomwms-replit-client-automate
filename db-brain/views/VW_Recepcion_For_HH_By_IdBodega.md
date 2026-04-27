---
id: db-brain-view-vw-recepcion-for-hh-by-idbodega
type: db-view
title: dbo.VW_Recepcion_For_HH_By_IdBodega
schema: dbo
name: VW_Recepcion_For_HH_By_IdBodega
kind: view
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Recepcion_For_HH_By_IdBodega`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-08-25 |
| Columnas | 13 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `N` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdPropietario` | `int` | ✓ |  |
| 5 | `Propietario` | `nvarchar(100)` |  |  |
| 6 | `IdProveedor` | `int` |  |  |
| 7 | `Proveedor` | `nvarchar(100)` |  |  |
| 8 | `No_Documento` | `nvarchar(30)` | ✓ |  |
| 9 | `MotivoDevolucion` | `nvarchar(50)` | ✓ |  |
| 10 | `Tipo` | `nvarchar(50)` | ✓ |  |
| 11 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 12 | `IdOrdenCompraEnc` | `int` |  |  |
| 13 | `TipoTrans` | `nvarchar(25)` | ✓ |  |

## Consume

- `bodega_muelles`
- `motivo_devolucion`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_enc`
- `trans_oc_ti`
- `trans_re_enc`
- `trans_re_oc`
- `trans_re_tr`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Recepcion_For_HH_By_IdBodega]
AS
SELECT        dbo.trans_re_enc.IdBodega, dbo.trans_re_enc.IdRecepcionEnc AS N, 
			  dbo.propietario_bodega.IdPropietarioBodega, 
			  dbo.propietario_bodega.IdPropietario, 
			  dbo.propietarios.nombre_comercial AS Propietario, 
              ISNULL(dbo.proveedor.IdProveedor,0) AS IdProveedor, 
			  ISNULL(dbo.proveedor.nombre,'') AS Proveedor, dbo.trans_oc_enc.No_Documento, dbo.motivo_devolucion.Nombre AS MotivoDevolucion, dbo.trans_oc_ti.Nombre AS Tipo, dbo.trans_oc_enc.Referencia, 
                         dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_re_tr.TipoTrans
FROM            dbo.trans_oc_ti INNER JOIN
                         dbo.trans_re_enc INNER JOIN
                         dbo.propietario_bodega ON dbo.trans_re_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                         dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                         dbo.trans_re_oc ON dbo.trans_re_enc.IdRecepcionEnc = dbo.trans_re_oc.IdRecepcionEnc INNER JOIN
                         dbo.trans_oc_enc ON dbo.propietario_bodega.IdPropietarioBodega = dbo.trans_oc_enc.IdPropietarioBodega AND dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc ON 
                         dbo.trans_oc_ti.IdTipoIngresoOC = dbo.trans_oc_enc.IdTipoIngresoOC INNER JOIN
                         dbo.trans_re_tr ON dbo.trans_re_enc.IdTipoTransaccion = dbo.trans_re_tr.IdTipoTransaccion LEFT OUTER JOIN
                         dbo.proveedor INNER JOIN
                         dbo.proveedor_bodega ON dbo.proveedor.IdProveedor = dbo.proveedor_bodega.IdProveedor ON dbo.proveedor_bodega.IdAsignacion = dbo.trans_oc_enc.IdProveedorBodega LEFT OUTER JOIN
                         dbo.bodega_muelles ON dbo.trans_re_enc.IdMuelle = dbo.bodega_muelles.IdMuelle AND dbo.trans_re_enc.IdBodega = dbo.bodega_muelles.IdBodega LEFT OUTER JOIN
                         dbo.motivo_devolucion ON dbo.propietarios.IdPropietario = dbo.motivo_devolucion.IdPropietario AND dbo.trans_oc_enc.IdMotivoDevolucion = dbo.motivo_devolucion.IdMotivoDevolucion
WHERE        (dbo.trans_re_enc.estado NOT IN ('Cerrado', 'Anulado')) AND (dbo.trans_re_enc.idusuariobloqueo = 0) AND (dbo.trans_re_enc.bloqueada_por = '') AND EXISTS
                             (SELECT        IdTipoTransaccion
                               FROM            dbo.trans_re_tr AS trt
                               WHERE        (UsaHH = 1) AND (dbo.trans_re_enc.IdTipoTransaccion = IdTipoTransaccion))
```
