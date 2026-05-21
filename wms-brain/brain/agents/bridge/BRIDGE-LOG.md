# BRIDGE LOG

Log append-only de eventos macro del puente Codex ↔ Mary Jane. NO mensajes individuales (esos van en los INBOX) — solo eventos relevantes a nivel de protocolo, decisiones de EJC, hitos de sync, registros nuevos de repos en Janeway, cambios al protocolo, etc.

Formato por entrada: `YYYY-MM-DD HH:MM UTC | actor | evento | refs`.

---

## 2026-05-21

- `13:17 UTC | codex | janeway sync-repos run id=55 ejecutado inadvertidamente | refs: indexRunId=55, finished=2026-05-21T13:17:23Z, delta files +91, symbols -265, refs -193`
- `13:45 UTC | ejc | decisión: wms-brain/ es principal, brain/ legacy queda de consulta | refs: chat sesión 2026-05-21`
- `13:45 UTC | ejc | decisión: establecer puente codex ↔ mary-jane auto-actualizable | refs: chat sesión 2026-05-21`
- `13:50 UTC | codex | bootstrap del puente: _bridge-protocol.md + 2 INBOX + este log | refs: wms-brain/brain/agents/bridge/*`

---
