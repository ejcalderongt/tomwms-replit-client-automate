---
id: rule-05-utf8-bom-vb
type: rule
title: UTF-8 con BOM en archivos VB.NET (preservar ñ y acentos)
status: vigente
severity: high
applies_to: [productor, consumidor, todo el equipo]
sources:
  - skill: wms-tomwms §4 #5
  - validated_at: 2026-04-27
---
# Regla 05 — UTF-8 con BOM en archivos VB.NET (preservar ñ y acentos)

## Statement

Todos los archivos `.vb` deben guardarse en **UTF-8 con BOM** (Byte Order Mark `EF BB BF` al inicio). NUNCA guardar como ANSI/Windows-1252.

## Rationale

VS 2026 (y anteriores) lee VB.NET asumiendo UTF-8-BOM como encoding canónico. Si un archivo se guarda en ANSI:
- Caracteres como `ñ`, `á`, `é`, `í`, `ó`, `ú` se corrompen al volver a abrir en otro editor.
- Strings literales con `ñ` (`"Año"`, `"Pequeñoes"`, etc.) terminan mostrando garbage en producción.
- Comentarios con acentos se vuelven ilegibles.
Esto **ya pasó** en el pasado del proyecto — la regla nace de incidentes reales.

## Cómo cumplir

1. Editor de VS: configurado por default a UTF-8 BOM.
2. Editor externo (VSCode, etc.): forzar UTF-8 with BOM al guardar archivos `.vb`.
3. Cualquier herramienta automática (formatter, scripted edit) DEBE preservar el BOM.
4. Verificación: `file <archivo>.vb` debe reportar "UTF-8 Unicode (with BOM) text".
5. PR/bundle review: rechazar cambios que pierdan el BOM.

## Excepciones

Ninguna. Es un default técnico inflexible.

## Cross-refs

- `modules/mod-arquitectura-solution`
- `rules/rule-12-no-romper-compatibilidad` — esto es compatibilidad de encoding
