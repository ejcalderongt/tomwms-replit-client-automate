-- O5: Columnas relacionadas con envio/error/proceso
-- Detecta si esta BD tiene cols extra de error/intentos que otras no tienen.
SELECT COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'i_nav_transacciones_out'
  AND (
       COLUMN_NAME LIKE '%enviad%'
    OR COLUMN_NAME LIKE '%intent%'
    OR COLUMN_NAME LIKE '%error%'
    OR COLUMN_NAME LIKE '%respues%'
    OR COLUMN_NAME LIKE '%fecha%'
    OR COLUMN_NAME LIKE '%fec_%'
    OR COLUMN_NAME LIKE '%proces%'
    OR COLUMN_NAME LIKE '%estado%'
    OR COLUMN_NAME LIKE '%docnum%'
    OR COLUMN_NAME LIKE '%doc_num%'
  )
ORDER BY ORDINAL_POSITION;
