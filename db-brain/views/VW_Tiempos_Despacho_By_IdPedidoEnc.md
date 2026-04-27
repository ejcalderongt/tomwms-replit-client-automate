---
id: db-brain-view-vw-tiempos-despacho-by-idpedidoenc
type: db-view
title: dbo.VW_Tiempos_Despacho_By_IdPedidoEnc
schema: dbo
name: VW_Tiempos_Despacho_By_IdPedidoEnc
kind: view
modify_date: 2024-10-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tiempos_Despacho_By_IdPedidoEnc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-10-01 |
| Columnas | 7 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdPedidoEnc` | `int` |  |  |
| 3 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 4 | `Fecha_Despacho` | `datetime` | ✓ |  |
| 5 | `Cliente` | `nvarchar(301)` | ✓ |  |
| 6 | `Lineas` | `int` | ✓ |  |
| 7 | `Horas` | `numeric` | ✓ |  |

## Consume

- `cliente`
- `trans_despacho_det`
- `trans_pe_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Tiempos_Despacho_By_IdPedidoEnc]
AS
SELECT dbo.trans_pe_enc.IdBodega, dbo.trans_pe_enc.IdPedidoEnc, dbo.trans_pe_enc.Fecha_Pedido, MAX(dbo.trans_despacho_det.Fecha) AS Fecha_Despacho, dbo.cliente.codigo + ' ' + dbo.cliente.nombre_comercial AS Cliente, 
                  COUNT(dbo.trans_despacho_det.IdDespachoDet) AS Lineas, DATEDIFF(SECOND, dbo.trans_pe_enc.Fecha_Pedido, MAX(dbo.trans_despacho_det.Fecha)) / 3600.0 AS Horas
FROM     dbo.trans_despacho_det INNER JOIN
                  dbo.trans_pe_enc ON dbo.trans_despacho_det.IdPedidoEnc = dbo.trans_pe_enc.IdPedidoEnc INNER JOIN
                  dbo.cliente ON dbo.trans_pe_enc.IdCliente = dbo.cliente.IdCliente
GROUP BY dbo.trans_pe_enc.IdPedidoEnc, dbo.trans_pe_enc.Fecha_Pedido, dbo.cliente.codigo, dbo.cliente.nombre_comercial, dbo.trans_pe_enc.IdBodega
```
