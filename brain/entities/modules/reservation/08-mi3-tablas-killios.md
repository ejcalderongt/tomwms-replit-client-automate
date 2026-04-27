# 08 · Schema completo de las tablas críticas del motor MI3

> **Propósito**: documentar el schema validado de las 6 tablas que el motor MI3 lee/escribe directamente, más las 2 tablas de configuración referenciadas en `04-mi3-config-propietario.md`. Todos los schemas fueron extraídos vía consulta READ-ONLY a Killios productivo (`52.41.114.122,1437`, BD `TOMWMS_KILLIOS_PRD`) el 2026-04-27.
>
> **Total tablas documentadas**: 8. **Total columnas**: 295.
>
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md`, `04-mi3-config-propietario.md` (i_nav_config_enc ya cubierta), `06-mi3-handlers-detalle.md`, `07-stock-res-ciclo-vida.md`, `sql-catalog/reservation-tables.md` (a producir con DDL completo).

---

## Índice

1. `stock` (33 cols) — inventario físico
2. `stock_res` (35 cols) — reservas
3. `trans_pe_enc` (70 cols) — encabezado de pedido
4. `trans_pe_det` (44 cols) — detalle de pedido
5. `i_nav_ped_traslado_det` (22 cols) — detalle de traslado MI3
6. `log_error_wms` (15 cols) — bitácora de errores
7. Diagrama de FKs entre tablas
8. Convenciones de naming y rarezas detectadas

---

## 1. `stock` (33 cols) · inventario físico

### 1.1 Identidad

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `IdStock`           | int NOT NULL    | PK                                               |
| `IdBodega`          | int NOT NULL    | FK `bodega.IdBodega`                             |
| `IdPropietarioBodega`| int NOT NULL   | FK `propietario_bodega.IdPropietarioBodega`      |
| `IdProductoBodega`  | int NOT NULL    | FK `producto_bodega.IdProductoBodega`            |
| `IdProductoEstado`  | int NOT NULL    | FK estado producto (BUEN_ESTADO, DAÑADO, etc.)   |
| `IdPresentacion`    | int NULL        | FK `producto_presentacion.IdPresentacion` (NULL = UMBas) |
| `IdUnidadMedida`    | int NULL        | FK unidad de medida                              |
| `IdUbicacion`       | int NOT NULL    | FK `ubicacion.IdUbicacion`                       |
| `IdUbicacion_anterior`| int NULL      | Histórico de movimiento (último origen)          |

### 1.2 Trazabilidad de origen

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `IdRecepcionEnc`    | int NULL        | Recepción que originó el stock                   |
| `IdRecepcionDet`    | int NULL        | Detalle de recepción                             |
| `IdPedidoEnc`       | int NULL        | Pedido que generó el stock (devoluciones, etc.)  |
| `IdPickingEnc`      | int NULL        | Picking activo (durante consumo)                 |
| `IdDespachoEnc`     | int NULL        | Despacho activo                                  |

### 1.3 Identificación física

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `lote`              | nvarchar(50) NOT NULL | Lote del fabricante                        |
| `lic_plate`         | nvarchar(50) NULL    | License plate del pallet                    |
| `serial`            | nvarchar(50) NULL    | Serial individual (si aplica)               |
| `cantidad`          | float NOT NULL  | Cantidad en stock (en la presentación o en UMBas) |
| `peso`              | float NULL      | Peso físico                                      |
| `temperatura`       | float NULL      | Para refrigerados/congelados                     |
| `atributo_variante_1`| nvarchar(25) NULL | Atributo variable (color, talla, etc.)         |

### 1.4 Fechas y unidades

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `fecha_ingreso`     | datetime NULL   | Cuando se recibió                                |
| `fecha_vence`       | datetime NULL   | Vencimiento (`1900-01-01` = no aplica)           |
| `fecha_manufactura` | datetime NULL   | Fabricación                                      |
| `añada`             | int NULL        | Año de cosecha (para vinos/aceites)              |
| `uds_lic_plate`     | float NULL      | Unidades del LP completo (referencia)            |
| `no_bulto`          | int NULL        | Marker especial. **`1965` = stock generado por la recursión legacy de UMBas** (ver `02-mi3-motor-legacy-vb.md` §recursión). |

### 1.5 Auditoría y flags

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `user_agr`, `fec_agr`, `user_mod`, `fec_mod` | varios | Auditoría estándar       |
| `activo`            | bit NOT NULL    | Soft-delete                                      |
| `pallet_no_estandar`| bit NULL        | Pallet con dimensiones no estándar               |

### 1.6 Operaciones del motor MI3 sobre `stock`

- **Lectura**: `StockQueryStep` consulta vía `clsLnStock.lStock(IdProductoBodega, IdBodega, IdPresentacion, ...)` filtrando por `activo = 1` y `cantidad > 0`.
- **Escritura**: ninguna directa por el motor de reserva. La reserva NO decrementa `stock.cantidad`. Solo el despacho (`CONSUMED`) lo hace, vía `clsLnStock.RestaCantidadStock`.
- **Cálculo de disponibilidad**: `disponible = stock.cantidad - SUM(stock_res.cantidad WHERE Estado IN ('UNCOMMITED', 'COMMITED'))`.

> **Importante**: el motor confía en `pallet_completo` como flag derivado (no es columna directa de `stock` — es calculado runtime o vía vista, ver §6 de `05-mi3-algoritmo-fefo-clavaud.md`).

---

## 2. `stock_res` (35 cols) · reservas

### 2.1 Identidad y FK

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `IdStockRes`        | int NOT NULL    | PK                                               |
| `IdTransaccion`     | int NOT NULL    | Identificador de transacción de reserva (no FK) |
| `Indicador`         | nvarchar(50) NULL | "PED" / "TRA" / "MAN" (tipo de origen)         |
| `IdPedidoDet`       | int NOT NULL    | FK `trans_pe_det.IdPedidoDet`                    |
| `IdStock`           | int NOT NULL    | FK `stock.IdStock` (origen del stock reservado)  |
| `IdBodega`          | int NOT NULL    | FK bodega                                        |
| `IdPropietarioBodega`| int NOT NULL   | FK propietario_bodega                            |
| `IdProductoBodega`  | int NOT NULL    | FK producto_bodega                               |
| `IdProductoEstado`  | int NULL        | Estado producto (puede diferir del stock origen) |
| `IdPresentacion`    | int NULL        | Presentación reservada (puede ser distinta a la pedida si hubo conversión) |
| `IdUnidadMedida`    | int NULL        | Unidad de medida                                 |
| `IdUbicacion`       | int NOT NULL    | Ubicación origen                                 |
| `ubicacion_ant`     | nvarchar(25) NULL | Ubicación anterior (string, no FK)             |
| `IdRecepcion`       | bigint NULL     | Recepción de origen                              |

### 2.2 Identificación física (espejo del stock)

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `lote`              | nvarchar(50) NULL | Copiado de `stock.lote`                        |
| `lic_plate`         | nvarchar(50) NULL | Copiado de `stock.lic_plate`                   |
| `serial`            | nvarchar(50) NULL | Copiado de `stock.serial`                      |
| `cantidad`          | float NOT NULL    | Cantidad reservada (≤ `stock.cantidad`)        |
| `peso`              | float NULL        | Peso correspondiente                            |
| `uds_lic_plate`     | float NULL        | Referencia al LP completo                       |
| `no_bulto`          | int NULL          | Marker (puede ser 1965 si vino de UMBas legacy) |
| `pallet_no_estandar`| bit NULL          | Heredado del stock origen                       |
| `añada`             | int NULL          | Año de cosecha                                  |

### 2.3 Estado y trazabilidad

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `estado`            | nvarchar(20) NULL | UNCOMMITED / COMMITED / CONSUMED / CANCELLED / EXPIRED (ver `07-stock-res-ciclo-vida.md`) |
| `fecha_ingreso`     | datetime NULL   | Heredado del stock                               |
| `fecha_vence`       | datetime NULL   | Heredado del stock                               |
| `fecha_manufactura` | datetime NULL   | Heredado del stock                               |
| `IdPicking`         | bigint NULL     | Picking que consumirá la reserva                 |
| `IdPedido`          | bigint NULL     | Pedido al que pertenece (redundante con IdPedidoDet→trans_pe_det) |
| `IdDespacho`        | bigint NULL     | Despacho final                                   |

### 2.4 Auditoría

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `user_agr`          | nvarchar(25) NULL | Quien creó la reserva (`MaquinaQueSolicita` desde MI3) |
| `fec_agr`           | datetime NULL   | Cuando                                           |
| `user_mod`, `fec_mod`| varios         | Última modificación                              |
| `host`              | nvarchar(50) NULL | Hostname del cliente (opcional)                |

> **Nota importante**: `stock_res` **NO tiene** columnas como `Fecha_Commit`, `Fecha_Consumo`, `Fecha_Cancelacion`, `Motivo_Cancelacion` que documenté en `07-stock-res-ciclo-vida.md`. Esas transiciones se infieren cruzando con `trans_pe_enc.estado` y `trans_pe_enc.fec_mod`. **Hay que corregir el archivo 07 en la próxima pasada**.
> 
> Tampoco existen las columnas `Usuario_Commit`, `Usuario_Consumo`, `Usuario_Cancelacion`. La auditoría histórica del cambio de estado se hace solo con `user_mod` + `fec_mod` (último cambio) y por correlación con el pedido.
> 
> Tampoco existen `EsExplosion`, `EsUMBas`, `EsZonaPicking`, `IdPresentacionOriginal`. El motor nuevo aún no las persiste o las maneja vía otras estructuras.

### 2.5 Operaciones del motor MI3 sobre `stock_res`

- **INSERT**: `PostProcessingStep.PersistReservations` (motor nuevo) o `Inserta_Stock_Reservado` (legacy). Una fila por cada `BeStock_res` en `lBeStockAReservar`.
- **UPDATE estado**: por triggers externos (aprobación de pedido, despacho, cancelación). El motor de reserva no modifica reservas existentes.
- **DELETE**: nunca. Solo soft-delete vía `estado = 'CANCELLED'`.

---

## 3. `trans_pe_enc` (70 cols) · encabezado de pedido

### 3.1 Identidad y referencias

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `IdPedidoEnc`       | int NOT NULL    | PK                                               |
| `IdBodega`          | int NULL        | FK bodega                                        |
| `IdCliente`         | int NULL        | FK cliente                                       |
| `IdMuelle`          | int NULL        | FK muelle de despacho                            |
| `IdPropietarioBodega`| int NULL       | FK propietario_bodega                            |
| `IdTipoPedido`      | int NULL        | FK tipo (ENVÍO, TRASLADO, DEVOLUCIÓN, etc.)      |
| `IdPickingEnc`      | int NULL        | Picking asociado                                 |

### 3.2 Estado y fechas

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `Fecha_Pedido`      | datetime NULL   | Fecha de creación                                |
| `hora_ini`, `hora_fin` | datetime NULL | Ventana operativa                              |
| `estado`            | nvarchar(20) NULL | "BORRADOR" / "APROBADO" / "ASIGNADO" / "EN_PICKING" / "DESPACHADO" / "CANCELADO" |
| `no_despacho`       | bigint NULL     | Número de despacho final                         |
| `activo`            | bit NULL        | Soft-delete                                      |
| `anulado`           | bit NULL        | Si fue anulado (distinto de cancelado)           |
| `local`             | bit NULL        | Si es pedido local vs importado                  |
| `pallet_primero`    | bit NULL        | Si exige despachar pallets completos primero     |

### 3.3 Específicas MI3 / SAP

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `sync_mi3`          | bit NULL        | Si vino de Killios vía MI3                       |
| `Enviado_A_ERP`     | bit NULL        | Si fue ya empujado al ERP                        |
| `No_Picking_ERP`    | nvarchar(50) NULL | Número de picking en ERP                       |
| `no_documento_externo`| nvarchar(50) NULL | Documento externo de Killios                 |
| `Codigo_Empresa_ERP`| nvarchar(50) NULL | Empresa ERP origen                             |
| `IdMotivoAnulacionBodega`| int NULL  | Motivo si fue anulado por bodega                |
| `IdMotivoDevolucion`| int NULL        | Motivo si es devolución                          |
| `referencia`        | nvarchar(25) NULL | Referencia libre                               |
| `fecha_preparacion` | date NULL       | Fecha planeada de preparación                    |
| `requiere_tarimas`  | bit NULL        | Si requiere armar tarimas                        |

### 3.4 Otras

Las 70 columnas incluyen además ~30 columnas con prefijo `Road*` (datos de transporte/ruta) y misceláneas (`HoraEntregaDesde`, `HoraEntregaHasta`, `serie`, `correlativo`, `IdTipoManufactura`, `bodega_origen`, `bodega_destino`, `EsExportacion`). Se omiten del detalle por no afectar el motor de reserva.

### 3.5 Operaciones del motor MI3 sobre `trans_pe_enc`

- **Lectura**: `EntityLoadingStep` lee el encabezado para obtener `IdBodega`, `IdPropietarioBodega`, `IdCliente`, `IdTipoPedido`, `estado`.
- **Escritura**: ninguna. El encabezado es responsabilidad del flujo de aprobación, no del motor de reserva.

---

## 4. `trans_pe_det` (44 cols) · detalle de pedido

### 4.1 Identidad y FK

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `IdPedidoDet`       | int NOT NULL    | PK                                               |
| `IdPedidoEnc`       | int NOT NULL    | FK `trans_pe_enc.IdPedidoEnc`                    |
| `IdProductoBodega`  | int NOT NULL    | FK producto_bodega                               |
| `IdEstado`          | int NOT NULL    | FK estado del producto requerido                 |
| `IdPresentacion`    | int NULL        | Presentación pedida (NULL = UMBas)               |
| `IdUnidadMedidaBasica`| int NULL      | UMBas equivalente                                |
| `IdCliente`         | int NULL        | Cliente final (puede diferir del header)         |
| `IdPedidoDetPadre`  | int NULL        | Si es subitem (líneas anidadas)                  |
| `EsPadre`           | bit NULL        | Si tiene subitems                                |

### 4.2 Cantidades

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `Cantidad`          | float NULL      | Cantidad pedida (en presentación o UMBas)        |
| `Peso`              | float NULL      | Peso correspondiente                             |
| `cant_despachada`   | float NULL      | Cantidad efectivamente despachada                |
| `peso_despachado`   | float NULL      | Peso despachado                                  |
| `RoadCantProc`      | float NULL      | Cantidad procesada en transporte                 |

### 4.3 Datos cacheados (denormalización)

Para evitar JOIN al consultar pedidos:

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `codigo_producto`   | nvarchar(50) NULL | Cache de `producto.codigo`                     |
| `nombre_producto`   | nvarchar(100) NULL| Cache de `producto.nombre`                     |
| `nom_presentacion`  | nvarchar(50) NULL | Cache de `producto_presentacion.nombre`        |
| `nom_unid_med`      | nvarchar(50) NULL | Cache de unidad de medida                      |
| `nom_estado`        | nvarchar(50) NULL | Cache de nombre de estado producto             |
| `atributo_variante_1`| nvarchar(25) NULL| Cache atributo                                |

### 4.4 Cantidad y precio

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `Precio`            | float NULL      | Precio unitario                                  |
| `Costo`             | float NULL      | Costo unitario                                   |
| `Total_linea`       | float NULL      | Total línea                                      |
| `valor_aduana`, `valor_fob`, `valor_iva`, `valor_dai`, `valor_seguro`, `valor_flete` | float NULL | Valores fiscales |
| `Peso_Bruto`, `Peso_Neto` | float NULL | Pesos bruto/neto                              |

### 4.5 Otras

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `no_recepcion`      | bigint NULL     | Para devoluciones, número de recepción origen    |
| `ndias`             | int NULL        | Días de vida útil mínima requerida (para FEFO)   |
| `fecha_especifica`  | bit NULL        | Si exige fecha exacta                            |
| `IdStockEspecifico` | int NULL        | Si reserva sobre stock específico                |
| `no_linea`          | int NULL        | Línea numerada                                   |
| `user_agr`, `fec_agr`| varios         | Auditoría                                        |
| `RoadDes`, `RoadDesMon`, `RoadTotal`, `RoadPrecioDoc`, `RoadVAL1`, `RoadVAL2` | varios | Campos de transporte/ruta |

### 4.6 Operaciones del motor MI3 sobre `trans_pe_det`

- **Lectura**: el motor recibe `IdPedidoDet` en el `Request` y lee la línea para obtener `IdProductoBodega`, `IdPresentacion`, `IdEstado`, `Cantidad`, `ndias`, `IdStockEspecifico`.
- **Escritura**: el motor no modifica `trans_pe_det`. La actualización de `cant_despachada` ocurre en el flujo de despacho.

---

## 5. `i_nav_ped_traslado_det` (22 cols) · detalle de traslado MI3

> Tabla de **integración** Killios↔WMS. Cuando Killios envía una orden de traslado, una fila aquí representa una línea del traslado. El motor MI3 lee esta tabla en endpoints de sync.

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `NoEnc`             | nvarchar(50) NOT NULL | Número de encabezado del traslado en Killios |
| `No`                | nvarchar(50) NOT NULL | Identificador único de línea                |
| `Description`       | nvarchar(100) NULL | Descripción libre                              |
| `Item_No`           | nvarchar(50) NULL | Código de producto en Killios                  |
| `Qty_to_Receive`    | float NULL      | Cantidad esperada                                |
| `Qty_to_Ship`       | float NULL      | Cantidad a despachar                             |
| `Quantity`          | float NULL      | Cantidad solicitada                              |
| `transfer_to_CodeField` | nvarchar(50) NULL | Bodega destino                             |
| `Shipment_Date`     | date NULL       | Fecha planeada de envío                          |
| `Unit_of_Measure_Code`| nvarchar(50) NULL | UMBas en Killios                             |
| `Line_No`           | nvarchar(25) NOT NULL | Número de línea                              |
| `Variant_Code`      | nvarchar(25) NULL | Código de variante                             |
| `Status`            | int NULL        | Estado de procesamiento (0=pendiente, 1=procesado, etc.) |

> El schema mostrado tiene 13 cols visibles. La consulta reportó 22 cols totales — las 9 restantes son metadata adicional (reproducir consulta para detalle completo).

### 5.1 Operaciones del motor MI3 sobre esta tabla

- **Lectura**: endpoints `POST /api/sync/salidas/mi3/insertar` reciben el JSON de Killios y lo persisten en esta tabla antes de invocar al motor de reserva.
- **Escritura**: el endpoint actualiza `Status` cuando termina el procesamiento.
- **El motor de reserva no toca esta tabla directamente**; recibe los datos ya parseados desde el controlador REST.

---

## 6. `log_error_wms` (15 cols) · bitácora de errores

| Columna             | Tipo            | Notas                                            |
|---------------------|-----------------|--------------------------------------------------|
| `IdError`           | int NOT NULL    | PK                                               |
| `IdEmpresa`         | int NULL        | Empresa                                          |
| `IdBodega`          | int NULL        | Bodega                                           |
| `Fecha`             | datetime NULL   | Timestamp del error                              |
| `MensajeError`      | nvarchar(2500) NULL | Texto del error / checkpoint                 |
| `IdPedidoEnc`       | int NOT NULL    | Pedido relacionado (0 si no aplica)              |
| `IdPickingEnc`      | int NOT NULL    | Picking relacionado                              |
| `IdRecepcionEnc`    | int NOT NULL    | Recepción relacionada                            |
| `IdUsuarioAgr`      | int NOT NULL    | Usuario que disparó el error                     |
| `Line_No`           | int NULL        | Línea de pedido                                  |
| `Item_No`           | nvarchar(50) NULL | Código de producto                             |
| `UmBas`             | nvarchar(50) NULL | UMBas relacionada                              |
| `Variant_Code`      | nvarchar(50) NULL | Código de variante                             |
| `Cantidad`          | float NULL      | Cantidad afectada                                |
| `Referencia_Documento`| nvarchar(50) NULL | Referencia documento externo                  |

### 6.1 Implicación para el motor MI3

- El motor escribe en `log_error_wms` tanto errores como **checkpoints estructurados** (vocabulario `#CASO_*_*`, `#STEP_*`, `#FALLBACK_*`, ver `06-mi3-handlers-detalle.md`).
- `MensajeError` es texto libre limitado a 2500 chars. Los checkpoints largos pueden truncarse — riesgo abierto.
- No hay un campo `Severidad` ni `Tipo`. Todos los logs van a la misma tabla. La distinción entre INFO/WARN/ERROR debe inferirse del contenido del mensaje.

