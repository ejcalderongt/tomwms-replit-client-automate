---
slug: 2026-05-19-codex-learning-bof-veri-movimientos-duplicados
agente: codex-local
fecha: 2026-05-19T00:00:00-06:00
repo_objetivo: TOMWMS
area: BOF / Picking / Verificacion
estado: propuesta_para_brain_keeper
---

# Learning: duplicacion de movimientos VERI en picking BOF

## Resumen ejecutivo

Se analizo un caso de duplicacion de movimientos de verificacion (`trans_movimientos.IdTipoTarea = 11`, tipo `VERI`) para el picking `IdPickingEnc = 1628`.

El patron observado no parece ser una diferencia de captura HH, sino una reinsercion exacta de movimientos `VERI`: misma barra/pallet, producto, pedido det, recepcion det, ubicacion origen/destino y misma cantidad. Para el primer mecanismo de correccion se propone atacar solo duplicados exactos, conservar un movimiento canonico por llave logica y dejar los casos huerfanos/mismatch para analisis separado.

## Datos clave confirmados

- `sis_tipo_tarea.IdTipoTarea = 11` corresponde a `VERI`.
- Para `VERI`, `trans_movimientos.IdTransaccion` apunta a `trans_picking_enc.IdPickingEnc`.
- La relacion con pedido se conserva en `trans_movimientos.IdPedidoEnc` y `trans_movimientos.IdPedidoDet`.
- La relacion con recepcion se conserva en `trans_movimientos.IdRecepcion` y `trans_movimientos.IdRecepcionDet`.
- La relacion operativa de lineas verificadas se infiere contra `trans_picking_ubic` por `IdPickingEnc`, pedido, producto, licencia/pallet, lote, fecha vence y recepcion.
- `tarea_hh` asociada al picking usa normalmente `IdTipoTarea = 8` (`PIK`) con `IdTransaccion = IdPickingEnc`; no se observo una tarea HH `VERI` separada para este caso.

## Caso de estudio: picking 1628

Encabezado observado:

- `IdPickingEnc = 1628`
- Estado picking: `Procesado`
- `verifica_auto = 0`
- `procesado_bof = 1`
- `fec_agr = 2026-03-04 22:25:45.580`
- `user_agr = 85`
- `fec_mod = 2026-03-10 08:38:59.893`
- `user_mod = 34`
- `LineasPickingUbic = 64`
- `CantidadRecibidaPU = 6552`
- `CantidadVerificadaPU = 6552`

Grupo representativo:

- `IdPedidoDet = 24055`
- `NoLineaPedido = 2`
- `CantidadPedido = 3360`
- `IdProductoBodega = 4233`
- `Lic_plate = MXL54480460040043`
- `Lote = L62226036CAFR`
- `Fecha_Vence = 2026-12-05`

Lineas `trans_picking_ubic` del grupo:

| IdPickingUbic | Cantidad_Verificada |
| ---: | ---: |
| 48370 | 76 |
| 48371 | 33 |
| 48372 | 5 |
| 48373 | 38 |
| 48374 | 16 |

Total esperado del grupo por `trans_picking_ubic`: `168`.

Movimientos `VERI` observados para el mismo grupo:

- 15 movimientos.
- Las cantidades `76`, `33`, `5`, `38`, `16` aparecen repetidas 3 veces.
- Total en `trans_movimientos`: `504`.
- Factor observado: `504 / 168 = 3`.
- Exceso: `336`.

Conclusion del caso: no es que la linea `IdPickingUbic = 48374` tenga 504 por si sola; el join amplio la presenta junto con sus lineas hermanas. El patron real es la repeticion exacta del set completo de movimientos de verificacion.

## Flujo de codigo relevante

Archivos y metodos identificados:

- `TOMIMSV4/TOMIMSV4/Transacciones/Picking/frmPicking.vb`
  - `mnuVerificarPickeados_ItemClick`
  - `Linea_No_Pickeada`
  - `Linea_No_Verificada`
  - `Verificar_Nuevamente`
- `TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb`
  - `Procesar_Verificacion_Desde_BOF`
  - `Marcar_Linea_No_Pickeada`
  - `Marcar_Linea_No_Verificada`
  - `Actualiza_Cant_Peso_Verificacion`
