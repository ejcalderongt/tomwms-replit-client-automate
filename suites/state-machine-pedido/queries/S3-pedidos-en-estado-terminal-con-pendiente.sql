-- S3: Pedidos en estado terminal (DESPACHADO/CERRADO/CANCELADO) que aun tienen
-- lineas con cantidad_despachada < cantidad. Anomalia clasica.
SELECT TOP 50
  e.idpedidoenc,
  e.no_pedido,
  e.estado AS estado_enc,
  e.fec_agr,
  e.fec_mod,
  d.idpedidodet,
  d.codigo_producto,
  d.cantidad,
  d.cantidad_despachada,
  d.cantidad - ISNULL(d.cantidad_despachada, 0) AS pendiente
FROM pedido_enc e
INNER JOIN pedido_det d ON d.idpedidoenc = e.idpedidoenc
WHERE e.estado IN ('DESPACHADO', 'CERRADO', 'CANCELADO')
  AND d.cantidad > ISNULL(d.cantidad_despachada, 0)
ORDER BY e.fec_mod DESC;
