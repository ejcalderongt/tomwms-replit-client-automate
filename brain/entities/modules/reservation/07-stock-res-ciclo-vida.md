# 07 · Ciclo de vida de `stock_res` y máquina de estados

> **CORRECCIÓN respecto a la versión anterior**: la versión 1 de este documento atribuía a `stock_res` columnas de auditoría como `Fecha_Commit`, `Fecha_Consumo`, `Fecha_Cancelacion`, `Motivo_Cancelacion`, `Usuario_Commit`, `Usuario_Consumo`, `Usuario_Cancelacion`, `EsExplosion`, `EsUMBas`, `EsZonaPicking`, `IdPresentacionOriginal`. **NINGUNA de esas columnas existe** en `stock_res`. La tabla real tiene 35 columnas validadas live contra Killios productivo (ver `08-mi3-tablas-killios.md` §2).
>
> Esta versión documenta el ciclo de vida con las **columnas reales** y explica cómo se infieren las transiciones desde la información disponible (cruzando con `trans_pe_enc.estado` y `trans_pe_enc.fec_mod`).
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md`, `06-mi3-handlers-detalle.md` (handlers que crean reservas), `08-mi3-tablas-killios.md` §2 (schema real `stock_res`), `09-mi3-logging-observabilidad.md` (cómo reconstruir transiciones desde logs).

---

## Índice

1. Estados posibles del campo `stock_res.estado`
2. Diagrama de máquina de estados
3. Transiciones · disparadores y persistencia
4. Limitaciones del modelo (auditoría incompleta)
5. Cómo inferir transiciones históricas
6. Reglas de invariantes
7. Operaciones de mantenimiento (limpieza de huérfanos)

---

## 1. Estados posibles del campo `stock_res.estado`

`stock_res.estado` es `nvarchar(20) NULL` (sin FK a catálogo). Los valores observados en producción y usados por el motor:

| Estado         | Significado                                              | Quién lo escribe                          |
|----------------|---------------------------------------------------------|------------------------------------------|
| `UNCOMMITED`   | Reserva creada por el motor, pero el pedido aún no fue aprobado. Es la primera transición desde NULL. | Motor MI3 (`PostProcessingStep.PersistReservations`) |
| `COMMITED`     | Pedido aprobado. La reserva queda firme.                | Flujo de aprobación de pedido (no motor MI3) |
| `CONSUMED`    | Stock fue efectivamente despachado. La reserva se consume y `stock.cantidad` se decrementa. | Flujo de despacho |
| `CANCELLED`    | Reserva cancelada (por cancelación de pedido o por limpieza manual). | Flujo de cancelación o limpieza |
| `EXPIRED`      | Reserva quedó huérfana (UNCOMMITED por > N días sin aprobación). | Job de limpieza programado |

> **Importante**: la columna **acepta cualquier string** porque no tiene FK ni CHECK constraint. Cualquier typo (`UNCOMITED`, `Commited`, `cancelled` minúscula) sería persistido sin validación. Riesgo abierto a documentar.

## 2. Diagrama de máquina de estados

```
                  ┌──────────────────────────┐
                  │ Motor MI3 crea reserva   │
                  │ PostProcessingStep        │
                  └─────────────┬─────────────┘
                                │
                                ▼
                       ╔════════════════╗
                       ║  UNCOMMITED    ║
                       ╚════════════════╝
                          │           │
                          │           │
        Pedido aprobado   │           │   Pedido cancelado
        (flujo aprobación)│           │   o expiración (job)
                          ▼           ▼
              ╔════════════════╗  ╔════════════════╗
              ║  COMMITED      ║  ║  CANCELLED     ║
              ╚════════════════╝  ║   o EXPIRED    ║
                          │       ╚════════════════╝
                          │
        Despacho ejecutado│
        (flujo despacho)  │
                          ▼
                ╔════════════════╗
                ║  CONSUMED      ║◄── Estado terminal
                ╚════════════════╝
