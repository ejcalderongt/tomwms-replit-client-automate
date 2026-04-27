---
id: db-brain-view-vw-recepcion
type: db-view
title: dbo.VW_Recepcion
schema: dbo
name: VW_Recepcion
kind: view
modify_date: 2022-06-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Recepcion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-07 |
| Columnas | 17 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `Propietario` | `nvarchar(100)` |  |  |
| 3 | `Proveedor` | `nvarchar(100)` | ✓ |  |
| 4 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 5 | `NoDocIngreso` | `int` | ✓ |  |
| 6 | `Referencia_DI` | `nvarchar(101)` | ✓ |  |
| 7 | `No_Contenedor` | `nvarchar(50)` | ✓ |  |
| 8 | `Fecha` | `datetime` | ✓ |  |
| 9 | `estado` | `nvarchar(20)` | ✓ |  |
| 10 | `TipoTrans` | `nvarchar(25)` | ✓ |  |
| 11 | `Descripcion` | `nvarchar(50)` |  |  |
| 12 | `Muelle` | `nvarchar(50)` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `Usuario_Agrego` | `nvarchar(100)` | ✓ |  |
| 15 | `Fecha_Agrego` | `datetime` | ✓ |  |
| 16 | `IdTipoTransaccion` | `nvarchar(50)` |  |  |
| 17 | `IdBodega` | `int` |  |  |

## Consume

- `bodega`
- `bodega_muelles`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_enc`
- `trans_re_enc`
- `trans_re_oc`
- `trans_re_tr`
- `usuario`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Recepcion]
AS
SELECT dbo.trans_re_enc.IdRecepcionEnc AS Código,
dbo.propietarios.nombre_comercial AS Propietario,
dbo.proveedor.nombre AS Proveedor, dbo.bodega.nombre AS Bodega,
dbo.trans_re_oc.IdOrdenCompraEnc AS NoDocIngreso,
CONVERT(nvarchar(50),dbo.trans_oc_enc.No_Documento) + ' ' + CONVERT(NVARCHAR(50),dbo.trans_oc_enc.Referencia) AS Referencia_DI,
dbo.trans_re_enc.NoGuia as No_Contenedor, dbo.trans_re_enc.fecha_recepcion AS Fecha,
dbo.trans_re_enc.estado, dbo.trans_re_tr.TipoTrans, dbo.trans_re_tr.Descripcion,
dbo.bodega_muelles.nombre AS Muelle, dbo.trans_re_enc.activo,
dbo.usuario.nombres AS Usuario_Agrego, dbo.trans_re_enc.fec_agr AS Fecha_Agrego,
dbo.trans_re_tr.IdTipoTransaccion, dbo.bodega.IdBodega
FROM dbo.bodega_muelles RIGHT OUTER JOIN
dbo.trans_re_oc INNER JOIN
dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc INNER JOIN
dbo.proveedor_bodega ON dbo.trans_oc_enc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion INNER JOIN
dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor RIGHT OUTER JOIN
dbo.trans_re_tr INNER JOIN
dbo.trans_re_enc ON dbo.trans_re_tr.IdTipoTransaccion = dbo.trans_re_enc.IdTipoTransaccion INNER JOIN
dbo.usuario ON dbo.trans_re_enc.user_agr = dbo.usuario.IdUsuario INNER JOIN
dbo.propietarios INNER JOIN
dbo.propietario_bodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario INNER JOIN
dbo.bodega ON dbo.propietario_bodega.IdBodega = dbo.bodega.IdBodega ON dbo.trans_re_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON
dbo.trans_re_oc.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc ON dbo.bodega_muelles.IdMuelle = dbo.trans_re_enc.IdMuelle
```
