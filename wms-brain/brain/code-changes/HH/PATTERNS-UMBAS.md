# Patron: Cantidad siempre en UMBAS para entidades de stock

> Regla canonizada a partir del handoff
> `2026-05-20-hh-recepcion-pallet-presentacion-cantidad`.
> Origen: correccion en vivo de Erik a Codex local.

## La regla

Toda asignacion al campo `Cantidad` de una entidad que vive en la capa
`stock` (cualquier `vBeStock*`, `vBeStockRec*`, `vBeStockPicking*`, etc.)
DEBE estar expresada en **UMBAS** (Unidad de Medida Basica) del producto.

Si la fuente del dato esta en otra unidad (presentacion, pallet, cama,
caja, master, suelta), hay que convertirla explicitamente multiplicando
por el factor correspondiente ANTES de asignarla.

## Por que importa

El error es **silencioso**:

- El stock queda con cantidad menor a la real (dividida por el factor).
- No hay excepcion ni log.
- Se detecta tarde, al ajustar inventario o al pickear y no encontrar
  unidades.

Esto explica una clase entera de descuadres "fantasma" en clientes con
presentaciones distintas a UMBAS.

## Jerarquia de fuente de cantidad (recepcion HH con pallet de proveedor)

```
si presentacion seleccionada tiene Nombre:
    fuente_visible = BeINavBarraPallet.Cantidad_Presentacion
sino:
    fuente_visible = BeINavBarraPallet.Cantidad_UMP   // ya esta en UMBAS

cantidad_umbas = fuente_visible
si presentacion existe:
    cantidad_umbas = cantidad_umbas * Factor
    si presentacion es pallet:
        cantidad_umbas = cantidad_umbas * CamasPorTarima * CajasPorCama

// Destinos
Uds_lic_plate           = fuente_visible      // operador-friendly
mensaje_operador        = fuente_visible      // operador-friendly
vBeStockRecPallet.Cantidad = cantidad_umbas   // SIEMPRE UMBAS
```

## Donde aplica (catalogo en construccion)

### Confirmado afectado (corregido)
| Archivo | Campo destino | Conversion necesaria |
|---|---|---|
| `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java` | `vBeStockRecPallet.Cantidad` | si presentacion: `Cantidad_Presentacion * Factor [* CamasPorTarima * CajasPorCama]` |

### Sospechoso (pendiente auditoria — abrir handoff)
| Archivo | Razon |
|---|---|
| `frm_recepcion_pallet_*.java` (variantes) | Mismo patron de escaneo de pallet |
| `frm_recepcion_caja_*.java` (variantes) | Asignacion de cantidad por caja |
| Cualquier `frm_picking_*` que escriba a `vBeStockPicking*.Cantidad` | Misma capa de stock, mismo riesgo |

## Defensa minima recomendada

Antes de asignar `cantidad_umbas`:

```java
if (Factor == null || Factor <= 0) {
    // abortar con mensaje claro al operador, NO continuar con 0
    mostrarError("Factor de conversion invalido para el producto X");
    return;
}
```

Lo mismo para `CamasPorTarima` y `CajasPorCama` cuando aplica nivel pallet.
Convencion sugerida: default a 1 si null/0, **solo** si Erik confirma que
operativamente es aceptable. Sino, abortar.

## Suposiciones pendientes de validar (de LEARNINGS)

- [ ] `Factor` siempre > 0 desde el maestro.
- [ ] `CamasPorTarima`/`CajasPorCama` defaultean a 1 vs abortan.
- [ ] La regla aplica a TODOS los `vBeStock*` del core (no solo recepcion).

## Referencias

- Handoff origen: `wms-brain/brain/handoffs/2026-05-20-hh-recepcion-pallet-presentacion-cantidad/`
- BeINavBarraPallet: entidad de barra/pallet del proveedor en HH.
- UMBAS: Unidad de Medida Basica del producto. Standard del core para todo
  lo que vive en `stock`.
- UMP: Unidad de Medida del Producto / Pallet (a confirmar nomenclatura
  exacta en `.local/skills/wms-tomwms/conventions.md`).
