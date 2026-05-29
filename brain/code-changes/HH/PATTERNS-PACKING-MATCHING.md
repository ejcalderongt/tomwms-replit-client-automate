---
tipo: pattern
area: HH
modulo: packing
archivo: frm_preparacion_packing.java
autores: [replit, ejc]
createdAt: 2026-05-28T21:00:00Z
updatedAt: 2026-05-29T00:00:00Z
refs:
  - L-019
  - handoff: 2026-05-28-hh-packing-fecha-vence-lic-plate
  - tag: EJC20260529-fix-procesado-cero-pendcount-savedlist
  - BOF/PATTERNS-PACKING-INSERTA.md
---

# Patrón: matching ubic ↔ packing_enc en frm_preparacion_packing

## Contexto

`listItems()` y `creaListaLotes()` necesitan cruzar:
- `List<clsBeTrans_picking_ubic>` (ubics del picking, `pick.items`) — items con `fecha_packing < '19010101'` (sin empacar o packing parcial)
- `List<clsBeTrans_packing_enc>` (líneas de packing ya confirmadas, `items` / savedList) — items completamente empacados en `trans_packing_enc`

para determinar si un ubic está completamente empacado (`cant == neto`).

**Importante:** `pick.items` y `items` son conjuntos complementarios, NO solapados:
- Ubics completamente empacados → `fecha_packing = Date.Now()` → **solo en `items`** (savedList)
- Ubics pendientes o parciales → `fecha_packing = 1900-01-01` → **solo en `pick.items`**

Ver `BOF/PATTERNS-PACKING-INSERTA.md` para el contrato de `fecha_packing` en el lado BOF.

---

## Campos de cruce y sus trampas

| Campo | Clase ubic | Clase packing_enc | Trampa |
|---|---|---|---|
| `Fecha_Vence` / `Fecha_vence` | Con hora: `"T00:00:00"` | Sin hora: date-only | `.equals()` falla |
| `Lic_plate` | LP picking O LP packing (según WS) | LP de picking original | WS puede devolver `No_linea` |
| `Lote` | puede ser null | puede ser null | comparar con null-safe |
| `IdProductoBodega` | int | int | OK directo |
| `IdProductoEstado` | int | int | OK directo |

---

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

---

## pendCount — contadores PENDIENTE y PROCESADO

`pendCount()` en `frm_preparacion_packing.java` calcula los totales visuales.

### Lógica correcta (post fix 2026-05-29)

```java
pendientes = 0;
int procesados = 0;

// 1. Iterar pick.items (ubics sin empacar o packing parcial)
for (clsBeTrans_picking_ubic obj : pick.items) {
    double neto = mu.round(obj.Cantidad_Verificada - obj.getCantidad_despachada(), 6);
    if (neto <= 0) continue;  // ubic ya despachado — saltar

    double cant = 0;
    // sumar lo empacado para este ubic (ver patrón matching arriba)
    for (clsBeTrans_packing_enc packing : items) { ... cant += ... }

    if (neto != cant) pendientes++;
    else procesados++;
}

// 2. Agregar items del savedList (completamente empacados, no están en pick.items)
// #EJC20260529 fix: sin esta línea PROCESADO siempre era 0
procesados += items.size();

lblProc.setText("Procesado : " + procesados);
lblPend.setText("Pendiente : " + pendientes);
```

### Por qué PROCESADO era siempre 0 (bug)

`pick.items` solo contiene ubics con `fecha_packing < '19010101'`. Los ubics
completamente empacados tienen `fecha_packing = Date.Now()` → **no están** en
`pick.items` → el loop nunca los ve → `procesados` se queda en 0.

`items` (savedList) contiene los packing_enc de los ubics ya empacados.
Sumar `items.size()` los cuenta correctamente.

Fix commit: `15b797462e` (TOMHH2025 dev_2028_merge, 2026-05-29).

---

## Anti-patrones a evitar

```java
// MAL: formato diferente → always false para fechas con hora
obj.Fecha_Vence.equals(packing.Fecha_vence)

// MAL: post-empaque el WS devuelve No_linea en Lic_plate → always false
obj.Lic_plate.equals(packing.Lic_plate)  // sin fallback No_linea

// MAL: NullPointerException si packing.No_linea es null
obj.Lic_plate.equals(packing.No_linea)

// MAL: PROCESADO siempre 0 — savedList no está en pick.items
procesados = <conteo solo desde pick.items>
// CORRECTO: sumar items.size() al final del loop
```

---

## Commits de referencia

| SHA | Repo | Descripción |
|---|---|---|
| `4eba8be7` | HH | guard neto<=0 + contador procesados independiente |
| `1fb9f125` | HH | normalización fecha_vence + Lic_plate/No_linea |
| `15b797462e` | HH | PROCESADO=0 fix: `procesados += items.size()` |
| `fba0a430d4` | BOF | Packing parcial: guard CantidadEmpacada >= Cantidad_Verificada |
| `16d80228c6` | BOF | BUG-004: Cargar() fecha_packing NULL→sentinel |
