-- O4: Sample de pendientes mas viejos (TOP 15)
-- Util para inspeccionar casos abandonados sin tener que volcar la tabla entera.
SELECT TOP 15
  idtransaccion,
  tipo_transaccion,
  no_pedido,
  no_documento_salida_ref_devol,
  codigo_producto,
  cantidad,
  cantidad_enviada,
  cantidad_pendiente,
  fec_agr,
  DATEDIFF(DAY, fec_agr, GETDATE()) AS dias_pendiente,
  user_agr,
  IdTipoDocumento,
  IdPedidoEncDevol
FROM i_nav_transacciones_out
WHERE enviado = 0
ORDER BY fec_agr ASC;
