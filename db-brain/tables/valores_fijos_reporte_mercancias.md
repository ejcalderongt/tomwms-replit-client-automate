---
id: db-brain-table-valores-fijos-reporte-mercancias
type: db-table
title: dbo.valores_fijos_reporte_mercancias
schema: dbo
name: valores_fijos_reporte_mercancias
kind: table
rows: 3
modify_date: 2022-10-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.valores_fijos_reporte_mercancias`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 3 |
| Schema modify_date | 2022-10-25 |
| Columnas | 30 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `regimen` | `nvarchar(50)` | ✓ |  |
| 2 | `codigobodega` | `nvarchar(150)` | ✓ |  |
| 3 | `fecha` | `date` | ✓ |  |
| 4 | `codigomercaderia` | `nvarchar(50)` | ✓ |  |
| 5 | `certificadodeposito` | `nvarchar(50)` | ✓ |  |
| 6 | `bonoprenda` | `nvarchar(50)` | ✓ |  |
| 7 | `saldosincertificado` | `numeric` | ✓ |  |
| 8 | `saldocertificado` | `numeric` | ✓ |  |
| 9 | `saldobono` | `nvarchar(50)` | ✓ |  |
| 10 | `nombreacreedor` | `nvarchar(50)` | ✓ |  |
| 11 | `descripcionmercaderia` | `nvarchar(100)` | ✓ |  |
| 12 | `fechaemisioncertificado` | `nvarchar(50)` | ✓ |  |
| 13 | `fechavencimientocertificado` | `nvarchar(50)` | ✓ |  |
| 14 | `fechaemisionbono` | `nvarchar(50)` | ✓ |  |
| 15 | `fechavencimientobono` | `nvarchar(50)` | ✓ |  |
| 16 | `fechaemisionds` | `nvarchar(4000)` | ✓ |  |
| 17 | `fechavencimientods` | `nvarchar(4000)` | ✓ |  |
| 18 | `cif` | `numeric` | ✓ |  |
| 19 | `impuestos` | `numeric` | ✓ |  |
| 20 | `seguros` | `nvarchar(50)` | ✓ |  |
| 21 | `seguros2` | `nvarchar(50)` | ✓ |  |
| 22 | `primerapellido` | `nvarchar(50)` | ✓ |  |
| 23 | `segundoapellido` | `nvarchar(50)` | ✓ |  |
| 24 | `apellidocasada` | `nvarchar(50)` | ✓ |  |
| 25 | `primernombre` | `nvarchar(50)` | ✓ |  |
| 26 | `segundonombre` | `nvarchar(50)` | ✓ |  |
| 27 | `tercernombre` | `nvarchar(50)` | ✓ |  |
| 28 | `razonsocial` | `nvarchar(150)` | ✓ |  |
| 29 | `nombrecomercial` | `nvarchar(150)` | ✓ |  |
| 30 | `terminacion` | `nvarchar(1)` | ✓ |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Stock_res_jornada_merca` (view)

