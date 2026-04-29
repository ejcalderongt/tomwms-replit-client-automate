# `legacy-process-flow/` — Caja blanca de `clsLnStock_res_Partial.vb`

> **Propósito**: convertir el archivo monolito `clsLnStock_res_Partial.vb` (34.382 líneas, 154 funciones, 99% del archivo en 8 funciones `Reserva_Stock*`) en un mapa punto-a-punto navegable, con cada función inventariada en bloques lógicos, reglas extraídas, errores tirados, recursión, dead code y open questions. La meta final es comparar contra el motor nuevo .NET 8 (`StockReservationFacade` + handlers) y detectar gaps funcionales.
>
> **Wave de origen**: Wave 12 (iniciada 2026-04-29).
> **Estado**: en construcción. Ver tabla de avance abajo.

---

## 1. Las 8 funciones `Reserva_Stock*` del archivo

| # | Archivo entregable | Función | Líneas (start→end) | Tamaño | Adapter de canal |
|---|---|---|---|---|---|
| 1 | `01-reserva-stock-core.{yml,md}` | `Reserva_Stock` | 138 → 427 | 290 | (core, llamado por todos) |
| 2 | `02-reserva-stock-nav-byb.{yml,md}` | `Reserva_Stock_NAV_BYB` | 442 → 855 | 414 | NAV BYB (a confirmar) |
| 3 | `03-reserva-stock-lista-result.{yml,md}` | `Reserva_Stock_Lista_Result` | 858 → 1.458 | 601 | wrapper de respuesta |
| 4 | `04-reserva-stock-especifico-ov1.{yml,md}` | `Reserva_Stock_Especifico` (overload 1) | 1.459 → 1.611 | 153 | reserva forzada por LP/ubic |
| 5 | `05-reserva-stock-especifico-ov2.{yml,md}` | `Reserva_Stock_Especifico` (overload 2) | 1.612 → 9.853 | 8.242 | reserva forzada (monstruo) |
| 6 | `06-reserva-stock-from-reabasto.{yml,md}` | `Reserva_Stock_From_Reabasto` | 9.856 → 18.190 | 8.335 | abastecimiento interno |
| 7 | `07-reserva-stock-from-mi3.{yml,md}` | `Reserva_Stock_From_MI3` | 18.192 → 26.678 | 8.487 | MI3 / Killios / NAV ERP |
| 8 | `08-reserva-stock-from-sap.{yml,md}` | `Reserva_Stock_From_SAP` | 26.680 → 34.381 | 7.702 | SAP ERP |

**Total inventariado al cierre de Wave 12**: 34.224 líneas (99% del archivo).

---

## 2. Convención de archivos por función

Cada función produce **2 archivos**:

- `NN-nombre-funcion.yml` — máquina-leíble, estructurado, alimenta el motor predictivo de Wave 15.
- `NN-nombre-funcion.md` — humano-leíble, narrativo, con anchor lines y observaciones.

Ambos archivos son hermanos: el YAML es la fuente única de verdad estructural; el MD es la narrativa explicada.

Diagramas mermaid del control-flow van en `diagrams/NN-nombre-funcion.mmd`.

---

## 3. Esquema YAML (contrato)

