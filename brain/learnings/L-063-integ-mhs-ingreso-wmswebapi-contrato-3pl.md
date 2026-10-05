---
protocolVersion: 1
id: L-063
title: El ingreso MHS entra como un agregado 3PL transaccional, no como una orden simple
operator: codex-carolina
operatorRole: developer
createdAt: 2026-08-24T00:00:00-06:00
target:
  codename: MS
  environment: DEV
relatedQuestions: []
relatedDocs:
  - brain/skills/wms-mhs-webapi/references/ingresos.md
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
status: open
priority: high
tags: [MHS, WMSWebAPI, ingresos, 3PL, contrato, transaccion]
---

## Que aprendimos

El endpoint observado para recibir documentos de ingreso desde el ERP hacia TOMWMS es `POST /api/sync/ingresos/documento-ingreso`. Recibe un arreglo de `OrdenCompra_3plDto` y el servicio intenta persistir un agregado amplio: OC, recepción, operadores, catálogos físicos, stock y movimientos. `#EJC20260824`

El guard actual exige `Encabezado` y varias colecciones no nulas (`Detalle`, `stockRec`, `movimientos`, `Proveedores`, `ProveedoresBodega`). Por ello, un payload que omite una colección puede terminar como HTTP 500 aun cuando el problema sea contractual o de datos.

Para el flujo MHS que materializa `trans_oc_det_lote`, la entrada real es `POST /api/sync/ingresos/mi3/insert`. Se confirmó que `Quantity_Base` se mantiene por compatibilidad, aunque contiene cantidad en presentación. La línea queda en presentación y cada lote debe persistirse en unidad básica (`cantidad lote × factor`). La suma de lotes puede ser parcial, pero no puede superar la cantidad del detalle. `#EJC20260824`

## Evidencia

- `WMSWebAPI/Controllers/SyncIngresosController.cs`: ruta, body `List<OrdenCompra_3plDto>`, transacción y respuesta.
- `WMSWebAPI/Services/Ingresos/SyncIngresosService.cs`: `ProcesarDocumentosIngreso_3pl`, guards y orden de llamadas `InsertarOActualizar*`.
- `WMS.EntityCore/Dtos/Ingresos/OrdenCompra_3plDto.cs`: agregado de primer nivel.
- `WMS.DALCore/I_nav_ped_compra_enc/clsLnI_nav_ped_compra_enc.cs`: `ProcesarLotes` convierte cada cantidad por el factor y valida el acumulado de la línea.
- Evidencia observada en checkout local `dev_2026_estable` el 2026-08-24; falta confirmar versión desplegada en MHS.

## Implicancias

### Para el codigo

- Diferenciar validación de contrato/datos (4xx) de fallos internos (5xx) mejoraría el diagnóstico del ERP.
- La idempotencia y las FK deben evaluarse sobre el agregado completo, no solo sobre `trans_oc_enc`.
- Cualquier fix debe preservar el rollback del conjunto y probar reintentos tras timeout.

### Para la operacion

- Para diagnosticar se necesita payload sanitizado, status/body, timestamp con zona y referencia del documento.
- Debe correlacionarse la auditoría HTTP con la primera llamada LN/DAL causal.

### Para el equipo

- Confirmar rama/commit desplegado en MHS antes de comparar contra el checkout local.
- Mantener un ejemplo contractual válido y uno mínimo de reproducción sin datos sensibles.

## Acciones propuestas

- [ ] Obtener el caso real MHS: payload sanitizado, respuesta, timestamp, endpoint y referencia.
- [ ] Confirmar ambiente y commit/binario desplegado.
- [ ] Identificar la primera excepción interna y la tabla/constraint/mapeo causal.
- [ ] Definir y probar la política de idempotencia para reintentos del ERP.
- [ ] Evaluar respuestas 400/409 con errores estructurados para fallos esperables.
- [x] Corregir en `dev_2028_merge` la conversión de lotes a UM básica sin renombrar `Quantity_Base` (2026-08-24; pendiente commit).

## Validacion API MHS DEV — 2026-08-24

Se ejecutaron tres llamadas reales contra una instancia local de `WMSWebAPI` conectada a `TOMWMS_MHS_DEV`, usando el producto `I40840`, presentación `9770`, factor `100` y UM básica `43`:

| Caso | Referencia | Detalle presentación | Lotes enviados | HTTP | Resultado persistido |
|---|---|---:|---:|---:|---|
| Parcial | `_CDX-LQ-0824152722-P` | 3 | 1 | 200 | 1 lote × 100 UM básicas |
| Completo | `_CDX-LQ-0824152722-C` | 3 | 1+1+1 | 200 | 3 lotes × 100 = 300 UM básicas |
| Excedido | `_CDX-LQ-0824152722-X` | 3 | 2+2 | 500 | Sin `trans_oc_enc` ni `trans_oc_det_lote` materializados |

El caso rechazado sí permanece en `i_nav_ped_compra_enc`, porque la tabla intermedia se persiste antes de iniciar la transacción de materialización MI3. El código validado quedó en el commit `06fee107b` de `dev_2028_merge`. `#EJC20260824`

## Como se cierra esta learning

Cuando el incidente real esté diagnosticado y el contrato de ingreso validado con una reproducción, consolidar el resultado en la referencia del skill y registrar el caso de regresión correspondiente.
