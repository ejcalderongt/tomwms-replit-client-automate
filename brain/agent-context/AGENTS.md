---
id: AGENTS
tipo: agent-context
estado: vigente
titulo: AGENTS.md — Contexto del proyecto WMS para agentes locales
ramas: [dev_2028_merge]
tags: [agent-context]
---

# AGENTS.md — Contexto del proyecto WMS para agentes locales

> **Cómo usar este archivo**: copialo (o symlinkealo) a la raíz del repo donde
> corre tu agente local. Los nombres aceptados según herramienta:
>
> | Herramienta            | Nombre del archivo en la raíz |
> |------------------------|-------------------------------|
> | Claude Code            | `CLAUDE.md`                   |
> | OpenCode               | `AGENTS.md`                   |
> | Cursor                 | `.cursorrules`                |
> | Aider                  | `CONVENTIONS.md`              |
> | Cualquier otro         | `AGENTS.md`                   |
>
> El contenido es el mismo. Si trabajás con varios repos, dejá uno en cada raíz.

---

## Identidad y entorno

Sos un agente técnico que trabaja en el proyecto **WMS de Erik Calderón
(PrograX24)**. Stack: **Visual Studio 2026, DevExpress 24, SQL Server**.

### Repos

| Repo            | Lenguaje  | Rol                                                              |
|-----------------|-----------|------------------------------------------------------------------|
| `TOMWMS_BOF`    | VB.NET    | Backend core: DAL, Entity, WebMethods `.asmx`. **El cerebro.**   |
| `TOMHH2025`     | Java      | Frontend Android para terminales handheld (HH).                  |
| `TOMWMS_DBA`   | T-SQL     | Schema y SPs SQL Server productivos.                             |

Branch activa: `dev_2028_merge`.

### Equipo

EJC (Erik), GT, AG, MA, AT, MECR, CF.

---

## Reglas duras (no negociables)

1. **No commits automáticos.** Erik los revisa y los hace él.
2. **No mezclar HH con VB** en un mismo cambio / commit / PR.
3. **No tocar `Reference.vb`** — son proxies SOAP autogenerados; la
   implementación real vive en `*.asmx.vb`.
4. **No reescribir desde cero.** Debuggear primero. Siempre.
5. **UTF-8 con BOM en VB.** Mantener `ñ` y acentos. Nunca guardar como ANSI.
6. **Nunca loguear ni imprimir** `WMS_KILLIOS_DB_PASSWORD` ni
   `BRAIN_IMPORT_TOKEN`. Pasarlos por referencia (`$env:VAR`), nunca el valor.
7. **La conexión a KILLIOS prod es solo lectura** desde este agente.
   Cualquier write lo hace Erik con el procedimiento normal.
8. **El módulo `definition` del catálogo SQL es sensible.** No lo expongas a
   logs ni respuestas que vayan al cliente final.

---

## Herramienta principal — TOMWMS Brain

API privada que indexa los 3 repos + el catálogo SQL productivo. Devuelve
relaciones cross-layer (Java HH → VB WebMethod → SP/tabla SQL).

```
BRAIN_BASE_URL=https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain
```

### Endpoints (GET, sin token)

| Endpoint           | Para qué                                           |
|--------------------|----------------------------------------------------|
| `/health`          | Sanity check. Devuelve totales de símbolos/refs.   |
| `/search`          | Encontrar el símbolo exacto por nombre + filtros.  |
| `/impact`          | Quién depende de mí (callers, blast radius).       |
| `/dependencies`    | Qué necesito yo (callees, upstream).               |
| `/writers`         | Quién escribe esta tabla/SP (insert/update/delete/merge/exec). |

### Endpoints de escritura (POST, requieren `X-Brain-Token`)

| Endpoint                    | Para qué                                  |
|-----------------------------|-------------------------------------------|
| `/import/sql-catalog`       | Subir el catálogo extraído por el script local. |
| `/index/vb`                 | Re-indexar VB tras cambios.               |
| `/index/java`               | Re-indexar Java HH tras cambios.          |
| `/repos/sync`               | Pull de los repos en el server Brain.     |

---

## Workflow obligatorio según el caso

### Caso 1 — Cambio en SP, vista, tabla o columna SQL
```bash
# 1. Localizar el símbolo
curl "$BRAIN_BASE_URL/search?q=VW_Stock_Res&kind=sql-view"

# 2. Blast radius — qué se rompe si lo toco?
curl "$BRAIN_BASE_URL/impact?symbol=VW_Stock_Res&depth=2"

# 3. Si hay writers, listar quién escribe (para entender contratos)
curl "$BRAIN_BASE_URL/writers?symbol=stock&op=update,delete,merge,insert&kind=sql-table"

# 4. Hacer el cambio. Re-correr el extractor SQL local (ver más abajo).
```

### Caso 2 — Refactor de un método VB con muchos llamadores
```bash
curl "$BRAIN_BASE_URL/search?q=Actualizar_Stock&kind=vb-method"
curl "$BRAIN_BASE_URL/impact?symbol=Actualizar_Stock&depth=3"
# Tras el cambio:
curl -X POST "$BRAIN_BASE_URL/index/vb" -H "X-Brain-Token: $BRAIN_IMPORT_TOKEN" \
  -H "Content-Type: application/json" -d '{"repos":["TOMWMS_BOF"]}'
```

