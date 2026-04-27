---
id: db-brain-view-vw-movimientos-retroactivos
type: db-view
title: dbo.VW_Movimientos_Retroactivos
schema: dbo
name: VW_Movimientos_Retroactivos
kind: view
modify_date: 2023-06-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Movimientos_Retroactivos`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-06-18 |
| Columnas | 11 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `estado` | `varchar(20)` |  |  |
| 2 | `licencia` | `nvarchar(50)` | ✓ |  |
| 3 | `fecha_ingreso` | `date` | ✓ |  |
| 4 | `fecha_inicial_historico` | `date` | ✓ |  |
| 5 | `dias_pendientes` | `int` | ✓ |  |
| 6 | `ticket_tms` | `int` | ✓ |  |
| 7 | `IdOrdenCompraEnc` | `int` |  |  |
| 8 | `IdRecepcionEnc` | `int` |  |  |
| 9 | `cliente` | `nvarchar(100)` |  |  |
| 10 | `regimen` | `nvarchar(50)` | ✓ |  |
| 11 | `Referencia` | `nvarchar(100)` | ✓ |  |

## Consume

- `bodega`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_jornada`
- `tms_ticket`
- `trans_oc_enc`
- `trans_re_det`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view [dbo].[VW_Movimientos_Retroactivos] as
WITH SomeCTE AS (
select re_det.lic_plate,oc.referencia,bd.nombre as regimen,prop.nombre_comercial cliente,
case
    when  oc.no_ticket_tms > 0  then cast(tms.Fecha_Ingreso as date)
    else cast(re_det.fec_agr as date)
end as fecha_ingreso,
oc.no_ticket_tms,oc.IdOrdenCompraEnc,re_det.IdRecepcionEnc
from trans_re_det re_det inner join stock st
			on re_det.lic_plate=st.lic_plate inner join trans_oc_enc oc
			on re_det.IdOrdenCompraEnc=oc.IdOrdenCompraEnc left outer join tms_ticket tms 
			on oc.no_ticket_tms = tms.IdTicket inner join bodega bd
			on oc.IdBodega=bd.IdBodega inner join propietario_bodega pb
			on oc.IdPropietarioBodega = pb.IdPropietarioBodega inner join propietarios prop
			on pb.IdPropietario=prop.IdPropietario
 ),

SOMECTE2 as(
select min(Fecha)fecha_min,lic_plate from stock_jornada group by lic_plate
)
 SELECT 
  case
        when  ct2.fecha_min = ct.fecha_ingreso    then 'Historico completo'
        else 'Historico incompleto'
    end as estado,
	ct2.lic_plate licencia,ct.fecha_ingreso,ct2.fecha_min fecha_inicial_historico,
	DATEDIFF(DAY,ct.fecha_ingreso , ct2.fecha_min)  AS dias_pendientes,
	ct.no_ticket_tms ticket_tms,
	ct.IdOrdenCompraEnc,ct.IdRecepcionEnc,ct.cliente,ct.regimen,ct.Referencia
FROM SOMECTE2 ct2 INNER JOIN
     SomeCTE ct ON  ct.lic_plate = ct2.lic_plate
	 --order by ct.fecha_ingreso,ct2.lic_plate
```
