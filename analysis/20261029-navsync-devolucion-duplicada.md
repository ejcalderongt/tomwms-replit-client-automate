# NavSync: devolución NAV importada varias veces

Etiqueta: #EJC20261029_NAVSYNC_DEVOLUCION_UNICA

Rama: `codex/byb-unidad-medida`, checkout principal `C:/Users/carol/source/repos/TOMWMS_BOF`.

Caso reportado: ingreso RA-165483 / referencia PDV-136152, varias filas anuladas y dos NUEVA. No se modificaron datos existentes.

## Causa en código

`DynamicsNavInterface/Clases Interface Sync/Devolucion_Venta/clsSyncNavDevolucionVenta.vb`, método `Insertar_Devolucion_Venta_Desde_Tabla_Intermedia_A_Tabla_TOMIMS`: buscaba referencia con `navPedidoCompraEnc.Document_Type` (dominio NAV), pero insertaba con `clsDataContractDI.tTipoDocumentoIngreso.Devolucion_Venta` (12 WMS). La ruta de actualización también usaba una entidad nueva sin recuperar el ID del encabezado encontrado.

## Corrección

- Buscar referencia con tipo WMS 12, bodega y propietario configurados, sin excluir anulados/cerrados/inactivos.
- Bloqueo transaccional de aplicación antes del uso de tablas intermedias para serializar esta importación entre procesos. Consulta de existencia con UPDLOCK/HOLDLOCK.
- Más de una coincidencia: omitir documento y registrar incidencia, sin escoger un duplicado arbitrario.
- Una coincidencia anulada/cerrada/inactiva: omitir sin reactivar ni crear otra.
- Una coincidencia activa: conservar ID, estado y demás campos existentes al actualizar; cargar lotes mediante API pública de DAL. Conservar número existente si NAV no aporta factura.
- Sin coincidencias: conservar creación original.

## Validación

Compilación MSBuild de NavSync y dependencias AppGlobal, Entity y DAL: exit 0. Resultados aislados en `analysis/navsync-duplicate-build/`; registro `analysis/navsync-duplicate-build.log`. `git diff --check` correcto, VB UTF-8 BOM / CRLF conservados. Tres archivos de Pedido/Picking previamente modificados permanecen fuera de este cambio.

Catálogo consultado: snapshot local db-brain de trans_oc_enc y propietario_bodega; no se alteró esquema. No se pudo ejecutar prueba funcional SQL/NAV: conexión integrada local rechazada por proveedor de seguridad. Brain remoto no accesible en consulta; token de reindexación ausente.

## Prueba funcional pendiente en ambiente de pruebas

1. Primera importación de devolución sin coincidencia: un encabezado y sus detalles.
2. Repetición (y devolución repetida dentro del mismo lote): mismo ID, ningún nuevo encabezado ni nueva recepción NAV.
3. Única coincidencia ANULADA/CERRADA/inactiva: no insertar ni reactivar.
4. Referencia con varios registros históricos: omitir y registrar incidencia.
5. Dos instancias simultáneas: segunda ejecución rechazada antes de manipular intermedias; repetir tras terminar la primera sin duplicados.
6. Misma referencia en otra bodega/propietario/tipo: mantener separación de documentos.

Límite: protección aplicada a esta ruta de devoluciones de NavSync; no equivale a una restricción única de toda la base para escritores externos. No corrige ni elimina duplicados históricos. Sin commits, push ni despliegue.
