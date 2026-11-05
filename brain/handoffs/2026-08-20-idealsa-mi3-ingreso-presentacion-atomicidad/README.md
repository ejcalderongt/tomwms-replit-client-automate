# IDEALSA MI3: presentación, tareas y atomicidad de importación — 2026-08-20

## Alcance y fuente de verdad

- Cliente: IDEALSA.
- Código fuente: `TOMWMS_BOF`.
- Rama validada y publicada: `dev_2026_estable`.
- Commit fuente: `031a1b96` (`Fix MI3 ingreso presentation and atomic import`).
- Servidor operativo confirmado por Erik: `192.168.125.30`.
- Base: `IMS4MB_IDEALSA_PRD`.
- Este handoff documenta el diagnóstico y los guardrails; el código compilable sigue siendo la fuente de verdad.

## Caso reproducido

- Referencia ERP: `7809960`.
- Bodega destino: `EN`; `IdBodega=16`.
- SKU/código de presentación recibido: `39002`.
- Presentación maestra: `UNIDAD`, factor `1`, `IdPresentacion=4956`.
- Síntoma inicial: la OC se generaba con `trans_oc_det.IdPresentacion=0` y nombre de presentación vacío.
- Síntoma de tarea: recepción creada con `IdTareaHH=0` y sin operadores, aun con asignación automática configurada.
- Síntoma posterior: `PedidoCliente.Insert` dejaba `i_nav_ped_traslado_enc/det` y devolvía `Object reference` sin crear `trans_pe_enc`.

## Causas raíz

### Presentación de ingreso

`ValidarYCalcularUMBas` podía resolver una unidad de medida válida y omitir la resolución explícita de `Variant_Code`. En el camino SAP decimal también podía reemplazarse la presentación explícita por la presentación por defecto.

Guardrail: si `Variant_Code` viene informado, resolverlo siempre contra `producto_presentacion` y propagar `IdPresentacion`, objeto `Presentacion`, `Nombre_presentacion` y variante tanto en insert como en update.

### Recepción y operadores

El overload de guardado recibía listas opcionales como `Nothing`. Se normalizan a listas vacías antes de invocar los persistidores y las excepciones ya no se absorben: deben propagarse para evitar éxito parcial.

### Intermedia de pedido de cliente

`Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS` puede devolver `Nothing`, por ejemplo cuando detecta un pedido existente. El wrapper confirmaba la transacción local aunque no existiera `trans_pe_enc`; después `PedidoCliente.Insert` ejecutaba `BePedidoEnc.IdPedidoEnc` sin null guard.

Guardrails aplicados:

1. Si el importador no genera un `clsBeTrans_pe_enc`, lanzar excepción antes del `Commit`; el `Catch` local ejecuta rollback de `i_nav_ped_traslado_enc/det`.
2. En `PedidoCliente.Insert`, validar `Nothing`/`IdPedidoEnc <= 0` antes de consultar las líneas.
3. Conservar el mensaje funcional del importador en vez de ocultarlo con `NullReferenceException`.

## Telemetría local

Los endpoints escriben trazas fail-open en:

```text
<MI3>\App_Data\MI3Trace\PedidoCompra-YYYYMMDD.log
<MI3>\App_Data\MI3Trace\PedidoCliente-YYYYMMDD.log
```

Eventos útiles de pedido cliente:

- `REQUEST_RECEIVED`
- `IMPORT_WITHOUT_WMS_ORDER`
- `WMS_ORDER_PERSISTED`
- `FAULT`
- `ERROR`

La cuenta del application pool de IIS necesita permiso de modificación sobre `App_Data\MI3Trace`. Un fallo de escritura de telemetría no debe cambiar el resultado funcional.

## Archivos fuente afectados

- `MI3/Transacciones/PedidoCompra.svc.vb`
- `MI3/Transacciones/PedidoCliente.svc.vb`
- `TOMIMSV4/DAL/Interface/Pedido_Compra/Pedido_Compra_Enc/clsLnI_nav_ped_compra_enc.vb`
- `TOMIMSV4/DAL/Interface/Pedido_Compra/Pedido_Compra_Enc/clsLnI_nav_ped_compra_enc_Partial.vb`
- `TOMIMSV4/DAL/Interface/Pedido_Traslado/Pedido_Traslado_Enc/clsLnI_nav_ped_traslado_enc_Partial.vb`
- `TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Encabezado/clsLnTrans_re_enc_Partial.vb`

## Validación

- `TOMIMSV4/DAL/DAL.vbproj`: build Debug exitoso.
- `MI3/MI3.vbproj`: build Debug exitoso.
- VB conservado como UTF-8 con BOM.
- Incidente confirmado resuelto funcionalmente por Erik el 2026-08-20.

## Regresión mínima

1. Enviar ingreso con `Variant_Code` explícito y validar `trans_oc_det.IdPresentacion` y `Nombre_presentacion`.
2. Validar creación de recepción, tarea HH y asignación a operadores configurados.
3. Reenviar un documento existente por `PedidoCliente.Insert`: debe responder error descriptivo y no dejar nuevas filas intermedias parciales.
4. Validar los archivos de traza usando la referencia y `TRACE_ID`.
