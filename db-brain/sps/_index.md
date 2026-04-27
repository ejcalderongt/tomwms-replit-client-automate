# Index — Stored Procedures

> Snapshot 2026-04-27: **0 SPs** publicados como entity individual. Total reportado: **39**.

Esperan extracción full. SPs de interés ya identificados (todos referencian `stock`):
- `CLBD`, `CLBD_INICIARBD`, `CLBD_PRC`, `CLBD_PRC_BY_IDBODEGA` — Cierre Bodega (familia CLBD).
- `GetListaStockByProductoBodega`, `GetResumenStockCantidad`.
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (+ `_Actualizacion`, `_Actualizacion_Sin_Importacion`) — importación de stock.
