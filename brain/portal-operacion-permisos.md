---
id: portal-operacion-permisos
tipo: documentation
estado: vigente
titulo: Portal TOMWMSUX — operación, permisos y publicación
ramas: [wms-brain]
tags: [portal, permisos, react, api, sql]
---

# Portal TOMWMSUX: guía operativa

Estado verificado el 24/09/2026 en `cealsa-02-srv`. No guardar usuarios, contraseñas, JWT, PAT ni cadenas de conexión en este documento. El contexto de implementación y las pruebas están en [portal-permisos-2026-09-24.md](portal-permisos-2026-09-24.md).

## Acceso y responsabilidad

| Función | URL pública | Quién la usa | Alcance |
| --- | --- | --- | --- |
| Login de propietario o delegado | `https://existenciasenlinea.com.gt/login` | Propietario WMS y personas delegadas | Obtiene propietario, módulos y permisos efectivos. |
| Usuarios y permisos | `https://existenciasenlinea.com.gt/permisos` | Propietario o delegado con `EsAdministrador` | Crea, edita, desactiva y asigna permisos a usuarios de su propietario. |
| Acceso interno | `https://existenciasenlinea.com.gt/acceso-interno` | Operación interna de la empresa | Permite elegir un propietario y habilitar/deshabilitar sus módulos en `/permisos`. |

El propietario 1 es Manuchar en la base QAS usada por este despliegue. La cuenta de propietario existente funciona como administrador inicial; no se crea una fila `portal_usuario` para ella. La cuenta interna se configura en `InternalAuth` dentro de `C:\inetpub\wwwroot\WMSWebAPI2\appsettings.json`. Es una credencial compartida: debe reemplazarse por identidad administrativa individual. Mantener ese archivo y los respaldos fuera de Git.

Para retirar un módulo a todo el propietario: iniciar sesión en `/acceso-interno`, escribir `IdPropietario`, pulsar **Cargar propietario** y desmarcar el módulo. El cambio se guarda al instante. Para limitar a una persona concreta: iniciar sesión como administrador propietario, abrir `/permisos`, elegir el usuario y retirar sus permisos; también puede desactivarlo. Un módulo deshabilitado por la empresa prevalece sobre cualquier permiso asignado. Al reactivarlo reaparecen los permisos asignados previamente.

## Modelo y fuente de permisos

| Entidad SQL en `IMS4MB_CEALSA_QAS` | Propósito | Código correspondiente |
| --- | --- | --- |
| `dbo.portal_usuario` | Persona delegada, propietario, hash de contraseña, indicador administrador y activo. | `WMS.EntityCore/Portal/clsBePortalUsuario.cs`; `WMS.DALCore/Portal/clsLnPortalUsuario.cs` |
| `dbo.portal_propietario_modulo` | Habilitación de cada módulo por propietario, administrada por la empresa. | `clsBePortalPropietarioModulo.cs`; `clsLnPortalPropietarioModulo.cs` |
| `dbo.portal_usuario_permiso` | Permisos explícitos de cada persona delegada. | `clsBePortalUsuarioPermiso.cs`; `clsLnPortalUsuarioPermiso.cs` |

Los scripts idempotentes están en GitHub `ejcalderongt/DBA`, rama `master`: `portal_usuario.sql`, `portal_propietario_modulo.sql` y `portal_usuario_permiso.sql`. Se aplican en ese orden. El script de módulos sembró todos los módulos para los propietarios activos existentes; al crear un propietario nuevo hay que provisionar sus siete filas. La ausencia de fila deniega el módulo. No hay tabla de roles propia del portal: `EsAdministrador` es el único perfil administrativo actual. Las tablas legacy `rol`, `menu_sistema` y `menu_rol` no gobiernan React.

| Módulo | Permisos | Menú/rutas actuales |
| --- | --- | --- |
| `inventario` | `inventario.ver` | Inventario en Línea, existencias, resumen, movimientos, dashboard. |
| `ingresos` | `ingresos.ver`, `ingresos.crear`, `ingresos.importar`, `ingresos.enviar` | Ingresos y detalle; las tres acciones de escritura son futuras. |
| `salidas` | `salidas.ver`, `salidas.crear`, `salidas.importar`, `salidas.enviar` | Salidas y detalle; las tres acciones de escritura son futuras. |
| `indicadores` | `indicadores.ver` | Cinco KPI operativos. |
| `analisis` | `analisis.ver` | Dashboard ejecutivo y análisis avanzados. |
| `kairos` | `kairos.usar` | Asistente Kairos EC. El servicio `/ai/chat` no está disponible en este IIS estático. |
| `automatizaciones` | `automatizaciones.ver` | Automatizaciones. |

