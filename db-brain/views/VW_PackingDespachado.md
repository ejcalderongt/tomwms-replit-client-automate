---
id: db-brain-view-vw-packingdespachado
type: db-view
title: dbo.VW_PackingDespachado
schema: dbo
name: VW_PackingDespachado
kind: view
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_PackingDespachado`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-02-11 |
| Columnas | 22 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Correlativo` | `int` |  |  |
| 2 | `Id_Despacho` | `int` | ✓ |  |
| 3 | `Id_Pedido` | `int` |  |  |
| 4 | `Referencia_Pedido` | `nvarchar(78)` |  |  |
| 5 | `Fecha_Packing` | `date` |  |  |
| 6 | `Nombre_Cliente` | `nvarchar(150)` | ✓ |  |
| 7 | `Bodega_Destino` | `nvarchar(203)` |  |  |
| 8 | `Licencia_Packing` | `int` |  |  |
| 9 | `Licencia` | `nvarchar(50)` | ✓ |  |
| 10 | `IdProductoBodega` | `int` |  |  |
| 11 | `Nombre_Producto` | `nvarchar(100)` | ✓ |  |
| 12 | `IdUnidadMedida` | `int` |  |  |
| 13 | `Unidad_Medida` | `nvarchar(50)` | ✓ |  |
| 14 | `Presentacion` | `varchar(1)` |  |  |
| 15 | `Cantidad` | `float` |  |  |
| 16 | `IdProductoEstado` | `int` |  |  |
| 17 | `Producto_Estado` | `nvarchar(50)` | ✓ |  |
| 18 | `Fecha_Vence` | `date` | ✓ |  |
| 19 | `IdBodega` | `int` |  |  |
| 20 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 21 | `IdOperadorBodega` | `int` |  |  |
| 22 | `Operador` | `nvarchar(201)` | ✓ |  |

## Consume

- `bodega`
- `cliente`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_estado`
- `trans_packing_enc`
- `trans_pe_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view  VW_PackingDespachado as
SELECT  
packing_enc.idpackingenc Correlativo,
packing_enc.iddespachoenc [Id_Despacho],
packing_enc.IdPedidoEnc [Id_Pedido],
CONCAT(pe_enc.no_documento_externo,' - ',pe_enc.referencia)  [Referencia_Pedido],
packing_enc.Fecha_Packing, 
cl.nombre_comercial [Nombre_Cliente],
CASE WHEN pe_enc.bodega_destino IS NULL OR pe_enc.bodega_destino = '' THEN '' ELSE ISNULL(concat(pe_enc.bodega_destino,' - ', c1.nombre_comercial collate Modern_Spanish_CI_AS),'') END Bodega_Destino,
packing_enc.no_linea [Licencia_Packing],
packing_enc.lic_plate Licencia,
packing_enc.IdProductoBodega, 
prod.nombre [Nombre_Producto],
packing_enc.IdUnidadMedida, 
um.Nombre [Unidad_Medida],
'' [Presentacion],
packing_enc.cantidad_bultos_packing Cantidad,
packing_enc.IdProductoEstado, 
pe.nombre [Producto_Estado],
packing_enc.Fecha_Vence,
packing_enc.IdBodega, 
bd.nombre [Bodega],
packing_enc.IdOperadorBodega, 
op.nombres +' '+op.apellidos [Operador]
FROM 
trans_packing_enc packing_enc inner join 
trans_pe_enc pe_enc on packing_enc.IdPedidoEnc=pe_enc.IdPedidoEnc inner join
cliente cl on pe_enc.IdCliente=cl.IdCliente inner join
producto_bodega pb on packing_enc.idproductobodega=pb.IdProductoBodega inner join
producto prod on pb.IdProducto=prod.IdProducto inner join 
unidad_medida um on packing_enc.idunidadmedida=um.IdUnidadMedida inner join
producto_estado pe on packing_enc.idproductoestado= pe.IdEstado inner join
bodega bd on packing_enc.idbodega = bd.IdBodega inner join 
operador_bodega ob on packing_enc.idoperadorbodega = ob.IdOperadorBodega inner join
operador op on ob.IdOperador= op.IdOperador LEFT JOIN 
cliente c1 ON c1.codigo COLLATE Modern_Spanish_CI_AS = pe_enc.bodega_destino
```
