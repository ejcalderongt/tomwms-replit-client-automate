# Relación con `tools/case-seed/`

## TL;DR

`tools/case-seed/` es la **herramienta de captura forense** ya existente y consolidada. `dataway-analysis/` es la **biblioteca de interpretación** que se construye sobre ella. División de responsabilidades estricta: case-seed captura, dataway-analysis interpreta.

## Inventario de `tools/case-seed/` (estado actual)

Ubicación: `TOMWMS_BOF/tools/case-seed/`

```
case-seed/
├── README.md
├── export_case_seed.ps1            ← script PowerShell de extracción
├── queries/
│   └── data-discrepancy/
│       ├── 01_stock_snapshot.sql
│       ├── 02_movements_window.sql
│       ├── 03_vw_stock_res.sql
│       └── 04_adjustments_window.sql
└── templates/
    └── CASE_INTAKE_TEMPLATE.md
```

### Diseño explícito declarado en su README

- **Solo lectura** sobre BD productiva.
- **Sin envío automático** a Replit (política de fuga de datos manual).
- Salida: ZIP con `manifest.json` + por cada query `NN_name.resolved.sql` + `NN_name.json` + `NN_name.csv`.
- Parametrización: `CaseId`, `CaseType`, `From`, `To`, `Sku`, `IdProducto`, `IdBodega`.
- 4 tipos de caso previstos en el intake template: `data-discrepancy | hh-bug | vb-exception | sql-perf`.
- Variables de entorno cliente Killios: `WMS_KILLIOS_DB_HOST`, `WMS_KILLIOS_DB_NAME`, `WMS_KILLIOS_DB_USER`, `WMS_KILLIOS_DB_PASSWORD`.

### Madurez

- README claro con uso paso a paso.
- Script funcional, parametrizado, defensivo (`Set-StrictMode`, `$ErrorActionPreference = 'Stop'`).
- Template de intake completo (5 secciones: Identificación, Síntoma, Alcance técnico, Evidencia adjunta, Contexto adicional).
- Comment del README dice: "Las queries base de `queries/data-discrepancy` son **plantilla inicial**. Ajusta nombres de columnas si tu esquema de cliente difiere." → diseñado para extensión.

## División de responsabilidades

| Responsabilidad | `tools/case-seed/` | `dataway-analysis/` |
|---|---|---|
| Definir CaseId y metadatos del caso | ✅ (`CASE_INTAKE_TEMPLATE.md`) | — |
| Extraer datos crudos de BD productiva | ✅ (`export_case_seed.ps1`) | — |
| Empaquetar evidencia para análisis | ✅ (ZIP + manifest.json) | — |
| Mantener queries SQL parametrizables | ✅ (`queries/<CaseType>/`) | — |
| Catalogar las **operaciones que mutan IdStock** | — | ✅ (capa 01) |
| Documentar la **fórmula de balance** y sus reglas | — | ✅ (capa 04) |
| Mantener inventario de **case-pointers** del código legacy | — | ✅ (capa 07) |
| Modelar el **particionamiento de licencia** | — | ✅ (capa 02) |
| Documentar bugs sistémicos del balance (`V-DATAWAY-NNN`) | — | ✅ |
| Definir el **guion del health check E2E** (OC → despacho) | — | ✅ (capa 05) |
| Proponer el **blockchain de IdStock** deseado | — | ✅ (capa 06) |
| Ejecutar el seed (RDP/VPN, BD productiva) | ✅ | — |
| Interpretar el ZIP recibido | parcialmente (queries crudas) | ✅ (mapeo síntoma → operación → bug conocido) |

## Flujo combinado síntoma → diagnóstico

```
1. Operador reporta síntoma en producción
        ↓
2. Líder técnico llena CASE_INTAKE_TEMPLATE.md (case-seed/templates/)
        ↓
3. Ejecuta export_case_seed.ps1 desde RDP/VPN con parámetros del caso
        ↓
4. Recibe ZIP con evidencia (snapshot + movements + vw_stock_res + adjustments)
        ↓
5. Adjunta ZIP + intake al chat con el agente
        ↓
6. Agente consulta dataway-analysis/ para interpretar:
   - dataway-analysis/04-ecuacion-de-balance/
       → ¿el balance cierra? ¿qué TipoTarea está faltando?
   - dataway-analysis/01-operaciones-que-mutan-idstock/
       → ¿qué operación pudo haber generado el split observado?
   - dataway-analysis/07-correlacion-codigo-data/case-pointers/
       → ¿este producto/IdStock ya tiene un case-pointer histórico?
        ↓
7. Diagnóstico + acción correctiva
        ↓
8. Si revela bug nuevo: registrar en dataway-analysis como V-DATAWAY-NNN
   Si revela patrón nuevo de mutación: agregar operación a capa 01
   Si revela necesidad de query nueva: agregar a case-seed/queries/<CaseType>/
```

## Reglas de no-superposición

1. **No duplicar queries SQL parametrizables**. Si dataway-analysis necesita una query, debe vivir en `case-seed/queries/<CaseType>/` y dataway-analysis solo la **referencia**.
2. **No duplicar el intake template**. Existe uno en `tools/case-seed/templates/CASE_INTAKE_TEMPLATE.md` y otro en `brain/agent-context/CASE_INTAKE_TEMPLATE.md`. **Pendiente armonizar** (probable que sean versiones distintas y haya que sincronizar). Acción: tarea futura `T-DATAWAY-001 armonizar CASE_INTAKE_TEMPLATE`.
3. **case-seed no documenta lógica**. Si un colaborador necesita entender por qué cierta query SQL es relevante, debe encontrar la explicación en `dataway-analysis/`, no en case-seed.

## Extensiones sugeridas a case-seed (no incluidas en este push)

A medida que dataway-analysis identifica patrones, `case-seed` debería extenderse con sub-tipos:

```
case-seed/queries/
├── data-discrepancy/             ← ya existe
├── hh-bug/                       ← previsto en intake, vacío
├── vb-exception/                 ← previsto en intake, vacío
├── sql-perf/                     ← previsto en intake, vacío
├── idstock-lineage/              ← propuesto: reconstruir cadena N→M→P
├── particionamiento-licencia/    ← propuesto: detectar licencias con N idstocks
└── balance-gap/                  ← propuesto: queries específicas para reproducir gap
```

Estas extensiones se proponen formalmente en sub-waves siguientes una vez que dataway-analysis tenga las capas 01, 02 y 05 escritas.

## Notas

- El `CaseType` que actualmente solo tiene queries (`data-discrepancy`) es exactamente el dominio que dataway-analysis cubre. Los otros 3 (`hh-bug | vb-exception | sql-perf`) están previstos pero sin queries — representan dominios paralelos donde dataway-analysis no entra (al menos en esta wave).
- El comment `'Puntero =>` en `frmStockEnUnaFecha.vb:172` muestra que el autor del reporte tenía la analogía C++ en la cabeza desde el inicio. Esto valida que el modelo conceptual de dataway-analysis no es invención retroactiva, sino formalización de algo que ya estaba implícito.
