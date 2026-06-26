# Long-Term Memory

- In `mposbi`, import flows should prefer local idempotent auto-resolution of catalog dependencies over hard failures when it does not touch shared contracts.
- The safe rule for inventory/Firebase work is: do not modify `P_stock_model::set_producto()` in place for batching or import changes.
- For product imports, the useful pattern is resolve parent dependency first, then child dependency, and finish with a friendly summary of any auxiliary records created.
- Inventory bulk loads should keep kardex traceability: create the movement header/detail alongside the inventory load when the flow is meant to preserve original movement history.
- The inventory "init" flow (`Inventario_model::reiniciar()`) currently deletes stock and movement rows, not sales; sales annulment is a separate path in `Venta_model` and POS flows.
- Future sales purge work should delete child rows first, keep audit/history by default, and treat cotizaciones, ventas, CxC, notes of envio, commissions and bitacoras as separate dependency groups.
- The initialization UI was structured around a dedicated `purge_scope.php` config and `Purge_scope_service`, with mandatory captcha plus inventory password before execution.
- The smoke taught that a valid session must include `usuario.id`; otherwise bitacora insert can fail even after the purge already ran.
- The contingency backup for initialization is file-based and should live under `application/backups/purge/empresa_{empresa}/sucursal_{sucursal}/empresa_{empresa}_sucursal_{sucursal}_{timestamp}/`.
- Active WMS brain is `wms-brain`; the main repo for operational work is `C:\Users\yejc2\source\repos\TOMWMS`.
- For WMS operational changes, follow the WMS brain first; use the DB brain when the task needs SQL, query plans, or index tuning.
- Keep the durable workspace anchor in `wms-brain/README.md` so the setup can be resumed without reloading context manually.
- The DBA repo is `C:\Users\yejc2\source\repos\DBA`; it now carries the shared KPI index script and the WMS KPI tuning handoff note.
- For MCP work, the durable brain/source of truth is `mcp-brain` on GitHub, not the local WMS brain; do not mix MCP context with WMS/QAS when validating `mposbi` inventory reports.
- Federated startup now has a single manifest at `brain/federated-index.yml` and an executable bootstrap script at `scripts/bootstrap-session.ps1`; routing should extend there first when adding persistent brains.
- `wms-brain` now owns a visible WMS bootstrap entry point at `wms-brain/bootstrap-session.ps1`; use that anchor for WMS sessions and machine/environment restores.
- MCP cost structure is hybrid: `T_costo`/`D_costo` keep the event trail, `p_producto.costo` stays the operational current cost, and sales reports currently read the master cost at query time unless a policy toggle is introduced.
- In `mPos2026` (`dev495b`), `P_PRODUCTO.METODO_COSTO` was added to the local model and `GetP_PRODUCTO` sync; `InvRecep`, `InvTrans`, and `WSEnv` now use it to decide whether to overwrite `P_PRODUCTO.COSTO`, with `P_PARAMEXT.ID=197` left only as legacy fallback when the field is empty.
- In `SapDescuentoController`, `SaveChangesAsync()` persists everything tracked in the `DbContext`; for escala batches, validate composite-key duplicates in memory and against DB before save, and translate SQL 2601/2627 into a friendly 409.
- Combo SAP on `SapDescuentoController` can fail because stale rows remain in `P_DESCUENTO_COMBO_DET` for the same `CODDESC`; `reemplazarExistente=true` must clear detail by `CODDESC` even if the header is missing.
- In `SapDescuentoController`, `reemplazarExistente=true` now applies to simple, escalas, and combo flows; when replacing, clear by `CODDESC` even if the commercial key changed, and return explicit 409s when an existing `CODDESC` would otherwise be silently upserted.
- Kelvyn rule: keep brain folders for context, documentation, learning, and reusable instructions only; do not mix application code into `brain/`, `docs/`, `learning/`, or `agents/`.

## Preferences

- Erik prefiere que aplique fixes automáticamente y sin mostrar los detalles por defecto; pediré confirmación solo para acciones destructivas o externas.
- Aumentar proactividad: chequeos ligeros y pequeñas correcciones en background con comunicación mínima.
