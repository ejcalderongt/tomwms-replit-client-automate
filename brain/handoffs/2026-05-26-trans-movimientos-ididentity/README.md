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

## 6) Pendientes de implementación de código (no ejecutados aún)

1. Refactor `clsLnTrans_movimientos.vb` para insertar sin `idmovimiento` explícito y retornar nuevo ID.
2. Refactor equivalente en `WMS.DALCore/Movimientos/clsLnTrans_movimientos.cs`.
3. Sustituir asignaciones `MaxID()+1` en flujos de movimiento por retorno real de `Insertar`.
4. Ajustar métodos WS que exponen `pIdMovimientoNuevo`.
5. Pruebas HH:
   - cambio ubicación (ciega/dirigida),
   - reabastecimiento,
   - packing/reemplazos con bitácora de movimientos.

