# mpos-brain

Workspace anchor for mPos / MCP context.

## Purpose

- Keep durable operational context in one place.
- Store decisions, traces, conventions, and runbooks here.
- Reference the application code, but do not copy source code into the brain.

## Explicit Separation Rule

- Brain folders are for context, documentation, learning, and reusable instructions.
- Application source code stays in the product repository, not in `brain/`, `docs/`, `learning/`, or `agents/`.
- If a fix or behavior change needs to be remembered, document the rule and the file path here, but keep the implementation in code.
- Kelvyn rule: do not mix code with documentation or learning folders. Keep that boundary explicit.

## Suggested Layout

- `agents/` for reusable agent instructions and operational notes.
- `branches/` for branch-level context, handoffs, and scan notes.
- `docs/` for runbooks, decisions, and learning notes.
- `modules/` for module-specific context maps.

## Startup Rule

Read this file first, then the relevant memory note, then the most recent task note before resuming work.
