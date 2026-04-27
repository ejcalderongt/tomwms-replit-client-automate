-- S7: Coherencia entre cantidad solicitada y cantidad efectivamente despachada
-- Detecta despachos parciales no reflejados en estado del encabezado.
SELECT
  e.estado AS estado_enc,
  COUNT(DISTINCT e.idpedidoenc) AS pedidos,
  SUM(d.cantidad) AS cantidad_solicitada_total,
  SUM(ISNULL(d.cantidad_despachada, 0)) AS cantidad_despachada_total,
  SUM(d.cantidad - ISNULL(d.cantidad_despachada, 0)) AS cantidad_pendiente_total,
  CAST(
    SUM(ISNULL(d.cantidad_despachada, 0)) * 100.0 / NULLIF(SUM(d.cantidad), 0)
    AS decimal(5,2)
  ) AS pct_despachado
FROM pedido_enc e
INNER JOIN pedido_det d ON d.idpedidoenc = e.idpedidoenc
GROUP BY e.estado
ORDER BY e.estado;
