---
slug: 2026-05-20-hh-recepcion-pallet-presentacion-cantidad
estado: aplicado
fecha_aplicado: 2026-05-20
agente: codex-local (con guia de Erik en sesion de chat)
commits:
  - repo: TOMHH2025
    sha: (pendiente, sin commit aun)
    msg: "(pendiente)"
incidentes: corregido en sesion (ver LEARNINGS.md)
---

# Output

## Cambio aplicado
Modificado `frm_recepcion_datos.java`, bloque de asignacion de cantidades tras
escaneo de pallet de proveedor.

## Build
- Gradle build debug: OK.
- Sin warnings nuevos.

## Estado git
- Cambio local en working tree.
- NO commiteado.
- NO pusheado a Azure DevOps.

# Notas para Replit

- Promocionar la regla "UMBAS vs Cantidad_Presentacion" al brain canonico
  (ver propuesta en LEARNINGS.md y archivo destino sugerido).
- Sumar a `replit.md` §4 como regla vinculante si Erik confirma que aplica
  cross-cliente (no solo recepcion HH).
- Considerar abrir handoff de **auditoria** sobre los demas flujos que
  manipulan `vBeStockRecPallet.Cantidad` y `vBeStockRecDet.Cantidad` por si
  el mismo error esta replicado en otros frm_*.java de Recepcion/Picking.
