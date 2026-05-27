# WMS Operational Checklists

## Bugfix Checklist

- Check Tier 0 governance (`replit.md`; `BRIDGE.md` only as historical context if present) in wms-brain.
- Run `scripts/wms-preflight.ps1 -Process <process>` when starting operational analysis.
- Identify process: picking, recepcion, cambio ubicacion, existencias, packing, verificacion, reemplazo, inventario.
- Read the operational trace for that process.
- Map entry point, callback/event, WS method, DAL method, and DB write/read.
- Confirm if change is HH-only, BOF-only, SQL-only, or cross-layer.
- Keep each patch scoped to one layer when possible.
- Add `#EJCYYYYMMDD` only for durable operational rules.
- Build affected project.
- Update Brain trace with the learned rule.
- Run `scripts/wms-patch-check.ps1` before final response.

## HH Android Checklist

- Check scanner/Enter double-trigger.
- Check async callback state and in-flight flags.
- Preserve focus after callbacks.
- Validate object sent to WS has required identity fields:
  `IdPickingUbic`, `IdPickingEnc`, `IdPickingDet`, `IdStock`, `IdStockRes`, `IdUbicacion`, `IdBodega`.
- Compare against `dev_2023_estable` or `origin/dev_2026_mampa` when regression is suspected.
- Build with `gradlew.bat :app:compileDebugJavaWithJavac`.

## BOF/VB Checklist

- Do not touch `Reference.vb`.
- Confirm the form/file is compiled by the active `.vbproj`; several BOF forms have orphan duplicates.
- Prefer DAL causal fixes over UI masking.
- Validate movement, stock, reservation, and operational link/puente table consistency.
- For `trans_movimientos`, verify origin/destination, task type, quantity, stock identity, and related order/picking ids.
- Keep DB writes separate from cosmetic grid refreshes when safe, especially in "aplicar/finalizar" flows.
- Build with MSBuild on `WSHHRN/WSHHRN.vbproj`.

## SQL/Brain Checklist

- Read `references/brain-governance.md` for direct Brain curation and legacy handoff interpretation.
- Use Brain `/search` to find symbol.
- Use Brain `/impact` for blast radius.
- Use Brain `/writers` for table mutation contracts.
- For `SqlBulkCopy`, validate destination column casing against schema metadata; driver mapping may be case-sensitive.
- KILLIOS prod queries must be SELECT-only.
- Do not expose sensitive SQL `definition` content in user-facing output.
