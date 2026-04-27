---
id: db-brain-view-vw-pedidos-idpickingenc
type: db-view
title: dbo.VW_Pedidos_IdPickingEnc
schema: dbo
name: VW_Pedidos_IdPickingEnc
kind: view
modify_date: 2025-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Pedidos_IdPickingEnc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-07-02 |
| Columnas | 11 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingEnc` | `int` | ✓ |  |
| 2 | `IdPedidoEnc` | `int` |  |  |
| 3 | `Referencia` | `nvarchar(25)` | ✓ |  |
| 4 | `Documento` | `nvarchar(50)` | ✓ |  |
| 5 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 6 | `Cliente` | `nvarchar(150)` | ✓ |  |
| 7 | `Propietario` | `nvarchar(100)` |  |  |
| 8 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 9 | `estado` | `nvarchar(20)` | ✓ |  |
| 10 | `Observacion` | `nvarchar(255)` | ✓ |  |
| 11 | `Dirección` | `nvarchar(255)` | ✓ |  |

## Consume

- `bodega`
- `cliente`
- `propietario_bodega`
- `propietarios`
- `trans_pe_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Pedidos_IdPickingEnc]
AS
SELECT penc.IdPickingEnc, penc.IdPedidoEnc, penc.referencia Referencia, 
       penc.Referencia_Documento_Ingreso_Bodega_Destino Documento,
       b.nombre AS Bodega, c.nombre_comercial AS Cliente, 
       pr.nombre_comercial AS Propietario, penc.Fecha_Pedido, penc.estado,
	   penc.Observacion, penc.RoadDirEntrega Dirección
FROM  dbo.trans_pe_enc AS penc INNER JOIN
dbo.propietario_bodega AS prb ON penc.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
dbo.bodega AS b ON penc.IdBodega = b.IdBodega INNER JOIN
dbo.cliente AS c ON penc.IdCliente = c.IdCliente
WHERE penc.estado <>'Anulado' and penc.IdPickingEnc<>0
```
