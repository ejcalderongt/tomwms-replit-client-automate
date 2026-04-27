---
id: db-brain-view-vw-cantidad-pedidos-vrs-despacho-clientes
type: db-view
title: dbo.VW_Cantidad_Pedidos_vrs_Despacho_Clientes
schema: dbo
name: VW_Cantidad_Pedidos_vrs_Despacho_Clientes
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Cantidad_Pedidos_vrs_Despacho_Clientes`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 7 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Codigo_Bodega` | `nvarchar(50)` | ✓ |  |
| 2 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 3 | `Cliente` | `nvarchar(150)` | ✓ |  |
| 4 | `Pedidos` | `int` | ✓ |  |
| 5 | `Despachados` | `int` | ✓ |  |
| 6 | `Diferencia` | `int` | ✓ |  |
| 7 | `IdBodega` | `int` | ✓ |  |

## Consume

- `bodega`
- `cliente`
- `trans_pe_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Cantidad_Pedidos_vrs_Despacho_Clientes]
AS
SELECT        dbo.bodega.codigo AS Codigo_Bodega, pe.Fecha_Pedido, c.nombre_comercial AS Cliente, COUNT(pe.IdPedidoEnc) AS Pedidos, SUM(CASE WHEN pe.estado = 'Despachado' THEN 1 ELSE 0 END) AS Despachados, 
                         SUM(CASE WHEN pe.estado <> 'Despachado' THEN 1 ELSE 0 END) AS Diferencia, pe.IdBodega
FROM            dbo.trans_pe_enc AS pe INNER JOIN
                         dbo.cliente AS c ON pe.IdCliente = c.IdCliente INNER JOIN
                         dbo.bodega ON pe.IdBodega = dbo.bodega.IdBodega
WHERE        (pe.anulado = 0)
GROUP BY c.nombre_comercial, pe.IdBodega, dbo.bodega.codigo, pe.Fecha_Pedido
```
