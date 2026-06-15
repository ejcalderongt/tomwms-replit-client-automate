# Traza 003 - Interface MAMPA (`SAPSYNCMAMPA`)

> Rama analizada: `dev_2028_merge`
> Proyecto: `C:\Users\carol\source\repos\TOMWMS_BOF\SAPSYNCMAMPA`
> Fecha de esta traza: 2026-06-12
> Estado: traza fina creada a partir de codigo real y huella previa de MAMPA
> Companion yml: `brain/code-deep-flow/traza-003-sapsyncmampa-interface.yml`

## 0. Resumen ejecutivo

`SAPSYNCMAMPA` es la interface dedicada de MAMPA para sincronizar WMS con SAP B1 sobre `SapServiceLayerClient`. No es una pieza aislada: el arranque va por `ModuleMain`, entra a `frmMenu`, y desde ahi `frmEjecucion` expone los flujos de importacion/exportacion.

Para trabajar mas rapido, esta traza se acompana con la skill `brain/skills/wms-mampa-interface/SKILL.md` y con el barrido `brain/skills/wms-mampa-interface/scripts/wms-mampa-scan.ps1`.

La traza fina muestra cuatro capas de verdad:

1. `ModuleMain` resuelve configuracion local desde `Conn.ini` y arma la instancia.
2. `frmMenu` carga `BeConfigEnc` y presenta la bodega/empresa activa.
3. `frmEjecucion` orquesta menus y dispara sincronizadores concretos.
4. `SapServiceLayerClient` hace el login a SAP HANA SL y materializa `B1SESSION` / `ROUTEID`.

## 1. Entrada del proceso

- `Clases\ModuleMain.vb: Main()` y `Init_App()` resuelven el arranque, leen `Conn.ini`, cargan `BD.Instancia` y fijan `IdConfiguracion`.
- `Formas\Principal\frmMenu.vb: frmMenu_Load()` consulta `clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)` y deja visible la empresa/bodega activa.
- `Formas\frmEjecucion.vb: frmEjecucion_Shown()` decide que acciones mostrar segun `Interface_A_Ejecutar` y resuelve `BeConfigEnc` / `IdUsuario`.

## 2. Mapa de llamadas de la UI

La pantalla principal no contiene la logica de negocio; solo enruta:

- `mnuBodegas` -> `clsSyncSAPBodega.Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMWMS`
- `mnuActualizarProveedores` -> `clsSyncSAPProveedor.Insertar_Proveedores_Desde_TablaIntermedia_A_Tabla_TOMWMS`
- `mnuClientes` -> `clsSyncSAPCliente.Insertar_Clientes_Desde_TablaIntermedia_A_Tabla_TOMWMSAsync`
- `mnuTallas` -> `clsSyncSapTalla.Get_Tallas_SAP_SL`
- `mnuColores` -> `clsSyncSapColor.Get_Colores_SAP_SL`
- `mnuProductos` -> `clsSyncSAPProducto.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS`
- `mnuCentroCosto` -> `clsSyncSapCentrosCosto.Importar_Centros_Costo_Desde_SAP`
- `mnuActualizarCodigosBarra` -> `clsSyncSapCodigosBarra.Importar_Codigos_Barra_Productos_SL`
- `mnuTraslados`, `mnuFacturas`, `mnuAjustes`, `mnuDevoluciones` y `mnuTransacWMS` -> los sincronizadores SAP/WMS específicos debajo

Referencia en `frmEjecucion`:

- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:41`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:70`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:237`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:266`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:536`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:580`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:657`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:1100`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:1280`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:1402`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:1431`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:1499`
- `SAPSYNCMAMPA/Formas/frmEjecucion.vb:1534`

## 3. Flujos SAP -> WMS

### 3.1 Facturas de reserva de cliente

