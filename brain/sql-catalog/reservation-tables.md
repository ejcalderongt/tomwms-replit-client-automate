---
id: reservation-tables
tipo: sql-catalog
estado: vigente
titulo: SQL Catalog · Reservation tables (DDL completo)
tags: [sql-catalog]
---

# SQL Catalog · Reservation tables (DDL completo)

> **Propósito**: catálogo SQL DDL canónico de las 9 tablas del módulo `reservation` (8 tablas críticas + tabla auxiliar `trans_pe_det_log_reserva`). Schemas validados live contra Killios productivo (`52.41.114.122,1437`, BD `TOMWMS_KILLIOS_PRD`) el 2026-04-27.
>
> **Cross-refs**: `entities/modules/reservation/08-mi3-tablas-killios.md` (descripción funcional), `entities/modules/reservation/04-mi3-config-propietario.md` (i_nav_config_enc específica), `entities/modules/reservation/07-stock-res-ciclo-vida.md` (ciclo de stock_res).
>
> **NOTA CRÍTICA**: Killios productivo es READ-ONLY desde wms-brain. Estos DDLs son referencia documental, NO se ejecutan contra producción. Cualquier cambio se hace por canal autorizado del DBA.

---

## Índice

1. `stock` (33 cols)
2. `stock_res` (35 cols)
3. `trans_pe_enc` (70 cols)
4. `trans_pe_det` (44 cols)
5. `i_nav_config_enc` (69 cols)
6. `i_nav_ped_traslado_det` (22 cols)
7. `log_error_wms` (15 cols)
8. `propietarios` (23 cols)
9. `propietario_bodega` (8 cols)
10. `trans_pe_det_log_reserva` (auxiliar bitácora granular)
11. Inventario de FKs y índices recomendados

---

## 1. `stock` (33 cols) · inventario físico

```sql
CREATE TABLE dbo.stock (
    IdBodega                INT             NOT NULL,
    IdStock                 INT             NOT NULL,
    IdPropietarioBodega     INT             NOT NULL,
    IdProductoBodega        INT             NOT NULL,
    IdProductoEstado        INT             NOT NULL,
    IdPresentacion          INT             NULL,
    IdUnidadMedida          INT             NULL,
    IdUbicacion             INT             NOT NULL,
    IdUbicacion_anterior    INT             NULL,
    IdRecepcionEnc          INT             NULL,
    IdRecepcionDet          INT             NULL,
    IdPedidoEnc             INT             NULL,
    IdPickingEnc            INT             NULL,
    IdDespachoEnc           INT             NULL,
    lote                    NVARCHAR(50)    NOT NULL,
    lic_plate               NVARCHAR(50)    NULL,
    serial                  NVARCHAR(50)    NULL,
    cantidad                FLOAT           NOT NULL,
    fecha_ingreso           DATETIME        NULL,
    fecha_vence             DATETIME        NULL,
    uds_lic_plate           FLOAT           NULL,
    no_bulto                INT             NULL,  -- 1965 = marker recursión UMBas legacy
    fecha_manufactura       DATETIME        NULL,
    añada                   INT             NULL,  -- año de cosecha (vinos/aceites)
    user_agr                NVARCHAR(50)    NOT NULL,
    fec_agr                 DATETIME        NOT NULL,
    user_mod                NVARCHAR(50)    NOT NULL,
    fec_mod                 DATETIME        NOT NULL,
    activo                  BIT             NOT NULL,
    peso                    FLOAT           NULL,
    temperatura             FLOAT           NULL,
    atributo_variante_1     NVARCHAR(25)    NULL,
    pallet_no_estandar      BIT             NULL,
    CONSTRAINT PK_stock PRIMARY KEY (IdStock)
);
```

### Índices recomendados

