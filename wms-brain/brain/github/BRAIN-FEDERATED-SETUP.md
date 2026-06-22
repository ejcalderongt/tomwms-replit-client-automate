# Federated Brain Bootstrap (TOMWMS)

Global core:
- C:\Users\yejc2\source\repos\brain-core

Local overlay:
- C:\Users\yejc2\source\repos\TOMWMS\brain\github\project-overlay.yml

Federated peers:
- MCP live context: C:\Users\yejc2\source\repos\MCP\mcp-context-brain
- MCP publishable brain: C:\Users\yejc2\source\repos\MCP\mcp-brain
- MCP overlay: C:\Users\yejc2\source\repos\MCP\brain\project-overlay.yml

Quick resolve:
- `C:\Users\yejc2\source\repos\brain-core\tools\resolve-context.ps1 -RepoPath C:\Users\yejc2\source\repos\TOMWMS -Trigger reserva`

Default load strategy:
1. Load core router and policies.
2. Load TOMWMS overlay.
3. Load only trigger domains.

Notes:
- Keep secrets in env vars.
- Keep Jira in draft mode unless explicitly confirmed.
- Treat "federated brain" as local governance plus overlays, not as a live bridge transport.
- Prefer trimming duplicated context into the owning `domain-*.yml` or `handoffs/` entry before adding new files.
