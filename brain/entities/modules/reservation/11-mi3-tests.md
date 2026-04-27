# 11 · Estrategia de testing del motor MI3

> **Propósito**: definir la estrategia de testing del motor MI3 reescrito (.NET 8) para garantizar paridad funcional con el legacy VB.NET (8K líneas) antes de habilitarlo en producción. Cubre niveles unit/integration/E2E, fixtures, datasets canary, criterios de paso y plan de rollout.
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md` (arquitectura), `03-comparison.md` (delta legacy/nuevo), `06-mi3-handlers-detalle.md` (handlers a testear), `09-mi3-logging-observabilidad.md` (assertions sobre checkpoints).

---

## Índice

1. Pirámide de tests
2. Nivel 1 — Unit tests por handler
3. Nivel 2 — Integration tests del flujo completo
4. Nivel 3 — Regression canary contra legacy
5. Fixtures y datasets requeridos
6. Tests no funcionales (rendimiento, concurrencia)
7. Criterios de paso para producción
8. Plan de rollout (canary → blue/green → full)

---

## 1. Pirámide de tests

```
                          /\
                         /  \
                        / E2E\        ←  Regression canary
                       / Test \           contra legacy
                      /────────\
                     /          \
                    /Integration \   ←  Flujo completo
                   /     Tests    \      con BD réplica
                  /────────────────\
                 /                  \
                /     Unit Tests     \  ←  Por handler
               /                      \    con mocks
              /────────────────────────\
```

| Nivel        | Cantidad esperada | Velocidad | Cubre                            |
|--------------|-------------------|-----------|----------------------------------|
| Unit         | ~80 tests         | < 5s total| Lógica de cada handler aislada   |
| Integration  | ~25 tests         | ~30s total| Cadena completa con BD efímera   |
| Canary E2E   | continuo          | en prod   | Paridad legacy vs nuevo en vivo  |

## 2. Nivel 1 — Unit tests por handler

### 2.1 Convención de nombres

```
ReservationEngine.Tests/
├── Handlers/
│   ├── EntityLoadingStepTests.cs
│   ├── RequestValidationStepTests.cs
│   ├── StockQueryStepTests.cs
│   ├── ReservationLoopStepTests.cs
│   ├── DefaultReservationHandlerTests.cs
│   ├── CompletePackagesHandlerTests.cs
│   ├── IncompletePackagesHandlerTests.cs
│   ├── UMBasExplosionHandlerTests.cs
│   ├── ZonaPickingHandlerTests.cs
│   └── PostProcessingStepTests.cs
└── Domain/
    ├── BeStock_resTests.cs
    ├── ReservationContextTests.cs
    └── ReservationStatusTests.cs