```sql
CREATE INDEX IX_stock_lookup_motor
    ON dbo.stock (IdProductoBodega, IdBodega, activo)
    INCLUDE (cantidad, IdUbicacion, IdPresentacion, fecha_vence, IdProductoEstado, lote, lic_plate);

CREATE INDEX IX_stock_ubicacion ON dbo.stock (IdUbicacion);
CREATE INDEX IX_stock_lic_plate ON dbo.stock (lic_plate) WHERE lic_plate IS NOT NULL;
CREATE INDEX IX_stock_fecha_vence ON dbo.stock (fecha_vence) WHERE fecha_vence IS NOT NULL;
```

---

## 2. `stock_res` (35 cols) · reservas

```sql
CREATE TABLE dbo.stock_res (
    IdStockRes              INT             NOT NULL,
    IdTransaccion           INT             NOT NULL,  -- ID lógico, NO es FK
    Indicador               NVARCHAR(50)    NULL,      -- "PED" / "TRA" / "MAN"
    IdPedidoDet             INT             NOT NULL,  -- FK trans_pe_det.IdPedidoDet
    IdStock                 INT             NOT NULL,  -- FK stock.IdStock
    IdPropietarioBodega     INT             NOT NULL,
    IdProductoBodega        INT             NOT NULL,
    IdProductoEstado        INT             NULL,
    IdPresentacion          INT             NULL,
    IdUnidadMedida          INT             NULL,
    IdUbicacion             INT             NOT NULL,
    ubicacion_ant           NVARCHAR(25)    NULL,      -- string, no FK
    IdRecepcion             BIGINT          NULL,
    lote                    NVARCHAR(50)    NULL,
    lic_plate               NVARCHAR(50)    NULL,
    serial                  NVARCHAR(50)    NULL,
    cantidad                FLOAT           NOT NULL,
    peso                    FLOAT           NULL,
    estado                  NVARCHAR(20)    NULL,      -- UNCOMMITED|COMMITED|CONSUMED|CANCELLED|EXPIRED
    fecha_ingreso           DATETIME        NULL,
    fecha_vence             DATETIME        NULL,
    uds_lic_plate           FLOAT           NULL,
    no_bulto                INT             NULL,      -- 1965 = marker UMBas legacy
    IdPicking               BIGINT          NULL,
    IdPedido                BIGINT          NULL,      -- redundante con IdPedidoDet→trans_pe_det
    IdDespacho              BIGINT          NULL,
    user_agr                NVARCHAR(25)    NULL,      -- MaquinaQueSolicita (hostname)
    fec_agr                 DATETIME        NULL,
    user_mod                NVARCHAR(25)    NULL,
    fec_mod                 DATETIME        NULL,
    host                    NVARCHAR(50)    NULL,
    añada                   INT             NULL,
    fecha_manufactura       DATETIME        NULL,
    IdBodega                INT             NOT NULL,
    pallet_no_estandar      BIT             NULL,
    CONSTRAINT PK_stock_res PRIMARY KEY (IdStockRes)
);
```

### Índices recomendados

```sql
CREATE INDEX IX_stock_res_pedido_det
    ON dbo.stock_res (IdPedidoDet, estado)
    INCLUDE (IdStock, cantidad, lote, lic_plate);

CREATE INDEX IX_stock_res_stock
    ON dbo.stock_res (IdStock, estado)
    INCLUDE (cantidad);

CREATE INDEX IX_stock_res_pedido
    ON dbo.stock_res (IdPedido, estado)
    WHERE IdPedido IS NOT NULL;

CREATE INDEX IX_stock_res_estado_fec
    ON dbo.stock_res (estado, fec_agr)
    WHERE estado IN ('UNCOMMITED', 'COMMITED');
```

### CHECK constraint sugerido (no existe en producción)

```sql
-- Para evitar typos en estado (RIESGO R-07)
ALTER TABLE dbo.stock_res
ADD CONSTRAINT CK_stock_res_estado
CHECK (estado IS NULL OR estado IN ('UNCOMMITED','COMMITED','CONSUMED','CANCELLED','EXPIRED'));
```

