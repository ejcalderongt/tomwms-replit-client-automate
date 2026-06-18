/*
    #EJC20260618
    MAMPA - Interface productos SAP -> WMS
    Servidor objetivo informado: 10.238.26.70

    Objetivo:
      - Reducir costo de lookups usados por clsSyncSAPProducto.
      - Dar un SP liviano de preflight para medir staging y pendientes antes/despues.

    Aplicar primero en QAS. Script idempotente.
*/

SET NOCOUNT ON;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.producto_bodega')
      AND name IN (
          N'UX_MAMPA_producto_bodega_IdProducto_IdBodega_20260618',
          N'IX_MAMPA_producto_bodega_IdProducto_IdBodega_20260618'
      )
)
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM dbo.producto_bodega WITH (NOLOCK)
        WHERE IdProducto IS NOT NULL AND IdBodega IS NOT NULL
        GROUP BY IdProducto, IdBodega
        HAVING COUNT_BIG(1) > 1
    )
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX UX_MAMPA_producto_bodega_IdProducto_IdBodega_20260618
        ON dbo.producto_bodega (IdProducto, IdBodega)
        WHERE IdProducto IS NOT NULL AND IdBodega IS NOT NULL;
    END
    ELSE
    BEGIN
        CREATE NONCLUSTERED INDEX IX_MAMPA_producto_bodega_IdProducto_IdBodega_20260618
        ON dbo.producto_bodega (IdProducto, IdBodega)
        INCLUDE (IdProductoBodega, activo);
    END
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.unidad_medida')
      AND name = N'IX_MAMPA_unidad_medida_nombre_20260618'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_MAMPA_unidad_medida_nombre_20260618
    ON dbo.unidad_medida (nombre)
    INCLUDE (IdUnidadMedida, codigo, activo);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.producto_clasificacion')
      AND name = N'IX_MAMPA_producto_clasificacion_codigo_20260618'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_MAMPA_producto_clasificacion_codigo_20260618
    ON dbo.producto_clasificacion (codigo)
    INCLUDE (IdClasificacion, nombre, activo, IdPropietario);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.producto_tipo')
      AND name = N'IX_MAMPA_producto_tipo_codigo_20260618'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_MAMPA_producto_tipo_codigo_20260618
    ON dbo.producto_tipo (codigo)
    INCLUDE (IdTipoProducto, NombreTipoProducto, activo, IdPropietario);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.producto_marca')
      AND name = N'IX_MAMPA_producto_marca_codigo_20260618'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_MAMPA_producto_marca_codigo_20260618
    ON dbo.producto_marca (codigo)
    INCLUDE (IdMarca, nombre, activo, IdPropietario);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.producto_familia')
      AND name = N'IX_MAMPA_producto_familia_codigo_20260618'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_MAMPA_producto_familia_codigo_20260618
    ON dbo.producto_familia (codigo)
    INCLUDE (IdFamilia, nombre, activo, IdPropietario);
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_mampa_producto_interface_preflight_v1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        StagingProductos = COUNT_BIG(1),
        StagingSinCodigo = SUM(CASE WHEN NULLIF(LTRIM(RTRIM(No)), N'') IS NULL THEN 1 ELSE 0 END),
        StagingSinUMBas = SUM(CASE WHEN NULLIF(LTRIM(RTRIM(Base_Unit_Of_Measure)), N'') IS NULL THEN 1 ELSE 0 END)
    FROM dbo.i_nav_producto WITH (NOLOCK);

    SELECT
        ProductosStaging = COUNT_BIG(1),
        YaExistenEnWMS = SUM(CASE WHEN p.IdProducto IS NULL THEN 0 ELSE 1 END),
        NuevosParaWMS = SUM(CASE WHEN p.IdProducto IS NULL THEN 1 ELSE 0 END)
    FROM dbo.i_nav_producto ip WITH (NOLOCK)
    LEFT JOIN dbo.producto p WITH (NOLOCK)
        ON p.codigo = ip.No;

    SELECT TOP (50)
        ip.No,
        ip.Description,
        ip.Base_Unit_Of_Measure,
        Motivo = CONCAT(
            CASE WHEN um.IdUnidadMedida IS NULL THEN 'UMBas no existe; ' ELSE '' END,
            CASE WHEN NULLIF(LTRIM(RTRIM(ip.No)), N'') IS NULL THEN 'Codigo vacio; ' ELSE '' END
        )
    FROM dbo.i_nav_producto ip WITH (NOLOCK)
    LEFT JOIN dbo.unidad_medida um WITH (NOLOCK)
        ON um.nombre = ip.Base_Unit_Of_Measure
    WHERE um.IdUnidadMedida IS NULL
       OR NULLIF(LTRIM(RTRIM(ip.No)), N'') IS NULL
    ORDER BY ip.No;

    SELECT TOP (50)
        IdProducto,
        IdBodega,
        Duplicados = COUNT_BIG(1)
    FROM dbo.producto_bodega WITH (NOLOCK)
    WHERE IdProducto IS NOT NULL AND IdBodega IS NOT NULL
    GROUP BY IdProducto, IdBodega
    HAVING COUNT_BIG(1) > 1
    ORDER BY COUNT_BIG(1) DESC, IdProducto, IdBodega;
END
GO
