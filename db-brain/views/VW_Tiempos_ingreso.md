---
id: db-brain-view-vw-tiempos-ingreso
type: db-view
title: dbo.VW_Tiempos_ingreso
schema: dbo
name: VW_Tiempos_ingreso
kind: view
modify_date: 2020-10-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tiempos_ingreso`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2020-10-07 |
| Columnas | 14 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionOc` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `IdOrdenCompraEnc` | `int` |  |  |
| 4 | `Codigo_Bodega` | `nvarchar(50)` | ✓ |  |
| 5 | `Propietario` | `nvarchar(100)` |  |  |
| 6 | `TipoDocumento` | `nvarchar(50)` | ✓ |  |
| 7 | `OC_Hora_Creacion` | `datetime` | ✓ |  |
| 8 | `RE_Hora_Creacion_Recepcion` | `datetime` | ✓ |  |
| 9 | `OC_Fecha_Recepcion` | `datetime` | ✓ |  |
| 10 | `REOC_HORA_FIN` | `datetime` | ✓ |  |
| 11 | `DIF1` | `nvarchar(20)` | ✓ |  |
| 12 | `DIF2` | `nvarchar(20)` | ✓ |  |
| 13 | `DIF3` | `nvarchar(20)` | ✓ |  |
| 14 | `DIF4` | `nvarchar(20)` | ✓ |  |

## Consume

- `bodega`
- `ConvertSecondsFormatoFecha`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_enc`
- `trans_oc_ti`
- `trans_re_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Tiempos_ingreso]
AS
SELECT ocre.IdRecepcionOc, re.IdRecepcionEnc, oc.IdOrdenCompraEnc, dbo.bodega.codigo AS Codigo_Bodega, dbo.propietarios.nombre_comercial AS Propietario, dbo.trans_oc_ti.Nombre AS TipoDocumento, oc.Fec_Agr AS OC_Hora_Creacion, 
                  re.fec_agr AS RE_Hora_Creacion_Recepcion, oc.Fecha_Recepcion AS OC_Fecha_Recepcion, ocre.hora_fin_hh AS REOC_HORA_FIN, dbo.ConvertSecondsFormatoFecha(ABS(DATEDIFF(SECOND, oc.Fec_Agr, re.fec_agr))) AS DIF1, 
                  dbo.ConvertSecondsFormatoFecha(ABS(DATEDIFF(SECOND, re.fec_agr, oc.Fecha_Recepcion))) AS DIF2, dbo.ConvertSecondsFormatoFecha(ABS(DATEDIFF(SECOND, oc.Fecha_Recepcion, ocre.hora_fin_hh))) AS DIF3, 
                  dbo.ConvertSecondsFormatoFecha(ABS(DATEDIFF(SECOND, re.fec_agr, ocre.hora_fin_hh))) AS DIF4
FROM     dbo.trans_re_oc AS ocre INNER JOIN
                  dbo.trans_re_enc AS re ON ocre.IdRecepcionEnc = re.IdRecepcionEnc INNER JOIN
                  dbo.trans_oc_enc AS oc ON ocre.IdOrdenCompraEnc = oc.IdOrdenCompraEnc INNER JOIN
                  dbo.trans_oc_ti ON oc.IdTipoIngresoOC = dbo.trans_oc_ti.IdTipoIngresoOC INNER JOIN
                  dbo.proveedor_bodega ON oc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion INNER JOIN
                  dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor INNER JOIN
                  dbo.propietario_bodega ON oc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                  dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                  dbo.bodega ON dbo.proveedor_bodega.IdBodega = dbo.bodega.IdBodega AND dbo.propietario_bodega.IdBodega = dbo.bodega.IdBodega
WHERE  (re.IdTipoTransaccion = 'HCOC00') AND (oc.Fecha_Creacion > '20190201')
```
