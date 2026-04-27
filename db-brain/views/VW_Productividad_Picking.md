---
id: db-brain-view-vw-productividad-picking
type: db-view
title: dbo.VW_Productividad_Picking
schema: dbo
name: VW_Productividad_Picking
kind: view
modify_date: 2025-05-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Productividad_Picking`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-05-05 |
| Columnas | 28 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Fecha_Hora_Inicio` | `datetime` |  |  |
| 2 | `Fecha_Hora_Fin` | `datetime` |  |  |
| 3 | `Fecha_Por_Línea` | `datetime` |  |  |
| 4 | `Tipo_Documento_Pedido` | `nvarchar(250)` | ✓ |  |
| 5 | `Tipo` | `nvarchar(50)` |  |  |
| 6 | `Código_Departamento` | `nvarchar(50)` | ✓ |  |
| 7 | `Descripción_Departamento` | `nvarchar(50)` | ✓ |  |
| 8 | `Código_Categoría` | `nvarchar(50)` | ✓ |  |
| 9 | `Descripción_Categoría` | `nvarchar(50)` | ✓ |  |
| 10 | `Código_Producto` | `nvarchar(50)` | ✓ |  |
| 11 | `Nombre_Producto` | `nvarchar(100)` | ✓ |  |
| 12 | `Cantidad_Solicitada` | `float` | ✓ |  |
| 13 | `Cantidad_Pickeada` | `float` |  |  |
| 14 | `Nombre_Estado_Producto` | `nvarchar(50)` |  |  |
| 15 | `Cantidad_Devolución_Picking` | `float` | ✓ |  |
| 16 | `Diferencia` | `float` | ✓ |  |
| 17 | `Nombre_Presentación_MPQ` | `nvarchar(50)` |  |  |
| 18 | `Cantidad_Pickeadas_Cajas` | `float` | ✓ |  |
| 19 | `Id_Recepción` | `int` |  |  |
| 20 | `Número_Picking` | `int` |  |  |
| 21 | `Fecha_Vence` | `datetime` |  |  |
| 22 | `Lic_Plate` | `nvarchar(25)` |  |  |
| 23 | `Código_Operador` | `nvarchar(25)` |  |  |
| 24 | `Descripción_Operador` | `nvarchar(201)` |  |  |
| 25 | `Código_Comprador` | `nvarchar(150)` |  |  |
| 26 | `Descripción_Comprador` | `nvarchar(150)` |  |  |
| 27 | `Solicitud_SAP` | `nvarchar(50)` | ✓ |  |
| 28 | `IdBodega` | `int` | ✓ |  |

## Consume

- `cliente`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_presentacion`
- `producto_tipo`
- `stock`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_pe_tipo`
- `trans_picking_enc`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
--PICKING
create VIEW [dbo].[VW_Productividad_Picking]
AS
SELECT
    ISNULL(fo.Fecha_Hora_Inicio, '19000101') AS Fecha_Hora_Inicio,
    ISNULL(fo.Fecha_Hora_Fin, '19000101') AS Fecha_Hora_Fin,
    ISNULL(a.fecha_picking, '19000101') AS Fecha_Por_Línea,
    o.descripcion AS Tipo_Documento_Pedido,
    ISNULL(e.nombre, 'ND') AS Tipo,
    f.codigo AS Código_Departamento,
    f.nombre AS Descripción_Departamento,
    g.codigo AS Código_Categoría,
    g.nombretipoproducto AS Descripción_Categoría,
    d.codigo AS Código_Producto, 
    d.nombre AS Nombre_Producto,
    t.cantidad AS Cantidad_Solicitada,
    ISNULL(a.cantidad_recibida, 0) AS Cantidad_Pickeada,
    ISNULL(h.nombre, '') AS Nombre_Estado_Producto,
    ROUND((t.cantidad - ISNULL(a.cantidad_recibida, 0)),2) AS Cantidad_Devolución_Picking, 
	ROUND((a.cantidad_solicitada - a.cantidad_recibida),2) AS Diferencia, 
    ISNULL(q.nombre, '') AS Nombre_Presentación_MPQ,
    CASE 
        WHEN p.IdPresentacion > 0 AND q.factor > 0
        THEN ROUND(ISNULL(a.cantidad_recibida, 0) / q.factor, 2) 
        ELSE 0 
    END AS Cantidad_Pickeadas_Cajas,
    ISNULL(j.IdRecepcionEnc, 0) AS Id_Recepción,
    ISNULL(b.IdPickingEnc, 0) AS Número_Picking,
    ISNULL(a.fecha_vence, '19000101') AS Fecha_Vence,
    ISNULL(a.lic_plate, '') AS Lic_Plate,
    ISNULL(l.codigo, 'Operador BOF') AS Código_Operador,
    ISNULL(l.nombres, 'Operador BOF') + ' ' + ISNULL(l.apellidos, '') AS Descripción_Operador,
    ISNULL(n.codigo, '') AS Código_Comprador,
    ISNULL(n.nombre_comercial, '') AS Descripción_Comprador,
    m.Referencia_Documento_Ingreso_Bodega_Destino AS Solicitud_SAP,
	m.IdBodega
