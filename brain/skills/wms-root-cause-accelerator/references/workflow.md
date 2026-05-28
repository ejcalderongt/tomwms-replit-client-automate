---
tipo: other
clientes: [mampa]
ramas_afectadas: [dev_2023_estable]
autores: [erik]
---
# Root Cause Workflow

`#EJC20260527` Standard flow for high-speed diagnosis and safer fixes.

1. Intake symptom and process.
2. Build candidate technical path from trace/index and code hits.
3. Detect branch drift against baseline (`dev_2026_mampa`, `dev_2023_estable`).
4. Scan for guard failures (`null`, `empty`, `list`, cast).
5. Patch causal point.
6. Execute post-fix checklist and capture evidence.
7. Update process trace/handoff with learned rule.

## Process Profiles

- `picking`: focus `trans_picking_ubic`, `trans_picking_ubic_stock`, `trans_movimientos`.
- `cambio_ubicacion`: focus `trans_movimientos`, destination location assignment, movement validation.
