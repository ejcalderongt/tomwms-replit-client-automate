---
tipo: bug-diagnosis
cliente: MAMPA (TOMWMS_MAMPA_QA)
rama: dev_2026_mampa
archivo_principal: TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Detalle/clsLnTrans_re_det_Partial.vb
archivo_secundario: TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Encabezado/clsLnTrans_re_enc_Partial.vb
tabla_afectada: stock_rec (FK FK_stock_rec_trans_re_det → trans_re_det.IdRecepcionDet)
error: INSERT statement conflicted with FOREIGN KEY constraint "FK_stock_rec_trans_re_det"
fecha_diagnostico: 2026-05-27
autor_diagnostico: agente-replit
estado: PATCH LISTO — pendiente revisión EJC
---

# BUG: FK_stock_rec_trans_re_det — MAMPA_QA

## Error observado

```
INSERT statement conflicted with the FOREIGN KEY constraint
"FK_stock_rec_trans_re_det". The conflict occurred in database
"TOMWMS_MAMPA_QA", table "dbo.trans_re_det", column "IdRecepcionDet".
```

Ocurre durante la recepción HH en `Guarda_Stock_Rec`, después del flujo
que involucra `Eliminar_Detalle_Recepción → Guarda_Trans_re_det → Guarda_Stock_Rec`.

---

## Causa raíz — Timeline del cambio roto

### 1. El cambio a IDENTITY (ejc20260226)

`trans_re_det.IdRecepcionDet` se convirtió a IDENTITY en febrero 2026.
Evidencia en código (`clsLnTrans_re_det_Partial.vb` línea 259):

```vb
BeRec.IdRecepcionDet = 0 'now is identity, ejc20260226
```

Y en el `Insertar` de `clsLnTrans_re_det.vb` (línea 110 y 159):
```vb
Dim sp As String = Ins.SQL() & "; SELECT CAST(SCOPE_IDENTITY() AS INT);"
...
Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
oBeTrans_re_det.IdRecepcionDet = newId   ' ← actualiza el objeto con el nuevo IDENTITY
```
El INSERT captura correctamente `SCOPE_IDENTITY()`. Hasta aquí está bien.

### 2. La HH sigue calculando el ID localmente

En `frm_recepcion_datos.java` (TOMHH2025), la HH calcula su propio
`pIdRecepcionDet` como `MAX(lista_local) + 1` (líneas 3688, 3805, 10342-10343):

```java
Integer tmpIdRecepcionDet = xobj.getresult(Integer.class, "Max_IdRecepcion_Det_By_IdRecepcionEnc");
pIdRecepcionDet = tmpIdRecepcionDet + 1;
```

La HH envía `pListStockRec` con `BeStockRec.IdRecepcionDet = X`
(ese ID calculado localmente). El WS retorna `String` y la HH NUNCA
actualiza `pIdRecepcionDet` con el IDENTITY real que generó el servidor.

---

## Bug principal: overload 2565 de `Guarda_Trans_re_det`

**Archivo:** `clsLnTrans_re_det_Partial.vb` (línea ~2565)

Firma:
```vb
Public Shared Function Guarda_Trans_re_det(ByRef pListRecDet As List(Of clsBeTrans_re_det),
                                           ByRef pListaStockRec As List(Of clsBeStock_rec),
                                           ByRef lConnection As SqlConnection,
                                           ByRef lTransaction As SqlTransaction) As Integer
```

Código actual BUGGY:
```vb
If BeTransReDet.IsNew Then
    MaxIdRecepcionDet = 0 'now is identity EJC20260226

    ' ← BUG: la propagación se hace ANTES del Insertar, con MaxIdRecepcionDet = 0
    pListaStockRec.FindAll(Function(x) x.IdRecepcionDet = BeTransReDet.IdRecepcionDet) _
                  .ForEach(Sub(s) s.IdRecepcionDet = MaxIdRecepcionDet)

    BeTransReDet.Fecha_ingreso = Now
    BeTransReDet.Fec_agr = Now
    If BeTransReDet.IdPresentacion = -1 Then BeTransReDet.IdPresentacion = 0
    Insertar(BeTransReDet, lConnection, lTransaction)
    ' Aquí: BeTransReDet.IdRecepcionDet = Y (nuevo Identity, ej. 547)
    ' PERO pListaStockRec ya fue actualizado a 0 → stock_rec.IdRecepcionDet = 0
End If
```

**Resultado:** `INSERT INTO stock_rec (IdRecepcionDet=0)` → 0 no existe en
`trans_re_det` → FK viola.

