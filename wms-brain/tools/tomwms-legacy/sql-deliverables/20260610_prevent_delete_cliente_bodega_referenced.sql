/*
    2026-06-10
    Autor: Codex/EJC
    Objetivo:
      Blindar eliminación de cliente_bodega cuando exista referencia en trans_pe_enc
      para la misma combinación (IdCliente, IdBodega).

    Contexto incidente MHS DEV:
      - Pedido: trans_pe_enc.Referencia = 'CSFA02-36522'
      - IdCliente = 1280, IdBodega = 2
      - Causa raíz observada: faltaba relación cliente_bodega para esa bodega.

    Nota:
      - Idempotente.
      - Bloquea DELETE físico; el mantenimiento debe resolver dependencia primero.
*/

SET NOCOUNT ON;
GO

IF OBJECT_ID('dbo.TR_cliente_bodega_prevent_delete_when_referenced', 'TR') IS NOT NULL
    DROP TRIGGER dbo.TR_cliente_bodega_prevent_delete_when_referenced;
GO

CREATE TRIGGER dbo.TR_cliente_bodega_prevent_delete_when_referenced
ON dbo.cliente_bodega
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM deleted d
        INNER JOIN dbo.trans_pe_enc pe
            ON pe.IdCliente = d.IdCliente
           AND pe.IdBodega = d.IdBodega
    )
    BEGIN
        RAISERROR('No se puede eliminar cliente_bodega: existe al menos un pedido (trans_pe_enc) asociado a esa bodega/cliente.', 16, 1);
        RETURN;
    END

    DELETE cb
    FROM dbo.cliente_bodega cb
    INNER JOIN deleted d
        ON d.IdClienteBodega = cb.IdClienteBodega;
END
GO

/* Validación rápida */
SELECT
    name AS trigger_name,
    is_disabled
FROM sys.triggers
WHERE parent_id = OBJECT_ID('dbo.cliente_bodega')
  AND name = 'TR_cliente_bodega_prevent_delete_when_referenced';
GO
