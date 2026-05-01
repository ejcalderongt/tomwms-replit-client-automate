---
id: README
tipo: entity
estado: vigente
titulo: "Módulo `reservation` · Motor MI3 de reservas WMS"
modulo: [reservation]
tags: [entity, modulo/reservation]
---

# Módulo `reservation` · Motor MI3 de reservas WMS

> **Propósito**: documentar exhaustivamente el motor MI3 (Insertar_Stock_Res_MI3) que reserva inventario en TOMWMS. Cubre el motor legacy VB.NET (8K líneas) y el motor nuevo .NET 8 (en construcción), con foco en paridad funcional para una transición segura.
>
> **Estado**: documentación 100% completa al 2026-04-27. Implementación del motor nuevo: 0%.
>
> **Cross-refs**: `decisions/003-mi3-reescrito.md`, `sql-catalog/reservation-tables.md`.

---

## Índice de archivos

### Núcleo del módulo (`entities/modules/reservation/`)

| # | Archivo | Tema | Tamaño | Dependencias de lectura |
|---|---|---|---|---|
| 01 | `01-mi3-motor-nuevo-net8.md` | Arquitectura del motor nuevo .NET 8 (handlers, pipeline, DI) | 32 KB | (ninguna) |
| 02 | `02-mi3-motor-legacy-vb.md` | Motor legacy VB.NET de 8K líneas — anatomía, anchors, secciones | 35 KB | (ninguna) |
| 03 | `03-comparison.md` | Mapeo legacy ⟶ nuevo, paridad funcional, riesgos abiertos | 22 KB | 01, 02 |
| 04 | `04-mi3-config-propietario.md` | Matriz de config en `i_nav_config_enc` (69 cols) — flags por bodega+propietario+usuario | 19 KB | 03 |
| 05 | `05-mi3-algoritmo-fefo-clavaud.md` | Algoritmo FEFO + lógica Clavaud (zona picking + paquetes completos/incompletos) | 18 KB | 03, 04 |
| 06 | `06-mi3-handlers-detalle.md` | Detalle por handler de la cadena (10 handlers principales) | 15 KB | 03, 05 |
| 07 | `07-stock-res-ciclo-vida.md` | Máquina de estados de `stock_res`, transiciones, invariantes | 16 KB | 04, 06 |
| 08 | `08-mi3-tablas-killios.md` | Schema validado live de 6 tablas críticas (stock, stock_res, trans_pe_*, i_nav_*, log_error_wms) | 30 KB | 04, 07 |
| 09 | `09-mi3-logging-observabilidad.md` | Contrato `IReservationLogger`, vocabulario de checkpoints, queries para reconstruir flujos | 22 KB | 06, 08 |
| 10 | `10-mi3-errores-troubleshooting.md` | Catálogo de 11 errores + 5 runbooks operativos + 5 casos de soporte | 18 KB | 06, 09 |
| 11 | `11-mi3-tests.md` | Estrategia de testing (unit/integration/canary) + plan de rollout | 20 KB | 03, 06 |
| 12 | `12-mi3-todos-roadmap.md` | TODOs consolidados, riesgos abiertos, decisiones pendientes, roadmap 8 sprints | 16 KB | todos |

### Decisiones (`decisions/`)

| # | Archivo | Tema |
|---|---|---|
| 003 | `003-mi3-reescrito.md` | Decisión de reescribir el motor MI3 en .NET 8 + criterios de paridad |

### SQL Catalog (`sql-catalog/`)

| Archivo | Tema |
|---|---|
| `reservation-tables.md` | DDL completo + índices + CHECK constraints sugeridos para las 9 tablas del módulo |

---

## Lectura recomendada por rol

### Para un nuevo desarrollador del motor nuevo

1. **02** (entender el legacy)
2. **01** (entender el nuevo)
3. **03** (mapeo + diferencias)
4. **04** (config)
5. **08** (schema BD)
6. **05, 06, 07** (algoritmo + handlers + ciclo de vida)
7. **09** (logging)
8. **11** (tests)
9. **12** (TODOs y roadmap)
10. **10** (referencia operativa)

### Para soporte/operaciones

1. **10** (catálogo de errores y runbooks)
2. **09** (queries para reconstruir flujos)
3. **07** (ciclo de vida `stock_res`)
4. **08** (schema)

### Para QA / testing

1. **11** (estrategia completa)
2. **03** (paridad esperada legacy/nuevo)
3. **06** (handlers a testear)
4. **04, 07** (configs y estados a cubrir)

### Para arquitectura / decisión

1. **decisions/003** (decisión de reescritura)
2. **01** (arquitectura propuesta)
3. **03** (gap analysis)
4. **12** (decisiones pendientes D-01 a D-08)

### Para DBA

1. **sql-catalog/reservation-tables.md** (DDL completo)
2. **08** (descripción funcional + rarezas detectadas)
3. **07** (operaciones de mantenimiento)

---

## Convenciones del módulo

### Vocabulario de checkpoints (en `log_error_wms`)