> **Nota**: agregar este CHECK en producción requiere validar primero que no haya valores fuera del enum.

---

## 3. `trans_pe_enc` (70 cols) · encabezado de pedido

```sql
CREATE TABLE dbo.trans_pe_enc (
    IdPedidoEnc                              INT             NOT NULL,
    IdBodega                                 INT             NULL,
    IdCliente                                INT             NULL,
    IdMuelle                                 INT             NULL,
    IdPropietarioBodega                      INT             NULL,
    IdTipoPedido                             INT             NULL,
    IdPickingEnc                             INT             NULL,
    Fecha_Pedido                             DATETIME        NULL,
    hora_ini                                 DATETIME        NULL,
    hora_fin                                 DATETIME        NULL,
    ubicacion                                NVARCHAR(35)    NULL,
    estado                                   NVARCHAR(20)    NULL,
    no_despacho                              BIGINT          NULL,
    activo                                   BIT             NULL,
    user_agr                                 NVARCHAR(30)    NULL,
    fec_agr                                  DATETIME        NULL,
    user_mod                                 NVARCHAR(30)    NULL,
    fec_mod                                  DATETIME        NULL,
    no_documento                             BIGINT          NULL,
    local                                    BIT             NULL,
    pallet_primero                           BIT             NULL,
    dias_cliente                             FLOAT           NULL,
    anulado                                  BIT             NULL,
    -- Campos Road* (~30 cols de transporte/ruta)
    RoadKilometraje                          FLOAT           NULL,
    RoadFechaEntr                            DATETIME        NULL,
    RoadDirEntrega                           NVARCHAR(255)   NULL,
    RoadTotal                                FLOAT           NULL,
    RoadDesMonto                             FLOAT           NULL,
    RoadImpMonto                             FLOAT           NULL,
    RoadPeso                                 FLOAT           NULL,
    RoadBandera                              NVARCHAR(5)     NULL,
    RoadStatCom                              NVARCHAR(1)     NULL,
    RoadCalcoBJ                              NVARCHAR(1)     NULL,
    RoadImpres                               INT             NULL,
    RoadADD1                                 NVARCHAR(5)     NULL,
    RoadADD2                                 NVARCHAR(5)     NULL,
    RoadADD3                                 NVARCHAR(35)    NULL,
    RoadStatProc                             NVARCHAR(3)     NULL,
    RoadRechazado                            BIT             NULL,
    RoadRazon_Rechazado                      NVARCHAR(50)    NULL,
    RoadInformado                            BIT             NULL,
    RoadSucursal                             NVARCHAR(10)    NULL,
    RoadIdDespacho                           INT             NULL,
    RoadIdFacturacion                        INT             NULL,
    RoadIdRuta                               INT             NULL,
    RoadIdVendedor                           INT             NULL,
    RoadIdRutaDespacho                       INT             NULL,
    RoadIdVendedorDespacho                   INT             NULL,
    Observacion                              NVARCHAR(255)   NULL,
    PedidoRoad                               BIT             NULL,
    HoraEntregaDesde                         DATETIME        NULL,
    HoraEntregaHasta                         DATETIME        NULL,
    referencia                               NVARCHAR(25)    NULL,
    IdMotivoAnulacionBodega                  INT             NULL,
    Enviado_A_ERP                            BIT             NULL,
    control_ultimo_lote                      BIT             NULL,
    serie                                    NVARCHAR(25)    NULL,
    correlativo                              INT             NULL,
    Referencia_Documento_Ingreso_Bodega_Destino NVARCHAR(50) NULL,
    sync_mi3                                 BIT             NULL,
    No_Picking_ERP                           NVARCHAR(50)    NULL,
    no_documento_externo                     NVARCHAR(50)    NULL,
    requiere_tarimas                         BIT             NULL,
    fecha_preparacion                        DATE            NULL,
    IdTipoManufactura                        INT             NULL,
    bodega_origen                            NVARCHAR(50)    NULL,
    bodega_destino                           NVARCHAR(50)    NULL,
    IdMotivoDevolucion                       INT             NULL,
    Codigo_Empresa_ERP                       NVARCHAR(50)    NULL,
    EsExportacion                            BIT             NOT NULL,
    CONSTRAINT PK_trans_pe_enc PRIMARY KEY (IdPedidoEnc)
);
```

