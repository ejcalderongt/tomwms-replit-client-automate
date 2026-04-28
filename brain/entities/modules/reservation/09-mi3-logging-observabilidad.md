# 09 · Logging y observabilidad del motor MI3

> **Propósito**: documentar el contrato `IReservationLogger`, los handlers de logging tanto del motor nuevo como del legacy, el formato de los checkpoints, cómo se persisten en `log_error_wms` (15 cols) y queries SQL para reconstruir el flujo completo de un pedido a partir de los logs.
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md`, `02-mi3-motor-legacy-vb.md` §5 (`LogError`), `06-mi3-handlers-detalle.md` (vocabulario de checkpoints), `08-mi3-tablas-killios.md` §6 (`log_error_wms`).

---

## Índice

1. Contrato `IReservationLogger` (motor nuevo .NET 8)
2. Métodos de logging del legacy (`LogError`, `Inserta_Pista_Reserva`)
3. Vocabulario completo de checkpoints (`#CASO_*`, `#STEP_*`, `#FALLBACK_*`)
4. Mapeo IReservationLogger ⟶ `log_error_wms`
5. Riesgos de truncamiento (2500 chars) y mitigaciones
6. Queries SQL para reconstruir flujo de un pedido
7. Métricas operativas derivables del log
8. Checklist de observabilidad mínima en producción

---

## 1. Contrato `IReservationLogger` (motor nuevo .NET 8)

### 1.1 Interfaz

```csharp
public interface IReservationLogger
{
    void LogInfo(string codepoint, string message, object payload = null);
    void LogCheckpoint(string checkpoint, ReservationContext context);
    void LogReservation(BeStock_res reservation, string action);
    void LogError(string codepoint, string message, ReservationContext context, Exception ex = null);
    void LogException(Exception ex, ReservationContext context);
}
```

### 1.2 Métodos · semántica esperada

| Método              | Cuándo se invoca                                       | Persistencia              |
|---------------------|-------------------------------------------------------|---------------------------|
| `LogInfo`           | Cualquier evento informativo no asociado a checkpoint específico (carga de entidades, decisiones intermedias). | `log_error_wms` con prefijo `INFO:` en `MensajeError`. |
| `LogCheckpoint`     | Cada checkpoint del vocabulario `#CASO_*` o `#STEP_*`. Punto de control determinista del flujo. | `log_error_wms` con `MensajeError = "{checkpoint} | {context.Resumen}"`. |
| `LogReservation`    | Cada vez que se agrega/modifica un `BeStock_res` en `lBeStockAReservar`. | `log_error_wms` con `MensajeError = "RES: {action} | IdStock={..} | Cant={..} | LP={..}"`. |
| `LogError`          | Error funcional (no excepción): violación de regla, no encontrado, etc. | `log_error_wms` con `MensajeError = "ERROR: {codepoint} | {message}"`. |
| `LogException`      | Excepción capturada con stack trace. | `log_error_wms` con `MensajeError = "EXC: {ex.GetType().Name} | {ex.Message} | {ex.StackTrace.FirstNLines}"`. |

### 1.3 `ReservationContext` (estructura mínima)

```csharp
public sealed class ReservationContext
{
    public int IdEmpresa { get; init; }
    public int IdBodega { get; init; }
    public int IdPedidoEnc { get; init; }
    public int IdPedidoDet { get; init; }
    public int IdProductoBodega { get; init; }
    public int IdPropietarioBodega { get; init; }
    public int IdUsuarioAgr { get; init; }
    public string MaquinaQueSolicita { get; init; }
    public double CantidadSolicitada { get; init; }
    public double CantidadAcumulada { get; set; } // Mutable
    public string ChainStep { get; set; }         // Handler activo
    public Stopwatch Elapsed { get; init; }
    // ... otros campos
}
```

> **Implicación**: cada llamada a `Log*` puede serializar el `ReservationContext` completo. Esto es útil para trazabilidad pero peligroso por el límite de 2500 chars del `MensajeError` (ver §5).

### 1.4 Implementaciones concretas (recomendadas)

