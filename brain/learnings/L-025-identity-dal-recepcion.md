---
id: L-025
tipo: learning
titulo: "Patrón IDENTITY en DAL de recepción — sincronizar listas antes de FK dependiente"
tags: [identity, dal, recepcion, stock_rec, trans_re_det, fk, hh]
fecha: 2026-05-27
relacionado: [BUG-002, BUG-FK_stock_rec_trans_re_det-MAMPA.md]
---

# L-025 — IDENTITY en DAL de recepción: sincronizar listas antes de FK dependiente

## La regla

Cuando un método BOF recibe **dos listas relacionadas por FK** —
`pListRecDet` (padre) y `pListStockRec` (hijo) — y hace primero el INSERT del
padre (que es IDENTITY), **debe actualizar los IDs del hijo antes de insertarlo**.

Si el método llama a un overload de `Guarda_Trans_re_det` que NO recibe
`pListaStockRec`, la sincronización debe hacerse manualmente en el método padre
con el patrón snapshot-propagación.

## Por qué existe este riesgo

`trans_re_det.IdRecepcionDet` es IDENTITY desde `ejc20260226`. La HH calcula
su propio ID localmente (`MAX + 1`). El servidor genera un ID diferente en el
INSERT. Si nadie sincroniza el hijo, la FK viola.

El DELETE que hace la HH al re-guardar agrava el problema porque el IDENTITY
nunca retrocede: después del DELETE el contador sigue adelante, y el `MAX + 1`
calculado por la HH ya no coincide con el próximo IDENTITY real.

## Dos patrones de fix en el código

### Patrón A — helper `Asignar_IdRecepcionDet_StockRec` (preferido, más robusto)

Usado cuando el overload de `Guarda_Trans_re_det` recibe `pListaStockRec`:

```vb
' Dentro de Guarda_Trans_re_det(pListRecDet, pListaStockRec, ...):
Dim IdRecepcionDetOrigen As Integer = BeTransReDet.IdRecepcionDet
Insertar(BeTransReDet, lConnection, lTransaction)
MaxIdRecepcionDet = BeTransReDet.IdRecepcionDet   ' nuevo IDENTITY
Asignar_IdRecepcionDet_StockRec(pListaStockRec, BeTransReDet,
                                 IdRecepcionDetOrigen, MaxIdRecepcionDet)
```

`Asignar_IdRecepcionDet_StockRec` filtra por `IdProductoBodega + No_linea + Lic_plate`
— discriminador semántico, más robusto que solo buscar por ID numérico.

### Patrón B — snapshot de diccionario (cuando el overload no recibe la lista)

Usado cuando el método padre llama a un overload SIN `pListaStockRec` y luego
llama a `Guarda_Stock_Rec` por separado:

```vb
' PASO 1: snapshot ANTES del INSERT
Dim dictIdOrigen As New Dictionary(Of Integer, clsBeTrans_re_det)
For Each det In pListRecDet.Where(Function(x) x.IsNew)
    If Not dictIdOrigen.ContainsKey(det.IdRecepcionDet) Then
        dictIdOrigen.Add(det.IdRecepcionDet, det)
    End If
Next

' PASO 2: INSERT normal (muta BeTransReDet.IdRecepcionDet = nuevo IDENTITY)
clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, lConn, lTran)

' PASO 3: propagar nuevos IDs al hijo
For Each kvp In dictIdOrigen
    Dim idOrig = kvp.Key           ' integer copiado ANTES del INSERT
    Dim nuevoId = kvp.Value.IdRecepcionDet   ' ya mutado por SCOPE_IDENTITY
    If nuevoId > 0 AndAlso idOrig <> nuevoId Then
        For Each s In pListStockRec.Where(Function(x) x.IdRecepcionDet = idOrig)
            s.IdRecepcionDet = nuevoId
        Next
    End If
Next
```

**Por qué funciona:** `Integer` es value type — la llave del diccionario guarda
el valor numérico original. El value del diccionario es la referencia al objeto,
que después del INSERT ya tiene el nuevo IDENTITY. Los dos se pueden leer
independientemente.

## Checklist para agentes al tocar DAL de recepción

Ante cualquier tarea que involucre inserción de `trans_re_det` + `stock_rec`:

1. ¿El overload de `Guarda_Trans_re_det` recibe `pListaStockRec`?
   - SÍ → verificar que llama a `Asignar_IdRecepcionDet_StockRec` DESPUÉS del `Insertar`.
   - NO → buscar si el método padre llama a `Guarda_Stock_Rec` por separado.
     Si sí → aplicar Patrón B (snapshot de diccionario) antes y después del call.

2. ¿Hay un `Eliminar_Detalle` antes del insert?
   - Si hay DELETE previo, el riesgo de desajuste es ALTO. El IDENTITY nunca
     retrocede. Aunque el MAX calculado por la HH coincidiera antes del DELETE,
     después del DELETE el IDENTITY ya saltó.

3. ¿La validación `#GT04122024` (`Existe_By_IdRecepcionEnc_And_IdRecepcionDet`)
   está antes del INSERT?
   - Verifica si el detalle ya existe. Para recepciones nuevas: el ID calculado
     por la HH no existe aún → `False` → OK. No interfiere con el fix.
   - Puede dar falso positivo solo si hay inconsistencia previa (datos corruptos
     de antes del fix). En ese caso el mensaje es diferente al FK error.

## Métodos BOF donde aplica esta regla

| Método | Archivo | Overload | Fix |
|---|---|---|---|
| overload lista+stock | `clsLnTrans_re_det_Partial.vb` | `Guarda_Trans_re_det(pListRecDet, pListaStockRec, ...)` | Patrón A — `#EJCCKFK20260520` |
| `Guardar` (BOF WinForms) | `clsLnTrans_re_enc_Partial.vb` | overload con `pObjTareaHH` y overload simple | Patrón B — `#EJC20260527_IDENTITY_FIX` |
| `GuardarHH` overload 2517 | `clsLnTrans_re_enc_Partial.vb` | overload con `pBeStockAnt` | Patrón B — `#EJC20260527_IDENTITY_FIX` |
| `GuardarHH_S` | `clsLnTrans_re_enc_Partial.vb` | overload `GuardarHH_S` | PENDIENTE VERIFICAR |
| `GuardarHH_CM` | `clsLnTrans_re_enc_Partial.vb` | overload `GuardarHH_CM` | PENDIENTE VERIFICAR |
