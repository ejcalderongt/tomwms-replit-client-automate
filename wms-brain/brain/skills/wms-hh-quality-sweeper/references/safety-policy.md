# HH Sweeper Safety Policy

`#EJC20260527`

Default operation is `report-only`.

Allowed automatic fixes in `safe-fix`:
- Replace null-check anti-patterns with `TextUtils.isEmpty(...)`.
- Remove `final` constants/fields provably unused in the same class.
- Convert private field to local variable only when usage is inside one method and easy to verify.

Not allowed automatically:
- Behavioral logic changes in movement/picking/recepcion/verificacion flows.
- Refactors spanning multiple classes without manual review.
- Dependency version bumps without compatibility report.

Current prioritized IDE rule set:
- Redundant boxing, `Integer.parseInt()` call can be used instead.
- Redundant boxing, `Double.parseDouble()` call can be used instead.
- Method invocation `getName` may produce `NullPointerException`.
- `switch` statement has too few case labels (1), should be replaced with `if`.
- Anonymous `new View.OnKeyListener()` can be replaced with lambda.
- Anonymous `new View.OnFocusChangeListener()` can be replaced with lambda.
- Field can be converted to a local variable.
- `X is assigned but never accessed`.
- Variable initializer is redundant.
- `equals("")` can be replaced with `isEmpty()`.
- `indexOf(...) == -1` can be replaced with `!contains(...)` when semantic equivalent.
- `indexOf(...) >= 0` / `indexOf(...) != -1` / `indexOf(...) > -1` can be replaced with `contains(...)` when semantic equivalent.
- `size() > 0` / `size() == 0` / `size() != 0` can be replaced with `!isEmpty()` / `isEmpty()` when semantic equivalent.
