---
slug: 2026-05-20-inverso-codex-replicar-kpi-portal-en-bof
agente_emisor: brain-keeper (Replit)
agente_receptor: Codex local "Mary Jane" (PrograX Windows EJC)
fecha: 2026-05-20
prioridad: media
cliente: GLOBAL (portal corporativo)
repo_afectado: TOMWMS_BOF (WMSWebAPI .NET Core) y/o WikiHub Portal
branch: dev_2028_merge
area: BOF / WMSWebAPI / dashboards / replicar KPIs del portal
fuente_observada: https://tomwms-wikidev.replit.app (HEAD 2026-05-20)
metodo: reverse engineering por respuesta HTTP + bundle JS (no acceso al codigo del portal)
---

# Replicar los KPIs del WikiHub Portal en el BOF: documentacion para Mary Jane

EJC quiere que repliques en el lado BOF (probablemente en `WMSWebAPI` .NET Core) los reportes/KPIs que hoy expone el portal en `https://tomwms-wikidev.replit.app`. Te dejo el inventario real de endpoints, el shape JSON exacto, las queries SQL equivalentes que el portal hace por dentro, y una propuesta de replica en BOF con dos opciones (catalogo curado vs datos operativos reales).

## 1. Realidad importante antes de empezar

El portal **NO consulta el WMS productivo** (`TOMWMS_KILLIOS_PRD` en `52.41.114.122,1437`). Tiene su propia BD Postgres local con un **catalogo curado** de releases, clientes, modulos y tech-docs. Los numeros que muestra son el inventario editorial del Portal, no metricas operativas en tiempo real del WMS.

Cuando repliques esto en BOF, decide con EJC cual de los dos modelos:

**Modo A — Replica fiel del catalogo**: copiar la BD Postgres del portal a SQL Server WMS (tablas `release_notes`, `clients`, `modules`, `tech_docs`) y exponer los mismos endpoints. Sirve si BOF tiene que mostrar las mismas paginas que el portal pero hospedadas dentro del WMS.

**Modo B — KPIs operativos sobre datos reales del WMS**: portar los queries a las tablas reales del WMS (`tarea_hh`, `trans_re_oc`, `trans_picking_ubic`, `stock`, `stock_res`, `outbox_*`, `log_error_wms`, etc.) y mostrar metricas vivas (cuantos picking pendientes, cuantas reservas fallidas, daños vs ajustes, etc.). Esto es lo que el `data-deep-dive/CROSS-COMPARATIVA.md` ya empezo a hacer manual via snapshots SQL.

**Mi lectura**: a EJC le interesa **Modo B** (KPIs operativos), pero antes de pushear cualquier endpoint, **preguntale explicitamente** si quiere replicar el catalogo (Modo A) o levantar KPIs nuevos sobre WMS (Modo B). Este handoff cubre los dos.

## 2. Endpoints reales del portal (inventario verificado HOY)

Probe a `https://tomwms-wikidev.replit.app` con GET. Resultado HTTP en columna izquierda:

| HTTP | Bytes | Ruta | Comentario |
|---|---|---|---|
| 200 | 2,232 | `/api/stats/dashboard` | **principal**, todo el dashboard cabe aca |
| 200 | 2 | `/api/stats/by-month` | retorna `[]` actualmente (vacio, posible bug o data faltante) |
| 200 | 151 | `/api/stats/by-client` | 3 clientes con count de releases |
| 200 | 787 | `/api/clients` | lista de 11 clientes |
| 200 | 773 | `/api/modules` | lista de 10 modulos |
| 200 | 2 | `/api/release-notes/timeline` | retorna `[]` actualmente |
| 200 | 9,211 | `/api/release-notes` | catalogo completo de 26 releases con detalle |
| 200 | 1,885 | `/api/tech-docs` | lista de tech-docs (id, title, version, date, summary, tags, sectionCount) |
| 404 | — | `/api/stats/by-type`, `/api/stats/by-author`, `/api/code-changes`, `/api/code-changes/stats`, `/api/kpi`, `/api/kpis` | **NO existen**, replit.md esta desactualizado |
| 401 | — | `/api/jira`, `/api/jira/stats`, `/api/db-changes`, `/api/db-changes/stats` | requieren auth Clerk; reverse-engineering pendiente cuando EJC te de credencial |

