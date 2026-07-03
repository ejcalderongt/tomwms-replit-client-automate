# Patron: Cantidad en UMBAS para `stock`, `movimientos`, `stock_res`

> Regla canonizada y **matizada** a partir de:
> - Handoff `2026-05-20-hh-recepcion-pallet-presentacion-cantidad` (origen).
> - Confirmacion de Erik en chat 2026-05-20 (alcance real).
>
> Reemplaza la version anterior de este archivo (commit `b7aea6c0fa8a`) que
> era demasiado generalista. La regla NO es "siempre UMBAS para todo stock";
> tiene matices por familia de tablas.
>
> **Actualizacion 2026-05-19**: ver §"Violaciones BOF confirmadas en
> produccion" abajo. Antes solo habia evidencia de afectacion HH. Ahora hay
> **dos casos BOF distintos** (Procesar_Verificacion_Desde_BOF y
> Reserva_Stock_From_MI3) que violan la misma regla con la misma causa raiz
> conceptual: variables/parametros que mezclan presentacion y UMBAS sin
> normalizar antes de persistir.

## La regla, acotada por familia

### Familia A — SIEMPRE UMBAS (HH y BOF)

- `stock`
- `movimientos` (familia `trans_mov_*`, `mov_stock`, etc.)
- `stock_res` (reservas)

En estas la `Cantidad` persistida esta SIEMPRE en UMBAS. Si el producto tiene
presentacion (`IdPresentacion > 0`), la **interpretacion** al usuario es
`Cantidad / Factor`, pero el dato guardado sigue en UMBAS.

Consecuencia: todos los `vBeStock*`, `vBeStockRec*`, `vBeStockPicking*`,
`BeStockResUMBas`, `vBeMovimiento*` que alimentan estas tres familias deben
tener `Cantidad` en UMBAS al momento de persistirse.

### Familia B — depende de presentacion (operativas)

- `trans_picking_ubic`
- Otras operativas similares.

Si el producto tiene presentacion: se guarda en la unidad de presentacion.
Si no tiene presentacion: se guarda en UMBAS.

### Familia C — operador-friendly (siempre presentacion si existe)

- `Uds_lic_plate`
- Mensajes al operador en HH.
- Reportes orientados a usuario final.

### Caso especifico HH picking detalle

En `frm_picking_datos` el valor de UMBas llega en `ProductoUnidadMedida` desde
WS/DAL, pero la fila del layout que contiene `txtUniBas` viene oculta por
defecto. Si `IdPresentacion = 0`, la pantalla debe mostrar la fila UMBas
aunque no exista presentacion real, porque ese es el fallback correcto del
dominio.

Regla operativa:
- lista HH: si hay presentacion, mostrar `ProductoPresentacion`.
- lista HH: si no hay presentacion, mostrar `ProductoUnidadMedida`.
- detalle HH: si no hay presentacion, hacer visible la fila UMBas y pintar
  `ProductoUnidadMedida`.

Huellas utiles:
- `frm_picking_datos.java`
- `activity_frm_picking_datos.xml`
- `clsLnTrans_picking_ubic_Partial.vb`
- `TOMHHWS.asmx.vb`

## Casos borde confirmados por Erik

### Producto sin presentacion

- Objeto presentacion = `Nothing`/null.
- Factor inferido vacio/0.
- Es **caso valido**, NO abortar.
- En Familia A: la cantidad ya esta en UMBAS, usar directo.

### `CamasPorTarima` / `CajasPorCama`

Datos de referencia del palletizado, NO multiplicadores fijos del calculo de
stock. Si vienen null/0:
- NO defaultear a 1.
- NO abortar.
- Pasarlos tal cual al backend (puede interpretarlos como palletizado nuevo
  definido y guardarlo).
- Solo multiplicarlos por la cantidad si vienen > 0 Y la presentacion es a
  nivel pallet.

> Pendiente verificar en codigo HH+BOF el manejo exacto cuando vienen null/0.
> Tracked en `2026-05-20-audit-hh-cantidad-umbas-pattern` (ver ADDENDUM).

## Por que importa

Persistir presentacion en Familia A es un error **silencioso**: stock queda
con cantidad menor a la real (dividida por el factor), sin excepcion ni log.
Solo se detecta al ajustar inventario o al pickear sin unidades. Es la causa
raiz de una clase de descuadres "fantasma" en clientes con presentaciones
distintas a UMBAS.

