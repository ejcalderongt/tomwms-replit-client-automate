---
id: db-brain-view-vw-tareas-activas-hh
type: db-view
title: dbo.VW_Tareas_Activas_HH
schema: dbo
name: VW_Tareas_Activas_HH
kind: view
modify_date: 2025-08-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tareas_Activas_HH`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-08-02 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Correlativo` | `int` | ✓ |  |
| 2 | `Tarea` | `varchar(13)` | ✓ |  |
| 3 | `Inicio` | `datetime` | ✓ |  |
| 4 | `Ult_Revision` | `datetime` |  |  |
| 5 | `TTM` | `int` | ✓ |  |
| 6 | `Propietario` | `nvarchar(100)` |  |  |
| 7 | `Estado` | `nvarchar(50)` | ✓ |  |
| 8 | `IdTareahh` | `int` |  |  |
| 9 | `IdBodega` | `int` | ✓ |  |
| 10 | `Observacion` | `nvarchar(max)` | ✓ |  |

## Consume

- `propietarios`
- `sis_estado_tarea_hh`
- `sis_tipo_tarea`
- `tarea_hh`
- `trans_oc_enc`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_re_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW VW_Tareas_Activas_HH
AS
SELECT 
    t.IdTransaccion AS Correlativo, 
    CASE ta.Nombre 
        WHEN 'RECE' THEN 'Recepción' 
        WHEN 'UBIC' THEN 'Ubicación' 
        WHEN 'CEST' THEN 'Cambio Estado' 
        WHEN 'TRS'  THEN 'Traslado' 
        WHEN 'DES'  THEN 'Despacho' 
        WHEN 'INVE' THEN 'Inventario' 
        WHEN 'AJU'  THEN 'Ajuste' 
        WHEN 'PIK'  THEN 'Picking' 
    END AS Tarea,
    
    t.FechaInicio AS Inicio,
    GETDATE() AS Ult_Revision,
    DATEDIFF(MINUTE, t.FechaInicio, GETDATE()) AS TTM,
    ISNULL(p.nombre_comercial, 'MULTIPROPIETARIO') AS Propietario,
    e.descripcion AS Estado,
    t.IdTareahh,
    t.IdBodega,

    -- Observación según tipo de tarea
    CASE 
        WHEN ta.Nombre = 'PIK' THEN 
            CASE 
                WHEN pk.observacion IS NOT NULL 
                     AND LTRIM(RTRIM(CAST(pk.observacion AS VARCHAR(MAX)))) <> '' 
                    THEN CAST(pk.observacion AS VARCHAR(MAX))
                WHEN sub_ped.CantidadPedidos = 1 
                    THEN sub_ped.ReferenciaUnica
                ELSE 'MULTIPEDIDO'
            END

        WHEN ta.Nombre = 'RECE' THEN 
            CASE 
                WHEN re.observacion IS NOT NULL 
                     AND LTRIM(RTRIM(CAST(re.observacion AS VARCHAR(MAX)))) <> '' 
                    THEN CAST(re.observacion AS VARCHAR(MAX))
                WHEN oc.Referencia IS NOT NULL THEN 
                    RTRIM(LTRIM(CAST(oc.Referencia AS VARCHAR(MAX)))) + ' ' + 
                    ISNULL(RTRIM(LTRIM(CAST(oc.Observacion AS VARCHAR(MAX)))), '')
                ELSE NULL
            END

        ELSE NULL
    END AS Observacion

FROM 
    dbo.tarea_hh AS t
    LEFT JOIN dbo.propietarios AS p 
        ON t.IdPropietario = p.IdPropietario
    LEFT JOIN dbo.sis_estado_tarea_hh AS e 
        ON t.IdEstado = e.IdEstado
    LEFT JOIN dbo.sis_tipo_tarea AS ta 
        ON t.IdTipoTarea = ta.IdTipoTarea

    -- JOIN condicional para Picking
    LEFT JOIN dbo.trans_picking_enc AS pk 
        ON ta.Nombre = 'PIK' AND t.IdTransaccion = pk.IdPickingEnc

    -- JOIN condicional para Recepción
    LEFT JOIN dbo.trans_re_enc AS re 
        ON ta.Nombre = 'RECE' AND t.IdTransaccion = re.IdRecepcionEnc
    LEFT JOIN dbo.trans_re_oc AS reoc 
        ON ta.Nombre = 'RECE' AND t.IdTransaccion = reoc.IdRecepcionEnc
    LEFT JOIN dbo.trans_oc_enc AS oc 
        ON reoc.IdOrdenCompraEnc = oc.IdOrdenCompraEnc

    -- OUTER APPLY: Determinar número de pedidos asociados al Picking
    OUTER APPLY (
        SELECT 
            COUNT(DISTINCT pd.IdPedidoEnc) AS CantidadPedidos,
            MAX(pe.referencia) AS ReferenciaUnica
        FROM dbo.trans_picking_det AS pd
        INNER JOIN dbo.trans_pe_enc AS pe 
            ON pd.IdPedidoEnc = pe.IdPedidoEnc
        WHERE pd.IdPickingEnc = t.IdTransaccion
    ) AS sub_ped

WHERE 
    e.IdEstado NOT IN (3, 4)
```
