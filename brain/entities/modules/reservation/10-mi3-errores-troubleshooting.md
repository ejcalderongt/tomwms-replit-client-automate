# 10 · Catálogo de errores y troubleshooting del motor MI3

> **Propósito**: catálogo accionable de los errores que el motor MI3 puede producir (legacy y nuevo), su causa raíz, cómo identificarlos en `log_error_wms`, y procedimientos paso a paso para diagnosticar y resolver. Pensado para uso operativo en guardia.
>
> **Cross-refs**: `06-mi3-handlers-detalle.md` (handlers donde nacen los errores), `08-mi3-tablas-killios.md` §6 (tabla `log_error_wms`), `09-mi3-logging-observabilidad.md` (queries base).

---

## Índice

1. Convención de códigos de error
2. Errores funcionales (NO_STOCK, INVALID_INPUT, CONFIG_INVALID, etc.)
3. Errores de excepción (NullRef, TimeoutSql, DeadlockVictim)
4. Errores semánticos (paridad legacy/nuevo)
5. Procedimientos de troubleshooting (runbooks)
6. Casos de soporte conocidos (postmortem-ready)
7. Tabla de cross-reference codepoint → error → archivo

---

## 1. Convención de códigos de error

El motor nuevo emite errores en `MensajeError` con prefijo `ERROR:` o `EXC:` seguido de un **código** y descripción:

```
ERROR: NO_STOCK | Pedido 12345 linea 6 sin stock disponible (cant_solicitada=10, disponible=0)
EXC: TimeoutSql | clsLnStock.lStock | Timeout 30s | conn=Killios
```

| Categoría     | Prefijo | Detección en `log_error_wms`              |
|---------------|---------|-----|
| Funcional     | `ERROR:`| `MensajeError LIKE 'ERROR:%'`             |
| Excepción     | `EXC:`  | `MensajeError LIKE 'EXC:%'`               |
| Checkpoint    | `#`     | `MensajeError LIKE '#%'`                  |
| Info          | `INFO:` | `MensajeError LIKE 'INFO:%'`              |
| Reserva       | `RES:`  | `MensajeError LIKE 'RES:%'`               |

## 2. Errores funcionales

### 2.1 `NO_STOCK`

**Mensaje típico**: `ERROR: NO_STOCK | Pedido X linea Y sin stock disponible (cant_solicitada=N, disponible=M)`

**Disparado por**:
- `ReservationLoopStep` cuando se agotaron todos los handlers y `CantidadAcumulada < CantidadSolicitada` y la config tiene `rechazar_pedido_incompleto = 1`.
- `IncompletePackagesHandler` cuando `pTarea_Reabasto = True` + `considerar_paletizado_en_reabasto = 1` y no hay tarimas completas.

**Causa raíz posibles**:
1. Stock realmente agotado (verificar `SELECT SUM(cantidad) FROM stock WHERE IdProductoBodega = ? AND activo = 1`).
2. Stock existe pero está totalmente reservado por otros pedidos (verificar `stock_res WHERE estado IN ('UNCOMMITED', 'COMMITED')`).
3. Stock existe pero no es elegible (vencido, mal estado, ubicación excluida).
4. Producto recién recibido pero todavía no liberado (estado distinto a BUEN_ESTADO).

**Diagnóstico**:
```sql
-- 1. Stock total del producto
SELECT IdBodega, IdProductoBodega, SUM(cantidad) AS StockTotal
FROM stock
WHERE IdProductoBodega = @IdProductoBodega
  AND IdBodega = @IdBodega
  AND activo = 1
GROUP BY IdBodega, IdProductoBodega;

-- 2. Reservas activas
SELECT estado, SUM(cantidad) AS Reservado
FROM stock_res
WHERE IdProductoBodega = @IdProductoBodega
  AND IdBodega = @IdBodega
  AND estado IN ('UNCOMMITED', 'COMMITED')
GROUP BY estado;

-- 3. Stock por estado del producto
SELECT IdProductoEstado, COUNT(*), SUM(cantidad)
FROM stock
WHERE IdProductoBodega = @IdProductoBodega AND activo = 1
GROUP BY IdProductoEstado;

-- 4. Stock por ubicación (¿hay en zona excluida?)
SELECT u.codigo, u.es_picking, SUM(s.cantidad) AS Cant
FROM stock s
JOIN ubicacion u ON u.IdUbicacion = s.IdUbicacion
WHERE s.IdProductoBodega = @IdProductoBodega
  AND s.IdBodega = @IdBodega
  AND s.activo = 1
GROUP BY u.codigo, u.es_picking;
```