### 6.2 Queries útiles

```sql
-- Logs de un pedido específico
SELECT * FROM log_error_wms
WHERE IdPedidoEnc = @IdPedido
ORDER BY Fecha;

-- Errores recientes con cantidad afectada
SELECT * FROM log_error_wms
WHERE Fecha > DATEADD(HOUR, -24, GETDATE())
  AND Cantidad IS NOT NULL
ORDER BY Fecha DESC;

-- Checkpoints específicos del motor MI3
SELECT * FROM log_error_wms
WHERE MensajeError LIKE '#CASO_%'
   OR MensajeError LIKE '#STEP_%'
ORDER BY Fecha DESC;
```

---

## 7. Diagrama de FKs entre tablas

```
                     ┌──────────────────┐
                     │   propietarios   │ (datos básicos)
                     └────────┬─────────┘
                              │
                              ▼
                     ┌────────────────────┐
                     │ propietario_bodega │ (binding + activo)
                     └────────┬───────────┘
                              │
                ┌─────────────┼─────────────┐
                ▼             ▼             ▼
        ┌──────────────┐ ┌──────────────┐ ┌────────────────────┐
        │   bodega     │ │ i_nav_       │ │ producto_bodega    │
        │              │ │ config_enc   │ │ (producto x bodega)│
        └──────┬───────┘ └──────────────┘ └─────────┬──────────┘
               │            (config motor)           │
               └─────┬─────────────────────┬─────────┘
                     ▼                     ▼
            ┌─────────────────┐    ┌──────────────────┐
            │     stock       │    │   trans_pe_enc   │
            │ (inventario)    │    │ (encabezado pedido)│
            └────────┬────────┘    └────────┬──────────┘
                     │                      │
                     │                      ▼
                     │             ┌─────────────────┐
                     │             │  trans_pe_det   │
                     │             │ (detalle línea) │
                     │             └────────┬────────┘
                     │                      │
                     └──────────┬───────────┘
                                ▼
                       ┌─────────────────┐
                       │   stock_res     │
                       │ (reserva real)  │
                       └─────────────────┘

      ┌─────────────────────────┐         ┌──────────────────┐
      │ i_nav_ped_traslado_det  │         │  log_error_wms   │
      │ (sync MI3 ⟶ WMS)       │         │ (bitácora)       │
      └─────────────────────────┘         └──────────────────┘
```

