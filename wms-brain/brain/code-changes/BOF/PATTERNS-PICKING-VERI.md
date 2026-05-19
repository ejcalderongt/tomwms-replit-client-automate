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
  Procesar_Verificacion_Desde_BOF        ← inserta VERI sin guarda
  Marcar_Linea_No_Pickeada
  Marcar_Linea_No_Verificada             ← elimina VERI solo si Verificacion_Auto=True
  Actualiza_Cant_Peso_Verificacion       ← flujo HH; mas conservador

TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb
  Insertar_Movimiento_Verificacion       ← punto natural para guarda
  Eliminar_Movimiento_Verificacion_By_PickingUbic
```

Hallazgos clave:
- `Procesar_Verificacion_Desde_BOF` recorre lineas con `Cantidad_Recibida > 0`,
  asigna `Cantidad_Verificada = Cantidad_Recibida` e invoca
  `Insertar_Movimiento_Verificacion(...)` sin verificar movimientos previos.
- `Actualiza_Cant_Peso_Verificacion` (flujo HH) filtra pendientes con
  `(Cantidad_Recibida - Cantidad_Verificada) <> 0.0` — por eso HH no genera
  duplicados en la misma forma.
- `Marcar_Linea_No_Verificada` solo limpia VERI cuando `Verificacion_Auto = True`.
  Si la verificacion fue manual/BOF, las VERI previas sobreviven y el
  siguiente "verificar pickeados" las duplica.

## Hipotesis operativa

Duplicacion ocurre cuando una linea/picking ya tiene movimientos VERI y desde
BOF se ejecuta nuevamente "verificar pickeados" (o secuencia "no verificado" +
"verificar pickeados") sin limpiar ni saltar los movimientos previos.

## Recomendaciones para el fix (a confirmar con handoff aparte)

1. **Idempotencia en `Insertar_Movimiento_Verificacion`**: antes de insertar,
   verificar que NO exista un movimiento VERI con la llave logica completa.
   Es la defensa minima.
2. **Decision explicita en `Procesar_Verificacion_Desde_BOF`**: el flujo debe
   elegir UNA de estas tres estrategias y dejarla documentada:
   - solo procesa lineas pendientes (skip si ya verificadas);
   - elimina + reconstruye VERI previos del picking/grupo;
   - conserva VERI previos y bloquea reinserciones duplicadas.
3. **Desacoplar limpieza VERI de `Verificacion_Auto`**: `Marcar_Linea_No_Verificada`
   debe poder limpiar VERI tambien en flujo manual/BOF si la regla de negocio
   asi lo indica.
4. **Para regularizacion historica**, separar 3 categorias y atacar por
   separado:
   - duplicado exacto depurable;
   - movimiento huerfano sin `trans_picking_ubic` equivalente;
   - mismatch de cantidad/llave que requiere revision funcional.

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

## Preguntas abiertas (resolver con Erik antes de handoff de fix)

- [ ] **Cual movimiento se conserva en duplicados exactos**: primer
      `IdMovimiento` o ultimo? El PROPOSAL asume primero (`ROW_NUMBER()=1`).
- [ ] **Duplicados con user/fecha distintos**: si dos movimientos tienen la
      llave logica identica pero `user_agr`/`fec_agr` diferentes, igual se
      elimina todo `rn > 1`?
- [ ] **Grupos con `CantidadVerificadaPickingUbic = 0` y `VERI > 0`**:
      son huerfanos a eliminar o representan flujo valido no cubierto?
- [ ] **Donde aplicar el fix definitivo**: `Procesar_Verificacion_Desde_BOF`,
      `Insertar_Movimiento_Verificacion`, o ambos niveles?

## Siguiente handoff propuesto (NO abierto aun)

Codex sugiere — y Brain Keeper concuerda — abrir un handoff BOF especifico
para:

1. SELECT-only de auditoria por `IdPickingEnc` (esta query de diagnostico).
2. Script transaccional de depuracion para duplicados exactos de un solo
   picking, con `BEGIN TRAN`, reporte previo/posterior y `ROLLBACK` por
   defecto. NO se ejecuta `COMMIT` hasta validacion humana.
3. Ajustar codigo BOF para hacer idempotente
   `Procesar_Verificacion_Desde_BOF` (estrategia a definir con preguntas
   abiertas).
4. Validar con `IdPickingEnc = 1628` antes de expandir a otros pickings.

Estado: **propuesto, requiere autorizacion de Erik**.

## Referencias

- PROPOSAL origen:
  `wms-brain/brain/handoffs/2026-05-19-codex-learning-bof-veri-movimientos-duplicados/PROPOSAL.md`
- Catalogo tipos de tarea: `wms-brain/brain/reference/catalogo-tarea-hh-estados.md`
- Capas WMSWebAPI (no aplica aca, es BOF VB.NET legacy):
  `wms-brain/brain/code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md`
- Forma A: `.local/skills/wms-tomwms/conventions.md` §1
- Pattern UMBAS (no relacionado, pero util al revisar `cantidad`):
  `wms-brain/brain/code-changes/HH/PATTERNS-UMBAS.md`
