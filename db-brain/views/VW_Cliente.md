---
id: db-brain-view-vw-cliente
type: db-view
title: dbo.VW_Cliente
schema: dbo
name: VW_Cliente
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Cliente`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 30 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 2 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 3 | `Tipo Cliente` | `nvarchar(50)` | ✓ |  |
| 4 | `activo_bodega` | `bit` | ✓ |  |
| 5 | `IdCliente` | `int` |  |  |
| 6 | `IdEmpresa` | `int` |  |  |
| 7 | `IdPropietario` | `int` |  |  |
| 8 | `IdTipoCliente` | `int` |  |  |
| 9 | `IdUbicacionManufactura` | `int` | ✓ |  |
| 10 | `codigo` | `nvarchar(150)` | ✓ |  |
| 11 | `nombre_comercial` | `nvarchar(150)` | ✓ |  |
| 12 | `nombre_contacto` | `nvarchar(150)` | ✓ |  |
| 13 | `telefono` | `nvarchar(125)` | ✓ |  |
| 14 | `nit` | `nvarchar(125)` | ✓ |  |
| 15 | `direccion` | `nvarchar(250)` | ✓ |  |
| 16 | `correo_electronico` | `nvarchar(150)` | ✓ |  |
| 17 | `activo` | `bit` | ✓ |  |
| 18 | `realiza_manufactura` | `bit` | ✓ |  |
| 19 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 20 | `fec_agr` | `datetime` | ✓ |  |
| 21 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 22 | `fec_mod` | `datetime` | ✓ |  |
| 23 | `despachar_lotes_completos` | `bit` | ✓ |  |
| 24 | `sistema` | `bit` | ✓ |  |
| 25 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 26 | `es_bodega_traslado` | `bit` | ✓ |  |
| 27 | `idubicacionvirtual` | `int` | ✓ |  |
| 28 | `referencia` | `nvarchar(25)` | ✓ |  |
| 29 | `IdBodega` | `int` | ✓ |  |
| 30 | `es_proveedor` | `bit` |  |  |

## Consume

- `cliente`
- `cliente_bodega`
- `cliente_tipo`
- `empresa`
- `propietarios`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Cliente]
AS
SELECT        e.nombre AS Empresa, p.nombre_comercial AS Propietario, ct.NombreTipoCliente AS [Tipo Cliente], dbo.cliente_bodega.activo AS activo_bodega, c.IdCliente, 
                         c.IdEmpresa, c.IdPropietario, c.IdTipoCliente, c.IdUbicacionManufactura, c.codigo, c.nombre_comercial, c.nombre_contacto, c.telefono, c.nit, c.direccion, 
                         c.correo_electronico, c.activo, c.realiza_manufactura, c.user_agr, c.fec_agr, c.user_mod, c.fec_mod, c.despachar_lotes_completos, c.sistema, 
                         c.es_bodega_recepcion, c.es_bodega_traslado, c.idubicacionvirtual, c.referencia, 
						 dbo.cliente_bodega.IdBodega, c.es_proveedor
FROM            dbo.cliente AS c LEFT OUTER JOIN
                         dbo.cliente_bodega ON c.IdCliente = dbo.cliente_bodega.IdCliente LEFT OUTER JOIN
                         dbo.empresa AS e ON c.IdEmpresa = e.IdEmpresa LEFT OUTER JOIN
                         dbo.propietarios AS p ON c.IdPropietario = p.IdPropietario LEFT OUTER JOIN
                         dbo.cliente_tipo AS ct ON c.IdTipoCliente = ct.IdTipoCliente
```