| Implementación               | Uso                                            | Estado      |
|------------------------------|-----------------------------------------------|-------------|
| `WmsErrorTableLogger`        | Producción. Persiste en `log_error_wms`.       | A construir |
| `ConsoleReservationLogger`   | Desarrollo local. Imprime a stdout.            | A construir |
| `CompositeReservationLogger` | Producción + dev (combina ambos).              | A construir |
| `NullReservationLogger`      | Tests donde no se quiere log.                  | A construir |
| `InMemoryReservationLogger`  | Tests con assertions sobre lo loggeado.        | A construir |

> **Nota**: el motor nuevo recibe `IReservationLogger` por DI. La fachada de compatibilidad (`Inserta_Stock_Res_MI3`) inyecta un `WmsErrorTableLogger` por defecto, que escribe en la misma tabla que el legacy.

## 2. Métodos de logging del legacy

### 2.1 `LogError` (modulo `clsLnUtil` o similar)

```vb
Public Sub LogError(ByVal MensajeError As String,
                    ByVal IdEmpresa As Integer,
                    ByVal IdBodega As Integer,
                    Optional ByVal IdPedidoEnc As Integer = 0,
                    Optional ByVal IdPickingEnc As Integer = 0,
                    Optional ByVal IdRecepcionEnc As Integer = 0,
                    Optional ByVal IdUsuarioAgr As Integer = 0,
                    Optional ByVal Line_No As Integer = 0,
                    Optional ByVal Item_No As String = "",
                    Optional ByVal UmBas As String = "",
                    Optional ByVal Variant_Code As String = "",
                    Optional ByVal Cantidad As Double = 0,
                    Optional ByVal Referencia_Documento As String = "")
    ' INSERT INTO log_error_wms (...)
End Sub
```

### 2.2 Llamadas desde `Inserta_Stock_Res_MI3`

| Línea legacy | Codepoint loggeado                                  | Datos enviados                          |
|--------------|----------------------------------------------------|----------------------------------------|
| L172         | `"#CASO_1_1: Inicio reserva pedido X linea Y"`     | `IdPedidoEnc, IdPedidoDet, Cantidad`   |
| L245         | `"#CASO_2_1: Iteracion stock candidato"`           | `IdStock, lic_plate, fecha_vence`      |
| L283         | `"#CASO_3_1: rechazar_pedido_incompleto = SI"`     | `IdPedidoEnc, PendingQty`              |
| L868         | `"#CASO_4_1: Clavaud activo, evaluando paquetes"`  | `IdProductoBodega`                     |
| L1273        | `"#CASO_5_1: Conservar zona picking activado"`     | `Conservar_Zona_Picking_Clavaud=1`     |
| L1320-1333   | `"#CASO_6_X: Explosion automatica"`                | `nivel, IdUbicacionOrigen`             |
| L1904        | `"#CASO_7_1: Iteracion ZP"`                        | `IdUbicacion zona picking`             |
| L1922-1936   | `"#CASO_8_1: Reabasto + paletizado"`               | `MessageBox + Exit Function (legacy)`  |
| L2712        | `"#FALLBACK_UMBAS: convertir presentacion a UMBas"`| `Cantidad_UMBas, equivalencia`         |
| L8059        | `"#RECURSION_1965: marker no_bulto = 1965"`        | `IdStock, motivo`                      |
| L8108        | `"#STEP_FINAL: total reservado vs solicitado"`     | `Cant_Reservada, Cant_Solicitada, status` |

> **Vocabulario consistente entre legacy y nuevo**: el motor nuevo debe emitir EXACTAMENTE los mismos checkpoints (`#CASO_X_Y`, `#FALLBACK_*`, `#STEP_*`) para que las queries de auditoría existentes sigan funcionando.

### 2.3 `Inserta_Pista_Reserva` (legacy auxiliar)

Función que persiste un INSERT puntual en `trans_pe_det_log_reserva` con:
- `IdPedidoDet`, `IdStock`, `Cantidad`, `IdUbicacion`, `lote`, `lic_plate`, `UmBas`, `fec_agr`, `user_agr`.

> Tabla `trans_pe_det_log_reserva` (no documentada en archivo 08 — agregar a `sql-catalog/reservation-tables.md` en proximo ciclo). Es la **bitácora granular de reservas** y complementa `log_error_wms`. Permite reconstruir QUÉ stock se reservó para QUÉ línea de pedido en QUÉ momento, incluso si la reserva se canceló después.

