# 07 · Ciclo de vida de `stock_res` (estados de la reserva)

> **Propósito**: documentar los estados que puede tomar una fila de `stock_res` desde su creación hasta su consumición o cancelación, las transiciones permitidas y los puntos del sistema que disparan cada cambio. Este ciclo aplica tanto al motor MI3 nuevo como al legacy.
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md` (creación), `06-mi3-handlers-detalle.md` (handler que crea), `sql-catalog/reservation-tables.md` (schema), `02-mi3-motor-legacy-vb.md` (`Inserta_Stock_Reservado` legacy).

---

## Índice

1. Estados posibles de `stock_res.Estado`
2. Diagrama de transiciones
3. Quién transiciona cada estado
4. Tabla `stock_res` — campos clave del ciclo de vida
5. Implicaciones operativas
6. Cómo investigar reservas "huérfanas" o inconsistentes

---

## 1. Estados posibles de `stock_res.Estado`

Validados contra Killios productivo (passada 3-2):

| Valor                | Significado                                                          | Origen              |
|----------------------|----------------------------------------------------------------------|---------------------|
| `UNCOMMITED`         | Reserva creada, stock disponible reducido, pero no confirmada        | Motor MI3 (creación)|
| `COMMITED`           | Reserva confirmada, asociada a una transacción de pedido aprobada    | Aprobación de pedido|
| `CONSUMED`           | Reserva consumida durante el picking (despacho real)                 | Despacho desde HH   |
| `CANCELLED`          | Reserva cancelada (por usuario o por timeout)                        | Cancelación de pedido|
| `EXPIRED`            | Reserva caducada (timeout sin confirmación) — uso poco común         | Job de expiración   |

> **Convención del legacy**: el estado se persiste como string en `varchar(20)`. NO hay tabla de catálogo (`tipos_estado_stock_res`); el código mantiene los strings hardcoded. El motor nuevo usa el enum `ReservationState` con `ToString()` para serializar.

### 1.1 Estado por defecto al crear

```vbnet
' Legacy: Inserta_Stock_Reservado
BeStockRes.Estado = "UNCOMMITED"
```

```csharp
// Nuevo: BaseReservationHandler.CreateReservation
reservation.Estado = ReservationState.UNCOMMITED.ToString();
```

Las reservas siempre nacen `UNCOMMITED`. Solo transicionan tras eventos externos.

---

## 2. Diagrama de transiciones

```
       Motor MI3 (handler)
              │
              ▼
       [UNCOMMITED]  ◄────┐
         │     │     │     │
         │     │     │     │ (cancelación dentro de timeout)
         │     │     │     │
   (aprobación) │  (timeout)
         │     │     │     │
         ▼     │     ▼     │
    [COMMITED]│  [EXPIRED]─┘
         │    │
   (pick HH)  │
         │    │ (cancelación post-aprobación)
         ▼    ▼
    [CONSUMED] [CANCELLED]
```

Transiciones permitidas:

| Desde         | A             | Disparador                                                |
|---------------|---------------|-----------------------------------------------------------|
| `UNCOMMITED`  | `COMMITED`    | Aprobación del pedido (job o usuario en BOF Forms)        |
| `UNCOMMITED`  | `CANCELLED`   | Cancelación manual del pedido                              |
| `UNCOMMITED`  | `EXPIRED`     | Job de expiración tras N horas sin commit                  |
| `COMMITED`    | `CONSUMED`    | Pick desde Handheld (despacho físico)                      |
| `COMMITED`    | `CANCELLED`   | Cancelación post-commit (raro, requiere autorización)      |
| `CANCELLED`   | (terminal)    | —                                                          |
| `CONSUMED`    | (terminal)    | —                                                          |
| `EXPIRED`     | (terminal)    | —                                                          |

---

## 3. Quién transiciona cada estado

### 3.1 Creación (`UNCOMMITED`)

- **Motor MI3 nuevo**: `PostProcessingStep.PersistReservations` ejecuta `INSERT INTO stock_res (..., Estado) VALUES (..., 'UNCOMMITED')`.
- **Motor MI3 legacy**: `Inserta_Stock_Reservado(lBeStockAReservar, ...)` recorre la lista y persiste con el mismo estado.

### 3.2 `UNCOMMITED` → `COMMITED`

Disparado por:

- **Aprobación de pedido en BOF**: `clsLnTrans_pe_enc.Aprobar_Pedido(IdPedido)` actualiza todos los `stock_res` asociados.
- **Job de aprobación automática**: para pedidos MI3 sincronizados desde Killios con flag de auto-commit.

```sql
-- Operación típica
UPDATE stock_res
SET Estado = 'COMMITED', Fecha_Commit = GETDATE(), Usuario_Commit = @Usuario
WHERE IdPedido = @IdPedido AND Estado = 'UNCOMMITED';
```

### 3.3 `COMMITED` → `CONSUMED`

Disparado por:

- **Pick desde Handheld (TOMHH2025)**: cuando el operador escanea el lote y confirma la cantidad picked.
- **Despacho desde BOF Forms**: para pedidos manuales.

```sql
UPDATE stock_res
SET Estado = 'CONSUMED', Fecha_Consumo = GETDATE(), Usuario_Consumo = @Usuario,
    IdTareaPickingDet = @IdTarea
