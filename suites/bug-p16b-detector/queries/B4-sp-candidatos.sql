-- B4: Stored Procedures cuyo cuerpo contiene UPDATE pedido_enc SET estado
-- Identifica los puntos de escritura del estado del encabezado (sospechosos del bug)
SELECT
  o.name AS sp_nombre,
  o.type_desc,
  o.create_date,
  o.modify_date,
  CASE WHEN m.definition LIKE '%pedido_enc%' AND m.definition LIKE '%estado%' AND m.definition LIKE '%UPDATE%' THEN 'PROBABLE'
       ELSE 'POSIBLE' END AS clasificacion
FROM sys.sql_modules m
INNER JOIN sys.objects o ON o.object_id = m.object_id
WHERE m.definition LIKE '%pedido_enc%'
  AND m.definition LIKE '%estado%'
  AND o.type IN ('P','TR','FN')
ORDER BY o.modify_date DESC;
