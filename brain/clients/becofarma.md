---
client_id: becofarma
db_name: IMS4MB_BECOFARMA_PRD
erp: SAP B1 (DI-API)
rubro: Farmacia / distribucion farmaceutica
authored_by: agente-brain
authored_at: 2026-04-28T23:00:00-03:00
version: V1
status: BD-DIAGNOSTICA-NO-PRODUCTIVA (ratificado por Erik 28-abr-2026, ver L-014)
fuente_principal: SQL live READ-ONLY 28-abr-2026
---

# Cliente BECOFARMA

> Distribuidora farmaceutica integrada a SAP B1 via DI-API. Operacion **extremo outbound** (98% PIK), bodega unica, FEFO obligatorio con control de lote y vencimiento. **BD restaurada/migrada hoy 28-abr-2026 desde una instancia previa cuya configuracion data del 11-sep-2017** (no es cliente nuevo).

## ⚠ ATENCION: BD DIAGNOSTICA, NO PRODUCTIVA

Esta BD `IMS4MB_BECOFARMA_PRD` en EC2 `52.41.114.122,1437` es una **copia/snapshot
restaurada por Erik el 28-abr-2026 para entrenamiento y documentacion del agente**.

**NO es la productiva del cliente BECOFARMA**. La productiva real corre en otro
server donde `SAPBOSync.exe` procesa el i_nav_transacciones_out (outbox) normalmente (ver L-015).

Implicancias:
- El **85% pendiente del i_nav_transacciones_out** (H-028) es snapshot historico congelado, NO indica
  caida de la interface. La productiva real puede tener otro % completamente.
- El **44% en estado Pickeado** (H-029) tampoco se puede leer como disfuncion
  operativa — esta congelado en el momento del backup.
- TODOS los hechos sobre **schema, modulos, naming, catalogos** SI son validos y
  reflejan la productiva (afinidad-procesos confirmable).
- TODAS las inferencias de **salud operativa, volumen, frecuencia** NO se pueden
  inferir desde aqui (afinidad-datos diferida).

Cierre formal: ver `brain/learnings/L-014-becofarma-prd-es-bd-diagnostica-no-productiva.md`
y `brain/_processed/20260428-2345-H25-*.json`.

---

## Identificacion

- **Database (server compartido EC2 `52.41.114.122,1437`)**: `IMS4MB_BECOFARMA_PRD`
- **ERP**: SAP B1 (Business One) via **DI-API**
- **Rubro**: Distribucion farmaceutica (lote + vencimiento + licencias sanitarias)
- **Bodegas operativas**: 1 (GENERAL)
- **Empresa registrada**: BECOFARMA (`IdEmpresa=1`)

## Bodegas

| IdBodega | Codigo | Nombre | Activo |
|---|---|---|---|
| 1 | 01 | GENERAL | True |

> **Hallazgo H25**: BECOFARMA es la unica BD del set actual con **una sola bodega**. K7=6, BB=2, CEALSA=2.

## Senial fuerte: BD migrada/restaurada el 28-abr-2026

| Indicador | Valor | Inferencia |
|---|---|---|
| `sys.databases.create_date` | 2026-04-28 08:32 | BD creada hoy |
| `i_nav_config_enc.fec_agr` | 2017-09-11 | Config existe desde hace 9 anios |
| `i_nav_config_enc.fec_mod` | 2024-08-23 | Ultima edicion hace 8 meses |
| `nombre_ejecutable` | `SAPBOSync.exe` | Binario distinto al `SAPSYNC.vbproj` documentado |
| `i_nav_transacciones_out` (i_nav_transacciones_out) | 36,576 filas, 85% pendiente | Backlog masivo previo a la migracion |

**Conclusion**: la BD fue **restaurada hoy** desde una instancia productiva previa. Hay que confirmar con Erik si esto se hizo (a) como movimiento de produccion (cliente activo migrado de hosting), (b) como copia para diagnostico, o (c) como sandbox para algo nuevo.

## Configuracion aprendida (i_nav_config_enc)

Aprendida 2026-04-28 desde `IMS4MB_BECOFARMA_PRD`. Bodega de referencia: id=1 (codigo `01`).

