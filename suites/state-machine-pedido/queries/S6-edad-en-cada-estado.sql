-- S6: Edad media y maxima de pedidos en cada estado
SELECT
  estado,
  COUNT(*) AS cnt,
  AVG(DATEDIFF(DAY, fec_mod, GETDATE())) AS dias_promedio,
  MAX(DATEDIFF(DAY, fec_mod, GETDATE())) AS dias_maximo,
  SUM(CASE WHEN DATEDIFF(DAY, fec_mod, GETDATE()) > 30 THEN 1 ELSE 0 END) AS mayores_30d
FROM pedido_enc
GROUP BY estado
ORDER BY dias_maximo DESC;