## 3. Vocabulario completo de checkpoints

### 3.1 `#CASO_*_*` · ramas funcionales del algoritmo

| Codepoint           | Descripción                                                        | Handler nuevo                          |
|---------------------|-------------------------------------------------------------------|----------------------------------------|
| `#CASO_1_*`         | Carga inicial y validación del pedido                             | `EntityLoadingStep`                    |
| `#CASO_2_*`         | Iteración sobre stock candidato (FEFO)                            | `BaseReservationHandler.IterateCandidates` |
| `#CASO_3_*`         | Decisión de rechazar pedido incompleto                            | `PostProcessingStep.DetermineFinalStatus` |
| `#CASO_4_*`         | Activación de Clavaud (evaluación de paquetes)                    | `EvaluateClavaudDynamic`               |
| `#CASO_5_*`         | Conservar zona picking — saltar pallets completos en ZP           | `CompletePackagesHandler.CanProcess`   |
| `#CASO_6_*`         | Explosión automática (presentación → UMBas)                       | `UMBasExplosionHandler`                |
| `#CASO_7_*`         | Iteración sobre zona picking (último recurso)                     | `ZonaPickingHandler`                   |
| `#CASO_8_*`         | Reabasto con paletizado considerado                               | `IncompletePackagesHandler`            |

### 3.2 `#FALLBACK_*` · activación de modos de respaldo

| Codepoint              | Descripción                                                    |
|------------------------|--------------------------------------------------------------- |
| `#FALLBACK_UMBAS`      | Conversión a UMBas activada porque presentación no alcanza     |
| `#FALLBACK_EXPLOSION`  | Explosión activada por flag + cantidad pendiente              |
| `#FALLBACK_CLAVAUD`    | Salto a flujo Clavaud porque modo principal falló              |
| `#FALLBACK_ZP`         | Caída a zona picking como último recurso                       |

### 3.3 `#STEP_*` · puntos de control determinista

| Codepoint            | Descripción                                                    |
|----------------------|---------------------------------------------------------------|
| `#STEP_INICIO`       | Entrada al motor (input recibido y validado)                   |
| `#STEP_LOAD_DONE`    | Entidades cargadas (producto, propietario, config, stock)      |
| `#STEP_QUERY_DONE`   | Stock candidato consultado y ordenado                          |
| `#STEP_HANDLER_X`    | Inicio de handler X de la cadena                               |
| `#STEP_HANDLER_X_END`| Fin de handler X con resultado (`Continue` / `Stop`)           |
| `#STEP_RESERVE`      | Reserva persistida en memoria (`lBeStockAReservar.Add`)        |
| `#STEP_PERSIST`      | Reservas persistidas en BD (`stock_res INSERT`)                |
| `#STEP_FINAL`        | Final del flujo con status (`Success` / `Partial` / `Failed`)  |

### 3.4 `#RECURSION_*` · markers especiales

| Codepoint                | Descripción                                                |
|--------------------------|-----------------------------------------------------------|
| `#RECURSION_1965`        | Stock generado por la recursión de UMBas (marker `no_bulto = 1965` en BD) |
| `#RECURSION_NIVEL_X`     | Profundidad actual de recursión en explosión              |
| `#RECURSION_LIMIT`       | Recursión cortada por `explosion_automatica_nivel_max`    |

## 4. Mapeo `IReservationLogger` ⟶ `log_error_wms`

| Campo BD (`log_error_wms`) | Origen del valor                                       |
|----------------------------|-------------------------------------------------------|
| `IdError`                  | IDENTITY (auto)                                        |
| `IdEmpresa`                | `context.IdEmpresa`                                    |
| `IdBodega`                 | `context.IdBodega`                                     |
| `Fecha`                    | `DateTime.Now`                                         |
| `MensajeError`             | `"{prefix}{checkpoint} | {message} | {payload}"` truncado a 2500 |
| `IdPedidoEnc`              | `context.IdPedidoEnc`                                  |
| `IdPickingEnc`             | 0 (no aplica en motor de reserva)                      |
| `IdRecepcionEnc`           | 0                                                       |
| `IdUsuarioAgr`             | `context.IdUsuarioAgr`                                 |
| `Line_No`                  | `context.IdPedidoDet` (sí, abuso del campo)            |
| `Item_No`                  | Código producto si está en contexto                    |
| `UmBas`                    | UMBas si está en contexto                              |
| `Variant_Code`             | `atributo_variante_1` si está                          |
| `Cantidad`                 | `context.CantidadSolicitada` o `cant_iteracion`        |
| `Referencia_Documento`     | `context.MaquinaQueSolicita` o documento externo       |

