---
tipo: verificacion
area: packing
fecha: 2026-05-28
rama: dev_2028_merge
db_verificacion: TOMWMS_MAMPA_QA (TOMWMS_KILLIOS_PRD no accesible desde sandbox)
---
# Verificación: Fix implosión en escenario multi-presentación

## Contexto

El fix del pre-check en `Aplica_LP_Stock` (`clsLnTrans_ubic_hh_det_Partial.vb` L711-740)
se probó originalmente con LP FU09122 / WMS100 CHAMPINONES KILLIOS / Presentación 200-Caja6.
Esta verificación cubre los tres escenarios de la tarea #44 y analiza el comportamiento
cuando la LP destino tiene un `IdPresentacion` distinto al origen.

---

## Análisis de código (dev_2028_merge)

### `Get_Stock_Implosion_By_LicPlate` — query exacta del código

```sql
SELECT TOP 1 Lic_Plate, IdUbicacion, Nombre_Completo, IdProductoEstado, NomEstado
FROM VW_Stock_Res
WHERE IdBodega = @IdBodega
  AND Lic_Plate = @LicPlate
  AND Activo = 1
  AND ISNULL(Bloqueada, 0) = 0
  AND ISNULL(Disponible_UMBas, 0) > 0
```

**Observación clave:** La query NO filtra por `IdPresentacion`. Esto es intencional:
el pre-check solo determina si la LP destino *existe con stock*, independientemente
de qué producto o presentación tenga.

---

## Escenarios verificados

### Escenario 1 — LP destino nueva con presentación distinta

**Flujo en código:**
1. `Aplica_LP_Stock` L709: `vPresentacion = pStockRes.IdPresentacion` (presentación nueva, p.ej. 200)
2. L720-723: `ValidarImplosionDirecta=True` AND LP cambió → entra al bloque
3. L725-729: `Get_Stock_Implosion_By_LicPlate(vNuevoLicPlate, ...)` → **devuelve Nothing** (LP nueva, sin filas en VW_Stock_Res)
4. L731: `If stockDestinoPreCheck IsNot Nothing` → **False** → skip `Validar_Implosion`
5. L757: `pStockRes.IdPresentacion = vPresentacion` → presentación nueva restaurada correctamente
6. Packing procede normalmente ✓

**Evidencia DB** (query replicando Get_Stock_Implosion_By_LicPlate contra TOMWMS_MAMPA_QA):

```sql
-- LP inexistente: devuelve 0 rows → Nothing en VB.NET
SELECT TOP 1 Lic_Plate, IdUbicacion, Nombre_Completo, IdProductoEstado, NomEstado
FROM VW_Stock_Res
WHERE IdBodega = 1
  AND Lic_Plate = 'LP_NUEVA_NO_EXISTE_TEST_9999'
  AND Activo = 1 AND ISNULL(Bloqueada,0)=0 AND ISNULL(Disponible_UMBas,0) > 0
-- Resultado: 0 rows ✓
```

**Conclusión:** LP nueva con cualquier `IdPresentacion` (incluido distinto al origen)
→ validación **OMITIDA** → no hay error falso de implosión ✓

---

### Escenario 2 — LP destino existente, misma ubicación y estado

**Flujo en código:**
1. `Get_Stock_Implosion_By_LicPlate` devuelve objeto con stock (LP existe)
2. `stockDestinoPreCheck IsNot Nothing` → True → ejecuta `Validar_Implosion_MismaUbicacionEstado`
3. `Validar_Implosion_MismaUbicacionEstado`: compara `stockOrigen.IdUbicacion` vs `stockDestino.IdUbicacion` → iguales
4. Compara `stockOrigen.IdProductoEstado` vs `stockDestino.IdProductoEstado` → iguales
5. Validación pasa → implosión real procede ✓

**Evidencia DB** (TOMWMS_MAMPA_QA, pares LP con misma ubicación y estado, ambos con stock):

| LP_Origen | LP_Destino | Ubic_Comun | Estado_Comun | NomEstado | Ubicacion | Resultado_Esperado |
|---|---|---|---|---|---|---|
| CK00000126 | CK0000125 | 9 | 3 | Buen Estado | RECEPCIÓN - #9 | IMPLOSION_REAL_VALIDA |
| CK00000126 | CK00000129 | 9 | 3 | Buen Estado | RECEPCIÓN - #9 | IMPLOSION_REAL_VALIDA |
| CK00000129 | CK0000125 | 9 | 3 | Buen Estado | RECEPCIÓN - #9 | IMPLOSION_REAL_VALIDA |

