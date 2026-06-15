---
name: wms-mampa-interface
description: MAMPA BOF interface tracing and change workflow for SAPSYNCMAMPA, clsSyncTransacWMS, Service Layer filters, ajuste idempotency by Referencia, talla/color, stock_rec, UI progress, and fine debug traces. Use when modifying or diagnosing the MAMPA interface or when creating structured traces and knowledge notes for that flow.
---

# WMS MAMPA Interface

Use this skill for the MAMPA interface only. Keep the scope on
`SAPSYNCMAMPA` and the related DAL callers.

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