WHERE IdStockRes = @IdStockRes;
```

Tras `CONSUMED`, también se descuenta físicamente del `stock` (no solo del disponible):

```sql
UPDATE stock SET Cantidad = Cantidad - @CantidadConsumida WHERE IdStock = @IdStock;
```

### 3.4 `UNCOMMITED|COMMITED` → `CANCELLED`

Disparado por:

- **Cancelación manual** desde BOF (form cancelación pedidos).
- **Anulación desde Killios**: si Killios anula un pedido MI3 sincronizado, el endpoint `POST /api/sync/salidas/mi3/anular` cancela las reservas asociadas.

```sql
UPDATE stock_res
SET Estado = 'CANCELLED', Fecha_Cancelacion = GETDATE(),
    Usuario_Cancelacion = @Usuario, Motivo_Cancelacion = @Motivo
WHERE IdPedido = @IdPedido AND Estado IN ('UNCOMMITED', 'COMMITED');
```

### 3.5 `UNCOMMITED` → `EXPIRED`

Job programado (típico cada hora) que cancela reservas viejas:

```sql
UPDATE stock_res
SET Estado = 'EXPIRED', Fecha_Cancelacion = GETDATE(),
    Motivo_Cancelacion = 'Auto-expirada tras 24h sin commit'
WHERE Estado = 'UNCOMMITED' AND Fecha_Reserva < DATEADD(HOUR, -24, GETDATE());
```

> El threshold de horas se configura en `propietarios.Horas_Auto_Expiracion_Reserva`. Default 24.

---

## 4. Tabla `stock_res` — campos clave del ciclo de vida

Detalle completo del schema en `sql-catalog/reservation-tables.md`. Acá los campos relevantes para el ciclo:

| Campo                       | Tipo            | Descripción                                              |
|-----------------------------|-----------------|----------------------------------------------------------|
| `IdStockRes`                | int identity    | PK                                                       |
| `IdStock`                   | int FK          | Referencia al stock origen                               |
| `IdPedido`, `IdPedidoDet`   | int FK          | Pedido y línea de pedido                                 |
| `IdTransaccion`             | int             | Identificador de transacción de reserva                 |
| `Cantidad`                  | float           | Cantidad reservada                                       |
| `Estado`                    | varchar(20)     | Ver §1                                                   |
| `Fecha_Reserva`             | datetime        | Cuando se creó (always populated)                        |
| `Fecha_Commit`              | datetime        | Cuando pasó a COMMITED (NULL si aún no)                  |
| `Fecha_Consumo`             | datetime        | Cuando pasó a CONSUMED                                   |
| `Fecha_Cancelacion`         | datetime        | Cuando pasó a CANCELLED o EXPIRED                        |
| `Usuario_Reserva`           | varchar(100)    | `MaquinaQueSolicita` original                            |
| `Usuario_Commit`            | varchar(100)    | Usuario que aprobó                                       |
| `Usuario_Consumo`           | varchar(100)    | Operador HH                                              |
| `Usuario_Cancelacion`       | varchar(100)    | Quien canceló                                            |
| `Motivo_Cancelacion`        | varchar(500)    | Texto libre o código predefinido                         |
| `Indicador`                 | varchar(10)     | "PED" / "TRA" / "MAN" (tipo de transacción origen)       |
| `EsExplosion`               | bit             | Si la reserva vino de explosión                          |
| `EsUMBas`                   | bit             | Si vino del fallback UMBas                               |
| `EsZonaPicking`             | bit             | Si fue reservada desde zona picking                      |
| `IdPresentacionOriginal`    | int             | Para reservas EsUMBas, conserva la presentación pedida   |

---

## 5. Implicaciones operativas

### 5.1 Stock disponible vs Stock físico

Una fila en `stock_res` con estado `UNCOMMITED` o `COMMITED` **reduce el stock disponible** pero **no el stock físico**:

- Stock físico (`stock.Cantidad`): se decrementa solo en `CONSUMED`.
- Stock disponible: `stock.Cantidad - SUM(stock_res.Cantidad WHERE Estado IN ('UNCOMMITED', 'COMMITED'))`.

Esta es la lógica que `Restar_Stock_Reservado` aplica en cada llamada del motor MI3.

### 5.2 Reservas huérfanas

Una "reserva huérfana" es una fila `stock_res` con estado `UNCOMMITED` cuyo pedido (`trans_pe_enc`) ya está cancelado. Causas históricas:

1. **Bug de la persistencia parcial pre-recursión del legacy** (corregido en motor nuevo): el legacy persistía reservas en presentación antes de intentar UMBas. Si la búsqueda UMBas fallaba sin completar, las reservas en presentación quedaban activas.
2. **Job de expiración no ejecutándose**: si el job está caído por días, se acumulan reservas viejas.
3. **Crash del proceso BOF a mitad de pedido**: con TransactionScope esto se mitigó en motor nuevo, pero histórico legacy sí sufría.

### 5.3 Reservas zombi

"Reserva zombi" = `stock_res` con estado `COMMITED` cuya transacción de despacho nunca llegó (por ejemplo, pedido aprobado pero el HH nunca lo procesó).

Detección:

```sql
SELECT sr.*, te.Estado AS EstadoPedido
FROM stock_res sr
JOIN trans_pe_enc te ON te.IdPedidoEnc = sr.IdPedido
WHERE sr.Estado = 'COMMITED'
  AND sr.Fecha_Commit < DATEADD(DAY, -7, GETDATE())
  AND te.Estado NOT IN ('DESPACHADO', 'CANCELADO');
