# Decisión 003 — Reescritura del motor MI3 de VB.NET monolítico a .NET 8 modular

> **Estado**: ACEPTADA · Implementada en `WMS.DALCore/Reserva_Stock/` (rama `dev_2028_merge`)  
> **Fecha de decisión**: 2024 (estimada por commits Azure DevOps `ejcalderon0892/TOMWMS_BOF`)  
> **Autores**: Erik Calderón + equipo PrograX24/TOMWMS  
> **Última verificación**: passada 3.5 (este wms-brain), 2026-04-27  
> **Cross-refs**: `entities/modules/reservation/01-mi3-motor-nuevo-net8.md`, `02-mi3-motor-legacy-vb.md`, `03-comparison.md`, `sql-catalog/reservation-tables.md`

---

## 1. Contexto

El método `Reserva_Stock_From_MI3` en `clsLnStock_res_Partial.vb` (TOMIMSV4 / DAL / Transacciones / Stock_Reservado) acumuló durante años toda la lógica de reserva de stock para pedidos MI3 (sincronización de salidas desde Killios u otro origen). Entre 2018 y 2024 creció hasta **~8 145 líneas en un único método VB.NET**, con:

- 12 parámetros (4 ByRef in-out)
- ~120 variables locales preasignadas
- 3 etiquetas `INICIAR_EN_*` + 4 etiquetas auxiliares
- ~9 regiones (`#Region`), varias duplicadas
- Estructuras de control vía `GoTo` + bandera-set `ListaEstadosDeProceso`
- 1 invocación recursiva del propio método (con marker `No_bulto = 1965`)
- Llamadas directas a `XtraMessageBox.Show` desde la capa DAL
- Mutación in-place del parámetro `pStockResSolicitud`

Estos atributos, sumados a la presión de incorporar nuevas reglas (Clavaud dinámico, explosión por nivel máximo, devoluciones de proveedor, reserva en UMBas para SAP, reabasto con paletizado), generaron una situación insostenible:

- Cada nueva regla aumentaba la duplicación de bloques (los regiones "Reservar stock de zona NO Picking" y "Explosión por múltiplo" terminaron apareciendo dos veces cada uno con variantes mínimas).
- Los bugs en runtime se manifestaban en `log_error_wms` con strings hardcoded `ERROR_YYYYMMDDhhmm[A-Z]`, sin tipificación.
- El testing era prácticamente imposible: el método no se podía instanciar fuera de un escenario completo de pedido + bodega + propietario + producto + stock.
- La invocación de `XtraMessageBox.Show` desde DAL (línea L1929) hacía que cualquier despliegue como servicio backend se bloqueara silenciosamente al esperar interacción de usuario inexistente.

## 2. Problema

> **¿Cómo evolucionar el motor de reservas MI3 sin congelar el desarrollo del BOF, manteniendo paridad funcional con el legacy y habilitando testing automatizado y deployment headless?**

Restricciones duras:

1. **No interrumpir producción Killios**: el motor nuevo debe convivir con el legacy hasta paridad verificada.
2. **No requerir host ASP.NET para la migración inicial**: el caller productivo (`clsLnTrans_pe_det.cs`) sigue siendo VB.NET con SQL ConnectionScope manual.
3. **Mismo esquema de BD**: las tablas `stock_res`, `trans_pe_det`, `i_nav_ped_traslado_det`, `trans_pe_det_log_reserva`, `log_error_wms`, `propietarios`, `propietario_bodega`, `stock`, `productos` no se tocan.
4. **Compatibilidad con presentaciones, UMBas, Clavaud y devoluciones** existentes en producción.
5. **Configuración por propietario**: `clsBeI_nav_config_enc` (mapeada a `propietarios` y `propietario_bodega`) define el comportamiento; cada propietario puede tener flags distintos.

## 3. Alternativas evaluadas

### 3.1 Refactorizar in-place el método VB.NET

- **Pros**: cero migración, compatibilidad total inmediata.
- **Contras**: la deuda estructural (GoTo, polimorfismo de variables, bloques duplicados) está en el ADN del método. Refactorizar manteniendo VB.NET no resuelve la imposibilidad de testing ni la dependencia UI.
- **Decisión**: descartada.

