-- S1: Distribucion de estados actuales del encabezado
SELECT
  estado,
  COUNT(*) AS cnt,
  MIN(fec_agr) AS pedido_mas_viejo,
  MAX(fec_agr) AS pedido_mas_reciente
FROM pedido_enc
GROUP BY estado
ORDER BY cnt DESC;
