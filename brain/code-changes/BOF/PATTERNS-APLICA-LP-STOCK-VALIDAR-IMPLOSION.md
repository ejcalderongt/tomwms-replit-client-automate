---
tipo: fix
area: packing
fecha: 2026-05-28
ado_commit: 77700ab6a3ab054f3492cc0ecd0b66de0f588831
ado_push_id: 2328
rama: dev_2028_merge
---
# Fix: Conectar ValidarImplosionDirecta en Aplica_LP_Stock

## Problema

`Aplica_LP_Stock` (`clsLnTrans_ubic_hh_det_Partial.vb` L691) declaraba el
parámetro `Optional ByVal ValidarImplosionDirecta As Boolean = False` pero
NUNCA lo usaba en el cuerpo de la función. El bloque de validación de
implosión se ejecutaba **siempre** que hubiera cambio de LP, sin respetar el
parámetro — parámetro completamente muerto.

`Set_LP_Stock` en `TOMHHWS.asmx.vb` L6640 pasaba `True` con la intención
de marcar una implosión directa, pero ese valor no tenía efecto real.

## Root cause

Desconexión entre la declaración del parámetro y la condición que lo debía
usar. El fix del pre-check de LP destino (EJC20260528 Aplica_LP_Stock-LicDestino-NuevoLP)
fue aplicado antes sin incorporar el guard del parámetro.

## Fix aplicado

Archivo: `TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb`

### Diff

```diff
-            If Not String.IsNullOrWhiteSpace(pStockRes.Lic_plate_Anterior) AndAlso
-               Not String.IsNullOrWhiteSpace(vNuevoLicPlate) AndAlso
-               pStockRes.Lic_plate_Anterior.Trim().ToUpper() <> vNuevoLicPlate.Trim().ToUpper() Then
+            '#EJC20260528 Aplica_LP_Stock-ValidarImplosionDirecta:
+            'ValidarImplosionDirecta=True solo cuando el caller es una implosión directa
+            '(Set_LP_Stock). Callers que NO son implosión directa (Aplica_LP_Stock_Mixto)
+            'pasan False (o defaultean False) y omiten toda esta sección.
+            If ValidarImplosionDirecta AndAlso
+               Not String.IsNullOrWhiteSpace(pStockRes.Lic_plate_Anterior) AndAlso
+               Not String.IsNullOrWhiteSpace(vNuevoLicPlate) AndAlso
+               pStockRes.Lic_plate_Anterior.Trim().ToUpper() <> vNuevoLicPlate.Trim().ToUpper() Then
```

## Callers y su comportamiento post-fix

| Caller | Archivo | Pasa ValidarImplosionDirecta | Comportamiento |
|--------|---------|------------------------------|----------------|
| `Set_LP_Stock` | `TOMHHWS.asmx.vb` L6640 | `True` (explícito) | Ejecuta validación de implosión con pre-check de LP destino ✓ |
| `Aplica_LP_Stock_Mixto` | `clsLnTrans_ubic_hh_det_Partial.vb` L878 | No pasa → default `False` | Omite toda la sección de validación ✓ |

## Matriz de comportamiento completa

| `ValidarImplosionDirecta` | LP destino tiene stock | Resultado |
|---|---|---|
| `True` | Sí | Llama `Validar_Implosion_MismaUbicacionEstado` → valida ubicación/estado |
| `True` | No (LP nueva) | Skip `Validar_Implosion` → packing normal ✓ |
| `False` | Cualquiera | Skip bloque entero → no hay validación de implosión |

## Relación con fix anterior (EJC20260528 LicDestino-NuevoLP)

El fix del pre-check (`stockDestinoPreCheck IsNot Nothing`) resuelve el caso
LP nueva con `ValidarImplosionDirecta=True`. Este fix activa el parámetro
para que los callers sin implosión directa no entren al bloque en absoluto.
Ambos fixes son complementarios; sin este segundo fix el pre-check era
alcanzable incluso desde `Aplica_LP_Stock_Mixto`.

## Tarea pendiente (#45)

Hacer explícito el `False` en `Aplica_LP_Stock_Mixto` L878 para que la
intención sea visible al leer el call site sin depender del default.
