-- ============================================================
-- SCENARIO doble-despacho — TEARDOWN
-- Borra todos los registros generados por el setup (matchea por marker)
-- Idempotente: se puede correr multiples veces.
-- ============================================================
SET XACT_ABORT ON;
SET NOCOUNT ON;

DECLARE @marker varchar(50) = 'SCENARIO-doble-despacho-001';

BEGIN TRANSACTION;

-- Outbox primero (no FK al revés)
DELETE FROM i_nav_transacciones_out WHERE user_agr = @marker;

-- Despacho detalle, despacho enc
DELETE dd
FROM despacho_det dd
INNER JOIN despacho_enc de ON de.iddespachoenc = dd.iddespachoenc
WHERE de.user_agr = @marker;

DELETE FROM despacho_enc WHERE user_agr = @marker;

-- Pedido detalle, pedido enc
DELETE FROM pedido_det WHERE user_agr = @marker;
DELETE FROM pedido_enc WHERE user_agr = @marker;

COMMIT TRANSACTION;

PRINT 'SCENARIO doble-despacho limpiado. Marker: ' + @marker;
