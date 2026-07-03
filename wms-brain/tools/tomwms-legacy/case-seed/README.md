# case-seed

Exporta un paquete de evidencia (seed) desde SQL productivo **solo lectura** para análisis en Replit.

## Requisitos
- PowerShell 5+
- Acceso SQL desde RDP/VPN
- Variables de entorno:
  - `WMS_KILLIOS_DB_HOST`
  - `WMS_KILLIOS_DB_NAME`
  - `WMS_KILLIOS_DB_USER`
  - `WMS_EC2_DB_PASSWORD`

## Uso rápido
```powershell
cd tools\case-seed
.\export_case_seed.ps1 `
  -CaseId "INC-2026-04-16-GUINDA" `
  -CaseType "data-discrepancy" `
  -From "2026-04-16 00:00:00" `
  -To   "2026-04-16 23:59:59" `
  -IdProducto 0 `
  -IdBodega 0 `
  -OutDir ".\out"
```

## Output
Genera:
- Carpeta: `out/seed_<CaseId>_<timestamp>/`
- Archivos por query:
  - `NN_name.resolved.sql` (SQL final con parámetros ya resueltos)
  - `NN_name.json`
  - `NN_name.csv`
- `manifest.json` (metadatos del caso + conteo de filas)
- ZIP final: `out/seed_<CaseId>_<timestamp>.zip`

## ¿Envía automáticamente a Replit?
**No.** Por diseño, este script **no hace envío automático**.

Flujo recomendado:
1. Ejecutas script en RDP.
2. Tomas el ZIP generado.
3. Lo adjuntas en Replit/chat junto al `CASE_INTAKE_TEMPLATE.md` lleno.

Así mantienes control manual y evitas fuga accidental de datos.

## Nota
Las queries base de `queries/data-discrepancy` son plantilla inicial. Ajusta nombres de columnas si tu esquema de cliente difiere.

