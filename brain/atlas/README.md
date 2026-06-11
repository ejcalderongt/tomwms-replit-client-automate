# Atlas Operativo Brain

## Entradas principales
- `brain/atlas/index.yml`: mapa maestro de paquetes y politicas.
- `brain/handoffs/_index.yml`: inventario de handoffs con sintomas y vigencia.
- `brain/tools/lint_brain_indexes.ps1`: validacion local de rutas/metadata.

## Flujo recomendado
1. Actualizar handoff o domain package.
2. Registrar/ajustar metadatos en los indices.
3. Ejecutar lint:

```powershell
powershell -ExecutionPolicy Bypass -File brain/tools/lint_brain_indexes.ps1 -RepoRoot .
```

4. Corregir errores y warnings stale relevantes.

## Regla de oro
No cargar handoffs por defecto. Cargar solo por match de sintomas/tags/domains.

## Actualizacion 2026-06-11
- Nuevo dominio: `brain/agents/domain-hh-android.yml`.
- Nuevo handoff operativo reutilizable:
  `brain/handoffs/2026-06-11-operativo-hh-bof-etiquetas-reemplazo-tc/README.md`.
- Router actualizado para triggers HH (`etiqueta`, `licencia`, `fardo`, `reemplazo`, `talla/color`).
