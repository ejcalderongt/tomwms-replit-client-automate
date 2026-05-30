# Codex context: fix packing parcial 2026-05-30
## Cambios en dev_2028_merge

### TOMWMS_BOF  7c1b670f
clsLnTrans_packing_enc_Partial.vb — Inserta_Packing:
  Else (parcial): UPDATE trans_picking_ubic SET fecha_packing=NULL WHERE IdPickingUbic=@id
  (antes: New Date(1900,1,1) + Actualizar_FechaPacking → excluía ubic del VW)

### TOMHH2025  5194b334
frm_preparacion_packing.java — pickSnap (ArrayList<clsBeTrans_picking_ubic>):
  processListUbic → guarda snap cuando non-empty
  agregaLP → repobla pick.items desde snap si vacío
  creaListaLotes → sourceItems = pick.items ?: pickSnap
  processActualizaEstado → snap.clear()
frm_lista_packing_lp.java — UX: "PENDIENTES DE PACKING" + LP/bodega/op siempre visible