```

> **Estados terminales**: `CONSUMED`, `CANCELLED`, `EXPIRED`. No salen a otro estado.
> **Transiciones inversas**: NO hay. Un `COMMITED` no puede volver a `UNCOMMITED`. Si se cancela, va a `CANCELLED`.

## 3. Transiciones · disparadores y persistencia

### 3.1 NULL → `UNCOMMITED` (creación por motor MI3)

**Disparador**: `Insertar_Stock_Res_MI3` en motor nuevo (o `Inserta_Stock_Res_MI3` en legacy).

**Persistencia**: 1 INSERT por cada `BeStock_res` en `lBeStockAReservar`.

**Columnas escritas**:
```sql
INSERT INTO stock_res (
    IdTransaccion, Indicador, IdPedidoDet, IdStock,
    IdPropietarioBodega, IdProductoBodega, IdProductoEstado,
    IdPresentacion, IdUnidadMedida, IdUbicacion,
    lote, lic_plate, serial, cantidad, peso,
    estado, fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto,
    user_agr, fec_agr, IdBodega, fecha_manufactura, añada,
    pallet_no_estandar
) VALUES (
    @IdTransaccion, @Indicador, @IdPedidoDet, @IdStock,
    @IdPropietarioBodega, @IdProductoBodega, @IdProductoEstado,
    @IdPresentacion, @IdUnidadMedida, @IdUbicacion,
    @lote, @lic_plate, @serial, @cantidad, @peso,
    'UNCOMMITED', @fecha_ingreso, @fecha_vence, @uds_lic_plate, @no_bulto,
    @MaquinaQueSolicita, GETDATE(), @IdBodega, @fecha_manufactura, @añada,
    @pallet_no_estandar
);
```

**Notas**:
- `user_agr` recibe `MaquinaQueSolicita` (hostname de la HH o nombre del servicio).
- `fec_agr = GETDATE()` (no se permite override).
- `user_mod = user_agr` y `fec_mod = fec_agr` inicialmente.

### 3.2 `UNCOMMITED` → `COMMITED` (aprobación de pedido)

**Disparador**: `trans_pe_enc.estado` cambia a 'APROBADO' o equivalente.

**Persistencia**: 1 UPDATE por cada reserva del pedido.

```sql
UPDATE stock_res
SET estado = 'COMMITED',
    user_mod = @UsuarioAprobador,
    fec_mod = GETDATE()
WHERE IdPedidoEnc = @IdPedidoEnc -- vía JOIN con trans_pe_det
  AND estado = 'UNCOMMITED';
```

> **Nota**: `stock_res` no tiene `IdPedidoEnc` directo. Hay que JOIN con `trans_pe_det.IdPedidoEnc` para identificar las reservas del pedido. Excepción: el campo `IdPedido bigint` en `stock_res` (sí, redundante) puede ser usado como atajo si el flujo lo populó.

### 3.3 `COMMITED` → `CONSUMED` (despacho)

**Disparador**: ejecución del despacho (`clsLnDespacho.Ejecutar` o equivalente).

**Persistencia**:
1. UPDATE `stock_res.estado = 'CONSUMED'`.
2. UPDATE `stock.cantidad -= reserva.cantidad`.
3. INSERT en `trans_de_*` (detalle de despacho).
4. UPDATE `trans_pe_enc.estado = 'DESPACHADO'`.

```sql
-- 1. Reserva consumida
UPDATE stock_res
SET estado = 'CONSUMED',
    IdDespacho = @IdDespacho,
    user_mod = @UsuarioDespacho,
    fec_mod = GETDATE()
WHERE IdStockRes = @IdStockRes;

-- 2. Stock decrementado (transaccional con la anterior)
UPDATE stock
SET cantidad = cantidad - @CantidadConsumida,
    user_mod = @UsuarioDespacho,
    fec_mod = GETDATE()
WHERE IdStock = @IdStock;
```

> **Invariante crítico**: las dos UPDATE deben estar en una sola transacción. Si falla la 2da, rollback completo.

### 3.4 `UNCOMMITED` o `COMMITED` → `CANCELLED` (cancelación)

**Disparador**:
- Cancelación manual de pedido en BOF.
- Anulación desde la HH.
- Limpieza programada cuando un pedido queda en limbo.

**Persistencia**:
```sql
UPDATE stock_res
SET estado = 'CANCELLED',
    user_mod = @UsuarioCancelacion,
    fec_mod = GETDATE()
WHERE IdPedidoDet IN (
    SELECT IdPedidoDet FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc
)
  AND estado IN ('UNCOMMITED', 'COMMITED');
```

> **Importante**: la cancelación NO modifica `stock.cantidad` (porque la reserva nunca se materializó). Solo libera la reserva.

### 3.5 `UNCOMMITED` → `EXPIRED` (limpieza programada)

**Disparador**: job nocturno o programado.

**Política sugerida**: reservas `UNCOMMITED` con `fec_agr < (hoy - 7 días)` → `EXPIRED`.

```sql
UPDATE stock_res
SET estado = 'EXPIRED',
    user_mod = 'JOB_LIMPIEZA',
    fec_mod = GETDATE()
WHERE estado = 'UNCOMMITED'
  AND fec_agr < DATEADD(DAY, -7, GETDATE());
