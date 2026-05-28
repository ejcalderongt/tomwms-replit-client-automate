---
tipo: other
---
# Patron: movimientos VERI en picking BOF (idempotencia + diagnostico)

> Origen: handoff inverso
> `2026-05-19-codex-learning-bof-veri-movimientos-duplicados/PROPOSAL.md`
> (Codex local 2026-05-19, caso `IdPickingEnc = 1628`).
>
> Promovido por Replit como Brain Keeper. Las 4 preguntas abiertas del
> PROPOSAL quedan al final con marca `[ ]` para resolucion con Erik antes de
> escribir el handoff de fix.

## Resumen

Las rutinas BOF que insertan movimientos de verificacion (`trans_movimientos`
con `IdTipoTarea = 11`, tipo `VERI`) **no son idempotentes hoy**. Si desde el
BOF se re-ejecuta "verificar pickeados" sobre un picking que ya tiene
movimientos `VERI`, el sistema vuelve a insertar el set logico completo,
generando multiplicadores exactos (2x, 3x, etc.) sobre `trans_movimientos`.

Caso confirmado: `IdPickingEnc = 1628`, factor `504/168 = 3`, exceso 336.

> **Actualizacion 2026-05-19 (commit Brain Keeper):** ver §"Patron 2 — VERI
> en presentacion vs UMBAS" mas abajo. El caso `1465` confirmo un **segundo
> bug independiente** en el mismo flujo: VERI guardada en presentacion en
> vez de UMBAS, lo que viola la regla §6 de `replit.md` (familia A).

## Modelo de datos del flujo VERI

```
trans_picking_enc                          (cabecera picking)
   IdPickingEnc PK
   │
   │ 1..N
   ▼
trans_picking_ubic                         (lineas operativas con cant verificada)
   IdPickingUbic PK, IdPickingEnc FK
   IdPedidoEnc, IdPedidoDet, IdProductoBodega
   Lic_plate, Lote, Fecha_Vence, IdRecepcion
   Cantidad_Solicitada, Cantidad_Recibida, Cantidad_Verificada

trans_movimientos                          (movimientos de stock)
   IdMovimiento PK
   IdTipoTarea = 11      → VERI
   IdTransaccion         = trans_picking_enc.IdPickingEnc
   IdPedidoEnc, IdPedidoDet
   IdRecepcion, IdRecepcionDet
   IdProductoBodega
   IdUbicacionOrigen, IdUbicacionDestino
   IdPresentacion
   IdEstadoOrigen, IdEstadoDestino
   IdUnidadMedida
   barra_pallet (= Lic_plate), lote, fecha_vence
   cantidad

tarea_hh                                   (cola HH; NO usada para VERI)
   IdTipoTarea = 8       → PIK (picking HH)
   IdTransaccion         = IdPickingEnc
   No se observa IdTipoTarea VERI en tarea_hh para este caso.
```

## Llave logica del movimiento VERI (idempotencia)

Un movimiento VERI esta "logicamente repetido" si coincide en TODA esta llave:

```
IdTipoTarea           (= 11 fijo)
IdTransaccion         (= IdPickingEnc)
IdPedidoEnc
IdPedidoDet
IdRecepcion
IdRecepcionDet
IdProductoBodega
IdUbicacionOrigen
IdUbicacionDestino
IdPresentacion
IdEstadoOrigen
IdEstadoDestino
IdUnidadMedida
barra_pallet
lote
fecha_vence
cantidad
```

Esta llave es la base de la guarda de idempotencia que falta y del SELECT de
diagnostico.

## Flujo de codigo BOF afectado

```
TOMIMSV4/TOMIMSV4/Transacciones/Picking/frmPicking.vb
  mnuVerificarPickeados_ItemClick        ← entry point BOF
  Linea_No_Pickeada
  Linea_No_Verificada
  Verificar_Nuevamente

TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb
  Procesar_Verificacion_Desde_BOF        ← inserta VERI sin guarda (Patron 1)
                                           ademas envia Cantidad_Recibida directa
                                           sin convertir a UMBAS (Patron 2)
  Marcar_Linea_No_Pickeada
  Marcar_Linea_No_Verificada             ← elimina VERI solo si Verificacion_Auto=True
  Actualiza_Cant_Peso_Verificacion       ← flujo HH; mas conservador

TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb
  Insertar_Movimiento_Verificacion       ← punto natural para guarda
  Eliminar_Movimiento_Verificacion_By_PickingUbic
                                           ← usa cantidad del PIK para localizar
                                             el VERI a borrar (gotcha post-Patron 2)
```

