# 03 · Reserva de Stock MI3 — Comparación Motor Nuevo vs Motor Legacy

> **Propósito**: matriz de paridad funcional entre `WMS.DALCore/Reserva_Stock/` (.NET 8, motor nuevo) y `clsLnStock_res_Partial.vb` (~8 145 L, motor legacy). Sirve para:
>
> 1. Verificar que ningún comportamiento del legacy se perdió silenciosamente.
> 2. Documentar las decisiones de diseño deliberadas que **cambiaron** la semántica.
> 3. Habilitar troubleshooting cruzado: si un pedido fallaba en legacy de cierta manera, qué `FailureCode` debería emitir el nuevo.
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md`, `02-mi3-motor-legacy-vb.md`, `decisions/003-mi3-reescrito.md`, `sql-catalog/reservation-tables.md`.

---

## Índice

1. Mapeo de etapas (legacy region → nuevo step/handler)
2. Mapeo de variables de control
3. Mapeo de etiquetas `INICIAR_EN_*` vs `startingPoint`
4. Diferencias semánticas deliberadas
5. Comportamientos del legacy que **NO** se trasladaron (intencional)
6. Comportamientos del legacy que **SÍ** se trasladaron literalmente
7. Tabla de equivalencia para troubleshooting (códigos de error → FailureCode)
8. Matriz de invariantes preservadas
9. Riesgos abiertos y áreas de validación pendiente

---

## 1. Mapeo de etapas (region/anchor legacy → step/handler nuevo)

| Etapa funcional                        | Legacy (anchor / region / line)                                       | Nuevo (clase / método)                                                                  |
|----------------------------------------|------------------------------------------------------------------------|------------------------------------------------------------------------------------------|
| Validación de input                    | (implícita, dispersa en setup L122-L169 + checks defensivos)          | `ValidationStep.Execute` (83 L)                                                          |
| Carga de bodega y línea de pedido      | `Cargar_Bodega_Y_Linea_Pedido` (L134)                                  | `EntityLoadingStep.LoadBodega` + `LoadPedidoDet` (parte de 141 L)                       |
| Carga de producto + presentación       | `Get_Objetos_Producto` (L143)                                          | `EntityLoadingStep.LoadProducto` + `LoadPresentations`                                   |
| Carga de 3 listas de stock             | `Obtener_Listas_De_Stock` (auxiliar 64 L)                              | `StockQueryStep.QueryStock` + `PartitionByZone` (parte de 354 L)                        |
| Conversión de cantidad a unidades      | `Split_Decimal` + lookup `clsBeProducto_Presentacion.Factor`           | `EntityLoadingStep.ConvertQuantityToUnits` (usa `QuantityConverter`)                     |
| Restar reservas pendientes del stock   | `Procesar_Y_Restar_Stock_Reservado` (3 invocaciones, 59 L)             | `StockQueryStep.SubtractPendingReservations` (incluido en 354 L)                        |
| Detección de stock vencido             | inline (`s.Fecha_vence < Now`) en cada loop                           | `StockQueryStep.FilterExpiredStock` → emite `ALL_STOCK_EXPIRED`                          |
| Cálculo de fecha mínima FEFO           | `Get_Fecha_Vence_Minima_Stock_Reserva_MI3` (3 invocaciones)            | `DateCalculationStep.Execute` (85 L) — calcula 4 fechas mínimas en un ciclo           |
| Decisión modo explosión / UMBas        | `Stock_Requiere_Explosion` + bloque tras anchor `EXPLOSIONAR_PRODUCTO` | `ReservationLoopStep.TryEnableExplosionFallback` + `TryEnableUMBasFallback`             |
| Reserva pallets COMPLETOS no-picking   | Anchor `INICIAR_EN_1` (L1273-L1903)                                    | `CompletePackagesHandler` (CASO_1, 144 L)                                                |
| Reserva pallets INCOMPLETOS no-picking | Anchor `INICIAR_EN_2` (L1904-L2711)                                    | `IncompletePackagesHandler` (CASO_2, 139 L)                                              |
| Reserva en zona PICKING                | Anchor `INICIAR_EN_3` + region "Reservar stock de zona de picking" (L2752-L3201) | `PickingZoneHandler` (CASO_3, 137 L)                                          |
| Reserva en zonas NO-picking            | Region "Reservar stock de zona NO Picking" (L3920-L4645 + L4654-L5683 duplicada) | `NonPickingZoneHandler` (CASO_4, 137 L)                                       |
| Explosión por múltiplo                 | Region "Explosión por múltiplo" (L3133-L3201 + L6204-L6272 duplicada)  | `UMBasExplosionHandler.ProcessExplosion` (parte de 193 L)                                |
| Reserva en UMBas                       | Recursión `Reserva_Stock_From_MI3(... No_bulto = 1965)` (L8059-L8132)  | `UMBasExplosionHandler.ProcessUMBas` (parte de 193 L)                                    |
| Persistencia en `stock_res`            | `Inserta_Stock_Reservado` (varias invocaciones)                        | `PostProcessingStep.PersistReservations` (parte de 121 L)                               |
| Update `trans_pe_det`                  | inline al final de cada bloque                                         | `PostProcessingStep.UpdateTransPeDet`                                                    |
| Update `i_nav_ped_traslado_det`        | `clsLnI_nav_ped_traslado_det.Actualizar_Process_Result` (L8103)        | `PostProcessingStep.UpdateTrasladoDet` (solo si Indicador = TRASLADO)                   |
| Log de error / mensaje                 | `clsLnLog_error_wms.Agregar_Error` (varias)                            | `PostProcessingStep.LogFailures` + `IReservationLogger` (5 niveles)                      |

---

## 2. Mapeo de variables de control

| Legacy (variable)                           | Nuevo (campo / método)                                  | Cambio semántico |
|---------------------------------------------|----------------------------------------------------------|------------------|
| `vCantidadCompletada As Boolean`            | `ReservationContext.IsQuantityFullyReserved()`           | Derivado de PendingQuantity (no se setea explícitamente) |
| `vCantidadPendiente As Double`              | `ReservationContext.PendingQuantity`                     | Mismo significado, mismo nombre conceptual |
| `vCantidadSolicitadaPedido As Double`       | `ReservationContext.OriginalRequestedQuantity`           | Inmutable (legacy se reasignaba) |
| `vBusquedaEnUmBas As Boolean`               | `ReservationContext.IsUMBasModeEnabled`                  | Mismo, pero se setea exclusivamente en `TryEnableUMBasFallback` |
| `vRestoStockReservado As Boolean`           | (no equivalente)                                         | Eliminado: el nuevo siempre resta reservas pendientes en `StockQueryStep` |
| `vEncontroExistenciaEnPresentacion`         | (no equivalente directo)                                 | Eliminado: la decisión vive en `CanProcess` de cada handler |
| `vOrdernarListaStockSinPresentacionPrimero` | (no equivalente)                                         | Eliminado: orden FEFO + lic_plate es universal |
| `vConvirtioCantidadSolicitadaEnUmBas`       | (implícito en `IsUMBasModeEnabled`)                      | Subsumido |
| `vSolicitudEsEnUMBas`                       | `ReservationContext.Request.IdPresentacion == 0`         | Derivado del request (no campo de estado) |
| `Iniciar_En As Integer`                     | parámetro `startingPoint` de `BuildHandlerChain`         | Mismo concepto, sin GoTo (ver §3) |
| `ListaEstadosDeProceso As List(Of Integer)` | (no equivalente)                                         | Eliminado: el pipeline secuencial + chain of responsibility no necesita guards |
| `ExcepcionFechaVenceEsInferiorEnZonaPicking`| (lógica reincorporada en `DateCalculationStep`)          | Reescrito; ya no es excepción sino fecha mínima distinta por zona |
| `vFechaMinimaVenceZonaPicking`              | `ReservationContext.MinExpirationDatePickingZone`        | Mismo |
| `vFechaMinimaVenceZonaALM`                  | `ReservationContext.MinExpirationDateNonPickingZones`    | Mismo |
| `vVenceMinimaPickingCompletoClavaud`        | `ReservationContext.MinExpirationCompletePalletsClavaud` | Mismo |
| `vVenceMinimaPickingInCompletoClavaud`      | `ReservationContext.MinExpirationIncompletePalletsClavaud` | Mismo |
| `lBeStockExistente` (polimórfica)           | `ReservationContext.WorkingStockList`                    | Inmutable en estructura, mutable solo en `Cantidad` por handlers |
| `lBeStockExistenteZonaPicking`              | `ReservationContext.StockListPickingZone`                | Mismo |
| `lBeStockExistenteZonasNoPicking`           | `ReservationContext.StockListNonPickingZones`            | Mismo |
| `lBeStockConPalletsCompletosClavaud`        | filtro `s.Pallet_Completo` dentro de `CompletePackagesHandler.CanProcess` | No se preacumula en lista, se filtra on-demand |
| `lBeStockConPalletsInCompletosClavaud`      | filtro `!s.Pallet_Completo` dentro de `IncompletePackagesHandler.CanProcess` | Idem |

---

## 3. Mapeo de `INICIAR_EN_*` vs `startingPoint`

| Legacy            | Nuevo (`startingPoint`) | Cadena resultante en `BuildHandlerChain` |
|-------------------|-------------------------|-------------------------------------------|
| `INICIAR_EN_1`    | `1`                     | `[Complete? + Incomplete? + Picking + NonPicking]` (Complete/Incomplete solo si Clavaud=true) |
| `INICIAR_EN_2`    | `2`                     | `[Incomplete? + Picking + NonPicking]` |
| `INICIAR_EN_3`    | `3`                     | `[Picking + NonPicking]` |
| `INICIAR_EN_4`*   | `4`                     | `[NonPicking]` |
| (caso default)    | `0`                     | Cadena completa (igual que case 1) |
| (modo explosión)  | (cualquiera)            | `[UMBasExplosion]` (override total) |

*`INICIAR_EN_4` no aparece en el extract leído del legacy pero está documentado en `decisions/003-mi3-reescrito.md` como existente en otra rama del archivo.

> **Diferencia mecánica clave**: el legacy usa `Select Case Iniciar_En` + `GoTo`, lo que convierte el método entero en un autómata implícito. El nuevo usa un factory que **construye dinámicamente la cadena** y la ejecuta con paso explícito `SetNext`. La cadena es introspectable, testeable y reusable.

---

## 4. Diferencias semánticas deliberadas

### 4.1 Clavaud dinámico vs Clavaud estático

| Aspecto | Legacy | Nuevo |
|---------|--------|-------|
| Evaluación | Una vez al entrar al método (`If pBeConfigEnc.Conservar_Zona_Picking_Clavaud Then`) | En **cada iteración** del loop (`EvaluateClavaudDynamic`) |
| Degradación a CASO_3 si pendiente < pallet | No (siempre intenta CASO_1/CASO_2 si flag=on) | Sí, degrada para no romper inutilmente la zona picking |
| Resultado típico | Vaciar pallets aunque sobre fracción | Preservar pallets, servir desde picking si tiene sentido |

**Justificación**: el legacy podía romper un pallet completo de no-picking para servir 2 cajas, dejando un pallet "casi-completo" inutilizable. El nuevo prioriza vaciar zona picking en esos casos.

### 4.2 Inmutabilidad del request

| Aspecto | Legacy | Nuevo |
|---------|--------|-------|
| `pStockResSolicitud.IdPresentacion = 0` permitido | Sí (L272 y L8067) | No (request es read-only para handlers) |
| Re-asignación `pStockResSolicitud = BeStockRes` permitida | Sí (L8119) | No (campo `Request` es get-only en context) |
| Efecto colateral en caller | Sí (caller recibe objeto mutado) | No (request original intacto post-ejecución) |

**Justificación**: la mutación silenciosa del legacy fue causa documentada de bugs en `clsLnTrans_pe_det.cs` cuando reusaba el objeto.

### 4.3 Persistencia parcial vs transaccional

| Aspecto | Legacy | Nuevo |
|---------|--------|-------|
| Reservas en presentación se persisten antes de buscar UMBas | Sí (`Inserta_Stock_Reservado` en L8077, **antes** de la recursión) | No (todas las reservas se acumulan en `context.CreatedReservations` y se persisten al final en `PostProcessingStep`) |
| Si la búsqueda UMBas falla | Las reservas en presentación quedan persistidas | Rollback total (TransactionScope) |
| Resultado en BD si error inesperado a mitad | Estado parcial inconsistente | Estado consistente (todo o nada) |

**Justificación**: durante la passada 3-2 se documentó en `log_error_wms` un patrón "reserva fantasma" donde quedaban filas en `stock_res` sin línea de pedido cerrada. El nuevo lo elimina por diseño.

### 4.4 Tipado de resultado

| Aspecto | Legacy | Nuevo |
|---------|--------|-------|
| Tipo de retorno | `Boolean` | `ReservationResultDto` (status enum + cantidades + 14 failure codes + reservas + flags) |
| Información de razón de fallo | Concatenada en `pBeTrasladoDet.Process_Result` (string) | Estructurada en `List<ReservationFailureReason>` con código + mensaje + qty |
| Distinción Partial vs Failed | No (caller infiere de cantidad) | Explícita (`ReservationStatus.Partial` distinto de `Failed`) |
| Adapter para callers viejos | (no aplica) | `ReservationResultDto.ToLegacyResult()` devuelve `ReservationResult` |

### 4.5 UI calls desde DAL

| Aspecto | Legacy | Nuevo |
|---------|--------|-------|
| Invocación de `XtraMessageBox.Show` | Sí (L1929) | No (eliminada, se devuelve `FailureReason`) |
| Compatible con backend headless | No (bloquea sin sesión gráfica) | Sí (puro C# sin UI) |
| Logging de la condición | A través de MessageBox + `clsLnLog_error_wms` | A través de `IReservationLogger` (`LogError`) y `FailureReasons` |

---

## 5. Comportamientos del legacy que NO se trasladaron (intencional)

1. **Recursión `Reserva_Stock_From_MI3` → sí mismo con marker `No_bulto = 1965`**: reemplazada por bucle externo en `ReservationLoopStep` que reconstruye la cadena de handlers cambiando el modo. Ventajas: stack predecible, debugger amigable, no riesgo de StackOverflow en pedidos atípicos.
2. **`ListaEstadosDeProceso` como guard de re-entry**: eliminada. El pipeline secuencial sin GoTo no requiere guards.
3. **Variable polimórfica `lBeStockExistente`**: eliminada. Hay 3 listas explícitas distintas, sin reasignación.
4. **Tres llamadas independientes a `clsLnStock.lStock`** con flags distintos: consolidadas en una única consulta + particionado en memoria (`StockQueryStep`).
5. **Tres llamadas independientes a `Get_Fecha_Vence_Minima_Stock_Reserva_MI3`** con duplicación de I/O: consolidadas en una única ciclo de `DateCalculationStep` que calcula las 4 fechas mínimas con LINQ sobre las listas ya en memoria.
6. **`XtraMessageBox.Show` desde DAL**: eliminado.
7. **Mutación de `pStockResSolicitud`**: eliminada.
8. **Persistencia parcial pre-recursión**: eliminada.
9. **Bloques duplicados "Reservar stock de zona NO Picking"** y **"Explosión por múltiplo"** (cada uno aparecía 2 veces en el legacy): consolidados en handlers únicos parametrizados.
10. **`Debug.Print` con códigos hardcoded** (IdProductoBodega = 616, código 00190454): eliminados. Logging unificado vía `IReservationLogger.LogCheckpoint`.
11. **Excepciones en string crudo con código `ERROR_YYYYMMDDhhmm[A-Z]`**: reemplazadas por enum `ReservationFailureCode` (14 valores). Los códigos legacy se documentan en §7 como **mapping de troubleshooting**.

---

## 6. Comportamientos del legacy que SÍ se trasladaron literalmente

1. **Tolerancia decimal `0.000001`**: hardcoded igual en ambos motores. Comparaciones `<= 0.000001` para considerar "cero".
2. **FEFO obligatorio**: ambos ordenan por `Fecha_vence ASC` en cada bloque de selección.
3. **Tie-breaker por `Lic_plate`**: ambos usan `OrderBy(Fecha_vence).ThenBy(Lic_plate)` cuando hay empate de fecha.
4. **Estado inicial de reserva = `"UNCOMMITED"`**: ambos crean filas con este estado en `stock_res`.
5. **`Indicador` = "PED"** por defecto si vacío: el legacy lo asigna en L1366 (`If BeStockRes.Indicador = "" Then BeStockRes.Indicador = "PED"`). En el nuevo, se preserva este default si el caller no provee otro.
6. **Distinción CASO_1 vs CASO_2 vs CASO_3 vs CASO_4** según pallet completo/incompleto + ubicación picking/no-picking: idéntica.
7. **Activación de Clavaud por flag `Conservar_Zona_Picking_Clavaud`** del propietario: ambos respetan; el nuevo agrega evaluación dinámica además.
8. **`IdPresentacion = 0` como marca de UMBas**: ambos usan esta convención semántica.
9. **`vCantidadProductoPorTarima = CajasPorCama * CamasPorTarima`**: el cálculo conceptual de "qué es un pallet completo" se preserva (aunque en el nuevo se confía en el flag persistido `clsBeStock.Pallet_Completo`).
10. **Mapeo campo-por-campo de `clsBeStock` → `clsBeStock_res`**: los 35 campos se copian igual en ambos motores (ver `sql-catalog/reservation-tables.md`).
11. **Persistencia en las mismas 5 tablas**: `stock_res`, `trans_pe_det`, `i_nav_ped_traslado_det`, `trans_pe_det_log_reserva`, `log_error_wms`.

---

## 7. Tabla de equivalencia para troubleshooting

Mapping de códigos legacy (hallados en `log_error_wms` histórico) → `ReservationFailureCode` del motor nuevo:

| Código legacy en `log_error_wms`   | Significado                                                         | `ReservationFailureCode` equivalente |
|-------------------------------------|----------------------------------------------------------------------|---------------------------------------|
| `ERROR_202302061300E`               | Cantidad disponible negativa en INICIAR_EN_1                         | `INVALID_QUANTITY` (con detalle CASO_1) |
| `ERROR_202302061300F`               | Cantidad disponible negativa en INICIAR_EN_2                         | `INVALID_QUANTITY` (con detalle CASO_2) |
| `Error_202303031731`                | Reabasto sin tarimas completas + config restrictiva                  | `NO_STOCK` con `Message` específico de reabasto |
| `Error_202212140140D`               | Pedido incompleto + `Rechazar_pedido_incompleto = Si`                | `NO_STOCK` con flag `Status = Failed` |
| `ERROR_202302021127` (comentado)    | Explosión sin presentación default (no se lanza)                     | `PRODUCT_NOT_FOUND` o `INVALID_QUANTITY` (situación se previene en `EntityLoadingStep`) |
| `ERROR_S0002`                       | Stock insuficiente código de mensaje genérico                        | `NO_STOCK` |
| `ERROR_S0004`                       | No hay stock en UMBas tras intento de explosión                      | `NO_STOCK` con flag `UsedExplosion = true` |

> Cuando se debugue un pedido viejo cuyo error está en `log_error_wms` con código legacy, mapear con esta tabla para entender qué `FailureCode` emitiría el motor nuevo en la misma circunstancia. Útil para verificar paridad con tests de regresión.

---

## 8. Matriz de invariantes preservadas

| # | Invariante                                                | Legacy | Nuevo |
|---|------------------------------------------------------------|--------|-------|
| 1 | Nunca se reserva stock vencido                             | sí (filtros inline) | sí (`StockQueryStep` + `ALL_STOCK_EXPIRED`) |
| 2 | FEFO obligatorio                                           | sí | sí |
| 3 | Tie-breaker por `Lic_plate`                                | sí | sí |
| 4 | Estado inicial `"UNCOMMITED"`                              | sí | sí |
| 5 | Tolerancia decimal `0.000001`                              | sí | sí |
| 6 | Una sola persistencia transaccional al final               | **no** (persistencia parcial pre-recursión) | sí (TransactionScope) |
| 7 | Inmutabilidad del request                                   | **no** (mutación de `pStockResSolicitud`) | sí (Request read-only) |
| 8 | Sin UI calls desde capa DAL                                | **no** (XtraMessageBox) | sí |
| 9 | Vocabulario único de error tipado                          | **no** (strings con códigos)  | sí (enum + DTO) |
| 10 | Logging estructurado con checkpoints                      | parcial (`Debug.Print` + `clsLnLog_error_wms`) | sí (`IReservationLogger` 5 niveles + checkpoints) |
| 11 | `IdPresentacion = 0` marca UMBas                          | sí | sí |
| 12 | Reservas creadas pertenecen al `IdTransaccion` del request | sí | sí |

> Las invariantes #6, #7, #8 y #9 son donde el motor nuevo **no preserva el legacy** sino que lo **mejora deliberadamente**. Las restantes 8 sí se preservan literalmente.

---

## 9. Riesgos abiertos y áreas de validación pendiente

1. **Comportamiento bajo `Explosion_Automatica_Nivel_Max`**: el legacy usa esta config en L1320-L1333 dentro de INICIAR_EN_1. En el motor nuevo no se ha verificado que la lógica esté presente con la misma semántica (referencia parcial en `ReservationLoopStep` pero no confirmada línea-a-línea). **Acción**: revisar `ReservationLoopStep.cs` L200-L300 contra L1320-L1333 del legacy.
2. **Manejo de `pEs_Devolucion` (devoluciones a proveedor)**: el legacy lo deriva en setup (L130) y lo pasa por todas las invocaciones a `clsLnStock.lStock`. En el nuevo se ha visto el flag en `ReservationContext` pero no se ha mapeado el camino completo a través de los handlers. **Acción**: documentar en archivo separado `reservas-devolucion-proveedor.md`.
3. **`considerar_paletizado_en_reabasto`**: el legacy abre MessageBox (L1929) si esta config está activa y no hay tarimas completas durante reabasto. El nuevo debería emitir `FailureCode` específico. **No verificado**.
4. **`Interface_SAP` flag**: usado en L270 para forzar UMBas si el estado del producto es `Reservar_En_UmBas`. Hay que verificar el camino equivalente en `EntityLoadingStep` o `ReservationLoopStep`.
5. **Tests de regresión**: no hay tests unitarios visibles en `WMS.DALCore/Reserva_Stock/` que ejerciten el nuevo motor con casos extraídos del histórico de `log_error_wms`. Se recomienda derivar 10-20 casos golden de Killios y armar suite de regresión.

---

> Próximo: `decisions/003-mi3-reescrito.md` documentará la **decisión arquitectónica** (por qué se reescribió, qué se evaluó como alternativas, qué se decidió y las matrices de configuración del propietario que afectan al motor).