| Flag | Valor | Significado / nota |
|---|---|---|
| `interface_sap` | `true` | **SI usa SAP B1 via DI-API** (a diferencia de BYB que tiene `false`) |
| `nombre_ejecutable` | `SAPBOSync.exe` | **Distinto del `SAPSYNC.vbproj`** documentado en `interfaces-erp-por-cliente.md` (corregir) |
| `Ejecutar_En_Despacho_Automaticamente` | `true` | El sync hacia SAP arranca automaticamente al cerrar despacho |
| `control_lote` | `true` | Trazabilidad por lote obligatoria |
| `control_vencimiento` | `true` | FEFO obligatorio |
| `IdTipoRotacion` | `3` | FEFO (1=FIFO, 2=LIFO, 3=FEFO) |
| `dias_vida_defecto_perecederos` | `365` | Defecto 1 anio (BB tiene 0) |
| `genera_lp` | `true` | Genera License Plate (pallet/tarima ID) en recepcion |
| `IdProductoEstado` | `1` | Estado por defecto al ingresar |
| `IdProductoEstado_NC` | `3` | Estado para producto en **no conformidad** (rechazado/dudoso) |
| `vence_defecto_nc` | `1900-01-01` | Defecto historico para vencimiento de NC |
| `codigo_bodega_erp_nc` | `15` | Codigo de bodega de **no conformidad** en SAP B1 |
| `equiparar_cliente_con_propietario_en_doc_salida` | `true` | 3PL: cliente del documento es propietario del producto |
| `generar_pedido_ingreso_bodega_destino` | `true` | Auto-genera pedido de ingreso al hacer traslado |
| `IdTipoEtiqueta` | `8` | **Hallazgo H27b**: apunta a tipo `8`, pero solo hay 3 tipos definidos (1, 2, 3). Posible inconsistencia o codigo legacy |
| `explosion_automatica_nivel_max` | `-1` | Sin limite de explosion recursiva |

(28 flags poblados visibles, los demas null/0)

## Modelo operativo: 98% PIK (extremo outbound)

Distribucion de tareas HH (Q-014 replicada en BECOFARMA):

| Codigo | Nombre | Cantidad | % |
|---|---|---:|---:|
| 8 | **PIK** | 7081 | **98.0%** |
| 1 | RECE | 144 | 2.0% |
| 6 | INVE | 2 | 0.0% |

Comparativa cross-cliente (extiende H07):

| BD | Top 1 | Pico | Patron |
|---|---|---:|---|
| K7 | PIK | 71% | Outbound-heavy |
| **BECOFARMA** | **PIK** | **98%** | **Extremo outbound-heavy** |
| C9-QAS | PIK | 74% | Outbound-heavy |
| BB | UBIC | 50% | Putaway-heavy |

> **Pregunta abierta**: ¿por que en BECOFARMA casi no se ven tareas de UBIC (ubicacion/putaway)? Hipotesis: las recepciones se ubican via flujo automatico/SAP B1 sin pasar por HH, o se omite la tarea HH en el flujo. A confirmar con operacion.

## Estados de pedido (extension de la maquina de estados)

Distribucion `trans_pe_enc.estado` en BECOFARMA:

| Estado | Cantidad | % |
|---|---:|---:|
| Despachado | 4411 | 51.4% |
| **Pickeado** | **3802** | **44.3%** |
| Pendiente | 282 | 3.3% |
| Anulado | 22 | 0.3% |
| NUEVO | 18 | 0.2% |
| Verificado | 4 | 0.0% |

**Hallazgo H29**: BECOFARMA acumula casi la mitad de los pedidos en estado **`Pickeado`** como estado terminal de hecho (no transitan a Despachado). Esto es un patron distinto a K7/BB/CEALSA donde la transicion `Pickeado → Despachado` es atomica. Posibles hipotesis:
- (a) Hay un workflow de **verificacion previa al despacho** en BECOFARMA que bloquea la transicion (compatible con la presencia de estado `Verificado` y modulo `trans_verificacion_etiqueta`).
- (b) El backlog del i_nav_transacciones_out SAP (85% pendiente) impide cerrar el despacho hasta que SAP confirma.
- (c) Bug operativo: el SP que mueve a Despachado se ejecuta con baja frecuencia.