Hallazgos clave:
- `Procesar_Verificacion_Desde_BOF` recorre lineas con `Cantidad_Recibida > 0`,
  asigna `Cantidad_Verificada = Cantidad_Recibida` e invoca
  `Insertar_Movimiento_Verificacion(...)` sin verificar movimientos previos
  ni convertir presentacion → UMBAS.
- `Actualiza_Cant_Peso_Verificacion` (flujo HH) filtra pendientes con
  `(Cantidad_Recibida - Cantidad_Verificada) <> 0.0` — por eso HH no genera
  duplicados en la misma forma.
- `Marcar_Linea_No_Verificada` solo limpia VERI cuando `Verificacion_Auto = True`.
  Si la verificacion fue manual/BOF, las VERI previas sobreviven y el
  siguiente "verificar pickeados" las duplica.

## Hipotesis operativa (Patron 1: duplicados exactos)

Duplicacion ocurre cuando una linea/picking ya tiene movimientos VERI y desde
BOF se ejecuta nuevamente "verificar pickeados" (o secuencia "no verificado" +
"verificar pickeados") sin limpiar ni saltar los movimientos previos.

## Patron 2 — VERI en presentacion vs UMBAS (caso 1465, confirmado por cliente)

Origen: handoff inverso commits `567287f / 9073061 / a256cb7` (Codex
2026-05-19), validado contra archivo cliente `RevisionVerificacionDoble.xlsx`.

### Sintoma

`trans_movimientos.cantidad` queda en cantidad de presentacion en vez de
UMBAS, violando la regla §6 de `replit.md` (familia A: `stock`, `movimientos`,
`stock_res` siempre UMBAS).

### Causa raiz

`Procesar_Verificacion_Desde_BOF` envia `Cantidad_Recibida` directo a
`trans_movimientos.cantidad`. Si la pantalla operaba en presentacion (y
`IdPresentacion > 0`), `Cantidad_Recibida` viene en presentacion. La insercion
NO convierte a UMBAS multiplicando por `Factor`.

### Caso confirmado (picking 1465, producto 440662, lote `L0114:1625312`,
licencia `TEA4406620021237`, presentacion CAJA24 factor 24)

| Fuente | Cantidad presentacion | Cantidad UMBAS |
|---|---:|---:|
| `trans_picking_ubic` (esperado) | 72 | 1728 |
| `trans_movimientos` VERI (real) | 144 | 3456 |
| Exceso | 72 (2x) | 1728 (2x por Patron 1) |

Hay dos bugs apilados:
- Patron 1: duplicacion (mov `288682+288706` y `288683+288707`, factor 2x).
- Patron 2: los 5 movimientos unicos restantes (`288676, 288678, 288679, 288680,
  288681`) tienen `cantidad = 72` cuando deberian ser `1728`.

### Llave de deteccion Patron 2

```sql
-- VERI candidato a corregir cantidad UMBAS si TODO lo siguiente se cumple:
-- 1. Es VERI unico para la llave logica (no duplicado).
-- 2. Tiene IdPresentacion > 0 con Factor > 1.
-- 3. La llave logica matchea exactamente 1 linea trans_picking_ubic con
--    Cantidad_Verificada > 0.
-- 4. trans_movimientos.cantidad == SUM(trans_picking_ubic.Cantidad_Verificada).
-- 5. El PIK del mismo grupo tiene cantidad = X * Factor (confirma UMBAS esperado).
-- → cantidad_corregida = cantidad_actual * Factor
```

### Gotcha cruzado con Patron 1

`Eliminar_Movimiento_Verificacion_By_PickingUbic` usa la cantidad del
movimiento `PIK` para localizar el `VERI` a borrar. Si un VERI historico
quedo con cantidad en presentacion, el delete por BOF puede NO encontrarlo.
Conclusion: para historicos hay que **regularizar UMBAS primero** y despues
validar la limpieza por BOF.

### Cruce con Patron BYB-reserva-MI3-UMBAS

Este Patron 2 es la **misma familia conceptual** que el bug detectado en
`Reserva_Stock_From_MI3` (caso BYB EA-153305, handoff `2026-05-19-codex-
learning-byb-mi3-reserva-umbas`). En ambos casos una variable nombrada
UMBAS/Cantidad acumula valor en presentacion sin re-anclar a la unidad base
canonica antes de persistir a familia A. Ver
`code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md` y meta-patron en
`code-changes/HH/PATTERNS-UMBAS.md` §"Violaciones BOF confirmadas".

