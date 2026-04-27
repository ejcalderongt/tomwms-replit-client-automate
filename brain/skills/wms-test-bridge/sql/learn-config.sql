-- learn-config: extrae la configuracion completa por bodega
-- Origen real: tabla i_nav_config_enc (no la vista VW_Configuracioninv).
-- VW_Configuracioninv solo expone metadatos (id, nombre, propietario), NO los flags.
--
-- Uso:
--   :id_empresa  (default 1)
--   :id_bodega   (filtra una bodega; omitir el WHERE de bodega para todas)

-- Forma 1: una bodega
SELECT c.*, b.nombre AS bodega_nombre, b.codigo AS bodega_codigo
FROM dbo.i_nav_config_enc c
LEFT JOIN dbo.bodega b ON b.idEmpresa = c.idempresa AND b.idBodega = c.idbodega
WHERE c.idEmpresa = :id_empresa AND c.idBodega = :id_bodega;

-- Forma 2: todas las bodegas de una empresa
-- SELECT c.*, b.nombre AS bodega_nombre, b.codigo AS bodega_codigo
-- FROM dbo.i_nav_config_enc c
-- LEFT JOIN dbo.bodega b ON b.idEmpresa = c.idempresa AND b.idBodega = c.idbodega
-- WHERE c.idEmpresa = :id_empresa
-- ORDER BY c.idbodega;

-- Bodegas en cada cliente (ya verificadas):
--   TOMWMS_KILLIOS_PRD  (idEmpresa=1): bodegas 1=BOD1, 2=PRTOK, 3=PRTO, 4=BOD5, 5=PRTK17, 6=PRT17
--   IMS4MB_BYB_PRD      (idEmpresa=1): 2 bodegas
--   IMS4MB_CEALSA_QAS   (idEmpresa=1): 2 bodegas (1=general, 2=fiscal)
