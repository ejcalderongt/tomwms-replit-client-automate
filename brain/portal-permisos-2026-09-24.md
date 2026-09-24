# Permisos del portal TOMWMSUX

Guía de operación y mantenimiento: [portal-operacion-permisos.md](portal-operacion-permisos.md).

## Fuentes y despliegue

- React: `TOMWMSReact`, rama `dev_replit`; Azure DevOps `TOMWMSReact` es la fuente principal y GitHub `ejcalderongt/tomwmsreact` es espejo.
- API y capas: `TOMWMS_BOF`, rama `dev_2028_merge`, remoto Azure DevOps.
- Scripts SQL: GitHub `ejcalderongt/DBA`, un archivo por tabla en la raíz. Aplicar `portal_usuario.sql`, `portal_propietario_modulo.sql` y `portal_usuario_permiso.sql` en ese orden.
- El script de módulos habilita inicialmente todos los módulos para propietarios activos existentes. Los propietarios nuevos necesitan sus filas de habilitación al darse de alta.

## Modelo mínimo

`portal_usuario` representa a la persona delegada y pertenece a un propietario WMS. La cuenta legacy del propietario es administradora inicial y conserva su autenticación actual. `portal_propietario_modulo` define qué módulos habilita la empresa para cada propietario. `portal_usuario_permiso` asigna permisos específicos a personas delegadas. No hay tabla de roles: `EsAdministrador` cubre el único perfil administrativo requerido por ahora.

La API mantiene el catálogo de módulos y permisos en `PortalCatalogo`. Los códigos de lectura corresponden a las secciones existentes: inventario, ingresos, salidas, indicadores, análisis, Kairos y automatizaciones. Los códigos de crear/importar/enviar quedan preparados para los futuros documentos de ingreso y salida. Un permiso de usuario solo es efectivo si el módulo del propietario está habilitado.

## Flujo

`POST /api/PortalAcceso/login` autentica propietario o usuario delegado y devuelve JWT, módulos y permisos efectivos. `GET /api/PortalAcceso/me` permite renovar la vista de permisos. El administrador propietario administra usuarios en `/permisos`; la cuenta interna configura módulos por propietario en `/acceso-interno`. React oculta enlaces y bloquea navegación a rutas sin permiso. La API vuelve a consultar usuario y módulos para las operaciones administrativas; desactivar usuario o módulo afecta esas operaciones de inmediato.

## Límite actual

Los endpoints legacy de datos siguen aceptando llamadas anónimas. Por eso la restricción de menú y rutas React no debe considerarse autorización completa de lectura de datos. Antes de exponer funciones de crear/importar/enviar documentos, esos endpoints deben exigir JWT, comprobar el permiso efectivo y tomar el `IdPropietario` del token, sin confiar en un ID recibido del navegador. El cambio se debe coordinar con consumidores legacy de la API.

## Validación y activación

Compilar `WMSWebAPI/WMSWebAPI.csproj` y React; aplicar SQL en QAS, publicar API y React en una ventana coordinada, y verificar login del propietario, creación de un delegado, revocación de un permiso y desactivación de Kairos. No publicar el React nuevo antes de la API, porque el login usa el endpoint nuevo.

## Publicación y prueba del 2026-09-24

- Se aplicaron los tres scripts en `IMS4MB_CEALSA_QAS`. Las tres tablas existen; el propietario 1 tiene siete módulos habilitados.
- Se publicaron WMSWebAPI2 en `C:\inetpub\wwwroot\WMSWebAPI2` y React en `C:\Sites\PortalDMS`. La configuración activa de IIS y la conexión QAS se conservaron. Respaldo local previo: `deploy-backup-20260924-permisos` en el directorio de trabajo de Codex; contiene secretos de configuración y debe mantenerse privado.
- `https://existenciasenlinea.com.gt/login` y el asset React respondieron 200. El login de Manuchar respondió con propietario 1, siete módulos y trece permisos. El endpoint `me` y la consulta de módulos respondieron correctamente.
- Una consulta real de salidas de agosto de 2026 devolvió cinco documentos. El rango 24/01/2021–24/09/2026 de ingresos devolvió cero, consistente con la base. HTTP en puerto 80 no respondió.
- Se probó la cuenta interna deshabilitando Kairos de forma temporal: el permiso efectivo desapareció. Se restauró Kairos y quedaron los siete módulos y trece permisos originales. Un usuario delegado temporal pudo autenticarse con un permiso y recibió 403 al intentar administrar usuarios; luego fue desactivado y eliminado de QAS. No quedaron usuarios de prueba.
- La limitación de autorización de endpoints legacy descrita arriba sigue vigente.