## Recomendaciones para el fix (estado: parche local aplicado por Codex)

1. **Idempotencia en `Insertar_Movimiento_Verificacion`**: antes de insertar,
   verificar que NO exista un movimiento VERI con la llave logica completa.
   Es la defensa minima. → **APLICADO LOCAL por Codex (sin commit a Azure)**.
2. **Decision explicita en `Procesar_Verificacion_Desde_BOF`**: el flujo debe
   elegir UNA de estas tres estrategias y dejarla documentada:
   - solo procesa lineas pendientes (skip si ya verificadas);
   - elimina + reconstruye VERI previos del picking/grupo;
   - conserva VERI previos y bloquea reinserciones duplicadas.
   → **Codex eligio (c)**: conserva + bloquea por idempotencia.
3. **Desacoplar limpieza VERI de `Verificacion_Auto`**: `Marcar_Linea_No_Verificada`
   debe poder limpiar VERI tambien en flujo manual/BOF si la regla de negocio
   asi lo indica. → **APLICADO LOCAL por Codex**.
4. **Conversion a UMBAS antes de insertar VERI** (Patron 2): si
   `IdPresentacion > 0` y `Factor > 1`, multiplicar `Cantidad_Recibida * Factor`
   antes de asignar a `trans_movimientos.cantidad`. → **APLICADO LOCAL por
   Codex en `clsLnTrans_movimientos_Partial.vb`**.
5. **Para regularizacion historica**, separar 3 categorias y atacar por
   separado:
   - duplicado exacto depurable;
   - movimiento huerfano sin `trans_picking_ubic` equivalente;
   - mismatch de cantidad/llave que requiere revision funcional.
   → **APLICADO**: ver `code-changes/BOF/SP-REGULARIZACION-VERI.md`.

## Stored procedures de regularizacion (Codex 2026-05-19)

Tres SPs creados en BD local (no productivos todavia):

- `dbo.usp_WMS_VERI_RegularizarDuplicadosExactos` — Patron 1.
- `dbo.usp_WMS_VERI_RegularizarCantidadUmbas` — Patron 2.
- `dbo.usp_WMS_VERI_PostCheck` — validacion post-fix.

Tres modos de ejecucion (`@EjecutarDelete/@EjecutarUpdate` + `@ConfirmarCommit`):
auditoria solo lectura / simulacion con ROLLBACK / aplicacion con COMMIT.
Bloqueo explicito `@ProcesarTodos = 1` requerido para alcance global.

Detalle completo en `code-changes/BOF/SP-REGULARIZACION-VERI.md`.

## Query de diagnostico (SELECT-only)

Por `IdPickingEnc`, agrupa lineas `trans_picking_ubic` y movimientos VERI por
llave compartida y reporta diferencia + factor de duplicacion.

