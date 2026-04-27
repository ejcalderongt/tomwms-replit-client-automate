---
id: db-brain-view-vw-trans-oc-det
type: db-view
title: dbo.VW_TRANS_OC_DET
schema: dbo
name: VW_TRANS_OC_DET
kind: view
modify_date: 2018-04-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_TRANS_OC_DET`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-04-26 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` |  |  |
| 2 | `No_Documento` | `nvarchar(30)` | ✓ |  |
| 3 | `Fecha_Creacion` | `datetime` | ✓ |  |
| 4 | `No_Linea` | `int` | ✓ |  |
| 5 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 6 | `nombre_producto` | `nvarchar(50)` | ✓ |  |
| 7 | `cantidad` | `float` | ✓ |  |
| 8 | `cantidad_recibida` | `float` | ✓ |  |
| 9 | `Cantidad_Pendiente` | `float` | ✓ |  |
| 10 | `UM` | `nvarchar(50)` | ✓ |  |

## Consume

- `trans_oc_det`
- `trans_oc_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_TRANS_OC_DET]
AS
SELECT     dbo.trans_oc_enc.IdOrdenCompraEnc, dbo.trans_oc_enc.No_Documento, dbo.trans_oc_enc.Fecha_Creacion, dbo.trans_oc_det.No_Linea, 
                      dbo.trans_oc_det.codigo_producto, dbo.trans_oc_det.nombre_producto, dbo.trans_oc_det.cantidad, dbo.trans_oc_det.cantidad_recibida, 
                      dbo.trans_oc_det.cantidad - dbo.trans_oc_det.cantidad_recibida AS Cantidad_Pendiente, dbo.trans_oc_det.nombre_unidad_medida_basica AS UM
FROM         dbo.trans_oc_enc INNER JOIN
                      dbo.trans_oc_det ON dbo.trans_oc_enc.IdOrdenCompraEnc = dbo.trans_oc_det.IdOrdenCompraEnc
```