| Prefijo         | Significado                                              |
|-----------------|---------------------------------------------------------|
| `#CASO_X_Y`     | Rama funcional del algoritmo (X=caso, Y=variante)       |
| `#STEP_*`       | Punto de control determinista del flujo                 |
| `#FALLBACK_*`   | Activación de un modo de respaldo                       |
| `#RECURSION_*`  | Marker de recursión (incluye `#RECURSION_1965` para UMBas legacy) |
| `INFO:`         | Evento informativo                                       |
| `RES:`          | Evento de creación/modificación de reserva              |
| `ERROR:`        | Error funcional                                          |
| `EXC:`          | Excepción capturada                                      |

Ver `09-mi3-logging-observabilidad.md` §3 para la lista completa.

### Tablas críticas

| Tabla                       | Cols | Rol                                          |
|-----------------------------|------|---------------------------------------------|
| `stock`                     | 33   | Inventario físico                            |
| `stock_res`                 | 35   | Reservas (UNCOMMITED → COMMITED → CONSUMED) |
| `trans_pe_enc`              | 70   | Encabezado de pedido                         |
| `trans_pe_det`              | 44   | Detalle de pedido (lo que se reserva)        |
| `i_nav_config_enc`          | 69   | Config del motor MI3 por bodega+propietario+usuario |
| `i_nav_ped_traslado_det`    | 22   | Sync MI3 ⟶ WMS                              |
| `log_error_wms`             | 15   | Bitácora de errores y checkpoints           |
| `propietarios`              | 23   | Datos básicos del propietario                |
| `propietario_bodega`        | 8    | Binding propietario↔bodega                  |

### Reglas críticas (NO violar)

1. **Killios productivo (`52.41.114.122,1437`/`TOMWMS_KILLIOS_PRD`) es READ-ONLY desde wms-brain**. Solo SELECTs vía cliente envoltorio que valida.
2. **El motor nuevo debe emitir EXACTAMENTE los mismos checkpoints que el legacy** para no romper queries de auditoría existentes.
3. **`MaquinaQueSolicita` debe persistirse byte-a-byte igual** entre legacy y nuevo (ver R-02 archivo 12).
4. **Tolerancia decimal hardcoded en `0.000001`**. NO cambiar sin coordinación.
5. **Marker `no_bulto = 1965`** reservado para recursión UMBas. NO usar para otros propósitos.

---

## Hallazgos importantes durante la documentación

### Hallazgo 1 · Flags del motor NO viven en `propietarios`

La versión 1 del archivo 04 ubicaba erróneamente los flags del motor MI3 (Conservar_Zona_Picking_Clavaud, Explosion_Automatica, etc.) en la tabla `propietarios`. La validación live contra Killios reveló que:

- `propietarios` (23 cols) solo tiene datos básicos.
- `propietario_bodega` (8 cols) solo tiene binding + activo.
- **Los flags viven en `i_nav_config_enc` (69 cols)** con granularidad bodega+propietario+usuario.

Corregido en `04-mi3-config-propietario.md` v2 (commit 5e84bbb4).

### Hallazgo 2 · Typo histórico en `i_nav_config_enc`

Existen DOS columnas para el mismo concepto:
- `explosio_automatica_nivel_max` (typo histórico, sin "n")
- `explosion_automatica_nivel_max` (versión corregida)

Ambas coexisten en producción. Riesgo R-01 (alta severidad) abierto en archivo 12.

### Hallazgo 3 · Campos inexistentes en `stock_res`

La versión 1 del archivo 07 atribuía a `stock_res` columnas como `Fecha_Commit`, `Usuario_Cancelacion`, `EsExplosion`, `IdPresentacionOriginal`. **NINGUNA existe**. Las transiciones se infieren cruzando con `trans_pe_enc` y los logs.

Corregido en `07-stock-res-ciclo-vida.md` v2 (commit f944cfdc).

### Hallazgo 4 · Marker `no_bulto = 1965`

Stock con `no_bulto = 1965` indica que fue generado por la recursión legacy de UMBas. Solo está documentado en código VB (no en BD). Riesgo R-05 abierto.

---

## Cómo contribuir a este módulo

1. **Lee primero** los archivos relevantes a tu cambio.
2. **Valida cualquier afirmación sobre BD** ejecutando una consulta READ-ONLY contra Killios.
3. **No inventes columnas o flags** sin validar contra `INFORMATION_SCHEMA.COLUMNS`.
4. **Documenta correcciones explícitamente** (como en archivo 04 v2 y 07 v2).
5. **Cross-refs en TODOS los archivos** que mencionen otra parte del módulo.
6. **Actualiza el roadmap** (`12-mi3-todos-roadmap.md` §5) con cada cambio significativo.
7. **No modifiques `decisions/003`** sin discusión previa.

---

## Estado al 2026-04-27

- Documentación: ✅ 100% (12 archivos núcleo + 1 decisión + 1 SQL catalog + este README)
- Implementación motor nuevo: ⏳ 0% (ver roadmap §5.2 archivo 12)
- Tests: ⏳ 0% (ver roadmap §5.3 archivo 12)
- Decisiones pendientes: ⏳ 8 (D-01 a D-08, ver archivo 12 §3)

**Próximo paso operativo**: resolver decisiones D-01, D-05, D-06 con Erik para desbloquear Sprint 1.
