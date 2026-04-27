---
id: mod-repo-tomwms-bof
type: module
title: Repo TOMWMS_BOF (backend core)
status: estable
sources:
  - skill: wms-tomwms §2
  - azure_devops: ejcalderon0892/TOMWMS_BOF
  - validated_at: 2026-04-26
---

# TOMWMS_BOF — backend core

| Atributo | Valor |
|---|---|
| Hosting | Azure DevOps `ejcalderon0892` |
| Tamaño | 376 MB · 9609 archivos |
| Lenguajes | VB.NET (3218 `.vb`), C# |
| Branch activa | `dev_2028_merge` (default Azure: `master`) |

## Rol

El "cerebro" del backend WMS. Contiene los proyectos clave del solution `TOMWMS_BOF.sln`. Detalle: `mod-arquitectura-solution`.

## Reglas de acceso desde Replit

- **Solo lectura** vía Azure DevOps REST API (PAT `AZURE_DEVOPS_PAT`, validado 2026-04).
- NO clonar el repo entero (376 MB) salvo extracción puntual.
- NUNCA pushear a Azure desde Replit. (Ver `rules/rule-01-no-push-automatico` cuando exista — M2.)
- Lectura on-demand por path conocido. Comandos pegables: `agent-context/AZURE_ACCESS.md`.

## Cross-refs
- `modules/mod-arquitectura-solution` — proyectos detallados del solution
- `modules/mod-protocolo-hh-ws` — punto de integración WSHHRN (M3)
- `decisions/dec-2026-04-killios-acceso-replit` — rationale del solo-lectura
