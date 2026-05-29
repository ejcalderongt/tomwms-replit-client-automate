---
protocolVersion: 1
slug: 2026-05-28-hh-packing-fecha-vence-lic-plate
estado: aplicado
area: HH
tipo: bugfix
prioridad: alta
autores: [replit]
createdAt: 2026-05-28T21:00:00Z
cliente: LA_CUMBRE_QA
picking: 3559
pedido: 7623
---

# Bug HH Packing — doble mismatch Fecha_vence + Lic_plate

## Contexto

Flujo: Picking → Verificación → Packing en `frm_preparacion_packing.java`.

Síntoma reportado: al entrar a la pantalla de packing después de verificar
y empacar una línea, el contador mostraba **PENDIENTE: 4, PROCESADO: 0**
en lugar de reflejar el estado real. La línea ya empacada seguía apareciendo
como pendiente. Al reingresar a la lista de la LP, el producto mostraba una
licencia incorrecta (LP de packing en lugar de LP de picking).

## Causas raíz (dos simultáneas)

### Causa 1 — `fecha_vence` con distinto formato entre ubic y packing_enc

| Objeto Java | Getter | Valor devuelto por WS |
|---|---|---|
| `clsBeTrans_picking_ubic` | `getFecha_Vence()` | `"2028-06-11T00:00:00"` (con hora) |
| `clsBeTrans_packing_enc` | `getFecha_vence()` | `"2028-06-11"` (solo fecha) |

La comparación era `obj.Fecha_Vence.equals(packing.Fecha_vence)` →
`"2028-06-11T00:00:00".equals("2028-06-11")` → **false**.
Resultado: `cant` nunca acumulaba, siempre quedaba en 0 → pendiente.

Raíz en clases Java:
- `clsBeTrans_picking_ubic.java`: `public String Fecha_Vence="1900-01-01T00:00:00"` (default con hora).
- `clsBeTrans_packing_enc.java`: `public String Fecha_vence;` (sin default, WS devuelve date-only).

### Causa 2 — WS devuelve `No_linea` del packing como `Lic_plate` del ubic para ubics ya empacados

Para un ubic cuya línea ya fue empacada, el WS
`Get_All_PickingUbic_By_PickingEnc` devolvía la LP de packing
(`MM000021393`) en el campo `Lic_plate` del ubic, pero
`trans_packing_enc.Lic_plate` almacenaba la LP de origen (`JJ000020918`).
Comparación `obj.Lic_plate.equals(packing.Lic_plate)` → **false**.

## Fix aplicado — commit `1fb9f12` (TOMHH2025, dev_2028_merge)

Archivo: `app/src/main/java/com/dts/tom/Transacciones/Packing/frm_preparacion_packing.java`

### En `listItems()` (líneas ~496-504 post-patch)

```java
String fvObj  = obj.Fecha_Vence != null && obj.Fecha_Vence.length() >= 10
                ? obj.Fecha_Vence.substring(0, 10) : obj.Fecha_Vence;
String fvPack = packing.Fecha_vence != null && packing.Fecha_vence.length() >= 10
                ? packing.Fecha_vence.substring(0, 10) : packing.Fecha_vence;
boolean licMatch = obj.Lic_plate.equals(packing.Lic_plate)
                || (packing.No_linea != null && obj.Lic_plate.equals(packing.No_linea));
if (licMatch && fvObj.equals(fvPack) && ...) { cant += packing.Cantidad_bultos_packing; }
```

### En `creaListaLotes()` — stream (líneas ~931-939 post-patch)

```java
String fvUbic = obj.getFecha_Vence() != null && obj.getFecha_Vence().length() >= 10
                ? obj.getFecha_Vence().substring(0, 10) : obj.getFecha_Vence();
items.stream()
  .filter(p -> p.getLic_plate().equals(obj.Lic_plate)
            || (p.getNo_linea() != null && p.getNo_linea().equals(obj.Lic_plate)))
  .filter(p -> p.getIdproductobodega() == obj.IdProductoBodega)
  .filter(p -> { String fvP = p.getFecha_vence() != null && p.getFecha_vence().length() >= 10
                              ? p.getFecha_vence().substring(0, 10) : p.getFecha_vence();
                 return fvUbic.equals(fvP); })
  .filter(p -> p.getIdproductoestado() == obj.getIdProductoEstado())
  .mapToDouble(clsBeTrans_packing_enc::getCantidad_bultos_packing).sum();
```

## Comportamiento esperado post-fix

- Antes de empacar: `obj.Lic_plate = JJ000020918 = packing.Lic_plate` → match por `Lic_plate` ✓
- Después de empacar: `obj.Lic_plate = MM000021394 = packing.No_linea` → match por `No_linea` ✓
- `fecha_vence` siempre normalizada a primeros 10 chars → `"2028-06-11" = "2028-06-11"` ✓
- Resultado: PENDIENTE: 0, PROCESADO: 4 al completar todas las líneas ✓

## Fixes previos relacionados (misma sesión, commits BOF)

| Commit | Repo | Fix |
|---|---|---|
| `2d845e6` | TOMWMS_BOF | `Get_All_PickingUbic_For_Packing` filtro `fecha_packing < '19010101'` |
| `5d7fb80f` | TOMWMS_BOF | `Inserta_Packing` INSERT/UPDATE separado + reset `Fecha_packing` en DELETE |
| `4eba8be7` | TOMHH2025 | `creaListaLotes` guard `neto<=0` + `listItems` contador `procesados` independiente |
| `1fb9f12` | TOMHH2025 | Este fix (fecha_vence + Lic_plate/No_linea) |
