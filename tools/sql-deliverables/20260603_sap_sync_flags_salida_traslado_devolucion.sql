/*
    2026-06-03
    Autor: Codex/EJC
    Objetivo:
      Parametrizar disparo asíncrono (via worker WMS) para envíos SAP desde WMS
      agregando 3 flags en i_nav_config_enc:
        - enviar_salida_sap_via_ws
        - enviar_traslado_sap_via_ws
        - enviar_devolucion_sap_via_ws

    Notas:
      - Script idempotente.
      - Mantiene compatibilidad con instalaciones existentes.
*/

SET NOCOUNT ON;
GO

/* 1) Flag: salida SAP via worker */
IF COL_LENGTH('dbo.i_nav_config_enc', 'enviar_salida_sap_via_ws') IS NULL
BEGIN
    ALTER TABLE dbo.i_nav_config_enc
        ADD enviar_salida_sap_via_ws bit NOT NULL
            CONSTRAINT DF_i_nav_config_enc_enviar_salida_sap_via_ws DEFAULT (0);
END
GO

/* 2) Flag: traslado SAP via worker */
IF COL_LENGTH('dbo.i_nav_config_enc', 'enviar_traslado_sap_via_ws') IS NULL
BEGIN
    ALTER TABLE dbo.i_nav_config_enc
        ADD enviar_traslado_sap_via_ws bit NOT NULL
            CONSTRAINT DF_i_nav_config_enc_enviar_traslado_sap_via_ws DEFAULT (0);
END
GO

/* 3) Flag: devolucion SAP via worker */
IF COL_LENGTH('dbo.i_nav_config_enc', 'enviar_devolucion_sap_via_ws') IS NULL
BEGIN
    ALTER TABLE dbo.i_nav_config_enc
        ADD enviar_devolucion_sap_via_ws bit NOT NULL
            CONSTRAINT DF_i_nav_config_enc_enviar_devolucion_sap_via_ws DEFAULT (0);
END
GO

/* 4) Validacion rapida */
SELECT
    CASE WHEN COL_LENGTH('dbo.i_nav_config_enc', 'enviar_salida_sap_via_ws') IS NULL THEN 0 ELSE 1 END AS ok_enviar_salida_sap_via_ws,
    CASE WHEN COL_LENGTH('dbo.i_nav_config_enc', 'enviar_traslado_sap_via_ws') IS NULL THEN 0 ELSE 1 END AS ok_enviar_traslado_sap_via_ws,
    CASE WHEN COL_LENGTH('dbo.i_nav_config_enc', 'enviar_devolucion_sap_via_ws') IS NULL THEN 0 ELSE 1 END AS ok_enviar_devolucion_sap_via_ws;
GO

