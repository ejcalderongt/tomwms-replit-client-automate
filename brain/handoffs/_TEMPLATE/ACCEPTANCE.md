---
tipo: other
autores: [erik]
---
# Verificaciones obligatorias

Marca como `[x]` cuando se cumplan. Codex debe llenar todas antes de pasar a `aplicado`.

## Tecnicas
- [ ] (ej: `sys.columns` reporta IdDispositivo como int)
- [ ] (ej: Build de TOMIMSV4.csproj exitoso, 0 errores)
- [ ] (ej: Test manual X devuelve Y)

## Commits
- [ ] Commit pusheado a `<repo>` con mensaje:
  `#EJCRP <tipo>(<area>): <msg> [brain:handoffs/<slug>]`
- [ ] Commit referencia el slug entre corchetes al final del mensaje.

## RESULT
- [ ] `RESULT.md` completado con:
  - `estado: aplicado`
  - `fecha_aplicado` en formato ISO
  - `commits[].sha` con hash real
  - `incidentes` (o "ninguno")
- [ ] `RESULT.md` pusheado a GitHub wms-brain.
