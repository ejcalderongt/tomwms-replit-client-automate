-- Movimientos en ventana de tiempo
SELECT TOP 5000
    m.*
FROM trans_movimientos m
WHERE m.fecha >= '{{FROM}}'
  AND m.fecha <= '{{TO}}'
  AND ({{IDPRODUCTO}} = 0 OR m.idproducto = {{IDPRODUCTO}})
  AND ({{IDBODEGA}} = 0 OR m.idbodega = {{IDBODEGA}})
ORDER BY m.fecha DESC;
