# MHS WMSWebAPI - contrato observado de ingresos

> Snapshot de código local: 2026-08-24. Fuente: checkout `TOMWMS_BOF`, rama observada `dev_2026_estable`. Confirmar contra la versión desplegada de MHS antes de diagnosticar. `#EJC20260824`

## Entrada HTTP

- Ruta: `POST /api/sync/ingresos/documento-ingreso`
- Controller: `WMSWebAPI/Controllers/SyncIngresosController.cs`
- Body: `List<OrdenCompra_3plDto>`; el JSON raíz esperado es un arreglo.
- Éxito observado: HTTP 200, `{ "Exito": true, "Mensaje": "Documento de ingreso procesado correctamente." }`.
- Error dentro del procesamiento: HTTP 500 con `Exito=false`, `Mensaje=ex.Message` y `Detalles` únicamente cuando `MostrarDetallesErrores` está habilitado.

### Ingreso MI3 utilizado por MHS

- Ruta: `POST /api/sync/ingresos/mi3/insert`.
- Body: `clsBeI_nav_ped_compra_enc` con `Lineas_Detalle` y `Lineas_Detalle_Lotes`.
- `Lineas_Detalle[].Quantity` representa cantidad en presentación cuando la línea tiene presentación.
- Por compatibilidad, `Lineas_Detalle_Lotes[].Quantity_Base` conserva su nombre, pero MHS envía allí la cantidad del lote en presentación.
- Al persistir, `trans_oc_det.cantidad` conserva la cantidad en presentación y `trans_oc_det_lote.cantidad` se convierte a unidad básica multiplicando cada lote por `producto_presentacion.factor`.
- La suma de lotes en presentación puede ser menor o igual que la cantidad de la línea; no se exige cobertura total. `#EJC20260824`

## Shape de primer nivel

`OrdenCompra_3plDto` contiene:

| Propiedad | Guard inicial observado | Destino principal |
|---|---|---|
| `Encabezado` | no null | `trans_oc_enc` |
| `Detalle` | no null | `trans_oc_det` y `producto_bodega` |
| `Polizas` | sin persistencia visible en este método | revisar si el payload la usa |
| `TipoIngreso` | sin persistencia visible en este método | revisar si el payload la usa |
| `Recepciones` | opcional; null produce listas vacías | `trans_re_enc`, `trans_re_det`, `trans_re_oc`, operadores |
| `stockRec` | no null | `stock_rec` |
| `stock` | sin guard explícito | `stock` y catálogos físicos anidados |
| `movimientos` | no null | `trans_movimientos` |
| `Proveedores` | no null | `proveedor` |
| `ProveedoresBodega` | no null | `proveedor_bodega` |

## Orden de persistencia observado

Dentro de la misma transacción SQL, el servicio llama en este orden:

1. proveedores;
2. proveedores por bodega;
3. operadores y operador por bodega;
4. producto por bodega;
5. encabezado y detalle de orden de compra;
6. encabezado/detalle de recepción 3PL y relaciones;
7. áreas, sectores, tramos y ubicaciones anidados en `stock`;
8. `stock_rec` y `stock` 3PL;
9. movimientos.

La primera excepción detiene el documento y el controller intenta rollback. Correlacionar el último log/SQL exitoso con la siguiente llamada, pero no confundir orden aparente con prueba causal.

## Hotspots para el incidente MHS

- Raíz JSON objeto en vez de arreglo.
- Propiedad omitida que deserializa como null, aunque conceptualmente sea una lista vacía.
- IDs externos enviados como IDs internos de TOMWMS.
- FK entre encabezado, líneas, recepciones, `stock_rec`, stock y movimientos.
- `No_Linea` inconsistente entre OC, recepción y lotes.
- maestros de producto/bodega/presentación/proveedor todavía no sincronizados.
- reintento del ERP después de timeout con inserciones no idempotentes.
- mensaje exterior truncado o reenvuelto que oculta tabla/SP/constraint causal.
- drift entre la rama local, la rama MHS y el binario desplegado.

## Archivos de código a abrir primero

- `WMSWebAPI/Controllers/SyncIngresosController.cs`
- `WMSWebAPI/Services/Ingresos/SyncIngresosService.cs`, método `ProcesarDocumentosIngreso_3pl`
- `WMS.EntityCore/Dtos/Ingresos/OrdenCompra_3plDto.cs`
- DTO hijos en `WMS.EntityCore/Dtos/Ingresos/`
- `WMSWebAPI/Mapping_Profile/MappingProfile.cs`
- implementaciones `InsertarOActualizar*` llamadas por el servicio
- `WMSWebAPI/README_WEBAPI_REQUEST_TRACE.md` y middleware de auditoría

## Checklist de comparación payload/contrato

- ¿El JSON raíz es arreglo?
- ¿Los nombres respetan la configuración real de serialización?
- ¿Las colecciones obligatorias están presentes, aunque sean `[]`?
- ¿Todas las líneas tienen identidad consistente y única?
- ¿Cantidades, fechas y decimales usan formatos aceptados por .NET?
- ¿Las referencias internas corresponden a maestros ya existentes/sincronizados?
- ¿Es un documento nuevo o un reintento? ¿Qué clave define idempotencia?
- ¿El error ocurre al deserializar, antes de abrir conexión o dentro de una llamada LN/DAL?