**Acciones correctivas**:
- Si stock real existe pero está reservado por pedido viejo: ejecutar limpieza de reservas EXPIRED (`UPDATE stock_res SET estado='EXPIRED' WHERE estado='UNCOMMITED' AND fec_agr < DATEADD(DAY,-7,GETDATE())`).
- Si stock está en estado distinto a BUEN_ESTADO: revisar workflow de aprobación de calidad.
- Si stock en ubicación excluida: revisar `i_nav_config_enc.excluir_ubicaciones_reabasto`.

### 2.2 `INVALID_INPUT`

**Mensaje típico**: `ERROR: INVALID_INPUT | Cantidad solicitada <= 0` o `IdProductoBodega no existe`

**Disparado por**: `EntityLoadingStep` o `RequestValidationStep` (motor nuevo).

**Causa raíz**: el JSON de Killios envía datos inválidos o el adapter no los normalizó.

**Diagnóstico**:
```sql
-- Última request del pedido
SELECT Fecha, MensajeError
FROM log_error_wms
WHERE IdPedidoEnc = @IdPedido
  AND MensajeError LIKE '#STEP_INICIO%'
ORDER BY Fecha DESC;

-- Detalle del pedido en BD
SELECT * FROM trans_pe_det WHERE IdPedidoDet = @IdPedidoDet;
```

**Acciones**:
- Si la cantidad es 0 o negativa: revisar el JSON de origen (Killios) y el adapter de entrada.
- Si `IdProductoBodega` no existe: posible desincronización de catálogos. Re-sync productos.

### 2.3 `CONFIG_INVALID`

**Mensaje típico**: `ERROR: CONFIG_INVALID | i_nav_config_enc no encontrado para bodega X propietario Y`

**Disparado por**: `EntityLoadingStep` cuando no encuentra fila en `i_nav_config_enc`.

**Causa raíz**: nuevo propietario o nueva bodega sin config inicial.

**Diagnóstico**:
```sql
SELECT * FROM i_nav_config_enc
WHERE idempresa = @IdEmpresa
  AND idbodega = @IdBodega
  AND idPropietario = @IdPropietario;
```

**Acción**: insertar fila con valores default desde una bodega/propietario equivalente.

### 2.4 `EXPLOSION_LIMIT`

**Mensaje típico**: `ERROR: EXPLOSION_LIMIT | Recursion explosion supero nivel max=3`

**Disparado por**: `UMBasExplosionHandler` cuando `recursion_actual > i_nav_config_enc.explosion_automatica_nivel_max`.

**Causa raíz**: producto con jerarquía de presentaciones más profunda que el límite (ej. caja > paquete > unidad > sub-unidad).

**Diagnóstico**:
```sql
SELECT explosio_automatica_nivel_max, explosion_automatica_nivel_max
FROM i_nav_config_enc
WHERE idempresa = @IdEmpresa AND idbodega = @IdBodega AND idPropietario = @IdPropietario;
```

**Acciones**:
- Aumentar `explosion_automatica_nivel_max` (ojo: actualizar AMBAS columnas por el typo, ver archivo 04 §6).
- Validar que la jerarquía de `producto_presentacion` no tenga ciclos.

### 2.5 `CANTIDAD_NEGATIVA_RESULTANTE`

**Mensaje típico**: `ERROR: CANTIDAD_NEGATIVA_RESULTANTE | Stock 12345 quedaria en -0.5 tras reserva`

**Disparado por**: `BaseReservationHandler.ValidateBeforeReserve` cuando `stock.cantidad - reserva_pendiente < 0`.

**Causa raíz**: condición de carrera. Otro proceso reservó stock entre la query de stock candidato y el INSERT en `stock_res`.

**Acción inmediata**:
- Reintentar el motor (idempotente si la primera no persistió).
- Si persiste, revisar locks/aislamiento de la transacción.

