# Cambio mayor: `trans_movimientos.IdMovimiento` a `IDENTITY`

Fecha: 2026-05-26  
Autor de análisis: Codex (tag `#EJC20260526`)

## 1) Resumen ejecutivo

Se confirma que `dbo.trans_movimientos.IdMovimiento` en `TOMWMS_MAMPA_QA` **no es identity** y hoy depende de `MaxID()+1` en múltiples capas.  
El cambio a `IDENTITY` es viable, pero **requiere faseado** para no romper HH/WS/BOF ni integraciones C# (`WMS.DALCore`).

Estado actual validado en DB:

- Tabla: `dbo.trans_movimientos`
- `IdMovimiento`: `int NOT NULL`, `is_identity = 0`
- Filas: `1813`
- Rango IDs: `1..2040`
- PK: `(IdEmpresa, IdBodegaOrigen, IdTransaccion, IdMovimiento)`
- Tablas con columna `IdMovimiento` relacionadas por valor:
  - `dbo.trans_picking_ubic_stock`
  - `dbo.log_error_wms_reab`

## 2) Árbol de impacto (mapa por capas)

## A. SQL Server / DAL VB clásico (`TOMIMSV4`)

- Alta central de movimientos:
  - `TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos.vb`
    - `Insertar(...)` incluye `idmovimiento` explícito.
- Generación manual de ID:
  - `TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb`
    - `MaxID()` y `MaxID(conn,tx)` (`SELECT MAX(IdMovimiento)`).
- Flujos críticos con `MaxID`:
  - `.../Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb`
  - `.../Stock/clsLnStock_Partial.vb`
  - `.../Stock_Reservado/clsLnStock_res_Partial.vb`
  - `.../Despacho/clsLnTrans_despacho_enc_Partial.vb`
  - UI BOF:
    - `TOMIMSV4/TOMIMSV4/Transacciones/Inventario/frmRegularizarInventario.vb`
    - `TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb`

## B. Web Service HH (`WSHHRN`)

- `WSHHRN/TOMHHWS.asmx.vb`
  - Métodos `Aplica_Cambio_Estado_Ubic_HH*` usan `ByRef pIdMovimientoNuevo`.
  - Contrato requiere devolver ID generado para continuidad del flujo HH.

## C. HH Android (`TOMHH2025`)

- Modelo:
  - `app/src/main/java/com/dts/classes/Transacciones/Movimiento/Trans_movimientos/clsBeTrans_movimientos.java`
- Flujos que consumen `pIdMovimientoNuevo`:
  - `.../CambioUbicacion/frm_cambio_ubicacion_ciega.java`
  - `.../Reabastecimiento/frm_datos_reabastecimiento.java`
- También inicializa `gMovimientoDet.IdMovimiento = 0` en varios puntos (compatible con identity si backend devuelve el nuevo id).

## D. DAL Core C# (`WMS.DALCore`) — proyecto adicional afectado

- Archivo crítico:
  - `WMS.DALCore/Movimientos/clsLnTrans_movimientos.cs`
    - `Insertar(...)` inserta `idmovimiento` explícito.
    - `MaxID(...)` (dos overloads).
    - `Insertar_Movimientos_Recepcion(...)` usa `MaxID()+1`.
    - `Existe(...)` y `InsertarOActualizar(...)` dependen del patrón ID manual.

## E. Entidades C# (`WMS.EntityCore`)

- Clases `clsBeTrans_movimientos` y DTOs incluyen `IdMovimiento` como propiedad int.
- No bloquea el cambio, pero requiere respetar asignación post-insert.

## 3) Riesgos funcionales detectados

1. **Colisión de IDs** por `MAX+1` en concurrencia (ya existente).
2. Si migramos DB sin cambiar DAL/WS, puede fallar por inserción explícita sobre identity.
3. Si no devolvemos `pIdMovimientoNuevo`, HH puede romper continuidad en cambio ubicación/reabasto.
4. Si no preservamos IDs históricos durante migración, se contamina trazabilidad con `trans_picking_ubic_stock` / logs.

## 4) Estrategia recomendada (faseada)

1. **DB primero (ventana controlada):**
   - recrear tabla con `IdMovimiento IDENTITY`,
   - copiar con `IDENTITY_INSERT ON` para preservar IDs,
   - reseed al `MAX(IdMovimiento)`,
   - conservar backup de tabla anterior.
2. **DAL VB y DALCore:**
   - eliminar dependencia de `MaxID` para inserción de movimientos,
   - que `Insertar(...)` capture y devuelva el `IdMovimiento` generado.
3. **WSHHRN:**
   - asegurar que `pIdMovimientoNuevo` se complete desde DB identity.
