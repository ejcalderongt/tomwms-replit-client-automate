-- B3: Frecuencia del bug por mes (ultimos 12 meses)
SELECT
  YEAR(l.fec_cambio) AS anio,
  MONTH(l.fec_cambio) AS mes,
  COUNT(DISTINCT l.idpedidoenc) AS pedidos_afectados,
  COUNT(*) AS transiciones_anomalas
FROM pedido_estado_log l
WHERE l.estado_anterior IN ('DESPACHADO', 'CERRADO')
  AND l.estado_nuevo IN ('PENDIENTE', 'EN_PROCESO', 'PARCIAL')
  AND l.fec_cambio >= DATEADD(MONTH, -12, GETDATE())
GROUP BY YEAR(l.fec_cambio), MONTH(l.fec_cambio)
ORDER BY anio DESC, mes DESC;
