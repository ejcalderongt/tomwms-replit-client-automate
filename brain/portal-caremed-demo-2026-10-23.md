# Portal React contra demo local CAREMED

<!-- #EJC20261105_PORTAL_PORTABLE -->
Para instalar en otra PC o en la PC de Carol, usar
[`runbooks/portal-react-instalacion-otra-pc.md`](runbooks/portal-react-instalacion-otra-pc.md).
Las rutas e IP de este documento son evidencia histórica de la demo de Erik,
no parámetros que se deban copiar a otra estación.

#EJC20261023_CAREMED_PORTAL

- Fuente React: Azure DevOps TOMWMSReact, dev_replit, clon local C:/Users/yejc2/source/repos/TOMWMSReact. SHA inicial e5b13f642d6b6ac4ddf30811fc401f6f1ae96b67.
- API portal: dev_2028_merge, SHA 1d2daee5, checkout separado C:/Users/yejc2/source/repos/TOMWMS_PORTAL_API. No sustituir BOF/HH estable; se conservaron en dev_2026_estable.
- Runtime: http://127.0.0.1:5174/ -> proxy /api y /kpi -> http://127.0.0.1:5197 -> prograx\SQLEXPRESS / TOMWMS_CAREMED_PRD, propietario 1.
- React usa vite.caremed.config.ts; no ejecutar vite.config.ts original para esta demo porque apunta a una API remota. API usa entorno CaremedDemo con configuracion privada excluida de Git y JWT exclusivo local.
- Faltaban portal_usuario, portal_propietario_modulo y portal_usuario_permiso. Los scripts no estaban en el master GitHub actual consultado 571e8f5e; se recuperaron del commit historico b93124e y se aplicaron juntos en transaccion, validando servidor/BD. Copias: DBA/caremed-demo-20261023.
- Cuenta administrativa exclusiva de demo creada con hash por PortalAccessService.CreateUser; no se alteraron credenciales legacy. No guardar su clave, JWT ni configuracion privada en Brain.
- Pruebas reales: login, /me, bodegas, stock, ingresos, salidas, detalles OC/PE, picking/verificacion/recepcion/despacho/tendencias/heatmap/stock-res HTTP 200. Sin token /me devuelve 401. UI muestra 6509 existencias y 319 ingresos; API lista 2376 salidas en septiembre-octubre 2026.
- Los datos principales son historicos: usar septiembre-octubre 2026 para la demo. Un rango actual vacio no prueba fallo de conexion.
- Arranque reproducible: C:/Users/yejc2/OneDrive/Documents/ChatGPT/TOMWMS/scripts/Start-CaremedPortal.ps1.
- Nota completa: C:/Users/yejc2/OneDrive/Documents/ChatGPT/TOMWMS/docs/caremed/portal-local-2026-10-23.md.
- Kairos necesita proveedor IA y servidor de integracion; no se configuraron claves IA ni SMTP. Entorno local, sin publicacion productiva ni cambios al inventario.
 #EJC20261023_CAREMED_PUBLICACION_LAN: solicitud final aclarada WMSWebAPI + React, no MI3. Publicados en deployments/CAREMED/api (Release, 0.0.0.0:5197) y deployments/CAREMED/react con portal-server.cjs (0.0.0.0:5174). Accesos http://192.168.0.6:5197 y http://192.168.0.6:5174; login y stock reales por IP correctos. Script Start-CaremedPortal.ps1 actualizado. MI3 legacy solo compilada/empaquetada, no iniciada. Pruebas desde el propio equipo; firewall global sin cambios por falta de elevacion.
 #EJC20261023_PORTAL_STOCK_OWNER: resuelto Analisis de Inventario. React getStock estaba bloqueado deliberadamente porque KPI stock/res no filtraba propietario. Se implemento filtro SQL parametrizado desde identidad validada, permiso inventario.ver, rechazo de idPropietario ajeno y paginacion completa React (2000 filas/pagina). API y frontend publicados. Test-CaremedInventoryAnalysis.ps1 valida 401/403/400, filtro de bodega, 6509 filas en cuatro paginas y disponible 542023 igual SQL. UI real Analisis de Inventario ya muestra metricas de vencimiento/antiguedad/bodega/ubicacion/familia. Sin alteracion de datos o esquema.

## #EJC20261023 Prueba recepción demo
PASS BOF→IIS8098→HH: documento0002235, recepción2176, tarea17861 cerrados; producto1976 recibido1UN y stock1. Prueba asistida en login/confirmación BOF; captura/finalización HH automatizada por ADB. Evidencia workspace docs/caremed/recepcion-e2e-2026-10-23.md. Conservar datos de prueba; no confundir con inventario real.
