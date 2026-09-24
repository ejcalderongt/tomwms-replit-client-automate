# Portal DMS: estado verificado el 2026-09-24

- Sitio IIS: `PortalDMS`, archivos en `C:\Sites\PortalDMS`.
- URL pública: `https://existenciasenlinea.com.gt/` (HTTP 200 en la prueba).
- `web.config` envía `/api/*` y `/kpi/*` a `http://192.168.1.4:8097/api/*`; las rutas restantes llegan al `index.html` de React.
- API: sitio IIS `WMSWebAPI2`; el login del propietario usa `POST /api/Auth/login-propietario`.
- Una identidad inválida devolvió HTTP 401 a través de HTTPS, comprobando que la ruta pública llega a autenticación. Con la credencial vigente proporcionada por el propietario, el login real de manuchar devolvió HTTP 200 para el propietario 1, activo; una consulta de stock con el token devolvió HTTP 200. No se guardaron la contraseña ni el token.
- El 2026-09-24 se documentó que el propietario 1, MANUCHAR GUATEMALA, S.A., recibió el código de acceso `manuchar`; una prueba real previa devolvió 200 y permitió consultar stock. Véase `DEPLOYMENT-NOTES.md` en el directorio de trabajo del servidor. No guardar contraseñas ni tokens aquí.
- Código React local: `TOMWMSReact`, inicialmente en `main` con remoto Azure. GitHub `ejcalderongt/tomwmsreact`, rama `dev_replit`, es la referencia solicitada. Al comparar, `dev_replit` en `70a4b4a` era ancestro de `main` local en `f7b247f`; había 239 commits posteriores en local.
- Se quitaron los dos bindings HTTP de `PortalDMS` y el binding comodín `*:80:` de `Default Web Site`. La comprobación posterior: HTTP puerto 80 agotó el tiempo de conexión; HTTPS puerto 443 devolvió 200. Los puertos internos 8091 y 8097 permanecieron en sus sitios IIS.
- GitHub `dev_replit` se avanzó sin reescribir historia hasta `f7b247f` y el checkout local cambió a esa rama, que sigue `github/dev_replit`. Azure `origin/main` local está en el mismo commit; la consulta remota Azure pidió autenticación y no pudo verificarse en esta sesión.

## Análisis de código y permisos

El mapa de archivos, flujo IIS/API/SQL, estrategia de traslado a Azure DevOps y diseño de permisos está documentado en `docs/PORTAL_ARCHITECTURE.md` de `ejcalderongt/tomwmsreact`, rama `dev_replit`.

Hallazgo prioritario: el menú React es fijo y `PrivateRoute` solo comprueba la presencia de sesión. El login de propietario emite `rol=admin` fijo. Las listas de ingresos y salidas y la consulta de stock devolvieron HTTP 200 sin token; los permisos de menú requieren control en la API antes de considerarse restricciones de acceso. Las tablas `rol`, `menu_sistema` y `menu_rol` existentes pertenecen al menú legacy; React no las usa.

Tras autenticación, se creó y verificó `dev_replit` en Azure DevOps desde el commit GitHub `edeab60`, sin modificar Azure `main`. El checkout local ahora sigue `origin/dev_replit`. Azure DevOps pasa a ser la fuente principal del código React y GitHub queda como espejo de esa rama. El documento de arquitectura en React registra el flujo de publicación.