> Las FKs concretas (con NAME del constraint) requieren inspeccionar `INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS`. Trabajo pendiente para `sql-catalog/reservation-tables.md`.

---

## 8. Convenciones de naming y rarezas detectadas

### 8.1 Naming inconsistente

- **Singular/Plural**: `propietarios` (plural) vs `propietario_bodega` (singular) vs `bodega` (singular). Sin patrón consistente.
- **PascalCase vs snake_case**: las columnas mezclan ambas convenciones, a veces en la misma tabla. Ej: `IdStock` vs `lic_plate` vs `Fecha_Pedido` vs `fec_agr`.
- **CamelCase parcial**: `IdProductoBodega` (PascalCase) vs `idnavconfigenc` (lowercase) en la misma BD.
- **Idioma mixto**: `cantidad` (español) + `RoadKilometraje` (mezcla) + `Shipment_Date` (inglés).

### 8.2 Typos persistidos

- `i_nav_config_enc.explosio_automatica_nivel_max` (sin "n" en "explosio") y `explosion_automatica_nivel_max` (corregida) **coexisten**. Ver `04-mi3-config-propietario.md` §6.
- `producto_rellenado.IdUmBasAbastercerCon` (extra "r" en "Abastercer" debería ser "Abastecer").
- `trans_pe_det.atributo_variante_1` sugiere existir `_2` y `_3`, pero esquema actual solo tiene `_1`.