### 3.2 Reescribir como C# 8 monolítico

- **Pros**: elimina dependencia UI, permite uso de LINQ moderno, testing posible.
- **Contras**: mantener un único método de 8K líneas en C# es marginalmente mejor pero conserva el problema de mantenimiento.
- **Decisión**: descartada (no resuelve el problema raíz).

### 3.3 Reescribir como pipeline modular (.NET 8) con Chain of Responsibility

- **Pros**: cada paso del pipeline es testeable aislado; cada handler de la cadena tiene responsabilidad única; el factory permite armar la cadena dinámicamente según `startingPoint`; tipado fuerte en el resultado (`ReservationResultDto`); separación neta de concerns.
- **Contras**: requiere más archivos (27 vs 1), curva de aprendizaje para quien venía del legacy, esfuerzo inicial de mapeo línea-a-línea.
- **Decisión**: ACEPTADA.

### 3.4 Adoptar un BPM/orchestrator externo (ej. Workflow Foundation, NServiceBus saga)

- **Pros**: visualización de flujo, persistencia de estado.
- **Contras**: agrega dependencia de infraestructura no justificada por la complejidad real; un pipeline secuencial in-process es suficiente.
- **Decisión**: descartada (over-engineering).

## 4. Decisión

Reescribir el motor MI3 como un módulo .NET 8 ubicado en `WMS.DALCore/Reserva_Stock/`, con la siguiente arquitectura:

### 4.1 Capas

```
StockReservationFacade.cs                 ← API pública (3 sobrecargas)
└─ ServiceFactory.cs                       ← DI manual (sin container)
   ├─ PipelineExecutor.cs                  ← Orquestador de IPipelineStep[]
   │  ├─ ValidationStep.cs                 ← Step 1
   │  ├─ EntityLoadingStep.cs              ← Step 2
   │  ├─ StockQueryStep.cs                 ← Step 3
   │  ├─ DateCalculationStep.cs            ← Step 4
   │  ├─ ReservationLoopStep.cs            ← Step 5 (con BuildHandlerChain por iteración)
   │  └─ PostProcessingStep.cs             ← Step 6
   └─ BuildHandlerChain (dinámico)         ← Chain of Responsibility
      ├─ CompletePackagesHandler           ← CASO_1
      ├─ IncompletePackagesHandler         ← CASO_2
      ├─ PickingZoneHandler                ← CASO_3
      ├─ NonPickingZoneHandler             ← CASO_4
      └─ UMBasExplosionHandler             ← CASO_EXPLOSION / CASO_UMBAS
```

### 4.2 Patrones aplicados

| Patrón                      | Donde                                                | Justificación |
|-----------------------------|------------------------------------------------------|---------------|
| **Pipeline (sequential)**   | `PipelineExecutor` + 6 steps                         | Romper el método legacy en pasos atómicos, abortables, observables |
| **Chain of Responsibility** | `IReservationHandler` + `BaseReservationHandler`     | Encadenar las estrategias de reserva con paso al siguiente si hay pendiente |
| **Factory**                 | `ServiceFactory.BuildHandlerChain(...)`              | Construir cadena dinámica según `startingPoint` y modo (Explosion/UMBas) |
| **Strategy**                | 5 handlers concretos                                 | Aislar la lógica de cada CASO sin condicionales dispersos |
| **Result object**           | `ReservationResultDto`                               | Tipado fuerte con status enum + razones de fallo + flags |
| **Adapter (legacy)**        | `ReservationResultDto.ToLegacyResult()`              | Mantener compat con callers VB.NET no migrados |

### 4.3 Decisiones de implementación clave