```

### 2.2 Patrón de test típico

```csharp
[Test]
public async Task DefaultReservationHandler_StockSuficiente_ReservaTodaCantidad()
{
    // Arrange
    var stock = new[]
    {
        new BeStock { IdStock = 1, cantidad = 10, fecha_vence = DateTime.Now.AddMonths(6), lote = "LOTE-A" },
        new BeStock { IdStock = 2, cantidad = 5,  fecha_vence = DateTime.Now.AddMonths(3), lote = "LOTE-B" }
    };
    var context = new ReservationContext
    {
        CantidadSolicitada = 8,
        IdProductoBodega = 100,
        // ...
    };
    var handler = new DefaultReservationHandler(new NullReservationLogger());

    // Act
    var result = await handler.Process(stock, context);

    // Assert
    Assert.That(context.CantidadAcumulada, Is.EqualTo(8));
    Assert.That(result.Reservations, Has.Count.EqualTo(1));
    Assert.That(result.Reservations[0].IdStock, Is.EqualTo(2)); // FEFO: LOTE-B vence antes
    Assert.That(result.Reservations[0].cantidad, Is.EqualTo(5));
    // Y la siguiente del LOTE-A:
    Assert.That(result.Reservations[1].cantidad, Is.EqualTo(3));
}
```

### 2.3 Casos de test mínimos por handler

#### `EntityLoadingStep`
- Carga exitosa con todas las entidades presentes.
- `i_nav_config_enc` no encontrado → `CONFIG_INVALID`.
- `IdProductoBodega` no existe → `INVALID_INPUT`.
- `propietario_bodega` inactivo → error específico.
- Reserva duplicada existente → `RESERVATION_DUPLICATE`.

#### `RequestValidationStep`
- `CantidadSolicitada <= 0` → `INVALID_INPUT`.
- `IdPedidoDet = 0` → `INVALID_INPUT`.
- Cantidad con > 6 decimales → trunca o rechaza según política.

#### `StockQueryStep`
- Devuelve solo `activo = 1`.
- Devuelve solo `cantidad > 0`.
- Excluye ubicaciones con `excluir_ubicaciones_reabasto = 1` cuando `pTarea_Reabasto = True`.
- Ordena por `fecha_vence ASC` (FEFO).
- Tie-break por `IdStock ASC` cuando `fecha_vence` es igual.

#### `ReservationLoopStep`
- Termina cuando `CantidadAcumulada >= CantidadSolicitada`.
- Itera todos los handlers en orden.
- Activa fallback UMBas cuando handler principal devuelve `Continue` y queda pendiente.
- Activa fallback Explosión cuando UMBas tampoco alcanza.

#### `DefaultReservationHandler`
- Reserva en orden FEFO.
- Respeta tolerancia decimal (`< 0.000001` se considera 0).
- Marca `IsExpired` correctamente cuando `fecha_vence < GETDATE() + dias_vida_defecto_perecederos`.

#### `CompletePackagesHandler`
- Solo activa cuando `Conservar_Zona_Picking_Clavaud = 1` y hay stock con `pallet_completo`.
- Reserva pallet entero (no fracciona).
- Salta pallets parciales.

#### `IncompletePackagesHandler`
- Activa cuando hay pallets incompletos en zona picking.
- Si `pTarea_Reabasto = True` + `considerar_paletizado_en_reabasto = 1` y no hay pallets completos → `NO_STOCK`.
- Caso contrario reserva parcialmente.

#### `UMBasExplosionHandler`
- Activa cuando `explosion_automatica = 1` y queda cantidad pendiente.
- Genera registros con `no_bulto = 1965`.
- Respeta `explosion_automatica_nivel_max` (recursión).
- Lee tanto `explosio_*` como `explosion_*` (typo) y usa el primero que no sea NULL.

#### `ZonaPickingHandler`
- Solo se invoca si los handlers anteriores no completaron.
- Solo lee ubicaciones `es_picking = 1`.
- Permite reservas parciales.

#### `PostProcessingStep`
- Si `CantidadAcumulada == CantidadSolicitada` → `Success`.
- Si `0 < CantidadAcumulada < CantidadSolicitada` y `rechazar_pedido_incompleto = 0` → `Partial`.
- Si `rechazar_pedido_incompleto = 1` y queda pendiente → `Failed` con `NO_STOCK`.
- Persiste `lBeStockAReservar` en `stock_res` (mocked en unit, real en integration).
- Emite `#STEP_FINAL` siempre, incluso en excepción.

### 2.4 Mocking strategy

- **`IDbConnection`**: usar `Microsoft.Data.SqlClient.MockDbConnection` o equivalente.
- **`IClsLnStock`**: interface envoltorio sobre `clsLnStock` para mockear queries.
- **`IReservationLogger`**: usar `InMemoryReservationLogger` en unit tests para verificar que se emitieron los checkpoints esperados.

```csharp
// Verificar checkpoints emitidos
var logger = new InMemoryReservationLogger();
// ... ejecutar handler
Assert.That(logger.Checkpoints, Has.Count.GreaterThanOrEqualTo(2));
Assert.That(logger.Checkpoints[0], Does.StartWith("#STEP_HANDLER_"));
Assert.That(logger.Checkpoints[^1], Does.StartWith("#STEP_HANDLER_") & Does.Contain("_END"));
```

## 3. Nivel 2 — Integration tests del flujo completo

### 3.1 Setup

- BD efímera por test (LocalDB o Docker SQL Server).
- Schema completo de las 8 tablas críticas (ver archivo 08).
- Fixture de datos via SQL scripts (`Fixtures/*.sql`).
- Limpieza automática post-test.

### 3.2 Casos de test mínimos

