# 12 · TODOs, riesgos abiertos y roadmap del motor MI3

> **Propósito**: consolidar todos los TODOs, riesgos abiertos y trabajo pendiente identificados en los archivos 01-11 del módulo `reservation`. Sirve como checklist accionable para terminar el motor MI3 nuevo y desplegarlo en producción con confianza.
>
> **Cross-refs**: archivos 01 a 11 del mismo módulo, `decisions/003-mi3-reescrito.md`, `sql-catalog/reservation-tables.md`.

---

## Índice

1. TODOs por archivo (origen)
2. Riesgos abiertos consolidados
3. Decisiones pendientes (requieren input de Erik)
4. Roadmap de implementación (8 sprints)
5. Métricas de progreso
6. Definition of Done del motor MI3 nuevo

---

## 1. TODOs por archivo

### Archivo 01 · motor nuevo .NET 8

- [ ] **Confirmar que `BeStock_res` del nuevo es 100% binario-compatible con el del legacy** (mismo orden de campos, mismos tipos). Necesario para fachada de compatibilidad.
- [ ] **Implementar `ReservationContextSerializer`** que convierta `ReservationContext` ↔ `String` para logging eficiente sin reflection.
- [ ] **Documentar la inyección de dependencias** completa: qué interfaces se inyectan, dónde, cómo se configura el contenedor DI.
- [ ] **Decidir el contenedor DI**: ¿`Microsoft.Extensions.DependencyInjection` o `Autofac`? El BOF VB.NET no tiene DI moderno; cualquier elección impacta el bridge.

### Archivo 02 · motor legacy VB.NET

- [ ] **Documentar la firma exacta de `clsLnStock.lStock`** para construir el wrapper `IClsLnStock`.
- [ ] **Inventariar todas las funciones globales** que `Inserta_Stock_Res_MI3` invoca (no es autocontenido).
- [ ] **Mapear cada `MessageBox.Show`** del legacy a un equivalente exception-based en el nuevo (los UIs no deben verlo).

### Archivo 03 · comparison

- [ ] **§9 Riesgos pendientes** sin resolver:
  - Tie-break FEFO cuando `fecha_vence` es igual.
  - Comportamiento del typo `explosio_*` vs `explosion_*`.
  - `MessageBox.Show + Exit Function` del legacy (debe ser excepción en el nuevo).
- [ ] Confirmar que el nuevo emite los mismos checkpoints en el mismo orden.

### Archivo 04 · config propietario

- [ ] **Auditar todas las filas de `i_nav_config_enc` con typo inconsistente** (query §8). Generar reporte para Erik.
- [ ] **Decidir cuál columna del typo es la fuente de verdad** y deprecar la otra (con DBA).
- [ ] **Construir vista `vw_mi3_config_efectiva`** que devuelva la config canónica por bodega+propietario.
- [ ] **Validar `combinaciones tóxicas` (§7)** en producción y reportar a Erik para limpieza.

### Archivo 05 · algoritmo FEFO+Clavaud

- [ ] **Confirmar fórmula exacta de `pallet_completo`** (vista o cálculo runtime).
- [ ] **Definir si `IsExpired` usa `>=` o `>`** para `dias_vida_defecto_perecederos` (afecta inclusión de stock que vence hoy).
- [ ] **Documentar el empate FEFO** cuando hay 2 lotes con misma `fecha_vence` y misma cantidad.

### Archivo 06 · handlers

- [ ] **Implementar `EvaluateClavaudDynamic`** que re-evalúa por iteración (no solo al inicio del flujo).
- [ ] **Agregar `BulkPersistReservations`** para evitar N INSERTs cuando hay > 50 reservas.
- [ ] **Cubrir el caso `IdStockEspecifico` en handler dedicado** (`SpecificStockHandler`).
- [ ] **Validar que `BaseReservationHandler.IsExpired` se invoca en TODOS los handlers**, no solo en algunos.

### Archivo 07 · ciclo de vida stock_res (corregido)

- [ ] **Definir política de transición a `EXPIRED`** (sugerido 7 días, validar con Erik).
- [ ] **Crear tabla `stock_res_historico`** para archivado > 90 días.
- [ ] **Crear job nocturno de limpieza** (`UPDATE UNCOMMITED → EXPIRED`).
- [ ] **Crear job semanal de detección de inconsistencias** (queries §6.1).
- [ ] **Decidir si agregar columnas de auditoría histórica** (`Fecha_Commit`, `Usuario_Cancelacion`, etc.) o construir tabla `stock_res_history` separada.

### Archivo 08 · schema tablas Killios

