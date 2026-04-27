---
id: db-brain-view-vw-indicador-despachos
type: db-view
title: dbo.vw_Indicador_Despachos
schema: dbo
name: vw_Indicador_Despachos
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.vw_Indicador_Despachos`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 15 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `unidades` | `float` | ✓ |  |
| 2 | `codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `producto` | `nvarchar(100)` | ✓ |  |
| 4 | `presentacion` | `nvarchar(50)` |  |  |
| 5 | `factor` | `float` |  |  |
| 6 | `IdDespachoEnc` | `int` |  |  |
| 7 | `IdDespachoDet` | `int` |  |  |
| 8 | `cajas/bultos` | `float` | ✓ |  |
| 9 | `fecha` | `date` | ✓ |  |
| 10 | `nombre_comercial` | `nvarchar(100)` |  |  |
| 11 | `cliente` | `nvarchar(150)` | ✓ |  |
| 12 | `bodega` | `nvarchar(50)` | ✓ |  |
| 13 | `IdTipoPedido` | `int` | ✓ |  |
| 14 | `tipo_salida` | `nvarchar(250)` | ✓ |  |
| 15 | `IdBodega` | `int` |  |  |

## Consume

- `bodega`
- `cliente`
- `producto`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_pe_enc`
- `trans_pe_tipo`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
create view vw_Indicador_Despachos as
select case when factor > 0 then round(sum(CantidadDespachada)*factor,4) else  sum(CantidadDespachada) end unidades,
despacho_det.codigo,prod.nombre producto,isnull(presentacion.nombre,'') presentacion,ISNULL(presentacion.factor,0) factor,
despacho_det.IdDespachoEnc,despacho_det.IdDespachoDet, 
case when factor > 0 then round(sum(CantidadDespachada),4) else 0  end [cajas/bultos]
, cast(enc.fecha as date) fecha,pr.nombre_comercial,cl.nombre_comercial cliente,bd.nombre bodega
,pe_enc.IdTipoPedido, doc_salida.Descripcion tipo_salida, bd.IdBodega
from trans_despacho_det despacho_det left outer join producto_presentacion presentacion
                                     on despacho_det.IdPresentacion = presentacion.IdPresentacion
                                     INNER JOIN trans_despacho_enc enc on despacho_det.IdDespachoEnc = enc.IdDespachoEnc
                                     INNER JOIN bodega bd on enc.IdBodega=bd.IdBodega
                                     INNER JOIN producto prod on despacho_det.Codigo = prod.codigo
									 INNER JOIN  propietario_bodega pb on enc.IdPropietarioBodega = pb.IdPropietarioBodega
									 INNER JOIN propietarios pr on pb.IdPropietario= pr.IdPropietario
									 INNER JOIN trans_pe_enc pe_enc on despacho_det.IdPedidoEnc = pe_enc.IdPedidoEnc
									 INNER JOIN cliente cl on pe_enc.IdCliente= cl.IdCliente
									 INNER JOIN trans_pe_tipo doc_salida on pe_enc.IdTipoPedido=doc_salida.IdTipoPedido
									 
where enc.activo=1 
group by CantidadDespachada,despacho_det.Codigo,presentacion.nombre,presentacion.factor,despacho_det.IdDespachoEnc,enc.fecha,
prod.nombre,pr.nombre_comercial,cl.nombre_comercial,bd.nombre,despacho_det.IdDespachoDet,pe_enc.IdTipoPedido,doc_salida.Descripcion, bd.IdBodega
```
