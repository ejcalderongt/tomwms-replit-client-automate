---
name: wms-operational-agent
description: Specialized WMS workflow for Erik/PrograX24 TOMWMS tasks. Use when diagnosing, fixing, reviewing, or documenting operational processes across TOMHH2025 Android HH, TOMWMS_BOF/VB/WSHHRN, TOMWMS_DBA SQL, wms-brain, picking, recepcion, packing, verificacion, reemplazo, existencias, inventario, movimientos, stock, ubicaciones, SOAP WebMethods, DAL, SQL impact, or when the user asks to blindar, mapear flujo, comparar ramas, tagear EJC, update Brain, build, patch, or prepare a reusable WMS agent workflow.
---

# WMS Operational Agent

## Core Rule

Act as a WMS coordinator with narrow context loading. Do not start by reading everything. Load the repo rules, then the process trace and only the domain packages needed for the task.

Always preserve Erik's workflow:
- No automatic commits unless explicitly requested.
- Do not mix HH Java and BOF/VB/SQL changes in one change set.
- Never touch `Reference.vb`.
- Tag durable code comments and Brain notes with `#EJCYYYYMMDD`.
- For code changes, prefer module-specific tags such as `#EJCYYYYMMDD_MODULO_TIPO` when the reason must be grep-able later.
- For VB edits, preserve UTF-8 with BOM.
- Prod KILLIOS is read-only.
- Treat Brain governance as direct local responsibility: legacy bridge/OpenClaw references are historical context, not active communication flows.

## Required Preflight

1. Read repo-local `AGENTS.md` or equivalent instructions.
2. If present in wms-brain, read Tier 0 governance before process work:
   - `brain/replit.md`
   - `brain/BRIDGE.md` only as historical protocol/context, not as an active send/apply workflow.
3. If present, use `brain/_index/ATLAS.md` as the Tier 1 table of contents. Load topic files only after matching frontmatter/triggers.
4. For operational processes, read:
   `C:/Users/yejc2/source/repos/wms-brain/wms-brain/brain/handoffs/2026-05-22-codex-performance-bof-hh/TRAZAS-FINAS-OPERATIVAS-INDEX-2026-05-26.yml`
5. If a trace exists for the process, load it. If not, create a focused trace before patching.
6. Load `brain/agents/_index.yml` and `brain/agents/coordinator.yml` only for complex or cross-domain work, then only the relevant `domain-*.yml`.
7. Check worktree status before edits and do not revert user changes.

## Domain Routing

Use the smallest domain set:
- HH Android: `TOMHH2025`, `domain-hh-android`, `domain-integration-services`.
- BOF/VB/WebMethods/DAL: `TOMWMS`, `domain-bof`, `domain-integration-services`.
- SQL/SP/view/table/column: `domain-database`, Brain `/search`, `/impact`, `/writers`.
- Cross-layer HH -> WS -> DAL -> SQL: coordinator + HH + integration-services + BOF + database.
- DB catalog/parametrizacion questions: use `wms-db-brain` first, then Brain API or read-only SQL only when the snapshot is insufficient.

## Operational Workflow

1. Reproduce the path in code:
   HH screen/event -> WS method -> DAL method -> movement/stock/table write.
2. Identify whether the symptom is data, state, concurrency, missing guard, wrong query, or wrong persistence.
3. Compare reference branches when the user asks or when regression is suspected:
   `dev_2023_estable`, `dev_2026_mampa`, `origin/dev_2026_mampa`.
4. Patch the causal point, not only the visible symptom.
5. For large BOF forms, search exact methods first and confirm the compiled copy from `.vbproj`; do not read/edit orphan duplicate files.
6. Add a short `#EJCYYYYMMDD` comment only where it preserves operational knowledge.
7. Build the affected layer:
   - BOF/WS: MSBuild `WSHHRN/WSHHRN.vbproj`.
   - HH: `gradlew.bat :app:compileDebugJavaWithJavac`.
8. Update the relevant Brain trace or topic file with the rule learned.
9. Attempt Brain/Janeway reindex if token is available and the indexed code may be stale; report if missing.

## Brain Context Budget

- Tier 0, always active when available: 9 binding rules, role ownership, direct Brain governance, team initials, and commit/tag conventions.
- Tier 1, first triage: `ATLAS.md`, active client, touched SP/table/method names, and current process trace.
- Tier 2, on demand: client snapshots, 2023-vs-2028 diffs, callsite flags, historical handoffs, and `PATTERNS-*.md`.
- Tier 3, only when Erik asks: raw JSON snapshots, SQL dumps, large logs, and full Janeway traces.
- For SQL object lookup, load compact `brain/_index/DB-BRAIN-BRANCH-MANIFEST.yml` or `wms-db-brain` scripts before reading generated object markdown.

Avoid context bloat:
- Do not read 10K-line forms wholesale. Use `rg -n "Sub|Function <name>"` then open a narrow range.
- Do not load all client snapshots when the ticket is single-client.
- Do not repeatedly verify git remotes/credentials in the same session unless a network operation failed.
- When porting fixes across drifted branches, adapt the idea to the destination structure instead of cherry-picking literally.

## Useful References

Read these only when needed:
- `references/architecture.md`: how this skill is versioned, installed, and recovered.
- `references/checklists.md`: operational checklist by task type.
- `references/paths.md`: known local repo paths and build commands.
- `references/brain-governance.md`: direct Brain governance, context tiers, historical bridge/OpenClaw caveats, and handoff intake.
- `references/health-and-performance.md`: maintenance rhythm, cleanup rules, and context performance guidance.
- `../wms-db-brain/SKILL.md`: SQL catalog and parametrizacion lookup from `origin/wms-db-brain`.
- `../wms-root-cause-accelerator/SKILL.md`: fast causal diagnosis workflow (trace2path, drift guard, null scan, post-fix verification).
- `../wms-regression-guardian/SKILL.md`: post-fix regression prevention workflow (footprint, sibling scan, residual-risk report).
- `../wms-state-machine-auditor/SKILL.md`: canonical state-machine extraction and validation across BOF/HH/WS/SQL.
- `../wms-telemetry-lite/SKILL.md`: low-cost end-to-end trace and parameter impact correlation.
- `../wms-guardrails-gate/SKILL.md`: pre-close go/no-go gate with evidence.
- `../wms-hh-quality-sweeper/SKILL.md`: continuous Android HH warning/lint cleanup with report-only and safe-fix modes.

## Scripts

- `scripts/install-skill.ps1`: copy this repo-versioned skill into Codex local skills.
- `scripts/validate-wms-skill.ps1`: validate skill structure and expected files.
- `scripts/wms-preflight.ps1`: print process trace, status, hotspots, and build commands before analysis.
- `scripts/wms-patch-check.ps1`: check patch hygiene before closeout.
- `scripts/wms-skill-doctor.ps1`: diagnose canonical Brain skill vs Codex runtime, wrong TOMWMS placement, stale install, and obsolete active bridge/OpenClaw wording.
- `scripts/wms-agent-maintenance.ps1`: run cleanup scan, validation, optional runtime install, doctor, optional Brain reindex, and git snapshot.
- `scripts/wms-build-routing-index.ps1`: regenerate `brain/_index/AGENT-ROUTING-INDEX.yml` for fast local context selection.

Run scripts from the repo root unless a script says otherwise.
