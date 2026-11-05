---
tipo: other
autores: [erik]
---
# WMS Paths And Commands

## Repos

Run `../scripts/resolve-wms-paths.ps1 -AsJson` first. Its output is authoritative
for this workstation. The manifest is relative to BrainRoot; the installer
stores a local locator outside the repository. Pass `-BrainRoot`, `-RepoRoot`
and `-HhRoot` explicitly to legacy helpers instead of trusting their old defaults.

- BOF/WS: resolved `RepoRoot` (currently `USERPROFILE/source/repos/TOMWMS_BOF`).
- HH: resolved `HhRoot` (currently `USERPROFILE/source/repos/TOMHH2025`).
- Brain: resolved `BrainRoot`; do not infer it from an obsolete clone path.

## Brain

- Base URL: `https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain`
- Token env var: `BRAIN_IMPORT_TOKEN`
- Governance, when synchronized locally:
  - `BrainRoot/AGENTS.md` is the current entry point.
  - `BrainRoot/brain/replit.md` and `BrainRoot/brain/BRIDGE.md` are historical where they conflict with current governance.
  - `BrainRoot/brain/_index/ATLAS.md` is a discovery map; use the source catalog for duplicate/disputed material.
- Reindex VB: `POST /index/vb` with `{"repos":["TOMWMS_BOF"]}`
- Reindex Java: `POST /index/java` with `{"repos":["TOMHH2025"]}`

## Builds

BOF/WS:
```powershell
& "C:/Program Files/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe" WSHHRN/WSHHRN.vbproj /t:Build /p:Configuration=Debug /p:Platform="AnyCPU" /v:minimal
```

HH:
```powershell
Set-Location $paths.HhRoot # $paths is the object returned by resolve-wms-paths.ps1
./gradlew.bat :app:compileDebugJavaWithJavac
```

## Skill Automation

Preflight:
```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-operational-agent/scripts/wms-preflight.ps1 -Process picking
```

Patch check:
```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-operational-agent/scripts/wms-patch-check.ps1
```

Skill doctor:
```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-operational-agent/scripts/wms-skill-doctor.ps1
```

Agent maintenance:
```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-operational-agent/scripts/wms-agent-maintenance.ps1 -CleanCaches -InstallRuntime
```

Routing index:
```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-operational-agent/scripts/wms-build-routing-index.ps1
```

## Operational Trace Index

```text
BrainRoot/brain/handoffs/2026-05-22-codex-performance-bof-hh/TRAZAS-FINAS-OPERATIVAS-INDEX-2026-05-26.yml
```
