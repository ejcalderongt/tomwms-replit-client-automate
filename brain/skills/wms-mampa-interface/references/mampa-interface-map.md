---
tipo: reference
autores: [carol, codex]
---

# MAMPA Interface Map

## Focus files

- `SAPSYNCMAMPA/Clases Interface Sync/Transacciones_WMS/clsSyncTransacWMS.vb`
- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb`
- `TOMIMSV4/DAL/Mantenimientos/Ajustes/clsLnTrans_ajuste_enc_Partial.vb`
- `TOMIMSV4/DAL/Transacciones/OrdenCompra/OC_Detalle/clsLnTrans_oc_det_Partial.vb`

## Frequent hotspots

| Area | What to inspect first | Why |
| --- | --- | --- |
| Ajustes SAP | `Procesar_Ajustes_SAP`, `MapearAAjustes` | Unicity by `Referencia`, progress UI, detail mapping |
| Recepcion OC | `Generar_Tarea_Recepcion_By_OrdenCompraEnc_Doc_Devolucion` | Talla/color and `stock_rec` filling |
| Service Layer | `Get_Ajustes_Tiendas`, `Marcar_Transac_Wms_Por_DocEntries_SLAsync` | Read/write sync and marking in SAP |
| Bitacora | `RegistrarTrazaTransacWms`, `RegistrarFalloTransacWmsAsync` | Human and technical trace split |

## Fast checklist

1. Run the scan script.
2. Open only the reported methods.
3. Confirm whether the rule belongs to the interface or to DAL.
4. Keep changes inside `SAPSYNCMAMPA` when the question is about MAMPA sync.
5. Record the lesson in `brain/learnings/` if the discovery is reusable.