**Causa subyacente**:
- Falta de `ROWLOCK, HOLDLOCK` en el `SELECT` de stock candidato.
- O falta de aislamiento `SERIALIZABLE` en la transacción del motor.

### 2.6 `STOCK_ESPECIFICO_NO_DISPONIBLE`

**Mensaje típico**: `ERROR: STOCK_ESPECIFICO_NO_DISPONIBLE | trans_pe_det.IdStockEspecifico=789 no tiene cantidad suficiente`

**Disparado por**: `EntityLoadingStep` cuando `trans_pe_det.IdStockEspecifico IS NOT NULL` y el stock referenciado no alcanza.

**Acción**: revisar si el stock fue consumido por otra reserva o si la cantidad pedida cambió.

### 2.7 `RESERVATION_DUPLICATE`

**Mensaje típico**: `ERROR: RESERVATION_DUPLICATE | Ya existen reservas para IdPedidoDet=456`

**Disparado por**: `EntityLoadingStep` cuando detecta `stock_res WHERE IdPedidoDet = X AND estado != 'CANCELLED'` antes de iniciar.

**Acción**: si era reintento idempotente, devolver el resultado anterior. Si fue invocación errónea, devolver error 409 al cliente.

## 3. Errores de excepción

### 3.1 `EXC: TimeoutSql`

**Mensaje típico**: `EXC: TimeoutSql | clsLnStock.lStock | Timeout 30s | conn=Killios`

**Causa**: query de stock candidato tarda > 30s. Posibles:
- Falta de índice en `stock` por `(IdProductoBodega, IdBodega, activo)`.
- Tabla `stock` muy grande (> 10M filas) sin partition.
- Query mal armada (cartesian product).

**Diagnóstico**:
```sql
-- Verificar índices en stock
SELECT i.name, i.type_desc, c.name AS Columna
FROM sys.indexes i
JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
WHERE i.object_id = OBJECT_ID('stock');

-- Tamaño de la tabla
SELECT SUM(rows) FROM sys.partitions WHERE object_id = OBJECT_ID('stock') AND index_id IN (0,1);
```

**Acciones**:
- Crear índice si falta: `CREATE INDEX IX_stock_lookup ON stock(IdProductoBodega, IdBodega, activo) INCLUDE (cantidad, IdUbicacion, fecha_vence)`.
- Aumentar timeout solo si es genuinamente necesario.
- Considerar archivar stock viejo a `stock_historico`.

### 3.2 `EXC: NullReferenceException`

**Mensaje típico**: `EXC: NullReferenceException | EvaluateClavaudDynamic | obj reference null`

**Causa**: típicamente acceso a `pBeConfigEnc.X` cuando `pBeConfigEnc` quedó null por error en `EntityLoadingStep`.

**Diagnóstico**: ver el `#STEP_LOAD_DONE` correspondiente. Si no apareció, la carga falló silenciosamente.

**Acción**: el motor nuevo debe **fallar fuerte** en `EntityLoadingStep` si una entidad crítica no carga. NUNCA degradar a null silencioso.

### 3.3 `EXC: SqlException · DeadlockVictim`

**Mensaje típico**: `EXC: SqlException | Transaction was deadlocked... | ErrorNumber=1205`

**Causa**: dos motores reservando simultáneamente sobre el mismo stock.

**Acción**: el motor debe **reintentar automáticamente** hasta 3 veces con backoff exponencial (100ms, 200ms, 400ms) cuando detecta `SqlException.Number == 1205`.

### 3.4 `EXC: InvalidOperationException · Connection closed`

**Causa**: el `IDbConnection` se cerró mid-flujo. Típicamente por timeout de conexión o reset de red.

**Acción**: reabrir conexión y reintentar (motor idempotente).

### 3.5 `EXC: OverflowException · Cantidad`

**Mensaje típico**: `EXC: OverflowException | cantidad acumulada excede double | cant=1.7E308`

**Causa**: bug en handler que duplica acumulación.

**Acción**: bisección del flujo (ver últimos `#STEP_HANDLER_X_END` para identificar handler problemático).

## 4. Errores semánticos (paridad legacy/nuevo)

Estos no son "errores" técnicamente pero indican divergencia entre el motor legacy y el nuevo. Detectables solo por **comparación canary**.

