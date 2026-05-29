---
tipo: pattern
area: BOF
modulo: packing
archivos:
  - TOMIMSV4/DAL/Transacciones/Packing/clsLnTrans_packing_enc_Partial.vb
  - TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic.vb
  - TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb
autores: [replit, ejc]
createdAt: 2026-05-29T00:00:00Z
refs:
  - handoff: 2026-05-28-hh-packing-fecha-vence-lic-plate
  - tag: EJC20260529-BUG004-fecha-packing-poison-verificacion
  - tag: EJC20260529-fix-packing-parcial-inserta-packing-guard
  - tag: packing-estable-cumbre-aws-ejc-ckfk
---

# Patrones BOF: Inserta_Packing y contrato fecha_packing

## 1. Sentinel de fecha_packing

`trans_picking_ubic.fecha_packing` usa `1900-01-01` como valor centinela de
"aún no empacado". **Nunca** debe ser NULL.

| Estado | Valor en BD |
|---|---|
| Sin empacar | `1900-01-01` (o `1900-01-01 00:00:00`) |
| Empacado (completo) | Fecha real del sistema (`Date.Now()`) |

### Por qué importa

`Get_All_PickingUbic_By_IdPickingEnc` y `Get_All_PickingUbic_For_Packing`
filtran con:
```sql
(fecha_packing IS NULL OR fecha_packing < '19010101')
```
Si un ubic tiene `fecha_packing = Date.Now()`, **desaparece** de los resultados
aunque aún tenga cantidad pendiente de empacar.

### Regla crítica: nunca hacer masa-update a Date.Now() en QA

Al limpiar datos QA de packing:
```sql
-- CORRECTO: reset a sentinel
UPDATE trans_picking_ubic
SET fecha_packing = '19000101', user_mod = 'EJC_QA_RESET', fec_mod = GETDATE()
WHERE IdPickingEnc = @picking AND fecha_packing >= '19010101';

-- MAL: bloquea Inserta_Packing para los ubics pendientes
UPDATE trans_picking_ubic SET fecha_packing = GETDATE() WHERE IdPickingEnc = @picking;
```

---

## 2. Cargar() — default para NULL

En `clsLnTrans_picking_ubic.vb`, la función `Cargar()` que mapea un DataRow
a `clsBeTrans_picking_ubic` **debe** defaultear `fecha_packing` NULL al sentinel,
**nunca** a `Date.Now()`.

```vb
' CORRECTO (#EJC20260529 BUG-004 fix)
.Fecha_packing = IIf(IsDBNull(dr.Item("fecha_packing")), New Date(1900, 1, 1), dr.Item("fecha_packing"))

' MAL — envenena el ubic: Actualizar() lo escribe en BD y el ubic desaparece de PENDIENTE
.Fecha_packing = IIf(IsDBNull(dr.Item("fecha_packing")), Date.Now, dr.Item("fecha_packing"))
```

**Por qué es peligroso `Date.Now`:** Si `Cargar()` se llama antes de `Actualizar()`,
la fecha envenenada se persiste en BD, bloqueando el ubic permanentemente en el
filtro `< '19010101'`.

Guard de defensa en `Actualizar_PickingUbic_Por_Verificacion`:
```vb
' VERIF_FP_GUARD: detectar fecha_packing envenenada antes de escribir
If oBeTrans_picking_ubic.Fecha_packing > New Date(1901, 1, 1) Then
    WmsTrace.Trace(...)
    oBeTrans_picking_ubic.Fecha_packing = New Date(1900, 1, 1)
End If
```

---

## 3. Inserta_Packing — guard de packing parcial vs completo

**Archivo:** `clsLnTrans_packing_enc_Partial.vb` — función `Inserta_Packing`

Solo estampar `fecha_packing = Date.Now()` cuando la cantidad total empacada
**>=** `Cantidad_Verificada` del ubic. Para packing parcial, mantener el sentinel.

```vb
' #EJC20260529 PACK_PARCIAL_GUARD — llamar DESPUÉS de Insertar() para incluir el registro nuevo
Dim CantidadEmpacada As Double = Get_CantidadEmpacada(BeTransPackingEnc, lConnection, lTransaction)
For Each Picking As clsBeTrans_picking_ubic In ListaPikcing
    If CantidadEmpacada >= Picking.Cantidad_Verificada Then
        Picking.Fecha_packing = Date.Now()       ' completo → sellar
    Else
        Picking.Fecha_packing = New Date(1900, 1, 1)  ' parcial → sigue en PENDIENTE
    End If
    clsLnTrans_picking_ubic.Actualizar_FechaPacking(Picking, lConnection, lTransaction)
Next
```

### Get_CantidadEmpacada filtra por lic_plate

La función `Get_CantidadEmpacada` en `clsLnTrans_packing_enc_Partial.vb` SÍ
filtra por `lic_plate`. El comentario anterior `'sin tomar en cuenta la licencia`
estaba desactualizado. Es seguro usarla para la guard.

Query interno:
```sql
SELECT SUM(Cantidad_bultos_packing)
FROM trans_packing_enc
WHERE IdProductoBodega = @IdProductoBodega
  AND IdPickingEnc = @IdPickingEnc
  AND IdUnidadMedida = @IdUnidadMedida
  AND lic_plate = @lic_plate
  AND ...
```

### Historia del bug

`#AT20250203` comentó la guard entera con el motivo incorrecto. Esto causó que
cualquier llamada a `Inserta_Packing` (parcial o completa) sellara `fecha_packing
= Date.Now()`, haciendo desaparecer el ubic del PENDIENTE aunque quedara cantidad
sin empacar. Fix commit `fba0a430d4` (dev_2028_merge, 2026-05-29).

---

## 4. Flujo completo del contrato fecha_packing

```
Verificación → Cargar() → fecha_packing = 1900-01-01 (sentinel)
                        ↓
             Actualizar() → VERIF_FP_GUARD resetea si viene envenenada

Packing → Get_All_PickingUbic_For_Packing → filtra fecha_packing < '19010101'
        → Insertar(packing_enc)
        → Get_CantidadEmpacada()
          ├─ empacado >= verificado → fecha_packing = Date.Now()  [COMPLETO]
          └─ empacado < verificado  → fecha_packing = 1900-01-01  [PARCIAL, sigue en PENDIENTE]

Get_All_PickingUbic_By_IdPickingEnc (HH lista PENDIENTE) → filtra fecha_packing < '19010101'
  → ubics con fecha real → no aparecen (correctamente)
  → ubics parciales → aparecen (correctamente desde fix fba0a430d4)
```

---

## 5. Tabla de commits de referencia

| SHA (BOF) | Qué resuelve |
|---|---|
| `16d80228c6` | BUG-004: `Cargar()` NULL→sentinel en clsLnTrans_picking_ubic.vb |
| `328cfe84e6` | BUG-004: guard VERIF_FP_GUARD en Actualizar_PickingUbic_Por_Verificacion |
| `fba0a430d4` | Packing parcial: restaurar guard CantidadEmpacada >= Cantidad_Verificada |
| `5d7fb80f` | INSERT vs UPDATE separados + reset Fecha_packing en Elimina_Packing |
| `2d845e6` | Get_All_PickingUbic_For_Packing filtro fecha_packing |

Tag estable: `packing-estable-cumbre-aws-ejc-ckfk` = HEAD dev_2028_merge al 2026-05-29.
