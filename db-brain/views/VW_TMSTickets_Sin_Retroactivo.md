---
id: db-brain-view-vw-tmstickets-sin-retroactivo
type: db-view
title: dbo.VW_TMSTickets_Sin_Retroactivo
schema: dbo
name: VW_TMSTickets_Sin_Retroactivo
kind: view
modify_date: 2022-08-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_TMSTickets_Sin_Retroactivo`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-08-12 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStock` | `int` |  |  |
| 2 | `IdTicketTMS` | `int` | ✓ |  |
| 3 | `Fecha_Creacion_Documento` | `datetime` | ✓ |  |
| 4 | `Fecha_Ingreso_Ticket` | `datetime` | ✓ |  |

## Consume

- `stock`
- `stock_jornada`
- `tms_ticket`
- `trans_oc_enc`
- `trans_re_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_TMSTickets_Sin_Retroactivo]
AS
SELECT DISTINCT dbo.stock.IdStock, dbo.trans_oc_enc.no_ticket_tms AS IdTicketTMS, dbo.trans_oc_enc.Fec_Agr AS Fecha_Creacion_Documento, dbo.tms_ticket.Fecha_Ingreso AS Fecha_Ingreso_Ticket
FROM     dbo.stock INNER JOIN
                  dbo.trans_re_enc ON dbo.stock.IdRecepcionEnc = dbo.trans_re_enc.IdRecepcionEnc AND dbo.stock.IdBodega = dbo.trans_re_enc.IdBodega INNER JOIN
                  dbo.trans_re_oc ON dbo.trans_re_enc.IdRecepcionEnc = dbo.trans_re_oc.IdRecepcionEnc INNER JOIN
                  dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc LEFT OUTER JOIN
                  dbo.tms_ticket ON dbo.trans_oc_enc.no_ticket_tms = dbo.tms_ticket.IdTicket
WHERE  (dbo.trans_oc_enc.no_ticket_tms NOT IN
                      (SELECT IdTicketTMS
                       FROM      dbo.stock_jornada))
GROUP BY dbo.trans_oc_enc.no_ticket_tms, dbo.stock.IdStock, dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_oc_enc.Fec_Agr, dbo.tms_ticket.Fecha_Ingreso
```
