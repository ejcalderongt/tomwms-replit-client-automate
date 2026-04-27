---
id: db-brain-view-vw-proveedor
type: db-view
title: dbo.VW_Proveedor
schema: dbo
name: VW_Proveedor
kind: view
modify_date: 2025-06-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Proveedor`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-11 |
| Columnas | 30 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 2 | `Propietario` | `nvarchar(100)` |  |  |
| 3 | `IdEmpresa` | `int` |  |  |
| 4 | `IdPropietario` | `int` |  |  |
| 5 | `IdProveedor` | `int` |  |  |
| 6 | `codigo` | `nvarchar(50)` | ✓ |  |
| 7 | `nombre` | `nvarchar(100)` | ✓ |  |
| 8 | `telefono` | `nvarchar(50)` | ✓ |  |
| 9 | `nit` | `nvarchar(25)` | ✓ |  |
| 10 | `direccion` | `nvarchar(250)` | ✓ |  |
| 11 | `email` | `nvarchar(50)` | ✓ |  |
| 12 | `contacto` | `nvarchar(100)` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `muestra_precio` | `bit` | ✓ |  |
| 15 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |
| 19 | `actualiza_costo_oc` | `bit` | ✓ |  |
| 20 | `IdBodega` | `int` | ✓ |  |
| 21 | `activo_proveedor_bodega` | `bit` | ✓ |  |
| 22 | `idubicacionvirtual` | `int` | ✓ |  |
| 23 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 24 | `es_bodega_traslado` | `bit` | ✓ |  |
| 25 | `referencia` | `nvarchar(25)` | ✓ |  |
| 26 | `sistema` | `bit` | ✓ |  |
| 27 | `IdConfiguracionBarraPallet` | `int` | ✓ |  |
| 28 | `es_proveedor_servicio` | `bit` | ✓ |  |
| 29 | `IdBodegaAreaSAP` | `int` | ✓ |  |
| 30 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |

## Consume

- `empresa`
- `propietarios`
- `proveedor`
- `proveedor_bodega`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Proveedor]
AS
SELECT        e.nombre AS Empresa, pr.nombre_comercial AS Propietario, p.IdEmpresa, p.IdPropietario, p.IdProveedor, p.codigo, p.nombre, p.telefono, p.nit, p.direccion, p.email, 
                         p.contacto, p.activo, p.muestra_precio, p.user_agr, p.fec_agr, p.user_mod, p.fec_mod, p.actualiza_costo_oc, dbo.proveedor_bodega.IdBodega, 
                         dbo.proveedor_bodega.activo AS activo_proveedor_bodega, p.idubicacionvirtual, p.es_bodega_recepcion, p.es_bodega_traslado, p.referencia, p.sistema, 
                         p.IdConfiguracionBarraPallet, es_proveedor_servicio, p.IdBodegaAreaSAP, p.Codigo_Empresa_ERP
FROM            dbo.proveedor AS p INNER JOIN
                         dbo.empresa AS e ON p.IdEmpresa = e.IdEmpresa INNER JOIN
                         dbo.propietarios AS pr ON p.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.proveedor_bodega ON p.IdProveedor = dbo.proveedor_bodega.IdProveedor
```
