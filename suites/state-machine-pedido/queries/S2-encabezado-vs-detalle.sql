-- S2: Coherencia estado encabezado vs estado de cada linea
-- Detecta encabezados que dicen "DESPACHADO" pero tienen lineas pendientes (sintoma P-16b)
SELECT
  e.estado AS estado_enc,
  d.estado AS estado_det,
  COUNT(*) AS lineas
FROM pedido_enc e
INNER JOIN pedido_det d ON d.idpedidoenc = e.idpedidoenc
GROUP BY e.estado, d.estado
ORDER BY e.estado, d.estado;
