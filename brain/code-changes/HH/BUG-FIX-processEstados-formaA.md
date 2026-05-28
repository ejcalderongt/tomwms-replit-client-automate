---
tipo: bug-fix
area: HH
archivos:
  - app/src/main/java/com/dts/base/WebService.java (TOMHH2025)
  - app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_inv_cic_add.java (TOMHH2025)
commit: 11182a0d3c898d0532aca57b1ed72e3cbd0f0583
rama: dev_2028_merge
fecha: 2026-05-28
autor: EJC
---

# BUG-FIX: processEstados — Error "Expected BEGIN_ARRAY but was BEGIN_OBJECT at $.items"

## Síntoma
Al abrir el spinner de estados en frm_inv_cic_add (inventario cíclico), aparecía:
`processEstados: Expected BEGIN_ARRAY but was BEGIN_OBJECT at line 1 column 11 path $.items`

## Causa Raíz

**Interacción entre `normalizeJsonCollections` y parseo con string-replace frágil.**

1. WS `Get_Estados_By_IdPropietario_JSON` devuelve correctamente: `{"data": [array de estados]}`
2. `callMethodJsonPost` pasa el response por `normalizeJsonCollections` → `normalizeNode`
3. `normalizeNode` transforma CUALQUIER propiedad cuyo valor sea JSONArray (excepto "items"):
   ```
   {"data": [...]} → {"data": {"items": [...], "data": [...]}}
   ```
4. El antiguo `processEstados` usaba: `ws.xmlresult.replace("\"data\"", "\"items\"")`
   Resultado: `{"items": {"items": [...], "items": [...]}}` — objeto en lugar de array
5. Gson intenta parsear `$.items` como `List<clsBeProducto_estado>` pero encuentra un objeto
   → **CRASH**

## Fixes Aplicados

### Fix 1 — WebService.java (normalizeNode)
Agregar "data" a la lista de claves que se omiten del wrapping:
```java
// ANTES:
if ("items".equalsIgnoreCase(key)) { obj.put(key, arr); continue; }

// DESPUÉS:
if ("items".equalsIgnoreCase(key) || "data".equalsIgnoreCase(key)) {
    obj.put(key, arr);
    continue;
}
```
**Razón**: "data" es la clave Forma A — nunca debe ser envuelta. El wrapping es para propiedades
de DTO internos que exponen colecciones sin wrapper.

### Fix 2 — frm_inv_cic_add.java (processEstados)
Reemplazar replace frágil con parseo robusto de Forma A:
```java
com.google.gson.JsonObject _formaA = new Gson().fromJson(ws.xmlresult, com.google.gson.JsonObject.class);
com.google.gson.JsonElement _dataEl = _formaA != null ? _formaA.get("data") : null;
clsBeProducto_estadoList listaEstados = null;
if (_dataEl != null && !_dataEl.isJsonNull()) {
    if (_dataEl.isJsonObject()) {
        // normalizeJsonCollections wrapeó el array: {"items":[...],"data":[...]}
        listaEstados = new Gson().fromJson(_dataEl, clsBeProducto_estadoList.class);
    } else if (_dataEl.isJsonArray()) {
        // Respuesta directa sin wrapping: [...]
        listaEstados = new clsBeProducto_estadoList();
        listaEstados.items = new Gson().fromJson(_dataEl,
            new com.google.gson.reflect.TypeToken<java.util.List<clsBeProducto_estado>>(){}.getType());
    }
}
```

## Patrón para futuros parseos JSON con callMethodJsonPost

Todo código que llame `callMethodJsonPost` y luego lea `ws.xmlresult` directamente
debe usar parseo explícito de Forma A — NO string-replace:

```java
// PATRÓN CORRECTO para parsear Forma A desde ws.xmlresult
com.google.gson.JsonObject root = new Gson().fromJson(ws.xmlresult, com.google.gson.JsonObject.class);
com.google.gson.JsonElement data = root != null ? root.get("data") : null;
if (data != null && data.isJsonObject()) {
    MyList obj = new Gson().fromJson(data, MyList.class); // MyList tiene campo "items"
} else if (data != null && data.isJsonArray()) {
    // parsear directamente como lista
}
```

## Archivos con patrón similar pendiente de revisión
- Cualquier `*.java` en TOMHH2025 que use `ws.xmlresult.replace("\"data\"", ...)` es candidato.
  (Code search confirmó 0 hits adicionales en dev_2028_merge.)
