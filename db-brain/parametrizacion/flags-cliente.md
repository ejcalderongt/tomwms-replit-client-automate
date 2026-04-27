# Flags del maestro `cliente`

> 9 columnas bit en `dbo.cliente` (validado 2026-04-27). Killios tiene 1.083 clientes registrados.

## Listado completo

| Flag | Default | Descripción funcional | Impacto cuando =1 |
|---|---|---|---|
| `activo` | (TBD) | (TBD) | (TBD) |
| `realiza_manufactura` | (TBD) | (TBD) | (TBD) |
| `despachar_lotes_completos` | (TBD) | (TBD) | (TBD) |
| `sistema` | (TBD) | (TBD) | (TBD) |
| `es_bodega_recepcion` | (TBD) | (TBD) | (TBD) |
| `es_bodega_traslado` | (TBD) | (TBD) | (TBD) |
| `control_ultimo_lote` | (TBD) | (TBD) | (TBD) |
| `control_calidad` | (TBD) | (TBD) | (TBD) |
| `es_proveedor` | (TBD) | (TBD) | (TBD) |

## Flags vinculados a lotes

| Flag | Killios actual | Significado |
|---|---|---|
| `despachar_lotes_completos` | 0/1083 | Si =1: no permite despacho parcial de un lote (todo o nada por lote en cada despacho). |
| `control_ultimo_lote` | 0/1083 | Si =1: valida que el lote despachado coincida con el último lote registrado para el producto. |

## Pendiente de documentar

Cada flag necesita:
1. Descripción funcional (qué cambia el comportamiento del WMS).
2. Default histórico.
3. Código que lo lee (VB DAL, SPs, vistas).
4. Casos de uso de Jira/release notes que lo introdujeron.

Trabajo curated por humano + agente. NO se auto-genera del extractor.

## Cross-refs
- `db-brain://tables/cliente`
- `parametrizacion/matriz-killios#cliente`