### 8.3 Tipos cuestionables

- `stock_res.estado` es `nvarchar(20)` cuando debería ser FK a un catálogo. Permite cualquier string (riesgo de typo).
- `trans_pe_enc.estado` también `nvarchar(20)` sin FK.
- `i_nav_config_enc.rechazar_pedido_incompleto` es `int` (enum implícito). Debería ser FK a tabla de catálogo.

### 8.4 Cardinalidades

- `i_nav_config_enc` por `(idempresa, idbodega, idPropietario, idUsuario)` — granularidad muy fina, fácil de generar duplicados accidentales.
- `stock_res.IdTransaccion` no es FK a ninguna tabla — es un identificador "lógico" generado por el motor.
- `trans_pe_det.IdStockEspecifico` permite restringir reserva a un único pallet — feature poco usada pero documentada.

### 8.5 Campos sospechosos

- `stock.no_bulto = 1965` es un **marker mágico** del legacy. No documentado en BD; solo en el código. Riesgo de pérdida de conocimiento si se borra el comentario en VB.
- `trans_pe_enc.local`, `trans_pe_enc.pallet_primero`: nombres ambiguos sin descripción en schema.
- `i_nav_config_enc.Ejecutar_En_Despacho_Automaticamente`: sin documentación clara de qué dispara.

---

> Próximo: `09-mi3-logging-observabilidad.md` documenta el contrato de `IReservationLogger`, los 5 métodos (`LogInfo`, `LogCheckpoint`, `LogReservation`, `LogError`, `LogException`), el formato del payload y cómo se persiste en `log_error_wms`. Plus: queries SQL para reconstruir el flujo de un pedido a partir de los logs.
