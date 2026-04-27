-- O2: Distribucion enviado x tipo_transaccion
-- Matriz 2x2. Permite ver si la interface procesa INGRESO y SALIDA de forma balanceada.
SELECT
  enviado,
  tipo_transaccion,
  COUNT(*) AS cnt,
  CAST(COUNT(*) * 100.0 / NULLIF(SUM(COUNT(*)) OVER (PARTITION BY tipo_transaccion), 0) AS decimal(5,2)) AS pct_dentro_tipo
FROM i_nav_transacciones_out
GROUP BY enviado, tipo_transaccion
ORDER BY tipo_transaccion, enviado;
