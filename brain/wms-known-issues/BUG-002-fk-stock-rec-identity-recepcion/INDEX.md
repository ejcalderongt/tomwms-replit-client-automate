---
id: BUG-002
tipo: wms-known-issue
titulo: "FK_stock_rec_trans_re_det — IDENTITY desincronizado en recepción HH"
estado: RESUELTO
rama_fix: dev_2028_merge
clientes_afectados: [MAMPA_QA, potencial cualquier cliente con IDENTITY desde ejc20260226]
fecha_detectado: 2026-05-27
fecha_fix: 2026-05-27
autores_fix: [EJC]
tags: [recepcion, stock_rec, trans_re_det, identity, fk, hh, dal]
---

# BUG-002 — FK_stock_rec_trans_re_det: IDENTITY desincronizado en recepción HH

## Error observado

```
INSERT statement conflicted with the FOREIGN KEY constraint
"FK_stock_rec_trans_re_det". The conflict occurred in database
"TOMWMS_MAMPA_QA", table "dbo.trans_re_det", column "IdRecepcionDet".
```

## Contexto rápido para el agente

Este bug surgió cuando `trans_re_det.IdRecepcionDet` fue convertido a IDENTITY
(`ejc20260226`, febrero 2026). La HH calcula el ID localmente. El servidor ya
no acepta ese valor — genera uno nuevo. Si nadie sincroniza `pListStockRec`
con el nuevo ID, el INSERT en `stock_rec` falla por FK.

**Estado en dev_2028_merge (rama activa):** COMPLETAMENTE RESUELTO.
Ver traza fina en `TRACE.md`.

## Archivos afectados

| Archivo | Función | Fix |
|---|---|---|
| `clsLnTrans_re_det_Partial.vb` | overload lista+stock | `#EJCCKFK20260520` helper `Asignar_IdRecepcionDet_StockRec` |
| `clsLnTrans_re_enc_Partial.vb` | `Guardar` (BOF WinForms) | `#EJC20260527_IDENTITY_FIX` commit `dea6197489d4` |
| `clsLnTrans_re_enc_Partial.vb` | `GuardarHH` overload 2517 (`GuardarRecepcionModif`) | `#EJC20260527_IDENTITY_FIX` commit `6adb53d92a02` |

## Commits en dev_2028_merge — TOMWMS_BOF

| Push | Commit | Path cubierto |
|---|---|---|
| ya existía | tag `#EJCCKFK20260520` | `GuardarHHSP` → `Guardar_Recepcion_Sin_Presentacion` |
| 2292 | `dea6197489d4` | `Guardar` BOF WinForms |
| 2293 | `6adb53d92a02` | `GuardarHH` 2517 → `GuardarRecepcionModif` |

## Pendiente

- Cherry-pick de los tres fixes a `dev_2026_mampa` (awaiting EJC).
- Verificar `GuardarHH_S` y `GuardarHH_CM` en `clsLnTrans_re_enc_Partial.vb`.