El catálogo vive en `WMSWebAPI/Services/Portal/PortalCatalogo.cs` y su representación de rutas en `TOMWMSReact/src/config/portalPermissions.ts`. Mantener ambos alineados. Los administradores propietarios reciben todos los permisos de módulos habilitados. Los delegados reciben solo la intersección entre permisos asignados y módulos habilitados. El JWT dura cuatro horas; la API vuelve a leer estado para sus endpoints administrativos.

## Flujo técnico

`Browser HTTPS :443` → IIS `PortalDMS` (`C:\Sites\PortalDMS`) → URL Rewrite/ARR `/api/*` → IIS `WMSWebAPI2` (`C:\inetpub\wwwroot\WMSWebAPI2`, `192.168.1.4:8097`) → SQL Server local `IMS4MB_CEALSA_QAS`. El puerto HTTP 80 del portal está cerrado. `web.config` del portal conserva rewrite y fallback de rutas React; no sustituirlo con archivos del build.

- `POST /api/PortalAcceso/login`: autentica propietario legacy o delegado; devuelve JWT, propietario, módulos y permisos.
- `GET /api/PortalAcceso/me`: devuelve permisos efectivos actuales.
- `GET /api/PortalAcceso/catalogo`: catálogo del backend.
- `GET|POST /api/PortalAcceso/usuarios`, `PUT /api/PortalAcceso/usuarios/{id}`: solo administrador del mismo propietario.
- `GET /api/PortalAcceso/modulos/{id}`: propietario correspondiente o cuenta interna.
- `PUT /api/PortalAcceso/modulos/{id}/{codigo}`: solo cuenta interna.
- `POST /api/Auth/login-interno`: emite el token interno existente que usa `/acceso-interno`.

React guarda la sesión en `wms_token`, `wms_user` y `wms_idPropietario`. `Layout.tsx` oculta enlaces según `canAccessPath`; `PrivateRoute.tsx` bloquea rutas directas y envía a `/sin-acceso`. Cambiar módulos o desactivar un usuario puede requerir renovar la sesión para refrescar el menú ya abierto; el backend administrativo consulta el estado vigente.

## Publicación y comprobación

Fuentes: React Azure DevOps `TOMWMSReact/dev_replit` y espejo GitHub `ejcalderongt/tomwmsreact/dev_replit` (implementación de permisos en `6873101`); API Azure DevOps `TOMWMS_BOF/dev_2028_merge` (implementación en `f9c49d3`); DBA GitHub `master` (scripts en `b93124e`). El brain se mantiene en GitHub `tomwms-replit-client-automate/wms-brain`. Los commits documentales posteriores no cambian esos artefactos desplegados.

1. Compilar API .NET 8 en Release y React con TypeScript y Vite. Validar que los scripts de DBA estén aplicados en la base `CST` activa. No aplicar estos scripts en KILLIOS: las reglas del BOF permiten allí solo lectura.
2. Respaldar las carpetas IIS completas. El respaldo de esta publicación está bajo `deploy-backup-20260924-permisos` en el directorio local de trabajo y contiene secretos: acceso restringido.
3. Publicar API primero; conservar `appsettings.json`, `appsettings.Development.json` y `web.config` activos. Usar `app_offline.htm` durante la copia y comprobar que Swagger incluya `/api/PortalAcceso/login`.
4. Publicar los assets React y luego `index.html`; conservar `C:\Sites\PortalDMS\web.config`. El script usado fue `deploy-stage/deploy-permisos.ps1` en el directorio local de trabajo; incluye reversión desde el respaldo si falla la API.
5. Probar HTTPS `/login` y asset JS; login de propietario, `/me`, módulos, un delegado con permisos limitados y un endpoint de datos. Comprobar HTTP 80 cerrado y que no queden usuarios de prueba. No imprimir tokens ni contraseñas.

Pruebas de esta publicación: portal/asset 200, propietario 1 con siete módulos y trece permisos, cinco salidas en agosto 2026, cero ingresos de Manuchar entre 24/01/2021 y 24/09/2026, Kairos deshabilitado y restaurado, delegado temporal con un permiso y HTTP 403 al consultar usuarios administrativos; el delegado se eliminó. Repositorios limpios después del despliegue. El código fuente no se publica automáticamente en IIS al hacer push.

## Límite de seguridad pendiente

Los endpoints legacy de datos, incluidos ingresos, salidas y stock, todavía permiten llamadas sin JWT. El menú y `PrivateRoute` limitan la interfaz, pero no protegen esos datos frente a llamadas directas. Antes de considerar el modelo como autorización completa, exigir JWT y permisos efectivos en esos endpoints y derivar `IdPropietario` del token; coordinarlo con otros consumidores de la API. Tampoco exponer `crear/importar/enviar` hasta aplicar esa validación. Rotar credenciales compartidas y el PAT que se expuso en la conversación anterior.
