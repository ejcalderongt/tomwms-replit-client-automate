-- Snapshot de stock por producto/bodega en rango de tiempo (ajusta campos según tu esquema)
SELECT TOP 2000
    s.*
FROM stock s
WHERE ({{IDPRODUCTO}} = 0 OR s.idproducto = {{IDPRODUCTO}})
  AND ({{IDBODEGA}} = 0 OR s.idbodega = {{IDBODEGA}})
ORDER BY s.idproducto, s.idbodega;
