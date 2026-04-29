# brain/debuged-cases — Índice

> Bitácora viva por case-pointer. Estado actual de cada caso en debug.

## Tabla de bitácoras

| ID | Bitácora | Status | Severidad | Última actualización | Resumen |
|---|---|---|---|---|---|
| CP-001 | [`CP-001.md`](./CP-001.md) | open | alta | 2026-04-29 | frmStockEnUnaFecha hardcode triple TheGoalDate / 030772033524 / SIN REGISTRO + DESP |
| CP-002 | [`CP-002.md`](./CP-002.md) | open | media | 2026-04-29 | frmMovimiento_Reporte comment "(Por error... fecha_vence = now -> JP.)" |
| CP-003 | [`CP-003.md`](./CP-003.md) | open | media | 2026-04-29 | frmMovimiento_Reporte comment "Magia por EJC para corregir cagada" + fix comentado |
| CP-004 | [`CP-004.md`](./CP-004.md) | open | media | 2026-04-29 | frmMovimiento_Reporte declaración `Dim TheGoalDate As Date = New Date(2019, 8, 30)` |
| CP-005 | [`CP-005.md`](./CP-005.md) | open | media | 2026-04-29 | frmMovimiento_Reporte breakpoint amplio `Fecha_Vence = TheGoalDate` |
| CP-006 | [`CP-006.md`](./CP-006.md) | open | alta | 2026-04-29 | frmMovimiento_Reporte breakpoint preciso triple (espejo idéntico de CP-001) |
| CP-007 | [`CP-007.md`](./CP-007.md) | open | alta | 2026-04-29 | frmStockEnUnaFecha marker `Serie = "#EJCAJUSTEDESFASE"` (auto-confirmable por query 06) |

## Agrupaciones

### Trinity TheGoalDate (limpieza atómica)

- CP-004 declara
- CP-005 consume amplio
- CP-006 consume preciso (= espejo idéntico de CP-001)

### Espejos entre los dos reportes

- CP-001 ↔ CP-006 — mismo caso histórico, dos reportes (estándar y fiscal)

### Pareja fix-bug (mismo bloque)

- CP-002 (bug introducido por JP)
- CP-003 (intento de fix por EJC, comentado)

### Único con efecto persistente en BD

- CP-007 — `Serie = "#EJCAJUSTEDESFASE"` se escribe a `trans_movimientos`, auto-confirmable por query

## Auto-confirmables (no requieren entrevista a Erik)

- **CP-007**: query 06 (`tools/case-seed/queries/data-discrepancy/06_movimientos_ejcajustedesfase.sql`) cuenta movimientos con marker
- **CP-006** (parcial): query 07 / 10 cuentan DESP desde SIN REGISTRO

## Pendientes globales

- [ ] Confirmar autoría JP (todos los CP referencian "JP" en CP-002)
- [ ] Lista completa de clientes con control de póliza (impacta CP-006 sobre todo)
- [ ] Confirmar autoría EJC (CP-003, CP-007 mencionan EJC)
- [ ] Implementar query 06 (CP-007) y 10 (CP-006) en `tools/case-seed/queries/`
- [ ] Buscar otras "magias EJC" en el repo (acción forense de CP-003)
- [ ] Buscar otras `Debug.Print("Wait a second!")` (acción forense de CP-005)
- [ ] Buscar otras `Serie = "#EJC..."` (acción forense de CP-007)

## Convención de archivos

Ver `CONVENCION.yml`.
