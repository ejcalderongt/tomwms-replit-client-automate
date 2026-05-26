---
name: wms-reserva-diagnostics
description: Use for TOMWMS reservation diagnostics and fixes: MI3 reserva/no-reserva, stock_res, trans_pe_det, i_nav_ped_traslado, FEFO, picking vs almacenaje, presentation CJ/UM base, BYB EA-153304/EA-153305 parity, WebAPI/Core vs legacy comparison, reservation performance traces, and safe cleanup of synthetic reservation documents.
---

# WMS Reserva Diagnostics

## Load First

When this skill triggers inside `C:\Users\yejc2\source\repos\TOMWMS`, read only the matching context:

1. `AGENTS.md`
2. `codex-context-mi3-di-estatus.yml`
3. `brain/agents/_index.yml`
4. `brain/agents/domain-reserva.yml`
5. `brain/agents/domain-database.yml`
6. If WebAPI/API/performance is involved: `domain-webapi.yml` and `domain-performance.yml`
7. If BYB, `EA-153304`, `EA-153305`, `BA0002` or `CJ`: `brain/agents/client-byb.yml`

Do not load all brain files by default.

## Workflow

1. Identify the document, client DB, payload, and accepted baseline.
2. Stop any local WebAPI process before builds/runs.
3. Clean only synthetic test documents, for example `EA-153304WEBAPI`.
4. Build before executing API/DB validation.
5. Run WebAPI with a hard timeout, usually 120 seconds.
6. Validate API response and DB state.
7. For parity, compare per-line `stock_res`, not only global totals.
8. Clean the test document after the cycle.
9. Commit only stable phases when Erik asks for phase commits.

## Acceptance Gate

Accept a reservation change only if:

- Build passes.
- API response is successful when API is part of the test.
- `trans_pe_det` count and `SUM(Cantidad)` match expected values.
- `stock_res` count and `SUM(Cantidad)` match expected values.
- `i_nav_ped_traslado_det` count and `SUM(Quantity_Reserved_WMS)` match expected values.
- Per-line `stock_res` partition matches the accepted baseline when parity is required.
- No synthetic test document remains in DB.

Reject or revert if a change modifies `IdStock`, lote, presentation, UM, or `stock_res` partition without explicit acceptance, even if global totals match.

## Useful Tools

- `tools/webapi-mi3-reserva/compare-mi3-reserva-baseline.ps1`
- `tools/webapi-mi3-reserva/diagnosticar-mi3-reserva-documento.sql`
- `tools/webapi-mi3-reserva/comparar-mi3-reserva-documentos.sql`
- `tools/webapi-mi3-reserva/README.md`

## Known BYB Baseline

For `EA-153304` accepted legacy baseline:

- 29 lines
- total requested/reserved: 1370/1370
- `stock_res` rows: 48
- `stock_res` sum: 12703

Use `EA-153304WEBAPI` or another synthetic suffix for WebAPI tests.

## Guardrails

- Never print DB passwords or full connection strings.
- KILLIOS production is SELECT-only.
- Do not optimize by weakening FEFO, picking, state, location, presentation or UM filters.
- Do not touch HH and VB/Core in the same change.