- Punto de entrada: `clsSyncSapFacturaReservaCliente.Procesar_Facturas_de_Reserva_Cliente_SAP`
- SAP SL: `Invoices?$filter=ReserveInvoice eq 'tYES' and U_ENVIADO_WMS eq 2`
- Filtro adicional: `U_Guia ne null and U_Guia ne ''`
- Mapeo a WMS: `clsBeI_nav_ped_traslado_enc` + `clsBeI_nav_ped_traslado_det`
- Marcado de procesado: `PATCH` a `U_ENVIADO_WMS`

### 3.2 Facturas de deudor

- Punto de entrada: `clsSyncSapFacturaDeudor.Procesar_Facturas_de_Deudor_SAP`
- SAP SL: `Invoices?$filter=ReserveInvoice eq 'tNO' and U_ENVIADO_WMS eq 2`
- Requisito funcional: debe existir `U_Guia`
- Exclusion de items sin `ItemCode` o grupo `19`
- Marcado de procesado: `PATCH` a `U_ENVIADO_WMS`

### 3.3 Traslados

- Punto de entrada: `clsSyncSapTrasladosEnvio.Procesar_Solicitud_Traslado_SAP`
- Casos derivados: prorrateo, traslado a bodega virtual, ingreso desde solicitudes de tiendas y salida desde solicitudes de tienda/prorrateo
- Control de negocio: validacion de bodega origen/destino, cliente, ubicacion virtual y coherencia con `BeConfigEnc`
- Marcado de procesado: `Actualizar_Bandera_Enviado` sobre `transacciones_out`

### 3.4 Ajustes, devoluciones y pedido de cliente

- `clsSyncTransacWMS.Procesar_Ajustes_SAP`
- `clsSyncTransacWMS.Procesar_Devoluciones_de_Cliente_SAP`
- `clsSyncTransacWMS.Procesar_Pedido_de_Cliente_SAP`
- `clsSyncTransacWMS.Procesar_Anulacion_Devolucion_SAP`
- `clsSyncTransacWMS.Procesar_Anulacion_Ventas_SAP`

Este flujo comparte una misma logica de trazabilidad: leer documentos con `U_Procesado_WMS` pendiente, construir contexto, registrar errores con `clsLnI_nav_ejecucion_det_error.Inserta_Log` y marcar el documento en SAP cuando corresponde.

Regla operativa nueva para MAMPA: no importar articulos de servicio que inicien con `SV`.
La preferencia es filtrar lo mas arriba posible en el `Service Layer` con `$filter` usando `startswith(...)` cuando el campo lo permita, porque reduce payload, tiempo de mapeo y ruido de trazas.
Si por compatibilidad del endpoint o del campo el filtro no es aceptado, se deja una segunda barrera local con LINQ o `Where(...)` despues de deserializar, pero esa debe ser la via de respaldo, no la principal.

Regla de tags inline para mejoras MAMPA: usar `CKFKYYMMDDFeature`, por ejemplo `CKFK260612SVFILTER`, sin el token "fecha" y sin prefijos extra.

Regla operativa fina: cuando el cambio sea de MAMPA, tocar primero `clsSyncTransacWMS`; no empujar la validacion al ciclico salvo que el alcance lo pida de forma expresa.

Para diagnostico fino del caso actual, `clsSyncTransacWMS` ya queda instrumentado con debug antes de serializar `payloadObj.ToString(Formatting.None)` y antes de cada `PATCH`. Eso permite distinguir rapido entre:

- fallo de runtime / ensamblado de `Newtonsoft.Json`
- fallo de construccion de payload
- fallo de red o respuesta no exitosa del Service Layer
- fallo de dato de negocio, como producto o talla-color faltante

Ademas, `MapearAAjustes` ya se considera un punto sensible de rendimiento y UX: conviene cachear lookups por lote, precalcular IDs de secuencia por transaccion y actualizar la UI con un wrapper seguro que respete el hilo de Windows Forms.

## 4. Flujos WMS -> SAP

### 4.1 Importaciones maestras

- `Bodegas`, `Proveedores`, `Clientes`, `Productos`, `Tallas`, `Colores`, `Centros de costo`, `Codigos de barra`
- Estas rutas actualizan tablas maestras del WMS desde SAP HANA SL.

### 4.2 Ajustes WMS a SAP