> **Importante**: `Line_No` se reusa como `IdPedidoDet`. No es un cambio de schema, es una **convención del motor** documentada acá. Cualquier query que filtre por `Line_No` debe entender este abuso.

## 5. Riesgos de truncamiento (2500 chars) y mitigaciones

### 5.1 Problema

Si `LogCheckpoint` serializa el `ReservationContext` completo + payload + stack trace, fácilmente excede 2500 chars y se trunca en mitad de un campo. Esto rompe parsers downstream.

### 5.2 Mitigaciones recomendadas

1. **Truncar deterministamente**: si el mensaje pasa de 2400 chars, truncar y agregar sufijo `"...[TRUNCATED:N]"` donde N = chars perdidos.
2. **Resumir contexto**: el helper `context.Resumen` debe devolver máximo 500 chars con campos críticos:
   ```
   "P=123 D=456 Pb=78 Cant=10/3.5 Step=Clavaud T=120ms"
   ```
3. **Stack trace abreviado**: solo las primeras 3 líneas del stack en `LogException`.
4. **Payload JSON**: serializar payload custom como JSON compacto (`JsonSerializer.Serialize(payload, options)` con `WriteIndented = false`).
5. **Secundario en otra tabla**: para casos donde se requiere el contexto completo, persistir el JSON en `trans_pe_det_log_reserva` (que tiene espacio) y referenciarlo desde `MensajeError` con un ID corto.

### 5.3 Código sugerido para `WmsErrorTableLogger`

```csharp
private const int MAX_LEN = 2400;

private string Truncate(string msg)
{
    if (string.IsNullOrEmpty(msg)) return msg;
    if (msg.Length <= MAX_LEN) return msg;
    return msg.Substring(0, MAX_LEN) + $"...[TRUNCATED:{msg.Length - MAX_LEN}]";
}

public void LogCheckpoint(string checkpoint, ReservationContext ctx)
{
    var raw = $"{checkpoint} | {ctx.Resumen()}";
    Insert(Truncate(raw), ctx);
}
```

## 6. Queries SQL para reconstruir flujo de un pedido

### 6.1 Línea de tiempo completa

```sql
-- Todos los logs de un pedido, ordenados temporalmente
SELECT
    Fecha,
    LEFT(MensajeError, 200) AS Resumen,
    Line_No        AS IdPedidoDet,
    Item_No,
    Cantidad,
    Referencia_Documento
FROM log_error_wms
WHERE IdPedidoEnc = @IdPedido
ORDER BY Fecha, IdError;
```

### 6.2 Solo checkpoints clave (sin INFOs ni excepciones)

```sql
-- Filtrar por vocabulario #CASO_*, #STEP_*, #FALLBACK_*
SELECT
    Fecha, MensajeError
FROM log_error_wms
WHERE IdPedidoEnc = @IdPedido
  AND (MensajeError LIKE '#CASO[_]%'
    OR MensajeError LIKE '#STEP[_]%'
    OR MensajeError LIKE '#FALLBACK[_]%'
    OR MensajeError LIKE '#RECURSION[_]%')
ORDER BY Fecha, IdError;
```

### 6.3 Comparativo legacy vs nuevo (mismo pedido procesado por ambos motores)

```sql
-- Si el pedido fue procesado dos veces (canary), comparar checkpoints
WITH Legacy AS (
    SELECT MensajeError FROM log_error_wms
    WHERE IdPedidoEnc = @IdPedido
      AND user_agr LIKE '%LEGACY%'
), Nuevo AS (
    SELECT MensajeError FROM log_error_wms
    WHERE IdPedidoEnc = @IdPedido
      AND user_agr LIKE '%NEWMI3%'
)
SELECT
    'Solo en Legacy' AS Origen, MensajeError FROM Legacy
    EXCEPT SELECT MensajeError FROM Nuevo
UNION ALL
SELECT
    'Solo en Nuevo' AS Origen, MensajeError FROM Nuevo
    EXCEPT SELECT MensajeError FROM Legacy;
```