### Fix overload 2565

```vb
If BeTransReDet.IsNew Then
    '#EJC20260527_IDENTITY_FIX: Capturar ID original ANTES del insert
    Dim IdRecepcionDetOrigen As Integer = BeTransReDet.IdRecepcionDet

    BeTransReDet.Fecha_ingreso = Now
    BeTransReDet.Fec_agr = Now
    If BeTransReDet.IdPresentacion = -1 Then BeTransReDet.IdPresentacion = 0

    Insertar(BeTransReDet, lConnection, lTransaction)
    ' Ahora: BeTransReDet.IdRecepcionDet = Y (nuevo IDENTITY correcto)

    '#EJC20260527_IDENTITY_FIX: Propagar el nuevo IDENTITY al stock_rec DESPUÉS del insert
    pListaStockRec.FindAll(Function(x) x.IdRecepcionDet = IdRecepcionDetOrigen) _
                  .ForEach(Sub(s) s.IdRecepcionDet = BeTransReDet.IdRecepcionDet)

    vFilas += 1
    ...
End If
```

**Referencia correcta:** El overload 2650 (para un solo BeTransReDet) ya
implementa este patrón correctamente con `IdRecepcionDetOrigen`.

---

## Bug secundario: `GuardarHH` overload 1805 — sin propagación al stock

**Archivo:** `clsLnTrans_re_enc_Partial.vb` (~línea 1701)

`GuardarHH` llama al overload 1805 de `Guarda_Trans_re_det` (el que
recibe `Comparar_Detalle_Actual = True` pero NO recibe `pListaStockRec`).
Después de ese call, los objetos `BeRecDet.IdRecepcionDet` ya tienen el
nuevo Identity, pero el `pListStockRec` que maneja `GuardarHH` por separado
todavía tiene el ID original calculado por la HH.

Código actual en `GuardarHH`:
```vb
' Recepción Detalle (modifica BeRecDet.IdRecepcionDet = nuevo IDENTITY)
clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, True, pRecEnc, lConnection, lTransaction)

' ... más operaciones ...

' Stock Rec  ← pListStockRec todavía tiene los IDs originales de la HH
clsLnStock_rec.Guarda_Stock_Rec(pRecEnc.IdRecepcionEnc, IdBodega, pListStockRec, lConnection, lTransaction)
```

### Fix `GuardarHH` — capturar snapshot de IDs antes del call

Insertar entre el call a `Guarda_Trans_re_det` y el call a `Guarda_Stock_Rec`:

```vb
' ─── ANTES del call a Guarda_Trans_re_det ─────────────────────────────────
'#EJC20260527_IDENTITY_FIX: snapshot {IdOriginal → objeto BeRecDet IsNew}
Dim dictIdOrigen As New Dictionary(Of Integer, clsBeTrans_re_det)
For Each det As clsBeTrans_re_det In pListRecDet.Where(Function(x) x.IsNew)
    If Not dictIdOrigen.ContainsKey(det.IdRecepcionDet) Then
        dictIdOrigen.Add(det.IdRecepcionDet, det)
    End If
Next

' Recepción Detalle
clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, True, pRecEnc, lConnection, lTransaction)

'#EJC20260527_IDENTITY_FIX: sincronizar pListStockRec con los nuevos Identities
' Después del call, det.IdRecepcionDet ya es el nuevo IDENTITY (la referencia fue mutada por Insertar)
For Each kvp As KeyValuePair(Of Integer, clsBeTrans_re_det) In dictIdOrigen
    Dim idOrig As Integer = kvp.Key
    Dim nuevoId As Integer = kvp.Value.IdRecepcionDet  ' ya mutado por Insertar
    For Each s As clsBeStock_rec In pListStockRec.Where(Function(x) x.IdRecepcionDet = idOrig)
        s.IdRecepcionDet = nuevoId
    Next
Next
' ─────────────────────────────────────────────────────────────────────────
```

---

## Archivos a modificar (cherry-pick)

| Archivo | Línea aproximada | Tipo de cambio |
|---|---|---|
| `clsLnTrans_re_det_Partial.vb` | ~2580-2595 | Mover propagación FindAll/ForEach a DESPUÉS del Insertar; agregar `IdRecepcionDetOrigen` |
| `clsLnTrans_re_enc_Partial.vb` | ~1700-1745 | Agregar snapshot de IDs antes de `Guarda_Trans_re_det` + propagación antes de `Guarda_Stock_Rec` |

