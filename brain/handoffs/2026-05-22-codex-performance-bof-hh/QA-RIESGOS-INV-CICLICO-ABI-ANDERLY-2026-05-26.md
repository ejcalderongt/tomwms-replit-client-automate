---
tipo: other
---
# Informe QA Visual — Inventario Cíclico (Abi + Anderly)

> Fecha: **2026-05-26**  
> Base técnica: `QA-RIESGOS-INV-CICLICO-ABI-ANDERLY-2026-05-26.yml`

---

## Estado General

![Bloqueantes](https://img.shields.io/badge/Bloqueantes-2-critical?style=for-the-badge&color=9b1c1c)
![Riesgos Medios](https://img.shields.io/badge/Riesgos%20Medios-3-warning?style=for-the-badge&color=d97706)
![Fortalezas](https://img.shields.io/badge/Fortalezas-2-success?style=for-the-badge&color=15803d)

### Lectura rápida
- Se detectaron **5 riesgos principales**.
- **2 riesgos** pueden generar caída/flujo roto (prioridad inmediata).
- El trabajo nuevo también trae mejoras importantes (filtro por inventario + SKU/TallaColor).

---

## Mapa de Riesgos (resumen)

| ID | Severidad | Tipo | Qué puede pasar | Archivo clave |
|---|---|---|---|---|
| `INV-CIC-R001` | 🔴 Alta | Crash | Índice fuera de rango en Caja Master | `frm_conteo_caja_master.java` |
| `INV-CIC-R002` | 🔴 Alta | Crash | `get(0)` sobre lista vacía en escaneo por barra | `frm_inv_cic_conteo.java` |
| `INV-CIC-R003` | 🔴 Alta | Negocio | Escaneo por presentación puede excluir líneas válidas | `clsLnProducto_Partial.vb` |
| `INV-CIC-R004` | 🟠 Media | Negocio | Caja Master puede pisar conteos manuales | `clsLnTrans_inv_ciclico_Partial.vb` |
| `INV-CIC-R005` | 🟠 Media | Flujo | Checkbox Caja Master depende de talla/color | `frm_inv_cic_conteo.java` |

---

## Detalle por Prioridad

### 🔴 Fase 1 — Bloqueantes (hacer primero)

#### `INV-CIC-R001` — Caja Master puede reventar por lista vacía
- Evidencia: `ListaCajaMasterConteo.get(0)` sin validar vacío.
- Fix propuesto:
  - validar `null/empty` antes del `get(0)`.
  - si vacío: toast amigable + `finish()` controlado.

#### `INV-CIC-R002` — Escaneo por barra puede romper con `get(0)`
- Evidencia: se valida `items != null`, pero no `!items.isEmpty()`.
- Fix propuesto:
  - condición robusta con `!items.isEmpty()`.
  - fallback seguro con mensaje y sin perder estado actual.

---

### 🟠 Fase 2 — Negocio (consistencia funcional)

#### `INV-CIC-R003` — Filtro por UMBas puede excluir presentaciones
- Evidencia: `EXISTS` filtra por `IdUnidadMedida = IdUnidadMedidaBasica`.
- Impacto: contradice casos de QA donde se escanea por presentación/caja.
- Fix propuesto:
  - ajustar criterio de unidad para no limitarse a UMBas.

#### `INV-CIC-R004` — Caja Master sobreescribe cantidad
- Evidencia: `Cantidad = Cant_stock` + `Contado=True`.
- Riesgo: pisa conteos manuales en escenarios mixtos.
- Fix propuesto:
  - respetar conteo manual si ya existe.
  - opcional: bandera de origen de conteo (`CM` / `MANUAL`).

---

### 🟡 Fase 3 — Ajuste UX/operativo

#### `INV-CIC-R005` — Caja Master atado a talla/color
- Evidencia: visibilidad `chkCajaMaster` depende de `Control_Talla_Color`.
- Fix propuesto:
  - desacoplar visibilidad de talla/color.
  - usar parámetro explícito de caja master.

---

## Cobertura del Checklist QA (lectura rápida)

| Área del checklist | Estado inferido |
|---|---|
| Tabs cíclico vs inicial | ✅ Sin alerta en esta pasada |
| Escaneo SKU/presentación/talla-color | ⚠️ Riesgo alto (`R002`, `R003`) |
| Caja Master / reconteo | ⚠️ Riesgo alto/medio (`R001`, `R004`, `R005`) |
| Reservas/picking durante conteo | 🧪 Requiere prueba integrada |
| Regresión técnica | ⚠️ Riesgo por índices/listas vacías |

---

## Recomendación de ejecución

1. **Aplicar Fase 1** (2 fixes de estabilidad).  
2. Ejecutar mini smoke QA de escaneo + caja master.  
3. Aplicar Fase 2 de reglas de negocio.  
4. Cerrar con checklist completo y evidencia por caso.

---

## Referencias

- YAML técnico base:  
  `wms-brain/brain/handoffs/2026-05-22-codex-performance-bof-hh/QA-RIESGOS-INV-CICLICO-ABI-ANDERLY-2026-05-26.yml`

