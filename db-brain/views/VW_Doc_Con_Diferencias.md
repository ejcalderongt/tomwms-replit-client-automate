---
id: db-brain-view-vw-doc-con-diferencias
type: db-view
title: dbo.VW_Doc_Con_Diferencias
schema: dbo
name: VW_Doc_Con_Diferencias
kind: view
modify_date: 2022-12-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Doc_Con_Diferencias`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-12-13 |
| Columnas | 28 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` |  |  |
| 2 | `ORDENCOMPRA` | `nvarchar(30)` | ✓ |  |
| 3 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 4 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 5 | `cantidad` | `float` | ✓ |  |
| 6 | `cantidad_recibida` | `float` | ✓ |  |
| 7 | `PRESENTACION` | `nvarchar(50)` | ✓ |  |
| 8 | `DIFERENCIA` | `float` | ✓ |  |
| 9 | `IdPropietarioBodega` | `int` |  |  |
| 10 | `CodigoBodega` | `nvarchar(50)` | ✓ |  |
| 11 | `BODEGA` | `nvarchar(50)` | ✓ |  |
| 12 | `PROPIETARIO` | `nvarchar(100)` |  |  |
| 13 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 14 | `IdProveedorBodega` | `int` |  |  |
| 15 | `IdTipoIngresoOC` | `int` | ✓ |  |
| 16 | `NOMBRE_INGRESOOC` | `nvarchar(50)` | ✓ |  |
| 17 | `IdProductoBodega` | `int` |  |  |
| 18 | `IdPresentacion` | `int` | ✓ |  |
| 19 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 20 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 21 | `ESTADO` | `nvarchar(50)` | ✓ |  |
| 22 | `activo` | `bit` |  |  |
| 23 | `Fecha_Creacion` | `datetime` | ✓ |  |
| 24 | `IdPropietario` | `int` |  |  |
| 25 | `IdBodega` | `int` |  |  |
| 26 | `No_Documento` | `nvarchar(30)` | ✓ |  |
| 27 | `referencia` | `nvarchar(100)` | ✓ |  |
| 28 | `Enviado_A_ERP` | `bit` | ✓ |  |

## Consume

- `bodega`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_oc_det`
- `trans_oc_enc`
- `trans_oc_estado`
- `trans_oc_pol`
- `trans_oc_ti`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view [dbo].[VW_Doc_Con_Diferencias] AS
SELECT       enc.IdOrdenCompraEnc, ENC.No_Documento AS ORDENCOMPRA, DET.codigo_producto, DET.nombre_producto, DET.cantidad, DET.cantidad_recibida, CASE WHEN ISNULL(DET.IdPresentacion, 0) 
                         = 0 THEN um.Nombre ELSE pr.nombre END AS PRESENTACION, DET.cantidad_recibida - DET.cantidad AS DIFERENCIA, ENC.IdPropietarioBodega,dbo.bodega.codigo as CodigoBodega, dbo.bodega.nombre_comercial AS BODEGA, 
                         PROP.nombre_comercial AS PROPIETARIO, oc_pol.codigo_poliza as Poliza,ENC.IdProveedorBodega, ENC.IdTipoIngresoOC, TIPO.Nombre AS NOMBRE_INGRESOOC, DET.IdProductoBodega, DET.IdPresentacion, DET.IdUnidadMedidaBasica, 
                         um.Nombre AS UMBas, ESTADO.Nombre AS ESTADO, DET.activo, ENC.Fecha_Creacion, PROP.IdPropietario, dbo.bodega.IdBodega, enc.No_Documento, enc.referencia, ENC.Enviado_A_ERP
FROM            dbo.trans_oc_enc AS ENC INNER JOIN
                         dbo.trans_oc_det AS DET ON ENC.IdOrdenCompraEnc = DET.IdOrdenCompraEnc INNER JOIN
                         dbo.trans_oc_ti AS TIPO ON ENC.IdTipoIngresoOC = TIPO.IdTipoIngresoOC INNER JOIN
                         dbo.propietario_bodega AS PROP_BD ON ENC.IdPropietarioBodega = PROP_BD.IdPropietarioBodega INNER JOIN
                         dbo.trans_oc_estado AS ESTADO ON ENC.IdEstadoOC = ESTADO.IdEstadoOC INNER JOIN
                         dbo.bodega ON PROP_BD.IdBodega = dbo.bodega.IdBodega INNER JOIN
                         dbo.propietarios AS PROP ON PROP_BD.IdPropietario = PROP.IdPropietario INNER JOIN
                         dbo.unidad_medida AS um ON um.IdUnidadMedida = DET.IdUnidadMedidaBasica LEFT OUTER JOIN
                         dbo.producto_presentacion AS pr ON DET.IdPresentacion = pr.IdPresentacion LEFT OUTER JOIN
						 dbo.trans_oc_pol oc_pol on ENC.IdOrdenCompraEnc = oc_pol.IdOrdenCompraEnc
WHERE        (TIPO.activo = 1) AND (ESTADO.IdEstadoOC <> 5)
```
