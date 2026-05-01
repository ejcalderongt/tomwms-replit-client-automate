---
id: README
tipo: test-scenario
estado: vigente
titulo: Catálogo de escenarios de prueba — TOM WMS
tags: [test-scenario]
---

# Catálogo de escenarios de prueba — TOM WMS

Este directorio contiene los **escenarios de prueba canónicos** que el Brain
usa para validar bundles del BOF y del HH contra cualquier cliente aprendido.

Skill que opera sobre este catálogo: `wms-brain/skills/wms-test-bridge/SKILL.md`.
ADR: `wms-brain/decisions/004-bridge-wms-test-automation.md`.

---

## 1. Estructura

```
test-scenarios/
├── README.md                                este archivo
├── reservation/
│   ├── RES-001-fefo-simple.yaml
│   ├── RES-002-paquete-completo-zona-picking.yaml
│   ├── RES-003-explosion-automatica.yaml
│   ├── RES-004-stock-insuficiente-parcial.yaml
│   ├── RES-005-cancelar-y-liberar.yaml
│   └── legacy-clsLnStock_res/               20 CASOs canónicos del legacy
│       ├── README.md                        índice + protocolo de migración
│       └── CASO-01-IDEAL_20231002011101.md
├── picking/
│   └── PIC-001-pedido-reservado-zona-picking.yaml
├── despacho/
│   └── DES-001-verificacion-obligatoria.yaml
└── ajustes/
    └── INV-001-ajuste-positivo-modifica-vencimiento.yaml
```

Naming: `<MOD>-NNN-slug-corto.yaml` donde `<MOD>` es el prefijo del módulo
(`RES`, `PIC`, `DES`, `INV`, `REC`, `UBI`, `SAP`, `SEG`, `CFG`, `REP`) y
`NNN` es número incremental dentro del módulo.

---

## 2. Estado actual del catálogo

### Reservation (módulo flagship — el más complejo)

| ID | Título | Prioridad | Legacy ref | Estado |
|---|---|---|---|---|
| RES-001 | Reserva FEFO simple, una bodega | P0 | `Ejecuta_QA_CASO_1_IDEAL_20231002011101` | drafted |
| RES-002 | Reserva con paquete completo en zona picking | P0 | (mapear a CASO N) | drafted |
| RES-003 | Reserva con explosión automática a nivel máximo | P0 | (mapear a CASO N) | drafted |
| RES-004 | Reserva con stock insuficiente (parcial) | P1 | (mapear a CASO N) | drafted |
| RES-005 | Cancelar reserva existente y verificar liberación | P1 | (mapear a CASO N) | drafted |
| RES-LEG-01..20 | 20 CASOs legacy `clsLnStock_res.Ejecuta_QA_CASO_*` | P0 | ver inventario | pendiente lectura |

### Picking

| ID | Título | Prioridad | Estado |
|---|---|---|---|
| PIC-001 | Picking de pedido reservado en zona picking | P0 | drafted |

### Despacho

| ID | Título | Prioridad | Estado |
|---|---|---|---|
| DES-001 | Despacho con verificación obligatoria | P0 | drafted |

### Ajustes

| ID | Título | Prioridad | Estado |
|---|---|---|---|
| INV-001 | Ajuste positivo con flag `momdifica_vencimiento` | P1 | drafted |

### Pendientes futuros (placeholders por módulo)

- **Recepción** (`REC-NNN`): TODO — escenarios de OC, recepción ciega,
  ASN, captura por handler, validación SAP.
- **Ubicaciones** (`UBI-NNN`): TODO — slotting, traslados internos,
  reabastecimiento.
- **Interface SAP** (`SAP-NNN`): TODO — sincronización maestros,
  traslado pedidos, ajustes hacia SAP.
- **Inventario cíclico** (`INV-1xx`): TODO — conteo, reclasificación,
  diferencias.
- **Seguridad** (`SEG-NNN`): TODO — roles, permisos, auditoría.
- **Configuración** (`CFG-NNN`): TODO — cambios de flags y su efecto.

---

## 3. Estados de un escenario

| Estado | Significado |
|---|---|
| `placeholder` | Solo nombre y descripción, sin YAML |
| `drafted` | YAML existe, puede que no resuelva todos los placeholders |
| `runnable` | `wmsa make-payload` resuelve OK al menos para 1 cliente |
| `validated` | Probado con `dry-run` en al menos 1 cliente |
| `replay-tested` | Probado con `replay` en QA en al menos 1 cliente |
| `production-ready` | Probado con `canary` en al menos 1 cliente |

El estado se declara dentro del propio YAML en el campo `status:`.

---

## 4. Cómo agregar un escenario nuevo

1. Definir el módulo (`reservation`, `picking`, etc.).
2. Tomar el siguiente número del módulo (`NNN`).
3. Crear `<modulo>/<MOD>-<NNN>-<slug>.yaml` siguiendo el esquema canónico
   (ver `skills/wms-test-bridge/SKILL.md` §3).
4. Si mapea a un `Ejecuta_QA_CASO_*` legacy, completar `legacy_ref:`.
5. Actualizar la tabla de §2 de este README.
6. Si es derivado de un caso real reportado por Erik, agregar tag
   `from-incident` y referencia en `_inbox/`.

---

## 5. Cómo declarar cobertura por bundle

Cada bundle al BOF en `entregables_ajuste/<fecha>/<v##_bundle>/MANIFEST.json`
declara qué escenarios cubre/toca:

```json
{
  "bundle": "v24_motor_mi3_handler_explosion",
  "coverage": ["RES-001", "RES-003", "RES-LEG-01", "RES-LEG-03"],
  "potentially_breaks": ["RES-002"],
  "validated_in": ["killios:dry-run", "killios:replay"]
}
```

El agente Brain bloquea el `wmsa bundle pack` si `coverage` está vacío
para un bundle que toca código del módulo declarado.

---

## 6. Convenciones de tags

| Tag | Significado |
|---|---|
| `golden-path` | Camino feliz, debe pasar siempre |
| `edge-case` | Borde, valida invariantes |
| `regression` | Generado a partir de un bug real |
| `from-incident` | Derivado de un evento en `_inbox/` |
| `slow` | Tarda >5s, no se corre en CI default |
| `requires-sap` | Necesita interface SAP up |
| `requires-hh` | Necesita simulador de HH |
| `multi-cliente` | Probado en N clientes distintos |
| `mi3` | Toca motor MI3 |
| `mi3-legacy` | Toca motor legacy de reservas |
| `fefo`, `paquete-completo`, `explosion-automatica`, etc. | Capacidades específicas |

---

## 7. Roadmap del catálogo

- **S1 (esta entrega)**: 8 escenarios drafted + inventario legacy.
- **S2**: completar 20 CASOs legacy a partir del código `clsLnStock_res.vb`.
- **S3**: agregar 5-10 escenarios de recepción (REC-NNN) cuando Erik
  comparta criterios.
- **S4**: agregar escenarios de picking adicionales (PIC-002..005).
- **S5**: agregar escenarios SAP (SAP-NNN) — interface es crítica.
- **S6+**: cobertura completa por módulo, multi-cliente.

---

## 8. Referencias

- Skill operativa: `wms-brain/skills/wms-test-bridge/SKILL.md`.
- ADR: `wms-brain/decisions/004-bridge-wms-test-automation.md`.
- Módulo flagship: `wms-brain/entities/modules/reservation/README.md`.
- DDL del catálogo SQL: `wms-brain/sql-catalog/reservation-tables.md`.
- Filosofía multi-cliente: `wms-db-brain/db-brain/parametrizacion/README.md`.
