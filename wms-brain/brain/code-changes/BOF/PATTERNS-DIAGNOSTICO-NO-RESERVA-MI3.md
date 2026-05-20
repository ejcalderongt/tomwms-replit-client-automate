---
slug: patterns-diagnostico-no-reserva-mi3
agente: brain-keeper
fecha: 2026-05-20
cliente: GLOBAL (descubierto en BYB QA caso EA-153305)
ambiente: QA
repo_afectado: TOMWMS
branch: dev_2028_merge
area: BOF / reserva MI3 / diagnostico operativo
relacionado:
  - handoffs/2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva
  - handoffs/2026-05-19-codex-learning-byb-mi3-reserva-umbas
  - code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md
  - code-changes/BOF/PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md
  - code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md
---

# Diagnostico tipificado de no-reserva MI3

Promovido desde el handoff `2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva` (Codex local / "Mary Jane"). Documenta la taxonomia de motivos que la reserva MI3 debe emitir cuando NO logra reservar, en lugar del mensaje generico `"No se pudo completar la reserva"` que actualmente devuelve el legacy `clsLnI_nav_ped_traslado_enc_Partial.vb` lineas 1178, 1316, 1752.

## 1. Problema

El resultado `"No se pudo completar la reserva"` (texto fijo en tres puntos del legacy VB) no permite distinguir:

- Si realmente no hay stock fisico aplicable.
- Si hay stock fisico pero esta totalmente reservado por otros pedidos.
- Si hay stock disponible pero una regla de negocio (FEFO, vencimiento, ubicacion obligatoria, presentacion, talla/color) lo excluye.

La operacion no puede actuar y desarrollo no puede distinguir bug vs regla correcta.

## 2. Capas obligatorias del diagnostico

```
Capa 1: ¿Hay stock fisico aplicable?           → SIN_STOCK_APLICABLE
Capa 2: ¿Hay disponible (post reservas)?       → RESERVADO_POR_OTROS
Capa 3: ¿Hay disponible pero regla lo excluye? → motivo especifico (FEFO/UM/zona/etc)
```

## 3. Taxonomia canonica (10 codigos operativos)

| Codigo | Cuando aplica | Datos minimos | Codigo Core equivalente (ya existe) |
|---|---|---|---|
| `SIN_STOCK_APLICABLE` | No queda ningun stock despues de filtros producto/bodega/estado/presentacion/ubicacion/vencimiento. | solicitado, pendiente, filtros activos | `NO_STOCK` (1) |
| `RESERVADO_POR_OTROS` | Hay stock fisico, pero `stock - stock_res` <= 0 o insuficiente. | stock fisico, reservado vigente, disponible | **NUEVO** (no esta en `ReservationFailureCode`) |
| `SIN_VENCIMIENTO_VALIDO` | Hay stock pero `Fecha_vence` no cumple `DiasVencimiento` o `cliente_tiempos.es_manufactura`. | fecha minima, dias requeridos | `ALL_STOCK_EXPIRED` (8) + `MANUFACTURING_DATE_INVALID` (13) (cubren parcialmente) |
| `FEFO_BLOQUEA_PICKING` | Hay stock en picking, pero existe stock mas viejo en ALM y FEFO no permite saltarlo. | fecha picking, fecha ALM, cantidades | `ZONE_PRIORITY_CONFLICT` (9) (genericо; falta detalle FEFO) |
| `SOLO_NO_PICKING_SIN_EXPLOSION` | Solo hay stock en no-picking/ALM y no se puede explosionar o tomar desde alli. | cantidad no-picking, ubicacion, nivel | `PICKING_ZONE_REQUIRED_NO_STOCK` (5) |
| `EXPLOSION_NIVEL_NO_APLICA` | Explosion automatica esta activa, pero nivel/ubicacion no cumple la regla. | ubicacion, nivel, max nivel | **NUEVO** |
| `PRESENTACION_NO_APLICA` | La presentacion/variant solicitada no coincide o no se puede convertir de forma segura. | id presentacion, factor, UM | **NUEVO** |
| `UBICACION_CLIENTE_OBLIGATORIA` | `BeCliente.IdUbicacionAbastecerCon` esta seteada y no hay stock aplicable alli. Mensaje existente: `"Verifique existencias en ubicacion: X la reserva se esta intentando realizar desde esta ubicacion"` (linea 1187 legacy). | ubicacion obligatoria, disponible alli | `LOCATION_RESTRICTED_NO_STOCK` (3) + `RECEPTION_LOCATION_NOT_ALLOWED` (7) (parcial) |
| `TALLA_COLOR_NO_APLICA` | Bodega controla talla/color y no hay stock con combinacion valida. | size, color, id talla-color | **NUEVO** |
| `PARCIAL_NO_PERMITIDA` | Existe reserva parcial posible pero `Configuration.Rechazar_pedido_incompleto = Si`. Core la levanta como Exception `PEDIDO_INCOMPLETO` (ReservationLoopStep.cs L219). | solicitado, reservado, faltante | **NUEVO** (Core la tipifica como exception, no como FailureCode) |

