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
- For VB edits, preserve UTF-8 with BOM.
- Prod KILLIOS is read-only.

## Required Preflight

1. Read repo-local `AGENTS.md` or equivalent instructions.
2. For operational processes, read:
   `C:/Users/yejc2/source/repos/wms-brain/wms-brain/brain/handoffs/2026-05-22-codex-performance-bof-hh/TRAZAS-FINAS-OPERATIVAS-INDEX-2026-05-26.yml`
3. If a trace exists for the process, load it. If not, create a focused trace before patching.
4. Load `brain/agents/_index.yml` and `brain/agents/coordinator.yml` only for complex or cross-domain work, then only the relevant `domain-*.yml`.
5. Check worktree status before edits and do not revert user changes.

## Domain Routing

Use the smallest domain set:
- HH Android: `TOMHH2025`, `domain-hh-android`, `domain-integration-services`.
- BOF/VB/WebMethods/DAL: `TOMWMS`, `domain-bof`, `domain-integration-services`.
- SQL/SP/view/table/column: `domain-database`, Brain `/search`, `/impact`, `/writers`.
- Cross-layer HH -> WS -> DAL -> SQL: coordinator + HH + integration-services + BOF + database.

## Operational Workflow

1. Reproduce the path in code:
   HH screen/event -> WS method -> DAL method -> movement/stock/table write.
2. Identify whether the symptom is data, state, concurrency, missing guard, wrong query, or wrong persistence.
3. Compare reference branches when the user asks or when regression is suspected:
   `dev_2023_estable`, `dev_2026_mampa`, `origin/dev_2026_mampa`.
4. Patch the causal point, not only the visible symptom.
5. Add a short `#EJCYYYYMMDD` comment only where it preserves operational knowledge.
6. Build the affected layer:
   - BOF/WS: MSBuild `WSHHRN/WSHHRN.vbproj`.
   - HH: `gradlew.bat :app:compileDebugJavaWithJavac`.
7. Update the relevant Brain trace with the rule learned.
8. Attempt Brain reindex if token is available; report if missing.

## Useful References

Read these only when needed:
- `references/architecture.md`: how this skill is versioned, installed, and recovered.
- `references/checklists.md`: operational checklist by task type.
- `references/paths.md`: known local repo paths and build commands.

## Scripts

- `scripts/install-skill.ps1`: copy this repo-versioned skill into Codex local skills.
- `scripts/validate-wms-skill.ps1`: validate skill structure and expected files.
- `scripts/wms-preflight.ps1`: print process trace, status, hotspots, and build commands before analysis.
- `scripts/wms-patch-check.ps1`: check patch hygiene before closeout.

Run scripts from the repo root unless a script says otherwise.
