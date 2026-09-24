# Portal DMS: estado verificado el 2026-09-24

- Sitio IIS: `PortalDMS`, archivos en `C:\Sites\PortalDMS`.
- URL pública: `https://existenciasenlinea.com.gt/` (HTTP 200 en la prueba).
- `web.config` envía `/api/*` y `/kpi/*` a `http://192.168.1.4:8097/api/*`; las rutas restantes llegan al `index.html` de React.
- API: sitio IIS `WMSWebAPI2`; el login del propietario usa `POST /api/Auth/login-propietario`.
- Una identidad inválida devolvió HTTP 401 a través de HTTPS, comprobando que la ruta pública llega a autenticación. La prueba con credenciales reales de manuchar queda pendiente de disponer de una credencial vigente.
- El 2026-09-24 se documentó que el propietario 1, MANUCHAR GUATEMALA, S.A., recibió el código de acceso `manuchar`; una prueba real previa devolvió 200 y permitió consultar stock. Véase `DEPLOYMENT-NOTES.md` en el directorio de trabajo del servidor. No guardar contraseñas ni tokens aquí.
- Código React local: `TOMWMSReact`, inicialmente en `main` con remoto Azure. GitHub `ejcalderongt/tomwmsreact`, rama `dev_replit`, es la referencia solicitada. Al comparar, `dev_replit` en `70a4b4a` era ancestro de `main` local en `f7b247f`; había 239 commits posteriores en local.
- Se quitaron los dos bindings HTTP de `PortalDMS` y el binding comodín `*:80:` de `Default Web Site`. La comprobación posterior: HTTP puerto 80 agotó el tiempo de conexión; HTTPS puerto 443 devolvió 200. Los puertos internos 8091 y 8097 permanecieron en sus sitios IIS.
- GitHub `dev_replit` se avanzó sin reescribir historia hasta `f7b247f` y el checkout local cambió a esa rama, que sigue `github/dev_replit`. Azure `origin/main` local está en el mismo commit; la consulta remota Azure pidió autenticación y no pudo verificarse en esta sesión.