- `TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb`
  - `Insertar_Movimiento_Verificacion`
  - `Eliminar_Movimiento_Verificacion_By_PickingUbic`

Hallazgos de flujo:

- `frmPicking.mnuVerificarPickeados_ItemClick` invoca `clsLnTrans_picking_ubic.Procesar_Verificacion_Desde_BOF(...)`.
- `Procesar_Verificacion_Desde_BOF` recorre lineas con `Cantidad_Recibida > 0`, asigna `Cantidad_Verificada = Cantidad_Recibida`, actualiza la linea e invoca `Insertar_Movimiento_Verificacion(...)`.
- En ese flujo no se observo una guarda de idempotencia que descarte lineas ya verificadas o movimientos `VERI` ya existentes.
- El flujo HH de `Actualiza_Cant_Peso_Verificacion` es mas conservador porque filtra pendientes con `(Cantidad_Recibida - Cantidad_Verificada) <> 0.0`.
- `Marcar_Linea_No_Verificada` elimina movimientos `VERI` solo bajo una condicion ligada a `Verificacion_Auto = True`; si la verificacion fue manual/BOF puede dejar movimientos previos.

## Hipotesis operativa

La duplicacion puede ocurrir cuando una linea/picking ya tiene movimientos `VERI` y desde BOF se ejecuta nuevamente una accion de verificacion completa o una secuencia de "no verificado" / "verificar pickeados" que no limpia o no ignora los movimientos previos.

En esos casos, el BOF vuelve a insertar el mismo set logico de movimientos `VERI`, generando multiplicadores exactos: 2x, 3x, etc.

## Modelo propuesto para correccion de datos de un picking

Primera fase conservadora: corregir solo duplicados exactos.

Llave logica propuesta para identificar duplicados exactos en `trans_movimientos`:

- `IdTipoTarea`
- `IdTransaccion`
- `IdPedidoEnc`
- `IdPedidoDet`
- `IdRecepcion`
- `IdRecepcionDet`
- `IdProductoBodega`
- `IdUbicacionOrigen`
- `IdUbicacionDestino`
- `IdPresentacion`
- `IdEstadoOrigen`
- `IdEstadoDestino`
- `IdUnidadMedida`
- `barra_pallet`
- `lote`
- `fecha_vence`
- `cantidad`

Regla de correccion propuesta:

1. Particionar movimientos `VERI` por la llave logica anterior.
2. Ordenar por `IdMovimiento`.
3. Conservar `ROW_NUMBER() = 1`.
4. Marcar `ROW_NUMBER() > 1` como candidatos a eliminar.
5. Simular el resultado y validar que la suma depurada por grupo coincida con `trans_picking_ubic.Cantidad_Verificada`.
6. No tocar en esta fase grupos huerfanos o mismatch donde no exista equivalencia clara contra `trans_picking_ubic`.

## Query de diagnostico recomendada

