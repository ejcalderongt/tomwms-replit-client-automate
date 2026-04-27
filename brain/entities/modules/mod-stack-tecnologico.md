---
id: mod-stack-tecnologico
type: module
title: Stack tecnológico TOM WMS
status: estable
sources:
  - skill: wms-tomwms §1
  - validated_at: 2026-04-27
---

# Stack tecnológico

| Capa | Tecnología |
|---|---|
| Backend principal | VB.NET, .NET Framework 4.8, **VS 2026 + DevExpress 24** |
| BD productiva | SQL Server 2022 CU18 Standard (EC2 AWS, Windows Server 2022) |
| Handheld (HH) | Java, Android API minSdk/targetSdk=24, compileSdk=34, app `com.dts.tom` v8.2.3 |
| Web moderno (parcial) | C# .NET Core / .NET 8.0 (EntityCore + DALCore + WMSWebAPI) — migración **incompleta** |
| Tooling productor | Node 24, Python 3.11+, pnpm workspace |

## Identidad

- Producto: **TOM WMS** (Warehouse Management System) multi-cliente.
- Empresa: **PrograX24**.
- Owner técnico: **Erik José Calderón (EJC)**.
- Cliente activo en este workspace: **Killios** (BD `TOMWMS_KILLIOS_PRD`).
- Otros clientes con releases productivas: Becofarma, La Cumbre, Cealsa, Mampa (MHS).

## Rol del agente Replit

Ingeniero senior permanente con conocimiento del WMS entre sesiones. **Productor** de bundles que el consumidor (`openclaw` en Windows) aplica al repo WMS. NO push automático al WMS.

## Cross-refs
- `decisions/dec-formato-commits` — autoría
- `modules/mod-arquitectura-solution` — proyectos del solution TOMWMS_BOF
- `modules/mod-repo-tomwms-bof`, `mod-repo-tomhh2025`, `mod-repo-dba`, `mod-repo-exchange`