```sql
DECLARE @IdPickingEnc INT = 1628;

WITH PickingGrupo AS (
    SELECT
        pu.IdPickingEnc, pu.IdPedidoEnc, pu.IdPedidoDet,
        pd.no_linea AS NoLineaPedido, pd.Cantidad AS CantidadPedido,
        pu.IdProductoBodega, pu.Lic_plate, pu.Lote, pu.Fecha_Vence,
        pu.IdRecepcion,
        COUNT(*) AS LineasPickingUbic,
        MIN(pu.IdPickingUbic) AS PrimerIdPickingUbic,
        MAX(pu.IdPickingUbic) AS UltimoIdPickingUbic,
        SUM(ISNULL(pu.Cantidad_Solicitada, 0)) AS CantidadSolicitadaPickingUbic,
        SUM(ISNULL(pu.Cantidad_Recibida,   0)) AS CantidadRecibidaPickingUbic,
        SUM(ISNULL(pu.Cantidad_Verificada, 0)) AS CantidadVerificadaPickingUbic
    FROM trans_picking_ubic pu
    LEFT JOIN trans_pe_det pd
        ON pd.IdPedidoEnc = pu.IdPedidoEnc
       AND pd.IdPedidoDet = pu.IdPedidoDet
    WHERE pu.IdPickingEnc = @IdPickingEnc
    GROUP BY pu.IdPickingEnc, pu.IdPedidoEnc, pu.IdPedidoDet,
             pd.no_linea, pd.Cantidad, pu.IdProductoBodega,
             pu.Lic_plate, pu.Lote, pu.Fecha_Vence, pu.IdRecepcion
),
MovGrupo AS (
    SELECT
        m.IdTransaccion AS IdPickingEnc, m.IdPedidoEnc, m.IdPedidoDet,
        m.IdProductoBodega,
        m.barra_pallet AS Lic_plate, m.lote AS Lote, m.fecha_vence AS Fecha_Vence,
        m.IdRecepcion,
        COUNT(*) AS MovimientosVERI,
        SUM(ISNULL(m.cantidad, 0)) AS CantidadVerificadaMovVERI,
        MIN(m.IdMovimiento) AS PrimerIdMovimiento,
        MAX(m.IdMovimiento) AS UltimoIdMovimiento
    FROM trans_movimientos m
    WHERE m.IdTipoTarea = 11
      AND m.IdTransaccion = @IdPickingEnc
    GROUP BY m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet,
             m.IdProductoBodega, m.barra_pallet, m.lote,
             m.fecha_vence, m.IdRecepcion
)
SELECT
    pg.*, ISNULL(mg.MovimientosVERI, 0) AS MovimientosVERI,
    ISNULL(mg.CantidadVerificadaMovVERI, 0) AS CantidadVerificadaMovVERI,
    ISNULL(mg.CantidadVerificadaMovVERI, 0)
        - ISNULL(pg.CantidadVerificadaPickingUbic, 0) AS DiferenciaMovVsPickingUbic,
    CASE WHEN ISNULL(pg.CantidadVerificadaPickingUbic, 0) = 0 THEN NULL
         ELSE ISNULL(mg.CantidadVerificadaMovVERI, 0)
              / NULLIF(pg.CantidadVerificadaPickingUbic, 0)
    END AS FactorMovVsPickingUbic,
    mg.PrimerIdMovimiento, mg.UltimoIdMovimiento
FROM PickingGrupo pg
LEFT JOIN MovGrupo mg
    ON mg.IdPickingEnc = pg.IdPickingEnc
   AND mg.IdPedidoEnc  = pg.IdPedidoEnc
   AND mg.IdPedidoDet  = pg.IdPedidoDet
   AND mg.IdProductoBodega = pg.IdProductoBodega
   AND ISNULL(mg.Lic_plate COLLATE DATABASE_DEFAULT, '')
       = ISNULL(pg.Lic_plate COLLATE DATABASE_DEFAULT, '')
   AND ISNULL(mg.Lote      COLLATE DATABASE_DEFAULT, '')
       = ISNULL(pg.Lote      COLLATE DATABASE_DEFAULT, '')
   AND ISNULL(mg.Fecha_Vence, '19000101') = ISNULL(pg.Fecha_Vence, '19000101')
   AND ISNULL(mg.IdRecepcion, 0) = ISNULL(pg.IdRecepcion, 0)
WHERE ISNULL(mg.MovimientosVERI, 0) > 0
ORDER BY DiferenciaMovVsPickingUbic DESC,
         FactorMovVsPickingUbic    DESC,
         pg.IdPedidoDet, pg.Lic_plate;
```

## Query de candidatos a eliminar (NO ejecuta DELETE)

Aplica `ROW_NUMBER() OVER (PARTITION BY <llave-logica> ORDER BY IdMovimiento)`
y lista los `rn > 1`. Solo enumera, no toca datos.