```yaml
function:
  name: <nombre>
  alias: <opcional>
  file: <ruta relativa al repo TOMWMS_BOF>
  line_start: <int>
  line_end: <int>
  size_lines: <int>
  visibility: <Public|Private|Friend> [Shared]
  return_type: <tipo>
  scanned_at: <YYYY-MM-DD>
  source_commit: <sha del repo en el momento del scan>

signature:
  parameters:
    - name: <nombre>
      type: <tipo>
      mode: <ByVal|ByRef>
      optional: <bool>
      default: <valor o null>
      purpose: <descripción semántica>

local_variables:
  - name: <nombre>
    type: <tipo>
    initial_value: <valor>
    purpose: <descripción>

dead_variables:    # declaradas pero nunca usadas
  - <nombre>: <razón inferida>

global_state_used:
  - name: <nombre>
    type: <cache|module-level>
    purpose: <descripción>

callees:           # funciones externas/internas que invoca
  - target: <Clase.Función o Función_local>
    type: <db_read|db_write|math|recursion|cache|log>
    location: <bloque o línea>
    note: <opcional>

blocks:            # bloques lógicos numerados B001..BNNN
  - id: B001
    lines: "<start>-<end>"
    type: <init|try_catch|branch|loop|db_read|db_write|call|return|logging|goto|recursion>
    summary: <descripción corta>
    condition: <para branches: condición evaluada>
    sub_branches:  # opcional, para branches anidados
      - condition: <...>
        on_true: <...>
        on_false: <...>
        dead: <bool>  # si es dead branch
    next: <id del siguiente bloque o lista>
    notes: <opcional>

extracted_rules:   # reglas de negocio inferidas
  - id: R001
    rule: <descripción declarativa>
    depends_on: [bodega, propietario, producto, presentacion, canal, ...]
    legacy_anchor: "L<start>-L<end>"
    net8_equivalent: <Step/Handler del nuevo o "?">
    parity_status: <confirmed|partial|missing|eliminated|unknown>

logging_checkpoints:
  - prefix: <ej. ERROR_202210182024 o #CASO_X_Y>
    location: L<n>
    meaning: <descripción>

errors_thrown:
  - code: <ej. clsDalEx.ErrorS0002 o "ad-hoc">
    location: L<n>
    condition: <cuándo se tira>
    semantics: <qué significa funcionalmente>
    accepts_partial: <bool>  # si la operación admite reserva parcial

recursion:
  exists: <bool>
  trigger: <condición>
  marker: <ej. "No_bulto = 1965">
  uses_marker: <bool>
  return_value_captured: <bool>
  parameters_passed: <lista, identificando cuáles pierden valor>

gotos:
  - label: <nombre>
    location: L<n>
    target: L<n>
    purpose: <descripción>

dead_code:
  - description: <qué es y dónde>
    location: L<start>-L<end>

open_questions:    # Q-* específicas a esta función
  - id: Q-<nombre-corto>
    text: <pregunta>
    blocks_block: <id del bloque>
    severity: <baja|media|alta|crítica>

net8_equivalent:   # mapeo al motor nuevo .NET 8 (placeholder hasta Wave 13)
  facade_method: <nombre>
  pipeline_steps: [...]
  parity_status: <unknown_pending_verification|confirmed|partial|missing>
```

---

## 4. Esquema markdown hermano

Estructura del MD:

1. **Header** con función + tamaño + anchor.
2. **Resumen ejecutivo** (3-5 líneas, qué hace la función en lenguaje humano).
3. **Tabla de bloques** (B001..BNNN con tipo + resumen + line range).
4. **Recorrida narrativa** bloque por bloque, con snippets del código relevante (nunca más de 10 líneas por bloque).
5. **Reglas extraídas** (lista R001..RNNN con anchor).
6. **Hallazgos críticos** (dead code, bugs latentes, typos, redundancias).
7. **Open questions** (Q-* a discutir con Erik/Carolina/Efrén).
8. **Comparación contra .NET 8** (sección a completar en Wave 13).

---

## 5. Convenciones de inventario

- **Granularidad**: por bloque lógico, no línea por línea. Un bloque agrupa líneas que ejecutan una sola operación conceptual (una validación, una rama, una lectura, un loop completo).
- **Anchors**: siempre por número de línea del archivo legacy (`L<start>` o `L<start>-L<end>`).
- **Reglas**: extraídas en lenguaje declarativo, parametrizadas por entidades (bodega, propietario, producto, presentación, canal). Las reglas son lo que alimentará el motor predictivo de Wave 15.
- **Dead code**: se reporta sin opinar — solo se documenta. La decisión de eliminar/preservar la toma Erik en una ADR posterior.
- **Recursión**: se documenta el trigger, marker (si lo hay) y si captura return value. La recursión `No_bulto = 1965` es exclusiva del adapter MI3.

---

## 6. Cross-refs

- `entities/modules/reservation/02-mi3-motor-legacy-vb.md` (mapeo histórico previo de MI3, será reemplazado/superado por `07-reserva-stock-from-mi3.{yml,md}`)
- `entities/modules/reservation/03-comparison.md` (comparación legacy vs nuevo a nivel narrativo, será extendida en Wave 13 con la matriz de paridad punto-a-punto)
- `decisions/003-mi3-reescrito.md` (decisión arquitectónica de reescribir MI3)
- `_index/INDEX.md` línea 907 y 942 (TODOs originales de inspección)

---

## 7. Avance Wave 12

| Función | Estado | Fecha |
|---|---|---|
| 01 Reserva_Stock (core) | piloto formato | 2026-04-29 |
| 02 Reserva_Stock_NAV_BYB | pendiente | — |
| 03 Reserva_Stock_Lista_Result | pendiente | — |
| 04 Reserva_Stock_Especifico ov1 | pendiente | — |
| 05 Reserva_Stock_Especifico ov2 | pendiente | — |
| 06 Reserva_Stock_From_Reabasto | pendiente | — |
| 07 Reserva_Stock_From_MI3 | pendiente | — |
| 08 Reserva_Stock_From_SAP | pendiente | — |
