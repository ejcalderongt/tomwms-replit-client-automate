---
tipo: bug-diagnosis
cliente: MAMPA (TOMWMS_MAMPA_QA)
ramas_afectadas: dev_2026_mampa (bug original), dev_2028_merge (fix aplicado 2026-05-27)
archivo_principal: TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Detalle/clsLnTrans_re_det_Partial.vb
archivo_secundario: TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Encabezado/clsLnTrans_re_enc_Partial.vb
tabla_afectada: stock_rec (FK FK_stock_rec_trans_re_det → trans_re_det.IdRecepcionDet)
error: INSERT statement conflicted with FOREIGN KEY constraint "FK_stock_rec_trans_re_det"
fecha_diagnostico: 2026-05-27
fecha_fix_2028: 2026-05-27
autor_diagnostico: agente-replit
estado: RESUELTO en dev_2028_merge — pendiente cherry-pick a dev_2026_mampa (awaiting EJC)
commits_fix:
  - rama: dev_2028_merge
    repo: TOMWMS_BOF
    descripcion: "GuardarHH — fix #1 clsLnTrans_re_det overload lista CON stock (GuardarHHSP path)"
    tag_original: "#EJCCKFK20260520"
    nota: ya estaba en 2028 al iniciar la sesion
  - rama: dev_2028_merge
    repo: TOMWMS_BOF
    commit: dea6197489d4
    push_id: 2292
    descripcion: "GuardarHH — fix #2 clsLnTrans_re_enc GuardarHH snapshot+propagacion (Guardar_Recepcion path)"
    tag: "#EJC20260527_IDENTITY_FIX"
---

# BUG: FK_stock_rec_trans_re_det — MAMPA_QA

## Error observado

```
INSERT statement conflicted with the FOREIGN KEY constraint
"FK_stock_rec_trans_re_det". The conflict occurred in database
"TOMWMS_MAMPA_QA", table "dbo.trans_re_det", column "IdRecepcionDet".
```

Ocurre durante la recepción HH.

---

## Estado del fix por rama

| Rama | Path afectado | Estado |
|---|---|---|
| `dev_2028_merge` | `Guardar_Recepcion_Sin_Presentacion → GuardarHHSP` | **RESUELTO** `#EJCCKFK20260520` (existía antes de esta sesión) |
| `dev_2028_merge` | `Guardar_Recepcion → GuardarHH` | **RESUELTO** commit `dea6197489d4` `#EJC20260527_IDENTITY_FIX` |
| `dev_2026_mampa` | Ambos paths | **PENDIENTE** — awaiting EJC para cherry-pick |

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

## Bug #1: overload lista+stock en `clsLnTrans_re_det_Partial.vb`

### Path afectado

`Guardar_Recepcion_Sin_Presentacion` → WS → `GuardarHHSP` → overload con firma:
```vb
Guarda_Trans_re_det(pListRecDet, pListaStockRec, lConnection, lTransaction)
```

### Código BUGGY (dev_2026_mampa, ~línea 2565)

```vb
If BeTransReDet.IsNew Then
    MaxIdRecepcionDet = 0 'now is identity EJC20260226

    ' ← BUG: propagación ANTES del Insertar, con MaxIdRecepcionDet = 0
    pListaStockRec.FindAll(Function(x) x.IdRecepcionDet = BeTransReDet.IdRecepcionDet) _
                  .ForEach(Sub(s) s.IdRecepcionDet = MaxIdRecepcionDet)

    Insertar(BeTransReDet, lConnection, lTransaction)
    ' Aquí: BeTransReDet.IdRecepcionDet = Y (nuevo IDENTITY)
    ' PERO pListaStockRec ya tiene IdRecepcionDet = 0 → FK viola
End If
```

### Fix en dev_2028_merge (ya existía con `#EJCCKFK20260520`)

```vb
If BeTransReDet.IsNew Then
    Dim IdRecepcionDetOrigen As Integer = BeTransReDet.IdRecepcionDet
    Insertar(BeTransReDet, lConnection, lTransaction)
    MaxIdRecepcionDet = BeTransReDet.IdRecepcionDet
    If MaxIdRecepcionDet <= 0 Then
        Throw New Exception("ERROR_202605201645: No se obtuvo IdRecepcionDet identity...")
    End If
    Asignar_IdRecepcionDet_StockRec(pListaStockRec, BeTransReDet, IdRecepcionDetOrigen, MaxIdRecepcionDet)
End If
```