### Índices recomendados

```sql
CREATE INDEX IX_trans_pe_enc_estado
    ON dbo.trans_pe_enc (estado, fec_mod)
    INCLUDE (IdBodega, IdPropietarioBodega);

CREATE INDEX IX_trans_pe_enc_documento_ext
    ON dbo.trans_pe_enc (no_documento_externo)
    WHERE no_documento_externo IS NOT NULL;

CREATE INDEX IX_trans_pe_enc_propietario
    ON dbo.trans_pe_enc (IdPropietarioBodega, IdBodega, fec_agr DESC);
```

---

## 4. `trans_pe_det` (44 cols) · detalle de pedido

```sql
CREATE TABLE dbo.trans_pe_det (
    IdPedidoDet            INT             NOT NULL,
    IdPedidoEnc            INT             NOT NULL,
    IdProductoBodega       INT             NOT NULL,
    IdEstado               INT             NOT NULL,
    IdPresentacion         INT             NULL,
    IdUnidadMedidaBasica   INT             NULL,
    Cantidad               FLOAT           NULL,
    Peso                   FLOAT           NULL,
    Precio                 FLOAT           NULL,
    no_recepcion           BIGINT          NULL,
    ndias                  INT             NULL,
    cant_despachada        FLOAT           NULL,
    codigo_producto        NVARCHAR(50)    NULL,  -- cache
    nombre_producto        NVARCHAR(100)   NULL,  -- cache
    nom_presentacion       NVARCHAR(50)    NULL,  -- cache
    nom_unid_med           NVARCHAR(50)    NULL,  -- cache
    nom_estado             NVARCHAR(50)    NULL,  -- cache
    user_agr               NVARCHAR(30)    NULL,
    fec_agr                DATETIME        NULL,
    fecha_especifica       BIT             NULL,
    RoadDes                FLOAT           NULL,
    RoadDesMon             FLOAT           NULL,
    RoadTotal              FLOAT           NULL,
    RoadPrecioDoc          FLOAT           NULL,
    RoadVAL1               FLOAT           NULL,
    RoadVAL2               NVARCHAR(50)    NULL,
    RoadCantProc           FLOAT           NULL,
    peso_despachado        FLOAT           NULL,
    no_linea               INT             NULL,
    atributo_variante_1    NVARCHAR(25)    NULL,
    IdStockEspecifico      INT             NULL,
    EsPadre                BIT             NULL,
    IdPedidoDetPadre       INT             NULL,
    Peso_Bruto             FLOAT           NULL,
    Peso_Neto              FLOAT           NULL,
    Costo                  FLOAT           NULL,
    valor_aduana           FLOAT           NULL,
    valor_fob              FLOAT           NULL,
    valor_iva              FLOAT           NULL,
    valor_dai              FLOAT           NULL,
    valor_seguro           FLOAT           NULL,
    valor_flete            FLOAT           NULL,
    Total_linea            FLOAT           NULL,
    IdCliente              INT             NULL,
    CONSTRAINT PK_trans_pe_det PRIMARY KEY (IdPedidoDet),
    CONSTRAINT FK_trans_pe_det__trans_pe_enc
        FOREIGN KEY (IdPedidoEnc) REFERENCES dbo.trans_pe_enc(IdPedidoEnc)
);
```

### Índices recomendados

```sql
CREATE INDEX IX_trans_pe_det_pedido_enc ON dbo.trans_pe_det (IdPedidoEnc);
CREATE INDEX IX_trans_pe_det_producto ON dbo.trans_pe_det (IdProductoBodega);
CREATE INDEX IX_trans_pe_det_padre
    ON dbo.trans_pe_det (IdPedidoDetPadre)
    WHERE IdPedidoDetPadre IS NOT NULL;
```

