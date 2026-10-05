---
name: wms-mampa-interface
description: MAMPA SAP Business One synchronization tracing and change workflow for SAPSYNCMAMPA on dev_2026_estable, SAPBOSyncMampa, clsSyncTransacWMS, Get_Traslados_SAP_SL, Get_Bodegas_SAP, Service Layer filters and errors, ajuste idempotency by Referencia, talla/color, stock_rec, UI progress, and fine debug traces. Use whenever MAMPA, SAPSYNCMAMPA, its SAP interface, or these methods are mentioned. Do not route this project to ROAD/Toledano.
---

# WMS MAMPA Interface

Use this skill for the MAMPA interface only. Keep the scope on
`SAPSYNCMAMPA` and the related DAL callers.

## Project identity

- Client: MAMPA.
- ERP: SAP Business One through Service Layer.
- Synchronization project: `SAPSYNCMAMPA` / `SAPBOSyncMampa`.
- Carolina's target final/stable branch: `dev_2026_estable`.
- Erik's active MHS branch: `dev_2028_merge`.
- Treat both branches as active. Confirm the requested client, scope, operator, and checked-out branch before changing code.
- Never confuse this project with ROAD/Toledano.
- Verify the checked-out repository and branch before changing code.

## First read

- `brain/code-deep-flow/traza-003-sapsyncmampa-interface.yml`
- `brain/code-deep-flow/traza-003-sapsyncmampa-interface.md`
- `brain/fingerprint/MAMPA.md`
- `brain/learnings/L-052-proc-transac-wms-idempotencia-por-documento.md`

## Change flow

1. Run the MAMPA scan script to locate the exact methods and trace points.
2. Open only the methods the script reports.
3. Change the interface first when the rule belongs to MAMPA.
4. Keep the cyclic inventory classes untouched unless the task explicitly asks for them.
5. Add fine debug traces before and after the causal point.
6. Update the trace and, if needed, add a short learning note in `brain/learnings/`.

## Stable rules

- Validate by `Referencia` when the document is inserted into
  `trans_ajuste_enc`.
- Use `CKFKYYMMDDFeature` for inline tags in code and trace notes.
- Prefer Service Layer filtering for obvious data exclusion, and keep
  LINQ as a fallback or safety net.
- Keep UI progress updates wrapped in a safe helper.
- Do not mix BOF and HH changes in the same task.

## Automation

Run the scan script when starting a change:

```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-mampa-interface/scripts/wms-mampa-scan.ps1 -RepoRoot C:\Users\carol\source\repos\TOMWMS_BOF
```

The script reports:

- main entry points
- adjustment hotspots
- trace anchors
- files to inspect first

## Output files to keep current

- `brain/code-deep-flow/traza-003-sapsyncmampa-interface.yml`
- `brain/code-deep-flow/traza-003-sapsyncmampa-interface.md`
- `brain/learnings/` entries for new findings
