---
protocolVersion: 1
id: L-019
title: Packing HH — doble mismatch fecha_vence (con/sin hora) y Lic_plate (picking vs packing LP) rompe contador PENDIENTE/PROCESADO
operator: agent-replit
operatorRole: developer
createdAt: 2026-05-28T21:00:00Z
target:
  codename: tomwms-hh-packing
  environment: cross-cliente
refs:
  - handoff: 2026-05-28-hh-packing-fecha-vence-lic-plate
  - commits: [4eba8be7, 1fb9f125]
  - archivo: app/src/main/java/com/dts/tom/Transacciones/Packing/frm_preparacion_packing.java
---

# L-019 — Packing HH: doble mismatch fecha_vence + Lic_plate

## El problema

En `frm_preparacion_packing.java`, los métodos `listItems()` y `creaListaLotes()`
comparan ubics (`clsBeTrans_picking_ubic`) con registros de packing_enc
(`clsBeTrans_packing_enc`) usando `.equals()` directo en dos campos que
**el WS devuelve con formato diferente según el objeto**:

### Mismatch 1: fecha_vence

| Clase | Campo | Formato WS |
|---|---|---|
| `clsBeTrans_picking_ubic` | `Fecha_Vence` | `"2028-06-11T00:00:00"` (ISO con hora) |
| `clsBeTrans_packing_enc` | `Fecha_vence` | `"2028-06-11"` (date-only) |

`"2028-06-11T00:00:00".equals("2028-06-11")` → **false**.
La diferencia viene del default Java: `picking_ubic` tiene default `"1900-01-01T00:00:00"`,
`packing_enc` no tiene default — el WS de cada tabla usa formato distinto.

### Mismatch 2: Lic_plate post-empaque

El WS `Get_All_PickingUbic_By_PickingEnc` devuelve `No_linea` del packing
(e.g. `MM000021394`) en el campo `Lic_plate` del ubic cuando ese ubic
ya fue empacado. Pero `trans_packing_enc.Lic_plate` guarda la LP de
origen del picking (e.g. `JJ000020918`).
`obj.Lic_plate.equals(packing.Lic_plate)` → **false**.

### Efecto combinado

Ambos mismatches simultáneos → el ubic empacado nunca encuentra su
`packing_enc` → `cant = 0` → siempre pendiente → **PENDIENTE: N, PROCESADO: 0**.

## El fix

Normalizar antes de comparar en ambas funciones:

```java
// fecha_vence: tomar solo primeros 10 chars de ambos lados
String fvObj  = obj.Fecha_Vence != null && obj.Fecha_Vence.length() >= 10
                ? obj.Fecha_Vence.substring(0, 10) : obj.Fecha_Vence;
String fvPack = packing.Fecha_vence != null && packing.Fecha_vence.length() >= 10
                ? packing.Fecha_vence.substring(0, 10) : packing.Fecha_vence;

// Lic_plate: match por LP origen O por No_linea del packing (null-safe)
boolean licMatch = obj.Lic_plate.equals(packing.Lic_plate)
                || (packing.No_linea != null && obj.Lic_plate.equals(packing.No_linea));
```

## Regla derivada

**Siempre que se comparen campos de fecha entre `clsBeTrans_picking_ubic`
y `clsBeTrans_packing_enc`, normalizar a `substring(0,10)` antes del `.equals()`.**

**Siempre que se compare `Lic_plate` de un ubic con el `Lic_plate` de un packing_enc,
incluir también `No_linea` como fallback — el WS puede devolver cualquiera de los dos.**

## Señales de alerta

- PENDIENTE: N, PROCESADO: 0 al abrir la pantalla de packing con líneas ya empacadas.
- La lista de pendientes muestra una LP de packing (ej. MM...) en lugar de la LP de picking (ej. JJ...).
- `cant` siempre = 0 tras iterar `items` aunque BD tenga registros de packing.
