---
id: BUG-002-TRACE
tipo: traza-fina
titulo: "BUG-002 — traza de ejecución completa del FK error"
tags: [recepcion, identity, fk, hh, dal, traza]
---

# BUG-002 — Traza fina de ejecución

## Por qué el DELETE agrava el bug

Antes de la conversión a IDENTITY (`ejc20260226`) el flujo funcionaba:

```
HH calcula: IdRecepcionDet = MAX(trans_re_det WHERE IdRecepcionEnc=X) + 1
            Resultado: 5

Servidor (antiguo, sin IDENTITY):
  1. DELETE trans_re_det   WHERE IdRecepcionEnc=X   → borra fila con Id=5
  2. INSERT trans_re_det   IdRecepcionDet = 5        → el servidor usaba el valor del cliente
  3. INSERT stock_rec      IdRecepcionDet = 5        → el 5 existe en trans_re_det → OK ✓
```

Después de la conversión a IDENTITY:

```
HH calcula: IdRecepcionDet = MAX(trans_re_det WHERE IdRecepcionEnc=X) + 1
            Resultado: 5

Servidor (IDENTITY activo desde ejc20260226):
  1. DELETE trans_re_det   WHERE IdRecepcionEnc=X   → borra fila con Id=5
                                                     IDENTITY counter sigue en 5, avanza a 6
  2. INSERT trans_re_det   → SQL Server ignora el Id que manda el cliente
                             SCOPE_IDENTITY() devuelve 6
                             BeTransReDet.IdRecepcionDet = 6  (objeto actualizado)
  3. INSERT stock_rec      IdRecepcionDet = 5  ← nadie actualizó pListStockRec
                           → el 5 NO existe en trans_re_det (existe el 6)
                           → FK_stock_rec_trans_re_det VIOLA 💥
```

**Nota sobre el DELETE y el IDENTITY:** SQL Server nunca retrocede el contador
de IDENTITY aunque se borren filas. Si el Id 5 fue borrado y luego se hace un
INSERT nuevo, el contador ya está en 6 (o más si hubo otros inserts). El número
que calculó la HH (`MAX + 1 = 5`) ya no coincide.

**Sin DELETE** el desajuste es menos frecuente pero igual existe: si entre el
momento en que la HH calculó el MAX y el momento en que el servidor hace el
INSERT, otro usuario insertó una fila, el IDENTITY avanzó y el número no coincide.

---

## Pila de llamadas — camino `Guardar_Recepcion` (MAMPA con presentaciones)

```
frm_recepcion_datos.java
  └─ xobj.getresult("Guardar_Recepcion")              ← HH elige este path porque chkPresentacion=ON
       │  pListStockRec[i].IdRecepcionDet = 5          ← ID calculado localmente por la HH
       ↓
TOMHHWS.asmx :: Guardar_Recepcion(...)
  └─ clsLnTrans_re_enc.GuardarHH(pRecEnc, ..., pLotesRec, ...)   ← overload línea 2768
       │
       ├─ Validación #GT04122024:
       │    Existe_By_IdRecepcionEnc_And_IdRecepcionDet(pListRecDet.First())
       │    → False (el Id=5 ya fue borrado o no existe) → pasa OK
       │
       ├─ clsLnTrans_re_det.Eliminar_Detalle(...)      ← borra trans_re_det + stock existente
       │
       ├─ clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, pListStockRec, ...)
       │    └─ overload lista+stock (línea ~2728 en clsLnTrans_re_det_Partial.vb)
       │         ← #EJCCKFK20260520 YA TIENE EL FIX
       │         Para cada BeTransReDet IsNew:
       │           IdRecepcionDetOrigen = BeTransReDet.IdRecepcionDet  (= 5)
       │           Insertar(BeTransReDet, ...)          → SCOPE_IDENTITY() = 6
       │           MaxIdRecepcionDet = 6
       │           Asignar_IdRecepcionDet_StockRec(pListStockRec, ..., origen=5, nuevo=6)
       │             → pListStockRec[i].IdRecepcionDet = 6  ✓
       │
       └─ clsLnStock_rec.Guarda_Stock_Rec(pListStockRec, ...)
            INSERT stock_rec IdRecepcionDet = 6   → el 6 existe en trans_re_det → OK ✓
```

---

## Pila de llamadas — camino `GuardarRecepcionModif` (fix commit 6adb53d92a02)

