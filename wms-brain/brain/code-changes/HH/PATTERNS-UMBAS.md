# Patron: Cantidad en UMBAS para `stock`, `movimientos`, `stock_res`

> Regla canonizada y **matizada** a partir de:
> - Handoff `2026-05-20-hh-recepcion-pallet-presentacion-cantidad` (origen).
> - Confirmacion de Erik en chat 2026-05-20 (alcance real).
>
> Reemplaza la version anterior de este archivo (commit `b7aea6c0fa8a`) que
> era demasiado generalista. La regla NO es "siempre UMBAS para todo stock";
> tiene matices por familia de tablas.

## La regla, acotada por familia

### Familia A — SIEMPRE UMBAS (HH y BOF)

- `stock`
- `movimientos` (familia `trans_mov_*`, `mov_stock`, etc.)
- `stock_res` (reservas)

En estas la `Cantidad` persistida esta SIEMPRE en UMBAS. Si el producto tiene
presentacion (`IdPresentacion > 0`), la **interpretacion** al usuario es
`Cantidad / Factor`, pero el dato guardado sigue en UMBAS.

Consecuencia: todos los `vBeStock*`, `vBeStockRec*`, `vBeStockPicking*` que
alimentan estas tres familias deben tener `Cantidad` en UMBAS al momento de
persistirse.

### Familia B — depende de presentacion (operativas)

- `trans_picking_ubic`
- Otras operativas similares.

Si el producto tiene presentacion: se guarda en la unidad de presentacion.
Si no tiene presentacion: se guarda en UMBAS.

### Familia C — operador-friendly (siempre presentacion si existe)

- `Uds_lic_plate`
- Mensajes al operador en HH.
- Reportes orientados a usuario final.

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
