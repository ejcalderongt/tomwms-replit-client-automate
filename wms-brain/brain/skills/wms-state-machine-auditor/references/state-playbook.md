# State Playbook

`#EJC20260527` Canonical transitions should be explicit per process.

Example baseline:
- `Nuevo -> Pendiente -> Procesado -> Verificado -> Despachado`
- `Anulado` is terminal from non-despachado states.

Validation rules:
- No action-only gates without state reason.
- No state transition hidden in UI-only path.
- DB and UI observed states must reconcile.

