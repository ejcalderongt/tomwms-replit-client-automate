-- O6: Edad de los pendientes (histograma por rango)
SELECT
  CASE
    WHEN DATEDIFF(DAY, fec_agr, GETDATE()) <= 1 THEN '0-1 dia'
    WHEN DATEDIFF(DAY, fec_agr, GETDATE()) <= 7 THEN '2-7 dias'
    WHEN DATEDIFF(DAY, fec_agr, GETDATE()) <= 30 THEN '8-30 dias'
    WHEN DATEDIFF(DAY, fec_agr, GETDATE()) <= 90 THEN '31-90 dias'
    ELSE '> 90 dias'
  END AS rango_edad,
  tipo_transaccion,
  COUNT(*) AS cnt
FROM i_nav_transacciones_out
WHERE enviado = 0
GROUP BY
  CASE
    WHEN DATEDIFF(DAY, fec_agr, GETDATE()) <= 1 THEN '0-1 dia'
    WHEN DATEDIFF(DAY, fec_agr, GETDATE()) <= 7 THEN '2-7 dias'
    WHEN DATEDIFF(DAY, fec_agr, GETDATE()) <= 30 THEN '8-30 dias'
    WHEN DATEDIFF(DAY, fec_agr, GETDATE()) <= 90 THEN '31-90 dias'
    ELSE '> 90 dias'
  END,
  tipo_transaccion
ORDER BY rango_edad, tipo_transaccion;