| Síntoma                                    | Causa probable                                              | Diagnóstico                                                           |
|--------------------------------------------|------------------------------------------------------------|----------------------------------------------------------------------|
| Distinto orden FEFO                        | Tie-break diferente cuando `fecha_vence` es igual          | Comparar `lBeStockAReservar` orden a orden                           |
| Distinto número de pallets reservados      | Threshold de tolerancia decimal distinto                   | Verificar `Math.Abs(diff) < 0.000001` en ambos                       |
| Distinta activación de explosión           | Lectura de `explosio_*` typo vs `explosion_*` columna      | Auditar `i_nav_config_enc` (archivo 04 §6.3)                         |
| Distinto `MaquinaQueSolicita` persistido   | El nuevo no copia el campo, o lo copia con cast incorrecto | Comparar `stock_res.user_agr` resultante                             |
| Diferencia en `lic_plate` reservado        | Orden diferente cuando hay dos pallets con misma fecha     | Imponer tie-break por `IdStock ASC` para determinismo                |

## 5. Procedimientos de troubleshooting (runbooks)

### 5.1 Runbook: pedido falla con `NO_STOCK` pero el cliente jura que hay stock

**Pasos**:
1. Recuperar logs del pedido (query §6.1 archivo 09).
2. Identificar el último `#CASO_*` antes del `#STEP_FINAL`.
3. Si terminó en `#CASO_3_1` (rechazar incompleto): revisar config `rechazar_pedido_incompleto`.
4. Si terminó en `#CASO_8_1` (reabasto + paletizado sin tarimas): el flag `considerar_paletizado_en_reabasto` está bloqueando. Decidir si aflojarlo.
5. Verificar stock real (queries §2.1).
6. Si hay stock pero está en estado distinto: reportar al equipo de calidad.
7. Si hay stock pero está reservado por pedido viejo: ejecutar limpieza EXPIRED.
8. Si nada anterior, el caso es una condición de carrera o un bug en el motor: escalar.

### 5.2 Runbook: motor lento (latencia p95 > 5s)

**Pasos**:
1. Revisar query de stock candidato (`SET STATISTICS TIME ON`).
2. Revisar tamaño de `lBeStockCandidato` típico (debe ser < 100 filas).
3. Si > 100, ajustar query con `TOP N` y ordenamiento estable.
4. Verificar índices (query §3.1).
5. Verificar que la cadena de handlers se corta apenas se cumple la cantidad (no debería iterar handlers innecesarios).

### 5.3 Runbook: tasa de fallback UMBas anormalmente alta (> 30%)

**Pasos**:
1. Identificar productos afectados (`SELECT Item_No, COUNT(*) FROM log_error_wms WHERE MensajeError LIKE '#FALLBACK_UMBAS%' GROUP BY Item_No ORDER BY 2 DESC`).
2. Verificar si los productos tienen suficiente stock en presentación (cajas) o solo en UMBas (unidades).
3. Si solo hay UMBas: workflow de armado de cajas no está corriendo.
4. Si hay cajas pero el motor las ignora: revisar config `reservar_umbas_primero` (debería ser 0 si se prefiere caja).

### 5.4 Runbook: pedidos colgados sin `#STEP_FINAL`

**Pasos**:
1. Query §6.4 archivo 09 para listarlos.
2. Para cada uno, verificar si la app cliente sigue conectada o si murió.
3. Si la app murió, los pedidos quedan en limbo. Marcarlos como `Failed` manualmente y limpiar `stock_res UNCOMMITED` correspondientes.
4. Si la app sigue corriendo, esperar (puede ser pedido enorme).
5. Si pasaron > 30 min: kill query SQL (`KILL spid`) y reiniciar motor.

### 5.5 Runbook: deadlock recurrente entre dos motores

**Pasos**:
1. Capturar deadlock graph (`SET TRACEFLAG 1222`).
2. Identificar las dos transacciones que chocan.
3. Verificar si ambas están reservando sobre el mismo `IdProductoBodega + IdBodega`.
4. Implementar **lock ordering**: ambas transacciones deben tomar locks en el mismo orden (típicamente `IdStock ASC`).
5. Reducir aislamiento si es posible (de SERIALIZABLE a READ COMMITTED SNAPSHOT).
6. Aumentar reintentos del motor si los deadlocks son raros y aceptables.