Para `stock_res` (reserva), la inversion es la otra cara del mismo error:
una variable mal mapeada puede provocar **sobre-reserva** (reservar mas
unidades de las solicitadas), bloqueando stock disponible para otros pedidos.
Ver caso BYB EA-153305 abajo.

## Pseudocodigo recepcion HH (pallet de proveedor)

```
// 1) cantidad visible al operador
si presentacion existe (Nombre no vacio):
    cantidad_visible = BeINavBarraPallet.Cantidad_Presentacion
sino:
    cantidad_visible = BeINavBarraPallet.Cantidad_UMP   // ya esta en UMBAS

// 2) cantidad para persistir en stock (Familia A)
si presentacion existe Y Factor > 0:
    cantidad_umbas = cantidad_visible * Factor
    si presentacion es pallet Y CamasPorTarima>0 Y CajasPorCama>0:
        cantidad_umbas *= CamasPorTarima * CajasPorCama
sino:
    cantidad_umbas = cantidad_visible   // ya esta en UMBAS

// destinos
Uds_lic_plate              = cantidad_visible     // Familia C
mensaje_operador           = cantidad_visible     // Familia C
vBeStockRecPallet.Cantidad = cantidad_umbas       // Familia A → UMBAS
```

## Donde aplica (catalogo)

### Confirmado afectado, corregido
| Archivo | Familia | Conversion |
|---|---|---|
| `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java` | A (alimenta `stock` via `vBeStockRecPallet`) | `Cantidad_Presentacion * Factor [* CamasPorTarima * CajasPorCama]` |

### Sospechoso, pendiente auditoria
Ver handoff `2026-05-20-audit-hh-cantidad-umbas-pattern`. Scope:
- HH: `frm_recepcion_pallet_*`, `frm_recepcion_caja_*`, `frm_picking_*` que
  escriben a `vBeStockPicking*.Cantidad` (Familia A).
- BOF (VB.NET): cualquier asignacion a `.Cantidad` de Familia A que provenga
  de fuente en presentacion.

### Fuera de alcance (NO sospechoso)
- Tablas Familia B (`trans_picking_ubic`, etc.).
- Tablas Familia C (Uds_lic_plate, mensajes).

## Violaciones BOF confirmadas en produccion (2026-05-19)

Hasta esta fecha la regla solo tenia evidencia productiva de violacion en HH.
Dos handoffs nuevos confirman que el patron **tambien existe en BOF VB.NET**:

### Caso 1 — `trans_movimientos` VERI (picking 1465)

| Item | Valor |
|---|---|
| Cliente | KILLIOS |
| Archivo BOF | `TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb` |
| Funcion | `Procesar_Verificacion_Desde_BOF` |
| Patron | `Cantidad_Recibida` (puede venir en presentacion) → `trans_movimientos.cantidad` (debe ser UMBAS) sin conversion. |
| Sintoma | Caso 1465: `cantidad = 72` cuando deberia ser `72 * 24 = 1728` UMBAS (presentacion CAJA24). |
| Evidencia cliente | `RevisionVerificacionDoble.xlsx` (5 movimientos: 288676, 288678, 288679, 288680, 288681). |
| Fix preventivo BOF | Aplicado local por Codex, sin commit a Azure. Convierte a UMBAS si `IdPresentacion > 0`. |
| Fix historico BD | SPs `usp_WMS_VERI_RegularizarCantidadUmbas` + `usp_WMS_VERI_PostCheck` (locales). |
| Detalle | `code-changes/BOF/PATTERNS-PICKING-VERI.md` §"Patron 2" y `code-changes/BOF/SP-REGULARIZACION-VERI.md`. |

### Caso 2 — `stock_res` reserva MI3 (BYB QA EA-153305)