- [ ] **Documentar las FKs concretas con NAME del constraint** (consulta `INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS`). → moverse a `sql-catalog/reservation-tables.md`.
- [ ] **Documentar las 9 cols faltantes de `i_nav_ped_traslado_det`** (vimos solo 13 de 22).
- [ ] **Crear catálogo separado de tablas de configuración** (`bodega`, `producto_bodega`, `producto_presentacion`, `ubicacion`).
- [ ] **Inventariar índices** de cada tabla crítica y proponer faltantes.

### Archivo 09 · logging y observabilidad

- [ ] **Implementar `WmsErrorTableLogger`, `ConsoleReservationLogger`, `CompositeReservationLogger`** (5 implementaciones listadas).
- [ ] **Definir convención de `ReservationContext.Resumen()`** (target: 500 chars máximo).
- [ ] **Crear vista `vw_mi3_metrics_hourly`** (en réplica o BOF, no en Killios prod).
- [ ] **Configurar alarmas operativas** (pedidos colgados > 30 min, tasa fallos > 5%).
- [ ] **Definir política de retención de `log_error_wms`** (sugerido: > 90 días → archivo).

### Archivo 10 · errores y troubleshooting

- [ ] **Inventariar todos los códigos de error** que el motor nuevo puede emitir y publicar lista canónica.
- [ ] **Validar runbooks 5.1 a 5.5** con casos reales recientes.
- [ ] **Crear playbook de cancelación masiva** para limpiar reservas huérfanas tras incidentes.
- [ ] **Implementar reintentos automáticos para deadlocks** (3 intentos, backoff 100/200/400ms).

### Archivo 11 · tests

- [ ] **Escribir los 80 unit tests** listados en §2.3.
- [ ] **Escribir los 15 integration tests** IT-01 a IT-15.
- [ ] **Construir el `CanaryReservationDispatcher`** y la tabla `mi3_canary_diff`.
- [ ] **Recolectar dataset canary anonimizado** de pedidos reales.
- [ ] **Ejecutar canary > 7 días** antes de habilitar el motor en producción.
- [ ] **Establecer baseline de performance** (BenchmarkDotNet) y detectar regresiones.

## 2. Riesgos abiertos consolidados

### 2.1 Riesgos técnicos

| ID  | Riesgo                                                                                  | Severidad | Archivo origen |
|-----|-----------------------------------------------------------------------------------------|-----------|---------------|
| R-01| Typo `explosio_*` vs `explosion_*` en `i_nav_config_enc` puede causar comportamiento divergente | Alta      | 04 §6         |
| R-02| `MaquinaQueSolicita` puede no copiarse exacto (cast distinto entre legacy/nuevo)        | Alta      | 03 §9         |
| R-03| Tie-break FEFO no determinista cuando `fecha_vence` es igual                            | Media     | 05 §6         |
| R-04| `MessageBox.Show + Exit Function` del legacy (L1922) sin equivalente en motor nuevo si corre headless | Alta | 02 §recursión |
| R-05| Marker `no_bulto = 1965` solo está en código (no en BD), pérdida de conocimiento si se borra comentario | Media | 08 §8.5  |
| R-06| `log_error_wms.MensajeError` truncado a 2500 chars puede romper parsers downstream      | Media     | 09 §5         |
| R-07| `stock_res.estado` sin FK ni CHECK permite typos catastróficos                          | Alta      | 07 §1         |
| R-08| Falta de tabla histórica → `stock_res` crece sin límite                                 | Media     | 07 §7.4       |
| R-09| Posibles deadlocks bajo concurrencia sin lock ordering                                  | Alta      | 10 §3.3       |
| R-10| Stock candidato sin `ROWLOCK, HOLDLOCK` puede generar `CANTIDAD_NEGATIVA_RESULTANTE`    | Alta      | 10 §2.5       |

### 2.2 Riesgos operativos

| ID  | Riesgo                                                                              | Severidad | Mitigación sugerida          |
|-----|-------------------------------------------------------------------------------------|-----------|-----------------------------|
| R-11| Erik es el único que conoce el dominio completo (bus factor = 1)                    | Crítica   | Documentación wms-brain (en curso) |
| R-12| Killios productivo es READ-ONLY → no se pueden hacer pruebas E2E reales              | Alta      | Réplica staging con datos sintéticos |
| R-13| El motor legacy (8K líneas) tiene lógica no documentada (descubrimiento via grep)   | Alta      | Continuar bisección + tests canary |
| R-14| No existe ambiente de testing separado del BOF productivo                          | Media     | Setup Docker SQL Server local |
| R-15| Cambios al motor requieren re-deploy del BOF (no hot reload)                       | Baja      | Aceptar; programar ventanas |