```
TOMHHWS.asmx :: GuardarRecepcionModif(...)
  └─ clsLnTrans_re_enc.GuardarHH(pRecEnc, ..., pBeStockAnt, ...)   ← overload línea 2517

       ├─ [ANTES DEL FIX] ─────────────────────────────────────────────────────
       │   clsLnTrans_re_det.Eliminar_Detalle(...)
       │   clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, lConn, lTran)
       │     └─ overload SIN pListStockRec — solo actualiza BeTransReDet.IdRecepcionDet = 6
       │        pListStockRec[i].IdRecepcionDet SIGUE EN 5
       │   clsLnStock_rec.Guarda_Stock_Rec(pListStockRec, ...)
       │     INSERT stock_rec IdRecepcionDet = 5 → FK VIOLA 💥
       │
       └─ [DESPUÉS DEL FIX — commit 6adb53d92a02] ──────────────────────────
           dictIdOrigenModif = { 5 → BeTransReDet }   ← snapshot ANTES del INSERT

           clsLnTrans_re_det.Eliminar_Detalle(...)
           clsLnTrans_re_det.Guarda_Trans_re_det(pListRecDet, lConn, lTran)
             └─ BeTransReDet.IdRecepcionDet = 6  (mutado por SCOPE_IDENTITY)
                dictIdOrigenModif[5].IdRecepcionDet también = 6  (misma referencia)

           Propagación:
             idOrig=5, nuevoId=6 → pListStockRec[i].IdRecepcionDet = 6  ✓

           clsLnStock_rec.Guarda_Stock_Rec(pListStockRec, ...)
             INSERT stock_rec IdRecepcionDet = 6 → OK ✓
```

---

## Por qué el diccionario funciona como snapshot

VB.NET (y .NET en general) maneja objetos **por referencia**. Cuando hago:

```vb
dictIdOrigenModif.Add(detOri.IdRecepcionDet, detOri)
```

Estoy guardando:
- **Llave** (`Key`): el número entero actual del ID — esto es una **copia del valor** (Integer es value type).
- **Valor** (`Value`): la referencia al objeto `detOri` — esto apunta al mismo objeto en memoria.

Cuando `Guarda_Trans_re_det` llama a `Insertar(BeTransReDet, ...)` y actualiza
`BeTransReDet.IdRecepcionDet = 6`, ese cambio se refleja automáticamente en
`kvpModif.Value.IdRecepcionDet` porque es la misma referencia.

La llave `5` (Integer, value type) NO cambia — ese es el ID original que necesito
para buscar en `pListStockRec`. El Value ya tiene el `6` nuevo.

---

## Helper `Asignar_IdRecepcionDet_StockRec` — lógica interna

Usado en el path `GuardarHHSP` / overload lista+stock (ya existía con `#EJCCKFK20260520`):

```vb
Private Shared Sub Asignar_IdRecepcionDet_StockRec(
    pListaStockRec, pBeTransReDet, pIdRecepcionDetOrigen, pIdRecepcionDetNuevo)

  ' Filtro primario: buscar por IdRecepcionDet original + IdProductoBodega + No_linea + Lic_plate
  Dim lStockRec = pListaStockRec.FindAll(x.IdRecepcionDet = pIdRecepcionDetOrigen
                                     AND x.IdProductoBodega = pBeTransReDet.IdProductoBodega
                                     AND x.No_linea = pBeTransReDet.No_Linea
                                     AND (LP vacío OR x.Lic_plate = LP))

  ' Fallback: si no encontró por ID origen, buscar los que tienen Id=0 (caso edge)
  If lStockRec.Count = 0 Then
      lStockRec = pListaStockRec.FindAll(x.IdRecepcionDet = 0
                                     AND x.IdProductoBodega = ...
                                     AND x.No_linea = ...
                                     AND LP match)
  End If

  For Each BeStockRec In lStockRec
      BeStockRec.IdRecepcionDet = pIdRecepcionDetNuevo  ' asignar el nuevo IDENTITY
  Next
End Sub
```

El filtro por `IdProductoBodega + No_linea + Lic_plate` es más robusto que el
snapshot de diccionario porque es discriminador semántico (no solo numérico).
Útil cuando dos detalles podrían tener el mismo ID calculado por la HH en edge cases.

---

## Validación post-fix sugerida

```sql
-- Ejecutar en TOMWMS_MAMPA_QA (y PRD antes de deploy)
-- Buscar stock_rec sin detalle padre en trans_re_det
SELECT sr.IdRecepcionEnc, sr.IdRecepcionDet, COUNT(*) AS N
FROM stock_rec sr
WHERE NOT EXISTS (
    SELECT 1 FROM trans_re_det trd
    WHERE trd.IdRecepcionDet = sr.IdRecepcionDet
)
GROUP BY sr.IdRecepcionEnc, sr.IdRecepcionDet
ORDER BY sr.IdRecepcionEnc DESC;
```

