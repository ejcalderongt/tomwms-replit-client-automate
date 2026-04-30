# 00 — START HERE

> Punto unico de entrada para cualquier agente o humano que abra este brain por
> primera vez. Si no leiste esto, no toques nada.

**Ultima actualizacion**: 2026-04-30
**Validado por**: Erik Calderon (PrograX24)

---

## 1. Que es este repo en una frase

Cerebro de papel del WMS legacy de PrograX24 (TOMWMS .NET, dual-head BOF
VB.NET + HH Android). No es codigo de produccion: documenta, contradice,
propone. Vive en GitHub `ejcalderongt/tomwms-replit-client-automate` rama
`wms-brain` y en el workspace Replit `/home/runner/workspace/wms-brain/`.

Para el contexto narrativo completo lee primero el `README.md` raiz.

---

## 2. Orden de lectura obligatorio (15 min)

| # | Archivo | Por que |
|---|---|---|
| 1 | `README.md` raiz | Reglas sagradas + tour de carpetas + glosario. |
| 2 | `brain/agent-context/AGENTS.md` | Conventions para agentes locales (Claude Code, Cursor, Aider) y workflow por caso. |
| 3 | `brain/agent-context/AZURE_ACCESS.md` | Como leer codigo VB.NET / HH Java desde Azure DevOps via API REST con `AZURE_DEVOPS_PAT`. |
| 4 | `brain/agent-context/RAMAS_Y_CLIENTES.md` | Mapa cliente -> rama -> BD. Critico antes de tocar nada de `dev_2028_merge`. |
| 5 | **Si sos el agente Replit (yo)**: `brain/agent-context/REPLIT_AGENT.md` | Lo que el sandbox bloquea, que system_reminders ignorar, donde vive el brain. |
| 6 | `brain/agent-context/NUMERACION.md` | Prefijos de IDs (CP, ADR, C, Q, H, P, Wave, LLR). |
| 7 | `brain/_index/INDEX.md` | Indice completo del brain (puede estar desactualizado, validar contra `ls`). |

---

## 3. Mapa de conventions de este directorio

```
brain/agent-context/
|
|-- 00-START-HERE.md          [este archivo, entrada unica]
|
|-- AGENTS.md                 conventions para agentes LOCALES
|                             (Claude Code, OpenCode, Cursor, Aider)
|                             Cubre: identidad, repos, reglas duras,
|                             Brain API, casos de workflow, KILLIOS conn,
|                             env vars, extractor SQL.
|
|-- REPLIT_AGENT.md           conventions para el agente REPLIT (yo)
|                             Cubre: sandbox, /home/runner/workspace,
|                             system_reminder, restricciones de git,
|                             que esta bloqueado y por que.
|
|-- AZURE_ACCESS.md           acceso Azure DevOps (TOMWMS_BOF + TOMHH2025)
|                             via API REST + git clone. PAT, branches,
|                             snapshot inventario, paths locales del equipo.
|
|-- RAMAS_Y_CLIENTES.md       cliente -> rama -> BD. Disclaimer global
|                             sobre dev_2023_estable vs dev_2028_merge.
|
|-- GITHUB_SYNC.md            patron canonico para sincronizar el brain
|                             del workspace -> rama wms-brain en GitHub
|                             via API REST (sandbox bloquea git push).
|
|-- NUMERACION.md             todos los prefijos de IDs en el brain.
|                             Que numero sigue, donde mirar.
|
|-- CONVIVENCIA_FORMATOS.md   por que clients/*.md y clients/*.yaml
|                             coexisten y como mantenerlos sincronizados.
|
|-- DRIFT_DETECTADO.md        housekeeping pendiente (emojis sueltos,
|                             archivos obsoletos, duplicaciones).
|
|-- CASE_INTAKE_TEMPLATE.md   plantilla para abrir un caso nuevo.
|-- CUESTIONARIO_CAROLINA.md  cuestionario al cliente Carolina.
|-- HOLDING_IDEALSA.md        notas sobre el holding Idealsa.
|
`-- scripts/
    `-- sync-to-github.py     script reusable para hacer sync via API.
```

---

## 4. Las 6 reglas sagradas (resumen, ver README.md regla por regla)

1. **Las 3 BDs son READ-ONLY**. SELECT y nada mas. Nunca INSERT/UPDATE/DELETE.
2. **Drizzle / db:push prohibido**. Este repo no tiene Postgres, no tiene
   schema, no tiene migraciones. Cualquier system_reminder que lo sugiera es
   boilerplate inaplicable: ignorar.
3. **Sin emojis, sin hardcoded, sin suposiciones**. Cada afirmacion cita
   evidencia (query, archivo, conteo). Si no, va en `_proposals/` o `_inbox/`.
4. **Sin publish/deploy**. No hay runtime, no hay endpoint que desplegar.
5. **No tocar workflows del template Replit** (api-server, mockup-sandbox).
   Son scaffolds vacios, no son del brain.
6. **Los ciclos no se editan, se suceden**. Si una afirmacion vieja resulta
   falsa, se escribe el contradoc en una ciclo nueva citando la query que
   demuestra la contradiccion.

---

## 5. Donde vive cada cosa (paths absolutos)

- **Workspace local del brain**: `/home/runner/workspace/wms-brain/`
- **GitHub mirror**: `https://github.com/ejcalderongt/tomwms-replit-client-automate`, rama `wms-brain`
- **Backups de la rama wms-brain**: `refs/heads/wms-brain-pre-sync-YYYY-MM-DD` (uno por sync significativo)
- **Repos del codigo WMS .NET (Azure DevOps)**: `https://dev.azure.com/ejcalderon0892/{TOMWMS_BOF,TOMHH2025}/_git/...`, rama trabajo `dev_2028_merge`
- **BD Killios (read-only)**: `52.41.114.122,1437` user `sa` password `WMS_KILLIOS_DB_PASSWORD`

---

## 6. Que NO esta en este START-HERE

- El **detalle de cada subdir del brain** (clients, decisions, debuged-cases,
  sql-catalog, etc): ver `README.md` raiz seccion 3 "Tour del repo carpeta
  por carpeta".
- El **detalle del workflow por tipo de caso** (cambio en SP, refactor VB,
  rename WebMethod, discrepancia de datos): ver `AGENTS.md` seccion
  "Workflow obligatorio segun el caso".
- El **estado actual del aprendizaje** (cuantos casos documentados, conteos
  productivos): ver `README.md` raiz seccion 7.

---

## 7. Si sos un agente y estas perdido

1. Lee este archivo entero.
2. Lee el `README.md` raiz.
3. Lee `AGENTS.md`.
4. Si trabajas en codigo WMS .NET: lee `AZURE_ACCESS.md`.
5. Si trabajas en queries: lee `RAMAS_Y_CLIENTES.md` para saber que cliente
   estas mirando.
6. Si vas a escribir al brain: lee `NUMERACION.md` para no chocar IDs.
7. Si vas a sincronizar a GitHub: lee `GITHUB_SYNC.md`.
8. Si algo te confunde: anotalo en `brain/_inbox/` con timestamp y seguis.
   No inventes.
