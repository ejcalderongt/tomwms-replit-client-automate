---
name: "federated-brain-bootstrap"
description: "Bootstrap federated workspace context from memory, brains, YAML maps, and workspace trees."
---

# Federated Brain Bootstrap

Use this skill to start a session with the smallest safe core of context, then expand into the right brain tree, YAML maps, memory, and workspace notes for the task at hand.

## Purpose

Build a repeatable session bootstrap for this workspace:
- load durable identity and memory first
- discover which brain owns the current task
- compile the relevant YAML maps and tree summaries
- surface a compact federated context pack for the session
- improve the bootstrap progressively as each day adds new durable context

## Session Startup Contract

When this skill is the first relevant context for a session:
- read the root anchors and the current daily note
- prefer `scripts/bootstrap-session.ps1` as the executable bootstrap entry point when available
- identify the active brain before reading task files
- load only the smallest relevant YAML set
- produce a compact context pack before deeper work
- if the user did not specify a target, ask for the active domain after showing the pack
- treat each session as a chance to refine what should be promoted to `MEMORY.md`

The bootstrap should feel automatic, but it must stay lightweight. Do not preload unrelated brains, agents, or historical notes unless the current task clearly needs them.

## Load Order

1. Read the workspace anchors:
   - `AGENTS.md`
   - `SOUL.md`
   - `USER.md`
   - `TOOLS.md`
   - `MEMORY.md`
   - `brain/federated-index.yml`
   - current daily note under `memory/YYYY-MM-DD.md`
2. Read the active project brain root:
   - `brain/` for federated routing and shared maps
   - `wms-brain/README.md` for WMS work
   - `mpos-brain/README.md` for mPos work
3. Load the narrowest task-specific context files next.
4. Load only the minimum related notes, traces, or scripts needed to act.

## Brain Routing Rules

- If the session is about WMS receiving, LP, packing, or implosion, prefer `brain/wms/recepcion/` and `brain/atlas/`.
- If the session is about mPos or MCP context, prefer `mpos-brain/`.
- If the session is about shared operational routing or workspace state, prefer `brain/` and the root workspace anchors.
- If the session needs startup routing, use `brain/federated-index.yml` as the authoritative source of brains and startup sets.
- If the task mentions implosion traces, load:
  - `brain/atlas/implosion-mapa.yml`
  - `brain/wms/recepcion/implosion/implosion-flujo.yml`
  - `brain/wms/recepcion/implosion/implosion-trazas.yml`
- If the task mentions LP flow or LP endpoint behavior, load:
  - `brain/wms/recepcion/lp-flujo-analisis.yml`
  - `brain/wms/recepcion/lp-endpoints.yml`
  - `brain/wms/recepcion/db-probes.yml`

## Federated Context Pack

When this skill runs, produce a compact context pack with:
- active workspace root
- active brain root
- current task domain
- relevant memory note(s)
- relevant YAML files
- relevant trace files
- open risks or blockers
- suggested next file to inspect

## Context Tree

Maintain a small tree in the output:
- Workspace
  - Root anchors
  - Memory
  - Active brain
  - Task brain
  - Trace files
  - Scripts
  - Notes

The tree should be a summary, not a dump. Include only files that matter to the current task.

## Output Contract

The skill output should answer these questions:
- What context is safe to assume now?
- Which files are authoritative for this task?
- Which brain should receive future edits?
- What is still missing and should be loaded next?

## Progressive Improvement Rule

This skill is intentionally evolutionary.
- Each session may add or refine durable context.
- New daily notes should promote important findings into `MEMORY.md` when they are stable.
- New files, trees, or YAML maps discovered during work should be added to the routing rules if they become recurring.
- If a brain starts to receive repeated work, promote it into the default routing set.
- Keep the skill lean even while it improves; prefer targeted additions over bulk dumping.
- Prefer to update the bootstrap contract after repeated misses, stale paths, or new durable brain trees are discovered.
- The federated index is the single source of truth for which brains exist and which startup set should be used for each route.
- The bootstrap script should stay thin and deterministic; the index owns the routing data.
- Review the bootstrap flow periodically and tighten it based on real session failures, missing context, or repeated lookups.

## Validation

Before acting on a change request, confirm:
- the right brain was selected
- the relevant YAML files were loaded
- the current memory note was checked
- no unrelated brain was pulled in
- the skill still reflects the newest stable context learned from daily work

## Notes

- Keep the skill lean. Do not turn it into a dump of all workspace knowledge.
- Prefer YAML and tree summaries over prose when the user wants structure.
- Prefer deterministic file discovery over memory guesswork.
- When the task is WMS implosion, favor the existing trace vocabulary instead of inventing new terms.
