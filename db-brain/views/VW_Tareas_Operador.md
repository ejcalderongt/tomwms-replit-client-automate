---
id: db-brain-view-vw-tareas-operador
type: db-view
title: dbo.VW_Tareas_Operador
schema: dbo
name: VW_Tareas_Operador
kind: view
modify_date: 2025-05-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tareas_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-05-05 |
| Columnas | 5 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `TipoTarea` | `nvarchar(50)` | ✓ |  |
| 3 | `IdTarea` | `int` | ✓ |  |
| 4 | `Fecha` | `date` | ✓ |  |
| 5 | `NombreOperador` | `nvarchar(201)` | ✓ |  |

## Consume

- `menu_rol_op`
- `menu_sistema_op`
- `operador`
- `operador_bodega`
- `sis_tipo_tarea`
- `tarea_hh`
- `trans_inv_enc`
- `trans_inv_operador`
- `trans_picking_op`
- `trans_picking_ubic`
- `trans_re_op`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
create VIEW VW_Tareas_Operador
AS
SELECT 
	thh.IdBodega,
    stt.Nombre AS TipoTarea,
    thh.IdTransaccion IdTarea,
    CONVERT(DATE, thh.FechaInicio) Fecha,
    op.nombres + ' ' + op.apellidos AS NombreOperador
FROM dbo.tarea_hh thh
INNER JOIN dbo.sis_tipo_tarea stt ON thh.IdTipoTarea = stt.IdTipoTarea
INNER JOIN dbo.trans_re_op treop ON stt.Nombre = 'RECE' AND treop.IdRecepcionEnc = thh.IdTransaccion
INNER JOIN dbo.operador_bodega ob ON treop.IdOperadorBodega = ob.IdOperadorBodega
INNER JOIN dbo.operador op ON ob.IdOperador = op.IdOperador

UNION ALL

SELECT 
	thh.IdBodega,
    stt.Nombre AS TipoTarea,
    thh.IdTransaccion IdTarea,
    CONVERT(DATE, thh.FechaInicio) Fecha,
    op.nombres + ' ' + op.apellidos AS NombreOperador
FROM dbo.tarea_hh thh
INNER JOIN dbo.sis_tipo_tarea stt ON thh.IdTipoTarea = stt.IdTipoTarea
INNER JOIN dbo.trans_picking_op tpop ON stt.Nombre = 'PIK' AND tpop.IdPickingEnc = thh.IdTransaccion
INNER JOIN dbo.operador_bodega ob ON tpop.IdOperadorBodega = ob.IdOperadorBodega
INNER JOIN dbo.operador op ON ob.IdOperador = op.IdOperador

UNION ALL

SELECT distinct
	thh.IdBodega,
    stt.Nombre AS TipoTarea,
    thh.IdTransaccion IdTarea,
    CONVERT(DATE, thh.FechaInicio) Fecha,
    op.nombres + ' ' + op.apellidos AS NombreOperador
FROM dbo.tarea_hh thh
INNER JOIN dbo.sis_tipo_tarea stt ON thh.IdTipoTarea = stt.IdTipoTarea
INNER JOIN dbo.trans_inv_enc tie ON stt.Nombre = 'INVE' AND tie.IdInventarioEnc = thh.IdTransaccion
INNER JOIN dbo.trans_inv_operador tio ON tio.IdInventarioEnc = tie.IdInventarioEnc
INNER JOIN dbo.operador op ON tio.IdOperador = op.IdOperador

UNION ALL

SELECT 
	ob.IdBodega,
    'VERI' AS TipoTarea,
    tpu.IdPedidoEnc AS IdTarea,
    CONVERT(DATE, tpu.fecha_verificado) AS Fecha,
    op.nombres + ' ' + op.apellidos AS NombreOperador
FROM dbo.trans_picking_op tpop
INNER JOIN dbo.operador_bodega ob ON tpop.IdOperadorBodega = ob.IdOperadorBodega
INNER JOIN dbo.operador op ON ob.IdOperador = op.IdOperador
INNER JOIN dbo.menu_rol_op mro ON op.IdRolOperador = mro.IdRolOperador
INNER JOIN dbo.menu_sistema_op mso ON mro.IdMenuSistemaOP = mso.IdMenuSistemaOP
INNER JOIN dbo.trans_picking_ubic tpu ON tpu.IdPickingEnc = tpop.IdPickingEnc
WHERE 
    op.verifica = 1 
    AND mso.IdTipoTarea = 11
    AND tpu.fecha_verificado IS NOT NULL
GROUP BY 
    tpu.IdPedidoEnc, op.nombres, op.apellidos, ob.IdBodega,CONVERT(DATE, tpu.fecha_verificado)
```
