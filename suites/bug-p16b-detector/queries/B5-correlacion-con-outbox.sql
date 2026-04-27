-- B5: Cruzar pedidos con vuelta atras vs eventos del outbox
-- Hipotesis: el bug ocurre cuando NavSync reprocesa una transaccion ya enviada
SELECT TOP 50
  l.idpedidoenc,
  e.no_pedido,
  l.estado_anterior,
  l.estado_nuevo,
  l.fec_cambio AS bug_fecha,
  o.idtransaccion,
  o.tipo_transaccion,
  o.enviado,
  o.cantidad,
  o.cantidad_enviada,
  o.fec_agr AS outbox_fec_agr,
  o.fec_mod AS outbox_fec_mod,
  DATEDIFF(MINUTE, o.fec_mod, l.fec_cambio) AS minutos_entre_outbox_y_bug
FROM pedido_estado_log l
INNER JOIN pedido_enc e ON e.idpedidoenc = l.idpedidoenc
LEFT JOIN i_nav_transacciones_out o ON o.no_pedido = e.no_pedido
WHERE l.estado_anterior IN ('DESPACHADO','CERRADO')
  AND l.estado_nuevo IN ('PENDIENTE','EN_PROCESO','PARCIAL')
  AND ABS(DATEDIFF(MINUTE, o.fec_mod, l.fec_cambio)) < 60
ORDER BY l.fec_cambio DESC;
