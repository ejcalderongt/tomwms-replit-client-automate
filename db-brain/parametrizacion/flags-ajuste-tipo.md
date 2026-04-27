# Flags del maestro `ajuste_tipo`

> 5 columnas bit en `dbo.ajuste_tipo` (validado 2026-04-27).

## Listado completo

| Flag | Descripción preliminar |
|---|---|
| `activo` | Tipo de ajuste habilitado para uso |
| `modifica_lote` | Este tipo de ajuste afecta el lote del stock |
| `momdifica_vencimiento` | **TYPO en producción** — debería ser `modifica_vencimiento`. Este tipo de ajuste afecta la fecha de vencimiento. |
| `modifica_cantidad` | Este tipo de ajuste afecta la cantidad del stock |
| `modifica_peso` | Este tipo de ajuste afecta el peso del stock |

## Hallazgo crítico — TYPO

`momdifica_vencimiento` (con "m" extra) es el nombre real en producción. Cualquier código VB que lea esta columna lo hace por nombre exacto. **No tocar sin coordinar con EJC** — corregir el typo requiere:
1. Migration SQL para renombrar la columna preservando datos.
2. Buscar todas las referencias en TOMWMS_BOF y reemplazar.
3. Posible re-deploy del WMS para todos los clientes.

Trade-off: vivir con el typo (zero risk) vs corregir (requiere ventana coordinada).

## Cross-refs
- `db-brain://tables/ajuste_tipo` (extracción posterior)
- `parametrizacion/matriz-killios#ajuste_tipo`
- `wms-brain://entities/decisions/dec-typo-momdifica-vencimiento` (TBD si se decide corregir)
