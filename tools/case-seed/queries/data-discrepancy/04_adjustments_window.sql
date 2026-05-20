-- Ajustes de inventario en ventana (si aplica)
SELECT TOP 5000
    d.*, e.*
FROM trans_ajuste_det d
INNER JOIN trans_ajuste_enc e ON e.idajuste = d.idajuste
WHERE e.fecha >= '{{FROM}}'
  AND e.fecha <= '{{TO}}'
  AND ({{IDPRODUCTO}} = 0 OR d.idproducto = {{IDPRODUCTO}})
  AND ({{IDBODEGA}} = 0 OR e.idbodega = {{IDBODEGA}})
ORDER BY e.fecha DESC;
