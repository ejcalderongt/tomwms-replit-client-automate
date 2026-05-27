# Regression Guardian Playbook

`#EJC20260527` Minimal regression protocol after each operational fix.

1. Capture the fix footprint.
2. Scan sibling modules for same failure class.
3. Build process checklist for re-test.
4. Record residual risk as low/medium/high with evidence.

## Typical Failure Classes

- Null/empty/list handling drift.
- State gating drift (`Pendiente`, `Procesado`, `Verificado`, `Despachado`).
- Optional-flag drift (client/bodega/product flags).
- Destination assignment loss (`IdUbicacionDestino`/related IDs).

## Closure Criteria

- No high residual risk unresolved.
- Checklist completed or explicitly waived with reason.
- Trace/handoff updated with reusable rule.
