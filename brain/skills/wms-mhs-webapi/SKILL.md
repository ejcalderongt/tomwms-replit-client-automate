---
name: wms-mhs-webapi
description: Diagnosticar y documentar la interfaz REST de MHS con TOMWMS mediante WMSWebAPI, especialmente documentos de ingreso, contratos 3PL, mapeo DTO, transacciones SQL y respuestas HTTP. No usar para HH/WSHHRN ni para interfaces MI3, NAV o SAP dedicadas.
---

# WMS MHS WebAPI

## Alcance

MHS (codename `MS`) es el primer cliente que integra su ERP directamente con `WMSWebAPI`. Para incidentes de documentos de ingreso, trabajar sobre el flujo REST de `WMSWebAPI`; no confundirlo con recepción HH, WSHHRN, MI3 ni con el outbox ERP saliente.

## Flujo de diagnóstico

1. Ejecutar el preflight de `tomwms-local-operator` y preservar los cambios locales existentes.
2. Confirmar ambiente, URL/ruta, método HTTP, fecha/hora, identificador o referencia del documento y respuesta completa del API. Sanitizar tokens, credenciales y cadenas de conexión.
3. Leer [references/ingresos.md](references/ingresos.md) y contrastar el payload real con los DTO de la rama activa. El código local observado puede diferir de `dev_2028_merge`; registrar rama y commit antes de concluir.
4. Seguir la ruta `Controller -> Service -> AutoMapper -> clases LN/DAL -> tablas`. El endpoint agrupa varias escrituras en una transacción; identificar la primera operación causal que falla, no solo el mensaje exterior reenvuelto.
5. Clasificar el incidente: deserialización/shape, colección obligatoria ausente, mapeo, llave/FK/identidad, duplicado o idempotencia, dato maestro inexistente, regla de negocio, conexión/configuración o contrato de respuesta.
6. Correlacionar auditoría HTTP y logs por timestamp/documento. Usar SQL solo en modo lectura y consultar `wms-db-brain` antes de inferir esquema.
7. Antes de proponer un fix, armar una reproducción mínima sanitizada y verificar atomicidad, reintento e idempotencia. No reenviar el documento a un ambiente vivo sin autorización explícita.

## Evidencia mínima para ver un incidente

- Ambiente y rama/versión desplegada.
- Endpoint y método.
- Payload JSON exacto sanitizado.
- Status HTTP, body y headers útiles de respuesta.
- Timestamp con zona horaria y referencia/no. de documento.
- Log correlacionado del `WMSWebAPI` y, si hubo escritura parcial aparente, evidencia SQL read-only.

## Reglas no obvias

- `POST /api/sync/ingresos/documento-ingreso` recibe una **lista** de documentos, aunque se envíe uno.
- El contrato observado usa `OrdenCompra_3plDto`, no un DTO pequeño de orden de compra.
- En el servicio observado son obligatorias, al menos como colecciones no nulas: `Detalle`, `stockRec`, `movimientos`, `Proveedores` y `ProveedoresBodega`; `Encabezado` también es obligatorio.
- Una colección vacía y una propiedad ausente/null no son equivalentes: varias colecciones vacías pasan el guard inicial, mientras `null` falla.
- El controller observado devuelve `500` también para errores de contrato/datos lanzados dentro del servicio. No asumir que todo `500` es infraestructura.
- El mensaje se reenvuelve como `Error al procesar las órdenes de compra -> ...`; buscar la excepción interna y la primera operación LN/DAL que la produjo.
- Hay `TransactionScope` y además `SqlTransaction`; validar el comportamiento real del rollback antes de afirmar que existe persistencia parcial.
- Los comentarios históricos que mencionan Cealsa/3PL no prueban que una regla aplique a MHS. Separar código reutilizado de reglas específicas del cliente.

## Cierre

Entregar: etapa causal, evidencia, documento afectado, alcance de datos, hipótesis descartadas, reproducción, propuesta mínima, validación y riesgo residual. Etiquetar aprendizaje durable con `#EJCYYYYMMDD`.