Helper privado `Asignar_IdRecepcionDet_StockRec` filtra por `IdProductoBodega + No_linea + Lic_plate`.

---

## Bug #2: `GuardarHH` en `clsLnTrans_re_enc_Partial.vb`

### Path afectado

`Guardar_Recepcion` (ruta principal con presentaciones) → WS → `GuardarHH`

### Problema

`GuardarHH` llama al overload `Guarda_Trans_re_det(pListRecDet, True, pRecEnc, ...)` (sin `pListaStockRec`), luego llama `Guarda_Stock_Rec(pListStockRec)` por separado. El `pListStockRec` llega a `Guarda_Stock_Rec` con los IDs calculados localmente por la HH, que no coinciden con los Identities reales generados por el INSERT.

### Fix aplicado en dev_2028_merge (commit `dea6197489d4`, `#EJC20260527_IDENTITY_FIX`)

En las DOS versiones de `GuardarHH` (v1 con `pObjTareaHH`, v2 simplificada):

```vb
'#EJC20260527_IDENTITY_FIX: snapshot IDs de detalles IsNew antes del INSERT
Dim dictIdOrigenV1 As New Dictionary(Of Integer, clsBeTrans_re_det)
For Each detOri As clsBeTrans_re_det In pListRecDet.Where(Function(x) x.IsNew)
    If Not dictIdOrigenV1.ContainsKey(detOri.IdRecepcionDet) Then
        dictIdOrigenV1.Add(detOri.IdRecepcionDet, detOri)
    End If
Next

' Recepción Detalle
clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, True, pRecEnc, lConnection, lTransaction)

'#EJC20260527_IDENTITY_FIX: propagar nuevos Identities al pListStockRec
For Each kvpV1 As KeyValuePair(Of Integer, clsBeTrans_re_det) In dictIdOrigenV1
    Dim idOrigV1 As Integer = kvpV1.Key
    Dim nuevoIdV1 As Integer = kvpV1.Value.IdRecepcionDet  ' mutado por Insertar
    If nuevoIdV1 > 0 AndAlso idOrigV1 <> nuevoIdV1 Then
        For Each sV1 As clsBeStock_rec In pListStockRec.Where(Function(x) x.IdRecepcionDet = idOrigV1)
            sV1.IdRecepcionDet = nuevoIdV1
        Next
    End If
Next
```

---

## Archivos a modificar para cherry-pick a dev_2026_mampa

| Archivo | Tipo de cambio |
|---|---|
| `clsLnTrans_re_det_Partial.vb` ~línea 2565 | Mover propagación DESPUÉS del Insertar; agregar `IdRecepcionDetOrigen`; agregar validación `<= 0` |
| `clsLnTrans_re_enc_Partial.vb` `GuardarHH` v1 y v2 | Agregar snapshot + propagación `#EJC20260527_IDENTITY_FIX` |

---

## Verificación post-fix sugerida

```sql
-- Verificar huérfanos en stock_rec después de aplicar el fix
SELECT COUNT(*) AS OrfanosMuestreo
FROM stock_rec sr
WHERE NOT EXISTS (
    SELECT 1 FROM trans_re_det trd
    WHERE trd.IdRecepcionDet = sr.IdRecepcionDet
)
```

Si > 0: limpiar filas huérfanas antes del deploy a producción.

---

## Impacto en HH

La HH **no requiere cambios** para este fix. El fix es en BOF (servidor).
Mejora futura opcional: el WS podría retornar el nuevo `IdRecepcionDet` para
que la HH actualice su sesión local (hoy retorna solo `String`).

---

## Fingerprint MAMPA

- 545 filas en `trans_re_det`, 64 recepciones
- Talla+Color ON (`IdProductoTallaColor` enviado por HH)
- Bug manifestado en MAMPA_QA primero; validar en TOMWMS_MAMPA_PRD antes de aplicar fix allí.

---

## Contexto del WS — qué endpoint usa cada path

| WS Method | LN llamado | Fix aplicado |
|---|---|---|
| `Guardar_Recepcion` | `GuardarHH` | `#EJC20260527_IDENTITY_FIX` commit `dea6197489d4` |
| `Guardar_Recepcion_Sin_Presentacion` | `GuardarHHSP` | `#EJCCKFK20260520` (ya existía) |
| `Guardar_Recepcion_S` | por confirmar | pendiente revisión |
| `Guardar_Recepcion_Caja_Master` | por confirmar | pendiente revisión |
