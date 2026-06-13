# Federated Brain Design

## Model
- Core: reusable policies, patterns, profile aliases.
- Overlay: project-specific domains and client packs.
- Router: selects minimal context by repo + trigger.

## Why this works
- No full-context copy between projects.
- Shared rules loaded once.
- Local details stay local.

## Onboarding for new project
1. Run `tools/init-project-brain.ps1`.
2. Add project to `catalog/projects.yml`.
3. Add domain files only for that repo.
4. Keep secrets in env vars.

## Obsidian integration
- Use `brain-core` as vault root.
- Keep one note per domain and handoff.
- Link project overlays to shared patterns.
