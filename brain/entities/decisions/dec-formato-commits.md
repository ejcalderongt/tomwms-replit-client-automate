---
id: dec-formato-commits
type: decision
title: Formato de commits y autoría por persona
status: vigente
sources:
  - skill: wms-tomwms §3
  - validated_at: 2026-04-27
---

# Formato de commits y autoría

Convención de commits por persona del equipo en TOMWMS_BOF y TOMHH2025.

| Iniciales | Persona | Formato de commit |
|---|---|---|
| **EJC** | Erik José Calderón (owner) | `#EJCRP <tipo>(<área>): <msg>` (en TOMWMS_BOF) |
| **GT** | Efren Gustavo Tuyuc | `#GT_DDMMAAAA:` |
| **AG** | Abigail Gaitán | `#AGDDMMAAAA` |
| **MA** | Marcela Álvarez | `#MA DDMMAAAA` o `#MADDMMAAAA` |
| **AT** | Anderly Teleguario | `#AT DDMMAAAA` |
| **MECR** | Melvin Cojtí | `#MECR DDMMAAAA` |
| **CF** | Carolina Fuentes | (formato por confirmar) |

Para identificar autor de cualquier convención particular (ej. reglas en `WebService.java`), buscar el prefijo en comentarios.

## Cross-refs
- `modules/mod-repo-tomwms-bof`
- `modules/mod-repo-tomhh2025`
