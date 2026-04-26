# TOMWMS Replit Client Automate

Repositorio de bootstrap, sync y restore para replicar el ambiente OpenClaw/TOMWMS entre máquinas.

## Qué guarda
- Skills propias
- Scripts de automatización
- Configuración exportable
- Manifiestos de estado
- Backups operativos

## Flujo
1. `scripts/openclaw-export.ps1`
2. commit/versionado
3. `scripts/openclaw-restore.ps1` en otra máquina
4. validación y log

## Objetivo
Replicar un entorno funcional con el menor riesgo posible, sin copiar secretos en claro.
