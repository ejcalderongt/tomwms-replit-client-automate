# TOMWMS Replit Client Automate

Repositorio base para sincronizar ambiente OpenClaw/TOMWMS entre máquinas.

## Incluye
- Scripts PowerShell de export, restore y set-config
- Carpeta para skills reutilizables
- Manifiestos de estado
- Reglas de sincronización

## Flujo recomendado
1. Exportar el estado actual.
2. Versionar los cambios.
3. Revisar diff en rama efímera.
4. Restaurar en otra máquina.
5. Validar que el estado coincida.

## Regla principal
No guardar secretos en claro.