**Resumen cobertura**: 5 de 10 motivos ya tienen equivalente parcial en `WMS.DALCore/Reserva_Stock/Core/Interfaces/ReservationResultDto.cs` (enum `ReservationFailureCode`, 14 valores). Los otros 5 son nuevos y deben sumarse al enum y al legacy VB en paralelo.

## 4. Formato de mensaje operativo

Para `I_nav_ped_traslado_det.Process_Result` (visible en pantalla, una linea):

```text
No se pudo completar la reserva: FEFO_BLOQUEA_PICKING - existe stock en picking,
pero hay stock mas antiguo en ALM/no-picking y la regla FEFO no permite saltarlo.
Solicitado: 121, disponible picking: 10, fecha picking: 2027-04-13,
fecha ALM: 2027-04-12.
```

Para `log_error_wms` y trace YAML (detalle tecnico):

```yaml
CodigoMotivo: FEFO_BLOQUEA_PICKING
Categoria: regla_negocio
MensajeOperativo: "..."
CantidadSolicitada: 121
CantidadReservada: 0
CantidadPendiente: 121
StockFisico: 250
StockDisponiblePostReserva: 10
PickingDisponible: 10
NoPickingDisponible: 240
FechaMinimaPicking: 2027-04-13
FechaMinimaNoPicking: 2027-04-12
IdUbicacion: ...
IdPresentacion: ...
IdStock: ...
NoEnc: EA-153305
Line_No: 40000
Item_No: 00025001
```

## 5. Donde vive cada cosa

### Legacy VB (HEAD dev_2028_merge faaf853)

