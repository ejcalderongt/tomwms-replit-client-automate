# Handoffs Replit ↔ Codex local

Protocolo para coordinar trabajo entre el agente Replit (Brain Keeper) y Codex
local (Hands) sobre TOMWMS.

## Quién escribe qué

| Carpeta / archivo                              | Escribe              |
|------------------------------------------------|----------------------|
| `wms-brain/brain/code-changes/`                | Replit               |
| `wms-brain/brain/design-notes/`                | Replit               |
| `wms-brain/brain/data-deep-dive/`              | Replit               |
| `wms-brain/brain/db-changes/`                  | Replit               |
| `wms-brain/_index/ATLAS.md`                    | Replit               |
| `wms-brain/brain/handoffs/<slug>/BRIEF.md`     | Replit               |
| `wms-brain/brain/handoffs/<slug>/ACCEPTANCE.md`| Replit               |
| `wms-brain/brain/handoffs/<slug>/patches/`     | Replit               |
| `wms-brain/brain/handoffs/<slug>/RESULT.md`    | Codex                |
| `TOMWMS_BOF/` (Azure DevOps)                   | Codex                |
| `TOMHH2025/` (Azure DevOps)                    | Codex                |
| `DBA/` (GitHub `ejcalderongt/DBA`)             | Codex                |

Regla de oro: **solo un agente escribe en cada carpeta**. Si Codex necesita
modificar algo fuera de su scope, abre un nuevo handoff y se lo deja a Replit.

## Naming de handoffs

`YYYY-MM-DD-<area>-<accion-corta>`

Ejemplos:
- `2026-05-20-rfid-fix-tipo-dispositivo`
- `2026-05-22-bof-ajuste-modifica-cantidad-flag`
- `2026-06-01-hh-fix-replace-enie`

## Estados del frontmatter

- `propuesto` — Replit terminó de escribirlo, esperando que Codex lo tome.
- `en_progreso` — Codex empezó a aplicarlo.
- `aplicado` — Codex completó, todos los checks de ACCEPTANCE pasaron, commits empujados.
- `bloqueado` — Codex no pudo continuar; razón en RESULT.md `incidentes`.
- `rechazado` — Erik canceló el handoff antes de aplicar.

## Convención de commits cruzados

Todo commit (Azure DevOps o GitHub) que aplique un handoff lleva el slug al final:

- Azure DevOps: `#EJCRP fix(rfid): IdDispositivo datetime->int [brain:handoffs/2026-05-20-rfid-fix-tipo-dispositivo]`
- GitHub wms-brain (Codex push del RESULT): `[handoff 2026-05-20-rfid-fix-tipo-dispositivo] result: aplicado en DBA@abc123`

Con eso, desde cualquiera de los dos repos se puede rastrear al brain con un grep.

## Reglas vinculantes (recordar SIEMPRE en cada BRIEF)

1. NO commit/push automático a Azure DevOps sin permiso explícito de Erik **por sesión**.
2. NO mezclar cambios HH (Android) y BOF (VB.NET) en el mismo commit ni en el mismo handoff.
3. Mantener `ñ` (no aplicar `.replace("ñ","n")`).
4. Patrón JSON nuevo: Forma A (`{data, error}` wrapper + JavaScriptSerializer + status 200/500).
5. Codex NO toma decisiones de diseño. Si BRIEF tiene ambigüedad, marca `estado: bloqueado` y pide aclaración.
6. Codex NO accede a SQL EC2 productivo en v1.

## Flujo end-to-end

```
1. Erik pide algo a Replit (chat)
2. Replit analiza, propone en chat
3. Erik aprueba -> Replit escribe BRIEF + patches + ACCEPTANCE en wms-brain/brain/handoffs/<slug>/
4. Replit pushea a GitHub
5. Erik abre Codex local -> "ejecutá el handoff <slug>"
6. Codex hace git pull en wms-brain
7. Codex lee BRIEF -> aplica patches -> corre ACCEPTANCE
8. Codex muestra diff -> Erik aprueba explicito -> Codex commitea a Azure DevOps / DBA
9. Codex escribe RESULT.md -> push a GitHub wms-brain
10. Erik avisa a Replit; Replit actualiza ATLAS y ficha de cliente
```

## System prompt sugerido para Codex local

Pegar en "Habilidades" / system instruction de Codex:

> Antes de cualquier tarea sobre TOMWMS:
> 1. Ejecutá `git -C <ruta-local>/wms-brain pull --ff-only`.
> 2. Toda decisión de diseño se acata de `wms-brain/brain/handoffs/<slug>/BRIEF.md`.
> 3. NO modifiques archivos fuera de TOMWMS_BOF, TOMHH2025, DBA y `wms-brain/brain/handoffs/<slug>/RESULT.md`.
> 4. Las reglas vinculantes están en `wms-brain/brain/handoffs/_README.md §Reglas`.
> 5. NO commit/push automático sin confirmación explícita de Erik en la sesión actual.
> 6. Si encontrás ambigüedad en el BRIEF, completá `RESULT.md` con `estado: bloqueado` y razón. NO improvises.
