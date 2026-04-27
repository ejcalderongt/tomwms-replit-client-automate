---
id: db-brain-view-vw-tiempos-ingreso-operador
type: db-view
title: dbo.VW_Tiempos_Ingreso_Operador
schema: dbo
name: VW_Tiempos_Ingreso_Operador
kind: view
modify_date: 2024-10-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tiempos_Ingreso_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-10-01 |
| Columnas | 15 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionOc` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `IdOrdenCompraEnc` | `int` |  |  |
| 4 | `Codigo_Bodega` | `nvarchar(50)` | ✓ |  |
| 5 | `Propietario` | `nvarchar(100)` |  |  |
| 6 | `TipoDocumento` | `nvarchar(50)` | ✓ |  |
| 7 | `DI_Hora_Creacion` | `datetime` | ✓ |  |
| 8 | `RE_Hora_Creacion_Recepcion` | `datetime` | ✓ |  |
| 9 | `DI_Fecha_Recepcion` | `datetime` | ✓ |  |
| 10 | `RE_DI_HORA_FIN` | `datetime` | ✓ |  |
| 11 | `Operador` | `nvarchar(252)` | ✓ |  |
| 12 | `Ini_Ingreso` | `datetime` | ✓ |  |
| 13 | `Fin_Ingreso` | `datetime` | ✓ |  |
| 14 | `Dif_Ingreso` | `nvarchar(50)` | ✓ |  |
| 15 | `Dif_Ingreso_Minutos` | `int` | ✓ |  |

## Consume

- `bodega`
- `ConvertSecondsFormatoFecha`
- `operador`
- `operador_bodega`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_enc`
- `trans_oc_ti`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Tiempos_Ingreso_Operador]
AS
SELECT  ocre.IdRecepcionOc, 
		re.IdRecepcionEnc, 
		oc.IdOrdenCompraEnc, 
		dbo.bodega.codigo AS Codigo_Bodega, 
		dbo.propietarios.nombre_comercial AS Propietario, 
		dbo.trans_oc_ti.Nombre AS TipoDocumento, 
		MAX(oc.Fec_Agr) AS DI_Hora_Creacion,
		MAX(re.Fec_Agr) as RE_Hora_Creacion_Recepcion,
		MAX(oc.Fecha_Recepcion) as DI_Fecha_Recepcion,
		MAX(ocre.hora_fin_hh) AS RE_DI_HORA_FIN,
		CONVERT(NVARCHAR(50),rd.IdOperadorBodega) + ' ' + 
		o.nombres + ' ' + o.apellidos AS Operador,
		MIN(rd.fec_agr) AS Ini_Ingreso,
		MAX(rd.fec_agr) AS Fin_Ingreso, 
		dbo.ConvertSecondsFormatoFecha(DateDiff(SECOND,MIN(rd.fecha_ingreso),MAX(rd.fecha_ingreso))) AS Dif_Ingreso,
		(DateDiff(MINUTE,MIN(rd.fecha_ingreso),MAX(rd.fecha_ingreso))) AS Dif_Ingreso_Minutos
FROM     dbo.trans_re_oc AS ocre INNER JOIN
                  dbo.trans_re_enc AS re ON ocre.IdRecepcionEnc = re.IdRecepcionEnc INNER JOIN
				  dbo.trans_re_det AS rd ON re.IdRecepcionEnc = rd.IdRecepcionEnc AND ocre.IdRecepcionEnc = rd.IdRecepcionEnc INNER JOIN
                  dbo.trans_oc_enc AS oc ON ocre.IdOrdenCompraEnc = oc.IdOrdenCompraEnc INNER JOIN
                  dbo.trans_oc_ti ON oc.IdTipoIngresoOC = dbo.trans_oc_ti.IdTipoIngresoOC INNER JOIN
                  dbo.proveedor_bodega ON oc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion INNER JOIN
                  dbo.proveedor ON dbo.proveedor_bodega.IdProveedor = dbo.proveedor.IdProveedor INNER JOIN
                  dbo.propietario_bodega ON oc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                  dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                  dbo.bodega ON dbo.proveedor_bodega.IdBodega = dbo.bodega.IdBodega AND dbo.propietario_bodega.IdBodega = dbo.bodega.IdBodega INNER JOIN
				  dbo.operador_bodega ob ON rd.IdOperadorBodega = ob.IdOperadorBodega INNER JOIN
				  dbo.operador o ON o.IdOperador = ob.IdOperador
WHERE  (re.IdTipoTransaccion = 'HCOC00') AND (oc.Fecha_Creacion > '20190201') AND oc.IdEstadoOC <> 5 and re.estado <>'Anulado'
GROUP BY ocre.IdRecepcionOc, re.IdRecepcionEnc, oc.IdOrdenCompraEnc, dbo.bodega.codigo, 
dbo.propietarios.nombre_comercial, dbo.trans_oc_ti.Nombre, rd.IdOperadorBodega, o.nombres, o.apellidos
```
