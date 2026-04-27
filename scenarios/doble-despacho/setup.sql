-- ============================================================
-- SCENARIO doble-despacho — SETUP
-- Crea un pedido con 1 producto y cantidad 100, lo despacha en 2 viajes (60 + 40)
-- y emite los registros de outbox correspondientes.
-- ============================================================
-- ABORTAR si alguna tabla no existe:
SET XACT_ABORT ON;
SET NOCOUNT ON;

DECLARE @marker varchar(50) = 'SCENARIO-doble-despacho-001';
DECLARE @no_pedido varchar(50) = 'TEST-' + CONVERT(varchar(20), GETDATE(), 112) + '-001';
DECLARE @idpedidoenc int;
DECLARE @idpedidodet int;
DECLARE @iddespacho1 int;
DECLARE @iddespacho2 int;

BEGIN TRANSACTION;

-- 1. Pedido encabezado
INSERT INTO pedido_enc (no_pedido, estado, fec_agr, user_agr, observaciones)
VALUES (@no_pedido, 'PENDIENTE', GETDATE(), @marker, 'SEED ' + @marker);
SET @idpedidoenc = SCOPE_IDENTITY();

-- 2. Pedido detalle (1 linea, 100 unidades)
INSERT INTO pedido_det (
  idpedidoenc, codigo_producto, cantidad, cantidad_despachada,
  estado, fec_agr, user_agr
)
VALUES (
  @idpedidoenc, 'PROD-TEST-001', 100, 0,
  'PENDIENTE', GETDATE(), @marker
);
SET @idpedidodet = SCOPE_IDENTITY();

-- 3. Primer despacho parcial (60 unidades)
INSERT INTO despacho_enc (
  no_documento_salida, idpedidoenc, estado, fec_agr, user_agr
)
VALUES (
  'DESP-' + @no_pedido + '-V1', @idpedidoenc, 'CONFIRMADO', GETDATE(), @marker
);
SET @iddespacho1 = SCOPE_IDENTITY();

INSERT INTO despacho_det (
  iddespachoenc, idpedidodet, codigo_producto, cantidad, fec_agr, user_agr
)
VALUES (
  @iddespacho1, @idpedidodet, 'PROD-TEST-001', 60, GETDATE(), @marker
);

UPDATE pedido_det
   SET cantidad_despachada = 60, estado = 'PARCIAL', fec_mod = GETDATE()
 WHERE idpedidodet = @idpedidodet;

UPDATE pedido_enc
   SET estado = 'PARCIAL', fec_mod = GETDATE()
 WHERE idpedidoenc = @idpedidoenc;

-- Outbox del despacho 1
INSERT INTO i_nav_transacciones_out (
  no_pedido, codigo_producto, cantidad, cantidad_enviada, cantidad_pendiente,
  tipo_transaccion, enviado, IdTipoDocumento, fec_agr, user_agr
)
VALUES (
  @no_pedido, 'PROD-TEST-001', 60, 0, 60,
  'SALIDA', 0, 1, GETDATE(), @marker
);

-- 4. Segundo despacho parcial (40 unidades) — completa el pedido
WAITFOR DELAY '00:00:01';

INSERT INTO despacho_enc (
  no_documento_salida, idpedidoenc, estado, fec_agr, user_agr
)
VALUES (
  'DESP-' + @no_pedido + '-V2', @idpedidoenc, 'CONFIRMADO', GETDATE(), @marker
);
SET @iddespacho2 = SCOPE_IDENTITY();

INSERT INTO despacho_det (
  iddespachoenc, idpedidodet, codigo_producto, cantidad, fec_agr, user_agr
)
VALUES (
  @iddespacho2, @idpedidodet, 'PROD-TEST-001', 40, GETDATE(), @marker
);

UPDATE pedido_det
   SET cantidad_despachada = 100, estado = 'DESPACHADO', fec_mod = GETDATE()
 WHERE idpedidodet = @idpedidodet;

UPDATE pedido_enc
   SET estado = 'DESPACHADO', fec_mod = GETDATE()
 WHERE idpedidoenc = @idpedidoenc;

-- Outbox del despacho 2
INSERT INTO i_nav_transacciones_out (
  no_pedido, codigo_producto, cantidad, cantidad_enviada, cantidad_pendiente,
  tipo_transaccion, enviado, IdTipoDocumento, fec_agr, user_agr
)
VALUES (
  @no_pedido, 'PROD-TEST-001', 40, 0, 40,
  'SALIDA', 0, 1, GETDATE(), @marker
);

COMMIT TRANSACTION;

PRINT 'SCENARIO doble-despacho aplicado.';
PRINT 'no_pedido seed: ' + @no_pedido;
PRINT 'idpedidoenc: ' + CAST(@idpedidoenc AS varchar(20));
PRINT 'Marker (para identificar y limpiar): ' + @marker;