1. **No usar DI container** (`Microsoft.Extensions.DependencyInjection`). Razón: el caller principal es VB.NET sin ASP.NET host. `ServiceFactory.cs` declara explícitamente: `// NO usa DI container (compatibilidad con VB.NET legacy)`.
2. **Construir la cadena de handlers en cada iteración del loop**, no una sola vez. Permite cambiar `startingPoint` y modo (Explosion/UMBas) sin estado residual.
3. **Tolerancia decimal `0.000001`** preservada literalmente del legacy. Hardcoded por consistencia.
4. **Inmutabilidad del request**: el `ReservationContext.Request` es read-only para los handlers. Solo `PendingQuantity` y los flags de modo se mutan.
5. **Una sola transacción SQL** en `PostProcessingStep`. Sin persistencia parcial intermedia (a diferencia del legacy que persistía antes de la recursión).
6. **Logging estructurado** vía `IReservationLogger` con 5 operaciones (`LogInfo`, `LogCheckpoint`, `LogReservation`, `LogError`, `LogException`) y un vocabulario de checkpoints (`#STEP_*`, `#CASO_*`, `#*_PASS_TO_NEXT`, `#*_COMPLETED`, `#*_SKIP`).
7. **14 `ReservationFailureCode`** tipificados (NONE, NO_STOCK, LOT_NOT_FOUND, LOCATION_RESTRICTED_NO_STOCK, PRODUCT_STATE_REQUIRED_NO_STOCK, PICKING_ZONE_REQUIRED_NO_STOCK, NON_PICKING_ZONE_REQUIRED_NO_STOCK, RECEPTION_LOCATION_NOT_ALLOWED, ALL_STOCK_EXPIRED, ZONE_PRIORITY_CONFLICT, PRODUCT_NOT_FOUND, INVALID_QUANTITY, STORAGE_CONDITION_MISMATCH, MANUFACTURING_DATE_INVALID).
8. **3 sobrecargas en el facade** (`Reserva_Stock_From_MI3`, `Reserva_Stock_FromHandheld`, `Reserva_Stock_Manual`) convergen en `Reserva_Stock_Internal` para no duplicar lógica de orquestación.

## 5. Diferencias semánticas deliberadas con el legacy

(referencia completa en `03-comparison.md` §4)

| # | Diferencia                                                | Motor legacy         | Motor nuevo                        |
|---|------------------------------------------------------------|----------------------|------------------------------------|
| 1 | Evaluación Clavaud                                         | Estática (1 vez)     | Dinámica (cada iteración)          |
| 2 | Mutación de request                                        | Sí                   | No                                 |
| 3 | Persistencia parcial pre-recursión                         | Sí                   | No (TransactionScope total)        |
| 4 | Tipado de resultado                                        | `Boolean`            | `ReservationResultDto` + enum      |
| 5 | UI calls desde DAL                                         | Sí (`XtraMessageBox`)| No                                 |
| 6 | Recursión `ReservaStockFromMI3` → sí mismo                 | Sí (marker 1965)     | No (loop externo + handler chain)  |
| 7 | Variable polimórfica `lBeStockExistente`                   | Sí                   | No (3 listas explícitas distintas) |

Estas 7 diferencias **no son bugs ni "features perdidas"**: son decisiones explícitas tomadas para resolver la deuda estructural. El comportamiento observable externo se preserva en los 12 invariantes documentados en `03-comparison.md` §8.

## 6. Configuración del propietario que afecta al motor

Las siguientes columnas/flags de `propietarios` (PLURAL, 23 cols en Killios) y `propietario_bodega` (8 cols) modifican el comportamiento del motor:

| Columna                                       | Tipo | Efecto en motor nuevo                                          |
|-----------------------------------------------|------|----------------------------------------------------------------|
| `Conservar_Zona_Picking_Clavaud`              | bool | Activa/desactiva CASO_1 + CASO_2 (handlers Clavaud)            |
| `Permitir_Explosion_Presentacion`             | bool | Activa fallback `TryEnableExplosionFallback`                   |
| `Permitir_UMBas_Fallback`                     | bool | Activa fallback `TryEnableUMBasFallback`                       |
| `Explosion_Automatica`                        | bool | Activa explosión cuando `IdPresentacion = 0`                   |
| `Explosion_Automatica_Nivel_Max`              | int  | Limita explosión por nivel de ubicación                        |
| `Explosion_Automatica_Desde_Ubicacion_Picking`| bool | Permite explosión desde zona picking                            |
| `Rechazar_pedido_incompleto`                  | enum | Si = `Si`, lanza excepción cuando hay pendiente; si = `No`, devuelve `Partial` |
| `considerar_paletizado_en_reabasto`           | bool | Tareas de reabasto exigen tarimas completas                    |
| `Interface_SAP`                               | bool | Modifica resolución de UMBas para productos con `Reservar_En_UmBas` |
| `Tolerancia_Decimal`                          | double | Override del default `0.000001` (raramente usado)             |