4. **HH Android:**
   - mantener `IdMovimiento=0` al enviar y consumir `pIdMovimientoNuevo` de respuesta.
5. **Hardening posterior:**
   - deprecar `MaxID()` sólo para `trans_movimientos` (sin tocar otras tablas).

## 5) Script SQL generado

Archivo:

- `tools/diagnostics/20260526_fix_trans_movimientos_idmovimiento_identity.sql`

El script:

- valida precondiciones,
- crea tabla nueva con identity,
- migra datos preservando IDs,
- recrea PK/FKs,
- recrea índices no clusterizados desde metadata,
- reseedea identity,
- deja backup: `trans_movimientos__pre_identity_20260526`.

## 6) Estado de implementación (actualizado 2026-05-26)

Aplicado:

1. `TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos.vb`
   - Insert de `trans_movimientos` ya no envía `idmovimiento`.
   - Se usa patrón estándar del repo: `Ins.SQL() & "; SELECT CAST(SCOPE_IDENTITY() AS INT);"`.
   - `Insertar(...)` asigna el identity devuelto a `oBeTrans_movimientos.IdMovimiento`.
2. `WMS.DALCore/Movimientos/clsLnTrans_movimientos.cs`
   - Ambos overloads de `Insertar(...)` migrados a `SCOPE_IDENTITY()`.
   - Se elimina `Ins.Add("idmovimiento", ...)` en insert.
   - `Insertar_Movimientos_Recepcion(...)` ya no preasigna `MaxID()+1`.
3. Flujos VB que seguían preasignando `IdMovimiento` con `MaxID`:
   - `.../Movimiento/clsLnTrans_movimientos_Partial.vb`
   - `.../Transaccion_Ubicacion_HH/.../clsLnTrans_ubic_hh_det_Partial.vb`
   - `.../Stock/clsLnStock_Partial.vb`
   - `.../Stock_Reservado/clsLnStock_res_Partial.vb`
   - `.../Despacho/clsLnTrans_despacho_enc_Partial.vb`
   - Todos ajustados para enviar `IdMovimiento = 0` y dejar que DB genere identity.

Evaluado en HH (`TOMHH2025`):

- El cliente ya trabaja con `gMovimientoDet.IdMovimiento = 0` en flujos críticos.
- Consume `pIdMovimientoNuevo` en respuestas de WS para continuidad.
- No se requirió cambio de código Android en esta fase.

Pendiente recomendado (fase 2):

1. Barrido de formularios BOF (`frmRegularizarInventario`, `frmAjusteStock`) para eliminar cualquier preasignación residual de `MaxID` en movimientos legacy.
2. Ejecutar pruebas de regresión HH/WS con DB ya migrada a identity en QA.

## 7) Estado guardado (checkpoint)

Fecha checkpoint: `2026-05-26`  
Estado: **EN CURSO / ESTABLE PARA CONTINUAR**

Resumen:

- `IdMovimiento` migrado en DAL VB + DALCore a patrón identity (`SCOPE_IDENTITY`).
- Corrección aplicada: `idmovimiento` removido del `SET` en `UPDATE` (solo se usa en `WHERE`).
- HH ajustada en flujos de cambio de ubicación y reabastecimiento con parse robusto de:
  - `pIdStockNuevo`
  - `pIdMovimientoNuevo`
- Validaciones agregadas en HH para cortar flujo cuando IDs retornan inválidos (`<= 0`).
- Kit de escalamiento/diagnóstico creado en:
  - `tools/diagnostics/scale-readiness-kit/*`

## 8) Cola de trabajo (pendiente priorizado)

### Cola A — Optimización

1. Eliminar preasignaciones residuales de `MaxID` en formularios legacy BOF:
   - `frmRegularizarInventario`
   - `frmAjusteStock`
2. Revisión de transacciones largas en flujos de alta concurrencia:
   - picking/verificación/reabastecimiento/cambio ubicación
3. Revisar índices en tablas calientes:
   - `stock`, `stock_res`, `trans_picking_ubic`, `trans_movimientos`

### Cola B — Trazabilidad de rendimiento (integral)

1. Implementar esquema de observabilidad SQL (tablas `obs_*` en español, propuesta aprobada).
2. Instrumentar WS/ASMX/WebAPI con `id_correlacion` end-to-end.
3. Registrar etapas por capa (`HH -> WS -> DAL -> SQL`) y duraciones por etapa.
4. Construir vista BOF DevExpress para:
   - p95/p99 por proceso
   - errores por endpoint/bodega/operador
   - timeline por `id_correlacion`
5. Definir alertas operativas tempranas:
   - deadlocks
   - bloqueos > umbral
   - degradación de p95
