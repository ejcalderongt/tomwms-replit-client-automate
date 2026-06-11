---
name: wms-bof-hh-guardrails
description: Use for TOMWMS BOF legacy VB.NET and HH Android changes: WinForms/DynamicsNavInterface, TOMIMSV4, .asmx WebMethods, Java HH callers, Reference.vb guardrails, UTF-8 BOM VB files, preventing mixed HH/VB commits, impact analysis across BOF-HH-WebAPI layers, and safe UI/logging improvements.
---

# WMS BOF HH Guardrails

## Load First

Inside `C:\Users\yejc2\source\repos\TOMWMS`, read:

1. `AGENTS.md`
2. `brain/agents/_index.yml`
3. For WinForms/VB/legacy: `brain/agents/domain-bof.yml`
4. For SQL touch: `brain/agents/domain-database.yml`
5. For reservation UI/logging: `brain/agents/domain-reserva.yml`

If a dedicated `domain-hh-android.yml` exists later, load it for HH Java tasks.

## Hard Rules

- Do not edit `Reference.vb`.
- Do not mix HH Java and VB/Core changes in the same commit.
- Keep VB files as UTF-8 with BOM.
- Debug first; do not rewrite from scratch.
- Use Brain impact/dependencies when changing public WebMethods or shared DAL methods.

## BOF Legacy Workflow

1. Locate implementation in `*.asmx.vb`, forms, or DAL, not generated proxies.
2. Read the surrounding method and current UI/data contract.
3. Preserve business logic and add UI/log improvements around it.
4. For reservation UI, keep Process_Result/DB logs authoritative; colors are only visual support.
5. Build or run the smallest available validation for the touched project.

## HH Workflow

1. Identify Java caller and BOF WebMethod.
2. Use Brain impact before rename/signature changes.
3. Update Java and VB together only when the task explicitly requires cross-layer work, but keep the commit scoped and documented.
4. Never change generated proxy files as the source of truth.

## Fast Path 2026-06 (Etiquetas + Reemplazo TC)

Use this when symptoms mention `licencia`, `fardo`, `etiqueta`, `talla/color`, `reemplazo`, `JSON vs SOAP`.

1. Load:
   - `brain/agents/domain-hh-android.yml`
   - `brain/handoffs/2026-06-11-operativo-hh-bof-etiquetas-reemplazo-tc/README.md`
2. Verify payload/list contract first:
   - `IdProductoTallaColor`, `Codigo_Talla`, `Codigo_Color`.
3. If HH list is JSON-based:
   - Keep JSON primary.
   - Add SOAP fallback only when JSON arrives without critical TC fields.
4. In HH cursors:
   - Prefer column-name mapping; keep index fallback.
5. In VB DAL loader:
   - Accept aliases (`Codigo_Talla/Codigo_Color` and `Talla/Color`) to avoid brittle coupling with view shape.

Quick validation:
- Scan/select license in HH and confirm TC is visible.
- Run replacement and confirm TC persists.
- Compile HH (`:app:compileDebugJavaWithJavac`) before close.

## Commit Hygiene

- Keep BOF-only and HH-only changes separate unless Erik explicitly requests a cross-layer change.
- Document any cross-layer contract change in the final response and in repo context when reusable.
