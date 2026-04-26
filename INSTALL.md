# Instalación

## Requisitos
- Git
- PowerShell 7+
- OpenClaw instalado en la máquina destino

## Primer bootstrap
```powershell
.\scripts\openclaw-restore.ps1 -Mode Bootstrap
```

## Exportar estado
```powershell
.\scripts\openclaw-export.ps1
```

## Restaurar
```powershell
.\scripts\openclaw-restore.ps1 -Mode Restore
```
