---
tipo: pattern
area: HH
modulo: packing
archivo: frm_preparacion_packing.java
autores: [replit]
createdAt: 2026-05-28T21:00:00Z
refs:
  - L-019
  - handoff: 2026-05-28-hh-packing-fecha-vence-lic-plate
---

# Patrón: matching ubic ↔ packing_enc en frm_preparacion_packing

## Contexto

`listItems()` y `creaListaLotes()` necesitan cruzar:
- `List<clsBeTrans_picking_ubic>` (ubics del picking)
- `List<clsBeTrans_packing_enc>` (líneas de packing ya empacadas)

para determinar si un ubic está completamente empacado (`cant == neto`).

## Campos de cruce y sus trampas

| Campo | Clase ubic | Clase packing_enc | Trampa |
|---|---|---|---|
| `Fecha_Vence` / `Fecha_vence` | Con hora: `"T00:00:00"` | Sin hora: date-only | `.equals()` falla |
| `Lic_plate` | LP picking O LP packing (según WS) | LP de picking original | WS puede devolver `No_linea` |
| `Lote` | puede ser null | puede ser null | comparar con null-safe |
| `IdProductoBodega` | int | int | OK directo |
| `IdProductoEstado` | int | int | OK directo |

## Patrón correcto (copiar literalmente)

### En `listItems()` — bucle for

```java
String fvObj  = obj.Fecha_Vence != null && obj.Fecha_Vence.length() >= 10
                ? obj.Fecha_Vence.substring(0, 10) : obj.Fecha_Vence;
String fvPack = packing.Fecha_vence != null && packing.Fecha_vence.length() >= 10
                ? packing.Fecha_vence.substring(0, 10) : packing.Fecha_vence;
boolean licMatch = obj.Lic_plate.equals(packing.Lic_plate)
                || (packing.No_linea != null && obj.Lic_plate.equals(packing.No_linea));
String vLotePacking = (packing.Lote == null ? "" : packing.Lote);

if (licMatch
    && fvObj.equals(fvPack)
    && obj.IdProductoBodega == packing.getIdproductobodega()
    && obj.IdProductoEstado == packing.Idproductoestado
    && (obj.Lote == null || obj.Lote.equals(vLotePacking))) {
    cant += packing.Cantidad_bultos_packing;
    cant = mu.round(cant, 6);
}
```

### En `creaListaLotes()` — stream

```java
String fvUbic = obj.getFecha_Vence() != null && obj.getFecha_Vence().length() >= 10
                ? obj.getFecha_Vence().substring(0, 10) : obj.getFecha_Vence();

double totalCantidad = items.stream()
    .filter(p -> p.getLote() == null || p.getLote().equals(obj.Lote))
    .filter(p -> p.getLic_plate().equals(obj.Lic_plate)
              || (p.getNo_linea() != null && p.getNo_linea().equals(obj.Lic_plate)))
    .filter(p -> p.getIdproductobodega() == obj.IdProductoBodega)
    .filter(p -> {
        String fvP = p.getFecha_vence() != null && p.getFecha_vence().length() >= 10
                     ? p.getFecha_vence().substring(0, 10) : p.getFecha_vence();
        return fvUbic.equals(fvP);
    })
    .filter(p -> p.getIdproductoestado() == obj.getIdProductoEstado())
    .mapToDouble(clsBeTrans_packing_enc::getCantidad_bultos_packing)
    .sum();
```

## Anti-patrones a evitar

```java
// MAL: formato diferente → always false para fechas con hora
obj.Fecha_Vence.equals(packing.Fecha_vence)

// MAL: post-empaque el WS devuelve No_linea en Lic_plate → always false
obj.Lic_plate.equals(packing.Lic_plate)

// MAL: NullPointerException si packing.No_linea es null
obj.Lic_plate.equals(packing.No_linea)
```

## Commits de referencia

| SHA | Descripción |
|---|---|
| `4eba8be7` | guard neto<=0 + contador procesados independiente |
| `1fb9f125` | normalización fecha_vence + Lic_plate/No_linea |