## 6. Casos de soporte conocidos (postmortem-ready)

### 6.1 "Pedido X no se puede procesar después de cancelar"

**Síntoma**: usuario crea pedido, lo cancela, lo recrea, y el segundo intento falla con `RESERVATION_DUPLICATE`.

**Causa**: la cancelación no marca todas las reservas como `CANCELLED`. Quedan `UNCOMMITED` huérfanas.

**Solución**: en el flujo de cancelación, ejecutar `UPDATE stock_res SET estado = 'CANCELLED', fec_mod = GETDATE() WHERE IdPedidoEnc = @IdPedidoEnc AND estado IN ('UNCOMMITED', 'COMMITED')`.

**Prevención**: trigger en `trans_pe_enc` que al cambiar `estado` a 'CANCELADO' actualice automáticamente las reservas.

### 6.2 "Cliente recibe distintas cantidades en repeticiones del mismo pedido"

**Síntoma**: el cliente reenvía el mismo pedido (mismo `no_documento_externo`) y obtiene cantidades distintas.

**Causa**: stock fluctuó entre las dos invocaciones (otro pedido consumió en el medio).

**Solución**: implementar idempotencia por `no_documento_externo`. Si ya existe `trans_pe_enc` con ese no, devolver el original.

### 6.3 "Lote vencido se reservó incorrectamente"

**Síntoma**: el motor reserva stock cuyo `fecha_vence < GETDATE()`.

**Causa**: `BaseReservationHandler.IsExpired` no se aplicó (handler nuevo no implementa la regla).

**Solución**: validar `IsExpired(stock, dias_vida_defecto_perecederos)` en TODOS los handlers, no solo en algunos.

### 6.4 "Reserva con `lic_plate` que no está en la bodega"

**Síntoma**: `stock_res.lic_plate = 'LP-789'` pero ese pallet ya fue movido o despachado.

**Causa**: el motor leyó stock con `IdUbicacion_anterior` y lo reservó sobre la ubicación vieja.

**Solución**: query de stock candidato debe filtrar `WHERE s.activo = 1 AND s.cantidad > 0` y rechazar stock con `IdUbicacion = NULL`.

### 6.5 "Explosión activa pero no encuentra UMBas"

**Síntoma**: `#CASO_6_*` se activa pero termina sin encontrar UMBas suficientes.

**Causa**: la presentación a explotar no tiene `producto_presentacion.cant_umbas` correcta.

**Solución**: validar catálogo de presentaciones. Bug típico: `cant_umbas = 0` o `NULL`.

## 7. Tabla de cross-reference codepoint → error → archivo

| Codepoint legacy/nuevo | Error posible              | Documento detalle                  |
|------------------------|---------------------------|-----------------------------------|
| `#CASO_1_1`            | `INVALID_INPUT`, `CONFIG_INVALID` | 06 §EntityLoadingStep         |
| `#CASO_2_*`            | `CANTIDAD_NEGATIVA_RESULTANTE` | 06 §IterateCandidates         |
| `#CASO_3_1`            | `NO_STOCK` (con flag rechazar) | 06 §PostProcessingStep        |
| `#CASO_4_*`            | `EXPLOSION_LIMIT` (en sub-handler) | 05 §Clavaud                |
| `#CASO_5_1`            | (informativo, no error directo) | 06 §CompletePackagesHandler  |
| `#CASO_6_*`            | `EXPLOSION_LIMIT`         | 06 §UMBasExplosionHandler         |
| `#CASO_7_*`            | `NO_STOCK` final           | 06 §ZonaPickingHandler            |
| `#CASO_8_1`            | `NO_STOCK` (legacy MessageBox) | 06 §IncompletePackagesHandler |
| `#FALLBACK_UMBAS`      | (degradación, no error)   | 09 §3.2                            |
| `#RECURSION_LIMIT`     | `EXPLOSION_LIMIT`         | 04 §6 (typo)                       |
| `#STEP_FINAL Failed`   | Cualquiera de los anteriores | 09 §6.6                         |

---

> Próximo: `11-mi3-tests.md` documenta la estrategia de testing del motor MI3, los 3 niveles (unit, integration, E2E canary), fixtures necesarios y el approach de regression testing contra el legacy.
