---
id: db-brain-view-vw-estado-envios-nav
type: db-view
title: dbo.VW_Estado_Envios_Nav
schema: dbo
name: VW_Estado_Envios_Nav
kind: view
modify_date: 2018-06-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Estado_Envios_Nav`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-06-25 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `FECHA` | `date` | ✓ |  |
| 2 | `TIPO` | `varchar(2)` |  |  |
| 3 | `ESTADO` | `varchar(11)` |  |  |
| 4 | `CANTIDAD` | `int` | ✓ |  |

## Consume

- `trans_oc_enc`
- `trans_pe_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Estado_Envios_Nav]
AS
SELECT CONVERT(DATE, Fecha_Pedido) AS FECHA, 'PT' AS TIPO, CASE WHEN Enviado_A_ERP = 1 THEN 'ENVIADOS' ELSE 'NO ENVIADOS' END AS ESTADO, COUNT(IdPedidoEnc) as CANTIDAD
FROM trans_pe_enc
GROUP BY enviado_a_erp, CONVERT(DATE, Fecha_Pedido)
UNION
SELECT CONVERT(DATE, Fecha_Recepcion) AS FECHA,'PC' AS TIPO, CASE WHEN Enviado_A_ERP = 1 THEN 'ENVIADOS' ELSE 'NO ENVIADOS' END AS ESTADO, COUNT(IdOrdenCompraEnc) as CANTIDAD
FROM trans_oc_enc
WHERE IdOrdenCompraEnc IN (SELECT IdOrdenCompraEnc FROM trans_re_oc)
GROUP BY Enviado_A_ERP, CONVERT(DATE, Fecha_Recepcion)
```
