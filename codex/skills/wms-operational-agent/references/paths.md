# WMS Paths And Commands

## Repos

- TOMWMS/BOF/WS: `C:/Users/yejc2/source/repos/TOMWMS`
- HH Android: `C:/Users/yejc2/StudioProjects/TOMHH2025`
- wms-brain: `C:/Users/yejc2/source/repos/wms-brain/wms-brain`

## Brain

- Base URL: `https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain`
- Token env var: `BRAIN_IMPORT_TOKEN`
- Reindex VB: `POST /index/vb` with `{"repos":["TOMWMS_BOF"]}`
- Reindex Java: `POST /index/java` with `{"repos":["TOMHH2025"]}`

## Builds

BOF/WS:
```powershell
& "C:/Program Files/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe" WSHHRN/WSHHRN.vbproj /t:Build /p:Configuration=Debug /p:Platform="AnyCPU" /v:minimal
```

HH:
```powershell
cd C:/Users/yejc2/StudioProjects/TOMHH2025
./gradlew.bat :app:compileDebugJavaWithJavac
```

## Skill Automation

Preflight:
```powershell
powershell -ExecutionPolicy Bypass -File codex/skills/wms-operational-agent/scripts/wms-preflight.ps1 -Process picking
```

Patch check:
```powershell
powershell -ExecutionPolicy Bypass -File codex/skills/wms-operational-agent/scripts/wms-patch-check.ps1
```

## Operational Trace Index

```text
C:/Users/yejc2/source/repos/wms-brain/wms-brain/brain/handoffs/2026-05-22-codex-performance-bof-hh/TRAZAS-FINAS-OPERATIVAS-INDEX-2026-05-26.yml
```