Si hay filas: son huérfanos de recepciones donde el bug ocurrió antes del fix.
Deben limpiarse antes del deploy a producción para no dejar datos corruptos.

---

## Bug de segundo nivel: `IsNew = False` en el overload 2728

El fix `#EJCCKFK20260520` tenía un gap: dentro del overload
`Guarda_Trans_re_det(pListRecDet, pListaStockRec, ...)` (línea 2728 de
`clsLnTrans_re_det_Partial.vb`) existía este bloque:

```vb
If BeTransReDet.IsNew Then
    ' snapshot + INSERT + Asignar_IdRecepcionDet_StockRec  ← fix funcionaba aquí
Else
    Actualizar(BeTransReDet, ...)  ← INOPERANTE tras DELETE previo → stock no sincronizado
End If
```

Si la HH envía los detalles con `IsNew = False` (caso MAMPA confirmado en debugger),
el código iba al `Else → Actualizar`. El `Eliminar_Detalle` ya había borrado la fila,
así que el `Actualizar` devolvía 0 filas. **No había INSERT, no había IDENTITY nuevo,
`pListaStockRec` quedaba con los IDs calculados por la HH → FK viola.**

### Fix `#EJCCKFK20260527` — commit `f8fa402b48a3`

El `Else` del overload 2728 fue reemplazado para aplicar el mismo patrón
INSERT+SCOPE_IDENTITY+sincronización cuando `IsNew = False`:

```vb
Else
    '#EJCCKFK20260527: Este overload se invoca siempre tras un DELETE previo.
    'Si IsNew=False, Actualizar no inserta (fila ya borrada) → misma FK.
    'Aplicar el mismo patron INSERT+SCOPE_IDENTITY que el branch IsNew=True.
    Dim IdRecepcionDetOrigenNotNew As Integer = BeTransReDet.IdRecepcionDet
    BeTransReDet.Fecha_ingreso = Now
    BeTransReDet.Fec_agr = Now
    If BeTransReDet.IdPresentacion = -1 Then BeTransReDet.IdPresentacion = 0
    Insertar(BeTransReDet, lConnection, lTransaction)
    Dim MaxIdRecepcionDetNotNew As Integer = BeTransReDet.IdRecepcionDet
    If MaxIdRecepcionDetNotNew <= 0 Then
        Throw New Exception("ERROR_202605271715: ...")
    End If
    Asignar_IdRecepcionDet_StockRec(pListaStockRec, BeTransReDet,
                                    IdRecepcionDetOrigenNotNew, MaxIdRecepcionDetNotNew)
    vFilas += 1
End If
```

Este fix cubre automáticamente **todos** los call sites del overload 2728:
`GuardarHH` overload 2790, `GuardarHHSP`, y cualquier otro que llame
`Guarda_Trans_re_det(lista, stock, ...)`.

---

## Tabla de commits final — dev_2028_merge TOMWMS_BOF

| Push | Commit | Qué fix | Archivo |
|---|---|---|---|
| — | `#EJCCKFK20260520` | Overload 2728 branch IsNew=True: snapshot+INSERT+sync | `clsLnTrans_re_det_Partial.vb` |
| 2292 | `dea6197489d4` | `Guardar` BOF WinForms: Patrón B | `clsLnTrans_re_enc_Partial.vb` |
| 2293 | `6adb53d92a02` | `GuardarHH` overload 2517: Patrón B | `clsLnTrans_re_enc_Partial.vb` |
| 2294 | `f8fa402b48a3` | `#EJCCKFK20260527` overload 2728 branch IsNew=False: INSERT+sync | `clsLnTrans_re_det_Partial.vb` |

---

## Paths WS — estado final

| WS Method | LN llamado | Estado |
|---|---|---|
| `Guardar_Recepcion` | `GuardarHH` overload 2790 | RESUELTO — overload 2728 con `#EJCCKFK20260527` |
| `Guardar_Recepcion_Sin_Presentacion` | `GuardarHHSP` | RESUELTO — overload 2728 con `#EJCCKFK20260527` |
| `GuardarRecepcionModif` | `GuardarHH` overload 2517 | RESUELTO — commit `6adb53d92a02` Patrón B |
| `Guardar` BOF WinForms | overloads 1354/1510 | RESUELTO — commit `dea6197489d4` Patrón B |
| `Guardar_Recepcion_S` | `GuardarHH_S` | PENDIENTE REVISIÓN |
| `Guardar_Recepcion_Caja_Master` | `GuardarHH_CM` | PENDIENTE REVISIÓN |
