---
tipo: other
---
# Agent Health And Performance

## Goal

Keep Codex fast by separating stable rules, routing indexes, operational traces, and raw evidence.

## Maintenance Rhythm

Run after skill/Brain changes:
```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-operational-agent/scripts/wms-agent-maintenance.ps1 -InstallRuntime
```

Run deeper maintenance when caches or stale indexes are suspected:
```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-operational-agent/scripts/wms-agent-maintenance.ps1 -CleanCaches -InstallRuntime -BuildRoutingIndex -ReindexBrain
```

## Healthy State

- Canonical skill lives only in `wms-brain/brain/skills/wms-operational-agent`.
- Runtime skill under `.codex/skills/wms-operational-agent` matches canonical source by hash.
- TOMWMS has no physical `codex/skills/wms-operational-agent` folder.
- `brain/_index/AGENT-ROUTING-INDEX.yml` exists and is regenerated after Brain structure changes.
- Bridge/OpenClaw are historical references, not active transport.
- Operational traces stay focused by process.
- Large raw artifacts stay out of Tier 0/Tier 1 context.

## Context Loading Budget

- Tier 0: hard rules, role ownership, direct governance, commit/tag conventions.
- Tier 1: process trace index, active process trace, client/branch, touched symbols.
- Tier 2: patterns, branch diffs, historical handoffs, callsite flags.
- Tier 3: raw logs, snapshots, dumps, and full traces only on explicit request.

## Cleanup Rules

Safe to remove automatically:
- `__pycache__`
- `.pytest_cache`
- `.mypy_cache`
- `.ruff_cache`

Do not remove automatically:
- Brain handoffs.
- Domain files.
- Operational traces.
- Jira/task assistant output.
- SQL/catalog material.
- Any user-created untracked folder unless Erik explicitly says so.

## Reindex Rules

Use Brain/Janeway reindex when:
- VB/BOF/WS code changed and semantic impact queries may be stale.
- HH Java code changed and Java caller/callee search may be stale.
- A handoff explicitly says Janeway is behind known commits.

Skip reindex when:
- Only skill docs/scripts changed.
- Only local runtime install changed.
- `BRAIN_IMPORT_TOKEN` is unavailable.

## Next Automation Candidate

Improve the routing-index scanner to parse richer frontmatter from:
- `brain/agents/domain-*.yml`
- `brain/handoffs/**/*.yml`
- `brain/code-changes/**/PATTERNS-*.md`

The generated index should be small and disposable, so Codex can choose files by metadata before reading bodies.