Confirme via `grep -oE '"/api/[a-z/_:.-]+"' bundle.js` que el front del portal solo llama 6 endpoints (`/api/__clerk`, `/api/clients`, `/api/modules`, `/api/release-notes`, `/api/release-notes/timeline`, `/api/stats/by-month`, `/api/stats/dashboard`). El resto son rutas legacy mencionadas en `replit.md` §6 que ya no existen o que aun no estan migradas.

## 3. Shape exacto de cada endpoint y SQL inferido

### 3.1 `GET /api/stats/dashboard`

Respuesta observada (2026-05-20):

```json
{
  "totalReleases": 26,
  "releasesThisMonth": 0,
  "totalClients": 11,
  "totalModules": 10,
  "byProductLine": { "HH": 2, "BOF": 18, "MI3": 1, "TODOS": 5 },
  "recentActivity": [
    {
      "id": 26,
      "title": "Release v5.8.0 — Integracion Cealsa/Odoo",
      "releaseVersion": "5.8.0",
      "softwareVersion": "5.8.0",
      "productLine": "BOF",
      "publishedAt": "2024-07-20T00:00:00.000Z",
      "shortDescription": "Interface Odoo para Cealsa.",
      "status": "published",
      "featured": false,
      "clientName": "Becofarma",
      "environment": "Produccion",
      "author": "Erik Jose Calderon",
      "moduleNames": [],
      "tags": ["Odoo","Integracion","Cealsa","Becofarma","pre-factura","sincronizacion","mapeo productos","bidireccional","interface_erp","TOMWMS_BOF"]
    }
    /* ... hasta 5-10 items */
  ]
}
```

SQL Postgres equivalente (catalogo curado):

```sql
-- totalReleases
SELECT COUNT(*) FROM release_notes WHERE status = 'published';

-- releasesThisMonth
SELECT COUNT(*) FROM release_notes
WHERE status = 'published'
  AND published_at >= date_trunc('month', CURRENT_DATE)
  AND published_at <  date_trunc('month', CURRENT_DATE) + INTERVAL '1 month';

-- totalClients
SELECT COUNT(*) FROM clients WHERE active = true;

-- totalModules
SELECT COUNT(*) FROM modules;

-- byProductLine
SELECT product_line, COUNT(*) AS count
FROM release_notes
WHERE status = 'published'
GROUP BY product_line;

-- recentActivity (LEFT JOIN clients, LEFT JOIN release_modules, LEFT JOIN release_tags)
SELECT r.id, r.title, r.release_version, r.software_version, r.product_line,
       r.published_at, r.short_description, r.status, r.featured,
       c.name AS client_name, r.environment, r.author,
       COALESCE(
         (SELECT array_agg(m.name) FROM release_modules rm
          JOIN modules m ON m.id = rm.module_id WHERE rm.release_id = r.id),
         ARRAY[]::text[]
       ) AS module_names,
       COALESCE(
         (SELECT array_agg(t.tag) FROM release_tags t WHERE t.release_id = r.id),
         ARRAY[]::text[]
       ) AS tags
FROM release_notes r
LEFT JOIN clients c ON c.id = r.client_id
WHERE r.status = 'published'
ORDER BY r.published_at DESC
LIMIT 10;
```

### 3.2 `GET /api/stats/by-month`

Respuesta actual: `[]` (vacio). Shape esperado por bundle JS (inferido de `recharts` consumer):

```json
[
  { "month": "2024-07", "count": 2, "productLine": "BOF" },
  { "month": "2024-06", "count": 1, "productLine": "BOF" },
  { "month": "2024-06", "count": 1, "productLine": "HH" }
]
```

SQL:

```sql
SELECT to_char(published_at, 'YYYY-MM') AS month,
       product_line,
       COUNT(*) AS count
FROM release_notes
WHERE status = 'published'
  AND published_at >= NOW() - INTERVAL '24 months'
GROUP BY 1, 2
ORDER BY 1, 2;
```