| ID    | Escenario                                                                    | Resultado esperado                                  |
|-------|------------------------------------------------------------------------------|----------------------------------------------------|
| IT-01 | Pedido simple 10 unidades, stock disponible 20                              | `Success`, 1 reserva de 10                         |
| IT-02 | Pedido 30 unidades, stock total 25                                           | `Partial` (sin flag rechazar) o `Failed` (con flag)|
| IT-03 | Pedido 5 unidades, 2 lotes con misma fecha vence                             | FEFO con tie-break por `IdStock`                   |
| IT-04 | Pedido 10 unidades en cajas, solo hay 5 cajas + UMBas suficientes           | Activa explosión, reserva 5 cajas + N UMBas        |
| IT-05 | Pedido con `Conservar_Zona_Picking_Clavaud = 1`, stock en ALM y ZP          | Reserva pallets completos de ALM antes de tocar ZP|
| IT-06 | Pedido reabasto con `considerar_paletizado_en_reabasto = 1`, sin pallets   | `NO_STOCK` con mensaje específico                  |
| IT-07 | Pedido con stock vencido (fecha_vence < hoy)                                | Excluye stock vencido                              |
| IT-08 | Pedido con `IdStockEspecifico = X` y stock X tiene cantidad suficiente      | Reserva solo del stock X                           |
| IT-09 | Pedido con `IdStockEspecifico = X` y stock X insuficiente                  | `STOCK_ESPECIFICO_NO_DISPONIBLE`                   |
| IT-10 | 2 motores reservando concurrente sobre mismo producto                       | Ambos terminan correctamente, sin negativos        |
| IT-11 | Reserva, luego despacho, luego nueva reserva                                | Segunda reserva ve stock decrementado              |
| IT-12 | Pedido con producto que tiene `Reservar_En_UmBas = 1` + `interface_sap = 1` | Fuerza modo UMBas, ignora presentación             |
| IT-13 | Recursión profunda (caja → paquete → unidad → sub-unidad)                  | Respeta `explosion_automatica_nivel_max`           |
| IT-14 | Reserva idempotente (segunda invocación con mismo IdPedidoDet)             | Devuelve `RESERVATION_DUPLICATE` sin duplicar      |
| IT-15 | Cancelación de pedido marca todas las `stock_res` como `CANCELLED`         | Verificar estado en BD                             |

### 3.3 Helpers

```csharp
public static class IntegrationTestHelpers
{
    public static async Task<int> CreateTestPedido(IDbConnection conn, int idBodega, int idPropietarioBodega)
    {
        // INSERT trans_pe_enc + trans_pe_det
        // Retorna IdPedidoEnc
    }

    public static async Task SeedStock(IDbConnection conn, int idProductoBodega, int idBodega,
                                        int cantidad, string lote, DateTime fechaVence)
    {
        // INSERT stock
    }

    public static async Task<List<BeStock_res>> GetReservations(IDbConnection conn, int idPedidoEnc)
    {
        // SELECT * FROM stock_res WHERE IdPedido = @IdPedidoEnc
    }

    public static async Task<List<string>> GetCheckpoints(IDbConnection conn, int idPedidoEnc)
    {
        // SELECT MensajeError FROM log_error_wms WHERE IdPedidoEnc = @IdPedidoEnc AND MensajeError LIKE '#%'
    }
}
```

### 3.4 Aserciones sobre checkpoints

```csharp
[Test]
public async Task IT_05_Clavaud_Activa_Salta_ZP_Hasta_Necesario()
{
    // Arrange: setup con Conservar_Zona_Picking_Clavaud = 1 + stock en ALM y ZP
    var idPedido = await Helpers.CreateTestPedido(conn, idBodega: 1, idPropietarioBodega: 10);
    await Helpers.SeedStock(conn, idProductoBodega: 100, idBodega: 1, cantidad: 5, lote: "ALM-1", ubicacion: "ALM");
    await Helpers.SeedStock(conn, idProductoBodega: 100, idBodega: 1, cantidad: 10, lote: "ZP-1", ubicacion: "ZP");

    // Act
    var result = await engine.Insertar_Stock_Res_MI3(...);

    // Assert
    var checkpoints = await Helpers.GetCheckpoints(conn, idPedido);
    Assert.That(checkpoints, Contains.Item.Matching("#CASO_5_1"));   // Conservar zona picking activado
    Assert.That(checkpoints, Contains.Item.Matching("#STEP_FINAL.*Success"));

    var reservas = await Helpers.GetReservations(conn, idPedido);
    Assert.That(reservas[0].lote, Is.EqualTo("ALM-1"));  // Reserva ALM primero
}
```

## 4. Nivel 3 — Regression canary contra legacy

### 4.1 Estrategia

