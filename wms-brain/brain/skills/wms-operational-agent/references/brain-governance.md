# WMS Brain Governance

## Source Handoff

This reference incorporates `HANDOFF-2026-05-27-TRANSICION-CODEX` from Mary Jane/Replit.

Codex local role:
- Curate `wms-brain/brain/` structure and schema.
- Keep the local WMS skill aligned with Brain rules.
- Process operational handoffs into durable indexes, patterns, traces, or proposals.
- Maintain direct local governance. Do not depend on bridge/OpenClaw as active infrastructure.

Mary Jane/Replit role:
- Historical/parallel source of operational material: production observations, fixes, DB snapshots, and client handoffs.
- Past bridge/OpenClaw work can be mined for parsers, digesters, data-shaping ideas, and diagnostics, but is not the current communication path.
- `wms-db-brain` is a valuable raw SQL snapshot branch. Mine it through the local `wms-db-brain` skill instead of merging the orphan branch directly.

EJC role:
- Technical owner and final approver.
- Only EJC authorizes commits/pushes unless explicitly delegating a specific action.

## Legacy Bridge And OpenClaw Status

Bridge/OpenClaw references are obsolete as an active workflow.

Historical value:
- Understand how prior tools parsed handoffs, traces, snapshots, and code signals.
- Reuse Python/JS routines that normalize raw data into summaries, maps, or diagnostic inputs.
- Extract tested heuristics for frontmatter, event envelopes, pattern detection, and indexing.

Policy:
- Do not send ACKs, directives, or updates through `_inbox` as an active channel.
- Do not treat `BRIDGE.md`, OpenClaw, or old bridge scripts as required runtime dependencies.
- Do not recreate a machine-to-Replit communication bridge unless EJC explicitly reopens that design.
- If old bridge/OpenClaw code is useful, port the small parsing/diagnostic idea into current local tools with clear `#EJCYYYYMMDD` documentation.
- `#EJC20260527` DB catalog access is now local-governed: use `origin/wms-db-brain` as the raw snapshot source and `wms-brain` as the interpretation/governance layer.

## Binding Rules To Keep Hot

- XML to JSON migration is opportunistic. Do not migrate stable legacy flows without a reason.
- New JSON endpoints use Forma A: `{data, error}` wrapper, `JavaScriptSerializer`, status 200/500.
- Preserve UTF-8 handling in HH; do not reintroduce string replacements that corrupt `n`/encoding.
- No automatic commit/push without explicit EJC permission.
- Do not mix HH Android and BOF/VB backend changes in one commit.
- `Cantidad` uses UMBAS in `stock`, `movimientos`, and `stock_res`; `trans_picking_ubic` uses presentation when present, otherwise UMBAS.
- Local agent files (`AGENTS.md`, `.codex/`, `.cursorrules`, etc.) belong in backup/recovery surfaces, not WMS production code commits, unless EJC explicitly chooses otherwise.
- WMSWebAPI layering is `DALCore` -> `EntityCore` -> `Services` -> `Controller` with Forma A.
- MI3 OC status resolves by `trans_oc_enc.Referencia`, not `IdOrdenCompraEnc`.

## Context Tiers

Tier 0, always active when present:
- `brain/replit.md`
- `brain/BRIDGE.md` only as historical context if it exists.
- Role ownership, team initials, commit and inline tag conventions.

Tier 1, first triage:
- `brain/_index/ATLAS.md`
- Active client and branch.
- Process trace index and current process trace.
- SP/table/method names touched by the ticket.

Tier 2, retrieval on demand:
- Client snapshots.
- 2023-vs-2028 diffs.
- `PATTERNS-*.md`.
- Historical handoffs, legacy bridge/OpenClaw scripts, and callsite flags.

Tier 3, only when EJC asks:
- Raw JSON snapshots.
- SQL dumps.
- Large logs and full Janeway traces.

## Intake Rules

- Prefer frontmatter search over reading full topic bodies.
- Topic files should include: `name`, `description`, `clientes`, `ramas`, and `tags`.
- If three or more handoffs repeat the same symptom, propose a consolidated `PATTERNS-*.md`.
- When Janeway/Brain index is stale after known commits, request or run reindex before semantic impact reasoning.

## Known Recent Handoff Facts

- Janeway runId 56 was stale against TOMWMS_BOF `a599caf` and TOMHH2025 `284c7da9`.
- Recent BOF commits noted by Mary Jane:
  - `aef063e`: MAMPA bulkcopy casing fix.
  - `895d843`: `dev_2028_merge` port of bulkcopy casing fix.
  - `7511135`: MAMPA inventory apply performance improvement.
- Reusable lesson: `SqlBulkCopy.ColumnMappings` destination validation can behave case-sensitive in the driver, independent of SQL Server collation.
- Reusable lesson: BOF apply routines often mix writes and cosmetic grid refreshes inside large SQL transactions; move noncritical refresh work outside the transaction when safe.
- Reusable lesson: branch drift requires adapting the idea of a fix, not literal cherry-pick.

## Direct Governance Stance

Current stance:
- Codex has direct local access and governance over the WMS agent/Brain maintenance flow.
- No active bridge send/receive loop is required.
- Old bridge/OpenClaw assets are reference material, not an operational contract.
- Codex remains curator of schema, indexes, patterns, traces, diagnostics, and local-agent skill behavior.
