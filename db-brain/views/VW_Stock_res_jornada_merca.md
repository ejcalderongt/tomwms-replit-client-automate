---
id: db-brain-view-vw-stock-res-jornada-merca
type: db-view
title: dbo.VW_Stock_res_jornada_merca
schema: dbo
name: VW_Stock_res_jornada_merca
kind: view
modify_date: 2022-10-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_res_jornada_merca`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-10-25 |
| Columnas | 30 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `regimen` | `nvarchar(50)` | ✓ |  |
| 2 | `codigobodega` | `nvarchar(150)` | ✓ |  |
| 3 | `fecha` | `date` | ✓ |  |
| 4 | `codigomercaderia` | `nvarchar(50)` | ✓ |  |
| 5 | `certificadodeposito` | `nvarchar(50)` | ✓ |  |
| 6 | `bonoprenda` | `nvarchar(50)` | ✓ |  |
| 7 | `saldosincertificado` | `numeric` | ✓ |  |
| 8 | `saldocertificado` | `numeric` | ✓ |  |
| 9 | `saldobono` | `nvarchar(50)` | ✓ |  |
| 10 | `nombreacreedor` | `nvarchar(50)` | ✓ |  |
| 11 | `descripcionmercaderia` | `nvarchar(100)` | ✓ |  |
| 12 | `fechaemisioncertificado` | `nvarchar(50)` | ✓ |  |
| 13 | `fechavencimientocertificado` | `nvarchar(50)` | ✓ |  |
| 14 | `fechaemisionbono` | `nvarchar(50)` | ✓ |  |
| 15 | `fechavencimientobono` | `nvarchar(50)` | ✓ |  |
| 16 | `fechaemisionds` | `nvarchar(4000)` | ✓ |  |
| 17 | `fechavencimientods` | `nvarchar(4000)` | ✓ |  |
| 18 | `cif` | `numeric` | ✓ |  |
| 19 | `impuestos` | `numeric` | ✓ |  |
| 20 | `seguros` | `nvarchar(50)` | ✓ |  |
| 21 | `seguros2` | `nvarchar(50)` | ✓ |  |
| 22 | `primerapellido` | `nvarchar(50)` | ✓ |  |
| 23 | `segundoapellido` | `nvarchar(50)` | ✓ |  |
| 24 | `apellidocasada` | `nvarchar(50)` | ✓ |  |
| 25 | `primernombre` | `nvarchar(50)` | ✓ |  |
| 26 | `segundonombre` | `nvarchar(50)` | ✓ |  |
| 27 | `tercernombre` | `nvarchar(50)` | ✓ |  |
| 28 | `razonsocial` | `nvarchar(150)` | ✓ |  |
| 29 | `nombrecomercial` | `nvarchar(150)` | ✓ |  |
| 30 | `terminacion` | `nvarchar(1)` | ✓ |  |

## Consume

- `bodega`
- `stock_jornada`
- `valores_fijos_reporte_mercancias`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
/************ GT vista ajustada con el campo fecha en tabla pivote ***********************/
CREATE view VW_Stock_res_jornada_merca as
select regimen, codigo_barra as codigobodega,
fecha,
'3' codigomercaderia,
'' certificadodeposito,
'' bonoprenda,
(cast(round(sum(valor_aduana), 2) as numeric(36,2))) as saldosincertificado,
0.00 saldocertificado,''saldobono,'' nombreacreedor,nombre_producto descripcionmercaderia,'' fechaemisioncertificado,
'' fechavencimientocertificado,'' fechaemisionbono,'' fechavencimientobono,
FORMAT (fecha_ingreso_ticket_tms, 'ddMMyyyy') fechaemisionds,FORMAT (DATEADD(year, 1, fecha_ingreso_ticket_tms), 'ddMMyyyy') fechavencimientods,
(cast(round(valor_fob+valor_flete+valor_seguro,2) as numeric(36,2))) as cif,
case Regimen when 'Fiscal' then cast(round(valor_dai + valor_iva,2) as numeric(36,2)) else 0.00 end impuestos,
'0.00' seguros,'0.00' seguros2,'' primerapellido,'' segundoapellido,'' apellidocasada,'' primernombre,'' segundonombre,
'' tercernombre, Proveedor as razonsocial,nombre_comercial nombrecomercial, '>' terminacion 
from   dbo.stock_jornada INNER JOIN
dbo.bodega ON stock_jornada.IdBodega = dbo.bodega.IdBodega 
where stock_jornada.activo =1
group by Regimen,nombre_producto,fecha_ingreso_ticket_tms,nombre_comercial,
valor_fob,valor_flete,valor_seguro,valor_dai,valor_iva,codigo_barra,Proveedor,fecha
union
select * from valores_fijos_reporte_mercancias
```
