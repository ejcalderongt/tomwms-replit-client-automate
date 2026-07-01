-- Vista de stock de consulta (puede variar por cliente)
SELECT TOP 5000
    v.*
FROM VW_Stock_Res v
WHERE ({{IDPRODUCTO}} = 0 OR v.idproducto = {{IDPRODUCTO}})
  AND ({{IDBODEGA}} = 0 OR v.idbodega = {{IDBODEGA}})
ORDER BY v.idproducto, v.idbodega;
