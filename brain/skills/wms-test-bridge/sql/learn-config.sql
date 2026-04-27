-- learn-config: lee la configuración de interface de un cliente desde SQL Server (Killios PRD)
-- Fuente legacy: /TOMIMSV4/DAL/Interface/Configuracion/ConfiguracionEncabezado/clsLnI_nav_config_enc_Partial.vb (línea 8)
--
-- USO:
--   Reemplazar :id_bodega por el id de la bodega del cliente que se desea aprender.
--   La consulta es READ-ONLY y se ejecuta sobre la rama dev_2023_merge en producción.
--
-- COMPATIBILIDAD dev_2023_merge ↔ dev_2028_merge:
--   La lógica de carga de configuración es IDÉNTICA entre ambas ramas (diff vacío).
--   El único cambio relevante en dev_2028_merge es la migración de varias tablas
--   transaccionales a IDENTITY (eliminando MAX(id)+1) por colisiones de concurrencia.
--   La tabla i_nav_config_enc NO está entre las migradas a IDENTITY a la fecha.
--
-- RESULTADO ESPERADO: 0 o 1 fila por (idEmpresa, idBodega).

SELECT *
FROM   VW_Configuracioninv
WHERE  idEmpresa = 1
  AND  idBodega  = :id_bodega;

-- Variante por correlativo (si se conoce el id del registro):
-- SELECT * FROM VW_Configuracioninv WHERE correlativo = :correlativo;

-- Variante todos los clientes/bodegas (para dump completo):
-- SELECT * FROM VW_Configuracioninv;

-- Tabla cruda (sin la vista):
-- SELECT * FROM i_nav_config_enc WHERE idnavconfigenc = :pIdConfiguracionEncabezado;