Ambos cambios son en `dev_2026_mampa`. Verificar si `dev_2028_merge` tiene el
mismo patrón — la conversión a IDENTITY se hizo en `dev_2026_mampa`; si se
mergeó a 2028 sin este fix, el bug existe allí también.

---

## Verificación post-fix sugerida

```sql
-- En TOMWMS_MAMPA_QA: verificar que no queden stock_rec con IdRecepcionDet huérfano
SELECT COUNT(*) AS OrfanosMuestreo
FROM stock_rec sr
WHERE NOT EXISTS (
    SELECT 1 FROM trans_re_det trd
    WHERE trd.IdRecepcionDet = sr.IdRecepcionDet
)
```

Si el resultado es > 0 tras el fix, limpiar esas filas huérfanas antes del
deploy a producción.

---

## Impacto en HH

La HH **no requiere cambios** para este fix. El fix es en BOF (servidor).
Sin embargo, como mejora futura: el WS podría retornar el nuevo
`IdRecepcionDet` para que la HH actualice su sesión local (hoy retorna solo `String`).

---

## Nota sobre el fingerprint MAMPA

- 545 filas en `trans_re_det`, 64 recepciones
- Talla+Color ON (`IdProductoTallaColor` enviado por HH)
- El bug se manifestó en MAMPA_QA primero; validar en TOMWMS_MAMPA_PRD antes de aplicar fix ahí.


---

## Diff exacto — `clsLnTrans_re_det_Partial.vb` (overload 2565)

Ruta en repo: `TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Detalle/clsLnTrans_re_det_Partial.vb`

Líneas actuales (BUGGY) ~2580–2596, dentro del overload `Guarda_Trans_re_det(pListRecDet, pListaStockRec, ...)`:

```diff
-                    MaxIdRecepcionDet = 0 'now is identity EJC20260226
-
-                    pListaStockRec.FindAll(Function(x) x.IdRecepcionDet = BeTransReDet.IdRecepcionDet).ForEach(Sub(s) s.IdRecepcionDet = MaxIdRecepcionDet)
-
-                    BeTransReDet.Fecha_ingreso = Now
-                    BeTransReDet.Fec_agr = Now
-
-                    If BeTransReDet.IdPresentacion = -1 Then
-                        BeTransReDet.IdPresentacion = 0
-                    End If
-
-                    Insertar(BeTransReDet, lConnection, lTransaction)
-
-                    vFilas += 1
+                    '#EJC20260527_IDENTITY_FIX: Capturar ID original ANTES del INSERT
+                    Dim IdRecepcionDetOrigen As Integer = BeTransReDet.IdRecepcionDet
+
+                    BeTransReDet.Fecha_ingreso = Now
+                    BeTransReDet.Fec_agr = Now
+
+                    If BeTransReDet.IdPresentacion = -1 Then
+                        BeTransReDet.IdPresentacion = 0
+                    End If
+
+                    Insertar(BeTransReDet, lConnection, lTransaction)
+
+                    '#EJC20260527_IDENTITY_FIX: Propagar nuevo IDENTITY a stock_rec DESPUÉS del INSERT
+                    pListaStockRec.FindAll(Function(x) x.IdRecepcionDet = IdRecepcionDetOrigen).ForEach(Sub(s) s.IdRecepcionDet = BeTransReDet.IdRecepcionDet)
+
+                    vFilas += 1
```

Eliminar `MaxIdRecepcionDet` de la declaración `Dim MaxIdRecepcionDet As Integer = 0`
(línea ~2574) ya que deja de ser necesaria en este overload.

---

## Confirmación de la pila del error

1. HH llama `Guardar_Recepcion` → WS → `GuardarHHSP` (`clsLnTrans_re_enc_Partial.vb` ~línea 1927)
2. `GuardarHHSP` llama `Eliminar_Detalle(pIdOrdenCompraEnc, pListRecDet, ...)` → borra `stock_rec`, `stock`, `trans_re_det` del detalle original
3. `GuardarHHSP` llama `Guarda_Trans_re_det(pListRecDet, pListStockRec, ...)` → overload 2565 BUGGY
4. Overload 2565: pone `pListaStockRec.IdRecepcionDet = 0` ANTES del INSERT → luego `Insertar` genera `Y` en `trans_re_det`
5. `GuardarHHSP` llama `Guarda_Stock_Rec(pListStockRec)` → INSERT `stock_rec` con `IdRecepcionDet = 0` → **FK viola** (0 no existe en `trans_re_det`)

