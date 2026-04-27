-- ============================================================
-- SCENARIO doble-despacho — EXPECTATIONS
-- Validaciones que deben pasar despues del setup. Cada query devuelve
-- una sola fila con result='OK' o 'FAIL' y un mensaje.
-- ============================================================
SET NOCOUNT ON;

DECLARE @marker varchar(50) = 'SCENARIO-doble-despacho-001';

-- E1: Existe el pedido encabezado en estado DESPACHADO
SELECT 'E1' AS test_id,
       'pedido_enc en estado DESPACHADO' AS descripcion,
       CASE WHEN COUNT(*) = 1 THEN 'OK' ELSE 'FAIL' END AS result,
       'Encontrados ' + CAST(COUNT(*) AS varchar(10)) + ' (esperado 1)' AS detalle
FROM pedido_enc
WHERE user_agr = @marker AND estado = 'DESPACHADO';

-- E2: Existen 2 despachos vinculados
SELECT 'E2' AS test_id,
       '2 despachos confirmados' AS descripcion,
       CASE WHEN COUNT(*) = 2 THEN 'OK' ELSE 'FAIL' END AS result,
       'Encontrados ' + CAST(COUNT(*) AS varchar(10)) + ' (esperado 2)' AS detalle
FROM despacho_enc
WHERE user_agr = @marker AND estado = 'CONFIRMADO';

-- E3: Cantidad despachada total = 100
SELECT 'E3' AS test_id,
       'Cantidad despachada total = 100' AS descripcion,
       CASE WHEN SUM(cantidad) = 100 THEN 'OK' ELSE 'FAIL' END AS result,
       'Total despachado: ' + CAST(SUM(cantidad) AS varchar(20)) AS detalle
FROM despacho_det dd
INNER JOIN despacho_enc de ON de.iddespachoenc = dd.iddespachoenc
WHERE de.user_agr = @marker;

-- E4: pedido_det tiene cantidad_despachada=100 y estado=DESPACHADO
SELECT 'E4' AS test_id,
       'pedido_det con cantidad_despachada=100 y estado DESPACHADO' AS descripcion,
       CASE WHEN COUNT(*) = 1 THEN 'OK' ELSE 'FAIL' END AS result,
       'Filas matching: ' + CAST(COUNT(*) AS varchar(10)) AS detalle
FROM pedido_det
WHERE user_agr = @marker
  AND cantidad_despachada = 100
  AND estado = 'DESPACHADO';

-- E5: 2 registros en outbox tipo SALIDA pendientes (60 + 40)
SELECT 'E5' AS test_id,
       '2 registros outbox SALIDA pendientes con cantidades 60 y 40' AS descripcion,
       CASE WHEN COUNT(*) = 2 AND SUM(cantidad) = 100 THEN 'OK' ELSE 'FAIL' END AS result,
       'Cnt: ' + CAST(COUNT(*) AS varchar(10)) + ' Sum: ' + CAST(ISNULL(SUM(cantidad),0) AS varchar(20)) AS detalle
FROM i_nav_transacciones_out
WHERE user_agr = @marker AND tipo_transaccion = 'SALIDA' AND enviado = 0;
