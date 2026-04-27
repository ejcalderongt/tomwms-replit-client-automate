-- O3: Pendientes actuales por tipo
SELECT
  tipo_transaccion,
  COUNT(*) AS pendientes,
  MIN(fec_agr) AS mas_vieja,
  MAX(fec_agr) AS mas_reciente,
  DATEDIFF(DAY, MIN(fec_agr), GETDATE()) AS dias_mas_vieja
FROM i_nav_transacciones_out
WHERE enviado = 0
GROUP BY tipo_transaccion
ORDER BY pendientes DESC;