**Bug detectado**: el endpoint devuelve `[]` aunque hay 26 releases en BD. Probable causa: filtro de fecha demasiado restrictivo (las releases datan de 2024-07 hacia atras y el filtro puede ser de los ultimos N meses con fecha presente 2026-05). Si replicas en BOF, no copies el bug — quita la ventana movil o usa un horizonte mas amplio.

### 3.3 `GET /api/stats/by-client`

Respuesta observada:

```json
[
  {"clientId":10,"clientName":"La Cumbre","count":2},
  {"clientId":11,"clientName":"IDEALSA","count":1},
  {"clientId":9,"clientName":"Becofarma","count":2}
]
```

SQL:

```sql
SELECT c.id AS client_id, c.name AS client_name, COUNT(r.id) AS count
FROM clients c
JOIN release_notes r ON r.client_id = c.id AND r.status = 'published'
GROUP BY c.id, c.name
ORDER BY count DESC;
```

**Observacion**: solo 3 de 11 clientes tienen releases asociadas. Los otros 8 quedan fuera del resultado (es INNER JOIN, no LEFT). Si en BOF queres mostrar TODOS los clientes activos con su count (incluyendo cero), cambia a `LEFT JOIN` y `COALESCE(COUNT(r.id), 0)`.

### 3.4 `GET /api/clients`

```json
[
  {"id":3,"name":"Almacenes Torres & Cia","code":"ALM-TOR","active":true},
  {"id":9,"name":"Becofarma","code":"BECO","active":true},
  ...
]
```

```sql
SELECT id, name, code, active
FROM clients
ORDER BY name;
```

### 3.5 `GET /api/modules`

```json
[
  {"id":5,"name":"Ajustes","description":"Stock adjustments and corrections"},
  {"id":10,"name":"Configuracion","description":"System configuration and parameters"},
  ...
]
```

(10 modulos: Ajustes, Configuracion, Despacho, Interface SAP, Inventario, Picking, Recepcion, Reportes, Seguridad, Ubicaciones.)

```sql
SELECT id, name, description FROM modules ORDER BY name;
```

### 3.6 `GET /api/release-notes`

Lista completa con campos extra vs `recentActivity`:

```json
{
  "id": 26, "title": "...", "releaseVersion": "...", "softwareVersion": "...",
  "productLine": "BOF", "publishedAt": "...", "shortDescription": "...",
  "status": "published", "featured": false, "clientName": "...",
  "environment": "Produccion", "author": "...",
  "moduleNames": ["Picking","Recepcion",...],   /* aca SI viene poblado */
  "tags": [...],
  "pdfUrl": null,
  "aiProcessingStatus": "completed",
  "hasAiAnalysis": true
}
```

Diferencias vs `recentActivity` del dashboard:
- `pdfUrl`, `aiProcessingStatus`, `hasAiAnalysis` **solo aca**.
- `moduleNames` viene poblado aca pero `[]` en el dashboard. Es un bug del query del dashboard (LEFT JOIN o subquery faltante).
- Sin `LIMIT` (devuelve los 26).

SQL (mismo que `recentActivity` pero sin LIMIT y con 3 campos extra):

```sql
SELECT r.id, r.title, r.release_version, r.software_version, r.product_line,
       r.published_at, r.short_description, r.status, r.featured,
       c.name AS client_name, r.environment, r.author,
       (SELECT array_agg(m.name) FROM release_modules rm
        JOIN modules m ON m.id = rm.module_id WHERE rm.release_id = r.id) AS module_names,
       (SELECT array_agg(t.tag) FROM release_tags t WHERE t.release_id = r.id) AS tags,
       r.pdf_url,
       r.ai_processing_status,
       (r.ai_analysis IS NOT NULL) AS has_ai_analysis
FROM release_notes r
LEFT JOIN clients c ON c.id = r.client_id
WHERE r.status = 'published'
ORDER BY r.published_at DESC;
```

### 3.7 `GET /api/release-notes/timeline`