### 2.3 Riesgos de paridad

| ID  | Riesgo                                                                              | Severidad | Detección                    |
|-----|-------------------------------------------------------------------------------------|-----------|-----------------------------|
| R-16| Distinto orden FEFO en empates → distinto `lic_plate` reservado                     | Media     | Comparator canary §4.4       |
| R-17| Distinto threshold de tolerancia decimal (`0.000001` vs `0.0001`)                  | Alta      | Test unit + comparator       |
| R-18| Diferencia en activación de explosión (typo de columna)                            | Alta      | Audit i_nav_config_enc       |
| R-19| Diferente cantidad reservada cuando hay condiciones de carrera                      | Media     | Tests concurrencia §6.2 archivo 11 |

## 3. Decisiones pendientes (requieren input de Erik)

| #   | Decisión                                                                                  | Bloqueante para |
|-----|------------------------------------------------------------------------------------------|----------------|
| D-01| ¿Política de transición UNCOMMITED → EXPIRED: 7 días, 14 días, configurable por bodega?  | Job de limpieza (07 §3.5) |
| D-02| ¿Resolver typo `explosio_*` vs `explosion_*`: deprecar la vieja? Migrar valores?         | Auditoría 04 §6.3 |
| D-03| ¿Agregar tabla `stock_res_history` o usar columnas `Fecha_*` adicionales en `stock_res`?  | Trazabilidad histórica |
| D-04| ¿Tolerancia de paridad canary: 99.5% es aceptable o exigir 99.9%?                        | Criterio paso producción |
| D-05| ¿Container DI: Microsoft.Extensions.DI vs Autofac?                                       | Arquitectura motor nuevo |
| D-06| ¿Incluir `IClsLnStock` wrapper en BOF o solo en nuevo motor?                             | Compatibilidad legacy/nuevo |
| D-07| ¿Política de cancelación masiva: trigger automático vs job manual?                       | Runbook 10 §5.4 |
| D-08| ¿Retention de `log_error_wms`: 90 días vs 180 días vs 1 año?                             | Job nocturno   |

## 4. Roadmap de implementación (8 sprints)

### Sprint 1 (semana 1-2) · Fundamentos

- [ ] Resolver decisiones D-01, D-05, D-06.
- [ ] Crear repo del motor nuevo .NET 8.
- [ ] Configurar contenedor DI.
- [ ] Implementar `BeStock_res`, `ReservationRequest`, `ReservationResult`, `ReservationContext`.
- [ ] Implementar `IReservationLogger` + 5 implementaciones (NullLogger, InMemory, Console, WmsErrorTable, Composite).

### Sprint 2 (semana 3-4) · Handlers core

- [ ] Implementar `EntityLoadingStep`, `RequestValidationStep`, `StockQueryStep`.
- [ ] Implementar `DefaultReservationHandler` con FEFO.
- [ ] 30 unit tests sobre los 4 handlers.
- [ ] Setup BD efímera para integration tests.

### Sprint 3 (semana 5-6) · Handlers Clavaud

- [ ] Implementar `CompletePackagesHandler`, `IncompletePackagesHandler`.
- [ ] Implementar `EvaluateClavaudDynamic`.
- [ ] 20 unit tests sobre handlers Clavaud.
- [ ] Integration tests IT-05, IT-06.

### Sprint 4 (semana 7-8) · Handlers explosión + ZP

- [ ] Implementar `UMBasExplosionHandler` con marker `no_bulto = 1965`.
- [ ] Implementar `ZonaPickingHandler`.
- [ ] Implementar `PostProcessingStep`.
- [ ] 30 unit tests adicionales.
- [ ] Integration tests IT-04, IT-07, IT-08, IT-09, IT-13.

### Sprint 5 (semana 9-10) · Bridge legacy

- [ ] Implementar fachada `Inserta_Stock_Res_MI3` con misma firma del legacy.
- [ ] Bridge VB.NET ↔ .NET 8 (probable: REST interno o COM interop).
- [ ] Tests bridge: comparar payload de entrada/salida.
- [ ] Performance benchmark del bridge (target < 5ms overhead).

### Sprint 6 (semana 11-12) · Canary infrastructure

- [ ] Construir `CanaryReservationDispatcher`.
- [ ] Crear tabla `mi3_canary_diff` (en BOF, no en Killios).
- [ ] Implementar `DbBackedComparator`.
- [ ] Dashboard de diferencias canary.
- [ ] Habilitar canary silencioso (Fase 1 del rollout).

