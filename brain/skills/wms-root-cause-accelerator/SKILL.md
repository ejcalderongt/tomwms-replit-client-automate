---
name: wms-root-cause-accelerator
description: Fast root-cause acceleration for TOMWMS operations. Use for HH/BOF incidents when we need trace-to-path mapping, branch drift detection, null/empty/list guard scanning, shared-reference array corruption, and post-fix verification evidence for picking, recepcion, packing, verificacion, existencias, inventario, and cambio_ubicacion.
---

# WMS Root Cause Accelerator

## Purpose

Deliver an `X+` in diagnosis speed and fix quality by converting incident symptoms into a repeatable path:

1. trace2path
2. branch-drift-guard
3. null-risk-scanner
4. **shared-reference-scanner** ← nuevo (BUG-003B, 2026-05-28)
5. post-fix-verifier

## Inputs

- Process: `picking`, `cambio_ubicacion`, `verificacion`, `recepcion`, or `existencias`.
- Symptom: user-visible error or unexpected behavior.
- Optional refs: suspect methods, tables, SPs, flags, or branches.

## Outputs

- RCA YAML report under:
  `wms-brain/brain/handoffs/2026-05-27-root-cause-accelerator/`
- Candidate path `HH -> WS -> DAL -> SQL`.
- Risk findings for null/empty/list failures.
- Branch-drift evidence vs baseline branches.
- Post-fix checklist and validation status.

## Scripts

- `scripts/wms-rca-run.ps1`
  Generates a per-incident report with path, findings, and verification slots.
- `scripts/wms-rca-drift.ps1`
  Compares code snippets/patterns across branches and stores drift evidence.
- `scripts/wms-rca-null-scan.ps1`
  Scans Java/VB for null/empty/list guard hotspots.
- `scripts/wms-rca-postfix-verify.ps1`
  Generates a test checklist tied to the process and incident.
- `scripts/db_diag_picking_mampa.py`
  DB-side incident snapshot for MAMPA picking (`enc`, `ubic`, and verification counts).
- `scripts/install-skill.ps1`
  Installs this skill into local Codex runtime.
- `scripts/validate-skill.ps1`
  Runs structural validation.

## Required Pairing

- Use together with `wms-operational-agent` for process routing.
- Use `wms-db-brain` for SQL/table/flag correlation.

## Tags

Always tag durable notes with `#EJCYYYYMMDD`.

---

## Patrón 4: Shared-Reference Array Corruption (BUG-003B, 2026-05-28)

### Descripción

Clase de bug donde un objeto se agrega a un array/lista global **sin clonar**, y luego un método posterior itera ese array modificando campos de todos los items cuando solo debería modificar el item actual.

Síntoma típico: campo como `No_Linea`, `IdRecepcionDet`, o `No_linea` aparece con el valor del **último item procesado** en todos los items anteriores del array, corrompiendo la concordancia det↔stock.

### Grep patterns para detectar en Java (HH)

```bash
# 1. Métodos que iteran arrays Y modifican campos de sus items
grep -n "for.*items\|\.items\.get(" archivo.java | head -40
grep -n "\.No_Linea\s*=\|\.IdRecepcionDet\s*=\|\.No_linea\s*=" archivo.java

# 2. Items agregados a array SIN clone (riesgo de referencia compartida)
grep -n "\.items\.add(" archivo.java
# Para cada add: verificar si el objeto fue clonado antes (buscar .clone())

# 3. Identificar si el método que itera también modifica via referencia
grep -n "completarDetalle\|LlenaDatosFaltantes\|sincronizarDetalle" archivo.java
```

### Grep patterns para detectar en VB.NET (BOF)

```bash
# Asignaciones dentro de For Each que tocan campos de todos los items
grep -n "For Each.*In pList\|For Each.*In lista" archivo.vb
grep -n "\.No_Linea\s*=\|\.IdRecepcionDet\s*=" archivo.vb

# Listas pasadas por referencia a Sub que las modifica internamente
grep -n "ByRef.*List\|ByVal.*List" archivo.vb
```

### Fix canónico (Java)

```java
// ANTES (bug): modifica TODOS los items del array con el valor actual
for (clsBeTrans_re_det det : lista.items) {
    if (det.IdProductoBodega == 0 && pLineaOC > 0) {
        det.No_Linea = pLineaOC;  // pisa items de iteraciones previas
    }
}

// DESPUÉS (fix): guard de identidad — solo modifica el objeto actual
for (clsBeTrans_re_det det : lista.items) {
    if (det == BeTransReDet && det.No_Linea == 0 && pLineaOC > 0) {
        det.No_Linea = pLineaOC;  // solo el objeto que estamos construyendo ahora
    }
}
```

### Fix canónico adicional: clonar antes de agregar al array

```java
// Al agregar al array, romper la referencia compartida con clone()
Auxredet = (clsBeTrans_re_det) BeTransReDet.clone();
AuxListTransReDet.items.add(Auxredet);
// NO: AuxListTransReDet.items.add(BeTransReDet);  ← referencia viva, peligroso
```

### Métodos HH de recepción con riesgo conocido (frm_recepcion_datos.java)

| Método | Riesgo | Estado |
|---|---|---|
| `completarDetalleRecepcion` | Itera `Detalle.items` + `pListTransRecDet.items`, asigna `No_Linea` | **FIXED** (guard `det == BeTransReDet`) |
| `Guardar_Caja_Master_enMemoria` | Agrega `BeTransReDet` a `AuxListTransReDet` | **FIXED** (clone antes del add) |
| `sincronizarDetalleOrdenCompraRecepcion` | Itera OC det — revisar si modifica campos compartidos | Pendiente de auditar |
| `LlenaDatosFaltantes` | Itera lista de dets — revisar si modifica via referencia | Pendiente de auditar |
| `llenarDetalleRecepcion` | Itera lista de dets — revisar si modifica via referencia | Pendiente de auditar |

### Checklist de revisión para cualquier método que itera arrays de BEs

- [ ] ¿El método recibe el array y modifica campos de sus items?
- [ ] ¿La condición del `if` puede ser `true` para items que ya estaban en el array antes de esta llamada?
- [ ] ¿Se agrega `BeTransReDet` (o cualquier objeto "en construcción") al array ANTES de llamar al método que itera?
- [ ] Si los tres anteriores son `true`: agregar guard de identidad (`det == BeTransReDet`) O clonar antes del add.
