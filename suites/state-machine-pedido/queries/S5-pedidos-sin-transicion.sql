-- S5: Pedidos sin ninguna transicion registrada (sospechoso si tienen >1 dia)
-- Detecta pedidos creados que nunca pasaron por el flujo
SELECT TOP 50
  e.idpedidoenc,
  e.no_pedido,
  e.estado,
  e.fec_agr,
  DATEDIFF(DAY, e.fec_agr, GETDATE()) AS edad_dias
FROM pedido_enc e
LEFT JOIN pedido_estado_log l ON l.idpedidoenc = e.idpedidoenc
WHERE l.idpedidoenc IS NULL
  AND DATEDIFF(DAY, e.fec_agr, GETDATE()) >= 1
ORDER BY e.fec_agr ASC;
