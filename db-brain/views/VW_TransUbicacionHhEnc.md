---
id: db-brain-view-vw-transubicacionhhenc
type: db-view
title: dbo.VW_TransUbicacionHhEnc
schema: dbo
name: VW_TransUbicacionHhEnc
kind: view
modify_date: 2026-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_TransUbicacionHhEnc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2026-02-11 |
| Columnas | 26 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareaUbicacionEnc` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` |  |  |
| 3 | `IdMotivoUbicacion` | `int` | ✓ |  |
| 4 | `DescripcionMotivo` | `nvarchar(50)` | ✓ |  |
| 5 | `FechaInicio` | `date` | ✓ |  |
| 6 | `HoraInicio` | `datetime` | ✓ |  |
| 7 | `FechaFin` | `date` | ✓ |  |
| 8 | `HoraFin` | `datetime` | ✓ |  |
| 9 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |
| 13 | `Observacion` | `nvarchar(150)` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `operador_por_linea` | `bit` | ✓ |  |
| 16 | `ubicacion_con_hh` | `bit` | ✓ |  |
| 17 | `estado` | `nvarchar(50)` | ✓ |  |
| 18 | `cambio_estado` | `bit` | ✓ |  |
| 19 | `IdReabastecimientoLog` | `int` | ✓ |  |
| 20 | `es_traslado_sap` | `bit` |  |  |
| 21 | `no_documento` | `nvarchar(50)` | ✓ |  |
| 22 | `Usuario` | `nvarchar(100)` | ✓ |  |
| 23 | `Rol` | `nvarchar(50)` | ✓ |  |
| 24 | `sync_mi3` | `bit` |  |  |
| 25 | `IdBodega` | `int` | ✓ |  |
| 26 | `CodigoBodega` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `motivo_ubicacion`
- `propietario_bodega`
- `rol`
- `trans_ubic_hh_enc`
- `usuario`
- `usuario_bodega`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_TransUbicacionHhEnc]
AS
SELECT
    e.IdTareaUbicacionEnc,
    e.IdPropietarioBodega,
    e.IdMotivoUbicacion,
    mu.Nombre AS DescripcionMotivo,
    e.FechaInicio,
    e.HoraInicio,
    e.FechaFin,
    e.HoraFin,
    e.user_agr,
    e.fec_agr,
    e.user_mod,
    e.fec_mod,
    e.Observacion,
    e.activo,
    e.operador_por_linea,
    e.ubicacion_con_hh,
    e.estado,
    e.cambio_estado,
    e.IdReabastecimientoLog,
    e.es_traslado_sap,
    e.no_documento,
    u.nombres AS Usuario,
    r.nombre AS Rol,
    e.sync_mi3,
    pb.IdBodega,
    b.Codigo AS CodigoBodega
FROM dbo.trans_ubic_hh_enc e
INNER JOIN dbo.motivo_ubicacion mu
    ON e.IdMotivoUbicacion = mu.IdMotivoUbicacion
INNER JOIN dbo.usuario u
    ON e.user_agr = u.IdUsuario
INNER JOIN dbo.usuario_bodega ub
    ON u.IdUsuario = ub.IdUsuario
INNER JOIN dbo.rol r
    ON ub.IdRol = r.IdRol
INNER JOIN dbo.propietario_bodega pb
    ON e.IdPropietarioBodega = pb.IdPropietarioBodega
   AND ub.IdBodega = pb.IdBodega
INNER JOIN dbo.bodega b
    ON b.IdBodega = pb.IdBodega;
```
