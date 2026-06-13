# brain-core

Federated context core for all local projects.

## Goals
- Keep one shared core (policies, patterns, infra profile model).
- Keep per-project overlays isolated.
- Load only what each task needs.
- Reuse the same onboarding and Jira guardrails across repos.

## Main files
- `brain-router.yml`: global routing rules.
- `catalog/projects.yml`: known repos and their overlay entry points.
- `policies/*.yml`: shared governance.
- `shared/*`: reusable patterns and glossary.
- `templates/new-project-bootstrap/*`: starter kit for new repos.
- `tools/init-project-brain.ps1`: scaffold a project overlay.

## Security
- Never store real passwords/tokens here.
- Use env vars and aliases only.

## Suggested flow
1. Identify active repo.
2. Load `brain-router.yml`.
3. Load global policies.
4. Load project overlay from `catalog/projects.yml`.
5. Load only triggered domain packs.
