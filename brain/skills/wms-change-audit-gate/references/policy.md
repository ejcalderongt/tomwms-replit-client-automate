---
tipo: other
autores: [erik]
---
# Change Audit Policy

`#EJC20260527` Decision policy:

- `FAIL`:
  Missing null guards in high-risk paths, invalid state gating, or unresolved DB object issues.
- `WARN`:
  Partial business-rule mapping, medium risks without hard breakage evidence.
- `PASS`:
  Risks low and evidence complete for null/state/business-rule/DB correlation.

DB correlation is required when:
- SQL files change, or
- code references `trans_`, `stock`, `sp_`, `IdUbicacionDestino`, or DAL/WS SQL execution paths.