> Nota: `log_error_wms.user_agr` no existe; el campo de auditoría se infiere de `IdUsuarioAgr`. La comparación canary requiere convención adicional (ej. crear usuarios `legacy_canary` y `new_canary`).

### 6.4 Detectar pedidos colgados (`#STEP_INICIO` sin `#STEP_FINAL`)

```sql
-- Pedidos que arrancaron pero no terminaron en últimas 4h
SELECT
    a.IdPedidoEnc,
    MIN(a.Fecha) AS Inicio,
    DATEDIFF(MINUTE, MIN(a.Fecha), GETDATE()) AS MinutosAtorado
FROM log_error_wms a
WHERE a.MensajeError LIKE '#STEP_INICIO%'
  AND a.Fecha > DATEADD(HOUR, -4, GETDATE())
  AND NOT EXISTS (
      SELECT 1 FROM log_error_wms b
      WHERE b.IdPedidoEnc = a.IdPedidoEnc
        AND b.MensajeError LIKE '#STEP_FINAL%'
        AND b.Fecha >= a.Fecha
  )
GROUP BY a.IdPedidoEnc
ORDER BY MinutosAtorado DESC;
```

### 6.5 Distribución de fallbacks usados

```sql
-- Cuántas veces se activó cada fallback en últimas 24h
SELECT
    CASE
        WHEN MensajeError LIKE '#FALLBACK_UMBAS%' THEN 'UMBAS'
        WHEN MensajeError LIKE '#FALLBACK_EXPLOSION%' THEN 'EXPLOSION'
        WHEN MensajeError LIKE '#FALLBACK_CLAVAUD%' THEN 'CLAVAUD'
        WHEN MensajeError LIKE '#FALLBACK_ZP%' THEN 'ZONA_PICKING'
    END AS Fallback,
    COUNT(*) AS Veces,
    COUNT(DISTINCT IdPedidoEnc) AS PedidosAfectados
FROM log_error_wms
WHERE MensajeError LIKE '#FALLBACK[_]%'
  AND Fecha > DATEADD(HOUR, -24, GETDATE())
GROUP BY
    CASE
        WHEN MensajeError LIKE '#FALLBACK_UMBAS%' THEN 'UMBAS'
        WHEN MensajeError LIKE '#FALLBACK_EXPLOSION%' THEN 'EXPLOSION'
        WHEN MensajeError LIKE '#FALLBACK_CLAVAUD%' THEN 'CLAVAUD'
        WHEN MensajeError LIKE '#FALLBACK_ZP%' THEN 'ZONA_PICKING'
    END
ORDER BY Veces DESC;
```

### 6.6 Pedidos que terminaron en `Failed` con razón

```sql
-- Pedidos con #STEP_FINAL = Failed y el último mensaje significativo
SELECT
    f.IdPedidoEnc,
    f.Fecha,
    f.MensajeError AS RazonFallo,
    p.no_documento_externo,
    p.estado AS EstadoActualPedido
FROM log_error_wms f
LEFT JOIN trans_pe_enc p ON p.IdPedidoEnc = f.IdPedidoEnc
WHERE f.MensajeError LIKE '#STEP_FINAL%Failed%'
  AND f.Fecha > DATEADD(HOUR, -24, GETDATE())
ORDER BY f.Fecha DESC;
```

## 7. Métricas operativas derivables del log

### 7.1 KPIs primarios

| Métrica                            | Cómo calcular                                            |
|------------------------------------|---------------------------------------------------------|
| **Throughput**                     | `COUNT(DISTINCT IdPedidoEnc) WHERE #STEP_FINAL en ventana` |
| **Tasa de éxito**                  | `COUNT(STEP_FINAL Success) / COUNT(STEP_INICIO) en ventana` |
| **Tasa de fallback**               | `COUNT(#FALLBACK_*) / COUNT(STEP_INICIO) en ventana`     |
| **Latencia p50/p95**               | Diferencia `STEP_FINAL.Fecha - STEP_INICIO.Fecha` por pedido |
| **Pedidos colgados**               | Query §6.4                                               |
| **Tasa de explosión activa**       | `COUNT(#CASO_6_*) / COUNT(STEP_INICIO)`                  |
| **Tasa de Clavaud activo**         | `COUNT(#CASO_4_*) / COUNT(STEP_INICIO)`                  |