Respuesta actual: `[]`. Misma sospecha que `by-month` (bug por ventana movil de fecha).

Shape esperado (inferido): array agrupado por mes/trimestre/año con releases en orden cronologico, para una vista timeline tipo carrusel vertical.

### 3.8 `GET /api/tech-docs`

```json
[
  {
    "id": "stock-reservation-system",
    "title": "Sistema de Reserva de Stock WMS",
    "version": "2.0",
    "date": "Enero-Febrero 2026",
    "project": "Refactoring VB.NET -> C# .NET 8.0",
    "database": "SQL Server 2022 - TOMWMS_KILLIOS_PRD",
    "summary": "...",
    "tags": ["C# .NET 8.0","SQL Server","Pipeline","Chain of Responsibility","FEFO","Killios"],
    "sectionCount": 16
  },
  ...
]
```

SQL (asumiendo tabla `tech_docs` con `tags JSONB`):

```sql
SELECT id, title, version, date, project, database, summary,
       tags::jsonb AS tags,
       (SELECT COUNT(*) FROM tech_doc_sections WHERE doc_id = td.id) AS section_count
FROM tech_docs td
ORDER BY date DESC;
```

`GET /api/tech-docs/:id` existe (replit.md lo menciona) pero **actualmente devuelve `{error}`** — bug pendiente del lado del portal. Cuando lo arregles, retornaria el doc completo con `sections: [...]`.

## 4. Esquema BD del portal (inferido)

Tablas Postgres locales del portal (no documentadas formalmente; inferidas del shape):

```sql
clients(id PK, name, code UNIQUE, active BOOL)
modules(id PK, name, description)
release_notes(
  id PK, title, release_version, software_version,
  product_line CHECK IN ('HH','BOF','MI3','TODOS'),
  published_at TIMESTAMPTZ,
  short_description TEXT,
  status CHECK IN ('draft','published','archived'),
  featured BOOL,
  client_id FK clients NULLABLE,
  environment TEXT, -- 'Produccion', 'QA', etc
  author TEXT,
  pdf_url TEXT NULLABLE,
  ai_processing_status TEXT, -- 'pending','processing','completed','failed'
  ai_analysis JSONB NULLABLE,
  created_at, updated_at
)
release_modules(release_id FK release_notes, module_id FK modules, PRIMARY KEY (release_id, module_id))
release_tags(release_id FK release_notes, tag TEXT, PRIMARY KEY (release_id, tag))
tech_docs(id TEXT PK, title, version, date, project, database, summary, tags JSONB)
tech_doc_sections(doc_id FK tech_docs, section_no, title, content_md)
-- pendiente revelar: jira_issues, db_changes, code_changes (auth-only)
```

## 5. Como replicarlo en `WMSWebAPI` (.NET Core) — Modo A

Capas (respeta `PATTERNS-WMSWEBAPI-LAYERS.md`):

```
Controllers/StatsController.cs
  GET /api/stats/dashboard       -> StatsService.GetDashboardAsync()
  GET /api/stats/by-month        -> StatsService.GetByMonthAsync()
  GET /api/stats/by-client       -> StatsService.GetByClientAsync()
  GET /api/release-notes         -> ReleaseNotesService.ListAsync()
  GET /api/release-notes/:id     -> ReleaseNotesService.GetAsync(id)
  GET /api/release-notes/timeline -> ReleaseNotesService.GetTimelineAsync()
  GET /api/clients               -> ClientsService.ListAsync()
  GET /api/modules               -> ModulesService.ListAsync()
  GET /api/tech-docs             -> TechDocsService.ListAsync()
  GET /api/tech-docs/:id         -> TechDocsService.GetAsync(id)

Services/{Stats,ReleaseNotes,Clients,Modules,TechDocs}Service.cs
  composicion + reglas (filtro published, fecha, etc.)

DALCore/{ReleaseNotes,Clients,Modules,TechDocs}/cls*.cs
  queries puntuales (un metodo, una query)

EntityCore/{ReleaseNote,Client,Module,TechDoc}{,Dto}.cs
  POCOs

Be/{ReleaseNote,Client,Module,TechDoc}Be.cs (compat legacy si aplica)
```

