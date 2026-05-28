---
tipo: other
autores: [erik]
---
# WMS Agent Architecture

## What Lives Where

Versioned source of truth:
- `wms-brain/brain/skills/wms-operational-agent`
- Commit and push this folder with the Brain repository.

Codex runtime installation:
- `C:/Users/yejc2/.codex/skills/wms-operational-agent`
- Codex discovers skills from the local skills folder at startup/session load.

Brain operational memory:
- `C:/Users/yejc2/source/repos/wms-brain/wms-brain/brain/...`
- Use Brain traces for process-specific learned rules and handoffs, not as a replacement for the skill.
- Legacy bridge/OpenClaw handoffs from Replit/Mary Jane are historical context. Codex now has direct governance and should update Brain indexes/patterns locally when appropriate.

Repo rules:
- `AGENTS.md` remains the hard project contract.
- The skill should point to it and enforce it, not duplicate every line.

## Update Flow

1. Edit the versioned skill under `wms-brain/brain/skills/wms-operational-agent`.
2. Validate it with `scripts/validate-wms-skill.ps1`.
3. Install/update local Codex copy with `scripts/install-skill.ps1`.
4. Commit/push the versioned folder when Erik approves.

## Recovery Flow

On a new machine or fresh Codex profile:
1. Clone/pull `wms-brain`.
2. Run:
   `powershell -ExecutionPolicy Bypass -File brain/skills/wms-operational-agent/scripts/install-skill.ps1`
3. Restart Codex or open a new session so the skill metadata is discovered.

## Why This Is Efficient

The skill keeps stable workflow rules local and reusable. Brain keeps evolving process discoveries. The repo keeps everything recoverable. Codex local install makes the skill trigger automatically.

## Governance Model

The mature model is not many runtime agents or a machine-to-Replit bridge doing uncontrolled edits. It is:
- One local Codex coordinator.
- Narrow domain packets from Brain.
- Direct local Brain curation by Codex.
- Historical bridge/OpenClaw scripts reused only when their parsing or diagnostic logic is useful.
- Durable `PATTERNS-*.md`, process traces, and indexed topic files.

This keeps context cost down because Codex loads stable rules once, then retrieves only the domain file or process trace needed for the current fix.
