# WMS Context Router + LM Studio

Herramienta local para reducir tokens antes de abrir una tarea en Codex.
Selecciona contexto desde `brain/agents/_index.yml` y, si LM Studio esta
corriendo, genera un brief compacto usando el endpoint OpenAI-compatible local.

## Requisitos

1. Abrir LM Studio.
2. Cargar un modelo local.
3. Iniciar el servidor local en `http://127.0.0.1:1234/v1`.

## Uso rapido

```powershell
.\tools\wms-context\wms_context.ps1 "reserva MI3 BYB EA-153305 performance"
```

Salida principal:

```text
tools/wms-context/out/current-brief.md
```

Modo sin LLM, solo seleccion deterministica:

```powershell
.\tools\wms-context\wms_context.ps1 "endpoint mi3 di-estatus" -NoLlm
```

Forzar regeneracion e indicar modelo:

```powershell
.\tools\wms-context\wms_context.ps1 "stock_res no reserva BYB" -Refresh -Model "local-model-name"
```

Variables opcionales:

```powershell
$env:LM_STUDIO_BASE_URL = "http://127.0.0.1:1234/v1"
$env:LM_STUDIO_MODEL = "local-model-name"
```

## Regla de seguridad

El brief no debe contener secretos ni el modulo `definition` del catalogo SQL.
Esta herramienta no escribe en Brain ni en la base productiva.
