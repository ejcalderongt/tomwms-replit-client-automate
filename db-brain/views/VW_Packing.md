---
id: db-brain-view-vw-packing
type: db-view
title: dbo.VW_Packing
schema: dbo
name: VW_Packing
kind: view
modify_date: 2024-10-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Packing`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-10-01 |
| Columnas | 29 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idpickingenc` | `int` |  |  |
| 2 | `idpackingenc` | `int` |  |  |
| 3 | `no_linea` | `int` |  |  |
| 4 | `codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `nombre` | `nvarchar(100)` | ✓ |  |
| 6 | `lote` | `nvarchar(50)` | ✓ |  |
| 7 | `fecha_vence` | `date` | ✓ |  |
| 8 | `umbas` | `nvarchar(50)` | ✓ |  |
| 9 | `presentacion` | `nvarchar(50)` | ✓ |  |
| 10 | `estado` | `nvarchar(50)` | ✓ |  |
| 11 | `licencia` | `nvarchar(50)` | ✓ |  |
| 12 | `cantidad_bultos_packing` | `float` |  |  |
| 13 | `cantidad_camas_packing` | `float` |  |  |
| 14 | `fecha_packing` | `date` |  |  |
| 15 | `operador` | `nvarchar(100)` | ✓ |  |
| 16 | `iddespachoenc` | `int` | ✓ |  |
| 17 | `marchamo` | `nvarchar(50)` | ✓ |  |
| 18 | `placa` | `nvarchar(20)` | ✓ |  |
| 19 | `nombre_piloto` | `nvarchar(150)` | ✓ |  |
| 20 | `apellido_piloto` | `nvarchar(150)` | ✓ |  |
| 21 | `no_licencia` | `nvarchar(50)` | ✓ |  |
| 22 | `placa_comercial` | `nvarchar(50)` | ✓ |  |
| 23 | `marca` | `nvarchar(50)` | ✓ |  |
| 24 | `modelo` | `nvarchar(50)` | ✓ |  |
| 25 | `propietario` | `nvarchar(100)` |  |  |
| 26 | `imagen` | `image` | ✓ |  |
| 27 | `referencia` | `nvarchar(50)` | ✓ |  |
| 28 | `IdPedidoEnc` | `int` |  |  |
| 29 | `Finalizado` | `bit` |  |  |

## Consume

- `empresa`
- `empresa_transporte_pilotos`
- `empresa_transporte_vehiculos`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietarios`
- `trans_despacho_enc`
- `trans_packing_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Packing]
AS
SELECT        dbo.trans_packing_enc.idpickingenc, dbo.trans_packing_enc.idpackingenc, dbo.trans_packing_enc.no_linea, dbo.producto.codigo, dbo.producto.nombre, dbo.trans_packing_enc.lote, dbo.trans_packing_enc.fecha_vence, 
                         dbo.unidad_medida.Nombre AS umbas, dbo.producto_presentacion.nombre AS presentacion, dbo.producto_estado.nombre AS estado, dbo.trans_packing_enc.lic_plate AS licencia, 
                         dbo.trans_packing_enc.cantidad_bultos_packing, dbo.trans_packing_enc.cantidad_camas_packing, dbo.trans_packing_enc.fecha_packing, dbo.operador.nombres AS operador, dbo.trans_packing_enc.iddespachoenc, 
                         dbo.trans_despacho_enc.marchamo, dbo.empresa_transporte_vehiculos.placa, dbo.empresa_transporte_pilotos.nombres AS nombre_piloto, dbo.empresa_transporte_pilotos.apellidos AS apellido_piloto, 
                         dbo.empresa_transporte_pilotos.no_licencia, dbo.empresa_transporte_vehiculos.placa_comercial, dbo.empresa_transporte_vehiculos.marca, dbo.empresa_transporte_vehiculos.modelo, 
                         dbo.propietarios.nombre_comercial AS propietario, dbo.empresa.imagen, dbo.trans_packing_enc.referencia, trans_packing_enc.IdPedidoEnc,trans_packing_enc.Finalizado
FROM            dbo.producto_presentacion RIGHT OUTER JOIN
                         dbo.trans_packing_enc INNER JOIN
                         dbo.producto_bodega ON dbo.trans_packing_enc.idproductobodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                         dbo.unidad_medida ON dbo.trans_packing_enc.idunidadmedida = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                         dbo.operador_bodega ON dbo.trans_packing_enc.idoperadorbodega = dbo.operador_bodega.IdOperadorBodega INNER JOIN
                         dbo.operador ON dbo.operador_bodega.IdOperador = dbo.operador.IdOperador INNER JOIN
                         dbo.propietarios ON dbo.producto.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
                         dbo.empresa ON dbo.operador.IdEmpresa = dbo.empresa.IdEmpresa AND dbo.propietarios.IdEmpresa = dbo.empresa.IdEmpresa ON 
                         dbo.producto_presentacion.IdPresentacion = dbo.trans_packing_enc.idpresentacion LEFT OUTER JOIN
                         dbo.empresa_transporte_vehiculos INNER JOIN
                         dbo.trans_despacho_enc ON dbo.empresa_transporte_vehiculos.IdVehiculo = dbo.trans_despacho_enc.IdVehiculo ON dbo.trans_packing_enc.iddespachoenc = dbo.trans_despacho_enc.IdDespachoEnc LEFT OUTER JOIN
                         dbo.producto_estado ON dbo.trans_packing_enc.idproductoestado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                         dbo.empresa_transporte_pilotos ON dbo.trans_despacho_enc.IdPiloto = dbo.empresa_transporte_pilotos.IdPiloto
```
