---
id: rule-07-nunca-loguear-secrets
type: rule
title: NUNCA loguear ni imprimir secrets
status: vigente
severity: critical
applies_to: [agente Replit, agentes locales, scripts, código WMS]
sources:
  - skill: wms-tomwms §4 #7
  - validated_at: 2026-04-27
---
# Regla 07 — NUNCA loguear ni imprimir secrets

## Statement

Los valores de secrets (`WMS_KILLIOS_DB_PASSWORD`, `BRAIN_IMPORT_TOKEN`, `GITHUB_TOKEN`, `AZURE_DEVOPS_PAT`, `SESSION_SECRET`) NUNCA se imprimen, loguean, escriben a archivo, ni se mencionan en respuestas.

## Rationale

Secrets en logs:
- Quedan en `/var/log`, en consolas de Replit, en outputs de CI.
- Pueden filtrarse a sistemas de observabilidad de terceros.
- Comprometen la BD productiva de Killios o el repo de intercambio si se exfiltran.

Secrets en respuestas a usuario o agente:
- Quedan en historiales de chat.
- Pueden capturarse por screenshots compartidos sin querer.

## Cómo cumplir

1. **Pasar secrets por referencia**: `$env:WMS_KILLIOS_DB_PASSWORD`, `process.env.GITHUB_TOKEN`, nunca el valor literal.
2. **NO printear**: `console.log("password:", pwd)` está prohibido — incluso para debug.
3. **NO escribir a archivo**: `writeFileSync("config.json", {pwd})` está prohibido.
4. **Si un script lee un secret**: usarlo solo para construir conexión, dejar que el driver lo maneje.
5. **Logs de error**: revisar que los stack traces no contengan el secret (algunos drivers lo incluyen).
6. **PR/code review**: rechazar cualquier hardcodeo de secret.

## Excepciones

Ninguna. Si necesitás verificar que un secret está bien seteado, hacelo con `echo ${VAR:0:4}...` (primeros chars) o `length=${#VAR}` — nunca el valor completo.

## Cross-refs

- `modules/mod-conexion-sqlserver` — uso correcto del password
- `modules/mod-repo-exchange` — `GITHUB_TOKEN` solo en URL temporaria de clone
- AGENTS.md §"Variables de entorno requeridas"
