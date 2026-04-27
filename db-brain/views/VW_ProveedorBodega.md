---
id: db-brain-view-vw-proveedorbodega
type: db-view
title: dbo.VW_ProveedorBodega
schema: dbo
name: VW_ProveedorBodega
kind: view
modify_date: 2025-06-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ProveedorBodega`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-11 |
| Columnas | 27 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdAsignacion` | `int` | ✓ |  |
| 3 | `Empresa` | `nvarchar(50)` | ✓ |  |
| 4 | `Propietario` | `nvarchar(100)` |  |  |
| 5 | `IdEmpresa` | `int` |  |  |
| 6 | `IdPropietario` | `int` |  |  |
| 7 | `IdProveedor` | `int` |  |  |
| 8 | `nombre` | `nvarchar(100)` | ✓ |  |
| 9 | `telefono` | `nvarchar(50)` | ✓ |  |
| 10 | `nit` | `nvarchar(25)` | ✓ |  |
| 11 | `direccion` | `nvarchar(250)` | ✓ |  |
| 12 | `email` | `nvarchar(50)` | ✓ |  |
| 13 | `contacto` | `nvarchar(100)` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `muestra_precio` | `bit` | ✓ |  |
| 16 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 17 | `fec_agr` | `datetime` | ✓ |  |
| 18 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 19 | `fec_mod` | `datetime` | ✓ |  |
| 20 | `actualiza_costo_oc` | `bit` | ✓ |  |
| 21 | `activo_proveedor_bodega` | `bit` | ✓ |  |
| 22 | `Código` | `nvarchar(50)` | ✓ |  |
| 23 | `idubicacionvirtual` | `int` |  |  |
| 24 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 25 | `es_bodega_traslado` | `bit` | ✓ |  |
| 26 | `IdAreaOrigen` | `int` | ✓ |  |
| 27 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |

## Consume

- `empresa`
- `propietarios`
- `proveedor`
- `proveedor_bodega`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view VW_ProveedorBodega as
SELECT dbo.proveedor_bodega.IdBodega, dbo.proveedor_bodega.IdAsignacion, e.nombre AS Empresa, pr.nombre_comercial AS Propietario, p.IdEmpresa, p.IdPropietario, p.IdProveedor, p.nombre, p.telefono, p.nit, p.direccion, p.email, p.contacto, 
                  p.activo, p.muestra_precio, p.user_agr, p.fec_agr, p.user_mod, p.fec_mod, p.actualiza_costo_oc, dbo.proveedor_bodega.activo AS activo_proveedor_bodega, p.codigo AS Código, 
				  isnull(p.idubicacionvirtual,0) As idubicacionvirtual, p.es_bodega_recepcion, 
                  p.es_bodega_traslado, dbo.proveedor_bodega.IdAreaOrigen, p.Codigo_Empresa_ERP
FROM     dbo.proveedor AS p INNER JOIN
                  dbo.empresa AS e ON p.IdEmpresa = e.IdEmpresa INNER JOIN
                  dbo.propietarios AS pr ON p.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.proveedor_bodega ON p.IdProveedor = dbo.proveedor_bodega.IdProveedor
```