## Modulos exclusivos / particularidades tecnicas

### Modulo: logging segmentado por proceso (H26, NUEVO)

BECOFARMA es **la unica BD del set** que tiene logs segmentados por proceso, ademas del log generico:

| Tabla | Filas | Cobertura |
|---|---:|---|
| `log_error_wms` | 52,418 | generico |
| `log_error_wms_pe` | 7,098 | pedidos |
| `log_error_wms_rec` | 6,941 | recepcion |
| `log_error_wms_reab` | 6,462 | reabastecimiento |
| `log_error_wms_ubic` | 6,426 | ubicacion |
| `log_error_wms_pick` | 5,762 | picking |

Las otras 3 BDs solo tienen `log_error_wms`. **Implicancia**: BECOFARMA tiene una version del WMS con logging mas granular (mejora) o quedo con tablas legacy de un piloto (dudoso, dado que estan pobladas con miles de filas).

### Modulo: verificacion de etiquetas (H27, NUEVO)

Tablas exclusivas:
- `trans_verificacion_etiqueta` — transacciones de verificacion (probablemente tras impresion de etiqueta).
- `log_verificacion_bof` — bitacora de verificaciones desde el BOF (front office).
- `verificacion_estado` — catalogo de estados de verificacion (2 estados definidos).
- `tipo_etiqueta` — catalogo de tipos de etiqueta con campos `dpi`, `codigo_zpl`, `es_inkjet`. Soporta ZPL (Zebra) **e** inkjet.
- `tipo_etiqueta_detalle` — campos a imprimir por tipo (orden, nombre, campo, coor_x, coor_y, width, height).
- `producto_clasificacion_etiqueta` — clasificacion de productos para asignacion de tipo de etiqueta.

**Implicancia**: BECOFARMA tiene un subsistema completo de **etiquetado y verificacion** que las demas BDs no tienen. Esto es coherente con regulacion farmaceutica (cada caja/lote requiere etiqueta verificable).

### Modulo: licencias farmaceuticas + licenciamiento de dispositivos (H27b)

Doble interpretacion de "licencias":

**Producto-licencia (sanitaria)**: tablas `Licencias` (5 filas) y `LIcencias2` (2 filas, **typo doble I que persistio**). Ambas con cols `LICENCIA, CODIGO PRODUCTO, LOTE, FECHA VENCIMIENTO, CANTIDAD, BODEGA`. Indica que cada lote farmaceutico esta atado a una licencia sanitaria (codigo nacional/regulatorio).

**Dispositivo-licencia (HH/PC)**: tablas `licencia_item`, `licencia_llave` (1 fila, la "llave maestra"), `licencia_login` (37 disp), `licencia_pendientes` (45), `licencia_solic` (1), `licencias_pendientes_retroactivo`, `temp_licencia_llave`. Cada dispositivo HH se identifica por `idDisp` y se valida contra una llave criptografica.

**Implicancia**: el sistema de licenciamiento esta sobreingenierizado o evolucionado. Hay tablas duplicadas (`Licencias`/`LIcencias2`) y temp tables que sugieren migracion en curso o codigo legacy no consolidado.

### Modulo: configuracion auto-routing por cliente y por proveedor (H15, NUEVO)

Tablas exclusivas:
- `cliente_config` (0 filas) — cols: `IdCliente, IdAreaDestino, IdUbicacionDestino, codigo_bodega_erp, IdProductoEstado`. Permite definir a que area/ubicacion van los productos de cada cliente automaticamente.
- `proveedor_config` (0 filas) — analogo para proveedores en recepcion.

**Implicancia**: BECOFARMA tiene capacidad construida para **auto-routing por entidad** que aun no esta poblada. Probablemente disenado para reducir la decision manual del operador en bodega.

### Modulo: reglas de vencimiento con notificacion (H24, NUEVO)

Tablas exclusivas (todas vacias):
- `regla_vencimiento` — define reglas: `IdBodega, IdProductoFamilia, IdProductoClasificacion, TiempoVencimientoDias, TipoNotificacion, IdPropietarioMercancia, IdProveedor, IdCliente`.
- `reglas_vencimiento_contacto` — contactos para notificar: `IdContacto, NombreContacto, CorreoElectronico, TelefonoFijo, TelefonoMovil, IdReglaVencimiento`.
- `cliente_lotes` (0 filas) — bloqueo/control de lotes por cliente: `IdCliente, IdProducto, Lote, IdProductoEstado, bloquear`.