```sql
DECLARE @IdPickingEnc INT = 1628;

WITH VeriRank AS (
    SELECT
        m.IdMovimiento, m.IdTransaccion AS IdPickingEnc,
        m.IdPedidoEnc, m.IdPedidoDet,
        m.IdRecepcion, m.IdRecepcionDet,
        m.IdProductoBodega,
        m.IdUbicacionOrigen, m.IdUbicacionDestino,
        m.IdPresentacion, m.IdEstadoOrigen, m.IdEstadoDestino,
        m.IdUnidadMedida,
        m.barra_pallet, m.lote, m.fecha_vence, m.cantidad,
        ROW_NUMBER() OVER (
            PARTITION BY
                m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet,
                m.IdRecepcion, m.IdRecepcionDet,
                m.IdProductoBodega,
                m.IdUbicacionOrigen, m.IdUbicacionDestino,
                m.IdPresentacion, m.IdEstadoOrigen, m.IdEstadoDestino,
                m.IdUnidadMedida,
                ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, ''),
                ISNULL(m.lote         COLLATE DATABASE_DEFAULT, ''),
                ISNULL(m.fecha_vence, '19000101'),
                m.cantidad
            ORDER BY m.IdMovimiento
        ) AS rn,
        COUNT(*) OVER (
            PARTITION BY
                m.IdTransaccion, m.IdPedidoEnc, m.IdPedidoDet,
                m.IdRecepcion, m.IdRecepcionDet,
                m.IdProductoBodega,
                m.IdUbicacionOrigen, m.IdUbicacionDestino,
                m.IdPresentacion, m.IdEstadoOrigen, m.IdEstadoDestino,
                m.IdUnidadMedida,
                ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, ''),
                ISNULL(m.lote         COLLATE DATABASE_DEFAULT, ''),
                ISNULL(m.fecha_vence, '19000101'),
                m.cantidad
        ) AS repeticiones
    FROM trans_movimientos m
    WHERE m.IdTipoTarea = 11
      AND m.IdTransaccion = @IdPickingEnc
)
SELECT *
FROM VeriRank
WHERE repeticiones > 1
ORDER BY IdPedidoDet, barra_pallet, lote, cantidad, rn, IdMovimiento;
```

## Preguntas abiertas — actualizadas con respuestas de Codex 2026-05-19

- [x] **Cual movimiento se conserva en duplicados exactos**: **primer
      `IdMovimiento`** (`ROW_NUMBER()=1`). Codex confirma criterio.
- [ ] **Duplicados con user/fecha distintos**: si dos movimientos tienen la
      llave logica identica pero `user_agr`/`fec_agr` diferentes, igual se
      elimina todo `rn > 1`? **Pendiente Erik.** Los SPs hoy borran sin
      distincion por usuario.
- [ ] **Grupos con `CantidadVerificadaPickingUbic = 0` y `VERI > 0`**:
      son huerfanos a eliminar o representan flujo valido no cubierto?
      **Pendiente Erik.** Los SPs los **NO tocan** (criterio conservador
      `Cantidad_Verificada > 0` requerido).
- [x] **Donde aplicar el fix definitivo**: Codex aplico en **ambos niveles**
      — `Procesar_Verificacion_Desde_BOF` (conversion UMBAS) +
      `Insertar_Movimiento_Verificacion` (idempotencia + limpieza simetrica
      con `Marcar_Linea_No_Verificada` en flujo BOF/manual).

## Estado del fix BOF (2026-05-19)

Parche local Codex compilado OK pero **NO commiteado** a TOMWMS_BOF
`dev_2028_merge`:

```
TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb
TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb
```

Cambios locales no relacionados detectados (NO deben mezclarse en este
commit):
- `TOMIMSV4/TOMIMSV4/Conn_Prograx.ini`
- `WSHHRN/TOMHHWS.asmx.vb`

Cuando se mergee a `dev_2028_merge` corresponde promover a `replit.md` §4 una
regla nueva del tipo: *"rutinas BOF que insertan en familia A
(`trans_movimientos`, `stock`, `stock_res`) deben ser idempotentes por llave
logica y convertir a UMBAS si `IdPresentacion > 0`"*.

## Referencias

- PROPOSAL origen Patron 1:
  `wms-brain/brain/handoffs/2026-05-19-codex-learning-bof-veri-movimientos-duplicados/PROPOSAL.md`
- Confirmacion Patron 2 (caso 1465):
  `.../ANALISIS_CLIENTE_REVISION_VERIFICACION_DOBLE.md`
- Guia operativa SPs:
  `.../GUIA_OPERATIVA_REGULARIZACION_VERI.md`
- Bitacora:
  `.../LOG_DIARIO.md`
- LEARNINGS del handoff:
  `.../LEARNINGS.md`
- Indice SPs: `wms-brain/brain/code-changes/BOF/SP-REGULARIZACION-VERI.md`
- Patron hermano (reserva BYB):
  `wms-brain/brain/code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md`
- Regla UMBAS canonica: `wms-brain/brain/code-changes/HH/PATTERNS-UMBAS.md`
- Catalogo tipos de tarea:
  `wms-brain/brain/reference/catalogo-tarea-hh-estados.md`
- Capas WMSWebAPI (no aplica aca, es BOF VB.NET legacy):
  `wms-brain/brain/code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md`
- Forma A: `.local/skills/wms-tomwms/conventions.md` §1