---

## 5. `i_nav_config_enc` (69 cols) · config motor MI3

> Documentada en detalle en `entities/modules/reservation/04-mi3-config-propietario.md`. Aquí solo se replica el DDL para completitud.

```sql
CREATE TABLE dbo.i_nav_config_enc (
    idnavconfigenc                                    INT             NOT NULL,
    idempresa                                         INT             NOT NULL,
    idbodega                                          INT             NOT NULL,
    idPropietario                                     INT             NULL,
    idUsuario                                         INT             NULL,
    nombre                                            VARCHAR(256)    NULL,
    fec_agr                                           DATETIME        NULL,
    user_agr                                          NVARCHAR(50)    NULL,
    fec_mod                                           DATETIME        NULL,
    user_mod                                          NVARCHAR(50)    NULL,
    IdProductoEstado                                  INT             NULL,
    rechazar_pedido_incompleto                        INT             NULL,
    despachar_existencia_parcial                      INT             NULL,
    convertir_decimales_a_umbas                       INT             NULL,
    generar_pedido_ingreso_bodega_destino             BIT             NULL,
    generar_recepcion_auto_bodega_destino             BIT             NULL,
    codigo_proveedor_produccion                       NVARCHAR(50)    NULL,
    idFamilia                                         INT             NULL,
    idclasificacion                                   INT             NULL,
    idMarca                                           INT             NULL,
    idTipoProducto                                    INT             NULL,
    control_lote                                      BIT             NULL,
    control_vencimiento                               BIT             NULL,
    genera_lp                                         BIT             NULL,
    nombre_ejecutable                                 NVARCHAR(50)    NULL,
    IdTipoDocumentoTransferenciasIngreso              INT             NULL,
    crear_recepcion_de_transferencia_nav              BIT             NULL,
    IdTipoEtiqueta                                    INT             NULL,
    equiparar_cliente_con_propietario_en_doc_salida   BIT             NOT NULL,
    control_peso                                      BIT             NULL,
    crear_recepcion_de_compra_nav                     BIT             NULL,
    IdAcuerdoEnc                                      INT             NULL,
    push_ingreso_nav_desde_hh                         BIT             NULL,
    reservar_umbas_primero                            BIT             NOT NULL,
    implosion_automatica                              BIT             NOT NULL,
    explosion_automatica                              BIT             NOT NULL,
    Ejecutar_En_Despacho_Automaticamente              BIT             NOT NULL,
    IdTipoRotacion                                    INT             NULL,
    explosio_automatica_nivel_max                     INT             NULL,  -- TYPO histórico
    explosion_automatica_desde_ubicacion_picking      BIT             NULL,
    explosion_automatica_nivel_max                    INT             NULL,  -- corregida
    conservar_zona_picking_clavaud                    BIT             NULL,
    excluir_ubicaciones_reabasto                      BIT             NOT NULL,
    considerar_paletizado_en_reabasto                 BIT             NOT NULL,
    considerar_disponibilidad_ubicacion_reabasto      BIT             NOT NULL,
    dias_vida_defecto_perecederos                     INT             NOT NULL,
    codigo_bodega_nc_erp                              NVARCHAR(50)    NULL,
    lote_defecto_nc                                   NVARCHAR(50)    NULL,
    vence_defecto_nc                                  DATETIME        NULL,
    Codigo_Bodega_ERP_NC                              NVARCHAR(50)    NULL,
    Lote_Defecto_Entrada_NC                           NVARCHAR(50)    NULL,
    IdProductoEstado_NC                               INT             NULL,
    interface_sap                                     BIT             NOT NULL,
    sap_control_draft_ajustes                         BIT             NOT NULL,
    sap_control_draft_traslados                       BIT             NOT NULL,
    IdIndiceRotacion                                  INT             NULL,
    Rango_Dias_Importacion                            INT             NULL,
    inferir_bonificacion_pedido_sap                   BIT             NOT NULL,
    rechazar_bonificacion_incompleta                  BIT             NOT NULL,
    equiparar_productos                               BIT             NOT NULL,
    bodega_facturacion                                NVARCHAR(50)    NULL,
    valida_solo_codigo                                BIT             NOT NULL,
    excluir_recepcion_picking                         BIT             NOT NULL,
    bodega_prorrateo                                  NVARCHAR(50)    NULL,
    bodega_prorrateo1                                 NVARCHAR(50)    NULL,
    centro_costo_erp                                  INT             NULL,
    centro_costo_dir_erp                              INT             NULL,
    centro_costo_dep_erp                              INT             NULL,
    bodega_faltante                                   NVARCHAR(50)    NULL,
    CONSTRAINT PK_i_nav_config_enc PRIMARY KEY (idnavconfigenc)
);
```

