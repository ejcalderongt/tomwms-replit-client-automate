---
id: mod-repo-tomhh2025
type: module
title: Repo TOMHH2025 (handheld Android)
status: estable
sources:
  - skill: wms-tomwms §2
  - skill: wms-tomhh2025 (skill propio)
  - azure_devops: ejcalderon0892/TOMHH2025
  - validated_at: 2026-04-26
---

# TOMHH2025 — handheld Android

| Atributo | Valor |
|---|---|
| Hosting | Azure DevOps `ejcalderon0892` |
| Tamaño | 14 MB · 694 archivos |
| Lenguaje | Java |
| Archivos `.java` | 405 |
| Activities declaradas | 64 (en `AndroidManifest.xml`) |
| Módulos de negocio | 13 |
| Package raíz | `com.dts.*` |
| App ID | `com.dts.tom` |
| Versión actual | v8.2.3 |
| Android API | minSdk/targetSdk 24, compileSdk 34 |
| Branch activa | `dev_2028_merge` |

## Skill canónico de la HH

`brain/skills/wms-tomhh2025/SKILL.md` — **leer antes** de cualquier tarea HH. Tiene convenciones específicas (KSOAP2, encoding UTF-8, anti-pattern del replace ñ→n).

## Punto de integración con backend

SOAP + JSON-sobre-SOAP-envelope (Forma A) hacia `WSHHRN` del repo TOMWMS_BOF. Detalle de transports y reglas: `mod-protocolo-hh-ws` (M3).

## Cross-refs
- `modules/mod-protocolo-hh-ws` (M3)
- `modules/mod-repo-tomwms-bof`
- `rules/rule-no-mezclar-hh-backend` (M2)
- `rules/rule-utf8-bom-vb` (M2) — encoding también aplica al consumidor Java