- `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
  - Funcion `Reserva_Stock_From_MI3`: linea 18513 -> 26850 (**8,337 lineas en una sola funcion**).
  - **NO existe** `Marcar_Motivo_No_Reserva_MI3` en HEAD. Codex la tiene LOCAL pero no la pusheo.
  - **NO existen** los 10 codigos tipificados en el VB.

- `TOMIMSV4/DAL/Interface/Pedido_Traslado/Pedido_Traslado_Enc/clsLnI_nav_ped_traslado_enc_Partial.vb`
  - Lineas 1178, 1316, 1752: hard-coded `"No se pudo completar la reserva"`.
  - Linea 1187 ya emite mensaje especifico cuando hay `BeCliente.IdUbicacionAbastecerCon` (es el unico motivo tipificado parcialmente en legacy).
  - Lineas 1227, 1349, 1778: cuando hay excepcion, `Process_Result = ex.Message` (pasa el detalle pero sin codigo).

### Core (WMS.DALCore/Reserva_Stock)

- `Core/Interfaces/ReservationResultDto.cs`
  - `enum ReservationFailureCode` (14 valores).
  - `class ReservationFailureReason` con campos `Code`, `Message`, `LoteNo`, `IdUbicacion`, `IdProductoEstado`, `ZoneName`, `QuantityAffected`.
  - `class ReservationResultDto.StatusMessage` ya construye el mensaje agregado.

- `Core/Domain/ReservationContext.cs`
  - Propiedad `List<ReservationFailureReason> FailureReasons` (linea 114).
  - Helper `AddFailure(code, message, qtyAffected)` (linea 156).
  - Helper `AddLotFailure(loteNo, qty)` (linea 170).
  - Helper `AddZoneFailure(pickingRequired, qty)` (linea 182).
  - Flag `HadExpiredStock` (linea 103) — Core ya distingue "no hay stock" vs "hay stock pero vencido".

- `Compatibility/StockReservationFacade.cs` linea 322
  - Si `reservations.Count == 0 && context.FailureReasons.Count > 0`: setea `oBeStockResRequest.UltimoMensajeFallo = context.FailureReasons.First().Message`.
  - **Esto es el "Process_Result tipificado" que Codex quiere en legacy**, ya implementado en Core.

## 6. Plan de adopcion (orden recomendado)

1. **No SQL schema nuevo**. La taxonomia vive como enum + campo string en VB y como `ReservationFailureCode` en Core.

2. **Core primero**: expandir `ReservationFailureCode` con los 5 nuevos: `RESERVADO_POR_OTROS`, `FEFO_BLOQUEA_PICKING`, `EXPLOSION_NIVEL_NO_APLICA`, `PRESENTACION_NO_APLICA`, `TALLA_COLOR_NO_APLICA`, `PARCIAL_NO_PERMITIDA` (6 si contamos `PARCIAL_NO_PERMITIDA` como reemplazo de la `Exception PEDIDO_INCOMPLETO` actual).

3. **Legacy VB**: agregar `Marcar_Motivo_No_Reserva_MI3` que reciba codigo + payload y rellene `Process_Result` + `log_error_wms`. Llamar desde cada `Return False` profundo en `Reserva_Stock_From_MI3`.

4. **`clsLnI_nav_ped_traslado_enc_Partial.vb`**: en lugar de hardcode `"No se pudo completar la reserva"`, leer `pStockResSolicitud.UltimoMensajeFallo` (campo a agregar a `clsBeStock_res` legacy, ya existe en Core).

5. **Replit DB / wms-brain**: ficha por motivo en `brain/test-scenarios/reservation/` con datos de stock minimos para reproducir.

## 7. Gotchas

- **FEFO falso negativo**: cuando la lista de ALM/no-picking no filtra `Cantidad > 0` despues de restar reservas, FEFO puede bloquear picking inexistentemente. Caso sospechado: `EA-153305` linea `40000` (item `00025001`). El handoff de origen lo deja pendiente de validar.

- **Performance trace `perf_restar_stock_reservado_fin` / `reserva-mi3-YYYYMMDD-HHMMSS-fff-PID-uuid`** (commit `a6d394e8` "evento_tiempo logreserva", commit `be4fbc50` "LOG! para proceso de reserva"): estos eventos miden costo, NO causa de negocio. Deben coexistir con `motivo_no_reserva` tipificado. Formato YAML aun no documentado en `reference/`.

- **Texto libre con codigos `#ERROR_202310021910`** (linea 1176, 1185): sirve para debug historico, no es estable para reporteria. Usar el codigo del enum y dejar `#ERROR_*` solo en `log_error_wms`.

- **`Process_Result` puede ser largo**: el handoff Codex asume que la pantalla de existencias aguanta un mensaje mas largo que el generico. Validar antes de pasar a produccion.

## 8. Referencias

- Handoff origen: `wms-brain/brain/handoffs/2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva/{PROPOSAL.md,LEARNINGS.md}`
- Paridad legacy vs Core: `code-changes/BOF/PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md`
- UMBAS reserva: `code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md`
- Capas WMSWebAPI: `code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md`
- Patron UMBAS: `code-changes/HH/PATTERNS-UMBAS.md`
- Caso: BYB QA, envio NAV `EA-153305`, pedido WMS `37320`, picking `PA-172112`.