Documentación detallada y mapeo a la matriz de comportamiento: `entities/modules/reservation/04-mi3-config-propietario.md` (a producir).

## 7. Plan de paridad y cutover

**Estado actual** (verificado en passada 3.5):

- Motor nuevo desplegado en `dev_2028_merge` y conectado al endpoint REST `POST /api/sync/salidas/mi3/insertar`.
- Caller productivo `clsLnTrans_pe_det.cs:1294` ya invoca `StockReservationFacade.Reserva_Stock_From_MI3`.
- El motor legacy en `clsLnStock_res_Partial.vb` permanece compilable pero **no se invoca** desde el flujo MI3 productivo.
- Otros flujos (HH picking, manual desde BOF Forms) usan las sobrecargas `Reserva_Stock_FromHandheld` y `Reserva_Stock_Manual` del facade nuevo.

**Pendiente** (riesgos identificados en `03-comparison.md` §9):

1. Verificación línea-a-línea del manejo de `Explosion_Automatica_Nivel_Max` (legacy L1320-L1333 vs `ReservationLoopStep.cs`).
2. Documentación de `pEs_Devolucion` (devoluciones a proveedor) en archivo separado.
3. Verificación del comportamiento de `considerar_paletizado_en_reabasto`.
4. Verificación de `Interface_SAP` flag.
5. Suite de tests de regresión derivada de 10-20 casos de `log_error_wms` histórico.

## 8. Consecuencias

### 8.1 Positivas

- **Testeabilidad**: cada step y cada handler son instanciables y mockeables aisladamente.
- **Observabilidad**: el vocabulario de checkpoints permite reconstruir cualquier decisión post-mortem.
- **Mantenibilidad**: agregar un nuevo CASO (ej. CASO_5 para reservas con prioridad de cliente) requiere 1 nuevo handler + 1 case en `BuildHandlerChain`, no modificar 8K líneas.
- **Deployment headless**: sin UI calls, el motor corre en cualquier proceso .NET 8 (servicio Windows, Docker, AWS Lambda, etc.).
- **Tipado de errores**: `ReservationFailureCode` permite UI consistente y telemetría agregable.
- **Inmutabilidad**: el caller no sufre side-effects en el request.

### 8.2 Negativas

- **Curva de aprendizaje** para devs que venían del legacy: hay que entender Pipeline + Chain antes de tocar.
- **27 archivos** vs 1: navegación distinta, IDE-dependiente.
- **Doble mantenimiento temporal**: hasta verificar paridad completa, ambos motores conviven en el repo (aunque solo uno está activo en producción).
- **Sin DI container**: cualquier dependencia nueva requiere instanciación manual en `ServiceFactory`. No hay auto-wiring.

### 8.3 Riesgos

- **Cambios en config del propietario** que el motor nuevo aún no maneje (pendientes en §7) podrían causar comportamiento divergente con el legacy. Mitigación: documentar exhaustivamente y agregar tests por cada flag.
- **Performance**: el legacy reusaba listas in-memory; el nuevo a veces reorden y filtra varias veces. No medido en producción aún.
- **`log_error_wms` cambia su tipología**: scripts de monitoreo que parsean strings hardcoded `ERROR_*` deben actualizarse para reconocer también el formato nuevo (`[FailureCode] Mensaje`).

## 9. Referencias

- Código fuente motor nuevo: `data/repos/TOMWMS_BOF/WMS.DALCore/Reserva_Stock/` (rama `dev_2028_merge`)
- Código fuente motor legacy: `data/repos/TOMWMS_BOF/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` L18192-L26337
- Caller productivo: `data/repos/TOMWMS_BOF/WMS.DALCore/Pedido/clsLnTrans_pe_det.cs` L1294
- Endpoint REST: `data/repos/TOMWMS_BOF/WMSWebAPI/Controllers/SyncSalidasController.cs` L133
- Documentación funcional: `entities/modules/reservation/01-mi3-motor-nuevo-net8.md`, `02-mi3-motor-legacy-vb.md`, `03-comparison.md`
- Schema Killios validado: `passada-3-2-killios/` + `sql-catalog/reservation-tables.md`