FROM trans_pe_enc m WITH (NOLOCK)
INNER JOIN trans_pe_det t WITH (NOLOCK) ON t.IdPedidoEnc = m.IdPedidoEnc
INNER JOIN trans_pe_tipo o  WITH (NOLOCK) ON o.IdTipoPedido = m.IdTipoPedido 
INNER JOIN producto_bodega c  WITH (NOLOCK) ON c.IdProductoBodega = t.IdProductoBodega
INNER JOIN producto d  WITH (NOLOCK) ON d.IdProducto = c.IdProducto
LEFT JOIN producto_familia e  WITH (NOLOCK) ON e.IdFamilia = d.IdFamilia
LEFT JOIN producto_clasificacion f WITH (NOLOCK)  ON f.IdClasificacion = d.IdClasificacion
LEFT JOIN producto_tipo g  WITH (NOLOCK) ON g.IdTipoProducto = d.IdTipoProducto
LEFT JOIN trans_picking_ubic a WITH (NOLOCK)  ON t.IdPedidoDet = a.IdPedidoDet AND t.IdProductoBodega = a.IdProductoBodega
LEFT JOIN trans_picking_enc b WITH (NOLOCK)  ON b.IdPickingEnc = a.IdPickingEnc
LEFT JOIN producto_estado h WITH (NOLOCK)  ON h.IdEstado = a.IdProductoEstado
LEFT JOIN (
    SELECT 
        IdOperadorBodega_Pickeo,
        IdPickingEnc,
        MIN(fecha_picking) AS Fecha_Hora_Inicio,
        MAX(fecha_picking) AS Fecha_Hora_Fin 
    FROM trans_picking_ubic WITH (NOLOCK) 
    WHERE no_encontrado = 0 AND dañado_picking = 0
    GROUP BY IdOperadorBodega_Pickeo, IdPickingEnc
) fo ON fo.IdOperadorBodega_Pickeo = a.IdOperadorBodega_Pickeo AND fo.IdPickingEnc = a.IdPickingEnc
LEFT JOIN cliente n  WITH (NOLOCK) ON n.codigo COLLATE Modern_Spanish_CI_AS = m.bodega_destino COLLATE Modern_Spanish_CI_AS
LEFT JOIN stock j  WITH (NOLOCK) ON j.IdStock = a.IdStock
LEFT JOIN operador_bodega k  WITH (NOLOCK) ON k.IdOperadorBodega = a.IdOperadorBodega_Pickeo
LEFT JOIN operador l WITH (NOLOCK)  ON l.IdOperador = k.IdOperador
LEFT JOIN (
    SELECT MAX(IdPresentacion) IdPresentacion, IdProducto 
	FROM producto_presentacion  WITH (NOLOCK) GROUP BY IdProducto) p ON c.IdProducto = p.IdProducto AND 
	                                                                    d.IdProducto = p.IdProducto
LEFT JOIN producto_presentacion q  WITH (NOLOCK) ON p.IdPresentacion = q.IdPresentacion  
WHERE m.estado <> 'Anulado'
AND m.ubicacion <> 'TMP'
```