### Índices recomendados

```sql
CREATE UNIQUE INDEX UX_i_nav_config_enc_clave
    ON dbo.i_nav_config_enc (idempresa, idbodega, idPropietario, idUsuario);

CREATE INDEX IX_i_nav_config_enc_bodega
    ON dbo.i_nav_config_enc (idbodega, idPropietario);
```

---

## 6. `i_nav_ped_traslado_det` (22 cols) · detalle traslado MI3

```sql
CREATE TABLE dbo.i_nav_ped_traslado_det (
    NoEnc                   NVARCHAR(50)    NOT NULL,
    No                      NVARCHAR(50)    NOT NULL,
    Description             NVARCHAR(100)   NULL,
    Item_No                 NVARCHAR(50)    NULL,
    Qty_to_Receive          FLOAT           NULL,
    Qty_to_Ship             FLOAT           NULL,
    Quantity                FLOAT           NULL,
    transfer_to_CodeField   NVARCHAR(50)    NULL,
    Shipment_Date           DATE            NULL,
    Unit_of_Measure_Code    NVARCHAR(50)    NULL,
    Line_No                 NVARCHAR(25)    NOT NULL,
    Variant_Code            NVARCHAR(25)    NULL,
    Status                  INT             NULL
    -- 9 cols restantes pendientes de inventariar (TODO archivo 12)
);
```

> **Pendiente**: documentar las 9 columnas faltantes (de las 22 reportadas por `INFORMATION_SCHEMA`).

---

## 7. `log_error_wms` (15 cols) · bitácora de errores

```sql
CREATE TABLE dbo.log_error_wms (
    IdError                 INT             NOT NULL,
    IdEmpresa               INT             NULL,
    IdBodega                INT             NULL,
    Fecha                   DATETIME        NULL,
    MensajeError            NVARCHAR(2500)  NULL,  -- truncar a 2400 desde código
    IdPedidoEnc             INT             NOT NULL,
    IdPickingEnc            INT             NOT NULL,
    IdRecepcionEnc          INT             NOT NULL,
    IdUsuarioAgr            INT             NOT NULL,
    Line_No                 INT             NULL,  -- abuso: motor MI3 lo usa como IdPedidoDet
    Item_No                 NVARCHAR(50)    NULL,
    UmBas                   NVARCHAR(50)    NULL,
    Variant_Code            NVARCHAR(50)    NULL,
    Cantidad                FLOAT           NULL,
    Referencia_Documento    NVARCHAR(50)    NULL,
    CONSTRAINT PK_log_error_wms PRIMARY KEY (IdError)
);
```

### Índices recomendados

```sql
CREATE INDEX IX_log_error_wms_pedido_fecha
    ON dbo.log_error_wms (IdPedidoEnc, Fecha)
    INCLUDE (MensajeError);

CREATE INDEX IX_log_error_wms_fecha_filtro
    ON dbo.log_error_wms (Fecha DESC)
    INCLUDE (MensajeError, IdPedidoEnc);
```

---

## 8. `propietarios` (23 cols) · datos básicos del propietario