- Punto de entrada: `clsSyncSapAjustes.Enviar_Ajustes_WMS_SAP`
- Usa la configuracion de interface activa y la bodega asociada

### 4.3 Facturas de reserva y deudor hacia SAP

- `clsSyncSapFacturaReservaCliente.Enviar_Factura_Reserva_ClienteAsync`
- `clsSyncSapFacturaDeudor.Enviar_Factura_Deudor_ClienteAsync`

Ambos flujos terminan posteando documentos en SAP Service Layer y actualizando banderas locales y remotas.

## 5. Contrato SAP Service Layer

`SapServiceLayerClient` es el punto de acceso real a SAP:

- `LoginAsync()` construye `SessionCookie` a partir de `B1SESSION` y `ROUTEID`.
- `GetPurchaseOrderAsync()` lee `PurchaseInvoices(<docEntry>)`.
- `Procesar_Detalle_Ingreso_HANA2()` y los flujos de factura usan `PurchaseDeliveryNotes` para crear entregas.
- El cliente desactiva validacion de certificado y usa `HttpClient` con `UseCookies = False` para controlar manualmente la sesion.

Puntos de referencia:

- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb:61`
- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb:119`
- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb:149`
- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb:196`
- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb:428`
- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb:763`
- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb:817`

## 6. Piezas de estado y persistencia

La interface escribe/lee sobre estas piezas contractuales:

- `i_nav_config_enc` para configuracion por bodega
- `i_nav_ejecucion_enc` y `i_nav_ejecucion_det_error` para bitacora de ejecuciones y errores
- `transacciones_out` para cola/estado de sincronizacion
- `U_ENVIADO_WMS`, `U_Procesado_WMS`, `U_DOCUMENTO_WMS`, `U_OPERADOR_WMS` como UDFs de control

## 7. Rasgos MAMPA visibles en esta interface

- `Conn.ini` del proyecto referencia `TOMWMS_MAMPA_QA`
- El binario apunta a `SAPBOSyncMampa.exe`
- MAMPA trabaja con una configuracion mas bodega-céntrica que otros clientes
- Los flujos de facturas y traslados usan `PurchaseDeliveryNotes`, `Invoices` y banderas UDF para coordinar WMS con SAP
- La huella de MAMPA ya estaba parcialmente documentada en `brain/fingerprint/MAMPA.md` y `brain/code-deep-flow/DIFF-BOF-2023-VS-2028.md`; esta traza aterriza el camino fino de la interface

## 8. Conclusion operativa

No existia una traza fina dedicada de `SAPSYNCMAMPA`; habia material parcial y transversal. Esta traza deja el mapa util para navegar la interface sin volver a empezar cada vez:

`ModuleMain -> frmMenu -> frmEjecucion -> sincronizador concreto -> SAP Service Layer / SQL local`

## 9. Q abiertas

- Q-MAMPA-ERP-REAL: confirmar si toda la interface realmente descansa en SAP B1 Service Layer para este cliente o si hay algun desvio local.
- Q-MAMPA-SYNC-PRIORIDAD: confirmar cual de los flujos es el de mayor criticidad operativa hoy: traslados, facturas o `TRANSAC_WMS`.
- Q-MAMPA-TRAZA-DERIVADA: crear trazas hijas separadas para `traslados`, `facturas-reserva` y `transac_wms` si se necesita mas detalle.

## 10. Automatizacion ligera

Para abrir una modificacion nueva sin perder tiempo:

```powershell
powershell -ExecutionPolicy Bypass -File brain/skills/wms-mampa-interface/scripts/wms-mampa-scan.ps1 -RepoRoot C:\Users\carol\source\repos\TOMWMS_BOF
```

El script devuelve las lineas de:

- `Procesar_Ajustes_SAP`
- `AjusteYaExisteEnWMS`
- `MapearAAjustes`
- `Procesar_Documentos_Ajustes`
- `RegistrarTrazaTransacWms`
- `RegistrarFalloTransacWmsAsync`

Con eso el siguiente cambio entra directo al punto exacto.