Tablas SQL Server WMS para Modo A (replica del catalogo):

```sql
CREATE TABLE dbo.release_notes (
  Id INT IDENTITY PRIMARY KEY,
  Title NVARCHAR(255) NOT NULL,
  ReleaseVersion NVARCHAR(50) NOT NULL,
  SoftwareVersion NVARCHAR(50) NOT NULL,
  ProductLine NVARCHAR(10) NOT NULL CHECK (ProductLine IN ('HH','BOF','MI3','TODOS')),
  PublishedAt DATETIME2 NOT NULL,
  ShortDescription NVARCHAR(MAX),
  Status NVARCHAR(20) NOT NULL CHECK (Status IN ('draft','published','archived')),
  Featured BIT NOT NULL DEFAULT 0,
  IdCliente INT NULL FOREIGN KEY REFERENCES dbo.cliente(IdCliente),
  Environment NVARCHAR(50),
  Author NVARCHAR(255),
  PdfUrl NVARCHAR(500) NULL,
  AiProcessingStatus NVARCHAR(20),
  AiAnalysis NVARCHAR(MAX) NULL, -- JSON
  CreadoEn DATETIME2 DEFAULT SYSDATETIME(),
  ActualizadoEn DATETIME2 DEFAULT SYSDATETIME()
);
CREATE INDEX IX_release_notes_status_published ON dbo.release_notes(Status, PublishedAt DESC);
CREATE INDEX IX_release_notes_cliente ON dbo.release_notes(IdCliente);

CREATE TABLE dbo.modulo_wms (IdModulo INT IDENTITY PK, Nombre NVARCHAR(100), Descripcion NVARCHAR(MAX));
CREATE TABLE dbo.release_modulo (IdRelease INT, IdModulo INT, PRIMARY KEY (IdRelease, IdModulo));
CREATE TABLE dbo.release_tag (IdRelease INT, Tag NVARCHAR(100), PRIMARY KEY (IdRelease, Tag));

CREATE TABLE dbo.tech_doc (
  IdDoc NVARCHAR(100) PRIMARY KEY,
  Title NVARCHAR(255), Version NVARCHAR(50), Fecha NVARCHAR(100),
  Proyecto NVARCHAR(255), DbName NVARCHAR(100), Summary NVARCHAR(MAX),
  Tags NVARCHAR(MAX) -- JSON
);
CREATE TABLE dbo.tech_doc_seccion (IdDoc NVARCHAR(100), NoSeccion INT, Titulo NVARCHAR(255), ContenidoMd NVARCHAR(MAX), PRIMARY KEY (IdDoc, NoSeccion));
```

Convencion de nombres: el WMS usa snake_case y prefijo de familia (`trans_*`, `i_nav_*`, `cliente`, etc.). Para no romper estilo, usa `release_notes` (no `ReleaseNotes`) pero respeta PascalCase en columnas si esa es la convencion existente en el cliente. **Confirma con EJC antes de crear las tablas.**

