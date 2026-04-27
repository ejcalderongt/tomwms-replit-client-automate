---
id: db-brain-view-vw-trans-servicios
type: db-view
title: dbo.VW_Trans_Servicios
schema: dbo
name: VW_Trans_Servicios
kind: view
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Trans_Servicios`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-08-25 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Correlativo` | `int` |  |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `Documento_Ingreso` | `nvarchar(30)` | ✓ |  |
| 4 | `Propietario` | `nvarchar(100)` |  |  |
| 5 | `no_orden` | `nvarchar(50)` | ✓ |  |
| 6 | `no_poliza` | `nvarchar(50)` | ✓ |  |
| 7 | `Fecha` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |
| 9 | `Estado_Servicio` | `nvarchar(50)` | ✓ |  |
| 10 | `MI3_Estatus` | `bit` | ✓ |  |

## Consume

- `bodega`
- `propietarios`
- `trans_oc_enc`
- `trans_servicio_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
create VIEW [dbo].[VW_Trans_Servicios]
AS
SELECT        t.IdServicioEnc AS Correlativo, b.nombre AS Bodega, o.No_Documento AS Documento_Ingreso, p.nombre_comercial AS Propietario, t.no_orden, t.no_poliza, t.fecha_servicio AS Fecha, b.activo, t.Estado AS Estado_Servicio, 
                         t.enviado_a_erp AS MI3_Estatus
FROM            dbo.trans_servicio_enc AS t INNER JOIN
                         dbo.bodega AS b ON t.IdBodega = b.IdBodega AND t.IdEmpresa = b.IdEmpresa INNER JOIN
                         dbo.trans_oc_enc AS o ON o.IdOrdenCompraEnc = t.IdOrdenCompraEnc INNER JOIN
                         dbo.propietarios AS p ON p.IdPropietario = t.IdPropietario
```
