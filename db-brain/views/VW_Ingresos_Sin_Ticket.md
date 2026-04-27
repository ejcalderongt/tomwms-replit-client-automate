---
id: db-brain-view-vw-ingresos-sin-ticket
type: db-view
title: dbo.VW_Ingresos_Sin_Ticket
schema: dbo
name: VW_Ingresos_Sin_Ticket
kind: view
modify_date: 2023-01-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Ingresos_Sin_Ticket`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-01-11 |
| Columnas | 3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStock` | `int` |  |  |
| 2 | `licencia` | `nvarchar(50)` | ✓ |  |
| 3 | `Fecha_Ingreso` | `date` | ✓ |  |

## Consume

- `stock`
- `stock_jornada`
- `trans_oc_enc`
- `trans_re_det`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view [dbo].[VW_Ingresos_Sin_Ticket] as
SELECT 
dbo.stock.IdStock, 
dbo.trans_re_det.lic_plate as licencia,
CAST(dbo.trans_re_det.fecha_ingreso AS date) Fecha_Ingreso
FROM     dbo.stock INNER JOIN dbo.trans_re_det on dbo.stock.lic_plate=dbo.trans_re_det.lic_plate
												  and dbo.stock.IdRecepcionEnc=dbo.trans_re_det.IdRecepcionEnc
												  and dbo.stock.IdRecepcionDet= dbo.trans_re_det.IdRecepcionDet
                  INNER JOIN dbo.trans_oc_enc ON dbo.trans_re_det.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc 
				  LEFT JOIN dbo.stock_jornada ON dbo.trans_re_det.lic_plate = dbo.stock_jornada.lic_plate 
												 and  CAST(dbo.trans_re_det.fecha_ingreso AS date) = dbo.stock_jornada.Fecha
WHERE
(dbo.trans_oc_enc.no_ticket_tms=0 or dbo.trans_oc_enc.no_ticket_tms='')
and dbo.stock_jornada.lic_plate is null
```