**Implicancia**: capacidad de notificacion proactiva de vencimientos por correo/telefono. **Funcionalidad construida pero no activada**. Para una farmaceutica esto es valor alto (cumplimiento regulatorio).

### Modulo: gestion de jornadas laborales (H20)

SP exclusivo `asignar_jornada_laboral` (cursores anidados operador→bodega) que asigna jornadas a operadores. Sugiere modulo de **planificacion de turnos** con tabla `operador_jornada_laboral`. No esta en otras BDs.

### Modulo: SAP B1 con flujos de TRASLADO solamente (H16)

Estado del bridge SAP en BECOFARMA:

| Tabla | Filas | Lectura |
|---|---:|---|
| `i_nav_ped_traslado_enc` | 8,441 | Cabezales de transferencias SAP B1 → WMS |
| `i_nav_ped_traslado_det` | 56,224 | Lineas de transferencias |
| `i_nav_ped_traslado_det_lote` | 0 | Lote por linea (estructura existe, sin datos) |
| `i_nav_ped_compra_enc` | 0 | **No se usan pedidos de compra via DI-API** |
| `i_nav_ped_compra_det_lote` | 0 | Idem |
| `i_nav_ejecucion_enc` / `_res` | 10,120 / 10,120 | Resultados de ejecuciones (sync) |
| `i_nav_ejecucion_det_error` | 7,588 | Errores de sync (volumen alto, **75% de las ejecuciones tienen al menos 1 error**) |

**Implicancia**: la integracion SAP B1 de BECOFARMA opera por **traslados de inventario** (no por pedidos de compra). Las cabezas `i_nav_ped_traslado_enc` traen cols con naming nativo SAP (`No`, `Posting_Date`, `Receipt_Date`), no naming WMS — confirma que son **replicas directas de tablas OINV/OWTR de SAP B1**.

### Outbox saturado: 85% pendiente, 100% de filas con TODAS las FKs (H28, H30 — CRITICO)

`i_nav_transacciones_out`:

| Metrica | Valor |
|---|---:|
| Total filas | 36,576 |
| Tipo SALIDA | 31,486 (86%) |
| Tipo INGRESO | 5,090 (14%) |
| Enviados (`enviado=1`) | 5,313 (**14.5%**) |
| Pendientes (`enviado=0`) | 31,263 (**85.5%**) |
| con `idpedidoenc` | 36,576 (100%) |
| con `iddespachoenc` | 36,576 (100%) |
| con `idrecepcionenc` | 36,576 (100%) |
| con `idordencompra` | 36,576 (100%) |

**HALLAZGOS**:
- **H28**: 85% del i_nav_transacciones_out esta pendiente. SAPBOSync.exe puede estar caido / nunca arrancado tras la migracion / sin scheduler configurado en el server nuevo.
- **H30**: el 100% de las filas tienen TODAS las FKs (pedido + despacho + recepcion + OC) pobladas. Esto es **distinto** al patron observado en K7/BB donde con_pedido==con_despacho pero recepcion y OC son selectivas. Hipotesis: SAPBOSync.exe (o el SP que escribe en el i_nav_transacciones_out) usa una logica de "copia universal" que rellena todos los IDs con valores asociados aunque no apliquen al evento. Esto **invalida parcialmente H08** para el caso BECOFARMA: el i_nav_transacciones_out NO se simplifica a 2 tipos efectivos en BECOFARMA, porque cada fila trae los 4 IDs.
- **Naming inconsistencia (H15-naming)**: BECOFARMA usa **snake_case** SOLO en `i_nav_transacciones_out` (`idpedidoenc`, `iddespachoenc`, `idordencompra`). El resto de las tablas trans_* usan PascalCase (`IdPedidoEnc`, `IdDespachoEnc`, `IdOrdenCompraEnc`). Inconsistencia intra-BD.

### Catalogo de tareas: 30 tipos (vs 33-35 en otras) (H18)

