---
tipo: runbook
cliente: CAREMED
ramas_afectadas: [wms-brain, codex/portal-portable-20261105]
triggers: [instala el portal, configura de forma interactiva, portal React, PC de Carol, CAREMED]
fecha: 2026-11-05
---
# Instalación del portal React en otra PC Windows

<!-- #EJC20261105_PORTAL_PORTABLE -->

## Orden que debe reconocer el agente

**Instala el portal y configura de forma interactiva.**

Al recibirla, cargar este runbook y `tools/portal/release.json`. Revisar si ya
existe una instalación del usuario. Si existe y responde, no recompilar ni
instalar otra vez: usar `Start-Portal.ps1`. Si falta, instalar requisitos y
fuentes fijadas; preguntar solamente la conexión y la autenticación de destino.
Continuar con configuración, arranque y validación sin detenerse en una propuesta.
Caso inicial: PC de Carol. El nombre no determina servidor, BD ni permisos.

El alcance es el portal operativo TOMWMSReact con API Core WMSWebAPI. BOF,
HH, IIS/ASMX, WikiHub, SQL Server local e IA no son dependencias que haya que
instalar para este caso. Se usa una base WMS existente, local o accesible por red.

## Obtener el contexto en la PC de Carol

Usar el checkout Brain existente y hacer `git pull --ff-only` si no hay cambios
locales que lo impidan. Si no existe, clonar el repositorio de conocimiento:

```powershell
git clone --branch wms-brain --single-branch https://github.com/ejcalderongt/tomwms-replit-client-automate.git "$env:USERPROFILE/source/repos/wms-brain"
```

Abrir esa carpeta en Codex. Su `AGENTS.md` enruta la orden corta a este documento.
No hace falta copiar el workspace ni `deployments/CAREMED` de Erik.

## Datos que debe pedir el agente

1. Servidor/instancia SQL o `servidor,puerto` y nombre exacto de la base.
2. Autenticación Windows o SQL. Para SQL, introducir usuario y contraseña en
   el diálogo local `Get-Credential`, no en el chat, argumentos ni logs.
3. Si hay conflicto de puertos, elegir dos puertos libres; normalmente 5174 y 5197.
4. Solo cuando falle la validación de certificado: confirmar con el responsable
   si se permite `-TrustServerCertificate` para ese servidor. Cifrado activado
   por defecto, certificado validado por defecto.

Ejecutar con el usuario Windows de Carol, ya que DPAPI liga el secreto al
usuario/equipo. La cuenta portal y su propietario/módulos pertenecen a la BD;
no se heredan del nombre de Windows y no se provisionan por instalar React.

## Instalar y construir

Desde la raíz del Brain:

```powershell
./tools/portal/Install-Portal.ps1 -SkipConfigure
```

El script comprueba Git, Node/npm y SDK .NET. Instala faltantes mediante winget
con identificadores exactos `Git.Git`, `OpenJS.NodeJS.LTS`,
`Microsoft.DotNet.SDK.8`. Requiere Node 22 o superior y SDK 8 o superior.
Si Windows solicita elevación o falta autenticación Azure, el usuario debe
completar ese paso local y el agente continúa. No cambiar ExecutionPolicy,
firewall, antivirus ni permisos de servicios para sortear un bloqueo.

Las fuentes privadas Azure requieren acceso a ambos repositorios. El manifiesto
fija URL, rama y commit de React/API; no usar el HEAD móvil de una rama BOF.
Se crean clones dedicados bajo `%LOCALAPPDATA%/TOMWMSPortal/source` con sparse
checkout, excluyendo los node_modules versionados del frontend y BOF/HH legacy.
Se ejecuta npm ci y Vite con `vite.caremed.config.ts`; se publica la API
self-contained para win-x64. No requiere Visual Studio, DevExpress ni IIS.

Las configuraciones `appsettings*.json` copiadas por el publish se reemplazan
por objetos vacíos antes del primer arranque. No reutilizar conexiones ni
claves SMTP/JWT históricas del repositorio. El instalador no arranca la API
hasta crear configuración propia. Nunca servir las carpetas `source`, `api`
o `private` desde el servidor estático, que tiene como raíz exclusiva `app/react`.

Si una instalación se interrumpe, se pueden reutilizar clones de la misma
revisión sin cambios; no sobrescribir carpetas ajenas. Para reconstruir una
instalación que ya arrancó, detenerla primero con `Stop-Portal.ps1`.

## Configurar la base de forma interactiva

```powershell
./tools/portal/Configure-Portal.ps1
```

Solicita servidor, BD y Windows/SQL. Alternativa para agente cuando servidor y
base ya están confirmados y se usa Windows:

```powershell
./tools/portal/Configure-Portal.ps1 -Server 'SERVIDOR\INSTANCIA' -Database 'BASE_CONFIRMADA' -WindowsAuthentication
```

