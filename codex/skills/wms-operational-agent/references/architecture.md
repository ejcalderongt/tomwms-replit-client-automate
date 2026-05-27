# WMS Agent Architecture

## What Lives Where

Versioned source of truth:
- `TOMWMS/codex/skills/wms-operational-agent`
- Commit and push this folder to GitHub/Azure DevOps with the repo.

Codex runtime installation:
- `C:/Users/yejc2/.codex/skills/wms-operational-agent`
- Codex discovers skills from the local skills folder at startup/session load.

Brain operational memory:
- `C:/Users/yejc2/source/repos/wms-brain/wms-brain/brain/...`
- Use Brain traces for process-specific learned rules and handoffs, not as a replacement for the skill.

Repo rules:
- `AGENTS.md` remains the hard project contract.
- The skill should point to it and enforce it, not duplicate every line.

## Update Flow

1. Edit the versioned skill under `TOMWMS/codex/skills/wms-operational-agent`.
2. Validate it with `scripts/validate-wms-skill.ps1`.
3. Install/update local Codex copy with `scripts/install-skill.ps1`.
4. Commit/push the versioned folder when Erik approves.

## Recovery Flow

On a new machine or fresh Codex profile:
1. Clone/pull `TOMWMS`.
2. Run:
   `powershell -ExecutionPolicy Bypass -File codex/skills/wms-operational-agent/scripts/install-skill.ps1`
3. Restart Codex or open a new session so the skill metadata is discovered.

## Why This Is Efficient

The skill keeps stable workflow rules local and reusable. Brain keeps evolving process discoveries. The repo keeps everything recoverable. Codex local install makes the skill trigger automatically.
