---
id: db-brain-view-vw-clientes-tiempos
type: db-view
title: dbo.VW_Clientes_Tiempos
schema: dbo
name: VW_Clientes_Tiempos
kind: view
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Clientes_Tiempos`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-07-02 |
| Columnas | 4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Codigo` | `nvarchar(150)` | ✓ |  |
| 2 | `Nombre_Comercial` | `nvarchar(150)` | ✓ |  |
| 3 | `Cant_Familias` | `int` | ✓ |  |
| 4 | `Cant_Clasificacion` | `int` | ✓ |  |

## Consume

- `cliente`
- `cliente_tiempos`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Clientes_Tiempos]
AS
SELECT  cliente.Codigo, cliente.Nombre_Comercial, 
        COUNT(DISTINCT IdFamilia) Cant_Familias, 
		COUNT(DISTINCT IdClasificacion) Cant_Clasificacion
FROM cliente_tiempos INNER JOIN cliente ON cliente.IdCliente = cliente_tiempos.IdCliente
WHERE cliente.activo = 1
GROUP BY cliente.codigo, cliente.nombre_comercial
UNION
SELECT cliente.Codigo, cliente.Nombre_Comercial, 
       0 Cant_Familias, 
	   0 Cant_Clasificacion
FROM cliente
WHERE cliente.activo = 1 AND
 NOT EXISTS (SELECT DISTINCT cliente_tiempos.idcliente 
                  FROM cliente_tiempos 
				  WHERE cliente_tiempos.IdCliente = cliente.IdCliente)
```
