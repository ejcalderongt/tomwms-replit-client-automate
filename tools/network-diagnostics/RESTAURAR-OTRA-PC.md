# Restaurar el Brain y ejecutar el diagnóstico

<!-- #EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS -->

En la otra PC, con Git y acceso autorizado al repositorio, abrir PowerShell. Para una instalación nueva usar una carpeta que todavía no exista:

```powershell
git clone --single-branch --branch wms-brain https://github.com/ejcalderongt/tomwms-replit-client-automate.git C:\WMSBrain
Set-Location C:\WMSBrain
```

Si el Brain ya está clonado, entrar en su carpeta y comprobar primero que la rama sea `wms-brain` y no haya cambios pendientes. En un checkout limpio de esa rama se puede actualizar con `git pull --ff-only origin wms-brain`. Conservar cualquier trabajo local; no usar reset/clean para forzar la restauración.

Leer `AGENTS.md` y `brain/runbooks/cumbre-diagnostico-latencia-en-sitio.md`. Entregar al agente el archivo `tools/network-diagnostics/PROMPT-AGENTE.md`, indicando que el Brain completo está en esta carpeta y que sus instrucciones actuales sustituyen la necesidad del extracto `brain-context/` del ZIP.

## Preparación y pruebas en La Cumbre

```powershell
Set-Location C:\WMSBrain\tools\network-diagnostics
.\Prepare-PC.ps1
# Opcional para capturar paquetes; completar instalación de Npcap:
.\Prepare-PC.ps1 -InstallWireshark
.\Start-Capture.ps1 -List
Copy-Item .\config.example.json .\config.local.json
notepad.exe .\config.local.json
# Completar destinos REALES y métodos de lectura según README.md antes de ejecutar:
.\Run-Diagnostics.ps1 -ConfigPath .\config.local.json
```

No sobrescribir config.local.json si ya existe una configuración local que deba conservarse. La plantilla no tiene destinos. El acceso del agente a Internet no demuestra acceso a WS/SQL; comprobar la ruta desde esa PC conectada a la red del cliente. Medir cable/Wi-Fi por separado y marcar episodios con Mark-Incident.ps1.

## Paquete portable opcional

Desde la raíz del Brain, usando un destino nuevo:

```powershell
.\tools\network-diagnostics\Export-Deployment.ps1 -Destination C:\CumbreDiag-Paquete
```

Se genera la carpeta y `C:\CumbreDiag-Paquete.zip` con scripts y contexto mínimo, sin configuraciones privadas ni capturas. La guía principal y el prompt están dentro del ZIP. Para conservar el Brain completo seguir trabajando en el checkout C:\WMSBrain.

## Repetir las pruebas sintéticas de preparación

Opcional, antes de medir la instalación: con Python 3 y ambos PowerShell instalados, desde la raíz del Brain ejecutar:

```powershell
python .\analysis\cumbre-network-suite-20261111\validate_suite.py
```

Solo usa loopback. Genera evidencia local de la máquina y un informe de validación; revisar y sanear antes de publicar nuevos resultados. No prueba Wireshark, un login SQL exitoso ni la red del cliente. Para el diagnóstico normal no hace falta Python.