```sql
CREATE TABLE dbo.propietarios (
    IdPropietario               INT             NOT NULL,
    IdEmpresa                   INT             NULL,
    IdTipoActualizacionCosto    INT             NULL,
    contacto                    NVARCHAR(100)   NOT NULL,
    nombre_comercial            NVARCHAR(100)   NOT NULL,
    imagen                      IMAGE           NULL,
    telefono                    NVARCHAR(50)    NULL,
    direccion                   NVARCHAR(50)    NULL,
    activo                      BIT             NULL,
    user_agr                    NVARCHAR(25)    NULL,
    fec_agr                     DATETIME        NULL,
    user_mod                    NVARCHAR(25)    NULL,
    fec_mod                     DATETIME        NULL,
    email                       NVARCHAR(100)   NULL,
    actualiza_costo_oc          BIT             NULL,
    color                       INT             NULL,
    codigo                      NVARCHAR(25)    NULL,
    sistema                     BIT             NULL,
    NIT                         NVARCHAR(50)    NULL,
    codigo_acceso               NVARCHAR(50)    NULL,
    clave_acceso                NVARCHAR(50)    NULL,
    es_consolidador             BIT             NULL,
    controlux                   BIT             NOT NULL,
    CONSTRAINT PK_propietarios PRIMARY KEY (IdPropietario)
);
```

> **Esta tabla NO contiene flags del motor MI3**. Esos viven en `i_nav_config_enc`.

---

## 9. `propietario_bodega` (8 cols) · binding propietario↔bodega

```sql
CREATE TABLE dbo.propietario_bodega (
    IdPropietarioBodega     INT             NOT NULL,
    IdPropietario           INT             NULL,
    IdBodega                INT             NULL,
    user_agr                NVARCHAR(30)    NULL,
    fec_agr                 DATETIME        NULL,
    user_mod                NVARCHAR(30)    NULL,
    fec_mod                 DATETIME        NULL,
    activo                  BIT             NULL,
    CONSTRAINT PK_propietario_bodega PRIMARY KEY (IdPropietarioBodega),
    CONSTRAINT FK_propietario_bodega__propietario
        FOREIGN KEY (IdPropietario) REFERENCES dbo.propietarios(IdPropietario)
);
```

> **Esta tabla solo es binding + audit + activo**. Ningún flag de comportamiento.

---

## 10. `trans_pe_det_log_reserva` (auxiliar) · bitácora granular de reservas

> Tabla mencionada en `09-mi3-logging-observabilidad.md` §2.3 como bitácora granular de la función `Inserta_Pista_Reserva` del legacy. Schema pendiente de validación live (**TODO**).

```sql
-- DDL aproximado (validar contra producción)
CREATE TABLE dbo.trans_pe_det_log_reserva (
    IdLogReserva            INT IDENTITY    NOT NULL,
    IdPedidoDet             INT             NOT NULL,
    IdStock                 INT             NOT NULL,
    Cantidad                FLOAT           NOT NULL,
    IdUbicacion             INT             NULL,
    lote                    NVARCHAR(50)    NULL,
    lic_plate               NVARCHAR(50)    NULL,
    UmBas                   NVARCHAR(50)    NULL,
    fec_agr                 DATETIME        NOT NULL DEFAULT GETDATE(),
    user_agr                NVARCHAR(50)    NULL,
    DisponibleUMBas         FLOAT           NULL,
    StockUMBas              FLOAT           NULL,
    ReservadoUmBas          FLOAT           NULL,
    NombreUmBas             NVARCHAR(50)    NULL,
    IdUmBasAbastercerCon    INT             NULL,
    -- Validar contra schema real
    CONSTRAINT PK_trans_pe_det_log_reserva PRIMARY KEY (IdLogReserva)
);
```

> **TODO**: ejecutar `INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='trans_pe_det_log_reserva'` y reemplazar este DDL aproximado con el real.

---

## 11. Inventario de FKs y índices recomendados