```sql
DECLARE @IdPickingEnc INT = 1628;

WITH PickingGrupo AS (
    SELECT
        pu.IdPickingEnc,
        pu.IdPedidoEnc,
        pu.IdPedidoDet,
        pd.no_linea AS NoLineaPedido,
        pd.Cantidad AS CantidadPedido,
        pu.IdProductoBodega,
        pu.Lic_plate,
        pu.Lote,
        pu.Fecha_Vence,
        pu.IdRecepcion,
        COUNT(*) AS LineasPickingUbic,
        MIN(pu.IdPickingUbic) AS PrimerIdPickingUbic,
        MAX(pu.IdPickingUbic) AS UltimoIdPickingUbic,
        SUM(ISNULL(pu.Cantidad_Solicitada, 0)) AS CantidadSolicitadaPickingUbic,
        SUM(ISNULL(pu.Cantidad_Recibida, 0)) AS CantidadRecibidaPickingUbic,
        SUM(ISNULL(pu.Cantidad_Verificada, 0)) AS CantidadVerificadaPickingUbic
    FROM trans_picking_ubic pu
    LEFT JOIN trans_pe_det pd
        ON pd.IdPedidoEnc = pu.IdPedidoEnc
       AND pd.IdPedidoDet = pu.IdPedidoDet
    WHERE pu.IdPickingEnc = @IdPickingEnc
    GROUP BY
        pu.IdPickingEnc,
        pu.IdPedidoEnc,
        pu.IdPedidoDet,
        pd.no_linea,
        pd.Cantidad,
        pu.IdProductoBodega,
        pu.Lic_plate,
        pu.Lote,
        pu.Fecha_Vence,
        pu.IdRecepcion
),
MovGrupo AS (
    SELECT
        m.IdTransaccion AS IdPickingEnc,
        m.IdPedidoEnc,
        m.IdPedidoDet,
        m.IdProductoBodega,
        m.barra_pallet AS Lic_plate,
        m.lote AS Lote,
        m.fecha_vence AS Fecha_Vence,
        m.IdRecepcion,
        COUNT(*) AS MovimientosVERI,
        SUM(ISNULL(m.cantidad, 0)) AS CantidadVerificadaMovVERI,
        MIN(m.IdMovimiento) AS PrimerIdMovimiento,
        MAX(m.IdMovimiento) AS UltimoIdMovimiento
    FROM trans_movimientos m
    WHERE m.IdTipoTarea = 11
      AND m.IdTransaccion = @IdPickingEnc
    GROUP BY
        m.IdTransaccion,
        m.IdPedidoEnc,
        m.IdPedidoDet,
        m.IdProductoBodega,
        m.barra_pallet,
        m.lote,
        m.fecha_vence,
        m.IdRecepcion
)
SELECT
    pg.IdPickingEnc,
    pg.IdPedidoEnc,
    pg.IdPedidoDet,
    pg.NoLineaPedido,
    pg.CantidadPedido,
    pg.IdProductoBodega,
    pg.Lic_plate,
    pg.Lote,
    pg.Fecha_Vence,
    pg.IdRecepcion,
    pg.LineasPickingUbic,
    pg.PrimerIdPickingUbic,
    pg.UltimoIdPickingUbic,
    pg.CantidadSolicitadaPickingUbic,
    pg.CantidadRecibidaPickingUbic,
    pg.CantidadVerificadaPickingUbic,
    ISNULL(mg.MovimientosVERI, 0) AS MovimientosVERI,
    ISNULL(mg.CantidadVerificadaMovVERI, 0) AS CantidadVerificadaMovVERI,
    ISNULL(mg.CantidadVerificadaMovVERI, 0)
        - ISNULL(pg.CantidadVerificadaPickingUbic, 0) AS DiferenciaMovVsPickingUbic,
    CASE
        WHEN ISNULL(pg.CantidadVerificadaPickingUbic, 0) = 0 THEN NULL
        ELSE ISNULL(mg.CantidadVerificadaMovVERI, 0)
             / NULLIF(pg.CantidadVerificadaPickingUbic, 0)
    END AS FactorMovVsPickingUbic,
    mg.PrimerIdMovimiento,
    mg.UltimoIdMovimiento
FROM PickingGrupo pg
LEFT JOIN MovGrupo mg
    ON mg.IdPickingEnc = pg.IdPickingEnc
   AND mg.IdPedidoEnc = pg.IdPedidoEnc
   AND mg.IdPedidoDet = pg.IdPedidoDet
   AND mg.IdProductoBodega = pg.IdProductoBodega
   AND ISNULL(mg.Lic_plate COLLATE DATABASE_DEFAULT, '')
       = ISNULL(pg.Lic_plate COLLATE DATABASE_DEFAULT, '')
   AND ISNULL(mg.Lote COLLATE DATABASE_DEFAULT, '')
       = ISNULL(pg.Lote COLLATE DATABASE_DEFAULT, '')
   AND ISNULL(mg.Fecha_Vence, '19000101') = ISNULL(pg.Fecha_Vence, '19000101')
   AND ISNULL(mg.IdRecepcion, 0) = ISNULL(pg.IdRecepcion, 0)
WHERE ISNULL(mg.MovimientosVERI, 0) > 0
ORDER BY
    DiferenciaMovVsPickingUbic DESC,
    FactorMovVsPickingUbic DESC,
    pg.IdPedidoDet,
    pg.Lic_plate;
```

## Query base para candidatos a eliminar

Esta query no elimina nada. Solo enumera duplicados exactos candidatos.