```

> **Política a definir con Erik**: 7 días es sugerido. Puede ser distinto por bodega.

## 4. Limitaciones del modelo (auditoría incompleta)

### 4.1 No hay historial de transiciones

El campo `estado` se sobrescribe en cada cambio. No existe tabla `stock_res_history` ni columnas `Fecha_Commit`/`Fecha_Consumo` separadas.

**Consecuencia**: para saber **cuándo** una reserva pasó de UNCOMMITED a COMMITED, hay que cruzar con:
- `trans_pe_enc.fec_mod` (cambio del pedido) — aproximación.
- `log_error_wms` filtrado por checkpoints relacionados.
- Audit log de aplicación si existe.

### 4.2 No hay motivo de cancelación en `stock_res`

`stock_res` no tiene `Motivo_Cancelacion`. El motivo está en `trans_pe_enc.IdMotivoAnulacionBodega`.

### 4.3 No hay marker de explosión / UMBas / Zona Picking

`stock_res` no tiene `EsExplosion`, `EsUMBas`, `EsZonaPicking`. Estos atributos solo viven en los logs (`log_error_wms` con checkpoints `#CASO_6_*`, `#FALLBACK_UMBAS`, `#CASO_7_*`).

**Único marker físico**: `stock_res.no_bulto = 1965` indica que la reserva fue generada por la recursión legacy de UMBas (heredado del marker `stock.no_bulto = 1965`).

### 4.4 No hay `IdPresentacionOriginal`

Si la reserva es resultado de explosión (caja → unidades), `stock_res.IdPresentacion` ya es la presentación reservada (unidad), no la pedida (caja). No hay forma directa de saber qué presentación pidió originalmente el cliente.

**Workaround**: cruzar con `trans_pe_det.IdPresentacion` que tiene la presentación pedida.

### 4.5 `user_mod` se sobrescribe

Solo queda registrada la última modificación en `user_mod` + `fec_mod`. No se sabe quién hizo cada transición intermedia.

## 5. Cómo inferir transiciones históricas

### 5.1 Línea de tiempo aproximada de una reserva

```sql
-- Reconstruir línea de tiempo de IdStockRes = @X
SELECT
    sr.IdStockRes,
    sr.estado AS EstadoActual,
    sr.fec_agr AS Cuando_Creada,
    sr.user_agr AS Quien_Creo,
    sr.fec_mod AS Ultima_Modificacion,
    sr.user_mod AS Ultimo_Modificador,
    pe.estado AS EstadoPedido,
    pe.fec_mod AS UltimoCambioPedido,
    pe.IdMotivoAnulacionBodega AS MotivoAnulacion,
    -- Inferencia: si pedido cambió después de fec_agr de reserva,
    -- probablemente fue cuando pasó a COMMITED o CANCELLED
    CASE
        WHEN sr.estado = 'COMMITED' THEN 'Aprobacion: ~' + CONVERT(NVARCHAR, pe.fec_mod, 120)
        WHEN sr.estado = 'CANCELLED' THEN 'Cancelacion: ~' + CONVERT(NVARCHAR, pe.fec_mod, 120) + ' Motivo:' + CONVERT(NVARCHAR, pe.IdMotivoAnulacionBodega)
        WHEN sr.estado = 'CONSUMED' THEN 'Despacho: ~' + CONVERT(NVARCHAR, sr.fec_mod, 120)
    END AS Inferencia
FROM stock_res sr
JOIN trans_pe_det pd ON pd.IdPedidoDet = sr.IdPedidoDet
JOIN trans_pe_enc pe ON pe.IdPedidoEnc = pd.IdPedidoEnc
WHERE sr.IdStockRes = @IdStockRes;
```

### 5.2 Cruce con logs para precisión

```sql
-- Logs del pedido relacionados con la reserva
SELECT Fecha, MensajeError
FROM log_error_wms
WHERE IdPedidoEnc = (SELECT IdPedidoEnc FROM trans_pe_det WHERE IdPedidoDet =
                      (SELECT IdPedidoDet FROM stock_res WHERE IdStockRes = @IdStockRes))
  AND (MensajeError LIKE '%RES:%' OR MensajeError LIKE '#%')
ORDER BY Fecha;
```

### 5.3 Estado actual vs esperado

```sql
-- Reservas que llevan demasiado tiempo en UNCOMMITED
SELECT IdStockRes, IdPedidoDet, fec_agr, DATEDIFF(HOUR, fec_agr, GETDATE()) AS HorasEnLimbo
FROM stock_res
WHERE estado = 'UNCOMMITED'
  AND fec_agr < DATEADD(DAY, -3, GETDATE())
ORDER BY fec_agr;

-- Reservas COMMITED de pedidos ya despachados (debería ser CONSUMED)
SELECT sr.IdStockRes, sr.estado AS EstadoReserva, pe.estado AS EstadoPedido, sr.fec_mod, pe.fec_mod
FROM stock_res sr
JOIN trans_pe_det pd ON pd.IdPedidoDet = sr.IdPedidoDet
JOIN trans_pe_enc pe ON pe.IdPedidoEnc = pd.IdPedidoEnc
WHERE sr.estado = 'COMMITED'
  AND pe.estado = 'DESPACHADO'
  AND pe.fec_mod < DATEADD(DAY, -1, GETDATE());
```

