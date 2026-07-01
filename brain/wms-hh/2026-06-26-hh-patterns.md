# HH Android WMS — Patterns, Rules, and Fix Log (2026-06-26)

Scope: TOMHH2025 HH Android. Durable notes for future work.

## Inventory Cíclico (Detalle Góndola)
- Key filter: Always restrict the detail list to the exact (Ubicación + Góndola) key. Never infer by “góndola vacía”.
- Location name: Resolve via WS `Get_Ubicacion_By_Codigo_Barra_And_IdBodega_JSON`; show exactly `Descripcion` or `NombreCompleto` (no local composition). Add a small in-flight/cache guard to avoid duplicate WS calls.
- Onboarding for missing góndola: Show a full-screen overlay to assign góndola. Block Load() and ProgressDialog while overlay is visible to avoid visual overlap.
- Safe user fields: If a field must not be typed/scanned (e.g., Ubicación), disable it completely (enabled=false, focusable=false, cursor false, TYPE_NULL) and route change through an explicit button + captcha.
- Change Ubicación with captcha: 4-digit code + server-side validation of the new Ubicación before applying. Rule: one góndola belongs to one Ubicación; do not allow moving the same góndola to a different Ubicación if it already exists.
- UI feedback: Prefer an inline loader row (spinner + bold text) for quick steps, plus a non-deprecated global ProgressDialog for longer calls. Avoid deprecated android.app.ProgressDialog; use a wrapper dialog.
- Antipise/debounce: Add fine-grained locks on Enter/clicks to avoid concurrent WS calls.
- Column mapping: Parse conteo/stock columns dynamically; when conteo is missing/zero, seed from the in-memory `gl.reconteo_list` for (Ubicación,Góndola,SKU) to keep totals correct.
- Banner clarity: Show a small banner with the active filter ("Ubicación X • Góndola Y"), and mark “(redireccionada)” when the UI corrected the Ubicación to the one actually owning the góndola.
- Layout anchors: When stacking banners/rows, anchor siblings (e.g., rowProcesando → pnlResumenConteo; lyFiltroClave above the recycler) to avoid overlay glitches.
- Errors/dialogs: Use ExDialog for functional messages (large readable typography) instead of `msgbox` where it makes sense (security confirmations, validation errors).

## MAMPA Implosión (Packing)
- Movement + payload must propagate variant identity:
  - Set `Movimiento.IdProductoTallaColor` from product/stock; include `Talla`/`Color` for traceability.
  - Mirror `IdProductoTallaColor` in `Stock_res` payload.
- If DB still gets 0 for `IdProductoTallaColor` after HH fix, audit the WS handlers (`Set_LP_Stock`, `Set_LP_Stock_Mixto`) to ensure the insert maps the field to `trans_movimientos`.

## Picking — UMBas rule
- When a product has no `Presentación` (IdPresentacion == 0):
  - Hide the UMBas row/field and the estiba block.
  - Show the UMBas name in the parentheses of the “Cantidad (...)” label. Use fallback "UNI" when empty.

## Verificación — UMBas rule
- Same policy as Picking: hide UMBas block when there is no `Presentación`, and show the UMBas name in the “Cantidad (...)” label (fallback "UNI").

## Non-deprecated Progress Dialog Pattern
- Use `com.dts.base.ProgressDialog` wrapper and inline loader row (spinner + text) instead of `android.app.ProgressDialog`.
- Provide a small helper to dismiss safely and call it on success/error/onDestroy to prevent leaks.

## One Góndola ↔ One Ubicación
- Enforce uniqueness at UI routing time: if user enters Ubicación A and Góndola G that actually belongs to Ubicación B, offer to navigate to B; do not allow creation of G under A when G exists elsewhere.

## Logging
- Keep consistent tags:
  - `WMS.Nav` (navigation extras, redirects)
  - `WMS.Onboarding` (overlay state)
  - `WMS.Ubic` (Ubicación resolution, cache/in-flight)
  - `WMS.Load` (Load gating & exec)
  - `WMS.Gondola` (cols/sample/totales/scan/filtro)
  - Prefer ExDialog for user-facing functional errors.

## Why these patterns
- Avoid double-calls, mixed states, and fragile UI composition.
- Maintain clear operator intent: explicit flow for góndola assignment and Ubicación change.
- Increase legibility and feedback under HH conditions (small screen, harsh lighting, gloves).
- Preserve variant identity (PTC) end-to-end in implosión.