### Caso 3 — Rename de un WebMethod (impacto Java HH)
```bash
# Antes del rename, listar Java callers
curl "$BRAIN_BASE_URL/impact?symbol=Get_Ubicacion_By_Codigo_Barra_And_IdBodega&depth=2"
# Renombrar en VB *.asmx.vb (NO en Reference.vb), luego renombrar también
# en TODOS los Java callers que aparecieron, luego re-indexar ambos.
```

### Caso 4 — Discrepancia de datos en producción ("este SKU está mal")
```bash
# Quiénes pueden haber tocado la tabla involucrada?
curl "$BRAIN_BASE_URL/writers?symbol=stock&op=update,delete,merge&kind=sql-table"
# Cruzar con la fecha del Kardex y el método que se ejecutó esa fecha.
# Validar la hipótesis con queries de solo lectura sobre KILLIOS prod.
```

---

## Cuándo NO usar el Brain

- Cambios cosméticos de UI (form layout, labels, colores).
- Edits aislados que no tocan datos ni firmas públicas.
- Bugfixes de lógica interna de un único método sin callers externos.
- Tocar `Reference.vb` (no se debe tocar nunca, ya está dicho).

---

## Conexión a la BD productiva KILLIOS (solo lectura)

```
Server   = 52.41.114.122,1437
Database = TOMWMS_KILLIOS_PRD
User     = wmsuser
Password = $env:WMS_KILLIOS_DB_PASSWORD
```

ConnectionString:
```
Server=52.41.114.122,1437;Database=TOMWMS_KILLIOS_PRD;User Id=wmsuser;Password=$env:WMS_KILLIOS_DB_PASSWORD;Encrypt=no;
```

Desde sqlcmd:
```bash
sqlcmd -S "52.41.114.122,1437" -d TOMWMS_KILLIOS_PRD -U wmsuser \
       -P $env:WMS_KILLIOS_DB_PASSWORD -Q "SELECT TOP 10 * FROM stock"
```

> **Importante**: cualquier query que ejecutes contra esta BD debe ser
> `SELECT`. Si necesitás escribir, parar y avisarle a Erik.

---

## Variables de entorno requeridas

PowerShell (sesión actual):
```powershell
$env:BRAIN_BASE_URL          = "https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain"
$env:BRAIN_IMPORT_TOKEN      = "<pegar valor del panel de Secrets de Replit>"
$env:WMS_KILLIOS_DB_HOST     = "52.41.114.122,1437"
$env:WMS_KILLIOS_DB_NAME     = "TOMWMS_KILLIOS_PRD"
$env:WMS_KILLIOS_DB_USER     = "wmsuser"
$env:WMS_KILLIOS_DB_PASSWORD = "<el mismo que usás en SSMS>"
$env:AZURE_DEVOPS_PAT        = "<solo si el agente clona/pulls los repos>"
```

Para que persistan entre sesiones (abre nueva terminal después):
```powershell
setx BRAIN_BASE_URL          "https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain"
setx BRAIN_IMPORT_TOKEN      "<valor>"
setx WMS_KILLIOS_DB_HOST     "52.41.114.122,1437"
setx WMS_KILLIOS_DB_NAME     "TOMWMS_KILLIOS_PRD"
setx WMS_KILLIOS_DB_USER     "wmsuser"
setx WMS_KILLIOS_DB_PASSWORD "<valor>"
```

---

## Re-extraer y subir el catálogo SQL al Brain

Ejecutar tras cambios de schema en producción (nuevo SP, vista modificada,
columna nueva). El extractor corre **local** porque la BD prod está detrás del
firewall de Erik.

```powershell
cd <repo>\tools\sql-catalog
pip install pyodbc requests          # una sola vez

python extract_sql_catalog.py `
    --server   $env:WMS_KILLIOS_DB_HOST `
    --database $env:WMS_KILLIOS_DB_NAME `
    --user     $env:WMS_KILLIOS_DB_USER `
    --upload   "$env:BRAIN_BASE_URL/import/sql-catalog"
```

El script lee la password de `$env:WMS_KILLIOS_DB_PASSWORD` y el token de
`$env:BRAIN_IMPORT_TOKEN`. Idempotente: cada run reemplaza el catálogo del
repo virtual `TOMWMS_KILLIOS_PRD`.

---

## Limitaciones conocidas del Brain

- **SP refs por variable** en VB: solo se capturan los SP cuyo nombre aparece
  como string literal. Los que se construyen en variable no salen como
  referencia.
- **`Reference.vb`** aparece en los resultados (es código indexable); ignoralo.
- **127 dependencias ambiguas** en el catálogo SQL (refs cross-DB o dinámicas).
  Es esperable.
- **OUTPUT clauses, CTEs que escriben, y SQL dinámico** no están cubiertos por
  el Movement Tracer; si encontrás un caso real, avisar para extender el parser.

---

## Antes de cerrar una tarea

1. ¿Re-indexaste el repo afectado? (`POST /index/vb` o `/index/java`).
2. ¿Verificaste el `/impact` post-cambio para confirmar que no quedaron
   referencias colgantes?
3. ¿Dejaste el código en UTF-8 con BOM?
4. ¿Documentaste el cambio en el ticket / nota interna?
5. **No commits automáticos.** Pasale el patch a Erik.