| Item | Valor |
|---|---|
| Cliente | BYB |
| Archivo BOF | `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` |
| Funcion | `Reserva_Stock_From_MI3` (linea ~25553) |
| Tag commit local | `#EJCBYB20250519CKF` |
| Patron | Cuando `pStockResSolicitud.IdPresentacion = 0` (pedido en UMBAS), la recursion para reservar el remanente usa `vCantidadDecimalUMBas` en vez del pendiente real `vCantidadPendiente`. `vCantidadDecimalUMBas` participa tambien en rutas de fraccion/explosion de presentacion. |
| Sintoma | Pedido `15 UNI` de producto `00194250` → reservo `12 CJ (= 48 UMBAS por factor 4) + 997 UMBAS = 1009` cuando el pendiente real era `3` UMBAS. Sobre-reserva ~336x. |
| Evidencia cliente | `Erores_WMS_140526.docx`, BD QA `IMS4MB_BYB_QAS2`. |
| Fix preventivo BOF | Aplicado local por Codex, sin commit a Azure: si `IdPresentacion = 0`, forzar `vCantidadDecimalUMBas = vCantidadPendiente` antes de armar `BeStockResUMBas.Cantidad`. |
| Fix historico BD | No preparado todavia (a evaluar segun decision Erik). |
| Detalle | `code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md`. |

### Meta-patron (causa raiz comun)

Ambos casos comparten la misma anti-pattern de fondo:

> **"Una variable cuyo nombre sugiere unidad UMBAS termina cargando un valor
> en presentacion (o viceversa) porque el codigo no re-ancla a la unidad
> canonica antes de asignar a un destino de Familia A."**

Sintoma observable: `trans_movimientos.cantidad` o `stock_res.cantidad` no
satisface la invariante de Familia A para casos con `IdPresentacion > 0`.

Defensas recomendadas para futuros fixes en BOF:

1. En `Reserva_Stock_*` y `Procesar_*_Desde_BOF`, justo antes del insert/update,
   reconvertir explicitamente a UMBAS basado en la presentacion del request
   original, no en variables intermedias.
2. Agregar `Debug.Assert(cantidad_destino == cantidad_visible * factor OR
   IdPresentacion = 0)` en builds de QA.
3. Auditoria periodica: `SELECT IdMovimiento FROM trans_movimientos m JOIN
   sis_presentacion p ON ... WHERE m.IdPresentacion > 0 AND m.cantidad < p.Factor`
   detecta casos donde la cantidad guardada esta por debajo de un factor (red flag).
4. Mismo patron para `stock_res`: `SELECT IdStockRes FROM stock_res WHERE
   Cantidad > (SELECT cantidad FROM trans_pe_det WHERE ...)` detecta
   sobre-reserva.

### Cuando promover a regla en `replit.md` §4

Los dos fixes BOF estan en local de Codex, sin commit a `dev_2028_merge`.
Cuando se mergeen, corresponde abrir entrada en `replit.md` §4 (candidata
regla 10):

> "Rutinas BOF que insertan/actualizan Familia A (`trans_movimientos`,
> `stock`, `stock_res`) deben (a) ser idempotentes por llave logica del
> dominio y (b) convertir explicitamente a UMBAS antes de persistir si la
> fuente venia en presentacion. No fiar en variables intermedias que mezclan
> unidades."

## Glosario

- **UMBAS**: Unidad de Medida Basica del producto. Standard de Familia A.
- **UMP**: Unidad de Medida del Producto/Pallet (en `Cantidad_UMP`).
- **Factor**: Conversion presentacion → UMBAS. 0/null si no hay presentacion
  (caso valido).
- **CamasPorTarima / CajasPorCama**: Palletizado (referencia, no fijos).

## Referencias

- Origen: `wms-brain/brain/handoffs/2026-05-20-hh-recepcion-pallet-presentacion-cantidad/`
- Auditoria: `wms-brain/brain/handoffs/2026-05-20-audit-hh-cantidad-umbas-pattern/`
- Confirmacion Erik (alcance): chat 2026-05-20 (sintetizado en
  `replit.md` §4 regla 6 y en `.local/skills/wms-tomwms/conventions.md` §2).
- Violacion BOF #1 (VERI Killios):
  `wms-brain/brain/handoffs/2026-05-19-codex-learning-bof-veri-movimientos-duplicados/`
- Violacion BOF #2 (reserva BYB):
  `wms-brain/brain/handoffs/2026-05-19-codex-learning-byb-mi3-reserva-umbas/`
- Patron VERI completo: `wms-brain/brain/code-changes/BOF/PATTERNS-PICKING-VERI.md`
- Patron reserva MI3: `wms-brain/brain/code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md`
