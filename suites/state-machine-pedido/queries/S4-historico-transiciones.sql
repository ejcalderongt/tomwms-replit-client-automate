-- S4: Historico de transiciones de los ultimos 30 dias
-- Asume tabla pedido_estado_log o similar. Si no existe, ajustar nombre.
SELECT TOP 200
  idpedidoenc,
  estado_anterior,
  estado_nuevo,
  fec_cambio,
  user_cambio
FROM pedido_estado_log
WHERE fec_cambio >= DATEADD(DAY, -30, GETDATE())
ORDER BY fec_cambio DESC;
