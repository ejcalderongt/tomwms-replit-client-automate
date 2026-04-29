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
| CP-008 | [`CP-008.md`](./CP-008.md) | open | alta | 2026-04-29 | Marker `#EJCAJUSTEDESFASE` copy-pasted en 3 reportes — expande V-DATAWAY-001 a familia |
| CP-009 | [`CP-009.md`](./CP-009.md) | open | media | 2026-04-29 | frmRegularizarInventario triple hardcode `01007121 / 01007011 / IdStock=4427` (Congelado) |
| CP-010 | [`CP-010.md`](./CP-010.md) | open | baja | 2026-04-29 | clsLnStock_res_Partial breakpoint `00190454` + Debug.Print("Aqui") activo |
| CP-011 | [`CP-011.md`](./CP-011.md) | open | baja | 2026-04-29 | clsLnStock_res_Partial breakpoint `00091035` + Debug.Write("Espera") activo |
| CP-012 | [`CP-012.md`](./CP-012.md) | open | baja | 2026-04-29 | frmExistenciasConReserva breakpoint `01008076` + 2 guards no-op |
| CP-013 | [`CP-013.md`](./CP-013.md) | **confirmed** | **alta** | 2026-04-29 | **Killios WMS164**: stock partido en 2 filas con misma llave natural (CEST sin merge). 919 filas afectadas (18.7%). Bug raíz `V-DATAWAY-004`. Refuta hipótesis ModoDepuracion |

## Agrupaciones

### Trinity TheGoalDate (limpieza atómica)

- CP-004 declara
- CP-005 consume amplio
- CP-006 consume preciso (= espejo idéntico de CP-001)

### Espejos entre los reportes con `ModoDepuracion`

- CP-001 ↔ CP-006 — mismo caso histórico, dos reportes (estándar y fiscal)

### Pareja fix-bug (mismo bloque)

- CP-002 (bug introducido por JP)
- CP-003 (intento de fix por EJC, comentado)

### Familia con efecto persistente en BD

- CP-007 — caso singular `frmStockEnUnaFecha` (predecesor)
- CP-008 — caso expandido a 3 reportes (`frmStockEnUnaFecha` + `frmMovimiento_Reporte` + `frmAnaliticaA`); CP-008 expande pero no reemplaza a CP-007

### Instancias del pattern P-001 (breakpoint arqueológico con código hardcoded)

- CP-001 — `030772033524` en frmStockEnUnaFecha
- CP-009 — triple `01007121 / 01007011 / IdStock=4427` en frmRegularizarInventario
- CP-010 — `00190454` en clsLnStock_res_Partial L20947
- CP-011 — `00091035` en clsLnStock_res_Partial L27264
- CP-012 — `01008076` en frmExistenciasConReserva

### Casos de campo confirmados con datos reales (categoría nueva en wave 13-9)

- CP-013 — Killios WMS164: stock partido por CEST sin merge. Status=`confirmed` desde apertura, severidad alta, 919 filas activas afectadas (18.7%), bug raíz `V-DATAWAY-004`. Subcarpeta con queries reproducibles + outputs raw + REPORTE.md en `CP-013-killios-wms164/`.

## Auto-confirmables (no requieren entrevista a Erik)

- **CP-007 / CP-008**: query 06 (`tools/case-seed/queries/data-discrepancy/06_movimientos_ejcajustedesfase.sql`) cuenta movimientos con marker
- **CP-006** (parcial): query 07 / 10 cuentan DESP desde SIN REGISTRO
- **CP-013**: query 11 (refutación ModoDepuracion en Killios) + query 12 (alcance sistémico del anti-patrón insert-sin-merge). Conservadas en `CP-013-killios-wms164/queries/q11_final.py` y `q12_alcance.py`.

## Pendientes globales

- [ ] Confirmar autoría JP (CP-002 referencia "JP")
- [ ] Lista completa de clientes con control de póliza (impacta CP-006 + CP-008)
- [ ] Confirmar autoría EJC (CP-003, CP-007, CP-008, CP-010, CP-011, CP-012)
- [ ] Implementar query 06 (CP-007 + CP-008) y 10 (CP-006) en `tools/case-seed/queries/`
- [ ] Implementar queries 11-14 (CP-009..CP-012) cuando Erik apruebe correr SQL contra Killios PRD
- [ ] Investigar los 4 "activadores sin mutador localizable" del CP-008 (FrmStock_Fiscal, frmLogErrorWMS, frmDocPeConDiferencias, frmDocConDiferencias)
- [ ] Verificar si `frmAnaliticaA` aparece en menú de clientes reales (CP-008)
- [ ] Verificar si carpeta `reservastockfrommi3/` es código muerto (clon CP-009)
- [ ] Búsquedas heurísticas pendientes B7-B10 (ver `case-pointers/00-INDEX.md`)
- [ ] **CP-013**: confirmar autorización para correr query 12 sobre `IMS4MB_BYB_PRD` y `IMS4MB_CEALSA_QAS` (medir cross-cliente)
- [ ] **CP-013 / Wave 13-10**: localizar en `TOMWMS_BOF` el flujo del CEST y leer su path INSERT vs UPDATE
- [ ] **CP-013 / Wave 13-11**: confirmar H1 (lic_plate vacío rompe comparador) / H2 (concurrencia inter-segundo) / H3 (CEST por lote partido) con caso de prueba
- [ ] **CP-013 / Wave 13-12**: si bug raíz se confirma, abrir `V-DATAWAY-004.md` formal
- [ ] Promover candidato pattern P-002 ("INSERT sin merge contra llave natural") cuando aparezca segunda instancia

## Convención de archivos

Ver `CONVENCION.yml`.