El script prueba conexión y existencia de `portal_usuario`,
`portal_usuario_permiso`, `portal_propietario_modulo`, `propietarios` y
`VW_Stock_Res`, y que exista algún usuario portal activo. Estas comprobaciones
son SELECT; no crean objetos, cuentas, permisos ni datos operativos. No bastan
para certificar compatibilidad de todas las consultas: terminar con la prueba
funcional de cada módulo requerido.

Guarda la conexión cifrada con DPAPI en `private/connection.dpapi`, genera una
clave JWT local aleatoria y limita el directorio privado al usuario actual y
SYSTEM. No compartir esa carpeta ni versionarla. El proceso API recibe CST
por variable de entorno; React no recibe esa conexión. La carpeta está fuera
del Brain y de cualquier checkout.

Si faltan tablas o usuarios, detener únicamente la parte dependiente y explicar
el requisito exacto. El antecedente CAREMED está en
`brain/portal-caremed-demo-2026-10-23.md`: los scripts del esquema portal se
recuperaron del histórico DBA b93124e. Eso no autoriza a aplicarlos a otra BD.
Solicitar al responsable el provisionamiento aprobado y reintentar; nunca
crear automáticamente un administrador con una clave compartida de demo.

## Arrancar y volver a usar

```powershell
./tools/portal/Start-Portal.ps1 -OpenBrowser
```

También queda **Iniciar Portal TOMWMS** en el escritorio. Después del reinicio
solo se requiere ese doble clic. El acceso inicia API/React si no estaban
ejecutándose, espera HTML React y 401 de `/api/PortalAcceso/me` sin token y
abre `http://127.0.0.1:5174/login`. El 401 es la respuesta esperada sin sesión.
Comprueba propietario del puerto mediante PID y fecha de inicio guardados;
no reutiliza ni detiene servicios desconocidos. No configura inicio automático.

Tras instalar, los scripts también quedan en
`%LOCALAPPDATA%/TOMWMSPortal/tools`; aceptan `-InstallRoot` si se eligió otra ruta.
Desde PowerShell, ante una política que impida ejecutar scripts, usar un
mecanismo aprobado por la organización; no debilitarla automáticamente.

Para parar solo esta instalación:

```powershell
./tools/portal/Stop-Portal.ps1
```

## Prueba funcional obligatoria del agente

Prueba de humo disponible (solicita la cuenta portal en un diálogo local):

```powershell
./tools/portal/Test-Portal.ps1 -From '2026-09-01' -To '2026-09-30'
```

Sustituir el período por uno con actividad en la BD elegida. Esta prueba revisa
login, identidad, bodegas, inventario y ambos KPIs; no registra datos ni tokens.
Un HTTP 200 prueba el contrato y ejecución de esa consulta, no sus totales.

- Iniciar sesión con una cuenta portal existente de la base elegida; el
  usuario introduce la contraseña localmente. No copiar `caremed.demo` de Erik.
- Confirmar propietario y módulos permitidos; consultar bodegas e inventario.
- Consultar ingresos y salidas con un rango donde la BD tenga actividad.
- Consultar KPI Picking y KPI Recepción en ese mismo rango y Análisis de
  Inventario si los permisos lo incluyen. En la base demo de Erik se utilizó
  septiembre de 2026; no asumir ese período en la PC de Carol.
- Distinguir respuesta vacía válida de un 401, 403, timeout o fallo SQL.
- Registrar versión del manifiesto, pruebas y límites; no guardar tokens,
  claves ni filas de clientes en el Brain.

El instalador habilita el runtime. La compatibilidad del esquema, permisos,
conectividad y acceso de Carol solo se certifican al ejecutar estas pruebas
en su PC. Las marcas de tiempo de picking/recepción deben validarse antes de
interpretar líneas/hora como rendimiento de personas.

## Diagnóstico y límites

Logs locales: `%LOCALAPPDATA%/TOMWMSPortal/logs` y `app/api/Logs`. No publicar
logs completos sin revisar su contenido. Configuración de puertos en
`private/ports.json`. Cambiarlos mediante Configure y arrancar de nuevo después
de Stop; no editar una conexión cifrada manualmente.

Este flujo sirve solo en el equipo local, con ambos procesos en 127.0.0.1.
Para acceso LAN/publicación productiva se necesita un alcance separado de
alojamiento, TLS, permisos, firewall y soporte. No mover servicios BOF/HH,
modificar bases productivas ni instalar SQL Server por asumir que falta.

## Evidencia y versiones

Fuentes fijadas en `tools/portal/release.json`: React 05586ce y API aeace3d3.
Incluyen el inventario por propietario y su paginación que estaban desplegados
en la demo pero antes no estaban publicados como revisiones instalables.
La evidencia de validación de este flujo se registra junto al runbook.
La instalación y validación específicas en la PC de Carol quedan pendientes
hasta ejecutarlo allí con su conexión.