`sis_tipo_tarea` BECOFARMA tiene 30 tipos. K7 tiene 35, BB tiene 35, CEALSA-QAS tiene 33. **Faltan en BECOFARMA**:
- `ANUL_PICK` (anulacion de picking)
- `AJLOTENI`, `AJLOTEPI` (ajustes de lote negativo/positivo iniciales)
- `AJVENCENI`, `AJVENCEPI` (ajustes de vencimiento negativo/positivo iniciales)

Implicancia: el catalogo de BECOFARMA esta **mas viejo** que las otras BDs. Coherente con la fec_agr=2017 de la config — el catalogo no se actualizo cuando se agregaron esos 5 tipos en versiones posteriores.

**H19**: en BECOFARMA existen **DOS codigos para picking**: `IdTipoTarea=8 (PIK)` y `IdTipoTarea=10 (PICK)`. PIK domina al 98%, PICK posiblemente sin uso. Sugiere migracion inacabada.

### SP MantenimientoBaseDeDatos (H20, NUEVO)

SP exclusivo de BECOFARMA. Codigo:

```sql
-- Reorganiza indices de TODAS las tablas
DECLARE TableCursor CURSOR FOR SELECT t.name FROM sys.tables ...
WHILE FETCH STATUS:
  ALTER INDEX ALL ON @TableName REORGANIZE;
-- Actualiza stats
EXEC sp_updatestats;
-- Compacta la BD (controvertido)
DBCC SHRINKDATABASE;
```

**Hallazgo**: `DBCC SHRINKDATABASE` es una practica controvertida — fragmenta indices y suele degradar performance. Probablemente heredada de la migracion 2017 sin revision actual.

## Implicancias para la WebAPI nueva (.NET 10)

### IMP-BECOFARMA-01: tener en cuenta operacion mono-bodega + extremo outbound

Para BECOFARMA el modelo de KPIs debe enfatizar **picking productivity** (98% del trabajo). Reportes de UBIC, RECE deberian estar deshabilitados o expuestos como "casi cero" para evitar ruido visual.

### IMP-BECOFARMA-02: validar el cumplimiento de H08 por cliente

H08 (i_nav_transacciones_out simplificable a 2 tipos efectivos) **NO se cumple** en BECOFARMA porque el 100% de las filas tienen todas las FKs. La WebAPI debe diferenciar el patron por cliente al consumir el i_nav_transacciones_out: K7/BB usan FKs selectivas, BECOFARMA usa FKs universales.

### IMP-BECOFARMA-03: backlog del i_nav_transacciones_out debe ser KPI publicado

Con 85% pendiente, BECOFARMA tiene un riesgo operativo serio si la WebAPI no expone "i_nav_transacciones_out health" (% pendiente > N dias) como KPI de monitoreo.

### IMP-BECOFARMA-04: corregir nombre del binario de sync en interfaces-erp-por-cliente.md

`SAPSYNC.vbproj` -> `SAPBOSync.exe` (o documentar ambos si conviven con roles distintos).

### IMP-BECOFARMA-05: la WebAPI debe mapear naming PascalCase ↔ snake_case por tabla

EF Core en BECOFARMA: para `i_nav_transacciones_out` usar `[Column("idpedidoenc")]`, para el resto PascalCase. Documentar como excepcion conocida.

### IMP-BECOFARMA-06: modulo de regla_vencimiento esta listo, falta activacion

Si la WebAPI .NET 10 quiere ofrecer notificacion proactiva de vencimientos, BECOFARMA es el cliente piloto natural — ya tiene las tablas, solo falta poblar reglas y conectar el job de notificacion.

## Cross-references

- `wms-specific-process-flow/becofarma-mapping.md` — mapeo profundo del schema, naming inconsistencies, top 30 tablas por volumen, comparativa cross-cliente.
- `wms-specific-process-flow/interfaces-erp-por-cliente.md` — corregir SAPSYNC.vbproj → SAPBOSync.exe.
- `clients/byb.md` — para contraste (BYB es alimentos NAV, BECOFARMA es farma SAP B1).
- `clients/README.md` — agregar BECOFARMA al catalogo.
- `_inbox/20260428-23**-H{25-30}-becofarma-*.json` — eventos para promover hallazgos a hipotesis ratificables.