```

Estas requieren intervención manual (cancelación o forzar despacho).

---

## 6. Cómo investigar reservas problemáticas

### 6.1 Reservas inconsistentes con su pedido

```sql
-- Reservas de un pedido específico con su estado y transiciones
SELECT
    sr.IdStockRes, sr.IdStock, sr.Cantidad, sr.Estado,
    sr.Fecha_Reserva, sr.Fecha_Commit, sr.Fecha_Consumo, sr.Fecha_Cancelacion,
    sr.Indicador, sr.EsExplosion, sr.EsUMBas, sr.EsZonaPicking,
    s.Cantidad AS StockFisicoActual,
    p.Codigo AS Producto,
    pe.Estado AS EstadoPedido
FROM stock_res sr
JOIN stock s ON s.IdStock = sr.IdStock
JOIN productos p ON p.IdProducto = s.IdProducto
JOIN trans_pe_enc pe ON pe.IdPedidoEnc = sr.IdPedido
WHERE sr.IdPedido = @IdPedido
ORDER BY sr.Fecha_Reserva;
```

### 6.2 Trazabilidad cruzada con `log_error_wms`

```sql
-- Logs del motor MI3 para una transacción
SELECT *
FROM log_error_wms
WHERE Mensaje LIKE '%IdTransaccion=' + CAST(@IdTrans AS VARCHAR) + '%'
   OR Mensaje LIKE '%IdPedido=' + CAST(@IdPedido AS VARCHAR) + '%'
ORDER BY Fecha;
```

Buscar checkpoints como `#CASO_*_RESERVED <IdStock>` para correlacionar con `stock_res.IdStock`.

### 6.3 Volumen por estado (dashboard de salud)

```sql
SELECT
    Estado,
    COUNT(*) AS Cantidad,
    MIN(Fecha_Reserva) AS MasAntigua,
    MAX(Fecha_Reserva) AS MasReciente
FROM stock_res
WHERE Fecha_Reserva > DATEADD(DAY, -30, GETDATE())
GROUP BY Estado
ORDER BY Cantidad DESC;
```

> Recordatorio: **Killios productivo es READ-ONLY** desde este wms-brain (regla 08). Estas queries son solo para investigación. Modificaciones se hacen por canal autorizado del DBA.

---

> Próximo: `08-mi3-tablas-killios.md` documenta el schema completo de las 5 tablas críticas (`stock`, `stock_res`, `trans_pe_det`, `i_nav_ped_traslado_det`, `log_error_wms`) y sus FKs cruzadas, con tipos validados contra Killios productivo.