```sql
DECLARE @IdPickingEnc INT = 1628;

WITH VeriRank AS (
    SELECT
        m.IdMovimiento,
        m.IdTransaccion AS IdPickingEnc,
        m.IdPedidoEnc,
        m.IdPedidoDet,
        m.IdRecepcion,
        m.IdRecepcionDet,
        m.IdProductoBodega,
        m.IdUbicacionOrigen,
        m.IdUbicacionDestino,
        m.IdPresentacion,
        m.IdEstadoOrigen,
        m.IdEstadoDestino,
        m.IdUnidadMedida,
        m.barra_pallet,
        m.lote,
        m.fecha_vence,
        m.cantidad,
        ROW_NUMBER() OVER (
            PARTITION BY
                m.IdTransaccion,
                m.IdPedidoEnc,
                m.IdPedidoDet,
                m.IdRecepcion,
                m.IdRecepcionDet,
                m.IdProductoBodega,
                m.IdUbicacionOrigen,
                m.IdUbicacionDestino,
                m.IdPresentacion,
                m.IdEstadoOrigen,
                m.IdEstadoDestino,
                m.IdUnidadMedida,
                ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, ''),
                ISNULL(m.lote COLLATE DATABASE_DEFAULT, ''),
                ISNULL(m.fecha_vence, '19000101'),
                m.cantidad
            ORDER BY m.IdMovimiento
        ) AS rn,
        COUNT(*) OVER (
            PARTITION BY
                m.IdTransaccion,
                m.IdPedidoEnc,
                m.IdPedidoDet,
                m.IdRecepcion,
                m.IdRecepcionDet,
                m.IdProductoBodega,
                m.IdUbicacionOrigen,
                m.IdUbicacionDestino,
                m.IdPresentacion,
                m.IdEstadoOrigen,
                m.IdEstadoDestino,
                m.IdUnidadMedida,
                ISNULL(m.barra_pallet COLLATE DATABASE_DEFAULT, ''),
                ISNULL(m.lote COLLATE DATABASE_DEFAULT, ''),
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
ORDER BY
    IdPedidoDet,
    barra_pallet,
    lote,
    cantidad,
    rn,
    IdMovimiento;
```

## Reglas / convenciones que Brain deberia considerar promover

- Las rutinas BOF que insertan movimientos `VERI` deben ser idempotentes por llave logica de movimiento.
- Antes de re-verificar desde BOF, el flujo debe decidir explicitamente si:
  - solo procesa pendientes;
  - elimina/reconstruye movimientos `VERI` previos de ese picking/grupo;
  - o conserva movimientos previos y bloquea reinserciones duplicadas.
- La limpieza de movimientos `VERI` no deberia depender solo de `Verificacion_Auto` si el flujo BOF permite desmarcar/verificar nuevamente lineas manuales.
- Para regularizacion historica, separar tres categorias:
  - duplicado exacto depurable;
  - movimiento huerfano sin `trans_picking_ubic` equivalente;
  - mismatch de cantidad/llave que requiere revision funcional.

## Preguntas abiertas para Erik / Brain Keeper

- [ ] Para auditoria, en duplicados exactos se debe conservar el primer `IdMovimiento` o el ultimo?
- [ ] Si el movimiento duplicado fue generado por usuarios distintos o fechas distintas, igual debe eliminarse todo `rn > 1` mientras la llave logica sea identica?
- [ ] Los grupos con `CantidadVerificadaPickingUbic = 0` y movimientos `VERI > 0` deben tratarse como huerfanos a eliminar, o pueden representar un flujo valido no cubierto por la relacion actual?
- [ ] La correccion definitiva debe ser en `Procesar_Verificacion_Desde_BOF`, en `Insertar_Movimiento_Verificacion`, o en ambos niveles?

## Recomendacion de siguiente handoff

Crear un handoff BOF especifico para:

1. Generar script SELECT-only de auditoria para un `IdPickingEnc`.
2. Generar script transaccional de depuracion para duplicados exactos de un solo picking, con `BEGIN TRAN`, reporte previo/posterior y `ROLLBACK` por defecto.
3. Ajustar codigo BOF para hacer idempotente `Procesar_Verificacion_Desde_BOF`.
4. Validar con el caso `IdPickingEnc = 1628` antes de expandir a mas pickings.