- **Modo paralelo**: cada request entrante se ejecuta en AMBOS motores (legacy + nuevo).
- **Fuente de verdad**: el legacy persiste real; el nuevo persiste en tablas paralelas (`stock_res_canary`, `log_error_wms_canary`).
- **Comparador**: proceso async que compara los resultados de ambos motores y reporta diferencias.

### 4.2 Setup canary

```csharp
public class CanaryReservationDispatcher
{
    private readonly LegacyReservationEngine _legacy;
    private readonly NewReservationEngine _new;
    private readonly IComparator _comparator;

    public async Task<ReservationResult> Insertar(ReservationRequest req)
    {
        // 1. Ejecuta legacy (fuente de verdad)
        var legacyResult = await _legacy.Insertar_Stock_Res_MI3(req);

        // 2. Ejecuta nuevo en paralelo (no bloqueante)
        _ = Task.Run(async () =>
        {
            try
            {
                var newReq = req.WithCanaryFlag(true);
                var newResult = await _new.Insertar_Stock_Res_MI3(newReq);
                await _comparator.Compare(legacyResult, newResult, req);
            }
            catch (Exception ex)
            {
                await _comparator.LogCanaryFailure(req, ex);
            }
        });

        // 3. Devuelve resultado del legacy al cliente (sin esperar al nuevo)
        return legacyResult;
    }
}
```

### 4.3 Métricas de paridad

| Métrica                                | Tolerancia aceptable |
|----------------------------------------|---------------------|
| % requests con resultado idéntico       | > 99.5%             |
| % diferencias en cantidad reservada     | < 0.1%              |
| % diferencias en orden FEFO             | < 1% (tie-breaks)   |
| % diferencias en `MaquinaQueSolicita`   | 0% (debe ser exacto)|
| % canary que lanzó excepción no esperada| < 0.01%             |

### 4.4 Comparador

```csharp
public interface IComparator
{
    Task Compare(ReservationResult legacy, ReservationResult newRes, ReservationRequest req);
    Task LogCanaryFailure(ReservationRequest req, Exception ex);
}

public class DbBackedComparator : IComparator
{
    public async Task Compare(ReservationResult l, ReservationResult n, ReservationRequest req)
    {
        var diffs = new List<string>();

        if (l.Status != n.Status)
            diffs.Add($"STATUS_DIFF: legacy={l.Status} new={n.Status}");

        if (Math.Abs(l.TotalReservado - n.TotalReservado) > 0.000001)
            diffs.Add($"CANT_DIFF: legacy={l.TotalReservado} new={n.TotalReservado}");

        if (l.Reservations.Count != n.Reservations.Count)
            diffs.Add($"COUNT_DIFF: legacy={l.Reservations.Count} new={n.Reservations.Count}");

        // Comparar reserva a reserva (orden importa para FEFO)
        for (int i = 0; i < Math.Min(l.Reservations.Count, n.Reservations.Count); i++)
        {
            if (l.Reservations[i].IdStock != n.Reservations[i].IdStock)
                diffs.Add($"FEFO_DIFF[{i}]: legacy={l.Reservations[i].IdStock} new={n.Reservations[i].IdStock}");
        }

        if (diffs.Any())
            await PersistDiff(req, diffs);
    }
}
```

### 4.5 Tabla `mi3_canary_diff` (a crear)

```sql
CREATE TABLE mi3_canary_diff (
    IdDiff INT IDENTITY PRIMARY KEY,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    IdPedidoEnc INT,
    IdPedidoDet INT,
    LegacyStatus NVARCHAR(20),
    NewStatus NVARCHAR(20),
    LegacyTotalRes FLOAT,
    NewTotalRes FLOAT,
    DiffsJson NVARCHAR(MAX), -- detalle de diferencias
    RequestJson NVARCHAR(MAX) -- request completo para reproducir
);
```

## 5. Fixtures y datasets requeridos

### 5.1 Fixtures básicos (carpeta `Fixtures/`)

- `seed_propietarios.sql`: 5 propietarios típicos (con/sin Clavaud, con/sin SAP).
- `seed_bodegas.sql`: 3 bodegas (una con SAP, una sin, una con muelle múltiple).
- `seed_productos.sql`: 20 productos con jerarquía de presentaciones (caja > paquete > unidad).
- `seed_stock_basico.sql`: 100 filas de stock en distintos estados, ubicaciones, lotes.
- `seed_config_motor.sql`: filas en `i_nav_config_enc` para cada combinación bodega+propietario.

