---
id: mod-arquitectura-solution
type: module
title: Arquitectura del solution TOMWMS_BOF.sln
status: estable
sources:
  - skill: wms-tomwms §5
  - validated_at: 2026-04-27
---

# Arquitectura del solution TOMWMS_BOF

Solution `TOMWMS_BOF.sln` agrupa los siguientes proyectos relevantes (no exhaustivo):

| Proyecto | Tipo | Rol |
|---|---|---|
| `TOMIMSV4` | WinForms .NET 4.8 | UI principal: módulos de transacciones (Ajustes, Recepciones, Despachos, Conteos), maestros, reportes, configuración |
| `Entity` | Class library | DTOs / entidades de dominio (legacy, `cls<Nombre>`) |
| `DAL` | Class library | Data Access Layer legacy: clase principal `cls_DBSql` / `lDB`. Wrap de `SqlConnection`/`SqlCommand`/`SqlDataReader` |
| `WSHHRN` | ASMX WebService | **WebService de los handhelds**. Endpoints SOAP que consume `TOMHH2025`. Punto de integración HH↔backend |
| `WSSAPSYNC` | ASMX WebService | Sync con SAP |
| `WMSWebAPI` | ASP.NET WebAPI | API moderna parcial (migración incompleta a .NET 8) |
| `EntityCore` | .NET Core class library | Versión moderna de Entity (parcial) |
| `DALCore` | .NET Core class library | Versión moderna de DAL (parcial) |
| `Interfaces.*` | Class libraries | Adaptadores: NAV, SAP, Cealsa, DMS, AWS |

## Convenciones de carpetas dentro de `TOMIMSV4`

```
TOMIMSV4/TOMIMSV4/
├── Maestros/             ← forms de maestros (clientes, productos, ubicaciones)
├── Transacciones/        ← forms operativos
│   ├── Ajustes/          ← donde vive frmAjusteStock.vb
│   ├── Recepciones/
│   ├── Despachos/
│   └── ...
├── Reportes/
├── Configuracion/
└── Comunes/              ← utilidades transversales
```

## Reglas duras relacionadas (M2)
- `rules/rule-03-no-tocar-reference-vb` — Reference.vb autogenerados
- `rules/rule-05-utf8-bom-vb` — encoding obligatorio
- `rules/rule-04-no-reescribir-desde-cero` — debuggear primero

## Cross-refs
- `modules/mod-repo-tomwms-bof`
- `modules/mod-protocolo-hh-ws` (M3) — interface WSHHRN ↔ TOMHH2025
