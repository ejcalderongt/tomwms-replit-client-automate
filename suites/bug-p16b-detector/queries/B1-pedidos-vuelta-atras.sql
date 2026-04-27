-- B1: Pedidos con transicion DESPACHADO -> PENDIENTE (vuelta atras = bug P-16b)
SELECT
  l.idpedidoenc,
  e.no_pedido,
  l.estado_anterior,
  l.estado_nuevo,
  l.fec_cambio,
  l.user_cambio,
  e.estado AS estado_actual
FROM pedido_estado_log l
INNER JOIN pedido_enc e ON e.idpedidoenc = l.idpedidoenc
WHERE l.estado_anterior IN ('DESPACHADO', 'CERRADO')
  AND l.estado_nuevo IN ('PENDIENTE', 'EN_PROCESO', 'PARCIAL')
ORDER BY l.fec_cambio DESC;