### 5.2 Datasets canary (de producción)

- Recolectar **muestra anonimizada** de pedidos reales de últimos 30 días.
- Categorizar por escenario (simple, con explosión, con Clavaud, etc.).
- Re-ejecutar contra el motor nuevo en ambiente staging.
- Comparar resultado con el persistido en producción.

### 5.3 Edge cases (carpeta `EdgeCases/`)

- Pedido con cantidad 0.000001 (límite de tolerancia).
- Pedido sobre producto recién creado sin stock.
- Pedido sobre producto con todas las presentaciones agotadas excepto UMBas.
- Pedido con explosión profundidad 5 (más allá del nivel típico 3).
- Pedido con `IdStockEspecifico` apuntando a stock vencido.
- Pedido con cancelación durante el procesamiento (race condition).

## 6. Tests no funcionales

### 6.1 Rendimiento

| Métrica                        | Target              |
|--------------------------------|---------------------|
| Latencia p50 (pedido simple)   | < 100ms             |
| Latencia p95 (pedido con explosión) | < 500ms        |
| Latencia p99                   | < 2s                |
| Throughput sostenido           | 100 reservas/seg    |
| Memoria por reserva            | < 5 MB              |

Herramientas: BenchmarkDotNet para microbench de handlers, k6 o similar para load testing del endpoint REST.

### 6.2 Concurrencia

- 50 motores simultáneos sobre mismos 10 productos: NO debe haber stock negativo.
- Deadlocks: < 1% de transacciones, todas reintentadas correctamente.
- Snapshot isolation: verificar que reservas commiteadas no impactan reservas concurrentes en lectura.

### 6.3 Resilencia

- Killios SQL caído mid-flujo: el motor debe rollback limpio y devolver error claro.
- Timeout de query: el motor debe abortar sin colgarse.
- Conexión perdida durante INSERT: la transacción debe quedar consistente.

## 7. Criterios de paso para producción

Antes de habilitar el motor nuevo en producción:

- [ ] **Cobertura unit tests > 85%** sobre handlers core.
- [ ] **100% de los integration tests pasando** (15 escenarios IT-01 a IT-15).
- [ ] **Canary corriendo > 7 días** con > 99.5% paridad.
- [ ] **Diferencias canary explicadas** (cada `mi3_canary_diff` tiene root cause documentado).
- [ ] **Performance dentro del target** (§6.1).
- [ ] **Concurrencia estable** (§6.2).
- [ ] **Documentación completa** (archivos 01-12 en wms-brain).
- [ ] **Runbooks operativos validados** (archivo 10 §5).
- [ ] **Logging completo y consistente con legacy** (archivo 09 §3).
- [ ] **Plan de rollback documentado y probado**.

## 8. Plan de rollout (canary → blue/green → full)

### Fase 1 — Canary silencioso (semana 1-2)

- Motor nuevo corre paralelo al legacy.
- 100% del tráfico va a legacy.
- Comparator persiste diferencias en `mi3_canary_diff`.
- Análisis diario de diferencias.

### Fase 2 — Canary visible (semana 3)

- 5% del tráfico se enruta al motor nuevo (decisión por hash de `IdPedidoEnc`).
- Resultado del nuevo se persiste real (no canario).
- Monitoreo intensivo: latencia, errores, diferencias contra muestra control.
- Rollback inmediato si tasa de error > 1%.

### Fase 3 — Blue/green (semana 4-5)

- 50% / 50% entre legacy y nuevo.
- Comparación operativa de KPIs (throughput, latencia, tasa éxito).

### Fase 4 — Full (semana 6+)

- 100% al motor nuevo.
- Legacy disponible para rollback inmediato (1 toggle en config).
- Mantener canary inverso (5% al legacy) por 2 semanas más como red de seguridad.

### Rollback strategy

En cualquier fase, el rollback es:
1. Cambiar feature flag `MI3_USE_NEW_ENGINE = false`.
2. Tráfico vuelve 100% al legacy en < 1 minuto.
3. Las reservas pendientes con el nuevo motor se completan o cancelan manualmente.
4. Investigar root cause sin urgencia.

---

> Próximo: `12-mi3-todos-roadmap.md` consolida los TODOs detectados en archivos 01-11, los riesgos abiertos, y el roadmap sugerido de implementación post-documentación.