Contrato de respuesta Forma A (segun `PATTERNS-WMSWEBAPI-LAYERS.md` y regla #2 de `replit.md`):

```csharp
return Ok(new {
    data = await statsService.GetDashboardAsync(ct),
    error = (string?)null
});
// y en catch:
return StatusCode(500, new { data = (object?)null, error = ex.Message });
```

DTO de ejemplo (`EntityCore/Stats/DashboardDto.cs`):

```csharp
public class DashboardDto {
    public int TotalReleases { get; set; }
    public int ReleasesThisMonth { get; set; }
    public int TotalClients { get; set; }
    public int TotalModules { get; set; }
    public Dictionary<string, int> ByProductLine { get; set; } = new();
    public List<RecentReleaseDto> RecentActivity { get; set; } = new();
}
public class RecentReleaseDto {
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string ReleaseVersion { get; set; } = "";
    public string SoftwareVersion { get; set; } = "";
    public string ProductLine { get; set; } = "";
    public DateTime PublishedAt { get; set; }
    public string? ShortDescription { get; set; }
    public string Status { get; set; } = "";
    public bool Featured { get; set; }
    public string? ClientName { get; set; }
    public string? Environment { get; set; }
    public string? Author { get; set; }
    public List<string> ModuleNames { get; set; } = new();
    public List<string> Tags { get; set; } = new();
}
```

Sigue **camelCase** en JSON (como el portal hace). Si `WMSWebAPI` usa `Newtonsoft.Json`, agrega `[JsonProperty("byProductLine")]` o configura `CamelCasePropertyNamesContractResolver`. Confirma el contrato actual.

## 6. Como replicarlo en BOF — Modo B (KPIs operativos sobre WMS real)

Si EJC prefiere KPIs operativos en vez de catalogo, mapping sugerido:

| KPI portal | KPI operativo equivalente | Tabla WMS | Query base |
|---|---|---|---|
| `totalReleases` | Releases instaladas en este cliente | `cliente_release` (si existe) o tabla `version_app` | `SELECT COUNT(DISTINCT version) FROM ...` |
| `releasesThisMonth` | Releases del mes en curso | igual | filtrar `MONTH(fecha_instalacion)` |
| `totalClients` | Bodegas activas en el cliente actual | `bodega` | `SELECT COUNT(*) FROM bodega WHERE activo = 1` |
| `totalModules` | Modulos habilitados via `i_nav_config_enc` | `i_nav_config_enc` | `SELECT COUNT(DISTINCT modulo) FROM i_nav_config_enc` |
| `byProductLine` (HH/BOF/MI3) | Distribucion de actividad operativa: tareas HH vs operaciones BOF vs llamadas MI3 | `tarea_hh`, `log_error_wms`, `outbox_*` | 3 sub-queries con UNION |
| `recentActivity` | Ultimas 10 transacciones grandes (`trans_re_oc`, `trans_pe_enc`, `trans_picking_ubic`) | familias `trans_*` | UNION ALL ordenado por timestamp |
| `by-month` | Volumen operativo por mes | mismas familias `trans_*` | `GROUP BY YEAR, MONTH` |
| `by-client` | (no aplica directo: WMS productivo es mono-cliente por BD) | — | reemplazar por `by-bodega` |
| `tech-docs` | Documentos vivos del cliente (manuales, procesos) | nueva tabla `wms_doc` (si no existe) | crear |

Para tener un ejemplo concreto, este KPI cruzado vale la pena:

```sql
-- Daños vs ajustes pendientes (BUG-001 que ya documentaste en CROSS-COMPARATIVA.md)
SELECT
  (SELECT COUNT(*) FROM trans_da_enc) AS total_danios,
  (SELECT COUNT(*) FROM trans_aj_enc WHERE tipo_documento = 'AJCANTN') AS total_ajustes_negativos,
  (SELECT COUNT(*) FROM trans_aj_enc WHERE tipo_documento = 'AJCANTN' AND Enviado_A_ERP = 0) AS ajustes_pendientes_erp;
```

Eso es exactamente el caso que `data-deep-dive/CROSS-COMPARATIVA.md` reporta como problema cross-cliente. Si BOF expone esto como KPI permanente, EJC ve el bug sin tener que correr snapshots manuales.

## 7. Bugs del portal que NO debes copiar

1. **`/api/stats/by-month` retorna `[]`** aunque hay 26 releases publicadas. Probable filtro de fecha incorrecto (ventana movil). En BOF: usar horizonte amplio o quitar el filtro.

2. **`/api/release-notes/timeline` retorna `[]`** por la misma razon.

3. **`recentActivity` del dashboard no pobla `moduleNames`** (siempre `[]`) pero `/api/release-notes` si lo pobla. El dashboard tiene un query con `JOIN` o subquery faltante. En BOF: una sola funcion `BuildReleaseDto(reader)` reutilizada por ambos endpoints elimina el bug.

4. **`recentActivity` muestra `clientName: "Becofarma"` para la release "Cealsa/Odoo"** (v5.8.0) — sospechoso, parece que el `client_id` esta mal asignado en BD. No es bug de query, es bug de dato. En BOF: validar que `client_id` coincida con el tag dominante.

5. **Endpoints listados en `replit.md` §6 que NO existen** (`/api/tech-docs/:id` devuelve `{error}`, `/api/db-changes` requiere auth y no esta documentado, `/api/stats/by-type` y `/api/stats/by-author` son 404). Cuando hagas la replica BOF, **no prometas estos endpoints hasta que los implementes**.

## 8. Auth (cuando lleguemos a Jira / DB-changes)

El portal usa **Clerk** (`/api/__clerk` aparece en el bundle). `/api/jira*` y `/api/db-changes*` devuelven `401` sin sesion. Para hacer reverse engineering de esos endpoints necesito que EJC me pase un token Clerk valido del portal o me ejecute un `curl` autenticado y me pegue la respuesta. Mientras tanto, este handoff cubre la parte publica.

En BOF, si replicas estos endpoints, **usa el mismo esquema de auth que ya usa `WMSWebAPI`** (probablemente JWT propio o Windows Auth). No introduzcas Clerk en el BOF — el WMS no necesita SaaS de auth.

## 9. Convencion de tipado y fechas

- El portal devuelve `publishedAt` en ISO 8601 UTC con sufijo `.000Z` (`"2024-07-20T00:00:00.000Z"`). SQL Server: usa `DATETIME2` y serializa con `o` format (`yyyy-MM-ddTHH:mm:ss.fffZ`) o configura `DateTimeKind.Utc` antes de `JsonConvert.SerializeObject`.
- `releasesThisMonth = 0` con `publishedAt` mas reciente = `2024-07-20`. Confirma con EJC que el portal usa fecha REAL (calendario actual) y no `date_trunc` sobre algo congelado.

## 10. Pedido concreto

Mary Jane, cuando termines el detalle que estas en, te pido:

1. **Hablalo con EJC primero**: Modo A (replica catalogo) o Modo B (KPIs operativos). No empieces a tirar tablas sin esa decision.

2. **Si Modo A**: arma el plan de tablas SQL Server segun §5, esqueleto de `StatsController` + `StatsService` + `clsLnReleaseNotes` (DAL) en `WMSWebAPI` y mandame un draft para revisar antes de pushear. **NO** ejecutes el `CREATE TABLE` en `TOMWMS_KILLIOS_PRD` sin permiso explicito de EJC.

3. **Si Modo B**: arrancamos por el dashboard operativo del cliente activo (Killios). Yo te paso las queries SQL ya validadas de `data-deep-dive/killios/snapshot-2026-05-05.md`. Vos las envolves en `WMSStatsService.GetClientDashboardAsync()` y devolves Forma A.

4. **No copies los 5 bugs del §7**. Si tu replica los reproduce, el handoff fallo.

5. **No uses Clerk en BOF**. Cuando lleguemos a Jira/DB-changes, usaremos el auth nativo de `WMSWebAPI`.

6. Cuando este listo el primer endpoint, ejecuta y devolve la respuesta como JSON pegado en un handoff de vuelta. Yo lo comparo byte-a-byte con la respuesta del portal y te marco diferencias.

## 11. Referencias

- Endpoints verificados HOY: `https://tomwms-wikidev.replit.app` HEAD 2026-05-20
- Bundle JS analizado: `/assets/index-HjkFSegD.js` (1.95 MB)
- Capas backend: `code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md`
- Snapshots operativos cross-cliente: `data-deep-dive/CROSS-COMPARATIVA.md` + por cliente en `data-deep-dive/<cliente>/`
- Catalogo de estados HH (si necesitas para KPI de tareas): `reference/catalogo-tarea-hh-estados.md`
- `replit.md` §6 esta **desactualizado**: lista endpoints que ya no existen o devuelven error. Actualizar despues de que valides cuales sobreviven.

---

Cualquier duda con el shape JSON, llamame. Tengo los 8 JSON descargados crudos en `/tmp/portal-kpi/` (ambiente Replit), si necesitas que te pase alguno especifico lo subo al brain.

— brain-keeper (Replit)
