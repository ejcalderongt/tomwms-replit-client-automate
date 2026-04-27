-- B2: Pedidos donde el encabezado dice un estado pero el detalle dice otro (estado actual)
-- No requiere pedido_estado_log. Detecta el bug aun sin historico.
SELECT TOP 50
  e.idpedidoenc,
  e.no_pedido,
  e.estado AS estado_enc,
  e.fec_mod AS fec_mod_enc,
  COUNT(d.idpedidodet) AS lineas_total,
  SUM(CASE WHEN d.estado = 'DESPACHADO' THEN 1 ELSE 0 END) AS lineas_despachadas,
  SUM(CASE WHEN d.estado = 'PENDIENTE' THEN 1 ELSE 0 END) AS lineas_pendientes,
  MAX(d.fec_mod) AS fec_mod_ult_det
FROM pedido_enc e
INNER JOIN pedido_det d ON d.idpedidoenc = e.idpedidoenc
GROUP BY e.idpedidoenc, e.no_pedido, e.estado, e.fec_mod
HAVING e.estado = 'PENDIENTE'
   AND SUM(CASE WHEN d.estado = 'DESPACHADO' THEN 1 ELSE 0 END) = COUNT(d.idpedidodet)
ORDER BY e.fec_mod DESC;