### Sprint 7 (semana 13-14) · Análisis canary y ajustes

- [ ] Análisis diario de `mi3_canary_diff`.
- [ ] Ajustar handlers para minimizar diferencias.
- [ ] Re-correr unit + integration tests con cada ajuste.
- [ ] Validar tasa de paridad > 99.5% en muestra de 10K pedidos.

### Sprint 8 (semana 15-16) · Producción

- [ ] Fase 2 del rollout: 5% al motor nuevo (canary visible).
- [ ] Monitoreo intensivo: latencia, errores, diferencias.
- [ ] Si KPIs OK, Fase 3: 50/50 blue/green.
- [ ] Si KPIs OK, Fase 4: 100% al nuevo.
- [ ] Mantener feature flag para rollback inmediato.

## 5. Métricas de progreso

### 5.1 Documentación (estado actual: ~95%)

- [x] 01 motor nuevo
- [x] 02 motor legacy
- [x] 03 comparison
- [x] 04 config propietario (corregido v2)
- [x] 05 FEFO + Clavaud
- [x] 06 handlers
- [x] 07 ciclo de vida stock_res (corregido v2)
- [x] 08 schema tablas Killios
- [x] 09 logging y observabilidad
- [x] 10 errores y troubleshooting
- [x] 11 tests
- [x] 12 TODOs y roadmap (este archivo)
- [ ] sql-catalog/reservation-tables.md (DDL completo)
- [ ] README.md del módulo (índice consolidado)
- [x] decisions/003 motor MI3 reescrito

### 5.2 Implementación (estado actual: 0% del nuevo motor)

| Componente                        | Estado    | Dueño |
|-----------------------------------|-----------|-------|
| Domain models                     | Pendiente | TBD   |
| `IReservationLogger` + impls      | Pendiente | TBD   |
| Handlers core (4)                 | Pendiente | TBD   |
| Handlers Clavaud (3)              | Pendiente | TBD   |
| Handlers explosión + ZP (3)       | Pendiente | TBD   |
| Post-processing                   | Pendiente | TBD   |
| Bridge VB↔.NET8                   | Pendiente | TBD   |
| Canary infrastructure             | Pendiente | TBD   |

### 5.3 Tests (estado actual: 0%)

- Unit: 0 / 80 (0%)
- Integration: 0 / 15 (0%)
- Canary: pendiente de infraestructura.

### 5.4 Decisiones (estado actual: 0 / 8)

Ninguna decisión D-01 a D-08 está resuelta.

## 6. Definition of Done del motor MI3 nuevo

Para considerar el motor MI3 nuevo "production-ready":

- [ ] **Documentación 100%** (12 archivos del módulo + sql-catalog + README).
- [ ] **Decisiones D-01 a D-08 resueltas** y documentadas.
- [ ] **Implementación 100%** de los 11 componentes (§5.2).
- [ ] **Cobertura tests > 85%** (unit + integration).
- [ ] **Canary > 7 días con paridad > 99.5%**.
- [ ] **Performance dentro del target** (p95 < 500ms, 100rps sostenido).
- [ ] **Concurrencia validada** (50 motores simultáneos sin negativos).
- [ ] **Runbooks operativos validados** (10 §5).
- [ ] **Dashboard de métricas operativas activo** (09 §7.3).
- [ ] **Plan de rollback probado** (toggle feature flag, < 1 min).
- [ ] **Erik aprobó cada uno de los 11 componentes**.

---

## Apéndice · Estado de archivos al 2026-04-27

| Archivo | Tamaño | Commit | Estado |
|---------|--------|--------|--------|
| 01 | 32 KB | 28cd4fc1 | Completo |
| 02 | 35 KB | a2c874c2 | Completo |
| 03 | 22 KB | 077e2744 | Completo, 4 riesgos abiertos |
| 04 | 19 KB | 5e84bbb4 (v2) | Corregido tras validar live |
| 05 | 18 KB | 3c212015 | Completo, 3 detalles a confirmar |
| 06 | 15 KB | 65370fcb | Completo |
| 07 | 16 KB | f944cfdc (v2) | Corregido tras descubrir campos inventados |
| 08 | 30 KB | 627ca2db | Completo, schema validado live |
| 09 | 22 KB | 0cc3642c | Completo |
| 10 | 18 KB | 3aece20f | Completo |
| 11 | 20 KB | a92a03bd | Completo |
| 12 | (este) | TBD | Completo |
| decisions/003 | 16 KB | 2eafba5a | Completo |

**Total**: ~263 KB de documentación en 13 archivos.