## 6. Reglas de invariantes

### 6.1 Invariantes que el motor MI3 debe respetar

1. **Total reservado ≤ stock disponible**:
   ```sql
   SELECT IdStock, stock.cantidad,
          (SELECT SUM(cantidad) FROM stock_res
           WHERE IdStock = stock.IdStock AND estado IN ('UNCOMMITED','COMMITED')) AS TotalReservado
   FROM stock
   WHERE stock.activo = 1
     AND stock.cantidad < (SELECT SUM(cantidad) FROM stock_res
                          WHERE IdStock = stock.IdStock AND estado IN ('UNCOMMITED','COMMITED'));
   ```
   Resultado esperado: 0 filas. Si > 0, hay invariante violado (over-reservation).

2. **Toda reserva CONSUMED tiene `IdDespacho NOT NULL`**:
   ```sql
   SELECT COUNT(*) FROM stock_res WHERE estado = 'CONSUMED' AND IdDespacho IS NULL;
   ```
   Resultado esperado: 0. Si > 0, despacho mal procesado.

3. **Toda reserva CANCELLED tiene su pedido en estado CANCELADO o ANULADO**:
   ```sql
   SELECT sr.IdStockRes, sr.estado AS Reserva, pe.estado AS Pedido
   FROM stock_res sr
   JOIN trans_pe_det pd ON pd.IdPedidoDet = sr.IdPedidoDet
   JOIN trans_pe_enc pe ON pe.IdPedidoEnc = pd.IdPedidoEnc
   WHERE sr.estado = 'CANCELLED'
     AND pe.estado NOT IN ('CANCELADO', 'ANULADO');
   ```
   Excepción aceptable: cancelaciones manuales por limpieza.

4. **Reservas con `no_bulto = 1965` son siempre presentación UMBas**:
   ```sql
   SELECT IdStockRes, IdPresentacion, no_bulto
   FROM stock_res
   WHERE no_bulto = 1965
     AND IdPresentacion IS NOT NULL
     AND IdPresentacion != (SELECT IdUMBas FROM producto_bodega WHERE IdProductoBodega = stock_res.IdProductoBodega);
   ```
   Resultado esperado: 0. Si > 0, marker mal aplicado.

### 6.2 Invariantes a NO violar manualmente

- NUNCA hacer `UPDATE stock_res SET cantidad = X WHERE ...`. La cantidad reservada es atómica con el stock origen.
- NUNCA hacer `DELETE FROM stock_res`. Solo `UPDATE estado`.
- NUNCA cambiar `IdStock` de una reserva existente. Si el stock origen se mueve, crear nueva reserva y cancelar la vieja.

## 7. Operaciones de mantenimiento

### 7.1 Job nocturno: limpieza de UNCOMMITED viejas

```sql
-- Job: marcar UNCOMMITED > 7 días como EXPIRED
UPDATE stock_res
SET estado = 'EXPIRED',
    user_mod = 'JOB_LIMPIEZA',
    fec_mod = GETDATE()
WHERE estado = 'UNCOMMITED'
  AND fec_agr < DATEADD(DAY, -7, GETDATE());
```

### 7.2 Job semanal: detección de inconsistencias

Ejecutar las 4 queries de invariantes (§6.1). Si alguna devuelve > 0, alertar al equipo de operaciones.

### 7.3 Detección de huérfanos

```sql
-- Reservas cuyo pedido fue eliminado (no debería pasar pero por si acaso)
SELECT sr.*
FROM stock_res sr
WHERE NOT EXISTS (
    SELECT 1 FROM trans_pe_det pd WHERE pd.IdPedidoDet = sr.IdPedidoDet
);
```

### 7.4 Compactación / archivado

`stock_res` puede crecer rápido. Política sugerida:

```sql
-- Mover reservas terminales (CANCELLED, EXPIRED, CONSUMED) > 90 días a tabla histórica
-- (definir tabla stock_res_historico con mismo schema + flag EsHistorico)

INSERT INTO stock_res_historico (...)
SELECT ... FROM stock_res
WHERE estado IN ('CANCELLED', 'EXPIRED', 'CONSUMED')
  AND fec_mod < DATEADD(DAY, -90, GETDATE());

DELETE FROM stock_res
WHERE estado IN ('CANCELLED', 'EXPIRED', 'CONSUMED')
  AND fec_mod < DATEADD(DAY, -90, GETDATE());
```

> **Importante**: ejecutar SOLO en réplica o en horario de baja actividad. Killios productivo es READ-ONLY desde wms-brain — esta operación se hace por canal autorizado del DBA.

---

> Próximo: `09-mi3-logging-observabilidad.md` documenta el contrato `IReservationLogger` y cómo reconstruir el flujo completo de un pedido a partir de los checkpoints.
