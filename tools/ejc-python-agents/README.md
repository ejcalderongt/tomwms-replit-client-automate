# EJC Python Agents (MVP)

Motor local en Python para acelerar diagnostico tecnico en dos vias:

1. **Data lane**: tablas, estados, cantidades, cruces SQL sugeridos.
2. **Operativity lane**: flujo real de UI/HH/WS, permisos, botones y transiciones.

La idea es procesar "lo grueso" del caso, dejar evidencia util, y darle a Codex
visibilidad inmediata para debug, traza y correccion.

## Estructura

- `agents/ejc-python-agent.yml`: configuracion del agente.
- `ejc_agent.py`: runtime principal.
- `out/current-case-report.md`: salida consolidada del ultimo analisis.

## Uso rapido

```powershell
python .\tools\ejc-python-agents\ejc_agent.py inspect `
  --case "Documento 17946 en back-order sin banderita no enviado" `
  --refs "17946,66967,301202600008320"
```

Opcionalmente puedes pasar evidencia en texto (correo, logs, etc.):

```powershell
python .\tools\ejc-python-agents\ejc_agent.py inspect `
  --case "Back-order CEALSA sin boton no enviado" `
  --evidence-file ".\tmp\correo_cealsa.txt"
```

## Notas

- No ejecuta escrituras en BD.
- No publica a Jira.
- Genera reporte de apoyo para dimensionamiento tecnico antes de aplicar fix.