**Conclusión:** Validación de misma ubicación+estado se ejecuta y pasa correctamente ✓

---

### Escenario 3 — LP destino existente, diferente ubicación o estado

**Flujo en código:**
1. `Get_Stock_Implosion_By_LicPlate` devuelve objeto (LP existe)
2. `Validar_Implosion_MismaUbicacionEstado`: detecta `IdUbicacion` o `IdProductoEstado` diferente
3. Lanza excepción: "No se puede implosionar, ubicaciones diferentes. Origen: X, Destino: Y." ✓

**Evidencia DB** (TOMWMS_MAMPA_QA, pares LP en distintas ubicaciones):

| LP_Origen | Ubic_Origen | LP_Destino | Ubic_Destino | Razon_Bloqueo | Resultado_Esperado |
|---|---|---|---|---|---|
| J30000064 | 6 | 0 | 3 | Ubicaciones diferentes | IMPLOSION_INVALIDA_BLOQUEADA |
| 0 | 3 | J100000158 | 6 | Ubicaciones diferentes | IMPLOSION_INVALIDA_BLOQUEADA |

**Conclusión:** Implosión inválida bloqueada correctamente ✓

---

## Comportamiento de `vPresentacion` durante packing multi-presentación

El flujo en `Aplica_LP_Stock` maneja la presentación correctamente:

```vbnet
' L709: captura presentación destino ANTES de manipular pStockRes
Dim vPresentacion As Integer = pStockRes.IdPresentacion

' L742-743: restaura Lic_plate y IdPresentacion al estado anterior (para buscar stock origen)
pStockRes.Lic_plate = pStockRes.Lic_plate_Anterior
pStockRes.IdPresentacion = pStockRes.IdPresentacion_Anterior

' ... búsqueda de stock por Lic_plate_Anterior ...

' L756-757: restaura LP y presentacion NUEVOS para insertar el stock destino
pStockRes.Lic_plate = vNuevoLicPlate
pStockRes.IdPresentacion = vPresentacion  ' ← presentación destino nueva, preservada
```

Si `IdPresentacion` origen ≠ `IdPresentacion` destino, el stock resultante en la
LP destino usa correctamente la presentación nueva. Esto es consistente con el
comentario de `Aplica_Implosion` L1408-1409 (`#CKFK20260518: la presentacion no se
debe cambiar`), que refleja la misma intención de diseño.

---

## Gap identificado (pre-existente, fuera del scope de este fix)

`Validar_Implosion_MismaUbicacionEstado` NO verifica `IdPresentacion`. Si LP destino
EXISTS con stock y tiene **diferente presentación** pero misma ubicación/estado, la
implosión procedería sin error. La query de evidencia en MAMPA_QA no encontró ningún
caso real de este escenario (0 pares), lo que indica que en la práctica operativa una
LP tiene una sola presentación. Sin embargo, el gap existe a nivel de código y está
propuesto como tarea de seguimiento (#47).

---

## Resumen de resultados

| Escenario | Código | Evidencia DB | Resultado |
|---|---|---|---|
| LP nueva (cualquier presentación) | `stockDestinoPreCheck Is Nothing` → skip | 0 rows en VW_Stock_Res para LP inexistente | No hay error falso ✓ |
| LP existente, misma ubic/estado | `Validar_Implosion` ejecuta y pasa | 3 pares (CK00000126/CK0000125/CK00000129) | Implosión real válida ✓ |
| LP existente, diferente ubic/estado | `Validar_Implosion` lanza excepción | 2 pares (J30000064, J100000158) | Bloqueada correctamente ✓ |
| LP existente, misma ubic/estado, distinta presentación | `Validar_Implosion` pasa (no chequea pres) | 0 casos reales en MAMPA_QA | Gap pre-existente (#47) |
| Caller `Aplica_LP_Stock_Mixto` | `ValidarImplosionDirecta=False` → bloque completo skipped | N/A (lógica de parámetro) | No afectado ✓ |
