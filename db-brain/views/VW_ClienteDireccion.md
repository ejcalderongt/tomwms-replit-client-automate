---
id: db-brain-view-vw-clientedireccion
type: db-view
title: dbo.VW_ClienteDireccion
schema: dbo
name: VW_ClienteDireccion
kind: view
modify_date: 2016-05-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ClienteDireccion`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-05-27 |
| Columnas | 20 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdCliente` | `int` |  |  |
| 2 | `IdDireccion` | `int` |  |  |
| 3 | `IdRegion` | `int` | ✓ |  |
| 4 | `NombreRegion` | `varchar(25)` | ✓ |  |
| 5 | `IdMunicipio` | `int` | ✓ |  |
| 6 | `NombreMunicipio` | `varchar(255)` | ✓ |  |
| 7 | `Avenida` | `nvarchar(50)` | ✓ |  |
| 8 | `Calle` | `nvarchar(50)` | ✓ |  |
| 9 | `No_Casa` | `nvarchar(50)` | ✓ |  |
| 10 | `Zona` | `nvarchar(50)` | ✓ |  |
| 11 | `Direccion` | `nvarchar(50)` | ✓ |  |
| 12 | `Referencia` | `nvarchar(50)` | ✓ |  |
| 13 | `coordenada_y` | `nvarchar(50)` | ✓ |  |
| 14 | `coordenada_x` | `nvarchar(50)` | ✓ |  |
| 15 | `Local` | `bit` | ✓ |  |
| 16 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 17 | `fec_agr` | `datetime` | ✓ |  |
| 18 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 19 | `fec_mod` | `datetime` | ✓ |  |
| 20 | `activo` | `bit` |  |  |

## Consume

- `cliente_direccion`
- `pais_municipio`
- `pais_region`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_ClienteDireccion
AS
SELECT        dbo.cliente_direccion.IdCliente, dbo.cliente_direccion.IdDireccion, dbo.cliente_direccion.IdRegion, dbo.pais_region.Nombre AS NombreRegion, 
                         dbo.cliente_direccion.IdMunicipio, dbo.pais_municipio.Nombre AS NombreMunicipio, dbo.cliente_direccion.Avenida, dbo.cliente_direccion.Calle, 
                         dbo.cliente_direccion.No_Casa, dbo.cliente_direccion.Zona, dbo.cliente_direccion.Direccion, dbo.cliente_direccion.Referencia, dbo.cliente_direccion.coordenada_y, 
                         dbo.cliente_direccion.coordenada_x, dbo.cliente_direccion.Local, dbo.cliente_direccion.user_agr, dbo.cliente_direccion.fec_agr, dbo.cliente_direccion.user_mod, 
                         dbo.cliente_direccion.fec_mod, dbo.cliente_direccion.activo
FROM            dbo.cliente_direccion INNER JOIN
                         dbo.pais_region ON dbo.cliente_direccion.IdRegion = dbo.pais_region.IdRegion INNER JOIN
                         dbo.pais_municipio ON dbo.cliente_direccion.IdMunicipio = dbo.pais_municipio.IdMunicipio
```