### 7.2 KPIs secundarios (calidad)

| Métrica                            | Cómo calcular                                            |
|------------------------------------|---------------------------------------------------------|
| **Cantidad media reservada por pedido** | `AVG(SUM(stock_res.cantidad) GROUP BY IdPedidoEnc)`  |
| **% pedidos con > 1 fallback**     | `COUNT(pedidos con 2+ #FALLBACK_*) / COUNT(pedidos)`    |
| **% recursión 1965**               | `COUNT(#RECURSION_1965) / COUNT(STEP_INICIO)`           |
| **% truncamientos**                | `COUNT(MensajeError LIKE '%TRUNCATED:%') / COUNT(*)`     |

### 7.3 Dashboard operativo (sugerencia)

Vista materializada `vw_mi3_metrics_hourly`:

```sql
CREATE VIEW vw_mi3_metrics_hourly AS
SELECT
    DATEADD(HOUR, DATEDIFF(HOUR, 0, Fecha), 0) AS Hora,
    SUM(CASE WHEN MensajeError LIKE '#STEP_INICIO%' THEN 1 ELSE 0 END) AS Inicios,
    SUM(CASE WHEN MensajeError LIKE '#STEP_FINAL%Success%' THEN 1 ELSE 0 END) AS Exitos,
    SUM(CASE WHEN MensajeError LIKE '#STEP_FINAL%Failed%' THEN 1 ELSE 0 END) AS Fallos,
    SUM(CASE WHEN MensajeError LIKE '#STEP_FINAL%Partial%' THEN 1 ELSE 0 END) AS Parciales,
    SUM(CASE WHEN MensajeError LIKE '#FALLBACK[_]%' THEN 1 ELSE 0 END) AS Fallbacks,
    SUM(CASE WHEN MensajeError LIKE 'EXC:%' THEN 1 ELSE 0 END) AS Excepciones
FROM log_error_wms
WHERE Fecha > DATEADD(DAY, -7, GETDATE())
GROUP BY DATEADD(HOUR, DATEDIFF(HOUR, 0, Fecha), 0);
```

> **Nota**: como Killios es READ-ONLY desde wms-brain, esta vista se crearía en una BD réplica o en TOMWMS_BOF, no en producción Killios.

## 8. Checklist de observabilidad mínima en producción

Antes de desplegar el motor MI3 nuevo en producción, verificar:

- [ ] **Implementación `WmsErrorTableLogger` activa** (no `NullReservationLogger`).
- [ ] **Truncamiento determinista** (sufijo `[TRUNCATED:N]` en mensajes >2400 chars).
- [ ] **Vocabulario `#CASO_*`, `#STEP_*`, `#FALLBACK_*`, `#RECURSION_*` consistente con legacy** (validar con query §6.3).
- [ ] **Cada handler emite checkpoint de inicio Y fin** (`#STEP_HANDLER_X` y `#STEP_HANDLER_X_END`).
- [ ] **`PostProcessingStep` siempre emite `#STEP_FINAL`** incluso si hay excepción (envolver en try/finally).
- [ ] **`LogException` captura stack trace de las primeras 3 líneas mínimo**.
- [ ] **Métricas hourly disponibles** (vista `vw_mi3_metrics_hourly` o equivalente).
- [ ] **Alarma para pedidos colgados** (query §6.4 corriendo cada 15 min).
- [ ] **Alarma para tasa de fallos > 5%** (sobre ventana de 1h).
- [ ] **Reglas de retención**: `log_error_wms` puede crecer rápido. Definir política de purga (ej. mover a tabla histórica los registros > 90 días).

---

> Próximo: `10-mi3-errores-troubleshooting.md` documenta el catálogo de errores conocidos del motor MI3, sus causas y procedimientos de troubleshooting paso a paso.