### 11.1 FKs detectadas en producción

```sql
-- Query para inventariar FKs (READ-ONLY)
SELECT
    fk.name AS NombreFK,
    OBJECT_NAME(fk.parent_object_id) AS Tabla,
    cp.name AS Columna,
    OBJECT_NAME(fk.referenced_object_id) AS TablaReferenciada,
    cr.name AS ColumnaReferenciada
FROM sys.foreign_keys fk
JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
JOIN sys.columns cp ON cp.object_id = fkc.parent_object_id AND cp.column_id = fkc.parent_column_id
JOIN sys.columns cr ON cr.object_id = fkc.referenced_object_id AND cr.column_id = fkc.referenced_column_id
WHERE OBJECT_NAME(fk.parent_object_id) IN
    ('stock','stock_res','trans_pe_enc','trans_pe_det','i_nav_config_enc',
     'i_nav_ped_traslado_det','log_error_wms','propietarios','propietario_bodega',
     'trans_pe_det_log_reserva')
ORDER BY Tabla, NombreFK;
```

> **TODO**: ejecutar y poblar este catálogo con los FK NAMEs reales de producción.

### 11.2 Índices que el motor MI3 NECESITA en producción

| Tabla              | Índice                                                                    | Razón |
|--------------------|---------------------------------------------------------------------------|-------|
| `stock`            | `(IdProductoBodega, IdBodega, activo) INCLUDE (cantidad, IdUbicacion, fecha_vence)` | Query de stock candidato |
| `stock`            | `(fecha_vence)` parcial WHERE IS NOT NULL                                | Ordenamiento FEFO |
| `stock_res`        | `(IdPedidoDet, estado) INCLUDE (IdStock, cantidad)`                      | Detección reserva duplicada |
| `stock_res`        | `(IdStock, estado)`                                                      | Cálculo disponibilidad |
| `stock_res`        | `(estado, fec_agr)` parcial WHERE estado IN ('UNCOMMITED','COMMITED')   | Job limpieza EXPIRED |
| `trans_pe_det`     | `(IdPedidoEnc)`                                                          | Carga de detalle |
| `i_nav_config_enc` | `UNIQUE (idempresa, idbodega, idPropietario, idUsuario)`                | Carga config motor |
| `log_error_wms`    | `(IdPedidoEnc, Fecha) INCLUDE (MensajeError)`                            | Reconstrucción de flujo |

### 11.3 CHECK constraints sugeridos

```sql
-- stock_res.estado: solo valores válidos
ALTER TABLE dbo.stock_res
ADD CONSTRAINT CK_stock_res_estado
CHECK (estado IS NULL OR estado IN ('UNCOMMITED','COMMITED','CONSUMED','CANCELLED','EXPIRED'));

-- stock.cantidad: no negativa
ALTER TABLE dbo.stock
ADD CONSTRAINT CK_stock_cantidad_positive
CHECK (cantidad >= 0);

-- stock_res.cantidad: positiva
ALTER TABLE dbo.stock_res
ADD CONSTRAINT CK_stock_res_cantidad_positive
CHECK (cantidad > 0);

-- trans_pe_enc.estado: solo valores conocidos (validar antes de aplicar)
ALTER TABLE dbo.trans_pe_enc
ADD CONSTRAINT CK_trans_pe_enc_estado
CHECK (estado IS NULL OR estado IN
    ('BORRADOR','APROBADO','ASIGNADO','EN_PICKING','DESPACHADO','CANCELADO','ANULADO'));
```

> **Importante**: aplicar estos CHECK constraints requiere validar primero que TODOS los registros existentes cumplen la condición. Killios productivo READ-ONLY desde wms-brain — coordinar con DBA.

---

> **Disclaimer final**: este catálogo es referencia documental. El esquema real es el que está vivo en `52.41.114.122,1437`/`TOMWMS_KILLIOS_PRD`. Cualquier divergencia debe resolverse a favor de la BD productiva.
