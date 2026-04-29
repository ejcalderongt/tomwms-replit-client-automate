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
| CP-013 | [`CP-013.md`](./CP-013.md) | **confirmed** | **alta** | 2026-04-29 | **Killios WMS164**: stock partido en 2 filas con misma llave natural (CEST sin merge). 919 filas afectadas (18.7%). Bug raíz `V-DATAWAY-004`. Refuta hipótesis ModoDepuracion. Wave 13-10: análisis estructural offline + nueva H4 + path origen confirmado HH Android (no BOF) |
| CP-014 | (sin bitácora aún) | **confirmed** | **alta** | 2026-04-29 | **Killios estructural**: `dbo.stock` sin UNIQUE INDEX sobre llave natural (14 NCLI, 0 UNIQUE). Bug raíz `V-DATAWAY-005`. Causal-permisivo de CP-013: convierte el bug aplicativo en daño persistente silencioso |

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

- CP-013 — Killios WMS164: stock partido por CEST sin merge. Status=`confirmed` desde apertura, severidad alta, 919 filas activas afectadas (18.7%), bug raíz `V-DATAWAY-004`. Subcarpeta con queries reproducibles + outputs raw + REPORTE.md en `CP-013-killios-wms164/`. Wave 13-10 agrega `REPORTE-wave-13-10.md` (análisis estructural offline) + `pedido-extraccion-hh-cest.md` (contrato Wave 13-11).

### Casos estructurales / anti-patrones de DDL/schema (categoría nueva en wave 13-10)

- CP-014 — Killios `dbo.stock` sin UNIQUE INDEX sobre llave natural. Status=`confirmed` desde apertura, severidad alta, descubierto **sin BD viva** mediante inspección offline del catálogo `wms-db-brain` (snapshot 2026-04-27). Bug raíz `V-DATAWAY-005`. Causal-permisivo de CP-013/V-DATAWAY-004 — convierte el bug aplicativo de path CEST en daño persistente silencioso. Sin bitácora propia (toda la documentación vive en el case-pointer y el anti-patrón).

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
- [ ] **CP-013**: restablecer firewall Killios (TCP timeout puro a `52.41.114.122:1437` desde IP saliente sandbox `35.227.125.212`); pendiente AWS Security Group + Windows Firewall + NACL + tcpdump
- [ ] **CP-013**: re-correr q01..q12 cuando se restablezca firewall y archivar los 12 outputs raw para auditoría línea por línea
- [ ] **CP-013**: confirmar autorización para correr query 12 sobre `IMS4MB_BYB_PRD` y `IMS4MB_CEALSA_QAS` (medir cross-cliente) cuando se restablezca firewall
- [x] ~~**CP-013 / Wave 13-10**: localizar en `TOMWMS_BOF` el flujo del CEST~~ — **redirigido**: Wave 13-10 confirmó que el path CEST se origina en HH Android (FK `_sis_tipo_tarea_hh`), no en BOF. Análisis estructural hecho via `wms-db-brain` (4 hallazgos + V-DATAWAY-005 + CP-014)
- [ ] **CP-013 / Wave 13-11**: Erik extrae bundle del HH Android (TOMHH2025) según contrato `CP-013-killios-wms164/pedido-extraccion-hh-cest.md` y empuja al repo de intercambio
- [ ] **CP-013 / Wave 13-12**: leer el bundle, confirmar H1 (lic_plate NULL rompe comparador) y/o H4 (UPDATE rechazado por check `Cantidad>0` → fallback INSERT) con cita archivo + línea
- [ ] **CP-013 / Wave 13-13**: si V-DATAWAY-004 confirmado, abrir `V-DATAWAY-004.md` formal y proponer DDL completo de R1 de V-DATAWAY-005 (consolidación batch + normalización lic_plate + UNIQUE INDEX filtrado)
- [ ] **CP-014 / Wave 13-14**: revisar si `stock_res`, `stock_se`, `stock_transito`, `stock_jornada` también carecen de UNIQUE INDEX (R3 de V-DATAWAY-005). Si confirma, formalizar pattern P-003 "invariante de dominio confiado al código sin defensa de BD"
- [ ] Promover candidato pattern P-002 ("INSERT sin merge contra llave natural") cuando aparezca segunda instancia
- [ ] Promover candidato pattern P-003 ("invariante de dominio confiado al código sin defensa de BD") cuando CP-014 + Wave 13-14 confirme segunda instancia en otra tabla del dominio stock

## Convención de archivos

Ver `CONVENCION.yml`.
