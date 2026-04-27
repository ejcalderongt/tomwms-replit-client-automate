---
id: db-brain-sp-sp-eliminar-by-referencia
type: db-sp
title: dbo.sp_eliminar_by_Referencia
schema: dbo
name: sp_eliminar_by_Referencia
kind: sp
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.sp_eliminar_by_Referencia`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2024-02-01 |
| Parámetros | 1 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@Referencia` | `nvarchar(255)` |  |

## Consume

- `stock_res`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_enc`
- `trans_picking_op`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE [dbo].[sp_eliminar_by_Referencia]
    @Referencia NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdPedidoEnc INT;
    DECLARE @IdPickingEnc INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Obtener IdPedidoEnc asociado con la Referencia
        SELECT @IdPedidoEnc = IdPedidoEnc FROM trans_pe_enc WHERE Referencia = @Referencia;

        -- Verificar si se encontró IdPedidoEnc
        IF @IdPedidoEnc > 0 BEGIN

            -- Obtener IdPickingEnc asociado con IdPedidoEnc
            SELECT @IdPickingEnc = IdPickingEnc FROM trans_pe_enc WHERE IdPedidoEnc = @IdPedidoEnc;

            -- Verificar si se encontró IdPickingEnc
            IF @IdPickingEnc > 0 BEGIN
                -- Eliminar registros de las tablas relacionadas con IdPickingEnc
                DELETE FROM trans_picking_ubic WHERE IdPickingEnc = @IdPickingEnc;
                DELETE FROM trans_picking_det WHERE IdPickingEnc = @IdPickingEnc;
                DELETE FROM trans_picking_op WHERE IdPickingEnc = @IdPickingEnc;
                DELETE FROM trans_picking_enc WHERE IdPickingEnc = @IdPickingEnc;
            END

            DELETE FROM stock_res WHERE IdPedido = @IdPedidoEnc;

            -- Eliminar registros de la tabla trans_pe_det
            DELETE FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc;

            -- Eliminar registros de la tabla trans_pe_enc
            DELETE FROM trans_pe_enc WHERE IdPedidoEnc = @IdPedidoEnc;
        END

        -- Si todo sale bien, confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, hacer rollback de la transacción
        ROLLBACK TRANSACTION;

        -- Opcionalmente, devolver información del error
        DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT;
        SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY();
        RAISERROR(@ErrMsg, @ErrSeverity, 1);
    END CATCH
END
```
